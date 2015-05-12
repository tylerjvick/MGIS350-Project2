using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MGIS350_Project2.Properties;

namespace MGIS350_Project2
{
    static class DbInterface
    {

        // Initialize string to handle all message box text
        private static string ErrorString { get; set; }

        // Initialize type to handle message box error buttons
        private static MessageBoxButtons ErrorButtons { get; set; }

        // Method to display messagebox on error
        private static void MessageBoxError()
        {
            // Show messagebox with current string and any set error buttons
            MessageBox.Show(ErrorString, @"Database Error", ErrorButtons);
            // Set the display string and messagebox button to default values
            ErrorString = "";
            ErrorButtons = MessageBoxButtons.OK;
        }

        private static string DefaultConnection()
        {
            // Application stored DB connection string
            return Settings.Default.TylerDBConnectionString;
        }

        // Method to initiate database connection
        private static SqlCommand OpenConnectionAndExecute(string query, string connectionName = null)
        {
            // Use the stored connection string if none was passed
            string connectionString = connectionName ?? DefaultConnection();
            // Ensure we have a connection string
            if (connectionString == null)
            {
                throw new Exception("No connection string found");
            }
            // Initialize database connection with passed query
            SqlCommand command = new SqlCommand(query, new SqlConnection(connectionString));
            // The given command is a query in string form
            command.CommandType = CommandType.Text;
            // Wait 1 minute before giving up
            command.CommandTimeout = 60;
            try
            {
                // Open the database connection
                command.Connection.Open();
            }
            catch (Exception)
            {
                // Append string to messagebox error dialog
                ErrorString += "Connection to database failed\n";
                //System.Windows.Forms.MessageBox.Show(@"Connection to database failed");
            }
            // Return the SqlCommand data type
            return command;
        }

        private static void CloseConnection(SqlCommand sqlCommand)
        {
            // If a connection exists and is open
            if (sqlCommand != null && sqlCommand.Connection.State == ConnectionState.Open)
            {
                // Close the connection
                sqlCommand.Connection.Close();
            }
        }

        private static int CreateNewOrder(int itemCount)
        {
            // declare int to return order id
            int newOrderNum = 0;
            // Query to insert a new order into the orders table
            string orderQueryInsert = @"INSERT INTO Orders (Item_Count) " +
                                   "VALUES (" + itemCount + "); " +
                                   "SELECT @@IDENTITY FROM Orders;";
            try
            {
                // Open the DB connection and run the query
                SqlCommand newOrder = OpenConnectionAndExecute(query: orderQueryInsert);
                // Start the data reader
                SqlDataReader reader = newOrder.ExecuteReader();
                // While data is returned
                while (reader.Read())
                {
                    // Get the ID of the newly inserted order
                    newOrderNum = Convert.ToInt32(reader[0]);
                }
                // Close the database connection
                CloseConnection(newOrder);
            }
            catch (Exception)
            {
                // Append string to messagebox dialog
                ErrorString += "Failed to insert new order into the database.\nPlease try again.\n";
                // Set messagebox error buttons to yes/no
                //this still needs functionality
                //ErrorButtons = MessageBoxButtons.YesNo;
                // Show messagebox for error
                MessageBoxError();

                //System.Windows.Forms.MessageBox.Show(
                //    "Failed to insert new order into the database.\nWould you like to retry?",
                //    @"Database Connection Failed", System.Windows.Forms.MessageBoxButtons.YesNo);

                newOrderNum = -1;
                //throw;
            }
            // Return the new Order ID
            return newOrderNum;

        }

        public static void InsertOrder(List<string> order)
        {
            // Insert and get the order ID for the current order
            var orderId = CreateNewOrder(order.Count);
            if (orderId < 0)
            {
                return;
            }
            // Create list of items, cast each "pizza" into
            //list of individual ingredients (originally separated by ', ')
            List<List<string>> orderItemList = order.Select(item => new List<string>(Regex.Split(item.ToString(),@",\s"))).ToList();
            // initialize the count of all "pizzas"
            int itemCount = 0;
            // Loop through each "pizza" in the order
            //*This could probably be simplified*
            orderItemList.ForEach(delegate(List<string> item)
            {
                // Iterate through each ingredient in the pizza
                item.ForEach(delegate(string ingredient)
                {
                    // Construct query with the new order id, current "pizza" number
                    //and the current ingredient name - which is used to return the ingredient ID
                    string itemQuery = @"INSERT INTO Item_List (Order_ID, Item_Number, Ingredient_ID) VALUES " +
                        " (" + orderId + "," + (itemCount + 1) + ", " +
                        "(SELECT Ingredient_ID FROM Ingredients WHERE Ingredient_Name = '" + ingredient + "'))";
                    // Open the DB connection and pass the query for the given ingredient
                    SqlCommand newItemLine = OpenConnectionAndExecute(query: itemQuery);
                    // Execute the query, we don't need any returned data
                    //so this is a "non-query"
                    newItemLine.ExecuteNonQuery();
                    // Close the database connection
                    CloseConnection(newItemLine);
                });
                // Increment the item (pizza) count
                //i.e. move to the next pizza in the order
                itemCount++;
            });
        }

        private static List<string> RetrieveItem(string orderId, int itemCount)
        {
            // Declare the list of all "pizzas" in a given order
            List<string> itemsInOrder = new List<string>();
            // Loop through each "pizza" in the order
            for (int i = 1; i <= itemCount; i++)
            {
                // Construct the query for all ingredient names in the current pizza
                var itemLineQuery = string.Format(@"SELECT Ingredient_Name " +
                                    "FROM Ingredients " +
                                    "WHERE Ingredient_ID IN ( " +
                                    "SELECT Ingredient_ID " +
                                    "FROM Item_List " +
                                    "WHERE Order_ID = {0} " +
                                    "AND Item_Number = {1});", orderId, i);
                // Open the database connection with the current pizza
                SqlCommand singleItem = OpenConnectionAndExecute(query: itemLineQuery);
                // Execute the query and start reading returned data
                SqlDataReader ingredientReader = singleItem.ExecuteReader();
                // Create list to hold each ingredient in the pizza
                List<string> individualIngredient = new List<string>();
                // Loop through reader data for each row (ingredient name) returned
                while (ingredientReader.Read())
                {
                    // Add the ingredient name to the ingredient list,
                    //Remove any whitespace on either side of the ingredient name string
                    individualIngredient.Add(ingredientReader[0].ToString().Trim());
                }
                // Close the database connection
                CloseConnection(singleItem);
                // Join all ingredients in the pizza as a comma-separated string
                //add the "Pizza" to the list of all pizzas in the given order
                itemsInOrder.Add(String.Join(", ", individualIngredient));
            }
            // Return the list of each pizza's ingredients in the order
            return itemsInOrder;
        }

        public static Dictionary<string, List<string>> OrderHistory()
        {
            // Initialize dictionary for all orders and their respective pizzas
            Dictionary<string, List<string>> orderHistoryDictionary = new Dictionary<string, List<string>>();
            // Query for every order in the Orders table
            var allOrdersQuery = @"SELECT * FROM Orders";
            try
            {
                // Open the database connection and pass the query for all orders
                SqlCommand allOrders = OpenConnectionAndExecute(query: allOrdersQuery);
                // Execute the query and begin reading data
                SqlDataReader reader = allOrders.ExecuteReader();
                // Loop through all orders returned by the database
                while (reader.Read())
                {
                    // Initialize string to contain the returned order id
                    var orderId = reader[0].ToString();
                    // Initialize string with order id number and the timestamp of the order
                    //This is what will be displayed in the "Past Orders" listbox
                    var orderLine = string.Format(@"Order ID {0} | {1}", orderId, reader[1].ToString());
                    // Initialize the count of "pizzas" in the current order
                    var itemCount = Convert.ToInt32(reader[2]);
                    // Get a list of strings for every pizza's ingredients
                    List<string> itemIngredients = RetrieveItem(orderId, itemCount);
                    // Add the order and pizzas to the dictionary
                    orderHistoryDictionary.Add(orderLine, itemIngredients);
                }
                // Close the database connection
                CloseConnection(allOrders);
            }
            catch (Exception)
            {
                // Append string to messagebox dialog
                ErrorString += "Cannot retrieve orders from the database\n";
                // Show messagebox for error
                MessageBoxError();

                //throw;
            }
            // Return the entire order history with every pizza
            //A better approach - Only return the "pizzas" for the currently selected order
            return orderHistoryDictionary;

        }

    }
}

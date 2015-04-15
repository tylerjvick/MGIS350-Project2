using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using MGIS350_Project2.Properties;

/*
 * Creator: Tyler Vick
 * Name: MGIS350-Project 2
 * Description: Project 2 for Developing Business Applications
 *              at RIT Spring 2015
 * Date: 04/14/2015
 * Extra Features:  Inventory values will persist between sessions
 *                  using Application Properties Settings.
 *                  Inventory values will update upon adding to order,
 *                  but if an order is cancelled or the form is closed
 *                  without placing, the values will return to the inventory.
 */

namespace MGIS350_Project2
{
    public partial class Form1 : Form
    {
        // Global variables
        // Working dictionary of quantity
        //of each ingredient in the current order
        private readonly Dictionary<string, double> _dictOrder = new Dictionary<string, double>();
        // Working dictionary of quantity
        //of each ingredient in inventory
        private readonly Dictionary<string, double> _dictInventory = new Dictionary<string, double>();
        // Settings reference to get/set inventory
        readonly Settings _settings = Settings.Default;

        public Form1()
        {
            InitializeComponent();
            // Add listeners on all order variables
            //to ensure inventory is available on any change
            rdoLarge.CheckedChanged += FormChanged;
            rdoMed.CheckedChanged += FormChanged;
            rdoSmall.CheckedChanged += FormChanged;
            chkExCheese.CheckedChanged += FormChanged;
            chkMushrooms.CheckedChanged += FormChanged;
            chkPepperoni.CheckedChanged += FormChanged;
            chkSausage.CheckedChanged += FormChanged;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Gather all saved ingredients and
            //add to working inventory dictionary
            _dictInventory.Add("Dough", _settings.Dough);
            _dictInventory.Add("Sauce", _settings.Sauce);
            _dictInventory.Add("Cheese", _settings.Cheese);
            _dictInventory.Add("Pepperoni", _settings.Pepperoni);
            _dictInventory.Add("Mushrooms", _settings.Mushrooms);
            _dictInventory.Add("Sausage", _settings.Sausage);

            // The dynamic way to load settings
            //though this automatically retrieves values in alpha order...
            //foreach (SettingsProperty currentProperty in _settings.Properties)
            //{
            //    Console.WriteLine(currentProperty.Name);
            //    // E.g. Add string "Dough" with quantity double of 0.0
            //    _dictInventory.Add(currentProperty.Name, Convert.ToDouble(_settings[currentProperty.Name]));
            //}

            // Call method to redraw inventory list
            //using updated inventory values
            UpdateInventory();

        }

        private void FormChanged(object sender, EventArgs e)
        {
            // Linq expression to get radio control
            //within the grpSize group box
            var checkedSize = grpSize.Controls.OfType<RadioButton>()
                .FirstOrDefault(p => p.Checked);
            // Ensure a radio button is active
            //This is fallback, as the initial state is always rdoLarge enabled
            if (checkedSize != null)
            {
                // Call function to ensure inventory levels
                //satisfies all required ingredients from the order
                var isInvAvailable = CheckIngredients();
                if (isInvAvailable == false)
                {
                    // If CheckIngredients returns false
                    //disable the Add to Order button
                    btnAddOrder.Enabled = false;
                }
                else
                {
                    // Else CheckIngredients is true
                    //so the Add to Order button is enabled
                    btnAddOrder.Enabled = true;
                }
            }

        }

        // This method returns a boolean determining
        //if the available inventory meets or exceeds
        //the ingredients required by the currently selected size&ingredients
        //plus any inventory already added to an order.
        private bool CheckIngredients()
        {
            // Call method to return dictionary
            //of constants given the currently selected
            //pizza size.
            var ingredientsRequired = GetRequiredIngredients();

            // Initialize list of ingredients needed
            //for the current pizza.
            // Here we assume that every pizza will at least require
            //Dough, Sauce, and Cheese.
            List<string> selectedIngredients = new List<string> { "Dough", "Sauce", "Cheese" };
            // Linq expression to get all values from the Toppings group
            //and add to the list of ingredients for current pizza.
            selectedIngredients.AddRange(from Control c in grpTopping.Controls let box = c as CheckBox where (box != null) && box.Checked select c.Text);
            // Iterate through each ingredient needed
            //for current pizza.
            foreach (var selectedIngredient in selectedIngredients)
            {
                // We have an exception for the "Extra Cheese" topping
                //since this topping uses the already existing Cheese ingredient
                if (selectedIngredient == "Extra Cheese")
                {
                    // If Extra Cheese is desired
                    //the current inventory of Cheese must not be less than
                    //the default amount of Cheese PLUS the amount of cheese required
                    //for the Extra Cheese topping
                    if (_dictInventory["Cheese"] < ingredientsRequired["ExtraCheese"] + ingredientsRequired["Cheese"])
                    {
                        // If there is not enough cheese to allow for the desired Extra Cheese,
                        //return false;
                        return false;
                    }
                }
                // For all other selected ingredients,
                //if the inventory of the ingredient is less than
                //the required amount
                else if (_dictInventory[selectedIngredient] < ingredientsRequired[selectedIngredient])
                {
                    // The ingredient doesn't satisfy the order
                    //so we return false
                    return false;
                }
            }
            // We have looped through all ingredients
            //and none have returned false.
            // Meaning we have sufficient inventory for needed ingredients
            return true;
        }

        // Method to prompt and handle clearing the current order items
        private bool CancelOrder(string message, string caption)
        {
            // Declare buttons for prompt as Yes/No choice
            const MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            // Display prompt with message & caption arguments
            //display Exclamation in messagebox, and select 'No' as the default button
            var result = MessageBox.Show(this, message, caption, buttons,
                MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

            // If user clicks 'No'
            if (result == DialogResult.No)
            {
                // Exit method returning true
                //this does not modify any other form values
                return true;
            }
            // User clicks 'Yes'
            else
            {
                // Loop through all ingredients in the current order
                foreach (var ingredient in _dictOrder)
                {
                    // Add ingredient quantity from order to
                    //current inventory for the given ingredient
                    _dictInventory[ingredient.Key] += _dictOrder[ingredient.Key];
                }
                // Clear the current order values
                _dictOrder.Clear();
                // Clear all items in the order preview list
                lstOrder.Items.Clear();
                // Update the inventory list to reflect new values
                UpdateInventory();
                // Save the application settings
                _settings.Save();
                // Exit method returning false
                return false;
            }
        }

        // Method to redraw inventory list
        private void UpdateInventory()
        {
            // Create new list named 'units'
            //from Constants class
            IList<KeyValuePair<string, string>> units = Constants.Units;
            // Convert readonly IList to dictionary
            Dictionary<string, string> unitList = units.ToDictionary(i => i.Key, i => i.Value);
            // Set default index of inventory list box
            //in case somehow there is no selection already
            var selectedIndex = 0;
            // If any item in the inventory is selected
            if (lstInventory.SelectedItem != null)
            {
                // Assign index value to the currently selected row index
                selectedIndex = lstInventory.SelectedIndex;
            }
            // Clear the inventory list
            lstInventory.Items.Clear();
            // Loop through all ingredients in the inventory dictionary
            foreach (KeyValuePair<string, double> ingredient in _dictInventory)
            {
                // Declare unit of ingredient equal to
                //value of ingredient name in the unitList dictionary
                // e.g. 'oz' or 'lbs'
                var ingredientUnit = unitList[ingredient.Key];
                // Format string to include Ingredient Name + Inventory Value + Ingredient Unit
                lstInventory.Items.Add(string.Format(@"{0} {1} {2}", ingredient.Key, ingredient.Value, ingredientUnit));
                // Write the given inventory value to
                //persistent application settings
                //... this is an extra feature
                _settings[ingredient.Key] = ingredient.Value;

            }
            // Re-select the list item at the original index
            lstInventory.SelectedIndex = selectedIndex;
            // Call method to check inventory values
            FormChanged(this, null);
        }

        // This method returns a dictionary of
        //required quantity of ingredients
        //for a given size
        private Dictionary<string, double> GetRequiredIngredients()
        {
            // Initialize a new dictionary
            Dictionary<string, double> ingredientsRequired = new Dictionary<string, double>();
            // Linq expression to get checked radio button
            //in grpSize Group Box
            var checkedSize = grpSize.Controls.OfType<RadioButton>()
                .FirstOrDefault(p => p.Checked);
            // If a radio button was checked
            if (checkedSize != null)
            {
                // Get text of checked radio button as string
                var size = checkedSize.Text;
                // Get type of Constants class from namespace as string
                var tp = typeof(Constants).ToString();
                // Declare new type variable equal to
                //the constants class Data type
                Type constants = Type.GetType(tp);
                // If the constants datatype is valid/exists
                if (constants != null)
                {
                    // Get the MethodInfo variable for the selected pizza size
                    MethodInfo staticMethodInfo = constants.GetMethod(size);
                    // Invoke the defined method, set result equal to
                    //a readonly IList with each constant value for the given ingredient
                    var ingredientsReadOnly = (IList<KeyValuePair<string, double>>)staticMethodInfo.Invoke(null, null);
                    // Convert the IList to a valid dictionary
                    ingredientsRequired = ingredientsReadOnly.ToDictionary(i => i.Key, i => i.Value);
                }
                // Return the full dictionary of size constants
                //Could be a redundant return..
                //but we want to catch and return an empty dictionary if no method was found
                return ingredientsRequired;
            }
            // return empty dictionary if no method for the given size was found
            return ingredientsRequired;
        }

        private void btnAddInv_Click(object sender, EventArgs e)
        {
            // First ensure an ingredient in the inventory listbox is selected
            if (lstInventory.SelectedItem != null)
            {
                // Get the ingredient currently selected
                //and convert to string
                var selectedItem = lstInventory.SelectedItem.ToString();
                // Trim the quantity and units of listbox item
                //we only want the NAME of the ingredient
                var selectedIngredient = selectedItem.Substring(0, selectedItem.IndexOf(" ", StringComparison.Ordinal));
                // Get the desired quantity to add
                //as qtyToAdd with the double type
                var qtyToAdd = Convert.ToDouble(nudAddInv.Value);
                // If this change does not result in a negative ingredient value
                if ((_dictInventory[selectedIngredient] + qtyToAdd) >= 0)
                {
                    // Add the nud value to the selected ingredient
                    //(This also allows negative values, provided the result
                    //will remain non-negative)
                    _dictInventory[selectedIngredient] += qtyToAdd;
                }
                else // The result of the calculation is negative
                {
                    // Set the selected ingredient to 0
                    _dictInventory[selectedIngredient] = 0;
                }

            }
            // Finally, update the inventory listbox
            //via method to reflect inventory changes
            UpdateInventory();
            // Save the application settings
            _settings.Save();
        }

        // Method that handles the "Add to Order" button click
        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            // Initialize the empty string for the ordered pizza
            var strOrderAdd = "";
            // Call method to get all required ingredients
            var ingredientsRequired = GetRequiredIngredients();

            // Get currently selected pizza size
            var size = grpSize.Controls.OfType<RadioButton>()
                .FirstOrDefault(p => p.Checked);
            // As long a size is selected,
            //make our order string equal to the size (plus a trailing space)
            if (size != null)
                strOrderAdd = size.Text + " ";

            // Initialize list of ingredients needed
            //for the current pizza.
            // Here we assume that every pizza will at least require
            //Dough, Sauce, and Cheese.
            List<string> selectedIngredients = new List<string> { "Dough", "Sauce", "Cheese" };
            // Linq expression to get all values from the Toppings group
            //and add to the list of ingredients for current pizza.
            selectedIngredients.AddRange(from Control c in grpTopping.Controls let box = c as CheckBox where (box != null) && box.Checked select c.Text);

            // For each ingredient in our pizza
            //we must subtract the required value from
            //the current inventory levels
            foreach (var selectedIngredient in selectedIngredients)
            {
                // Exception for Extra Cheese topping.
                if (selectedIngredient == "Extra Cheese")
                {
                    // Subtract Extra Cheese ingredient requirement
                    //from the Cheese inventory.
                    //Since the cheese inventory is shared between Cheese and Extra Cheese.
                    _dictInventory["Cheese"] -= ingredientsRequired["ExtraCheese"];
                }
                // For all other selected ingredients in the inventory
                else if (_dictInventory.ContainsKey(selectedIngredient))
                    // Subtract the required ingredient number from the current inventory
                    _dictInventory[selectedIngredient] -= ingredientsRequired[selectedIngredient];

                // Next, we update the current order dictionary with the required ingredients

                // Should the ingredient already be in one of the items in our order
                if (_dictOrder.ContainsKey(selectedIngredient))
                {
                    // Add the required value of this ingredient to the respective
                    //existing ingredient in the current order values
                    _dictOrder[selectedIngredient] += ingredientsRequired[selectedIngredient];
                }
                // The ingredient has not yet been added
                //to the current order.
                else
                {
                    // Exception for Extra Cheese
                    if (selectedIngredient == "Extra Cheese")
                    {
                        // If we already require cheese in the order.
                        //..the alternate is unlikely since all orders require cheese
                        //but on the off chance Extra Cheese is processed before Cheese,
                        //we need this check
                        if (_dictOrder.ContainsKey("Cheese"))
                        {
                            // Add the Extra Cheese required amount
                            //to the total Cheese required by the order
                            _dictOrder["Cheese"] += ingredientsRequired["ExtraCheese"];
                        }
                        // Somehow we have a pizza that has Extra Cheese, but no Cheese..
                        else
                        {
                            // Declare the Cheese total for the order
                            //and assign the initial value of the required Extra Cheese
                            _dictOrder.Add("Cheese", ingredientsRequired["ExtraCheese"]);
                        }
                    }
                    // For all other ingredients
                    else
                    {
                        // Declare the ingredient total for the order
                        //and assign the initial value of the required value of the ingredient
                        //for the current item.
                        _dictOrder.Add(selectedIngredient, ingredientsRequired[selectedIngredient]);
                    }
                }
            }

            // Loop through all toppings that are checked
            foreach (Control c in grpTopping.Controls)
            {
                // If topping is checked (and exists)
                if ((c is CheckBox) && ((CheckBox)c).Checked)
                    // Append the topping text to the order string
                    strOrderAdd += c.Text + " ";
            }
            // Add the full order string to the order preview list box
            lstOrder.Items.Add(strOrderAdd);

            // Finally, update the inventory list
            //to reflect inventory minus the amount needed
            //for the current order.

            // NOTE: This approach has been discussed. Since we are able to cancel orders
            //and subsequently add inventory back, it is not a problem to update inventory
            //at this point. (Prior to the order being placed)
            UpdateInventory();
        }

        // Method that handles "Place Order" click
        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (lstOrder.Items.Count > 0)
            {
                // Linq expression to gather all items
                //within the order and assign to string
                var orderMessage = lstOrder.Items.Cast<object>()
                    // Get each item in order preview
                    //prepend with "Pizza #: "
                    .Select((t, i) => string.Format("Pizza {0}: {1}\n", i + 1, t.ToString()))
                    // Combine all items into dialog message
                    .Aggregate("", (current, itemList) => current + itemList);
                // Display dialog that lists all items on current order
                MessageBox.Show(orderMessage, @"Your Order");

                // Clear the order dictionary and order listbox
                //now that the order is complete
                _dictOrder.Clear();
                lstOrder.Items.Clear();
                // And redraw inventory... to comply with requirements
                UpdateInventory();
                // Save the application settings
                _settings.Save();
            }
        }

        // This method handles the "Cancel Order" button click
        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            // Check to ensure we have at least one item in the order
            if (lstOrder.Items.Count > 0)
            {
                // Create prompt message for cancel button
                string message = Resources.CancelPrompt;
                // Create message box title
                const string caption = @"Confirm Order Cancellation!";
                // Call method to display prompt
                CancelOrder(message, caption);
            }
        }

        // Method to handle application closing
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Check to see if we have any items in order
            if (lstOrder.Items.Count > 0)
            {
                // Create prompt message from saved string resource
                string message = Resources.UnsavedPrompt;
                // Create message box title
                const string caption = @"Unsaved Order!";
                // Set event cancel to returned boolean from method
                //if method returns true, don't close the form.
                e.Cancel = CancelOrder(message, caption);
            }
        }

    }

}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace MGIS350_Project2
{
    static class OrderHistory
    {

        private static String DefaultConnection()
        {
            return Properties.Settings.Default.MGIS350GroupConnectionString;
        }

        public static SqlCommand OpenCommand(String storedProcedure, List<SqlParameter> sqlParameters,
            String connectionName)
        {
            string settings = connectionName ?? DefaultConnection();
            if (settings == null)
            {
                throw new Exception("No connection string registered!");
            }

            SqlCommand command = new SqlCommand(storedProcedure, new SqlConnection(settings));

            command.CommandType = CommandType.StoredProcedure;

            if (sqlParameters != null)
            {
                foreach (SqlParameter parameter in sqlParameters)
                {
                    if (parameter.Value == null)
                    {
                        parameter.Value = DBNull.Value;
                    }
                }

                command.Parameters.AddRange(sqlParameters.ToArray());
            }

            // Set the max time to wait for a response
            command.CommandTimeout = 60;
            command.Connection.Open();

            return command;
        }

        public static void CloseCommand(SqlCommand sqlCommand)
        {
            if (sqlCommand != null) // Might need to add && check for connection being open
            {
                sqlCommand.Connection.Close();
            }
        }


        public static Int32 InsertOrder(String storedProcedure, List<SqlParameter> parameters, String connectionName)
        {
            SqlCommand sqlCommand = null;
            Int32 affectedRows = 0;
            try
            {
                sqlCommand = OpenCommand(storedProcedure: storedProcedure, sqlParameters: parameters,
                    connectionName: connectionName);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                string error = err.ToString();
                Console.WriteLine(error);
            }
            finally
            {
                CloseCommand(sqlCommand);
            }
            return affectedRows;
        }


        //public void ArchiveOrder()
        //{
        //    string connectionString = Properties.Settings.Default.MGIS350GroupConnectionString;

        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        // Open SQL connection
        //        con.Open();

        //        using (SqlCommand command = new SqlCommand("SELECT TOP 2 * FROM Ingredients", con))
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                Console.WriteLine("{0} {1}", reader.GetInt32(0), reader.GetString(1));
        //            }
        //        }

        //        con.Close();

        //    }

        //}

    }
}

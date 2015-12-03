using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace JobEngine
{
    public class Database
    {
        public enum Validation { Connecton, Query, Data }

        private bool recallSql = false;
        private string connectString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sam\Documents\GitHub\eal\jobboard\JobEngine\Case.mdf;Integrated Security=True";
        private int numRows = 0;

        public bool FastRecall
        {
            get
            {
                return recallSql;
            }
            set
            {
                recallSql = value;
            }
        }

        public string Query { get; set; }

        public Dictionary<string, object> Parameters = null;

        public int Rows
        {
            get
            {
                return numRows;
            }
        }


        public Database(string query)
        {
            // Set SQL query
            Query = query;
        }

        public Database(string query, Dictionary<string, object> param)
        {
            // Set SQL query
            Query = query;
            // Set Query Parameters
            Parameters = param;
        }

        public Database(string query, Dictionary<string, object> param, bool recall)
        {
            // Set SQL query
            Query = query;
            // Set Query Parameters
            Parameters = param;
            // Set Recall for Prepare query in server for faster execution (same query)
            recallSql = recall;
        }

        /// <summary>
        /// Execute General SQL Query
        /// if needed for execute stored procedure use Database.Procedure() instead
        /// </summary>
        public void Execute()
        {
            // Running SQL query
            try
            {
                ExecuteSql(CommandType.Text);
            }
            catch (Exception exc)
            {
                Error(exc);
            }
        }

        /// <summary>
        /// Execute Stored Procedure
        /// </summary>
        public void Procedure()
        {
            // Running SQL query
            try
            {
                ExecuteSql(CommandType.StoredProcedure);
            }
            catch (Exception exc)
            {
                Error(exc);
            }
        }

        public List<Dictionary<string, object>> Fetch()
        {
            try
            {
                return Fetching(CommandType.Text);
            }
            catch (Exception exc)
            {
                Error(exc);
                return null;
            }
        }

        public List<Dictionary<string, object>> FetchProcedure()
        {
            try
            {
                return Fetching(CommandType.StoredProcedure);
            }
            catch (Exception exc)
            {
                Error(exc);
                return null;
            }
        }

        private void Error(Exception exc)
        {
            Console.WriteLine("SQL Error: {0}", exc.Message);
        }

        private void ExecuteSql(CommandType type)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                // Validation SQL Query
                CheckState();

                // Open Database Connection
                connect.Open();

                using (SqlCommand command = new SqlCommand(Query, connect))
                {
                    // Prepare statement
                    if (Parameters != null)
                    {
                        foreach (KeyValuePair<string, object> item in Parameters)
                        {
                            command.Parameters.AddWithValue("@" + item.Key, item.Value);
                        }
                    }

                    // Query type
                    if (type != CommandType.Text)
                    {
                        command.CommandType = type;
                    }

                    // Stored query
                    if (recallSql)
                    {
                        command.Prepare();
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        private List<Dictionary<string, object>> Fetching(CommandType type)
        {
            // Database connection
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                // Validation Query string
                CheckState();

                // Open Database Connection
                connect.Open();

                // SQL Commands
                using (SqlCommand command = new SqlCommand(Query, connect))
                {
                    // Prepare statement
                    if (Parameters != null)
                    {
                        foreach (KeyValuePair<string, object> item in Parameters)
                        {
                            command.Parameters.AddWithValue("@" + item.Key, item.Value);
                        }
                    }

                    // Query type
                    if (type != CommandType.Text)
                    {
                        command.CommandType = type;
                    }

                    // Stored query
                    if (recallSql)
                    {
                        command.Prepare();
                    }

                    // SQL Data results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Prepare data list
                        List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>> { };

                        // Reading SQL data
                        while (reader.Read())
                        {
                            // Prepare Associative array
                            Dictionary<string, object> item = new Dictionary<string, object> { };

                            // Adding SQL result in to array
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                item.Add(reader.GetName(i), reader.GetValue(i));
                            }

                            // Adding to data list
                            dataList.Add(item);

                            // Increase number of rows
                            numRows++;
                        }

                        if(dataList.Count == 0)
                        {
                            return null;
                        }
                        else
                        {
                            return dataList;
                        }
                    }
                }
            }
        }

        private void CheckState()
        {
            if (Query == null)
            {
                throw new Exception("Empty query");
            }
        }

        private void CheckState(Validation valid)
        {
            if (valid == Validation.Query && Query == null)
            {
                throw new Exception("Empty query");
            }
            else if (valid == Validation.Connecton && connectString == null)
            {
                throw new Exception("Empty connection string");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace JobEngine
{
    public class Db
    {
        private string localDb = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sam\Documents\GitHub\eal\jobboard\JobEngine\Case.mdf;Integrated Security=True";
        private int numRows = 0;

        public string SqlQuery { get; set; }

        public Dictionary<string, object> Parameters { get; set; }

        public int Rows
        {
            get
            {
                return numRows;
            }
        }

        public string ConnectData
        {
            get
            {
                return localDb;
            }
            set
            {
                localDb = value;
            }
        }


        public Db()
        {

        }

        public Db(string query)
        {
            Query(query);
        }

        public Db(string query, Dictionary<string, object> param)
        {
            Query(query, param);
        }

        public void Query(string query)
        {
            SqlQuery = query;
        }

        public void Query(string query, Dictionary<string, object> param)
        {
            SqlQuery = query;
            Parameters = param;
        }

        public bool Execute()
        {
            if(SqlQuery.Length == 0)
            {
                throw new Exception("Need to set SQL query before execution");
            }
            else
            {
                return ExecuteSql(GetCommandType());
            }
        }

        public bool Execute(CommandType type)
        {
            if (SqlQuery.Length == 0)
            {
                throw new Exception("Need to set SQL query before execution");
            }
            else
            {
                return ExecuteSql(type);
            }
        }

        public List<Dictionary<string, object>> Fetch()
        {
            if(SqlQuery.Length == 0)
            {
                throw new Exception("Need to set SQL query before fetching data");
            }
            else
            {
                return FetchList();
            }
        }

        private bool ExecuteSql(CommandType type)
        {
            using (SqlConnection connect = new SqlConnection(ConnectData))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        // Open connection
                        connect.Open();

                        command.CommandText = SqlQuery;
                        command.CommandType = type;
                        command.Connection = connect;

                        // Prepare Statement
                        if (Parameters.Count != 0)
                        {
                            foreach (KeyValuePair<string, object> item in Parameters)
                            {
                                command.Parameters.AddWithValue("@" + item.Key, item.Value);
                            }
                        }

                        // Stored procedure
                        if (type == CommandType.StoredProcedure)
                        {
                            command.Prepare();
                        }

                        // Execute statement
                        numRows = command.ExecuteNonQuery();

                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        private List<Dictionary<string, object>> FetchList()
        {
            using (SqlConnection connect = new SqlConnection(ConnectData))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.CommandText = SqlQuery;
                        command.CommandType = GetCommandType();
                        command.Connection = connect;

                        // Prepare Statement
                        if (Parameters.Count != 0)
                        {
                            foreach (KeyValuePair<string, object> item in Parameters)
                            {
                                command.Parameters.AddWithValue("@" + item.Key, item.Value);
                            }
                        }

                        // Open connection
                        connect.Open();

                        // Stored procedure
                        if (command.CommandType == CommandType.StoredProcedure)
                        {
                            command.Prepare();
                        }

                        // Prepare Sql Data result
                        using (SqlDataReader data = command.ExecuteReader())
                        {
                            // Prepare data list
                            List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>> { };

                            // Reading SQL data
                            while (data.Read())
                            {
                                // Prepare Associative array
                                Dictionary<string, object> item = new Dictionary<string, object> { };

                                // Adding SQL result in to array
                                for (int i = 0; i < data.FieldCount; i++)
                                {
                                    item.Add(data.GetName(i), data.GetValue(i));
                                }

                                // Adding to data list
                                dataList.Add(item);

                                // Increase number of rows
                                numRows++;
                            }

                            return dataList;
                        }
                    }
                    catch
                    {
                        return new List<Dictionary<string, object>> { };
                    }
                }
            }
        }

        private CommandType GetCommandType()
        {
            if(Regex.IsMatch(SqlQuery.ToUpper(), "^(?:SELECT|INSERT INTO|UPDATE|DELETE FROM)"))
            {
                return CommandType.Text;
            }
            else
            {
                return CommandType.StoredProcedure;
            }
        }
    }
}

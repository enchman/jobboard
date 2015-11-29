using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Timers;

namespace JobLib
{
    public class Database
    {
        const int TIMEOUT = 20;

        private string connectData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sam\Documents\GitHub\eal\JobBoard\JobLib\JobData.mdf;Integrated Security=True";
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader data;
        private int dataSize = 0;


        public Database()
        {
            // Do SQL Connection
            this.Establish();
        }

        public Database(string query)
        {
            // Do SQL Connection
            this.Establish();

            // Run SQL Query
            this.Query(query);
        }

        public Database(string query, Dictionary<string, object> param)
        {
            // Do SQL Connection
            this.Establish();

            // Run SQL query with binding parameters
            this.Query(query, param);
        }


        public void Query(string query)
        {
            try
            {
                this.command = new SqlCommand(query, connection);
                // Prepare statement
                this.command.Prepare();
                this.data = this.command.ExecuteReader();
            }
            catch
            {
                this.command = new SqlCommand(query, connection);
                this.data = this.command.ExecuteReader();
            }
        }
        public void Query(string query, Dictionary<string, object> param)
        {
            try
            {
                this.command = new SqlCommand(query, connection);
                foreach (KeyValuePair<string, object> item in param)
                {
                    this.command.Parameters.AddWithValue("@" + item.Key, item.Value);
                }

                // Store precedure for reuse
                this.command.Prepare();
                this.data = this.command.ExecuteReader();
            }
            catch
            {
                this.command = new SqlCommand(query, connection);
                foreach (KeyValuePair<string, object> item in param)
                {
                    this.command.Parameters.AddWithValue("@" + item.Key, item.Value);
                }

                this.data = this.command.ExecuteReader();
            }
        }

        public List<Dictionary<string, object>> Fetch()
        {
            try
            {
                List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>> { };
                while(this.data.Read())
                {
                    Dictionary<string, object> item = new Dictionary<string, object> { };
                    for (int i = 0; i < this.data.FieldCount; i++)
                    {
                        item.Add(this.data.GetName(i), this.data.GetValue(i));
                    }
                    dataList.Add(item);
                }

                return dataList;
            }
            catch (Exception exc)
            {
                Error(exc);
                return new List<Dictionary<string, object>> { };
            }
        }

        #region Local Functionality
        private void Establish()
        {
            try
            {
                // Making connection to SQL
                this.connection = new SqlConnection(connectData);
                this.connection.Open();
            }
            catch (Exception exc)
            {
                // Critical error on connection, display in console
                Error(exc);
            }
        }

        private SqlDbType GetDataType(object value)
        {
            if (value.GetType() == typeof(int))
            {
                this.dataSize = sizeof(int);
                return SqlDbType.Int;
            }
            else if (value.GetType() == typeof(string))
            {
                string data = value as string;
                this.dataSize = data.Length;
                if (data.Length < 8000)
                {
                    return SqlDbType.VarChar;
                }
                else
                {
                    return SqlDbType.Text;
                }
            }
            else if (value.GetType() == typeof(byte))
            {
                this.dataSize = sizeof(byte);
                return SqlDbType.TinyInt;
            }
            else if (value.GetType() == typeof(short))
            {
                this.dataSize = sizeof(short);
                return SqlDbType.SmallInt;
            }
            else if (value.GetType() == typeof(long))
            {
                this.dataSize = sizeof(long);
                return SqlDbType.BigInt;
            }
            else if (value.GetType() == typeof(double))
            {
                this.dataSize = sizeof(double);
                return SqlDbType.Float;
            }
            else if (value.GetType() == typeof(decimal))
            {
                this.dataSize = sizeof(decimal);
                return SqlDbType.Decimal;
            }
            else if (value.GetType() == typeof(DateTime))
            {
                this.dataSize = 8;
                return SqlDbType.DateTime;
            }
            else
            {
                throw new ArgumentException("Cannot match data type for this object");
            }
        }

        private void Error(Exception exc)
        {
            StackTrace trace = new StackTrace(exc, true);
            StackFrame frame = trace.GetFrame(0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Line: {0}", frame.GetFileLineNumber());
            Console.WriteLine("Message: {0}", exc.Message);
            Console.ResetColor();
        }

        private void Done()
        {
            // Close connection
            this.connection.Close();
            
            if(this.data != null)
            {
                try
                {
                    this.data.Close();
                }
                catch
                {
                    
                }
            }
        }

        #endregion
    }
}

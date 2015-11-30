using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Timers;
namespace JobEngine
{
    class Database
    {
        private string connectData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sam\Documents\GitHub\eal\jobboard\JobEngine\Database.mdf;Integrated Security=True";
        private bool status = false;
        private int numRows = 0;
        private SqlConnection Connection;
        private SqlCommand Command;
        private SqlDataReader Data;
        private Timer timeout;
        private int duration = 20000;

        public bool Success
        {
            get
            {
                return status;
            }
        }

        public int Rows
        {
            get
            {
                return numRows;
            }
        }

        public Database(string query, Dictionary<string, object> param)
        {

        }

        

        public bool Query(string query)
        {
            try
            {
                // Open connection
                Connection.Open();

                // Execute SQL Query
                Command = new SqlCommand(query, Connection);
                Data = Command.ExecuteReader();

                // Set SQL status
                status = true;

                // Initiate Auto Dispose
                Determinate();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Query(string query, Dictionary<string, object> param)
        {
            try
            {
                // Open connection
                Connection.Open();

                // Prepare statement
                Command = new SqlCommand(query, Connection);
                foreach (KeyValuePair<string, object> item in param)
                {
                    Command.Parameters.AddWithValue("@" + item.Key, item.Value);
                }

                // Execute SQL Query
                Data = Command.ExecuteReader();

                // Set SQL status
                status = true;

                // Initiate Auto Dispose
                Determinate();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Dictionary<string, object>> Fetch()
        {
            try
            {
                // Prepare data list
                List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>> { };

                // Reading SQL data
                while (Data.Read())
                {
                    // Prepare Associative array
                    Dictionary<string, object> item = new Dictionary<string, object> { };

                    // Adding SQL result in to array
                    for (int i = 0; i < Data.FieldCount; i++)
                    {
                        item.Add(Data.GetName(i), Data.GetValue(i));
                    }

                    // Adding to data list
                    dataList.Add(item);

                    // Increase number of rows
                    numRows++;
                }
                Done();
                return dataList;
            }
            catch
            {
                Done();
                return new List<Dictionary<string, object>> { };
            }
        }

        public void Done()
        {
            if(Command != null)
            {
                Command.Dispose();
            }
            if(Data != null)
            {
                Data.Close();
            }
            if(Connection != null)
            {
                Connection.Close();
                Connection.Dispose();
            }
            ClearTimer();
        }

        public void Done(object source, ElapsedEventArgs e)
        {
            if (Command != null)
            {
                Command.Dispose();
            }
            if (Data != null)
            {
                Data.Close();
            }
            if (Connection != null)
            {
                Connection.Dispose();
                Connection.Close();
            }
            ClearTimer();
        }

        /// <summary>
        /// Auto Dispose object(Self destruction)
        /// If the object is not manually dispose, within 20 seconds Timer will trig Object Determination
        /// </summary>
        private void Determinate()
        {
            timeout = new Timer(duration);
            timeout.Elapsed += Done;
            timeout.AutoReset = false;
            timeout.Enabled = true;
        }

        private void ClearTimer()
        {
            timeout.Stop();
            timeout.Dispose();
        }

        private void Establish()
        {
            try
            {
                Connection = new SqlConnection(connectData);
            }
            catch
            {

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobEngine
{
    class Db
    {
        private string localDb = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sam\Documents\GitHub\eal\jobboard\JobEngine\Database.mdf;Integrated Security=True";

        public string SqlQuery { get; set; }

        public Dictionary<string, object> Parameters { get; set; }

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

        public void Execute()
        {

        }

        public void Query()
        {

        }

        public List<Dictionary<string, object>> Fetch()
        {
            return new List<Dictionary<string, object>> { };
        }
    }
}

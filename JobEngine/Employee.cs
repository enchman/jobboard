using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobEngine
{
    public class Employee
    {
        #region Properties
        private int employeeId = 0;

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }

        public int Id
        {
            get
            {
                return employeeId;
            }
        }

        

        #endregion

        #region Constructor
        public Employee(int id)
        {
            employeeId = id;
        }

        public Employee(string firstname)
        {
            Firstname = firstname;
        }

        public Employee(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }
        #endregion

        public void Sync()
        {
            
        }

        public void Add()
        {
            if(Id == 0 && Firstname != null && Lastname != null && Username != null)
            {
                Dictionary<string, object> param = new Dictionary<string, object> { };
                param.Add("user", Username);
                param.Add("username", Firstname);
                param.Add("lastname", Lastname);

                Database db = new Database("setEmployee", param);
                Load(db.Fetch());
            }
        }

        public void Edit()
        {

        }

        public void Remove()
        {

        }

        private void Load(List<Dictionary<string, object>> data)
        {
            if(data.Count != 0)
            {
                Dictionary<string, object> source = data.ElementAt(0);
                employeeId = (int)source["id"];
                Username = source["user"].ToString();
                Firstname = source["firstname"].ToString();
                Lastname = source["lastname"].ToString();
            }
        }
    }
}

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
        private int activeJob = -1;
        private string userName;

        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Username
        {
            get
            {
                return userName;
            }
        }

        public int Id
        {
            get
            {
                return employeeId;
            }
        }

        public int CurrentTaskId
        {
            get
            {
                return activeJob;
            }
            set
            {
                activeJob = value;
            }
        }

        public List<Job> Joblist { get; set; }

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

        public Employee(int id, string firstname, string lastname, string username)
        {
            employeeId = id;
            Firstname = firstname;
            Lastname = lastname;
            userName = username;
        }

        public Employee(int id, string firstname, string lastname, string username, List<Job> joblist)
        {
            employeeId = id;
            Firstname = firstname;
            Lastname = lastname;
            userName = username;
            Joblist = joblist;
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
                param.Add("firstname", Firstname);
                param.Add("lastname", Lastname);
                param.Add("user", Username);

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

        public static List<Employee> GetEmployees()
        {
            try
            {
                Database db = new Database("SELECT * FROM [employees] ORDER BY [firstname] ASC");
                List<Dictionary<string, object>> datalist = db.Fetch();

                if(datalist != null)
                {
                    List<Employee> workers = new List<Employee> { };
                    foreach (Dictionary<string, object> item in datalist)
                    {
                        // Initiate Employee informations
                        int id = (int)item["id"];
                        string firstname = (string)item["firstname"];
                        string lastname = (string)item["lastname"];
                        string user = (string)item["username"];

                        Employee worker = new Employee(id, firstname, lastname, user);

                        // Load task for each Employee
                        Dictionary<string, object> param = new Dictionary<string, object> { };
                        param.Add("id", id);
                        List<Dictionary<string, object>> block = new Database("getTask", param).FetchProcedure();

                        // Checking if Employee have any task
                        if (block != null)
                        {
                            foreach (Dictionary<string, object> jobdata in block)
                            {
                                int mid = (int)jobdata["machineId"];
                                int eid = (int)jobdata["employeeId"];
                                DateTime aDate = (DateTime)jobdata["activeDate"];
                                DateTime dDate = (DateTime)jobdata["deadline"];
                                bool jid = (bool)jobdata["completed"];

                                worker.Joblist.Add(new Job(mid, eid, aDate, dDate, jid));
                            }
                        }

                        // Add Employee to list
                        workers.Add(worker);
                    }

                    return workers;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exc)
            {
                Log.Record(exc);
                return null;
            }
        }

        private void Load(List<Dictionary<string, object>> data)
        {
            if(data.Count != 0)
            {
                Dictionary<string, object> source = data.ElementAt(0);
                employeeId = (int)source["id"];
                userName = (string)source["user"];
                Firstname = (string)source["firstname"];
                Lastname = (string)source["lastname"];

                // Load task for each Employee
                Dictionary<string, object> param = new Dictionary<string, object> { };
                param.Add("id", employeeId);
                List<Dictionary<string, object>> block = new Database("getTask", param).FetchProcedure();

                // Checking if Employee have any task
                if (block != null)
                {
                    foreach (Dictionary<string, object> jobdata in block)
                    {
                        int mid = (int)jobdata["machineId"];
                        int eid = (int)jobdata["employeeId"];
                        DateTime aDate = (DateTime)jobdata["activeDate"];
                        DateTime dDate = (DateTime)jobdata["deadline"];
                        bool jid = (bool)jobdata["completed"];

                        Joblist.Add(new Job(mid, eid, aDate, dDate, jid));
                    }
                }
            }
        }
    }
}

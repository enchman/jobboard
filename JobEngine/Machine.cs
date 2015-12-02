using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace JobEngine
{
    public class Machine
    {
        /// <summary>
        /// Time cycle of work process in milisecond
        /// </summary>
        const int TIME_CYCLE = 1000;

        #region Properties

        private int machineId = 0;
        private string machineName = null;
        private int machineDuration = 0;
        private int numParts = 0;
        private Timer cycle;

        public int Id
        {
            get
            {
                return machineId;
            }
        }

        public string Name
        {
            get
            {
                return machineName;
            }
        }

        public int Duration
        {
            get
            {
                return machineDuration;
            }
        }

        public int Part
        {
            get
            {
                return numParts;
            }
        }

        public Employee Worker { get; set; }
        #endregion

        #region Constructor
        public Machine()
        {

        }

        public Machine(int id, string name, int durable)
        {
            machineId = id;
            machineName = name;
            machineDuration = durable;
        }
        #endregion

        public void Sync()
        {

        }

        public void Start()
        {
            if(cycle == null)
            {

            }
        }

        public void Stop()
        {

        }

        public static List<Machine> GetMachines()
        {
            Database db = new Database("SELECT * FROM [machines]");
            List<Dictionary<string,object>> dataList = db.Fetch();
            if(dataList != null)
            {
                List<Machine> result = new List<Machine> { };
                foreach(Dictionary<string, object> item in dataList)
                {
                    int id = (int)item["id"];
                    int duration = (int)item["duration"];
                    int number = (int)item["amount"];
                    string name = (string)item["name"];

                    for(int i = 0; i < number; i++)
                    {
                        result.Add(new Machine(id, name, duration));
                    }
                }

                if(result.Count != 0)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}

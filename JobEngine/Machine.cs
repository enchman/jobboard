using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace JobEngine
{
    public class Machine: Operation
    {
        #region Properties

        /// <summary>
        /// Time process of work process in milisecond
        /// </summary>
        private readonly int ClockCycle = 1000;
        private readonly int OverWork = 30;
        private Timer process = null;

        public Employee Worker { get; set; }

        #endregion

        #region Constructor
        public Machine()
        {

        }

        public Machine(Employee employee)
        {
            Worker = employee;
        }

        #endregion

        public void Start()
        {
            if(process == null)
            {
                process = new Timer(base.Duration * ClockCycle);
            }
            else
            {
                process.Enabled = true;
            }
        }

        public void Pause()
        {
            if(process != null)
            {
                process.Enabled = false;
            }
        }

        public void Stop()
        {
            if(process != null)
            {
                // Stop event
                process.Enabled = false;
                // Releasing resources
                process.Stop();
                // Disposing object
                process.Dispose();
                // Nullify Timer object
                process = null;
            }
        }

        public bool CheckWorkerShift(Employee worker)
        {
            if(worker.Joblist != null)
            {
                if(worker.Joblist.Any(x => CheckTime(x.TaskDate)))
                {
                    Worker = worker;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void WorkLapse(object source, ElapsedEventArgs e)
        {
            if(Worker != null && CheckTime(e.SignalTime))
            {
                base.numParts++;
                // Need to check employee is complete the task, if so nullify Machine.Worker

            }
            else
            {
                // Stop Machine
                Stop();
            }
        }

        private bool CheckTime(DateTime date)
        {
            if(date.DayOfYear == DateTime.Now.DayOfYear)
            {
                if (Production.EndHour == date.Hour && Production.EndMinute >= date.Minute)
                {
                    return true;
                }
                else if (Production.StartHour < date.Hour && Production.EndHour > date.Hour)
                {
                    return true;
                }
                else if (Production.StartHour <= date.Hour && Production.EndHour > date.Hour)
                {
                    return true;
                }
                else if ((Production.StartHour == date.Hour && Production.StartMinute >= date.Minute) && Production.EndHour > date.Hour)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static List<Operation> GetMachines()
        {
            Database db = new Database("SELECT * FROM [machines]");
            List<Dictionary<string,object>> dataList = db.Fetch();
            if(dataList != null)
            {
                List<Operation> result = new List<Operation> { };
                foreach(Dictionary<string, object> item in dataList)
                {
                    int id = (int)item["id"];
                    int duration = (int)item["duration"];
                    int number = (int)item["amount"];
                    string name = (string)item["name"];
                    result.Add(new Operation(id, name, duration, number));
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

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
        public enum Status { Stop, Acitve, Pause }

        /// <summary>
        /// Time process of work process in milisecond
        /// </summary>
        private readonly int ClockCycle = 1000;

        private Employee currentWorker = null;
        private Timer process = null;
        private Status activity = Status.Stop;

        
        public Status State
        {
            get
            {
                return activity;
            }
        }

        public Employee Worker
        {
            get
            {
                return currentWorker;
            }
            set
            {
                // Verify Employee Shift
                int index = value.Joblist.FindIndex(x => CheckTime(x.TaskDate));
                if(index != -1)
                {
                    value.CurrentTaskId = index;
                    currentWorker = value;
                }
            }
        }

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
            // Giving Active status
            activity = Status.Acitve;

            // Start up Timer event
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
                // Giving Pause status
                activity = Status.Pause;
                process.Enabled = false;
            }
        }

        public void Stop()
        {
            // Giving Stop status
            activity = Status.Stop;

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
                int index = worker.Joblist.FindIndex(x => CheckTime(x.TaskDate));
                if (index != -1)
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
                // Get current task index
                try
                {
                    if(!CheckTime(currentWorker.Joblist[currentWorker.CurrentTaskId].Deadline))
                    {
                        currentWorker.Joblist[currentWorker.CurrentTaskId].Complete = true;
                    }
                }
                catch (Exception exc)
                {
                    Log.Record(exc);
                }
            }
            else
            {
                // Stop Machine
                Stop();
            }
        }

        private bool CheckTime(DateTime date)
        {
            if(date.DayOfYear == DateTime.Now.DayOfYear && date.Year == DateTime.Now.Year)
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

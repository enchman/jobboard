using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobEngine
{
    class Job
    {
        #region Properties
        private int jobId = 0;
        private int machineId = 0;
        private int workerId = 0;
        private DateTime workDate = DateTime.Now;
        private bool complete = false;

        public int Id
        {
            get
            {
                return jobId;
            }
        }

        public int StationId
        {
            get
            {
                return machineId;
            }
        }

        public int EmployeeId
        {
            get
            {
                return workerId;
            }
        }

        public DateTime TaskDate
        {
            get
            {
                return workDate;
            }
        }

        public bool Complete
        {
            get
            {
                return complete;
            }
            set
            {
                if (value)
                {
                    try
                    {
                        Dictionary<string, object> param = new Dictionary<string, object> { };
                        param.Add("id", Id);
                        new Database("completeTask", param).Procedure();
                        complete = value;
                    }
                    catch (Exception exc)
                    {
                        Log.Record(exc);
                        // Error occur during Database processing
                    }
                }
                else
                {
                    complete = false;
                }
            }
        }

        public bool IsShift
        {
            get
            {
                return !complete && DateTime.Now == workDate;
            }
        }
        #endregion

        #region Constructors
        public Job(int machine, int employee, DateTime task)
        {
            machineId = machine;
            workerId = employee;
            workDate = task;
        }

        public Job(int machine, int employee, DateTime task, bool done)
        {
            machineId = machine;
            workerId = employee;
            workDate = task;
            complete = done;
        }
        #endregion

        public void Add()
        {
            if(Id == 0)
            {
                try
                {
                    Dictionary<string, object> param = new Dictionary<string, object> { };
                    param.Add("employee", EmployeeId);
                    param.Add("machine", StationId);
                    param.Add("date", TaskDate);

                    Database db = new Database("addTask", param);

                    List<Dictionary<string, object>> data = db.FetchProcedure();
                    jobId = (int)data[0]["id"];
                }
                catch
                {
                    // Error occur during Database processing
                }
            }
        }
    }
}

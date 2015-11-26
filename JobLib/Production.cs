using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
namespace JobLib
{
    class Production
    {
        public enum Status { None, Active, Pause, Done }

        public Status Action
        {
            get
            {
                if(working == null)
                {
                    return Status.None;
                }
                else
                {
                    if(done)
                    {
                        return Status.Done;
                    }
                    else if(working.Enabled)
                    {
                        return Status.Active;
                    }
                    else
                    {
                        return Status.Pause;
                    }
                }
            }
        }


        public List<Joblist> Works = new List<Joblist> { };

        private int step = 0;
        private int timing = 0;
        private bool done = false;
        private Joblist work;
        private Timer working;


        public void Start()
        {
            
        }

        public void Pause()
        {

        }

        public void Stop()
        {

        }

        public void AddJoblist(Joblist job)
        {
            Works.Add(job);
        }

        private void SeekWork()
        {
            // Catch Current Work or Next Work
            foreach(Joblist job in Works)
            {
                if(job.Status == Joblist.WorkProgress.Active || job.Status == Joblist.WorkProgress.Waiting)
                {
                    this.work = job;
                    this.timing = job.WorkingTime;
                    
                    break;
                }
                else
                {
                    this.step++;
                }
            }
        }

        private void WorkCycle()
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLib
{
    class Joblist
    {
        public enum WorkProgress { Waiting, Active, Done, Cancel }

        public List<Job> WorkLines = new List<Job> { };
        public WorkProgress Status { get; set; }

        public int WorkingTime
        {
            get
            {
                int total = 0;
                foreach(Job job in WorkLines)
                {
                    total += job.Timing;
                }
                return total;
            }
        }
    }
}

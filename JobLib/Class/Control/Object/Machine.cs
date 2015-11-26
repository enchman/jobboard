using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLib
{
    public class Machine
    {
        public enum Status { Off, Working, Done }

        public Status Action { get; set; }
        public Job.Function Type
        {
            get
            {
                return machineType;
            }
        }

        private Job.Function machineType = Job.Function.Assembling;

        public Machine()
        {

        }

        public Machine(Job.Function type)
        {
            machineType = type;
        }

        public void ChangeAction(Status status)
        {
            this.Action = status;
        }

        private void Run()
        {

        }
    }
}

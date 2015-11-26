using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLib
{
    class Machine
    {
        public enum Status { Off, Working, Done }

        public Status Action = Status.Off;

        public Machine()
        {

        }

        private void Run()
        {

        }
    }
}

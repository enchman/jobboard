using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLib
{
    public class Job
    {
        public enum Function { Punching = 4, Welding = 10, Bending = 6, LaserCutter = 10, Milling = 10, Shears = 6, Assembling = 10 }

        public Function Work = Function.Assembling;

        public int Timing
        {
            get
            {
                return (int)Work;
            }
        }

        public Job()
        {

        }

        public Job(Function word)
        {
            this.Work = word;
        }
    }
}

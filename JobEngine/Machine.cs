using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobEngine
{
    public class Machine
    {
        public enum Function { Punching = 4, Welding = 10, Bending = 6, LaserCutter = 10, Milling = 10, Shears = 6, Assembling = 10 }

        #region Properties
        private int machineId = 0;

        public int Id
        {
            get
            {
                return machineId;
            }
        }

        public Function Type { get; set; }

        public Employee Worker { get; set; }
        #endregion

        #region Constructor
        public Machine(Employee worker)
        {
            Worker = worker;
        }
        public Machine(Function type)
        {
            Type = type;
        }
        public Machine(int id)
        {
            machineId = id;
        }
        #endregion

        public void Sync()
        {

        }
    }
}

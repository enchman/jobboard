using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobEngine
{
    public class Production
    {
        private Dictionary<Machine, int> machines = new Dictionary<Machine, int> { };

        
        private void SetMachines()
        {
            List<Machine.Function> machineType = Enum.GetValues(typeof(Machine.Function)).Cast<Machine.Function>().ToList();
            foreach(Machine.Function type in machineType)
            {
                Machine engine = new Machine(type);
                
            }
        }
    }
}

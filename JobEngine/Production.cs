using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobEngine
{
    class Production
    {
        private List<Machine> machines = new List<Machine> { };

        public Production()
        {

        }

        public void GetItem(Item item)
        {
            
        }

        private void Load()
        {
            // Read machine
            machines = Machine.GetMachines();
        }
    }
}

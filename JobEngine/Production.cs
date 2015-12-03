using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobEngine
{
    class Production
    {
        #region Properties
        public const int StartHour = 9;
        public const int StartMinute = 0;
        public const int EndHour = 16;
        public const int EndMinute = 0;

        private List<Operation> stations = new List<Operation> { };
        #endregion

        public Production()
        {
            // Load machine into production line
            Load();

        }

        public void GetItem(Item item)
        {
            foreach (KeyValuePair<int, int> block in item.Parts)
            {
                
            }
        }

        private void Load()
        {
            // Read machine
            stations = Machine.GetMachines();
        }
    }
}

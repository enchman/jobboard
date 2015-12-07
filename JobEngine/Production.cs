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

        public DateTime GetWorkDate(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, StartHour, StartMinute, 0);
        }
        public DateTime GetWorkDate(int year, int month, int date)
        {
            return new DateTime(year, month, date, StartHour, StartMinute, 0);
        }

        /// <summary>
        /// Get Managed DeadLine
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime GetDeadline(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, EndHour, EndMinute, 0);
        }
        public DateTime GetDeadline(int year, int month, int date)
        {
            return new DateTime(year, month, date, EndHour, EndMinute, 0);
        }

        private void Load()
        {
            // Read machine
            stations = Machine.GetMachines();
        }
    }
}

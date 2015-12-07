using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobEngine
{
    public class Controller
    {
        internal static List<Order> Orders = new List<Order> { };
        internal static List<Employee> Workers = new List<Employee> { };
        internal static Production production = new Production();

        public Controller()
        {
            Load();
        }

        #region Control
        private void Load()
        {
            // Load unfinish
            Orders = Order.GetWorkLine();
            // Load Workers
            Workers = Employee.GetEmployees();

        }

        private void WorkSchedule()
        {
            foreach(Employee co in Workers)
            {
                if(co.Joblist != null)
                {
                    
                }
            }
        }

        
        #endregion
    }
}

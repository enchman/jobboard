using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobEngine
{
    public class Employee
    {
        #region Properties
        private int employeeId = 0;

        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public int Id
        {
            get
            {
                return employeeId;
            }
        }
        #endregion

        #region Constructor
        public Employee(int id)
        {
            employeeId = id;
        }

        public Employee(string firstname)
        {
            Firstname = firstname;
        }

        public Employee(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }
        #endregion

        public void Sync()
        {

        }
    }
}

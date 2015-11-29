using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace JobEngine
{
    public class Customer
    {
        #region Properties
        private int customerId = 0;
        private string telephone;

        public List<Order> Orders = new List<Order> { };

        public int Id
        {
            get
            {
                return customerId;
            }
        }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Phone
        {
            get
            {
                return telephone;
            }
            set
            {
                if (value.Length == 8)
                {
                    telephone = "+45" + value;
                }
                else if(value.Length == 11)
                {
                    telephone = value;
                }
                else if(value.Length == 12)
                {
                    string tlf = value.Substring(4);
                    telephone = "+45" + tlf;
                }
            }
        }

        public string Fullname
        {
            get
            {
                return Firstname + " " + Lastname;
            }
        }
        #endregion

        #region Constructor
        public Customer()
        {

        }

        public Customer(int id)
        {
            
        }

        public Customer(string firstname, string lastname)
        {
            this.Firstname = firstname;
            this.Lastname = lastname;
        }

        public Customer(string firstname, string lastname, string email)
        {
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Email = email;
        }
        #endregion

        public void Sync()
        {

        }
    }
}

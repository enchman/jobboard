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

        public string Fullname
        {
            get
            {
                return Firstname + " " + Lastname;
            }
        }

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
        #endregion

        #region Constructor
        public Customer()
        {

        }

        public Customer(int id)
        {
            customerId = id;
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

        public Customer(string firstname, string lastname, string phone, string email)
        {
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Phone = phone;
            this.Email = email;
        }

        public Customer(int id, string firstname, string lastname, string phone, string email)
        {
            this.customerId = id;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Phone = phone;
            this.Email = email;
        }
        #endregion

        /// <summary>
        /// Create a Customer
        /// </summary>
        public void Create()
        {
            if (Id == 0)
            {
                try
                {
                    Database db = new Database("setCustomer");
                    db.Bind("firstname", Firstname);
                    db.Bind("lastname", Lastname);
                    db.Bind("phone", Phone);
                    db.Bind("email", Email);
                    Dictionary<string, object> data = db.GetProcedure();
                    if(data.Count == 1)
                    {
                        customerId = (int)data["id"];
                    }
                    else if(data.Count > 1)
                    {
                        customerId = (int)data["id"];
                        Firstname = (string)data["firstname"];
                        Lastname = (string)data["lastname"];
                        telephone = (string)data["phone"];
                        Email = (string)data["email"];
                    }
                }
                catch (Exception exc)
                {
                    Log.Record(exc);
                }
            }
        }

        public void AddOrder(Order order)
        {
            Orders.Add(order);
        }

        private void Load()
        {
            if(Id != 0)
            {
                List<Order> data = Order.GetOrders(Id);
                if(data != null)
                {
                    Orders = data;
                }
            }
        }

        public static List<Customer> GetCustomers()
        {
            try
            {
                List<Dictionary<string,object>> datalist = new Database("SELECT * FROM [customers]").Fetch();
                if(datalist != null)
                {
                    List<Customer> customers = new List<Customer> { };
                    foreach (Dictionary<string, object> item in datalist)
                    {
                        int id = (int)item["id"];
                        string firstname = (string)item["firstname"];
                        string lastname = (string)item["lastname"];
                        string email = (string)item["email"];
                        string phone = (string)item["phone"];

                        customers.Add(new Customer(id, firstname, lastname, phone, email));
                    }
                    return customers;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exc)
            {
                Log.Record(exc);
                return null;
            }
        }
    }
}

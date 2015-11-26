using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLib
{
    public class Customer
    {
        public int Id
        {
            get
            {
                return customerId;
            }
        }

        public List<Order> Orders = new List<Order> { };

        private int customerId = 1;
        private static int currentId = 1;

        public Customer()
        {
            this.customerId = currentId;
            currentId++;
        }

        public Customer(Order order)
        {
            this.Orders.Add(order);
            this.customerId = currentId;
            currentId++;
        }

        public Customer(List<Item> items)
        {
            this.Orders.Add(new Order(items));
            this.customerId = currentId;
            currentId++;
        }
    }
}

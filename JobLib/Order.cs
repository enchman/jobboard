using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLib
{
    class Order
    {
        public List<Item> OrderLine = new List<Item> { };
        public readonly Customer Owner;
        public DateTime OrderDate = DateTime.Now;
        public DateTime DeliverDate = new DateTime(1, 1, 1);

        public Order()
        {

        }

        public Order(Customer customer)
        {
            this.Owner = customer;
        }

        public Order(Customer customer, List<Item> items)
        {
            this.Owner = customer;
            this.OrderLine = items;
        }
    }
}

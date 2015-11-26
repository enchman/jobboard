using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLib
{
    public class Order
    {
        public List<Item> OrderLine = new List<Item> { };
        public DateTime OrderDate = DateTime.Now;
        public DateTime DeliverDate = new DateTime(1, 1, 1);

        public Order()
        {

        }

        public Order(List<Item> items)
        {
            this.OrderLine = items;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLib
{
    class Controller
    {
        private Production engine = new Production();
        private List<Order> orders = new List<Order> { };

        public void AddOrder(Customer buyer, List<Item> items)
        {
            orders.Add(new Order(buyer));
        }
    }
}

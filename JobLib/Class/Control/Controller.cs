using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLib
{
    public class Controller
    {
        private Production engine = new Production();
        private List<Customer> customers = new List<Customer> { };

        public Controller()
        {
            SetupDefault();
        }

        public void AddOrder(Customer buyer, List<Item> items)
        {
            buyer.Orders.Add(new Order(items));
            customers.Add(buyer);
        }

        public void ManageProduction()
        {
            engine.Start();
        }

        private void SetupDefault()
        {
            Customer client1 = new Customer();
            List<Item> item1 = new List<Item>{
                new Item(Item.ItemType.Normal),
                new Item(Item.ItemType.Bread)
            };
            client1.Orders.Add(new Order(item1));

            Customer client2 = new Customer();
            List<Item> item2 = new List<Item>{
                new Item(Item.ItemType.Normal)
            };
            client2.Orders.Add(new Order(item2));

            Customer client3 = new Customer();
            List<Item> item3 = new List<Item>{
                new Item(Item.ItemType.Toast),
                new Item(Item.ItemType.Press)
            };
            client3.Orders.Add(new Order(item3));
            

        }
    }
}

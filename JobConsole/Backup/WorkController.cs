using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobConsole
{
    class WorkController
    {
        private List<Customer> Customers = new List<Customer> { };
        private Instruction Fragments = new Instruction();
        
        public void RunRutine()
        {

        }

        #region Customer Action

        public void AddOrder(string name, List<Item> items)
        {
            Customer user = new Customer(name);
            user.Orders.Add(new Order(items));

            Customers.Add(user);
        }

        public void AddOrder(int id, List<Item> items)
        {
            int index = Customers.FindIndex(x => x.Id == id);
            if(index != -1)
            {
                Customer user = Customers.ElementAt(index);
                user.Orders.Add(new Order(items));
                Customers.Insert(index, user);
            }
        }

        public void AddOrder(Customer who, List<Item> items)
        {
            int index = Customers.FindIndex(x => x.Id == who.Id);
            if (index != -1)
            {
                Customer user = Customers.ElementAt(index);
                user.Orders.Add(new Order(items));
                Customers.Insert(index, user);
            }
            else
            {
                who.Orders.Add(new Order(items));
                Customers.Add(who);
            }
        }
        #endregion

        private void WorkingRutine()
        {

        }

        private void CheckOrder()
        {

        }

        private void ReadHistory()
        {

        }
    }
}

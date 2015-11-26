using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobConsole
{
    class Order
    {
        public List<Item> Items = new List<Item> { };
        public DateTime OrderDate;
        public DateTime DeliverDate = new DateTime(1, 1, 1);

        public readonly int Id = 1;
        private static int currentId = 1;

        public Order()
        {
            this.Id = currentId;
            currentId++;
        }

        public Order(List<Item> items)
        {
            this.Id = currentId;
            currentId++;
            this.Items = items;
        }

        public Order(DateTime date)
        {
            this.OrderDate = date;
            this.Id = currentId;
            currentId++;
        }

        public Order(int id)
        {
            this.Id = id;
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobConsole
{
    class Customer
    {
        public List<Order> Orders = new List<Order> { };
        public readonly string Name;
        public readonly int Id = 1;

        private static int currentId = 1;

        public Customer()
        {
            this.Id = currentId;
            currentId++;
        }

        public Customer(string name)
        {
            this.Name = name;
            this.Id = currentId;
            currentId++;
        }

        public Customer(int id)
        {
            this.Id = id;
        }

        public Customer(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

    }
}

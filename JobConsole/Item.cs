using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobConsole
{
    class Item
    {
        public int Id;
        private Blueprint Type;

        public static int Increment = 1;

        public Item()
        {
            this.Id = Increment;
            Increment++;
        }

        public Item(int id)
        {
            this.Id = id;
        }

        public Item(Blueprint type)
        {
            this.Type = type;
            this.Id = Increment;
            Increment++;
        }
    }
}

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
        public string Name;
        private Blueprint Type;

        public Item()
        {

        }

        public Item(int id)
        {
            this.Id = id;
        }

        public Item(string name)
        {
            this.Id = id;
        }
    }
}

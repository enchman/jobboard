using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobEngine
{
    public class OrderLine
    {
        #region Properties
        private int orderId = 0;

        public Dictionary<Item, int> Items = new Dictionary<Item, int> { };

        public int OrderId
        {
            get
            {
                return orderId;
            }
        }
        #endregion

        #region Constructor
        public OrderLine(int id)
        {
            this.orderId = id;
        }
        #endregion

        public void AddItem(Item item)
        {
            if(Items.ContainsKey(item))
            {
                Items[item]++;
            }
            else
            {
                Items[item] = 1;
            }
        }

        public void AddItem(Item item, int quantity)
        {
            if(quantity < 1)
            {
                Items.Remove(item);
            }
            else
            {
                Items[item] = quantity;
            }
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }
    }
}

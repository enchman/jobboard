using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobEngine
{
    public class Order
    {
        private int orderId = 0;
        private int customerId = 0;

        public int Id
        {
            get
            {
                return orderId;
            }
        }

        public List<OrderLine> ItemLine = new List<OrderLine> { };

        public void Sync()
        {

        }
    }
}

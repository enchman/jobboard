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
        private DateTime orderdate = DateTime.Now;
        private DateTime expectdate = DateTime.Now;
        private DateTime? deliverdate = null;

        public List<OrderLine> ItemLine = new List<OrderLine> { };

        public int Id
        {
            get
            {
                return orderId;
            }
        }

        public DateTime OrderDate
        {
            get
            {
                return orderdate;
            }
        }

        public DateTime ExpectDate
        {
            get
            {
                return expectdate;
            }
        }


        public DateTime? DeliverDate
        {
            get
            {
                return deliverdate;
            }
        }

        #region Constructor
        public Order()
        {

        }

        public Order(int id, int customer, DateTime dateOrder, DateTime dateExpect, DateTime? dateDeliver)
        {
            orderId = id;
            customerId = customer;
            orderdate = dateOrder;
            expectdate = dateExpect;
            deliverdate = dateDeliver;
        }
        #endregion

        public void Sync()
        {

        }

        #region Global
        //public static List<Order> GetOrders()
        //{
            
        //}


        #endregion
    }
}

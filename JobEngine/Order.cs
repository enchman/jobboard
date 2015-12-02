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
        public static List<Order> GetOrders()
        {
            Database db = new Database("SELECT * FROM [viewOrderList]");
            List<Dictionary<string, object>> data = db.Fetch();

            if(data != null)
            {
                List<Order> orders = new List<Order> { };

                foreach (Dictionary<string, object> item in data)
                {
                    int id = (int)item["id"];
                    int cid = (int)item["customerId"];
                    DateTime oDate = (DateTime)item["orderDate"];
                    DateTime eDate = (DateTime)item["expectDate"];
                    DateTime? dDate = (DateTime?)item["deliverDate"];

                    orders.Add(new Order(id, cid, oDate, eDate, dDate));
                }

                return orders;
            }
            else
            {
                return null;
            }
        }


        #endregion
    }
}

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
        private DateTime expectdate = DateTime.Now.AddDays(3);
        private DateTime? deliverdate = null;
        private List<OrderLine> orderlines = new List<OrderLine> { };

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

        public List<OrderLine> Items
        {
            get
            {
                return orderlines;
            }
            set
            {
                orderlines = value;
            }
        }

        #region Constructor
        public Order()
        {

        }

        public Order(int id)
        {

        }

        public Order(int customer, DateTime dateOrder)
        {
            customerId = customer;
            orderdate = dateOrder;
            Sync();
        }

        public Order(int customer, DateTime dateOrder, DateTime dateExpect)
        {
            customerId = customer;
            orderdate = dateOrder;
            expectdate = dateExpect;
            Sync();
        }

        public Order(int customer, DateTime dateOrder, DateTime dateExpect, DateTime? dateDeliver)
        {
            customerId = customer;
            orderdate = dateOrder;
            expectdate = dateExpect;
            deliverdate = dateDeliver;
            Sync();
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

        #region Action methods
        public void Edit(DateTime expect)
        {
            expectdate = expect;
        }

        public void Edit(DateTime expect, DateTime deliver)
        {
            expectdate = expect;
            expectdate = deliver;
        }
        public void Remove()
        {
            if(Id != 0)
            {
                Database db = new Database("removeOrder");
                db.Bind("id", Id);
                db.Procedure();
            }
        }

        #endregion


        public void AddItem(OrderLine line)
        {
            if(line.OrderId == orderId)
            {
                if (orderlines.Any(x => x.ItemId != line.ItemId))
                {
                    // Add new item to order
                    orderlines.Add(line);
                }
                else
                {
                    // Update quantity
                    int index = orderlines.FindIndex(x => x.ItemId == line.ItemId);
                    if (index != -1)
                    {
                        orderlines[index].Quantity = line.Quantity;
                    }
                }
            }
        }
        public void AddItem(int itemId, int quantity)
        {
            if(orderId != 0 && quantity > 0)
            {
                int index = orderlines.FindIndex(x => x.ItemId == itemId);
                if (index != -1)
                {
                    orderlines[index].Quantity += quantity;
                }
                else
                {
                    OrderLine line = new OrderLine(orderId, itemId, quantity);
                    line.Sync();
                    orderlines.Add(line);
                }
            }
        }

        public void Sync()
        {
            try
            {
                if (Id == 0)
                {
                    Database db = new Database("addOrder");
                    db.Bind("customer", customerId);
                    db.Bind("order", OrderDate);
                    db.Bind("expect", ExpectDate);

                    Dictionary<string, object> data = db.GetProcedure();
                    if (data != null)
                    {
                        orderId = (int)data["id"];
                    }
                }
            }
            catch (Exception exc)
            {
                Log.Record(exc);
            }
        }

        #region Global
        public static List<Order> GetWorkLine()
        {
            Database db = new Database("SELECT * FROM [viewWorkLine]");
            List<Dictionary<string, object>> data = db.Fetch();

            if (data != null)
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

        public static List<Order> GetOrders()
        {
            Database db = new Database("SELECT * FROM [viewOrderList]");
            List<Dictionary<string, object>> data = db.Fetch();

            if (data != null)
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

        public static List<Order> GetOrders(int id)
        {
            Database db = new Database("getOrder");
            db.Bind("id", id);
            List<Dictionary<string, object>> data = db.Fetch();

            if (data != null)
            {
                List<Order> orders = new List<Order> { };

                foreach (Dictionary<string, object> item in data)
                {
                    int oid = (int)item["id"];
                    int cid = (int)item["customerId"];
                    DateTime oDate = (DateTime)item["orderDate"];
                    DateTime eDate = (DateTime)item["expectDate"];
                    DateTime? dDate = (DateTime?)item["deliverDate"];
                    
                    orders.Add(new Order(oid, cid, oDate, eDate, dDate));
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

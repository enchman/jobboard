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
        private int itemId = 0;
        private int quantity = 1;
        private bool isupdate = false;

        public int OrderId
        {
            get
            {
                return orderId;
            }
        }

        public int ItemId
        {
            get
            {
                return itemId;
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                // Prevent under set below 1
                if(value > 0)
                {
                    quantity = value;
                    Sync();
                }
            }
        }

        #endregion

        #region Constructor
        public OrderLine(int orderid, int itemid)
        {
            this.orderId = orderid;
            this.itemId = itemid;
        }
        public OrderLine(int orderid, int itemid, int amount)
        {
            this.orderId = orderid;
            this.itemId = itemid;
            this.Quantity = amount;
        }

        public OrderLine(int orderid, int itemid, int amount, bool update)
        {
            this.orderId = orderid;
            this.itemId = itemid;
            this.Quantity = amount;
            this.isupdate = update;
        }
        #endregion

        /// <summary>
        /// Remove OrderLine from database
        /// </summary>
        public void Remove()
        {
            try
            {
                Database db = new Database("removeOrderLine");
                db.Bind("order", OrderId);
                db.Bind("item", ItemId);
                db.Procedure();
            }
            catch (Exception exc)
            {
                Log.Record(exc);
            }
        }

        public void Sync()
        {
            try
            {
                // Synchronize data
                Database db = new Database("addItems");
                db.Bind("order", OrderId);
                db.Bind("item", ItemId);
                db.Bind("num", Quantity);
                db.Procedure();
            }
            catch (Exception exc)
            {
                Log.Record(exc);
            }
        }

        public static List<OrderLine> GetOrderLine(int order)
        {
            try
            {
                Database db = new Database("getOrderLine");
                db.Bind("id", order);
                List<Dictionary<string,object>> datalist = db.FetchProcedure();
                if(datalist != null)
                {
                    List<OrderLine> items = new List<OrderLine> { };
                    foreach(Dictionary<string, object> item in datalist)
                    {
                        int oid = (int)item["orderId"];
                        int iid = (int)item["itemId"];
                        int num = (int)item["quantity"];

                        items.Add(new OrderLine(oid, iid, num, true));
                    }

                    return items;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception exc)
            {
                Log.Record(exc);
                return null;
            }
        }
    }
}

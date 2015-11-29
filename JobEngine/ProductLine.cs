using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobEngine
{
    public class ProductLine
    {
        #region Properties
        private int lineId = 0;
        private bool lineStatus = false;

        public int Id
        {
            get
            {
                return lineId;
            }
        }

        public Item Product { get; set; }

        public bool Status
        {
            get
            {
                return lineStatus;
            }
            set
            {
                lineStatus = value;
            }
        }

        public int TotalTime
        {
            get
            {
                int total = 0;
                foreach (Machine.Function item in Product.Instructions)
                {
                    total += (int)item;
                }
                return total;
            }
        }

        public DateTime ProductionTime = new DateTime(1, 1, 1);
        #endregion

        #region Constructor
        public ProductLine(int id)
        {
            lineId = id;
        }
        public ProductLine(Item item)
        {
            Product = item;
        }
        public ProductLine(Item item, DateTime date)
        {
            Product = item;
            ProductionTime = date;
        }
        public ProductLine(Item item, bool status)
        {
            Product = item;
            Status = status;
        }
        public ProductLine(Item item, bool status, DateTime date)
        {
            Product = item;
            Status = status;
            ProductionTime = date;
        }
        #endregion

        public void Sync()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobEngine
{
    public class Item
    {
        private int itemId = 0;
        private int numItems = 0;

        public int Id
        {
            get
            {
                return itemId;
            }
        }

        public int InStock
        {
            get
            {
                return numItems;
            }
            set
            {

            }
        }

        public string Name { get; set; }

        #region Constructor
        public Item()
        {

        }

        

        #endregion
        public void Sync()
        {

        }

        public void Add()
        {
            // Create new item
            if(Id == 0)
            {
                Db db = new Db("setItem");
                Dictionary<string, object> param = new Dictionary<string, object> { };
                //param.Add("name");
            }
            // Update item
            else
            {

            }
                
        }

        public void Delete()
        {

        }
    }
}

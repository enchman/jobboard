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
        private Dictionary<int, int> machines = new Dictionary<int, int> { };

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

        public Dictionary<int,int> Parts
        {
            get
            {
                return machines;
            }
        }

        public string Name { get; set; }

        #region Constructor
        public Item()
        {

        }

        public Item(int id, string name, int stock)
        {
            itemId = id;
            Name = name;
            numItems = stock;
            Load();
        }

        public Item(int id, string name, int stock, Dictionary<int, int> part)
        {
            itemId = id;
            Name = name;
            numItems = stock;
            machines = part;
        }

        #endregion
        public void Sync()
        {

        }

        public void Add()
        {

        }

        public void Delete()
        {

        }

        private void Load()
        {
            Dictionary<string, object> param = new Dictionary<string, object> { };
            param.Add("id", itemId);
            Database db = new Database("getItemProp", param);
            List<Dictionary<string,object>> datalist = db.FetchProcedure();
            if(datalist != null)
            {
                foreach (Dictionary<string,object> item in datalist)
                {
                    int id = (int)item["machineId"];
                    int num = (int)item["amount"];
                    machines.Add(id, num);
                }
            }
        }
    }
}

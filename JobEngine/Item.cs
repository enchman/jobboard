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
        public Item(string name, int stock)
        {
            Name = name;
            numItems = stock;
        }
        public Item(string name, int stock, Dictionary<int, int> part)
        {
            Name = name;
            numItems = stock;
            machines = part;
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

        public void Create()
        {
            try
            {
                // Create item
                Database db = new Database("setItem");
                db.Bind("name", Name);
                db.Bind("stock", 0);
                Dictionary<string, object> data = db.GetProcedure();
                itemId = (int)data["id"];
                // Adding item properties
                if(machines.Count > 0)
                {
                    // Generate Multi Insert query
                    string query = null;
                    foreach (KeyValuePair<int,int> item in machines)
                    {
                        if(query != null)
                        {
                            query += String.Format(",({0},{1},{2})", Id, item.Key, item.Value);
                        }
                        else
                        {
                            query += String.Format("INSERT INTO [itemProp] ([itemId],[machineId],[amount]) VALUES({0},{1},{2})", Id, item.Key, item.Value);
                        }
                    }
                    // Insert data
                    new Database(query).Execute();
                }

            }
            catch (Exception exc)
            {
                Log.Record(exc);
            }
        }

        public void Delete()
        {

        }

        public static List<Item> GetItems()
        {
            try
            {
                Database db = new Database("SELECT * FROM [items]");
                List<Dictionary<string, object>> datalist = db.Fetch();
                if (datalist != null)
                {
                    List<Item> items = new List<Item> { };
                    foreach (Dictionary<string, object> data in datalist)
                    {
                        int id = (int)data["id"];
                        int num = (int)data["inStock"];
                        string name = (string)data["name"];
                        items.Add(new Item(id, name, num));
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

        private void Load()
        {
            Database db = new Database("getItemProp");
            db.Bind("id", itemId);
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

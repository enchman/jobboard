using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobEngine
{
    public class Item
    {
        public enum DefaultItem { Customized, Normal, Bread, Toast, Press }

        private int itemId = 0;
        private List<Machine.Function> itemProcess = new List<Machine.Function> { };

        public int Id
        {
            get
            {
                return itemId;
            }
        }
        public List<Machine.Function> Instructions
        {
            get
            {
                return itemProcess;
            }
        }

        public DefaultItem Type { get; set; }
        public string Name { get; set; }

        #region Constructor
        public Item(int id)
        {
            this.itemId = id;
        }
        public Item(string name, List<Machine.Function> functions)
        {
            this.Name = name;
            this.itemProcess = functions;
        }
        public Item(string name, DefaultItem type)
        {
            this.Name = name;
            this.Type = type;
            SetDefault();
        }
        #endregion

        public void EditDefault(DefaultItem type)
        {
            this.Type = type;
            SetDefault();
        }

        public void Sync()
        {

        }

        private void SetDefault()
        {
            if (Type != DefaultItem.Customized)
            {
                if (Type == DefaultItem.Normal)
                {
                    this.itemProcess.Add(Machine.Function.LaserCutter);
                    this.itemProcess.Add(Machine.Function.Bending);
                    this.itemProcess.Add(Machine.Function.Assembling);
                }
                else if (Type == DefaultItem.Bread)
                {
                    this.itemProcess.Add(Machine.Function.Milling);
                    this.itemProcess.Add(Machine.Function.LaserCutter);
                    this.itemProcess.Add(Machine.Function.Bending);
                    this.itemProcess.Add(Machine.Function.Assembling);
                }
                else if (Type == DefaultItem.Toast)
                {
                    this.itemProcess.Add(Machine.Function.Milling);
                    this.itemProcess.Add(Machine.Function.Punching);
                    this.itemProcess.Add(Machine.Function.LaserCutter);
                    this.itemProcess.Add(Machine.Function.Shears);
                    this.itemProcess.Add(Machine.Function.Bending);
                    this.itemProcess.Add(Machine.Function.Assembling);
                }
                else if (Type == DefaultItem.Press)
                {
                    this.itemProcess.Add(Machine.Function.Milling);
                    this.itemProcess.Add(Machine.Function.Bending);
                    this.itemProcess.Add(Machine.Function.Assembling);
                }
            }
        }
    }
}

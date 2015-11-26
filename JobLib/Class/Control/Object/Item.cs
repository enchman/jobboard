using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLib
{
    public class Item
    {
        public enum ItemType { Customized, Normal, Bread, Toast, Press }

        public ItemType Type
        {
            get
            {
                return category;
            }
            set
            {
                this.category = value;
                DefaultType(value);
            }
        }

        public List<Job.Function> Parts = new List<Job.Function> { };

        private ItemType category = ItemType.Customized;

        public Item()
        {
            this.DefaultType();
        }

        public Item(ItemType type)
        {
            this.Type = type;
            this.DefaultType(type);
        }

        private void DefaultType()
        {
            this.SetDefault(Type);
        }

        private void DefaultType(ItemType choice)
        {
            this.SetDefault(choice);
        }

        private void SetDefault(ItemType choice)
        {
            if (choice != ItemType.Customized)
            {
                if (choice == ItemType.Normal)
                {
                    this.Parts.Add(Job.Function.LaserCutter);
                    this.Parts.Add(Job.Function.Bending);
                    this.Parts.Add(Job.Function.Assembling);
                }
                else if (choice == ItemType.Bread)
                {
                    this.Parts.Add(Job.Function.Milling);
                    this.Parts.Add(Job.Function.LaserCutter);
                    this.Parts.Add(Job.Function.Bending);
                    this.Parts.Add(Job.Function.Assembling);
                }
                else if (choice == ItemType.Toast)
                {
                    this.Parts.Add(Job.Function.Milling);
                    this.Parts.Add(Job.Function.Punching);
                    this.Parts.Add(Job.Function.LaserCutter);
                    this.Parts.Add(Job.Function.Shears);
                    this.Parts.Add(Job.Function.Bending);
                    this.Parts.Add(Job.Function.Assembling);
                }
                else if (choice == ItemType.Press)
                {
                    this.Parts.Add(Job.Function.Milling);
                    this.Parts.Add(Job.Function.Bending);
                    this.Parts.Add(Job.Function.Assembling);
                }
            }
        }

    }
}

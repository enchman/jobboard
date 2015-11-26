using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobConsole
{
    class Instruction
    {
        private List<Blueprint> Schematics = new List<Blueprint> { };

        public Instruction()
        {

        }

        public Instruction(List<Blueprint> items)
        {
            Schematics = items;
        }

        public void AddSchematic(Blueprint item)
        {
            // Find existing Blueprint to reproduce the item
            if(!Schematics.Exists(x => x.Id == item.Id || x.WorkLines.SequenceEqual(item.WorkLines)))
            {
                Schematics.Add(item);
            }
        }

        public void RemoveScematic(int id)
        {
            Schematics.RemoveAll(x => x.Id == id);
        }

        public void RemoveScematic(Blueprint item)
        {
            Schematics.RemoveAll(x => x.WorkLines.SequenceEqual(item.WorkLines));
        }

        private void SetDefault()
        {
            foreach(Blueprint.DefaultItem item in Enum.GetValues(typeof(Blueprint.DefaultItem)))
            {
                if(item != Blueprint.DefaultItem.Customized)
                {
                    Blueprint form = new Blueprint();

                }
            }
        }
    }
}

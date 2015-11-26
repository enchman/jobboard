using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLib
{
    class Item
    {
        public enum ItemType { Customized, Normal, Bread, Toast, Press }

        public List<Job.Function> Parts = new List<Job.Function> { };

    }
}

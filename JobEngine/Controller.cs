using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobEngine
{
    public class Controller
    {
        private List<Customer> customers = new List<Customer> { };
        private static Dictionary<Employee, bool> Workers = new Dictionary<Employee, bool> { };

        public Controller()
        {

        }

        public void Run()
        {

        }

        #region Manipulation event
        public void Add()
        {
            
        }

        public void Modify()
        {

        }

        public void Remove()
        {

        }
        #endregion

        #region Add object to system
        public void AddOrder()
        {

        }
        
        public void AddItems(Item item)
        {

        }
        #endregion

        #region Edit object in system
        public void EditOrder()
        {

        }

        public void EditItem()
        {

        }
        #endregion

        #region Remove object in system
        public void RemoveOrder()
        {

        }

        public void RemoveItem()
        {

        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobEngine;
using System.Windows;
using System.Windows.Controls;

namespace JobBoard
{
    class ViewCustomer
    {
        private List<Customer> customers = null;
        public ViewCustomer()
        {

        }

        private void ManageUI()
        {


            //Height="20" Margin="0,10,0,0" Orientation="Horizontal"
            StackPanel parent = new StackPanel();
            parent.Height = 20;
            parent.Orientation = Orientation.Horizontal;
        }

        private void GetCustomers()
        {
            if(customers == null)
            {
                customers = Customer.GetCustomers();
            }
        }
    }
}

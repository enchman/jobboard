using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JobEngine;

namespace JobBoard
{
    /// <summary>
    /// Interaction logic for ManagementControl.xaml
    /// </summary>
    public partial class ManagementControl : UserControl
    {
        private AdminController control;

        public ManagementControl()
        {
            InitializeComponent();
            control = new AdminController(this);
            // Show customers list
            control.ShowCustomers(ref panelCustomer);
        }

        internal void SelectCustomer_Click(object sender, MouseButtonEventArgs e)
        {
            tabCustomer.IsSelected = false;
            tabOrder.IsSelected = true;
            tabMain.SelectedIndex = 1;

        }

        private void buttonTest_Click(object sender, RoutedEventArgs e)
        {
            tabMain.SelectedIndex = 1;
        }
    }
}

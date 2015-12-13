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
        private AdminController adminControl;
        private PopupWindow popup;

        public ManagementControl()
        {
            InitializeComponent();
            adminControl = new AdminController(this);
            // Show customers list
            adminControl.ShowCustomers(ref panelCustomer);
        }


        internal void SelectOrder_Click(object sender, RoutedEventArgs e)
        {
            // Prepare & Show Popup
            popup = new PopupWindow(new OrderLineControl());
            popup.Show();
        }

        internal void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        internal void SelectCustomer_Click(object sender, MouseButtonEventArgs e)
        {
            TextBlock item = sender as TextBlock;
            try
            {
                adminControl.CurrentClient = Convert.ToInt32(item.Uid);
                orderTabContent.Children.Clear();
                adminControl.ShowOrders(ref orderTabContent);

                tabCustomer.IsSelected = false;
                tabOrder.IsSelected = true;
                tabMain.SelectedIndex = 1;
            }
            catch
            {

            }
        }
    }
}

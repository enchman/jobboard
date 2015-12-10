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
    /// Interaction logic for OrderLineControl.xaml
    /// </summary>
    public partial class OrderLineControl : UserControl
    {
        private AdminController adminControl;
        
        public OrderLineControl()
        {
            InitializeComponent();
            adminControl = new AdminController(this);
            ShowItemList();
        }

        public OrderLineControl(AdminController control)
        {
            adminControl = control;
            adminControl.AddUserControl(this);
            adminControl.ShowItems(ref panelItem);
        }

        private void ShowItemList()
        {
            adminControl.ShowItems(ref panelItem, adminControl.ItemList);
        }

        internal void Additem_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}

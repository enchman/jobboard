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
        private ViewCustomer viewer;

        public ManagementControl()
        {
            InitializeComponent();
            
        }

        private void SelectCustomer_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void GetCustomers()
        {
            if (customers == null)
            {
                customers = Customer.GetCustomers();
            }
        }

        private void CustomerList(Customer customer)
        {
            // Border
            Border border = new Border();
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(0, 0, 0, 1);

            // Main panel
            StackPanel panel = new StackPanel();
            panel.Height = 20;
            panel.Margin = new Thickness(0, 10, 0, 0);
            panel.Orientation = Orientation.Horizontal;

            // Subpanel
            StackPanel subpanel1 = new StackPanel();
            subpanel1.Width = 60;

            StackPanel subpanel2 = new StackPanel();
            subpanel2.Width = 200;

            StackPanel subpanel3 = new StackPanel();
            subpanel3.Width = 220;

            // Id Block
            TextBlock txt1 = new TextBlock();
            txt1.Padding = new Thickness(15, 0, 0, 0);
            txt1.HorizontalAlignment = HorizontalAlignment.Left;

            txt1.Uid = customer.Id.ToString();
            txt1.Text = customer.Id.ToString();

            // Name Block
            TextBlock txt2 = new TextBlock();
            txt2.Padding = new Thickness(10, 0, 0, 0);
            txt2.HorizontalAlignment = HorizontalAlignment.Left;
            txt2.Cursor = Cursors.Hand;

            txt2.PreviewMouseDown += SelectCustomer_Click;
            txt2.Uid = customer.Id.ToString();
            txt2.Text = customer.Fullname;

            // Email Block
            TextBlock txt3 = new TextBlock();
            txt3.Padding = new Thickness(10, 0, 0, 0);
            txt3.HorizontalAlignment = HorizontalAlignment.Left;

            txt3.Uid = customer.Id.ToString();
            txt3.Text = customer.Email;

            // Appending
            subpanel1.Children.Add(txt1);
            subpanel2.Children.Add(txt2);
            subpanel3.Children.Add(txt3);

            panel.Children.Add(subpanel1);
            panel.Children.Add(subpanel2);
            panel.Children.Add(subpanel3);

            border.Child = panel;

            panelCustomer.Children.Add(border);
        }
    }
}

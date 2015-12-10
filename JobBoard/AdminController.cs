using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobEngine;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace JobBoard
{
    class AdminController
    {
        public static List<Customer> Clients = null;
        private static List<Item> items = null;
        private UserControl viewer = null;
        

        public int CurrentClient { get; set; }

        public List<Customer> Customers
        {
            get
            {
                if(Clients == null)
                {
                    Clients = Customer.GetCustomers();
                }
                return Clients;
            }
        }

        public List<Item> ItemList
        {
            get
            {
                if (items == null)
                {
                    items = Item.GetItems();
                }
                return items;
            }
        }

        public AdminController(UserControl ui)
        {
            viewer = ui;
        }

        #region Customers Section

        public void ShowCustomers(ref StackPanel ui)
        {
            if(Customers != null)
            {
                int i = 0;
                foreach (Customer client in Customers)
                {
                    Border item = CustomerUI(client, i);
                    ui.Children.Add(item);
                    i++;
                }
            }
        }

        public void ShowItems(ref StackPanel ui)
        {
            //if(items)
        }

        /*
        <Border BorderBrush="Black" Height="30" Padding="0,4" BorderThickness="0,0,0,1">
            <StackPanel Height="20" Orientation="Horizontal">
                <StackPanel Width="60">
                    <TextBlock Uid="1" TextAlignment="Center" Text="1"/>
                </StackPanel>
                <StackPanel Width="250">
                    <TextBlock Uid="1" Text="Hanne" TextAlignment="Center"/>
                </StackPanel>
                <StackPanel Width="90">
                    <TextBlock Uid="1" Text="1" TextAlignment="Center"/>
                </StackPanel>
                <StackPanel Width="90" Orientation="Horizontal" HorizontalAlignment="Center">
                    <Button Content="+" Padding="5,0" Margin="35,0,0,0" Foreground="LightGreen"/>
                </StackPanel>
            </StackPanel>
        </Border>
        */


        private Border ItemUI(Item stuff, int index)
        {
            OrderLineControl view = viewer as OrderLineControl;

            // Border
            Border border = new Border();
            border.BorderBrush = Brushes.Black;
            border.Height = 30;
            border.Padding = new Thickness(0, 4, 0, 4);
            border.BorderThickness = new Thickness(0, 0, 0, 1);

            // Main panel
            StackPanel main = new StackPanel();
            main.Height = 20;
            main.Orientation = Orientation.Horizontal;

            // Sub panel
            StackPanel sub1 = new StackPanel();
            sub1.Width = 60;
            StackPanel sub2 = new StackPanel();
            sub2.Width = 250;
            StackPanel sub3 = new StackPanel();
            sub3.Width = 90;
            StackPanel sub4 = new StackPanel();
            sub4.Width = 90;

            // Text & Button
            TextBlock data1 = new TextBlock();
            data1.Uid = index.ToString();
            data1.TextAlignment = TextAlignment.Center;
            data1.Text = stuff.Id.ToString();

            TextBlock data2 = new TextBlock();
            data2.Uid = index.ToString();
            data2.TextAlignment = TextAlignment.Center;
            data2.Text = stuff.Name;

            TextBlock data3 = new TextBlock();
            data3.Uid = index.ToString();
            data3.TextAlignment = TextAlignment.Center;
            data3.Text = stuff.InStock.ToString(); ;

            Button data4 = new Button();
            data4.Uid = index.ToString();
            data4.Padding = new Thickness(5, 0, 5, 0);
            data4.Margin = new Thickness(35, 0, 0, 0);
            data4.Foreground = Brushes.LightGreen;
            data4.Click += view.Additem_Click;
            data4.Content = "+";

            sub1.Children.Add(data1);
            sub2.Children.Add(data2);
            sub3.Children.Add(data3);
            sub4.Children.Add(data4);

            main.Children.Add(sub1);
            main.Children.Add(sub2);
            main.Children.Add(sub3);
            main.Children.Add(sub4);

            border.Child = main;

            return border;
        }

        private Border CustomerUI(Customer customer, int index)
        {
            ManagementControl view = viewer as ManagementControl;

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

            txt1.Uid = index.ToString();
            txt1.Text = customer.Id.ToString();

            // Name Block
            TextBlock txt2 = new TextBlock();
            txt2.Padding = new Thickness(10, 0, 0, 0);
            txt2.HorizontalAlignment = HorizontalAlignment.Left;
            txt2.Cursor = Cursors.Hand;

            txt2.MouseDown += view.SelectCustomer_Click;
            txt2.MouseUp += view.SelectCustomer_Click;
            txt2.Uid = index.ToString();
            txt2.Text = customer.Fullname;

            // Email Block
            TextBlock txt3 = new TextBlock();
            txt3.Padding = new Thickness(10, 0, 0, 0);
            txt3.HorizontalAlignment = HorizontalAlignment.Left;

            txt3.Uid = index.ToString();
            txt3.Text = customer.Email;

            // Appending
            subpanel1.Children.Add(txt1);
            subpanel2.Children.Add(txt2);
            subpanel3.Children.Add(txt3);

            panel.Children.Add(subpanel1);
            panel.Children.Add(subpanel2);
            panel.Children.Add(subpanel3);

            border.Child = panel;

            return border;
        }

        private Border OrderUI()
        {

            return new Border();
        }

        #endregion
    }
}

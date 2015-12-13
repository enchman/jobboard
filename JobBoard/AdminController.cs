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
    public class AdminController
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

        public void AddUserControl(UserControl control)
        {
            viewer = control;
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

        public void ShowOrders(ref StackPanel ui)
        {
            if(Clients[CurrentClient].Orders != null)
            {
                int i = 0;
                foreach (Order order in Clients[CurrentClient].Orders)
                {
                    Border elm = OrderUI(order, i);
                    ui.Children.Add(elm);
                    i++;
                }
            }
        }

        public void ShowItems(ref StackPanel ui)
        {
            if(ItemList != null)
            {
                int i = 0;
                foreach (Item item in ItemList)
                {
                    Border bord = ItemUI(item, i);
                    ui.Children.Add(bord);
                    i++;
                }
            }
        }

        public void ShowItems(ref StackPanel ui, List<Item> atoms)
        {
            if (atoms != null)
            {
                int i = 0;
                foreach (Item item in atoms)
                {
                    Border bord = ItemUI(item, i);
                    ui.Children.Add(bord);
                    i++;
                }
            }
        }

        /*
        <Border BorderBrush="Black" BorderThickness="0,0,0,1">
            <StackPanel Height="20" Margin="0,10,0,0" Orientation="Horizontal">
                <StackPanel Width="60">
                    <TextBlock Uid="1" Padding="15,0,0,0" TextAlignment="Center" Text="1"/>
                </StackPanel>
                <StackPanel Width="200">
                    <TextBlock Uid="1" Text="Sam Møller" TextAlignment="Center"/>
                </StackPanel>
                <StackPanel Width="160">
                    <TextBlock Uid="1" Text="09-12-2015 08:40:30" TextAlignment="Center"/>
                </StackPanel>
                <StackPanel Width="60" Orientation="Horizontal" HorizontalAlignment="Center">
                    <Button Content="+" Padding="5,0" Foreground="LightGreen" Click="SelectOrder_Click" />
                    <Button Content="x" Padding="5,0" Margin="10,0,0,0" Foreground="Red" />
                </StackPanel>
            </StackPanel>
        </Border>
        */
        private Border OrderUI(Order order, int index)
        {
            ManagementControl view = viewer as ManagementControl;

            Border bord = new Border();
            bord.BorderBrush = Brushes.Black;
            bord.BorderThickness = new Thickness(0, 0, 0, 1);

            // Main panel
            StackPanel panel = new StackPanel();
            panel.Height = 20;
            panel.Margin = new Thickness(0, 10, 0, 0);
            panel.Orientation = Orientation.Horizontal;

            // Sub panel
            StackPanel sub1 = new StackPanel();
            sub1.Width = 60;
            StackPanel sub2 = new StackPanel();
            sub2.Width = 200;
            StackPanel sub3 = new StackPanel();
            sub3.Width = 160;
            StackPanel sub4 = new StackPanel();
            sub4.Width = 60;
            sub4.Orientation = Orientation.Horizontal;
            sub4.HorizontalAlignment = HorizontalAlignment.Center;

            Customer owner = Customers.Find(x => x.Id == order.OwnerId);
            // Content
            TextBlock data1 = new TextBlock();
            data1.Uid = index.ToString();
            data1.Padding = new Thickness(15, 0, 0, 0);
            data1.TextAlignment = TextAlignment.Center;
            data1.Text = order.Id.ToString();

            TextBlock data2 = new TextBlock();
            data2.Uid = index.ToString();
            data2.TextAlignment = TextAlignment.Center;
            data2.Text = owner.Fullname;

            TextBlock data3 = new TextBlock();
            data3.Uid = index.ToString();
            data3.TextAlignment = TextAlignment.Center;
            data3.Text = order.OrderDate.ToString("dd-MM-YYYY HH:mm:ss");

            Button data4 = new Button();
            data4.Uid = index.ToString();
            data4.Padding = new Thickness(5, 0, 5, 0);
            data4.Foreground = Brushes.LightGreen;
            data4.Click += view.SelectOrder_Click;
            data4.Content = "+";

            Button data5 = new Button();
            data5.Uid = index.ToString();
            data5.Padding = new Thickness(5, 0, 5, 0);
            data5.Margin = new Thickness(10, 0, 0, 0);
            data5.Foreground = Brushes.Red;
            data5.Click += view.DeleteOrder_Click;
            data5.Content = "x";

            return bord;
        }

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
            sub4.Orientation = Orientation.Horizontal;
            sub4.HorizontalAlignment = HorizontalAlignment.Center;
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
            //data4.Width = 20;
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

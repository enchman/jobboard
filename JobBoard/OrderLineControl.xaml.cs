﻿using System;
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
        
        public OrderLineControl(List<Item> items)
        {
            InitializeComponent();
        }

        private void ShowItems(List<Item> items)
        {

        }

        internal void Additem_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}

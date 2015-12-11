using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using JobEngine;

namespace JobConsole
{
    class Menu
    {
        private int menuNum = 0;
        private Customer customer = null;
        private List<Item> stocks = null;
        private List<Customer> clients = null;
        private Dictionary<int, string> machines = null;

        #region Constructor
        public Menu()
        {
            // Initiate Items
            stocks = Item.GetItems();

            // Main menu
            MenuCycle();
        }
        #endregion

        #region Action methods
        private void MenuCycle()
        {
            while (KeyChoice())
            {
                Action();
            }

            Console.WriteLine("Ha' en god dag...");
        }

        private void Action()
        {
            switch (menuNum)
            {
                case 1:
                    // Create a customer to system
                    CreateCustomer();
                    break;
                case 2:
                    // Select a customer
                    SelectCustomer();
                    break;
                case 3:
                    AddOrder();
                    break;
                case 4:
                    // Create item
                    ViewOrders();
                    break;
                case 5:
                    // Create item
                    CreateItem();
                    break;
            }
        }
        #endregion

        #region Customer Function
        private void CreateCustomer()
        {
            Console.WriteLine("Create Customer\n");

            Console.Write("Firstname: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string firstname = Console.ReadLine();
            Console.ResetColor();

            Console.Write("Lastname: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string lastname = Console.ReadLine();
            Console.ResetColor();

            Console.Write("Phone: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string phone = Console.ReadLine();
            Console.ResetColor();

            Console.Write("Email: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string mail = Console.ReadLine();
            Console.ResetColor();

            Console.Clear();

            // Creating new Customer
            Customer person = new Customer(firstname, lastname, phone, mail);
            person.Create();
            Console.WriteLine("Added: " + person.Fullname);

            // Pull Customers from database
            clients = Customer.GetCustomers();

            // Select recently customer
            customer = person;

            Console.WriteLine("Please any to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        private void SelectCustomer()
        {
            Console.WriteLine("Select Customer by id");
            if(clients == null)
            {
                clients = Customer.GetCustomers();
            }
            
            if(clients != null)
            {
                string cache = string.Empty;
                foreach (Customer item in clients)
                {
                    cache += String.Format("{0}. {1}\n", item.Id, item.Fullname);
                    //Console.WriteLine("{0}. {1}", item.Id, item.Fullname);
                }

                bool run = true;
                while(run)
                {
                    Console.WriteLine(cache);
                    Console.Write("Customer ID: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    try
                    {
                        
                        int id = int.Parse(Console.ReadLine());
                        int index = clients.FindIndex(x => x.Id == id);
                        if (index != -1)
                        {
                            Console.Beep(600, 500);
                            customer = clients[index];
                            run = false;
                        }
                    }
                    catch
                    {
                        Console.Beep(1000, 500);
                    }
                    Console.ResetColor();
                    Console.Clear();
                }
            }
            else
            {
                Console.Beep(600, 500);
                Console.WriteLine("There is no customer yet");
                Console.WriteLine("Press any to continue...");
                Console.ReadKey();
            }
            Console.Clear();
        }
        #endregion

        #region Order Management
        private void AddOrder()
        {
            if (customer != null)
            {
                Order order = new Order(customer.Id, DateTime.Now);

                Console.WriteLine("Add order for " + customer.Fullname);
                do
                {
                    Console.WriteLine("\nENTER to set item");
                    Console.WriteLine("SPACE to search item");
                    Console.WriteLine("ESC to go back");

                    ConsoleKey key = Console.ReadKey().Key;

                    if (key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        AddItem(ref order);
                    }
                    else if (key == ConsoleKey.Spacebar)
                    {
                        Console.Clear();
                        SearchItems();
                    }
                    else if (key == ConsoleKey.Escape)
                    {
                        Console.Clear();
                        break;
                    }
                    Console.Clear();
                }
                while (true);
                Console.Clear();
                customer.Orders.Add(order);
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Please select a customer before create order");
                Console.ResetColor();
            }
        }

        private void CreateItem()
        {
            Console.WriteLine("Create a new item\n");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Clear();

            // Show Machines list
            Dictionary<int, int> properties = new Dictionary<int, int> { };
            Console.WriteLine("Add Item's properties\n");
            bool run = true;
            while(run)
            {
                try
                {
                    ShowMachines();
                    Console.Write("\nMachine ID: ");
                    string input = Console.ReadLine();

                    int id = int.Parse(input);

                    if(machines.Keys.Any(x => x == id))
                    {
                        Console.Write("Amount required: ");
                        int num = int.Parse(Console.ReadLine());
                        // Add Item Properties
                        properties.Add(id, num);
                    }
                    else
                    {
                        Console.WriteLine("\nNot found...");
                    }

                    Console.WriteLine("\nPress ENTER to add more property");
                    Console.WriteLine("Press ESC to go back");

                    while (true)
                    {
                        ConsoleKey key = Console.ReadKey().Key;
                        if (key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                            break;
                        }
                        else if (key == ConsoleKey.Escape)
                        {
                            Console.Clear();
                            run = false;
                            break;
                        }
                    }
                }
                catch(Exception exc)
                {
                    Console.Beep(1000, 500);
                    Log.Record(exc);
                }
            }

            // Adding Item & Item's properties
            Item stuff = new Item(name, 0, properties);
            stuff.Create();
        }

        private void ShowMachines()
        {
            if(machines == null)
            {
                List<Dictionary<string, object>> datalist = new Database("SELECT * FROM [machines]").Fetch();
                if (datalist != null)
                {
                    machines = new Dictionary<int, string> { };

                    foreach (Dictionary<string, object> item in datalist)
                    {
                        int id = (int)item["id"];
                        string name = (string)item["name"];
                        if (id.GetType() == typeof(int) && name.GetType() == typeof(string))
                        {
                            machines.Add(id, name);
                        }
                    }
                }
                else
                {

                }
            }

            if (machines != null)
            {
                foreach (KeyValuePair<int, string> item in machines)
                {
                    Console.WriteLine("{0}. {1}", item.Key, item.Value);
                }
            }
        }



        private void AddItem(ref Order order)
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.Write("Set item ID: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string set = Console.ReadLine();
                Console.ResetColor();
                try
                {
                    int id = int.Parse(set);
                    Item item = stocks.Find(x => x.Id == id);
                    if(item != null)
                    {
                        Console.Clear();
                        int amount = 1;
                        while(true)
                        {
                            try
                            {
                                // Validate integer input
                                Console.WriteLine("Item: {0}", item.Name);
                                Console.Write("Amount: ");
                                string num = Console.ReadLine();
                                if(num == String.Empty)
                                {
                                    break;
                                }
                                else
                                {
                                    amount = int.Parse(num);
                                    break;
                                }
                            }
                            catch
                            {
                                Console.Beep(1000, 500);
                            }
                        }

                        // Record Order
                        order.Sync();

                        // Adding Item to Order's orderline
                        order.AddItem(item.Id, amount);

                    }
                    else
                    {
                        Console.Beep(600, 500);
                        Console.WriteLine("Invalid item");
                    }

                }
                catch
                {
                    Console.Beep(1000, 500);
                    Console.WriteLine("Invalid Item ID");
                }

                Console.WriteLine("\nPress ENTER to add more\nPress ESC to go back");
                while(true)
                {
                    ConsoleKey key = Console.ReadKey().Key;
                    if (key == ConsoleKey.Enter)
                    {
                        break;
                    }
                    else if(key == ConsoleKey.Escape)
                    {
                        run = false;
                        break;
                    }
                }
                
            }
        }

        /// <summary>
        /// Search Items by ID or name
        /// </summary>
        private void SearchItems()
        {
            bool run = true;
            while(run)
            {
                Console.Write("Search: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string search = Console.ReadLine();
                Console.ResetColor();

                try
                {
                    // Seek only for ID
                    if (Regex.IsMatch(search, "^[0-9]+$"))
                    {
                        int id = int.Parse(search);
                        Item item = stocks.Find(x => x.Id == id);
                        if (item != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n{0}. {1}", item.Id, item.Name);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("\nNot found...");
                            Console.ResetColor();
                        }
                    }
                    // Match by name
                    else if (search.Length > 1)
                    {
                        List<Item> items = stocks.FindAll(x => x.Name.ToLower().Contains(search.ToLower()));

                        if (items != null)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("\nTotal items: {0}\n", items.Count);
                            Console.ResetColor();
                            Console.WriteLine("ID \t Name");
                            foreach (Item item in items)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("{0}. \t {1}", item.Id, item.Name);
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.Beep(600, 500);
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("Not found...");
                            Console.ResetColor();
                        }
                    }
                }
                catch
                {
                    
                }

                Console.WriteLine("\n\nPress ESC to go back or any to search more");
                while(true)
                {
                    ConsoleKey key = Console.ReadKey().Key;
                    if (key == ConsoleKey.Escape)
                    {
                        run = false;
                        break;
                    }
                    else if(key == ConsoleKey.Enter)
                    {
                        run = true;
                        break;
                    }
                }
                Console.Clear();
            }
            
        }

        public void ViewOrders()
        {
            int opt = 0;
            bool update = false;
            do
            {
                // Sync order
                if(!update)
                {
                    customer.Orders = Order.GetOrders(customer.Id);
                }

                Console.WriteLine("ID \t Dato");

                foreach (Order order in customer.Orders)
                {
                    Console.WriteLine("{0} \t {1}", order.Id, order.OrderDate.ToString("dd-MM-YYYY HH:mm:ss"));
                }

                Console.WriteLine("\nENTER to select Order");
                Console.WriteLine("\nESC to go back");

                while(Console.ReadKey().Key != ConsoleKey.Escape)
                {
                    Console.WriteLine("Choose Order ID: ");
                    string input = Console.ReadLine();
                }
            }
            while (OrderOption(ref opt));
        }

        private bool OrderOption(ref int choice)
        {
            ConsoleKey key = Console.ReadKey().Key;
            if (key != ConsoleKey.Escape)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Text Display
        private void Selection()
        {
            if(customer == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("No Selected customer");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Selected: " + customer.Fullname);
                Console.ResetColor();
            }
        }

        private void MenuText()
        {
            Console.WriteLine("########\tMenu choice\t########");
            Console.WriteLine("(Press Escape or 0 to exit)\n");
            Selection();
            Console.WriteLine("1. New customer");
            Console.WriteLine("2. Select a customer");

            if (customer == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            Console.WriteLine("3. Add an order");
            Console.WriteLine("4. Show orders");
            Console.ResetColor();

            Console.WriteLine("5. Create item");

        }

        private bool KeyChoice()
        {
            // Show menu text while looping
            MenuText();

            bool choice = true;
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D0:
                    choice = false;
                    menuNum = 0;
                    break;
                case ConsoleKey.Escape:
                    choice = false;
                    menuNum = 0;
                    break;
                case ConsoleKey.D1:
                    menuNum = 1;
                    break;
                case ConsoleKey.D2:
                    menuNum = 2;
                    break;
                case ConsoleKey.D3:
                    menuNum = 3;
                    break;
                case ConsoleKey.D4:
                    menuNum = 4;
                    break;
                case ConsoleKey.D5:
                    menuNum = 5;
                    break;
                case ConsoleKey.D6:
                    menuNum = 6;
                    break;
                case ConsoleKey.D7:
                    menuNum = 7;
                    break;
                case ConsoleKey.D8:
                    menuNum = 8;
                    break;
                case ConsoleKey.D9:
                    menuNum = 9;
                    break;
                default:
                    menuNum = -1;
                    break;
            }

            Console.Clear();

            return choice;
        }
        #endregion
    }
}


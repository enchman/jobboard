using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLib
{
    public class ViewConsole
    {
        public Controller control = new Controller();

        public void MenuList()
        {
            
            Console.WriteLine("############ \t\tWork board\t\t ############\n");
            Console.WriteLine("Press ESC to exit");
            Console.WriteLine("1. Update work rutine");
            Console.Write("2. Add order{0}\n", "");
        }

        public void Action(ConsoleKey key)
        {
            Console.Clear();
            switch (key)
            {
                case ConsoleKey.D0:
                    break;
                case ConsoleKey.D1:
                    break;
                case ConsoleKey.D2:
                    break;
                case ConsoleKey.D3:
                    break;
                case ConsoleKey.D4:
                    break;
                case ConsoleKey.D5:
                    break;
                case ConsoleKey.D6:
                    break;
                case ConsoleKey.D7:
                    break;
                case ConsoleKey.D8:
                    break;
                case ConsoleKey.D9:
                    break;
                default:
                    break;
            }
        }

        private void FakeOrder()
        {
            
        }

        private void FakeItem()
        {

        }
    }
}

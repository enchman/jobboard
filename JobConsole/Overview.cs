using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobEngine;

namespace JobConsole
{
    class Overview
    {
        private int menuNum = 0;

        private List<Employee> workers = new List<Employee> { };
        private Employee currentWorker = null;

        public Overview()
        {
            workers = Employee.GetEmployees();

        }

        #region View method

        private void SearchEmployee(string query)
        {
            Employee result = workers.Find(x => x.Username == query);
            if (result != null)
            {
                Console.WriteLine("{0}. {1}", result.Id, result.Fullname);
            }
        }



        #endregion


        #region Action methods

        private void Action()
        {

        }

        #endregion
        private void MenuText()
        {
            Console.WriteLine("#################### Job overview ####################\n\n");
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
    }
}

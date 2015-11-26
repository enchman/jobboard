using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobLib;

namespace JobConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Initialize();
        }

        static void Initialize()
        {
            while(Console.ReadKey().Key != ConsoleKey.Escape)
            {
                // Key cycle
            }
        }
    }
}

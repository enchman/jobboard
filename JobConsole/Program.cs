using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobEngine;
using System.Data.SqlClient;
using System.Data;

namespace JobConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Initialize();
            //DateTime time = DateTime.Now.AddDays(5);
            //Console.WriteLine(time.ToString());
            //Console.ReadKey();
        }

        static void Initialize()
        {
            Menu nav = new Menu();
        }

        static void DisplayData(List<Dictionary<string, object>> data)
        {
            if(data != null)
            {
                foreach(Dictionary<string, object> item in data)
                {
                    foreach(KeyValuePair<string, object> row in item)
                    {
                        Console.Write("{0}", row.Key);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(":");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("{0}", row.Value);
                        Console.ResetColor();
                        Console.Write("; ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}

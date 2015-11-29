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
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                string sql = "[setEmployee]";
                Dictionary<string, object> param = new Dictionary<string, object> { };
                param.Add("name", "Test Stored Procedure");

                Database db = new Database(sql, param);

                Console.WriteLine("Inserted");
            }
                

            //while(Console.ReadKey().Key != ConsoleKey.Escape)
            //{
            //    //Console.Write("SQL QUERY: ");
            //    //string sql = Console.ReadLine();
            //    string sql = "INSERT INTO [employees] ([name]) VALUES(@name)";
            //    Console.Write("QUERY: {0}", sql);
            //    Dictionary<string, object> param = new Dictionary<string, object> { };
            //    param.Add("name", "Sam Møller");

            //    Database db = new Database(sql, param);

            //    Console.WriteLine("Executed");

            //    //List<Dictionary<string, object>> items = db.Fetch();

            //    //foreach(Dictionary<string, object> item in items)
            //    //{
            //    //    foreach(KeyValuePair<string, object> data in item)
            //    //    {

            //    //        Console.Write("{0}", data.Key);
            //    //        Console.ForegroundColor = ConsoleColor.Gray;
            //    //        Console.Write(":");
            //    //        Console.ResetColor();
            //    //        Console.ForegroundColor = ConsoleColor.Green;
            //    //        Console.Write("{0}", data.Value.ToString());
            //    //        Console.ResetColor();
            //    //        Console.ForegroundColor = ConsoleColor.DarkRed;
            //    //        Console.Write(";\t");
            //    //        Console.ResetColor();
            //    //    }
            //    //    Console.WriteLine();
            //    //}

            //    //Console.WriteLine("Inserted employee");
            //}
        }
    }
}

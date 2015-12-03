using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using JobLib;
using JobEngine;
using System.Data.SqlClient;
using System.Data;

namespace JobConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize();
            //Run();
        }

        static void TraceMessage(string message,
        [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            System.Diagnostics.Trace.WriteLine("message: " + message);
            System.Diagnostics.Trace.WriteLine("member name: " + memberName);
            System.Diagnostics.Trace.WriteLine("source file path: " + sourceFilePath);
            System.Diagnostics.Trace.WriteLine("source line number: " + sourceLineNumber);
            int index = sourceFilePath.LastIndexOf(@"\");
            string path = sourceFilePath.Substring(index + 1);
            Console.WriteLine(path);
            Console.ReadKey();
        }

        static void Run()
        {
            Console.WriteLine("Welcome to SQL Console");
            Console.WriteLine("Press Any to continue");
            while(Console.ReadKey().Key != ConsoleKey.Escape)
            {
                //PrepareParam();
                //Database db = new Database("SELECT * FROM viewCustomers");
                //List<Dictionary<string, object>> data = db.Fetch();
                //DisplayData(data);
            }
        }

        static void Initialize()
        {
            do
            {
                Console.Write("Query: ");
                string sql = Console.ReadLine();
                Console.Clear();
                Console.WriteLine(sql);

                Console.Write("Firstname: ");
                string firstname = Console.ReadLine();

                Console.Write("Lastname: ");
                string lastname = Console.ReadLine();

                Console.Write("Phone: ");
                string phone = Console.ReadLine();

                Console.Write("Email: ");
                string email = Console.ReadLine();

                Dictionary<string, object> param = new Dictionary<string, object> { };
                param.Add("firstname", firstname);
                param.Add("lastname", lastname);
                param.Add("phone", phone);
                param.Add("email", email);

                Db db = new Db(sql, param);
                db.Execute(System.Data.CommandType.StoredProcedure);

                Console.Clear();
                Console.WriteLine("Execute -> {0}", sql);
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        static void PrepareParam()
        {
            Console.WriteLine("Running SQL");
            Dictionary<string, object> param = new Dictionary<string, object> { };

            Console.Write("Query: ");
            string sql = Console.ReadLine();

            Console.Clear();

            Console.WriteLine("Press Enter to add parameters");

            while (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Console.Write("Parameter name: ");
                string name = Console.ReadLine();
                Console.Write("Parameter value: ");
                string value = Console.ReadLine();

                Console.Clear();
                param.Add(name, value);
                Console.WriteLine("Press Enter to add more");
            }

            Console.Clear();

            Console.WriteLine("Want to run \"{0}\"", sql);
            Console.WriteLine("Press ENTER to run");
            if(Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Db db;
                if (param.Count != 0)
                {
                    db = new Db(sql, param);
                }
                else
                {
                    db = new Db(sql);
                }

                db.Execute();

                Console.WriteLine("Executed query");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Cancel SQL");
            }
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

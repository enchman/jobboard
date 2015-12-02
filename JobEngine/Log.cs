using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace JobEngine
{
    class Log
    {
        public static bool FullPath = false;
        public static void Record(
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string method = "",
            [CallerFilePath] string path = "")
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object> { };
                param.Add("line", line);
                param.Add("occurDate", DateTime.Now);
                param.Add("method", method);
                param.Add("file", TrimPath(path));
                new Database("recordError", param).FetchProcedure();
            }
            catch
            {

            }
        }

        public static void Record(string message,
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string method = "",
            [CallerFilePath] string path = "")
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object> { };
                param.Add("line", line);
                param.Add("occurDate", DateTime.Now);
                param.Add("method", method);
                param.Add("file", TrimPath(path));
                param.Add("message", message);
                new Database("recordError", param).FetchProcedure();
            }
            catch
            {

            }
        }

        public static void Record(Exception exc,
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string method = "",
            [CallerFilePath] string path = "")
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object> { };
                param.Add("line", line);
                param.Add("occurDate", DateTime.Now);
                param.Add("method", method);
                param.Add("file", TrimPath(path));
                param.Add("message", exc.Message);
                new Database("recordError", param).FetchProcedure();
            }
            catch
            {

            }
        }

        private static string TrimPath(string data)
        {
            if(!FullPath)
            {
                int index = data.LastIndexOf(@"\");
                if(index >= 0)
                {
                    return data.Substring(index + 1);
                }
            }
            return data;
        }
    }
}


    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;
    using System.Net;
    namespace LS.MetadataAdapter
    {
        public class Helpers
        {
            public static void PrintMessage(string message, ConsoleColor color)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
            }
            public static string[] SplitString(string text, string separators)
            {
                // return arg.Split(separators.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                // not available in .NET 1.1

                IList tokens = new ArrayList(text.Split(separators.ToCharArray()));

                int pos = 0;
                while (pos < tokens.Count)
                {
                    string token = (string)tokens[pos];
                    if ((token == null) || (token.Length == 0)) tokens.RemoveAt(pos);
                    else pos++;
                }

                string[] _tokens = new string[tokens.Count];
                for (int i = 0; i < tokens.Count; i++) _tokens[i] = (string)tokens[i];
                return _tokens;
            }
            public static string GetLsHostIP()
            {
                //IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
                //IPAddress[] addr = ipEntry.AddressList;
                //foreach (var ip in addr)
                //    MyLogger.PrintConsoleMessage("Currrent IP: " + ip.ToString(), ConsoleColor.Cyan);

                //return addr[0].ToString();

                return System.Configuration.ConfigurationManager.AppSettings["LsHostIP"].ToString();
            }
        }
    }



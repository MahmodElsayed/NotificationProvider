using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.MetadataAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Starting adapter
                AdapterLauncher adapterLauncher = new AdapterLauncher();
                adapterLauncher.StartMetaDataAdapter();
            }
            catch(Exception exp)
            {
                //Starting adapter
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Startup Error. Error Message : {0} ",exp.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
          
        }
    }
}

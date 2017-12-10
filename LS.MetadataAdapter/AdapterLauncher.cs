using Lightstreamer.DotNet.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LS.MetadataAdapter
{
    public class AdapterLauncher
    {
        #region Declerations
        string lsServerIP = string.Empty;
        int lsPort = -1;
        #endregion

        #region Public Methods
        public void StartMetaDataAdapter()
        {
            try
            {
                Console.WriteLine("Starting MetaDataAdapter..........!");

                MetadataAdapter metadataAdapter = new MetadataAdapter();

                MetadataProviderServer serverMeta = new MetadataProviderServer();

                serverMeta.Adapter = metadataAdapter;

                string lsServerIP = ConfigurationManager.AppSettings.Get("LsServerIP");
               
                int lsPort = Convert.ToInt32(ConfigurationManager.AppSettings.Get("LsPort"));

                TcpClient reqrepSocket = new TcpClient(lsServerIP, lsPort);

                serverMeta.RequestStream = reqrepSocket.GetStream();

                serverMeta.ReplyStream = reqrepSocket.GetStream();

                serverMeta.Start();


                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Remote Meta Adapter connected to Lightstreamer Server on  : {0}.",lsServerIP);
                Console.ForegroundColor = ConsoleColor.White;
              
                // log to file here
            }
            catch(Exception exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Remote Meta Adapter failed to establish connectin with Lightstreamer Server on IP : {0} and Port {1}.",lsServerIP,lsPort);
                Console.Write("Error : {0}", exp.Message);
                Console.ForegroundColor = ConsoleColor.White;

                // log to file should be added here.
            }
        }

        #endregion
    }
}

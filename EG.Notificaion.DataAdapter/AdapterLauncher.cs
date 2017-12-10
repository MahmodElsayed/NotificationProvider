

using System;
using System.Net.Sockets;

using Lightstreamer.DotNet.Server;

using System.Threading;
using NLog;

namespace EFG.Notification.DataAdapter
{
    public class DataAdaptersLauncher
    {
        private static Logger m_NLog = LogManager.GetLogger("AppLogger");

        public static void Main(string[] args)
        {

            try
            {
                // Startup Provider and Connect to LightStreamer Server.

                string host = System.Configuration.ConfigurationManager.AppSettings.Get("LsServerIP");
                int reqrepPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get("LsRequestPort"));
                int notifPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get("LsNotifyPort"));

                DataAadapter feedDataAdapter = new DataAadapter();
                feedDataAdapter.Initialize();
                feedDataAdapter.ConnectToFeedProvider();

                
                DataProviderServer server = new DataProviderServer();
                server.Adapter = feedDataAdapter;

                TcpClient reqrepSocket = new TcpClient(host, reqrepPort);
                server.RequestStream = reqrepSocket.GetStream();
                server.ReplyStream = reqrepSocket.GetStream();


                TcpClient notifSocket = new TcpClient(host, notifPort);
                server.NotifyStream = notifSocket.GetStream();

                server.Start();
                
                m_NLog.Info("Remote DataAdapter connected succssfully to LS on IP {0} and Port {1}", host, reqrepPort);
                
                Console.ReadLine();



            }
            catch (Exception exp)
            {
                m_NLog.Warn("Make sure Lightstreamer Server is started before this Adapter.");
                m_NLog.Error("Error : Could not connect to Lightstreamer Server. Error Details : {0}", exp.ToString());
                Console.ReadLine();

            }
        }

    }
}
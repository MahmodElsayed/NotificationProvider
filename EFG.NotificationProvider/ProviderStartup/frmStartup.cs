using Contract;
using FeedMessagesProcessror;
using MessageRouter;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SessionsManagement;
using FeedServiceWCF;
using System.ServiceModel;
using EFG.OPS.NotificationEngineService.Contracts.Entities;
using NotificaionEngineHandler;
using ProviderStartUp;
using MonitorStorage;
using NotificationsLogger;

namespace EFG.NotificationProvider
{
    public partial class frmStartup : Form
    {

        #region private memebers
        private static Logger m_NLog = null;
        private dsMonitor m_dsMonitor = null;

        private BlockingCollection<string> m_NotificationRequests = null;
        private BlockingCollection<string> m_NotificationRemoveRequests = null;
        private BlockingCollection<NotificationMessage> m_NotificationMessages = null;
        private BlockingCollection<NotificationMessage> m_NotificationMessagesLog = null;
        private BlockingCollection<FeedMessage[]> m_FeedMessagesLog= null;
        private NotificationLogHandler m_NotificaionLogHandler = null;
        private Router m_Router = null;
        private NotificaionEngineProxy m_NotificaionEngineProxy = null;
        FeedProcessor m_FeedProcessor = null;
        SessionsManager m_SessionManager = null;

        FeedService m_FeedService = null;
        private ServiceHost serviceHost = null;

        #endregion

        #region Constructor
        public frmStartup()
        {
            InitializeComponent();

            StartService();

        }
        #endregion

        private void StartService()
        {
            try
            {

                m_NLog = LogManager.GetLogger("AppLogger");
                m_NLog.Info("Starting Notification Provider.");

                m_NotificationRequests = new BlockingCollection<string>();
                m_NotificationMessages = new BlockingCollection<NotificationMessage>();
                m_NotificationMessagesLog = new BlockingCollection<NotificationMessage>();
                m_NotificationRemoveRequests = new BlockingCollection<string>();
                m_FeedMessagesLog = new BlockingCollection<FeedMessage[]>();

                m_dsMonitor = new dsMonitor();

                //Start NotificationEngine proxy handler.
                m_NotificaionEngineProxy = new NotificaionEngineProxy(m_NotificationRequests, m_NotificationMessages,
                m_NotificationMessagesLog,m_NotificationRemoveRequests);
                m_NotificaionEngineProxy.Initialize();
                m_NotificaionEngineProxy.ConnectToNotificationEngine();  // subscribe to Notificaion Engine.


                #region Sessions Management
                m_SessionManager = new SessionsManager(m_NotificationRequests, m_NotificationRemoveRequests,m_FeedMessagesLog,m_dsMonitor);
                #endregion

                #region Initialize Router
                m_Router = new Router(m_SessionManager);
                #endregion

                #region Start Process Received Feed
                m_FeedProcessor = new FeedProcessor(m_NotificationMessages, m_Router);
                m_FeedProcessor.StartHanldeReceivedFeed();
                #endregion

                #region Logs 
                if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["LogNotificationMsgs"]))
                {
                    m_NotificaionLogHandler = new NotificationLogHandler(m_NotificationMessagesLog, m_FeedMessagesLog, m_dsMonitor); ;
                    m_NotificaionLogHandler.StartLogging();
                }

                #endregion


                #region test
                //  Test
              //  m_NotificationRequests.Add("Notify_User#1248");

                //Task.Factory.StartNew(() =>
                //{
                //    TestFeed();
                //});
                #endregion

                #region Start  Wcf Service
                try
                {
                    m_FeedService = new FeedService(m_SessionManager);
                    serviceHost = new ServiceHost(m_FeedService);
                    serviceHost.Open();
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    m_NLog.Info("Notification Provider Service started and ready to handle dataAdapters requests.");
                    Console.ResetColor();
                }
                catch (Exception exp)
                {
                    m_NLog.Error("Error in Start Notification Provider Service(). Error Details : {0}", exp.ToString());
                }
                #endregion
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in Service Startup. Error Details : ", exp.ToString());
            }
        }
        private void TestFeed()
        {
            // while (true)
            {
                foreach (NotificationMessage notificationMsg in m_NotificationMessages.GetConsumingEnumerable())
                {
                    Console.WriteLine("NotificaionMessage received at {0} . NotificationKey = {1}", DateTime.Now, notificationMsg.NotificationKey);

                    foreach (string key in notificationMsg.BodyDictionary.Keys)
                    {

                        string values = string.Format("FieldName = {0} , Value = {1} ", key, notificationMsg.BodyDictionary[key]);
                        Console.WriteLine(values);

                    }

                    Console.WriteLine("=====================================");

                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void monitorToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMonitor frm = new frmMonitor(m_dsMonitor);
            frm.ShowDialog();
        }
    }
}
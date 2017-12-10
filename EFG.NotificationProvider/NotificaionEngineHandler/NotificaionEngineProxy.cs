using EFG.OPS.NotificationEngineService.Contracts.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFG.OPS.NotificationEngineService.Contracts.Entities;
using System.ServiceModel;
using System.Collections.Concurrent;
using System.Timers;

namespace NotificaionEngineHandler
{
    public class NotificaionEngineProxy : IPublishNotification
    {
        #region private members
        private static Logger m_NLog;
        private ISubscribeNotification proxy;
        private BlockingCollection<string> m_NotificationRequests = null; // to subscribe in notificaion
        private BlockingCollection<string> m_NotificationRemoveRequests = null; // to remove subscription in Notificaion
        private BlockingCollection<NotificationMessage> m_NotificationMessages = null;
        private BlockingCollection<NotificationMessage> m_NotificationMessagesLog= null;
        private bool logNotificationMessages = false;
        private bool IsConnectedToEngine = false;
        private System.Timers.Timer PingTimer = null;
        private int m_PingTimerIntervals = 30000;



        #endregion


        #region Constructor
        public NotificaionEngineProxy(
            BlockingCollection<string> notificationReq,
            BlockingCollection<NotificationMessage> notificationMessages,
            BlockingCollection<NotificationMessage> notificationMessagesLog,
            BlockingCollection<string> notificationRemoveRequests)
        {
            try
            {
                m_NLog = LogManager.GetLogger("AppLogger");
                m_NotificationRequests = notificationReq;
                m_NotificationMessages = notificationMessages;
                m_NotificationMessagesLog = notificationMessagesLog;
                m_NotificationRemoveRequests = notificationRemoveRequests;
               
            }
            catch(Exception exp)
            {
                Console.WriteLine("Nlog load Error. Error Details : {0} ",exp.ToString());
            }
        }
        #endregion

        #region Private Methods


        private void InitProxy()
        {
            try     
            {
                proxy = null;
                DuplexChannelFactory<ISubscribeNotification> factory = new DuplexChannelFactory<EFG.OPS.NotificationEngineService.Contracts.Interfaces.ISubscribeNotification>(new InstanceContext(this), "ISubscribeNotificationEndPoint");
                proxy = factory.CreateChannel();

            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in InitProxy method . Error Details : {0} ", exp.ToString());
            }
        }

        private void NotificationRequestsHandler()
        {
           
            while (IsConnectedToEngine == false)
            {
                System.Threading.Thread.Sleep(3000);
                continue;
            }


            foreach (string notificaionRequest in m_NotificationRequests.GetConsumingEnumerable())
            {
                try
                {
            
                    GetNotifications(notificaionRequest);
                   
                }
                catch (Exception ex)
                {
                    m_NLog.Error("Error in NotificationRequestsHandler method. Error details : {0}", ex.ToString());
                }

            }
        }

        private void RemoveNotificationRequestsHandler()
        {

           

            foreach (string notificaionRequest in m_NotificationRemoveRequests.GetConsumingEnumerable())
            {
                try
                {
                    UnsubscribeNotificaions(notificaionRequest);
                }
                catch (Exception ex)
                {
                    m_NLog.Error("Error in GLRequestsHandler method. Error details : {0}", ex.ToString());
                }

            }
        }

       

        #region Wcf ping routine.
        private void StartPing()
        {
            try
            {
                PingTimer = new System.Timers.Timer(m_PingTimerIntervals);
                PingTimer.Elapsed += new System.Timers.ElapsedEventHandler(PingTimer_Elapsed);
                PingTimer.Start();
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in StartPing(). Error Details : {0}", exp.ToString());
                IsConnectedToEngine = false;
            }

        }
        private void PingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {

                PingTimer.Stop();
                PingNotificaionEngine();
                

            }
            catch (Exception ex)
            {
                m_NLog.Error("Error in PingTimer_Elapsed . Error Details : {0} ", ex.ToString());
            }
            finally { PingTimer.Start(); };
        }

        private void PingNotificaionEngine()
        {
            try
            {
                if (proxy.Ping())
                    m_NLog.Info("Ping to Notificaion Engine applied at {0} ", DateTime.Now.ToLongTimeString());

            }
            catch (Exception ex)
            {
                m_NLog.Error("Failed to ping Notification Engine . Error Details : {0} ", ex.ToString());
                InitProxy();
                ConnectToNotificationEngine();
            }
        }

        #endregion
      
        #endregion

        #region PublicMethods
        public void Initialize()
        {
            try
            {
                logNotificationMessages = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["LogNotificationMsgs"]);
                m_PingTimerIntervals = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["PingTiemrIntervals"]);


                InitProxy();


                Task.Factory.StartNew(() =>
                {
                    NotificationRequestsHandler();
                });

            }
            catch (Exception ex)
            {
                m_NLog.Error("Error in Initialize(). Error Details : {0}", ex.ToString());
            }

        }


        public void ConnectToNotificationEngine()
        {

            try
            {
                m_NLog.Info("Connecting to Notifications Engine...");

                bool result = Subscribe();
                if (result)
                {
                    m_NLog.Info("Connected successfully to Notifications Engine.");
                    IsConnectedToEngine = true;

                    //start ping on Notificaion Engine
                    //Ping timer
                    m_NLog.Info("Start apply heartbeat to Notoficaion Engine.");
                    StartPing();

                }
                else
                {
                    m_NLog.Warn("Could not subscribe to Notifications Engine.");
                    System.Threading.Thread.Sleep(1000);
                    InitProxy();
                    ConnectToNotificationEngine();
                }
            }
            catch (Exception ex)
            {
                m_NLog.Error("Error in ConnectToNotificationEngine(). Error Details : {0}", ex.ToString());
                //reconnect to engine
                InitProxy();
                ConnectToNotificationEngine();
            }
        }

     



        #endregion




        #region Public Members
       
        #endregion


        #region IPublishNotification and ISubscribeNotification Implmentaion
        /// <summary>
        /// Wcf callback 
        /// </summary>
        /// <param name="notificationMessage"></param>
        /// <param name="notificationKey"></param>
        void IPublishNotification.PublishNotification(NotificationMessage notificationMsg)
        {
            try
            {
                Console.WriteLine("Notificaion Received");
              //  notificationMsg.NotificationKey = "Notify_User_1248";

                if (notificationMsg != null)
                    m_NotificationMessages.Add(notificationMsg);

                if (logNotificationMessages)
                    m_NotificationMessagesLog.Add(notificationMsg);
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in PublishNotifications(). Error Details : {0}", exp.ToString());
            }

        }
        /// <summary>
        /// Wcf callback
        /// </summary>
        /// <param name="notificationMessages"></param>
        /// <param name="notificationKey"></param>
        void IPublishNotification.PublishNotifications(NotificationMessage[] notificationMessages)
        {
            try
            {
                Console.WriteLine("Snapshot Notificaions Received");
                foreach (NotificationMessage notificationMsg in notificationMessages)
                {
                    if(notificationMsg!=null)
                    m_NotificationMessages.Add(notificationMsg);

                    if (logNotificationMessages)
                        m_NotificationMessagesLog.Add(notificationMsg);
                }
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in PublishNotifications(). Error Details : {0}", exp.ToString());
            }
        }

        bool Subscribe()
        {
            bool subscribeResult = false;
            try
            {
              subscribeResult =  proxy.Subscribe();

            }
            catch(Exception exp)
            {
                m_NLog.Error("Error in ISubscribeNotification.Subscribe(). Error Details : {0}", exp.ToString());
                subscribeResult = false;
            }
            return subscribeResult;
        }

        void GetNotifications(string notificationKey)
        {
            try
            {
                if (notificationKey != null && notificationKey.Length > 0)
                {
                    proxy.GetNotifications(notificationKey);
                    m_NLog.Info("Notification requset : {0} sent to Notificaion Engine at {1}", notificationKey, DateTime.Now.ToLongTimeString());
                }
            }
            catch(Exception exp)
            {
                m_NLog.Error("Error in ISubscribeNotification.GetNotifications(). Error Details : {0}", exp.ToString());
            }
        }

        void UnsubscribeNotificaions(string notificationKey)
        {
            try
            {
                if (notificationKey != null && notificationKey.Length > 0)
                    proxy.UnsubscribeNotificaions(notificationKey);
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in ISubscribeNotification.UnsubscribeNotificaions(). Error Details : {0}", exp.ToString());
            }
        }
        #endregion
    }
}

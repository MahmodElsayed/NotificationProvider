

using System;
using System.Collections;
using System.Threading;
using Lightstreamer.Interfaces.Data;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using NLog;
using Contract;
using System.ServiceModel;
using System.Timers;
using MessagesLogger;

namespace EFG.Notification.DataAdapter
{
    public class DataAadapter : IDataProvider
    {
        #region Declerations
        //event listener
        private IItemEventListener m_listener;
        //subscribtion list of items
        private Dictionary<string, string> m_itmesSubsciptions = null; //will hold list of subscribed items.
       
        //wcf
        IDataFeed m_IDataFeedService = null;
        IFeedCallbackHandler m_FeedcallbackHandler;
        InstanceContext m_InstanceContext;
        DuplexChannelFactory<IDataFeed> m_factory;

        string m_providerUserName;
        string m_providerPassword;

        // feed storage
        BlockingCollection<FeedMessage> m_ReceivedFeedBCollecion = null; // will store received feed from Notification provider
        private ConcurrentQueue<string> m_subscirptionQueue = null; // receive subscription from lightstreamer server.


    

        private static Logger m_NLog = LogManager.GetLogger("AppLogger");

        private System.Timers.Timer PingTimer = null;
        private int m_PingTimerIntervals = 3000;
        private bool IsConnectedToProvider = false;
        private bool EnableLogger = false;
        LogsHandler m_logHandler = null;

        #endregion

        #region Constructor
        public DataAadapter()
        {
           
        }
        #endregion

        #region Public Methods
        public void Initialize()
        {
            try
            {
               
                m_itmesSubsciptions = new Dictionary<string, string>();

                //
                m_providerUserName = System.Configuration.ConfigurationManager.AppSettings.Get("UserName");
                m_providerPassword = System.Configuration.ConfigurationManager.AppSettings.Get("Password");

                m_ReceivedFeedBCollecion = new BlockingCollection<FeedMessage>();
                // establish connecion with wcf FeedProvider Service
                //wcf create channel with  Provider.
                EnableLogger = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["EnableLogger"]);
                 m_logHandler = new LogsHandler();
                m_FeedcallbackHandler = new IFeedCallbackHandler(m_ReceivedFeedBCollecion,EnableLogger,m_logHandler);
                m_InstanceContext = new InstanceContext(m_FeedcallbackHandler);
                m_factory = new DuplexChannelFactory<IDataFeed>(m_InstanceContext, "netTcpBinding_Ifeed");
                m_IDataFeedService = m_factory.CreateChannel();

                ((ICommunicationObject)m_IDataFeedService).Closed += DataAadapter_Closed;
                ((ICommunicationObject)m_IDataFeedService).Faulted += DataAadapter_Faulted;
                ((ICommunicationObject)m_IDataFeedService).Opened += DataAadapter_Opened;

                m_PingTimerIntervals = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["PingTiemrIntervals"]);
              

                if(EnableLogger)
                {
                    m_logHandler.StartLogging();
                }

                m_subscirptionQueue = new ConcurrentQueue<string>();

                Task.Factory.StartNew(() =>
                {
                    StartProcessFeed(); // sending feed to LS server.
                });


                Task.Factory.StartNew(() =>
                {
                    HandleSubscriptions();  // receive subscribtion from LS server.
                });
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error on Constructor. Error Details : {0} ", exp.ToString());
            }
        }

        public void ConnectToFeedProvider()
        {
            bool loginResult = false;
            try
            {
                 loginResult = LoginToProvier();
            }
            catch(Exception exp)
            {
                m_NLog.Error("Error in method ConnectToFeedProvider. Error Details : {0}", exp.ToString());
                loginResult = false;
            }
            finally
            {
                if(!loginResult)
                {
                    System.Threading.Thread.Sleep(500);
                    m_factory = null;
                    m_IDataFeedService = null;
                    m_factory = new DuplexChannelFactory<IDataFeed>(m_InstanceContext, "netTcpBinding_Ifeed");
                    m_IDataFeedService = m_factory.CreateChannel();
                    ConnectToFeedProvider();
                }
            }
        }
        #endregion

        #region Private Methods


        private bool LoginToProvier()
        {
            bool loginResult = false;
            try
            {
                m_NLog.Info("Try connect to Notificaion Provider....");
                LoginRequest lrerquest = new LoginRequest();
                lrerquest.UserName = m_providerUserName;
                lrerquest.Password = m_providerPassword;

                LoginResponse lresponse = m_IDataFeedService.Login(lrerquest);
                if (lresponse.ServiceLoginStatus == LoginStatus.Succeeded)
                {
                    m_NLog.Info("Logged in successfully to Notificaion Provider!");
                    IsConnectedToProvider = true;
                    ReSubscribe();
                    //start ping timer
                    StartPing();

                    return loginResult = true;
                }
                else
                {
                    m_NLog.Warn("Failed to login to Notificaion Provider. Login Status :", lresponse.ServiceLoginStatus.ToString());
                }
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in method LoginToProvier. Error Details : {0} ", exp.ToString());
            }
            return loginResult;
        }

        private void HandleSubscriptions()
        {
            while (true)
            {
                try
                {
                    string subscriptionCode;
                    bool isSubcodeValid = m_subscirptionQueue.TryDequeue(out subscriptionCode);
                    if (isSubcodeValid)
                    {
                        m_IDataFeedService.Subscribe(subscriptionCode, m_providerUserName);

                        if(EnableLogger)
                        {
                            m_logHandler.SubscriptionInfo.Add(string.Format("Subscription to Item : {0} sent to Provider : {1}", subscriptionCode, m_providerUserName));
                        }

                    }
                    else
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
                catch (Exception exp)
                {

                    m_NLog.Error("Error in method HandleSubscriptions. Error Details: {0}", exp.ToString());
                    continue;
                }
            }
        }

        private void DataAadapter_Opened(object sender, EventArgs e)
        {
            m_NLog.Info("Channel Opened to FeedProvider !");
        }

        private void DataAadapter_Faulted(object sender, EventArgs e)
        {
            m_NLog.Warn("Session to FeedProvider Faulted!");
            // reconnect
            //1-Login
            //2- Resubscribe to all items.

        }

        private void DataAadapter_Closed(object sender, EventArgs e)
        {
            m_NLog.Warn("Session to FeedProvider Closed");
            // reconnect
        }

        private void StartProcessFeed()
        {

            foreach (FeedMessage feedMsg in m_ReceivedFeedBCollecion.GetConsumingEnumerable())
            {
                try
                {
                      SendToLsServer(feedMsg);
                }
                catch (Exception ex)
                {
                    m_NLog.Error("Error in StartProcessFeed() . error detaisl : {0}", ex.ToString());
                }
            }
        }
        private void SendToLsServer(FeedMessage feedMsg)
        {
            try
            {
                string itemName = string.Empty;
                itemName = feedMsg.SubscriptionCode;
                m_listener.Update(itemName, feedMsg.MessageFields, true);

                if(EnableLogger)
                {
                    m_logHandler.SentFeedMessages.Add(feedMsg);
                }
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in ProcessFeed method. Error Details : {0}", exp.ToString());
            }
        }

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
                IsConnectedToProvider= false;
            }

        }

        private void PingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {

                PingTimer.Stop();

                bool pingresult = m_IDataFeedService.Ping();

                if (pingresult)
                    m_NLog.Info("Ping applied to Notificaion Provider at : {0} ", DateTime.Now.ToLongTimeString());
                else
                    ConnectToFeedProvider();

            }
            catch (Exception ex)
            {
                m_NLog.Error("Error in PingTimer_Elapsed . Error Details : {0} ", ex.ToString());
                ConnectToFeedProvider();
            }
            finally { PingTimer.Start(); };
        }
        #endregion

        #region IDataProvider Interface
        void IDataProvider.Init(IDictionary parameters, string configFile)
        {
            // throw new NotImplementedException();
        }

        bool IDataProvider.IsSnapshotAvailable(string itemName)
        {
            return false;
        }

        void IDataProvider.SetListener(IItemEventListener eventListener)
        {
            m_listener = eventListener;
        }

        void IDataProvider.Subscribe(string subscriptionCode)
        {
            try
            {

                if (m_itmesSubsciptions.ContainsKey(subscriptionCode) == false)
                {
                    m_itmesSubsciptions.Add(subscriptionCode, subscriptionCode);

                    //send subsciption to the queue.
                    m_subscirptionQueue.Enqueue(subscriptionCode);

                    if(EnableLogger)
                    {
                        m_logHandler.SubscriptionInfo.Add(String.Format("Subscription of Item : {0} Received from LightStreamer Server",subscriptionCode));
                    }
                }
            }
            catch (Exception exp)
            {
                m_NLog.Error("Erro in IDataProvider.Subscribe. Error Details : {0} ", exp.ToString());
            }
        }

        void IDataProvider.Unsubscribe(string subscriptionCode)
        {

            try
            {
                if (m_itmesSubsciptions.ContainsKey(subscriptionCode))
                {
                    m_itmesSubsciptions.Remove(subscriptionCode);


                }
                if (EnableLogger)
                {
                    m_logHandler.SubscriptionInfo.Add(String.Format("UnSubscribe request of Item : {0} Received from LightStreamer Server", subscriptionCode));
                }



                // unsubscibe from the feed provider.
                m_IDataFeedService.UnSubscribe(subscriptionCode, m_providerUserName);
                if (EnableLogger)
                {
                    m_logHandler.SubscriptionInfo.Add(String.Format("UnSubscribe request of Item : {0} sent to Provider : {1}", subscriptionCode, m_providerUserName));
                }

                m_NLog.Info("Item : {0} Unsubscibed from FeedService!",subscriptionCode);
            }
            catch (Exception exp)
            {
                m_NLog.Error("Erro in method IDataProvider.Unsubscribe. Error Details : {0} ", exp.ToString());
            }

        }
        /// <summary>
        /// will be used to resubscribe in items
        /// if dataAdapter reconnected to Notificaion provider
        /// </summary>
        void ReSubscribe()
        {
            try
            {
                foreach (string subscription in m_itmesSubsciptions.Keys)
                {

                    m_IDataFeedService.Subscribe(subscription, m_providerUserName);
                    m_NLog.Info("Item : {0} Resubscribed to Notificaion Provider.");
                }
            }
            catch(Exception exp)
            {
                m_NLog.Error("Error in ReSubscribe method . Error Details : {0}", exp.ToString());
            }

        }
        #endregion
    }
 
     #region Wcf Callback Handler
    public class IFeedCallbackHandler : IDataFeedCallback
    {

        BlockingCollection<FeedMessage> m_receivedFeedBCollecion;
        bool EnableLogger = false;
        LogsHandler m_logsHandler;

        public IFeedCallbackHandler(BlockingCollection<FeedMessage> receivedFeedBCollecion,bool enableLogger,LogsHandler logsHanlder)
        {
            m_receivedFeedBCollecion = receivedFeedBCollecion;
            EnableLogger = enableLogger;
            m_logsHandler = logsHanlder;
           
        }

        void IDataFeedCallback.SendDataFeed(FeedMessage[] dataFeedMsgList)
        {
            foreach (FeedMessage feedMsg in dataFeedMsgList)
            {
                 m_receivedFeedBCollecion.Add(feedMsg);

                if (EnableLogger)
                {
                    try
                    {
                        m_logsHandler.ReceivedFeedMessages.Add(feedMsg);
                    }
                    catch (Exception exp)
                    {
                        continue;
                    }
                }


            }
        }
    }
    #endregion
}
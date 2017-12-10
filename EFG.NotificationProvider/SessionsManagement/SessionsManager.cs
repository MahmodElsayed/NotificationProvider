using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using NLog;
using Contract;
using FeedMessagesSnapShot;
using System.Linq;
using System.Collections.Generic;
using EFG.OPS.NotificationEngineService.Contracts.Entities;
using MonitorStorage;

namespace SessionsManagement
{
    public class SessionsManager
    {
        #region Declerations

        private static Logger m_NLog = LogManager.GetLogger("AppLogger");
        private dsMonitor m_dsMonitor;
        private BlockingCollection<string> m_NotificationRequests = null; // to subscribe in notificaion
        private BlockingCollection<string> m_NotificationRemoveRequests = null; // to remove subscription in Notificaion
        private BlockingCollection<FeedMessage[]> m_FeedMessagesLog = null;



        ConcurrentDictionary<string, ClientSession> m_dictionaryClientSessions = null;
        public ConcurrentQueue<FeedMessage> UpdatesFeedMessagesQueue { get; set; }

        //  private SnapShotManager m_SnapShotManager = null;
        //  private ConfigCash m_configCash;

        Task taskHandleFeedMsgsUpdates = null;

      


        #endregion

        #region Constructor

        public SessionsManager(
             BlockingCollection<string> notificationRequests,
            BlockingCollection<string> notificationRemoveRequests,BlockingCollection<FeedMessage[]> feedMessagesLog,dsMonitor dsmonitor)
            
        {
            try
            {
                m_NotificationRequests = notificationRequests;

                m_NotificationRemoveRequests = notificationRemoveRequests;
                m_FeedMessagesLog = feedMessagesLog;

                m_dsMonitor = dsmonitor;

                m_dictionaryClientSessions = new ConcurrentDictionary<string, ClientSession>();
                this.UpdatesFeedMessagesQueue = new ConcurrentQueue<FeedMessage>();

               // m_SnapShotManager = snapshotMgr;

               // m_configCash = configcash;

               taskHandleFeedMsgsUpdates = Task.Factory.StartNew(() => HandleFeedMessagesUpdates());
            }
            catch (Exception exp)
            {
              
                m_NLog.Error("Error in SessionsManager Constructor Error Details : {0}", exp.ToString());
            }
        }

        #endregion

        #region Public Methods

        public void RegisterClient(ClientSession clientsession)
        {
            string username = string.Empty;
            try
            {
                username = clientsession.UserName;


                //1-Chcek if Client Already exist
                if (!m_dictionaryClientSessions.ContainsKey(clientsession.UserName))
                {
                    m_dictionaryClientSessions.TryAdd(clientsession.UserName, clientsession);
                }
                else
                {
                    ClientSession expiredClientSession;
                    m_dictionaryClientSessions.TryRemove(clientsession.UserName, out expiredClientSession);
                    expiredClientSession = null;
                    m_dictionaryClientSessions.TryAdd(clientsession.UserName, clientsession);
                }


                clientsession.FeedMessagesLog = m_FeedMessagesLog;
                clientsession.StartFeedHandlers();

                m_NLog.Info("Client: {0} registerd successfully and added to clients sessions list at {1}", clientsession.UserName, DateTime.Now.ToLongTimeString());


            }
            catch (Exception exp)
            {
                m_NLog.Error("Error while register client: {0} . Error Details: {1}", username, exp.ToString());
            }
        }
        public void LogOutClientSession(string username)
        {
            try
            {
                //1-Chcek if Client Already exist
                if (m_dictionaryClientSessions.ContainsKey(username))
                {
                    ClientSession expiredClientSession;
                    bool result = m_dictionaryClientSessions.TryRemove(username, out expiredClientSession);

                    if (result)
                    {
                        m_NLog.Info("Clinet : {0} Loged out at {0}", expiredClientSession.UserName, DateTime.Now.ToLongTimeString());
                        expiredClientSession = null;
                       
                    }

                }
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error while logout client: {0} . Error Details: {1}", username, exp.ToString());
            }
        }


        #endregion

        #region Private Methods
        private object HandleFeedMessagesUpdates()
        {
            while (true)
            {
                try
                {
                    //if (m_dictionaryClientSessions.Count < 1)
                    //    continue;
                    FeedMessage feedmsg;

                    if (UpdatesFeedMessagesQueue.Count == 0)
                        System.Threading.Thread.Sleep(100);
                    bool isFeedMsgvalid = UpdatesFeedMessagesQueue.TryDequeue(out feedmsg);
                    if (isFeedMsgvalid)
                    {
                        lock (m_dictionaryClientSessions)
                        {
                            foreach (ClientSession clientSession in m_dictionaryClientSessions.Values)
                            {
                                if (clientSession.FeedCallback != null)
                                {
                                    clientSession.UpdatesFeedMessagesQueue.Enqueue(feedmsg);

                                }

                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    m_NLog.Error("Error while update clients seessions with received feed.");
                    m_NLog.Error("Error Details : {0}", exp.ToString());
                    continue;
                }
            }
        }
        #endregion

        #region Public Properties

        #endregion

       

        public bool Subscribe(string SubscriptionCode, string UserName)
        {
            ClientSession clientSession = null;
            try
            {
                //check if client logge in or not;
                if (m_dictionaryClientSessions.ContainsKey(UserName) == false)
                {
                    // client not logged in
                    return false;
                }
                else
                {
                    clientSession = m_dictionaryClientSessions[UserName];

                    if (clientSession.UpdateSubscriptionList(SubscriptionCode))
                    {

                        m_NotificationRequests.Add(SubscriptionCode); // SubscriptionCode  will be sent to NotoficaionEngine to push snapshot and updates of SubscriptionCode

                        try
                        {
                            m_dsMonitor.Subscription_Items.AddSubscription_ItemsRow(SubscriptionCode, DateTime.Now);
                        }
                        catch(Exception exp)
                        {
                            m_NLog.Error("Error updating Monitor dataset . Error Details : {0} ", exp.ToString());
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in Subscribe method of client : {0} . Error Details {1}", UserName, exp.ToString());
                return false;
            }

          
        }

        public bool UnSubscribe(string SubscriptionCode, string UserName)
        {
            try
            {
                //check if client logge in or not;
                if (m_dictionaryClientSessions.ContainsKey(UserName) == false)
                {
                    // client not logged in
                    return false;
                }
                else
                {
                    ClientSession clientSession = m_dictionaryClientSessions[UserName];

                    if (clientSession.UnSubscribe(SubscriptionCode))
                    {
                        m_NotificationRemoveRequests.Add(SubscriptionCode); //// SubscriptionCode  will be sent to NotoficaionEngine to stop pushing  updates of that SubscriptionCode

                        try
                        {
                            dsMonitor.Subscription_ItemsRow dr = m_dsMonitor.Subscription_Items.FindByNotificationKey(SubscriptionCode);
                            if (dr != null)
                                m_dsMonitor.Subscription_Items.RemoveSubscription_ItemsRow(dr);
                        }
                        catch (Exception exp)
                        {
                            m_NLog.Error("Error updating Monitor dataset . Error Details : {0} ", exp.ToString());
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in UnSubscribe method of client : {0} . Error Details {1}", UserName, exp.ToString());
                return false;
            }
        }
    }
}
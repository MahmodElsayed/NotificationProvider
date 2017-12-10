
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using NLog;
using System.ServiceModel;
using Contract;

namespace SessionsManagement
{
    public class ClientSession
    {
        #region Declerations
        Task taskHandleFeedMsgsUpdates = null;

        private static Logger m_NLog = null;
      
        private Dictionary<string, string> m_dictioanrySubscirbedItems = null;//<subscribtionKey,subscribtionKey> // 

        private BlockingCollection<FeedMessage[]> m_FeedMessagesLog = null;

        private bool logMessages = false;

        #endregion

        #region Constructor
        public ClientSession()
        {
            try
            {
                m_NLog = LogManager.GetLogger("AppLogger");
            }
            catch(Exception exp)
            {
                Console.WriteLine("Error in initialize Nlog . Error Details : {0}", exp.ToString());
            }

        }
        #endregion

        #region Public Methods
        public void Initialize()
        {

            try
            {
                this.UpdatesFeedMessagesQueue = new ConcurrentQueue<FeedMessage>();
                m_dictioanrySubscirbedItems = new Dictionary<string, string>();

                logMessages = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["LogNotificationMsgs"]);

            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in ClientSession Constructor. Error Details {0}", exp.ToString());
            }

        }
     
        public void StartFeedHandlers()
        {
            try
            {

                taskHandleFeedMsgsUpdates = Task.Factory.StartNew(() => HandleFeedMessagesUpdates());

            }
            catch (Exception exp)
            {
                m_NLog.Error("Failed to start FeedHandlers threads for cleint: {0}. Error details : {1}", this.UserName, exp.ToString());
            }
        }

        public bool UpdateSubscriptionList(string SubscriptionCode)
        {
            try
            {
                if (m_dictioanrySubscirbedItems.ContainsKey(SubscriptionCode) == false)
                {
                    m_dictioanrySubscirbedItems.Add(SubscriptionCode, SubscriptionCode);
                }
                return true;
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in UpdateSubscriptionList method of client session : {0} . Error Details {1}", UserName, exp.ToString());
                return false;
            }
        }

       
        public bool UnSubscribe(string subscriptionCode)
        {
            try
            {
                
                if (m_dictioanrySubscirbedItems.ContainsKey(subscriptionCode))
                {
                    m_dictioanrySubscirbedItems.Remove(subscriptionCode);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in method UnSubscribe. Errors Details : {0} ", exp.ToString());
                return false;
            }
        }

     


        #endregion

        #region Private Methods



        private void HandleFeedMessagesUpdates()
        {
            while (true)
            {
                try
                {
 
                    FeedMessage feedmsg;
                    bool isfeedMsgvalid = UpdatesFeedMessagesQueue.TryDequeue(out feedmsg);
                    if (isfeedMsgvalid)
                    {
                        if (!m_dictioanrySubscirbedItems.ContainsKey(feedmsg.SubscriptionCode))// client is not subscirbed in this feedmessage.
                        {
                          
                            continue; // ignore feedMessage
                        }

                        else  // client subscribed in this FeedMessage.
                        {

                            SendMessage(new FeedMessage[] { feedmsg });
                        }
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(100);

                       

                       
                    }
                }
                catch (Exception exp)
                {
                    m_NLog.Error("Error while sending FeedMessage client: {0}", this.UserName);
                    m_NLog.Error("Error Details: {0}", exp.ToString());
                    continue;
                }
                finally
                {
                    System.Threading.Thread.Sleep(10);
                }
            }
        }


        private void SendMessage(FeedMessage[] feedMsgArr)
        {
            try
            {
                FeedCallback.SendDataFeed(feedMsgArr);

                try
                {
                    if (logMessages) // messages sent to lightStreamer DataAdapter should be logged.
                    {
                        FeedMessagesLog.Add(feedMsgArr);
                    }
                }
                catch(Exception exp)
                {
                    m_NLog.Error("Error lin log of  FeedMessages routine. Error Details: {0}", exp.ToString());
                }
               
            }

            catch (CommunicationObjectAbortedException abortException)
            {
                this.FeedCallback = null;
                m_NLog.Error("Error : {0}", abortException.Message);
                m_NLog.Error("Client : {0} disconnected .", this.UserName);
              
            }

            catch (Exception exp)
            {
                m_NLog.Error("Error while sending feed to client : {0} ", this.UserName);
                m_NLog.Error("Error Details : {0} ", exp.ToString());
            }
        }

  
        #endregion

        #region public properties

        public string UserName { get; set; }
        public IDataFeedCallback FeedCallback { get; set; }
        public ConcurrentQueue<FeedMessage> UpdatesFeedMessagesQueue { get; set; }

        public bool IsOnline { get; set; }

        public Dictionary<string, string> DictioanrySubscirbedItems
        {
            get
            {
                return m_dictioanrySubscirbedItems;
            }

            set
            {
                m_dictioanrySubscirbedItems = value;
            }
        }

        public BlockingCollection<FeedMessage[]> FeedMessagesLog
        {
            get
            {
                return m_FeedMessagesLog;
            }

            set
            {
                m_FeedMessagesLog = value;
            }
        }

        #endregion

    }
}
using Contract;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace MessagesLogger
{
    public class LogsHandler
    {
        #region Declerations
        private BlockingCollection<FeedMessage> m_ReceivedFeedMessages;
        private BlockingCollection<FeedMessage> m_SentFeedMessages;// to log sent messages to dataAdapter of light streamer.
        private BlockingCollection<string> m_SubscriptionInfo;
        private static Logger m_AppLogger;
        private static Logger m_ReceivedMessagesLogger;
        private static Logger m_OutgoingMessagesLogger;
        private static Logger m_SubscriptionInfoLogger;


        #endregion Declerations

        #region Constructors
        public LogsHandler()
        {
            try
            {
                m_AppLogger = LogManager.GetLogger("AppLogger");

                m_ReceivedMessagesLogger = LogManager.GetLogger("ReceivedMessagesLogger");
                m_OutgoingMessagesLogger = LogManager.GetLogger("OutgoingMessagesLogger");
                m_SubscriptionInfoLogger = LogManager.GetLogger("SubscriptionInfoLogger");

                ReceivedFeedMessages = new BlockingCollection<FeedMessage>();
                SentFeedMessages = new BlockingCollection<FeedMessage>();
                SubscriptionInfo = new BlockingCollection<string>();
                

            }
            catch (Exception exp)
            {
                m_AppLogger.Error("Error in LogsHandler Constructor. Error Details : {0} ", exp);
            }
        }
        #endregion Constructors

        #region Public Methods


        /// <summary>
        /// Log received messages from Notificaion Engine.
        /// </summary>
        public void StartLogging()
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    ReceivedMsgsLogProcessor();
                });

                Task.Factory.StartNew(() =>
                {
                    SentMessagesLogProcessor();
                });

                Task.Factory.StartNew(() =>
                {
                    SubscriptionLogProcessor();
                });
            }
            catch (Exception exp)
            {
                m_AppLogger.Error("Error in StrartLogReceivedNotificationsMessages method. Error Details : {0} ", exp);
            }
        }




        #endregion Public Methods

        #region Private Methods
        private void ReceivedMsgsLogProcessor()
        {
            foreach (FeedMessage msg in ReceivedFeedMessages.GetConsumingEnumerable())
            {
                try
                {
                    LogReceivedMessage(msg);
                }
                catch (Exception ex)
                {
                    m_AppLogger.Error("Error in ReceivedMsgsLogProcessor method. Error Details : {0}", ex.ToString());
                }

            }
        }


        private void SentMessagesLogProcessor()
        {
            foreach (FeedMessage msg in SentFeedMessages.GetConsumingEnumerable())
            {
                try
                {
                    LogSentMessage(msg);
                }
                catch (Exception ex)
                {
                    m_AppLogger.Error("Error in SentMessagesLogProcessor method. Error Details : {0}", ex.ToString());
                }

            }
        }

        private void SubscriptionLogProcessor()
        {
            foreach (string subsriptionInfo in SubscriptionInfo.GetConsumingEnumerable())
            {
                try
                {
                    LogSubscriptionInfo(subsriptionInfo);
                }
                catch (Exception ex)
                {
                    m_AppLogger.Error("Error in SentMessagesLogProcessor method. Error Details : {0}", ex.ToString());
                }

            }
        }

        private void LogReceivedMessage(FeedMessage feedmsg)
        {
            try
            {
                string msg = GenarateStringMsg(feedmsg);
                m_ReceivedMessagesLogger.Info(msg);

              
            }
            catch (Exception exp)
            {
                m_AppLogger.Error("Error in method LogReceivedMessage. Error Details: {0}", exp);
            }
        }

        private void LogSentMessage(FeedMessage feedmsg)
        {
            try
            {
                string msg = GenarateStringMsg(feedmsg);
                m_OutgoingMessagesLogger.Info(msg);


            }
            catch (Exception exp)
            {
                m_AppLogger.Error("Error in method LogReceivedMessage. Error Details: {0}", exp);
            }
        }

        private void LogSubscriptionInfo(string subscirptionInfo)
        {
            try
            {
               
                m_SubscriptionInfoLogger.Info(subscirptionInfo);

            }
            catch (Exception exp)
            {
                m_AppLogger.Error("Error in method LogSentMessages. Error Details: {0}", exp);
            }
        }

        private string GenarateStringMsg(FeedMessage feedMsg)
        {
            string msg = string.Empty;

            msg = string.Format("SubscriptionKey={0},", feedMsg.SubscriptionCode);

            foreach (string field in feedMsg.MessageFields.Keys)
            {
                msg = msg + string.Format("{0}={1},", field, feedMsg.MessageFields[field]);
            }
            int indexOflstSpliter = msg.LastIndexOf(',');
            return msg.Remove(indexOflstSpliter);

        }




        #endregion Private Methods

        #region Public Members
        public BlockingCollection<FeedMessage> SentFeedMessages
        {
            get
            {
                return m_SentFeedMessages;
            }

            set
            {
                m_SentFeedMessages = value;
            }
        }

        public BlockingCollection<FeedMessage> ReceivedFeedMessages
        {
            get
            {
                return m_ReceivedFeedMessages;
            }

            set
            {
                m_ReceivedFeedMessages = value;
            }
        }

        public BlockingCollection<string> SubscriptionInfo
        {
            get
            {
                return m_SubscriptionInfo;
            }

            set
            {
                m_SubscriptionInfo = value;
            }
        }
        #endregion
    }
}

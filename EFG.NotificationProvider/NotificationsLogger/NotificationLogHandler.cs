using Contract;
using EFG.OPS.NotificationEngineService.Contracts.Entities;
using MonitorStorage;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationsLogger
{

    /// <summary>
    /// Log received notificaions
    /// from Notifications Engine.
    /// Log outgoing messaget to LightStreamer DataAdapter
    /// </summary>
    public class NotificationLogHandler
    {
        #region Declerations
        private BlockingCollection<NotificationMessage> m_NotificationsMessages;
        private BlockingCollection<FeedMessage[]> m_FeedMessages;// to log sent messages to dataAdapter of light streamer.
        private static Logger m_AppLogger;
        private static Logger m_ReceivedNotificationLogger;
        private static Logger m_OutgoingMessagesLogger;
        private dsMonitor m_dsMonitor = null;
        #endregion Declerations

        #region Constructors
        public NotificationLogHandler(BlockingCollection<NotificationMessage> notificationsMessages, BlockingCollection<FeedMessage[]> feedMessages, dsMonitor dsmonitor)
        {
            try
            {
                m_AppLogger = LogManager.GetLogger("AppLogger");
                m_ReceivedNotificationLogger = LogManager.GetLogger("NotificaionsLogger");
                m_OutgoingMessagesLogger = LogManager.GetLogger("OutgoingMessagesLogger");
                m_NotificationsMessages = notificationsMessages;
                m_FeedMessages = feedMessages;
                m_dsMonitor = dsmonitor;

            }
            catch (Exception exp)
            {
                Console.WriteLine("Error in NotificationLogHandler Constructor. Error Details : {0} ", exp);
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
                    NotificationLogProcessor();
                });

                Task.Factory.StartNew(() =>
                {
                    SentMessagesLogProcessor();
                });
            }
            catch (Exception exp)
            {
                m_AppLogger.Error("Error in StrartLogReceivedNotificationsMessages method. Error Details : {0} ", exp);
            }
        }

       


        #endregion Public Methods

        #region Private Methods
        private void NotificationLogProcessor()
        {
            foreach (NotificationMessage msg in m_NotificationsMessages.GetConsumingEnumerable())
            {
                try
                {
                    LogReceivedMessage(msg);
                }
                catch (Exception ex)
                {
                    m_AppLogger.Error("Error in NotificationLogProcessor method. Error Details : {0}", ex.ToString());
                }

            }
        }


        private void SentMessagesLogProcessor()
        {
            foreach (FeedMessage[] msgList in m_FeedMessages.GetConsumingEnumerable())
            {
                try
                {
                    foreach (FeedMessage msg in msgList)
                    {

                        LogSentMessages(msg);
                    }
                }
                catch (Exception ex)
                {
                    m_AppLogger.Error("Error in SentMessagesLogProcessor method. Error Details : {0}", ex.ToString());
                }

            }
        }

        private void LogReceivedMessage(NotificationMessage notificationMsg)
        {
            try
            {
                string msg = GenarateStringMsg(notificationMsg);
                m_ReceivedNotificationLogger.Info(msg);

                try
                {
                    m_dsMonitor.ReceivedNotificaions.AddReceivedNotificaionsRow(notificationMsg.NotificationKey, msg, DateTime.Now);
                }
                catch(Exception exp)
                {
                    m_AppLogger.Error("Error in update Monitor dataset. Error Details : {0}", exp.ToString());
                }


            }
            catch (Exception exp)
            {
                m_AppLogger.Error("Error in method LogReceivedMessage. Error Details: {0}", exp);
            }
        }

        private void LogSentMessages(FeedMessage feedMsg)
        {
            try
            {
                string msg = GenarateStringMsg(feedMsg);
                m_OutgoingMessagesLogger.Info(msg);

                try
                {
                    m_dsMonitor.SentNotificaions.AddSentNotificaionsRow(feedMsg.SubscriptionCode, msg, DateTime.Now);
                }
                catch (Exception exp)
                {
                    m_AppLogger.Error("Error in update Monitor dataset. Error Details : {0}", exp.ToString());
                }


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
          return  msg.Remove(indexOflstSpliter);
            
        }

        private string GenarateStringMsg(NotificationMessage notificationMsg)
        {
            string msg = string.Empty;

            msg = string.Format("SubscriptionKey={0},", notificationMsg.NotificationKey);

            foreach(string field in notificationMsg.BodyDictionary.Keys)
            {
                msg = msg + string.Format("{0}={1},", field, notificationMsg.BodyDictionary[field]);
            }

            int indexOflstSpliter = msg.LastIndexOf(',');
            return msg.Remove(indexOflstSpliter);
        }


        #endregion Private Methods

    }
}

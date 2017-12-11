using Contract;
using EFG.OPS.NotificationEngineService.Contracts.Entities;
using FeedProcessorConfigurations;
using MessageRouter;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedMessagesProcessror
{
   
    public class FeedProcessor
    {

        #region private members
        BlockingCollection<NotificationMessage> m_NotificationMessages = null;
        private Router m_msgRouter;
        private long SequenceNumber = 0; // will be used to set sequence numbres of feedMessages
        private FeedProcessorConfigStorage m_FeedProcessorConfigStorage = null;
        private static Logger m_NLog = LogManager.GetLogger("AppLogger");
        #endregion

        #region Construcot
        public FeedProcessor(BlockingCollection<NotificationMessage> notificationMessages, Router router)
        {
            try
            {
              
                m_NotificationMessages = notificationMessages;
                m_msgRouter = router;

                m_FeedProcessorConfigStorage = new FeedProcessorConfigStorage();
                m_FeedProcessorConfigStorage.Initialize();
            }
            catch(Exception exp)
            {
                m_NLog.Error("Error in FeedProcessor constructor. Error Details : ", exp.ToString());
            }


        }

        #endregion


        #region private methods

        private void StartProcessNotificaions()
        {
            // lock (objLocker)
            {
                foreach (NotificationMessage notificaionMsg in m_NotificationMessages.GetConsumingEnumerable())
                {
                    try
                    {
                        ProcessNotificaionFeed(notificaionMsg);
                    }
                    catch (Exception ex)
                    {
                        m_NLog.Error("Error in StartProcessNotificaions() . error detaisl : {0}", ex.ToString());
                    }
                }
            }
        }

        private void ProcessNotificaionFeed(NotificationMessage notificationMsg)
        {
            try
            {
                FeedMessage feedMsg = new FeedMessage();
                feedMsg.SubscriptionCode = notificationMsg.NotificationKey;

                string messageType = GetMessageType(feedMsg.SubscriptionCode);

                feedMsg.MessageAction = notificationMsg.MessageAction.ToString();
                 feedMsg.MessageFields = new Dictionary<string, string>();
              
               
                 SequenceNumber = SequenceNumber + 1;
                 feedMsg.SequenceNumber = SequenceNumber;


               // string notificaionType = notificationMsg.NotificationKey;// will be replaced with Notificaion type.

                MessageAttributes msgAttributes = m_FeedProcessorConfigStorage.MessagesAttributesInfo[messageType];

               foreach(MessageTag tag in  msgAttributes.MessageTagsList)
                {
                    if(notificationMsg.BodyDictionary.ContainsKey(tag.SourceFeild))
                    {
                        string fieldName = tag.ColumnName;
                        string value = notificationMsg.BodyDictionary[tag.SourceFeild];
                        feedMsg.MessageFields.Add(fieldName, value);
                    }
                }
                           
                m_msgRouter.Route(feedMsg);

            }
            catch (Exception ex)
            {
                m_NLog.Error("Error in ProcessNotificaionFeed() .Error Details : {0}", ex.ToString());
            }
        }

        private string GetMessageType(string subscriptionCode)
        {
            string messageType = string.Empty;
            try
            {
                 int indexOfTypeSpliter = subscriptionCode.IndexOf('#');
                 messageType = subscriptionCode.Remove(indexOfTypeSpliter);
            }
            catch(Exception exp)
            {
                m_NLog.Error("Error in method GetMessageType. Error Details: {0}. ", exp.ToString());
            }
            return messageType;
        }

        #endregion

        #region public method

        public void StartHanldeReceivedFeed()
        {
            try
            {
               //Run Consumer
                Task.Factory.StartNew(() =>
                {
                    StartProcessNotificaions();
                });
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in StartHanldeReceivedFeed() . Error details : {0}", exp.ToString());
            }
        }
        #endregion


    }
}
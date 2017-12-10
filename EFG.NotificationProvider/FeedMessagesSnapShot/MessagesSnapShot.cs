
using Contract;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedMessagesSnapShot
{
    public class MessagesSnapShot
    {
        #region Declerations
        private static Logger m_NLog = LogManager.GetLogger("AppLogger");
        #endregion

        #region Constructor
        public MessagesSnapShot()
        {
           
            dictioanryFeedMessage = new Dictionary<string, List<FeedMessage>>();  // < "SubscribtionCode = mpEGS045454",List<FeedMessages> >();
        }
        #endregion

        #region Public Methods
  

        public void AddFeedMessage(FeedMessage feedMessage)
        {
            try
            {
                if (dictioanryFeedMessage.ContainsKey(feedMessage.SubscriptionCode))
                {
                    List<FeedMessage> lstFeedMessage = dictioanryFeedMessage[feedMessage.SubscriptionCode];

                    if (feedMessage.MessageAction == FeedMessageAction.Add.ToString())
                    {
                        lstFeedMessage.Add(feedMessage);  // like in Trades and News Feed .
                    }

                    if (feedMessage.MessageAction == FeedMessageAction.Update.ToString())
                    {
                        // in update case only one message in the list will be available.
                        FeedMessage storedFeedMsg = lstFeedMessage.First<FeedMessage>();
                        storedFeedMsg.SequenceNumber = feedMessage.SequenceNumber;

                        foreach (string colmunName in feedMessage.MessageFields.Keys)
                        {
                            if (storedFeedMsg.MessageFields.ContainsKey(colmunName))
                            {
                                string newValue;
                                // get new value.
                                bool result = feedMessage.MessageFields.TryGetValue(colmunName, out newValue);
                                // set the new value in the stored feedMessage.
                                if (result)
                                    storedFeedMsg.MessageFields[colmunName] = newValue;
                            }
                            else // add new columns to the origianl message 
                            {
                                string newValue;
                                // get new value.
                                bool result = feedMessage.MessageFields.TryGetValue(colmunName, out newValue);
                                if (result)
                                    storedFeedMsg.MessageFields.Add(colmunName, newValue);
                            }
                        }
                    }

                    if (feedMessage.MessageAction == FeedMessageAction.Delete.ToString())
                    {
                        //remove message from snapshot
                    }
                }
                else
                {
                    List<FeedMessage> lstFeedMsgs = new List<FeedMessage>();
                    lstFeedMsgs.Add(feedMessage);
                    dictioanryFeedMessage.Add(feedMessage.SubscriptionCode, lstFeedMsgs);
                }
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error while handling FeedMessage in MessageSnaphot");
                m_NLog.Error("Error Details : {0}", exp.ToString());
            }
        }
        #endregion

        #region Public Properties
        public Dictionary<string, List<FeedMessage>> dictioanryFeedMessage { get; set; }// Dictionary<"mpEGS212022", List<FeedMessage>>
        #endregion

    }
}


using Contract;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedMessagesSnapShot
{
    public class SnapShotManager
    {
        #region Declerations
        private Object lockObj = new object();
        private Task taskHandleFeedMsgsSnapshot = null;
        private Dictionary<string, MessagesSnapShot> m_DictionarySnapShots;  // will hold snapshots for each message type.ex <"Trades",MessagesSnapshot>();
        private static Logger m_NLog = LogManager.GetLogger("AppLogger");
        #endregion

        #region Constructor
        public SnapShotManager()
        {
            this.SnapShotFeedMessagesQueue = new ConcurrentQueue<FeedMessage>();
            m_DictionarySnapShots = new Dictionary<string, MessagesSnapShot>();

         
        }
        #endregion

        #region Public Methods


        public void StartSnapshotManager()
        {
            m_NLog.Info("Starting Snapshot Manager....");
            taskHandleFeedMsgsSnapshot = Task.Factory.StartNew(() => StoreFeedMessagesToSnapShot());
        }


        public void EnqueFeedMessage(FeedMessage feedMsg)
        {

            this.SnapShotFeedMessagesQueue.Enqueue(feedMsg);
         //   m_NLog.Info("SnapshotManager received FeedMessage of sequence number: {0} MessageType : {1} and SymbolCode: {2}", feedMsg.SequenceNumber, feedMsg.MessageType, feedMsg.SubscribtionCode);
        }

        public List<FeedMessage> GetSnapShot(string messageType, string SubscriptionCode, out List<long> snapshotSequenceNumbers)
        {
            lock (m_DictionarySnapShots)
            {

                snapshotSequenceNumbers = new List<long>();

                List<FeedMessage> snapShotList = new List<FeedMessage>();
                List<FeedMessage> SortedSnapshotList = null;
                try
                {
                    lock (m_DictionarySnapShots)
                    {
                        if (m_DictionarySnapShots.ContainsKey(messageType) == false)
                            return new List<FeedMessage>(); // count will be zero.

                        MessagesSnapShot msgSnapshot = m_DictionarySnapShots[messageType];

                        List<FeedMessage> symbolfeedMsgList;
                        bool result = msgSnapshot.dictioanryFeedMessage.TryGetValue(SubscriptionCode, out symbolfeedMsgList);
                        if (result)
                        {
                            foreach (FeedMessage feedMsg in symbolfeedMsgList)
                            {
                                snapShotList.Add(feedMsg);
                                snapshotSequenceNumbers.Add(feedMsg.SequenceNumber); // store snapshot sequecenumber to avoid sending the feedmessage more than once in the ClientSession. 
                            }
                        }

                    }

                    SortedSnapshotList = snapShotList.OrderBy(feedmsg => feedmsg.SequenceNumber).ToList();

                    if (SortedSnapshotList == null)
                        return new List<FeedMessage>();

                    if (SortedSnapshotList.Count <= 0)
                    {
                        return new List<FeedMessage>();  // count will be zero.
                    }
                    else
                    {
                        m_NLog.Info("Snapshot prepared successfully. MessageType : {0} , SubscribtionKey : {1} and number of FeedMessages in the snapshot : {2}", messageType, SubscriptionCode, SortedSnapshotList.Count);
                        return SortedSnapshotList;
                    }
                }
                catch (Exception exp)
                {
                    m_NLog.Error("Error in GetSnapShot method.");
                    m_NLog.Error(exp.ToString());
                    return new List<FeedMessage>();

                }
            }
        }
        #endregion

        #region Private Methods
        private void StoreFeedMessagesToSnapShot()
        {
            try
            {
                while (true)
                {

                    if (SnapShotFeedMessagesQueue.Count == 0)
                        System.Threading.Thread.Sleep(500);

                    FeedMessage feedMsg;
                    bool isSnaphotMsgvalid = SnapShotFeedMessagesQueue.TryDequeue(out feedMsg);
                    if (isSnaphotMsgvalid)
                    {
                        if (m_DictionarySnapShots.ContainsKey(feedMsg.MessageType))
                        {
                            MessagesSnapShot msgSnapshot = m_DictionarySnapShots[feedMsg.MessageType];
                            msgSnapshot.AddFeedMessage(feedMsg);
                        }

                        else
                        {
                            MessagesSnapShot msgsSnapshot = new MessagesSnapShot();
                            m_DictionarySnapShots.Add(feedMsg.MessageType, msgsSnapshot);
                            msgsSnapshot.AddFeedMessage(feedMsg);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in StoreFeedMessagesToSnapShot method.");
                m_NLog.Error(exp.ToString());
            }
        }

        #endregion

        #region Public Properties
        public ConcurrentQueue<FeedMessage> SnapShotFeedMessagesQueue { get; set; }
        #endregion

     
    }
}
using Contract;
using FeedMessagesSnapShot;
using NLog;
using SessionsManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageRouter
{
    public class Router
    {
         private SessionsManager m_SessionManager;
        private SnapShotManager m_SnapShotManager;
        private static Logger m_NLog = LogManager.GetLogger("AppLogger");

        public Router(SessionsManager sessionManager)
        {

            m_SessionManager = sessionManager;
           
        }

        public Router(SnapShotManager snapshotMgr,SessionsManager sessionManager)
        {
         
            m_SessionManager = sessionManager;
            m_SnapShotManager = snapshotMgr;
        }

        public void Route(FeedMessage feedMessage)
        {
            try
            {
                m_SessionManager.UpdatesFeedMessagesQueue.Enqueue(feedMessage);
              //  m_SnapShotManager.EnqueFeedMessage(feedMessage);
                
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error while routing feedMessage. Error Details : {0}", exp.ToString());
            }
        }
    }
}

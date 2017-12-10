using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Contract;
using SessionsManagement;

namespace FeedServiceWCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class FeedService : IDataFeed
    {
        #region Declerations
        private SessionsManager m_SessionManager = null;
        private static Logger m_NLog = LogManager.GetLogger("AppLogger");
        private static Logger m_NLog_SubscriptionInfo = LogManager.GetLogger("SubscriptionInfoLogger");

        #endregion

        #region Constructor
        public FeedService(SessionsManager sessionManager)
        {

               m_SessionManager = sessionManager;
           
        }
        #endregion

        #region Private methods
        private void RegisterClientSession(ClientSession clientsession)
        {
            m_SessionManager.RegisterClient(clientsession);
        }
        private LoginResponse AuthenticateClient(LoginRequest loginRequest)
        {
            LoginResponse lresponse = null;
            try
            {
                bool loginResult = true; // should be handled throw database or xml file to return login result. 
                lresponse = new LoginResponse();

                if (loginResult)
                    lresponse.ServiceLoginStatus = LoginStatus.Succeeded;
                else
                    lresponse.ServiceLoginStatus = LoginStatus.Failed;
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error authenticate client : {0} . Error Details {1}", loginRequest.UserName, exp.ToString());
            }
            return lresponse;
        }
        #endregion

        #region IDataFeed Interface members
        LoginResponse IDataFeed.Login(LoginRequest loginRequest)
        {
            LoginResponse lResponse = null;
            string username = string.Empty;
            try
            {
                username = loginRequest.UserName;


                IDataFeedCallback  FeedCallback = OperationContext.Current.GetCallbackChannel<IDataFeedCallback>();
              
                lResponse = AuthenticateClient(loginRequest);

                if (lResponse.ServiceLoginStatus == LoginStatus.Succeeded)
                {
                 
                    m_NLog.Info("Client : {0} loggedin succeessfully.", username);
                    ClientSession clientsession = new ClientSession();
                    clientsession.UserName = loginRequest.UserName.Trim();
                    clientsession.FeedCallback = FeedCallback;
                    clientsession.IsOnline = true;
                    clientsession.Initialize();
                    m_NLog.Info("Starting Client: {0} registration process.", username);
                  
                    RegisterClientSession(clientsession);
                }
                else
                {
                    m_NLog.Warn("Client : {0} login failed with stauts : {1} ", lResponse.ServiceLoginStatus + " " + lResponse.Message);
                }
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in Client {0} login process. Error Details {1}", username, exp.ToString());
            }
            return lResponse;
        }
        void IDataFeed.Subscribe(string SubscriptionCode, string UserName)
        {
            try
            {
                if (m_SessionManager.Subscribe(SubscriptionCode, UserName))
                    m_NLog_SubscriptionInfo.Info("DataAdapter : {0} Subscribed to Item {1} at {2}",UserName,SubscriptionCode,DateTime.Now.ToLongTimeString());
                else
                    m_NLog_SubscriptionInfo.Warn("DataAdapter : {0} Couldn't apply Subscribe to Item {1} at {2}", UserName, SubscriptionCode, DateTime.Now.ToLongTimeString());

            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in Subscribe method . client {0} . Error Details {1}", UserName, exp.ToString());

            }
        }

        void IDataFeed.UnSubscribe(string SubscriptionCode, string UserName)
        {
            try
            {
               if(m_SessionManager.UnSubscribe(SubscriptionCode, UserName))
                m_NLog_SubscriptionInfo.Info("DataAdapter : {0} Unsubscribed from Item {1} at {2}", UserName, SubscriptionCode, DateTime.Now.ToLongTimeString());
               else
                    m_NLog_SubscriptionInfo.Warn("DataAdapter : {0} Couldn't apply Unsubscribe from Item {1} at {2}", UserName, SubscriptionCode, DateTime.Now.ToLongTimeString());
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in Subscribe method . client {0} . Error Details {1}", UserName, exp.ToString());

            }
        }
        void IDataFeed.LogOut(string UserName)
        {
            try
            {

                m_SessionManager.LogOutClientSession(UserName);
            }
            catch (Exception exp)
            {
                m_NLog.Error("Error in logout client : {0} . Error Details {1}", UserName, exp.ToString());
            }
        }

        public bool Ping()
        {
            // update this method to Check UserName before return true.
            return true;
        }
        #endregion
    }
}
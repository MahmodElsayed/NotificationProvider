using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    //test git
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IDataFeedCallback))]
    public interface IDataFeed
    {
        [OperationContract(IsOneWay = false)]
        LoginResponse Login(LoginRequest loginRequest);

        [OperationContract(IsOneWay = true)]
        void Subscribe(string SubscriptionCode, string UserName);

        [OperationContract(IsOneWay = true)]
        void UnSubscribe(string SubscriptionCode, string UserName);

        [OperationContract(IsOneWay = true)]
        void LogOut(string UserName);

        [OperationContract(IsOneWay = false)]
        bool Ping();
    }

    public interface IDataFeedCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendDataFeed(FeedMessage[] dataFeedMsgList);
    }
}
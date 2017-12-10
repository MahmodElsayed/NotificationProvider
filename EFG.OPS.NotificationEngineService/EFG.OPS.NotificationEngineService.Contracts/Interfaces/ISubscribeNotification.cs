using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using EFG.OPS.NotificationEngineService.Contracts.Interfaces;

namespace EFG.OPS.NotificationEngineService.Contracts.Interfaces
{
    [ServiceContract(CallbackContract = typeof(IPublishNotification))]
    public interface ISubscribeNotification
    {
        [OperationContract]
        bool Subscribe();

        [OperationContract]
        void GetNotifications(string notificationKey);

        [OperationContract]
        void UnsubscribeNotificaions(string notificationKey);
                
        [OperationContract]
        bool Ping();

       
    }
}

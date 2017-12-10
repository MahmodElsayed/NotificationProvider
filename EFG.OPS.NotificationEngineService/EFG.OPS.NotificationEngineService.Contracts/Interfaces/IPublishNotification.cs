using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using EFG.OPS.NotificationEngineService.Contracts.Entities;

namespace EFG.OPS.NotificationEngineService.Contracts.Interfaces
{
    [ServiceContract]
    public interface IPublishNotification
    {
        [OperationContract(IsOneWay = true)]
        void PublishNotification(NotificationMessage notificationMessage);

        [OperationContract(IsOneWay = true)]
        void PublishNotifications(NotificationMessage[] notificationMessages);
    }
}

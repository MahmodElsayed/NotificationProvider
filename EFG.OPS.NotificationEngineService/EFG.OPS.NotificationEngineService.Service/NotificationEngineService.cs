using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using EFG.Brokerage.General.Notification;
using EFG.OPS.NotificationEngineService.Business;
using EFG.OPS.NotificationEngineService.Contracts.Entities;
using EFG.OPS.NotificationEngineService.Contracts.Enums;
using EFG.OPS.NotificationEngineService.Contracts.Interfaces;
using EFG.OPS.NotificationEngineService.Data;

namespace EFG.OPS.NotificationEngineService.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class NotificationEngineService: IPublishNotification, ISubscribeNotification
    {
        public void PublishNotification(NotificationMessage notificationMessage)
        {
            List<IPublishNotification> subscribers = bzSubscributionManager.GetSubscribers();
            if (subscribers == null) return;

            Type type = typeof(IPublishNotification);
            MethodInfo publishMethodInfo = type.GetMethod("PublishNotification");

            foreach (IPublishNotification subscriber in subscribers)
            {
                try
                {
                    notificationMessage.NotificationKey = "Notify_User#1248";
                    publishMethodInfo.Invoke(subscriber, new object[] { notificationMessage });
                }
                catch
                {

                }

            }
        }

        public void PublishNotifications(NotificationMessage[] notificationMessages)
        {
            List<IPublishNotification> subscribers = bzSubscributionManager.GetSubscribers();
            if (subscribers == null) return;

            Type type = typeof(IPublishNotification);
            MethodInfo publishMethodInfo = type.GetMethod("PublishNotifications");

            foreach (IPublishNotification subscriber in subscribers)
            {
                try
                {
                    publishMethodInfo.Invoke(subscriber, new object[] { notificationMessages });
                }
                catch
                {

                }

            }
        }

        public bool Subscribe()
        {
            IPublishNotification subscriber = OperationContext.Current.GetCallbackChannel<IPublishNotification>();
            bzSubscributionManager.AddSubscriber(subscriber);
            return true;

        }

        public void GetNotifications(string notificationKey)
        {
            tdsEventNotification oTdsEventNotification = new tdsEventNotification();
            dmSendEventNotification dmSendEventNotification = new dmSendEventNotification();
            //dmSendEventNotification.GetUserIFANotifications(oTdsEventNotification, notificationKey);
            dmSendEventNotification.GetDumyUserIFANotifications(oTdsEventNotification, notificationKey);

            Dictionary<int, List<tdsEventNotification.UserIFANotificationsRow>> notificationMessages = new Dictionary<int, List<tdsEventNotification.UserIFANotificationsRow>>();

            foreach (var userIfaNotificationsRow in oTdsEventNotification.UserIFANotifications)
            {
                if (notificationMessages.ContainsKey(userIfaNotificationsRow.FirstParentEventmessageID))
                {
                    notificationMessages[userIfaNotificationsRow.FirstParentEventmessageID].Add(userIfaNotificationsRow);
                }
                else
                {
                    List<tdsEventNotification.UserIFANotificationsRow> parentList = new List<tdsEventNotification.UserIFANotificationsRow>();
                    parentList.Add(userIfaNotificationsRow);
                    notificationMessages.Add(userIfaNotificationsRow.FirstParentEventmessageID, parentList);
                }
            }

            var notificationsArray =
                notificationMessages.Select(
                    userMessage => userMessage.Value.OrderByDescending(c => c.EventIFANotificationID).FirstOrDefault())
                    .ToArray();

            List<NotificationMessage> notificationMessageList = new List<NotificationMessage>();

            foreach (var notification in notificationsArray)
            {
                Dictionary<string, string> bodyDictionary = notification.Table.Columns
                  .Cast<DataColumn>()
                  .ToDictionary(c => c.ColumnName, c => notification[c].ToString());

                notificationMessageList.Add(new NotificationMessage() {BodyDictionary = bodyDictionary ,MessageAction = MessageAction.Insert,NotificationKey=notificationKey});
            }
            
            new Task(() => { PublishNotifications(notificationMessageList.ToArray()); }).Start();
        }

        public void UnsubscribeNotificaions(string notificationKey)
        {

        }
        public bool Ping()
        {
            return true;
        }
    }
}

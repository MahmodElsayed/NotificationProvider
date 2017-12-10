using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using EFG.OPS.NotificationEngineService.Data;

namespace EFG.Brokerage.General.Notification
{
    public partial class dmSendEventNotification : Component
    {
        #region PrivateMembers

        #endregion PrivateMembers
        public dmSendEventNotification()
        {
            InitializeComponent();
        }

        public dmSendEventNotification(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void GetUserIFANotifications(tdsEventNotification o_tdsEventNotification, string EventIFASubscriberNotificationAddress)
        {
            try
            {
                using (daGetUserIFANotifications.SelectCommand.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EventsDB"].ConnectionString))
                {
                    daGetUserIFANotifications.SelectCommand.Parameters["@EventIFASubscriberNotificationAddress"].Value = EventIFASubscriberNotificationAddress;

                    daGetUserIFANotifications.Fill(o_tdsEventNotification.UserIFANotifications);
                }
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }

        public void GetDumyUserIFANotifications(tdsEventNotification o_tdsEventNotification, string EventIFASubscriberNotificationAddress)
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    var x = o_tdsEventNotification.UserIFANotifications.NewUserIFANotificationsRow();
                    x.EventIFANotificationBody = "Test" + i;
                    x.EventIFANotificationID = i;
                    x.EventIFANotificationTitle = "Test" + i;
                    x.EventIFASubscriberID = i;
                    x.EventIFASubscriberNotificationAddress = "Notify_User#1248";
                    x.EventMessageID = i;
                    x.EventMessageStatus = i;
                    x.ExpiryDate = DateTime.Now;
                    x.FirstParentEventmessageID = i;
                    x.ParentEventMessageID = i;
                    x._EventMessagesStatusID = i;
                    x.EventIFANotificationID = i;
                    o_tdsEventNotification.UserIFANotifications.AddUserIFANotificationsRow(x);
                }
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }
    }
}

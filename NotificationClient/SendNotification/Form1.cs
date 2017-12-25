using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EFG.OPS.NotificationEngineService.Contracts.Entities;
using EFG.OPS.NotificationEngineService.Contracts.Enums;

namespace SendNotification
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var factory = new System.ServiceModel.ChannelFactory<EFG.OPS.NotificationEngineService.Contracts.Interfaces.IPublishNotification>("IPublishNotificationEndPoint"))
            {
                var proxy = factory.CreateChannel();

                Dictionary<string, string> bodyDictionary = new Dictionary<string, string>();
                bodyDictionary.Add("EventIFANotificationID", txtCode.Text);
                bodyDictionary.Add("EventMessageID", "1");
                bodyDictionary.Add("EventIFANotificationBody", "Test");
                bodyDictionary.Add("EventIFANotificationTitle", "New TITLE" + DateTime.Now.TimeOfDay.ToString());
                bodyDictionary.Add("EventIFASubscriberID", "1");
                bodyDictionary.Add("EventIFASubscriberNotificationAddress", "Notify_User#1248");
                bodyDictionary.Add("_EventMessagesStatusID", "1");
                bodyDictionary.Add("ParentEventMessageID", "1");
                bodyDictionary.Add("ExpiryDate", DateTime.Now.ToString());
                bodyDictionary.Add("EventMessageStatus", "1");
                bodyDictionary.Add("FirstParentEventmessageID", "1");




                ////////////////////

                NotificationMessage notificaionMsg = new NotificationMessage();
                notificaionMsg.BodyDictionary = bodyDictionary;
                
                switch(comboBox1.Text)
                {
                    case "Add":
                        notificaionMsg.MessageAction = MessageAction.Insert;
                        break;

                    case "Update":
                        notificaionMsg.MessageAction = MessageAction.Update;
                        break;

                    case "Delete":
                        notificaionMsg.MessageAction = MessageAction.Delete;
                        break;


                }
              

                proxy.PublishNotification(notificaionMsg,textBox1.Text.Trim());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}

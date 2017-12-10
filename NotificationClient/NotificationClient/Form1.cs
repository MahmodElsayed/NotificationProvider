using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;
using EFG.OPS.NotificationEngineService.Contracts.Entities;
using EFG.OPS.NotificationEngineService.Contracts.Interfaces;

namespace NotificationClient
{
    public partial class Form1 : Form, IPublishNotification
    {
        private ISubscribeNotification proxy;
        static int _eventCount;
        public Form1()
        {
            InitializeComponent();
            MakeProxy();
            _eventCount = 0;
        }

        public void MakeProxy()
        {
            DuplexChannelFactory<ISubscribeNotification> factory = new DuplexChannelFactory<EFG.OPS.NotificationEngineService.Contracts.Interfaces.ISubscribeNotification>(new InstanceContext(this), "ISubscribeNotificationEndPoint");
            proxy = factory.CreateChannel();
        }

        public void PublishNotification(NotificationMessage notificationMessage)
        {
            if (notificationMessage != null)
            {
                int itemNum = (lstEvents.Items.Count < 1) ? 0 : lstEvents.Items.Count;
                lstEvents.Items.Add(itemNum.ToString());
                string text = "";
                foreach (var keyValue in notificationMessage.BodyDictionary)
                {
                    text += keyValue.Key + "=>" + keyValue.Value + ";";
                }
                lstEvents.Items[itemNum].SubItems.AddRange(new string[] { text, notificationMessage.MessageAction.ToString() });
                _eventCount += 1;

            }
        }

        public void PublishNotifications(NotificationMessage[] notificationMessages)
        {
            foreach (var notificationMessage in notificationMessages)
            {
                if (notificationMessage != null)
                {
                    int itemNum = (lstEvents.Items.Count < 1) ? 0 : lstEvents.Items.Count;
                    lstEvents.Items.Add(itemNum.ToString());
                    string text = "";
                    foreach (var keyValue in notificationMessage.BodyDictionary)
                    {
                        text += keyValue.Key + "=>" + keyValue.Value + ";";
                    }
                    lstEvents.Items[itemNum].SubItems.AddRange(new string[] { text, notificationMessage.MessageAction.ToString() });
                    _eventCount += 1;

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            proxy.Subscribe();
            proxy.Ping();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            proxy.GetNotifications(textBox1.Text);
        }
    }
}

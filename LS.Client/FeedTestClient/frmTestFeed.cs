using LightStreamerConnector;
using LS.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FeedTestClient
{
    public partial class frmTestFeed : Form
    {
        private LsConnector m_LsConnector = null;
        BlockingCollection<FeedMessage> m_ReceivedFeedBCollecion = null;
        dsFeed m_dsFeed = new dsFeed();
        public frmTestFeed()
        {
            InitializeComponent();
            dataGridNotificaions.DataSource = m_dsFeed.Notificaionts;
            
            comboBoxSubscriptionMode.SelectedIndex = 0;
           
        }

        private void btnGetNotificaions_Click(object sender, EventArgs e)
        {
            try
            {
                m_LsConnector = new LsConnector();

            

                m_LsConnector.PushServer = "localhost";
                m_LsConnector.Port = 8080;
                m_LsConnector.AdapterSetName = "WELCOME";
                m_LsConnector.FeedAdapter = "MY_REMOTE";
                m_LsConnector.UserName = "EfgUser";
                m_LsConnector.Password = "pasword";

                // will be used to resolve schema info to send it to Metadata Provider
                m_LsConnector.SchemaFilePath = @"E:\NotificationProvider\LS.Client\LightStreamerConnector\SchemaInfo.xml";


                m_LsConnector.Initialize();
                m_LsConnector.LoadSchemaInfo();
                bool result =   m_LsConnector.ConnectToLs();
                if(result == true)
                {
                    MessageBox.Show("Connected to LightStreamer Successfully");

                    m_ReceivedFeedBCollecion = m_LsConnector.ReceivedFeedBCollecion;

                    Task.Factory.StartNew(() =>
                    {
                        StartProcessFeed();
                    });
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

            
        }

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            try
            {

                m_LsConnector.Subscibe(txtboxSubscriptionItem.Text, comboBoxSubscriptionMode.Text);
            }
            catch(Exception exp)
            { MessageBox.Show(exp.ToString()); }
        }

        private void StartProcessFeed()
        {
            foreach (FeedMessage msg in m_ReceivedFeedBCollecion.GetConsumingEnumerable())
            {
                try
                {
                    HandleFeedMessage(msg);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void _HandleFeedMessage(FeedMessage msg)
        {
            try
            {
                dsFeed.NotificaiontsRow dr = m_dsFeed.Notificaionts.NewNotificaiontsRow();
                foreach (string columnName in msg.DataItems.Keys)
                {
                    string value = msg.DataItems[columnName];
                    dr[columnName] = value;
                }

                m_dsFeed.Notificaionts.AddNotificaiontsRow(dr);
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }


        private void HandleFeedMessage(FeedMessage msg)
        {
            try
            {
                switch(msg.Action)
                {
                    case "ADD":
                        AddNewNotificaion(msg);
                        break;

                    case "UPDATE":
                        UpdateNotificaion(msg);
                        break;

                    case "DELETE":
                        DeleteNotificaion(msg);
                        break;
                }



              
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void DeleteNotificaion(FeedMessage msg)
        {
            dsFeed.NotificaiontsRow dr = m_dsFeed.Notificaionts.FindByCode(msg.Code);
            if (dr != null)
            {
                m_dsFeed.Notificaionts.RemoveNotificaiontsRow(dr);

                m_dsFeed.AcceptChanges();
            }
        //    dataGridNotificaions.Refresh();
        }

        private void UpdateNotificaion(FeedMessage msg)
        {
            dsFeed.NotificaiontsRow dr = m_dsFeed.Notificaionts.FindByCode(msg.Code);
            if(dr!=null)
            {
                dr.BeginEdit();

                foreach (string columnName in msg.DataItems.Keys)
                {
                    string value = msg.DataItems[columnName];
                    dr[columnName] = value;
                }


                dr.EndEdit();

                m_dsFeed.AcceptChanges();

             //   dataGridNotificaions.Refresh();
            }
        }

        private void AddNewNotificaion(FeedMessage msg)
        {
            dsFeed.NotificaiontsRow dr = m_dsFeed.Notificaionts.NewNotificaiontsRow();
            foreach (string columnName in msg.DataItems.Keys)
            {
                string value = msg.DataItems[columnName];
                dr[columnName] = value;
            }
            dr.Code = msg.Code;
            m_dsFeed.Notificaionts.AddNotificaiontsRow(dr);
            m_dsFeed.AcceptChanges();

          //  dataGridNotificaions.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                m_LsConnector = new LsConnector();

               
                m_LsConnector.SchemaFilePath = @"E:\NotificationProvider\LS.Client\LightStreamerConnector\SchemaInfo.xml";

                m_LsConnector.Initialize();
              
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            m_LsConnector.Unsubscribe();
        }
    }
}

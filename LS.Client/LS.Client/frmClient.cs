using Lightstreamer.DotNet.Client;
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

namespace LS.Client
{
    public partial class frmClient : Form
    {
       
        DataView dvStocks = null;
        string schecmName = string.Empty;
        string itemprefix = string.Empty;
        string subsciptionmdoe = string.Empty;
        LSClient myClient = null;
        BlockingCollection<UpdateMessage> m_ReceivedFeedBCollecion = null;
        dsFeed m_dsFeed = new dsFeed();
        public frmClient()
        {
            InitializeComponent();
            comboBoxSubscriptionMode.SelectedIndex = 0;
        }

        private void frmClient_Load(object sender, EventArgs e)
        {
            m_dsFeed = new dsFeed();
            
            
           

           
            dgNotifications.DataSource = m_dsFeed.Notificaionts;

            m_ReceivedFeedBCollecion = new BlockingCollection<UpdateMessage>();

            Task.Factory.StartNew(() =>
            {
                StartProcessFeed();
            });

        }

        private void StartProcessFeed()
        {
            foreach (UpdateMessage msg in m_ReceivedFeedBCollecion.GetConsumingEnumerable())
            {
                try
                {
                    HandleUpdateMessage(msg);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void HandleUpdateMessage(UpdateMessage msg)
        {
            try
            {
                UpdateNotificaionTable(msg);


                Console.WriteLine("UpdateMessage Received.");
                 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //private void ProcessMarketPriceTable(string code, IUpdateInfo updateInfo)
        //{
        //    try
        //    {
        //        string subCode = updateInfo.GetNewValue(1); // subscriptionCode
        //        string receivedcode = updateInfo.GetNewValue(2); //Cdoe
        //        string LastPrice = updateInfo.GetNewValue(3); // lastPrice
        //        string Quantity = updateInfo.GetNewValue(4); //Quantity
        //        string RecSerial = updateInfo.GetNewValue(5);//RecSerial

        //     //   dsFeed.MarketPriceRow drMP = m_dsFeed.MarketPrice.FindByCode(code);

        //        if(drMP == null)
        //        {
        //            m_dsFeed.MarketPrice.AddMarketPriceRow(subCode, code, LastPrice, Quantity, RecSerial);
        //        }
        //        else
        //        {
        //            drMP.BeginEdit();
        //            drMP.SubscriptionCode = subCode;
        //            drMP.LastPrice = LastPrice;
        //            drMP.Quantity = Quantity;
        //            drMP.RecSerial = RecSerial;


        //            drMP.EndEdit();

        //            drMP.Table.AcceptChanges();
        //        }



        //    }
        //    catch(Exception exp)
        //    {
        //        MessageBox.Show(exp.ToString());
        //    }
        //}


        private void UpdateNotificaionTable(UpdateMessage updateMsg)
        {
            try
            {
                string EventIFANotificationID = updateMsg.UpdateInfo.GetNewValue(1);
                string EventMessageID = updateMsg.UpdateInfo.GetNewValue(2);
                string EventIFANotificationBody = updateMsg.UpdateInfo.GetNewValue(3);
                string EventIFANotificationTitle = updateMsg.UpdateInfo.GetNewValue(4);
                string EventIFASubscriberID = updateMsg.UpdateInfo.GetNewValue(5);
                string EventIFASubscriberNotificationAddress = updateMsg.UpdateInfo.GetNewValue(6);
                string _EventMessagesStatusID = updateMsg.UpdateInfo.GetNewValue(7);
                string ParentEventMessageID = updateMsg.UpdateInfo.GetNewValue(8);
                string ExpiryDate = updateMsg.UpdateInfo.GetNewValue(9);
                string EventMessageStatus = updateMsg.UpdateInfo.GetNewValue(10);
                string FirstParentEventmessageID = updateMsg.UpdateInfo.GetNewValue(11);





                dsFeed.NotificaiontsRow dr = m_dsFeed.Notificaionts.NewNotificaiontsRow();
                dr.EventIFANotificationID = EventIFANotificationID;
                dr.EventMessageID = EventMessageID;
                dr.EventIFANotificationBody = EventIFANotificationBody;
                dr.EventIFANotificationTitle = EventIFANotificationTitle;
                dr.EventIFASubscriberID = EventIFASubscriberID;
                dr.EventIFASubscriberNotificationAddress = EventIFASubscriberNotificationAddress;
                dr._EventMessagesStatusID = _EventMessagesStatusID;
                dr.ParentEventMessageID = ParentEventMessageID;
                dr.ExpiryDate = ExpiryDate;
                dr.EventMessageStatus = EventMessageStatus;
                dr.FirstParentEventmessageID = FirstParentEventmessageID;

                m_dsFeed.Notificaionts.AddNotificaiontsRow(dr);

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }


        private void UpdateSubscriptionParam(string messageType)
        {
            try
            {
                switch (messageType)
                {
                    case "MarketPrices":
                        schecmName = "schmp";
                        itemprefix = "mp";
                        subsciptionmdoe = "MERGE";

                       
                        textboxSchema.Text = schecmName;

                        break;

                    case "Trades":
                        schecmName = "schtr";
                        itemprefix = "tr";
                        subsciptionmdoe = "destinct";

                       
                        textboxSchema.Text = schecmName;

                        break;

                    default:
                        MessageBox.Show("Couldn't update Subscriptiopn Params.");
                        break;
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void btnSubscribeToLs_Click(object sender, EventArgs e)
        {

            try
            {
                SubscribedTableKey[] tableRefs;


                string items = txtBoxSubsciptionItems.Text;
                subsciptionmdoe = comboBoxSubscriptionMode.Text;
                schecmName = textboxSchema.Text;

             



              DialogResult dialogresult =  MessageBox.Show("Subscribe to items : " + items, "Subscription", MessageBoxButtons.YesNo);

                if (dialogresult == DialogResult.No)
                    return;

                SimpleTableInfo tableInfo = new SimpleTableInfo(
                      items,
                       subsciptionmdoe,
                       schecmName,
                       false
                       );

                tableInfo.DataAdapter = txtboxFeedAdatperName.Text;


               
                SubscribedTableKey tableRef = myClient.SubscribeTable(
                  tableInfo,
                  new TableListenerForExtended(m_ReceivedFeedBCollecion),
                  false
                  );


                tableRefs = new SubscribedTableKey[] { tableRef };
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }


        }

      

       

        private void btnConnectToLs_Click(object sender, EventArgs e)
        {
            try
            {
                lblConnecionStatus.Text = "";
                LSClient.SetLoggerProvider(new Log4NetLoggerProviderWrapper());

                string pushServerHost = txtboxLsServer.Text;
                int pushServerPort = Convert.ToInt32(txtLsPort.Text);

                ConnectionInfo connInfo = new ConnectionInfo();
                connInfo.PushServerUrl = "http://" + pushServerHost + ":" + pushServerPort;
                connInfo.Adapter = txtboxAdapterSetName.Text;

                connInfo.User = txtboxUserName.Text;
                connInfo.Password = txtboxPassword.Text;


                myClient = new LSClient();

                myClient.OpenConnection(connInfo, new LsConnectionListener(lblConnecionStatus));
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

      

        private void dgStocks_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
          

        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                SubscribedTableKey[] tableRefs;


                string items = txtBoxSubsciptionItems.Text;
                subsciptionmdoe = comboBoxSubscriptionMode.Text;
                schecmName = textboxSchema.Text;





                DialogResult dialogresult = MessageBox.Show("Subscribe to items : " + items, "Subscription", MessageBoxButtons.YesNo);

                if (dialogresult == DialogResult.No)
                    return;

                SimpleTableInfo tableInfo = new SimpleTableInfo(
                      items,
                       subsciptionmdoe,
                       schecmName,
                       false
                       );

                tableInfo.DataAdapter = txtboxFeedAdatperName.Text;



                SubscribedTableKey tableRef = myClient.SubscribeTable(
                  tableInfo,
                  new TableListenerForExtended(m_ReceivedFeedBCollecion),
                  false
                  );


                tableRefs = new SubscribedTableKey[] { tableRef };
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

            //try
            //{
            //    SubscribedTableKey[] tableRefs;


            //    string items = txtBoxSubsciptionItems.Text;
            //    subsciptionmdoe = comboBoxSubscriptionMode.Text;
            //    schecmName = textboxSchema.Text;



            //    string[] itemsArr = items.Split(new char[] {' '});

            //    string[] itemscodes = new string[] { "mpEGS700" };
            //    string[] schema = new string[] { "Code", "SubscriptionCode", "LastPrice", "Quantity", "RecSerial" };

            //    ExtendedTableInfo tableInfo = new ExtendedTableInfo(
            //          itemscodes,
            //           subsciptionmdoe,
            //           schema,
            //           false
            //           );

            //    tableInfo.DataAdapter = txtboxFeedAdatperName.Text;



            //    SubscribedTableKey tableRef = myClient.SubscribeTable(
            //      tableInfo,
            //      new TableListenerForExtended(),
            //      false
            //      );


            //    tableRefs = new SubscribedTableKey[] { tableRef };
            //}
            //catch (Exception exp)
            //{
            //    MessageBox.Show(exp.ToString());
            //}

        }

        private void textboxSchema_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
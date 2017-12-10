using Lightstreamer.DotNet.Client;
using LS.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightStreamerConnector
{
    /// <summary>
    /// Wrapping lightstreamer functionality
    /// </summary>
    public class LsConnector
    {
        LSClient m_lsClient = null;

        private TableListenerForExtended m_TableListenerForExtended = null;
        private  BlockingCollection<FeedMessage> m_ReceivedFeedBCollecion = null;
        private dsSchema m_dsSchema = null;
        public string PushServer { get; set; }
        public int Port { get; set; }
        public string FeedAdapter { get; set; }

        public string AdapterSetName { get; set; }
      
        public string UserName { get; set; }

        public string Password { get; set; }

        public int ProbeTimeoutMillis { get; set; }

        public string SchemaFilePath { get; set; } 
        public BlockingCollection<FeedMessage> ReceivedFeedBCollecion
        {
            get
            {
                return m_ReceivedFeedBCollecion;
            }

            set
            {
                m_ReceivedFeedBCollecion = value;
            }
        }



        /// <summary>
        /// Establish Connecion with 
        /// LighStreamer
        /// </summary>
        /// <returns></returns>
        public bool ConnectToLs()
        {

           
                // LSClient.SetLoggerProvider(new Log4NetLoggerProviderWrapper());

                string pushServerHost = PushServer;
                int pushServerPort = Port;

                ConnectionInfo connInfo = new ConnectionInfo();

                connInfo.PushServerUrl = "http://" + pushServerHost + ":" + pushServerPort;
                connInfo.Adapter = AdapterSetName;

                connInfo.User = UserName;
                connInfo.Password = Password;


            

                 

                m_lsClient.OpenConnection(connInfo, new LsConnectionListener());
                return true;
           
        }
        /// <summary>
        /// Subscirbe in lightstreamer
        /// server to Get item feed
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="SchemaCode"></param>
        public void Subscibe(string Item, string Mode)
        {
           
                string schema = GetSchema(Item);
               SimpleTableInfo tableInfo = new SimpleTableInfo(
                      Item,
                       Mode,
                       schema,
                       true
                       );

                tableInfo.DataAdapter = FeedAdapter;

                SubscribedTableKey tableRef = m_lsClient.SubscribeTable(
                  tableInfo,
                  m_TableListenerForExtended,
                  false
                  );


          
        }

      
        //public void Test()
        //{
        //    dsSchema ds = new dsSchema();
        //    ds.SchemaInfo.AddSchemaInfoRow("Notify_User", "EventIFANotificationID", 1);
        //    ds.SchemaInfo.AddSchemaInfoRow("Notify_User", "EventMessageID", 2);
        //    ds.SchemaInfo.AddSchemaInfoRow("Notify_User", "EventIFANotificationBody", 3);
        //    ds.SchemaInfo.AddSchemaInfoRow("Notify_User", "EventIFANotificationTitle", 4);
        //    ds.SchemaInfo.AddSchemaInfoRow("Notify_User", "EventIFASubscriberID", 5);
        //    ds.SchemaInfo.AddSchemaInfoRow("Notify_User", "EventIFASubscriberNotificationAddress", 6);
        //    ds.SchemaInfo.AddSchemaInfoRow("Notify_User", "_EventMessagesStatusID", 7);
        //    ds.SchemaInfo.AddSchemaInfoRow("Notify_User", "ParentEventMessageID", 8);
        //    ds.SchemaInfo.AddSchemaInfoRow("Notify_User", "ExpiryDate", 9);
        //    ds.SchemaInfo.AddSchemaInfoRow("Notify_User", "EventMessageStatus", 10);
        //    ds.SchemaInfo.AddSchemaInfoRow("Notify_User", "FirstParentEventmessageID", 11);

        //    ds.SchemaInfo.WriteXml("C:\\SchemaInfo.xml");
        //}

        private string GetSchema(string item)
        {
           
                int indexOfSchemaSpliter = item.IndexOf('#');
                string schemacode = item.Remove(indexOfSchemaSpliter);




                IEnumerable<dsSchema.SchemaInfoRow> rows = m_dsSchema.SchemaInfo.Where(r => r.SchemaCode == schemacode);

                StringBuilder sbuilder = new StringBuilder();

                foreach (dsSchema.SchemaInfoRow dr in rows)
                {
                    sbuilder.Append(dr.FieldName);
                    sbuilder.Append(" ");
                }
              
            return sbuilder.ToString().TrimEnd();
            

        }

        public void Initialize()
        {
          
                m_lsClient = new LSClient();
                ReceivedFeedBCollecion = new BlockingCollection<FeedMessage>();
            m_dsSchema = new dsSchema();
            m_TableListenerForExtended = new TableListenerForExtended(m_ReceivedFeedBCollecion,m_dsSchema);
          
               
            
          
        }

        public void LoadSchemaInfo()
        {

            
            m_dsSchema.SchemaInfo.ReadXml(SchemaFilePath);
            
            
        }
    }
}

using Lightstreamer.DotNet.Client;
using LightStreamerConnector;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Client
{
    public class TableListenerForExtended : IHandyTableListener
    {

        BlockingCollection<FeedMessage> m_ReceivedFeedBCollecion = null;

        dsSchema m_dsSchema = null;
        string m_schemapath = string.Empty;
        public TableListenerForExtended(BlockingCollection<FeedMessage> receivedFeedBCollecion,dsSchema dsschema)
        {
            m_ReceivedFeedBCollecion = receivedFeedBCollecion;
            m_dsSchema = dsschema;
        }

        
        public TableListenerForExtended()
        {
        }

        private string NotifyUpdate(IUpdateInfo update)
        {
            return update.Snapshot ? "snapshot" : "update";
        }

        public void OnUpdate(int itemPos, string itemName, IUpdateInfo updateInfo)
        {
            FeedMessage feedMsg = new FeedMessage();
            int indexOfSchemaSpliter = itemName.IndexOf('#');
            string schemacode = itemName.Remove(indexOfSchemaSpliter);

           IEnumerable<dsSchema.SchemaInfoRow> rows =  m_dsSchema.SchemaInfo.Where(r => r.SchemaCode == schemacode);

            Dictionary<string, string> data = new Dictionary<string, string>();

            foreach (dsSchema.SchemaInfoRow dr in rows)
            {
                string fieldName = dr.FieldName;
                string value = updateInfo.GetNewValue(dr.FieldName.Trim());
                data.Add(fieldName, value);
            }
            feedMsg.ItemName = itemName;
            feedMsg.DataItems = data;
            m_ReceivedFeedBCollecion.Add(feedMsg);
        }
        /// <summary>
        /// SimpleTableInfo
        /// </summary>
        /// <param name="itemPos"></param>
        /// <param name="itemName"></param>
        /// <param name="updateInfo"></param>
        public void _OnUpdate(int itemPos, string itemName, IUpdateInfo updateInfo)
        {
            

            itemName = updateInfo.GetNewValue(6);  // will be modified to remove hard coded value.
            FeedMessage feedMsg = new FeedMessage();
            int indexOfSchemaSpliter = itemName.IndexOf('#');
            string schemacode = itemName.Remove(indexOfSchemaSpliter);

            IEnumerable<dsSchema.SchemaInfoRow> rows = m_dsSchema.SchemaInfo.Where(r => r.SchemaCode == schemacode);

            Dictionary<string, string> data = new Dictionary<string, string>();

            foreach (dsSchema.SchemaInfoRow dr in rows)
            {
                string fieldName = dr.FieldName;
                string value = updateInfo.GetNewValue(dr.FieldIndex);
                data.Add(fieldName, value);
            }
            feedMsg.ItemName = itemName;
            feedMsg.DataItems = data;
            m_ReceivedFeedBCollecion.Add(feedMsg);
        }

        public void OnSnapshotEnd(int itemPos, string itemName)
        {
            Console.WriteLine("end of snapshot for " + itemName);
        }

        public void OnRawUpdatesLost(int itemPos, string itemName, int lostUpdates)
        {
            Console.WriteLine(lostUpdates + " updates lost for " + itemName);
        }

        public void OnUnsubscr(int itemPos, string itemName)
        {
            Console.WriteLine("unsubscr " + itemName);
        }

        public void OnUnsubscrAll()
        {
            Console.WriteLine("unsubscr table");
        }

        
    }

    
}

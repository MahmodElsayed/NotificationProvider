using Lightstreamer.DotNet.Client;
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

        BlockingCollection<UpdateMessage> m_ReceivedFeedBCollecion = null;
        public TableListenerForExtended(BlockingCollection<UpdateMessage> receivedFeedBCollecion)
        {
            m_ReceivedFeedBCollecion = receivedFeedBCollecion;
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


            
          //  string itemsubCode =   updateInfo.GetNewValue(1);

            Console.WriteLine("===============================================================");
           // Console.WriteLine("Item of Code : {0} received at : {1}", itemsubCode, DateTime.Now);
            Console.WriteLine("Value = {0} at inedex 1 received", updateInfo.GetNewValue(1));
            Console.WriteLine("Value = {0} at inedex 2 received", updateInfo.GetNewValue(2));
            Console.WriteLine("Value = {0} at inedex 3 received", updateInfo.GetNewValue(3));
            Console.WriteLine("Value = {0} at inedex 4 received", updateInfo.GetNewValue(4));
            Console.WriteLine("Value = {0} at inedex 5 received", updateInfo.GetNewValue(5));
            Console.WriteLine("***************************************************************");
           
            UpdateMessage msg = new UpdateMessage();
          //  msg.MessagePrefix = itemsubCode.Substring(0,2);
            msg.ItemName = itemName;//itemsubCode;
           // msg.Code = itemsubCode.Substring(2, itemsubCode.Length - 2);
            msg.UpdateInfo = updateInfo;
            m_ReceivedFeedBCollecion.Add(msg);
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

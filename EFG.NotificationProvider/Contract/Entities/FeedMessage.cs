using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    [Serializable]
    [DataContract]
    public class FeedMessage
    {
      
      
        public string Exchange { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string SubscriptionCode { get; set; }
        [DataMember]
        public string MessageType { get; set; }
        [DataMember]
        public string command { get; set; }
        [DataMember]
        public string key { get; set; }
        [DataMember]
        public string MessageAction { get; set; }
        [DataMember]
        public Dictionary<string, string> MessageFields { get; set; }
        [DataMember]
        public long SequenceNumber { get; set; }
       
        
    }
}
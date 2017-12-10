using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EFG.OPS.NotificationEngineService.Contracts.Enums;

namespace EFG.OPS.NotificationEngineService.Contracts.Entities
{
    [DataContract]
    public class NotificationMessage
    {
        [DataMember]
        public Dictionary<string, string> BodyDictionary { get; set; }

        [DataMember]
        public MessageAction MessageAction { get; set; }

        [DataMember]
        public string NotificationKey { get; set; }

       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{

    [DataContract]
    public class LogOutRequest
    {
        [DataMember]
        public string UserName { get; set; }
     
    }
}
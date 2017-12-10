using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
   [DataContract]
    public class LoginResponse
    {
       [DataMember]
       public LoginStatus ServiceLoginStatus { get; set; }

       [DataMember]
       public string Message { get; set; }

    }
}

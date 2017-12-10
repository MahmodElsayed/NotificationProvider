using Lightstreamer.DotNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Client
{/// <summary>
/// encapsulate received feed
/// </summary>
   public class FeedMessage
    {
        public string ItemName { get; set; }

        public string Code { get; set; }

        public string MessagePrefix { get; set; }

        public Dictionary<string,string> DataItems { get; set; }
    }
}

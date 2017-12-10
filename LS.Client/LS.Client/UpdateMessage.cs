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
   public class UpdateMessage
    {
        public string ItemName { get; set; }

        public string Code { get; set; }

        public string MessagePrefix { get; set; }

        public IUpdateInfo UpdateInfo { get; set; }
    }
}

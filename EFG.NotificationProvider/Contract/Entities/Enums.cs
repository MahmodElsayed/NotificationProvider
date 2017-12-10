using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{

    public enum LoginStatus
    {
        Succeeded = 1,
        Failed = 2,
        Expired = 3,
        UnkownError = 4
    }

    public enum FeedMessageAction : int
    {
        Add = 1, 
        Update = 2,
        Delete = 3,
    }
    public enum MessageType : int
    {
        MarketByPrice = 1,
        MarketByOrder = 2,
        SymbolMarketStatistics = 3,
        ExchangeStatus = 7
    }
}
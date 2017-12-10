using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    [DataContract]
    public class FeedDataFilters
    {
        [DataMember]
        public List<FilterItem> FeedFiltersList { get; set; }
    }
    
    [DataContract]
    public class FilterItem
    {
        [DataMember]
        public string FilterKey { get; set; }
        [DataMember]
        public object value { get; set; }
    }
}
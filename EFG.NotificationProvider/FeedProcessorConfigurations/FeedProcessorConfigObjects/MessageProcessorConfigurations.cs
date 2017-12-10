using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedProcessorConfigurations
{
    public class MessageProcessorConfigurations
    {
        private MessageAttributes[] m_MessageAttributesList = null;

        public MessageAttributes[] MessageAttributesList
        {
            get { return m_MessageAttributesList; }
            set { m_MessageAttributesList = value; }
        }

    }

    public class MessageAttributes
    {
        
        public string MessageTypeName { get; set; }
       

        private MessageTag[] m_MessageTags = null;

        public MessageTag[] MessageTagsList
        {
            get { return m_MessageTags; }
            set { m_MessageTags = value; }
        }
    }

    public class MessageTag
    {
        public string SourceFeild { get; set; }
        public string ColumnName { get; set; }
       

    }
}
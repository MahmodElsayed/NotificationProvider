using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaHandler
{
    public class SchemaInfo
    {
        private MsgTypeSchema[] m_SchemaList = null;

        public MsgTypeSchema[] SchemaList
        {
            get { return m_SchemaList; }
            set { m_SchemaList = value; }
        }

    }

    public class MsgTypeSchema
    {
        public string Exchange { get; set; }
      
        public string MessageType { get; set; }

        public string SchemaCode { get; set; }

        public ColumnInfo[] ColumnsList { get; set; }
       


    }

    public class ColumnInfo
    {

        public string ColumnName { get; set; }
        public string DataType { get; set; }

        public string DefaultVal { get; set; }




    }
}

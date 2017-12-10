using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace SchemaHandler
{
    /// <summary>
    /// Load MDF required configuraions into dictionaries.
    /// </summary>
    public class SchemaInfoLoader
    {
        #region Declerations

        SchemaInfo m_SchemaInfo = null;
        #endregion

        #region Constructor
        public SchemaInfoLoader()
        {
            try
            {
                LoadSchema();
            }
            catch(Exception exp)
            {
                Console.WriteLine("Failed to load schema");
                Console.WriteLine("Error Details : {0} ", exp.ToString());
            }
        }

        #endregion

        #region Private Members

        private object ReadConfiguraion(string fileName, Type objectType)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException(string.Format("File {0} was not found", fileName));

            FileStream fs = null;

            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

            XmlSerializer reader = null;

            try
            {
                reader = new XmlSerializer(objectType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                try
                {
                    fs.Close();
                }
                catch (Exception fEx)
                {
                    Console.WriteLine(ex.ToString());
                }

                return null;
            }


            object configObject = null;

            try
            {
                configObject = reader.Deserialize(fs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                try
                {
                    fs.Close();
                }
                catch (Exception fEx)
                {
                    Console.WriteLine(fEx.ToString());
                }
                return null;
            }

            try
            {
                fs.Close();
            }
            catch (Exception fEx)
            {
                Console.WriteLine(fEx.ToString());
            }

            return configObject;
        }

        public Dictionary<string, string[]> GetItemsSchema()
        {
            Dictionary<string, string[]> schemaDictionary = new Dictionary<string, string[]>();
            try
            {
                if (m_SchemaInfo != null)
                {
                    foreach (MsgTypeSchema msgtypeSchema in m_SchemaInfo.SchemaList)
                    {

                        if (schemaDictionary.ContainsKey(msgtypeSchema.SchemaCode) == false)
                        {
                            List<string> columns = new List<string>();

                            foreach (ColumnInfo col in msgtypeSchema.ColumnsList)
                            {
                                columns.Add(col.ColumnName.Trim());
                            }

                            schemaDictionary.Add(msgtypeSchema.SchemaCode, columns.ToArray());
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
            }

            return schemaDictionary;
        }
        #endregion

        #region Public Members


        private void LoadSchema()
        {
            #region FeedMessage Processor Configuraions
            try
            {
                string SchemaInfoPath = System.Configuration.ConfigurationManager.AppSettings.Get("SchemaInfoPath");
                m_SchemaInfo = (SchemaInfo)ReadConfiguraion(SchemaInfoPath, typeof(SchemaInfo));


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
        }

      
       
        #endregion
        #endregion

    }
}

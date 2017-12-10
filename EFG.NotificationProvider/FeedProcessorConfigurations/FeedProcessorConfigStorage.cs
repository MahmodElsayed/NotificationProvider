
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FeedProcessorConfigurations

{
    /// <summary>
    /// Load required configuraions into dictionaries.
    /// </summary>
    public class FeedProcessorConfigStorage
    {
        #region Declerations


        private static Logger m_AppLogger;

        private MessageProcessorConfigurations m_MessageProcessorConfigurations = null;
        private Dictionary<string, MessageAttributes> m_MessagesAttributesInfo = null;

       

      
        #endregion

        #region Constructor
        public FeedProcessorConfigStorage()
        {
            m_AppLogger = LogManager.GetLogger("AppLogger");
            m_MessagesAttributesInfo = new Dictionary<string, MessageAttributes>();
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
                m_AppLogger.Error("The following error occured while trying to construct file stream for file {0}", fileName);
                m_AppLogger.Error(ex);
                return null;
            }

            XmlSerializer reader = null;

            try
            {
                reader = new XmlSerializer(objectType);
            }
            catch (Exception ex)
            {
                m_AppLogger.Error("The following error occured while trying to construct XMLReader for file {0} and type {1}", fileName, objectType);
                m_AppLogger.Error(ex);
                try
                {
                    fs.Close();
                }
                catch (Exception fEx)
                {
                    m_AppLogger.Error("The following error occured while trying to close file stream");
                    m_AppLogger.Error(fEx);
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
                m_AppLogger.Error("The following error occured while trying to deserialize type {0} from file {1}", objectType, fileName);
                m_AppLogger.Error(ex);
                try
                {
                    fs.Close();
                }
                catch (Exception fEx)
                {
                    m_AppLogger.Error("The following error occured while trying to close file stream");
                    m_AppLogger.Error(fEx);
                }
                return null;
            }

            try
            {
                fs.Close();
            }
            catch (Exception fEx)
            {
                m_AppLogger.Error("The following error occured while trying to close file stream");
                m_AppLogger.Error(fEx);
            }

            return configObject;
        }

        private void FillMessagesAttributesInfoDictionary()
        {
            try
            {
                foreach (MessageAttributes msgAttributes in m_MessageProcessorConfigurations.MessageAttributesList)
                {
                    m_MessagesAttributesInfo.Add(msgAttributes.MessageTypeName, msgAttributes);
                }

            }
            catch (Exception exp)
            {
                throw exp;

            }
        }
        #endregion

        #region Public Members


        public void Initialize()
        {
            
            try
            {
                string feedProcessorConfigPath = System.Configuration.ConfigurationManager.AppSettings.Get("feedProcessorConfigPath");
                m_MessageProcessorConfigurations = (MessageProcessorConfigurations)ReadConfiguraion(feedProcessorConfigPath, typeof(MessageProcessorConfigurations));

              
                // Fill processing dictionaries

                FillMessagesAttributesInfoDictionary();


            }
            catch (Exception exp)
            {
                m_AppLogger.Error("Error in method LoadFeedProcessorConfiguraions. Error details : {0} ", exp.ToString());

            }
            
         

        }

      

        ////public MessageProcessorConfigurations MessageProcessorConfig
        ////{
        ////    get { return m_MessageProcessorConfigurations; }
        ////    set { m_MessageProcessorConfigurations = value; }
        ////}

        public Dictionary<string, MessageAttributes> MessagesAttributesInfo
        {
            get
            {
                return m_MessagesAttributesInfo;
            }

            set
            {
                m_MessagesAttributesInfo = value;
            }
        }

        
        #endregion

    }
}
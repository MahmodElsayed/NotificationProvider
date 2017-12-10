using Lightstreamer.Interfaces.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using SchemaHandler;

namespace LS.MetadataAdapter
{
    public class MetadataAdapter : IMetadataProvider
    {

        #region Private Members
        private Dictionary<string,string[]> m_schemaDictionary = null; //<orders,columns[]>
        #endregion


        #region Constructor
        public MetadataAdapter()
        {

            try
            {
                SchemaInfoLoader m_SchemaLoader = new SchemaInfoLoader();
                m_schemaDictionary = m_SchemaLoader.GetItemsSchema();
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp.ToString());
            }

        }
        #endregion

        #region Implmented IMetadataProvider Interface Members
        void IMetadataProvider.NotifyUser(string user, string password, IDictionary httpHeaders)
        {
            AuthenticationService authenticateService = new AuthenticationService();

            // get sessionid or token from httpHeaders
            string sessionid = "getfromHeader";
            bool result = authenticateService.AuthenticateClient(sessionid, user);
            if (result == false)
            {
                throw new AccessException("You are not authorized to access requested datafeed!");
            }
        }

        string[] IMetadataProvider.GetItems(string user, string sessionID, string items)
        {

            string[] clientItems = Helpers.SplitString(items, " ");

            try
            {
                if (clientItems.Length > 0)
                    return clientItems;
                else
                    return clientItems;
            }
            catch (Exception ex)
            {
                //myLogger.FileLogger.Error("Couldn't Get The User's Items [ User ID:" + user + ", Items: " + items + " ] | ", ex.ToString().Replace("\n", "\r"));
                return clientItems;
            }


 
        }

        string[] IMetadataProvider.GetSchema(string user, string sessionID, string id, string schema)
        {

            string[] clientSchema = Helpers.SplitString(schema, " ");

            try
            {
                if (clientSchema.Length > 0)
                    return clientSchema;
                else
                    return clientSchema;
            }
            catch (Exception ex)
            {
               // myLogger.FileLogger.Error("Couldn't User's Schema [ User ID:" + user + ", schmea: " + schema + " ] | ", ex.ToString().Replace("\n", "\r"));
                return clientSchema;
            }

        }
        #endregion


        #region Non Implmented IMetadataProvider Interface Members


        int IMetadataProvider.GetAllowedBufferSize(string user, string item)
        {
            //throw new NotImplementedException();
            return 10000;
        }

        double IMetadataProvider.GetAllowedMaxBandwidth(string user)
        {
            //throw new NotImplementedException();
            return 10000;
        }

        double IMetadataProvider.GetAllowedMaxItemFrequency(string user, string item)
        {
            return 10000;
        }

        int IMetadataProvider.GetDistinctSnapshotLength(string item)
        {
            return 10000;
        }

        double IMetadataProvider.GetMinSourceFrequency(string item)
        {
            return 10000;
        }

        void IMetadataProvider.Init(IDictionary parameters, string configFile)
        {
            //throw new NotImplementedException();
        }

        bool IMetadataProvider.IsModeAllowed(string user, string item, Mode mode)
        {

            return true;
        }

        bool IMetadataProvider.ModeMayBeAllowed(string item, Mode mode)
        {
            return true;
        }

        void IMetadataProvider.NotifyMpnDeviceAccess(string user, MpnDeviceInfo device)
        {
            
        }

        void IMetadataProvider.NotifyMpnDeviceTokenChange(string user, MpnDeviceInfo device, string newDeviceToken)
        {
           
        }

        void IMetadataProvider.NotifyMpnSubscriptionActivation(string user, string sessionID, TableInfo table, MpnSubscriptionInfo mpnSubscription)
        {
           
        }

        void IMetadataProvider.NotifyNewSession(string user, string sessionID, IDictionary clientContext)
        {
            //throw new NotImplementedException();
        }

        void IMetadataProvider.NotifyNewTables(string user, string sessionID, TableInfo[] tables)
        {
           
        }

        void IMetadataProvider.NotifySessionClose(string sessionID)
        {
            
        }

        void IMetadataProvider.NotifyTablesClose(string sessionID, TableInfo[] tables)
        {
            throw new NotImplementedException();
        }

        void IMetadataProvider.NotifyUser(string user, string password, IDictionary httpHeaders, string clientPrincipal)
        {
           
        }

        void IMetadataProvider.NotifyUserMessage(string user, string sessionID, string message)
        {
           
        }

        bool IMetadataProvider.WantsTablesNotification(string user)
        {
            //throw new NotImplementedException();
            return false;

        }

        #endregion

        #region Private Members
        public static string[] SplitString(string text, string separators)
        {
            // return arg.Split(separators.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            // not available in .NET 1.1

            IList tokens = new ArrayList(text.Split(separators.ToCharArray()));

            int pos = 0;
            while (pos < tokens.Count)
            {
                string token = (string)tokens[pos];
                if ((token == null) || (token.Length == 0)) tokens.RemoveAt(pos);
                else pos++;
            }

            string[] _tokens = new string[tokens.Count];
            for (int i = 0; i < tokens.Count; i++) _tokens[i] = (string)tokens[i];
            return _tokens;
        }
        #endregion

    }
}

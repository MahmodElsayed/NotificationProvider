namespace EFG.Brokerage.General.Notification
{
    partial class dmSendEventNotification
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dmSendEventNotification));
            this.cmdGetEventSubscribersSelect = new System.Data.SqlClient.SqlCommand();
            this.Con = new System.Data.SqlClient.SqlConnection();
            this.daGetEventSubscribers = new System.Data.SqlClient.SqlDataAdapter();
            this.cmdGetEventSubscriberContactsSelect = new System.Data.SqlClient.SqlCommand();
            this.daGetEventSubscriberContacts = new System.Data.SqlClient.SqlDataAdapter();
            this.cmdGetEventkeyValueSelect = new System.Data.SqlClient.SqlCommand();
            this.daGetEventkeyValue = new System.Data.SqlClient.SqlDataAdapter();
            this.cmdGetEventNotificationMessagesSelect = new System.Data.SqlClient.SqlCommand();
            this.daGetEventNotificationMessages = new System.Data.SqlClient.SqlDataAdapter();
            this.cmdGetEventMessageParentSelect = new System.Data.SqlClient.SqlCommand();
            this.daGetEventMessageParent = new System.Data.SqlClient.SqlDataAdapter();
            this.cmdEventIFANotificationsSelect = new System.Data.SqlClient.SqlCommand();
            this.cmdEventIFANotificationsInsert = new System.Data.SqlClient.SqlCommand();
            this.cmdEventIFANotificationsUpdate = new System.Data.SqlClient.SqlCommand();
            this.cmdEventIFANotificationsDelete = new System.Data.SqlClient.SqlCommand();
            this.daEventIFANotifications = new System.Data.SqlClient.SqlDataAdapter();
            this.cmdEventMessagesStatusSetpsSelect = new System.Data.SqlClient.SqlCommand();
            this.daEventMessagesStatusSetps = new System.Data.SqlClient.SqlDataAdapter();
            this.cmdEventMessageSelect = new System.Data.SqlClient.SqlCommand();
            this.cmdcmdEventMessageInsert = new System.Data.SqlClient.SqlCommand();
            this.cmdcmdEventMessageUpdate = new System.Data.SqlClient.SqlCommand();
            this.cmdEventMessageDelete = new System.Data.SqlClient.SqlCommand();
            this.daGetEventMessage = new System.Data.SqlClient.SqlDataAdapter();
            this.cmdMSMQEventTypesSelect = new System.Data.SqlClient.SqlCommand();
            this.daMSMQEventTypes = new System.Data.SqlClient.SqlDataAdapter();
            this.cmdEventMessageIFANotificationsSelect = new System.Data.SqlClient.SqlCommand();
            this.daEventMessageIFANotifications = new System.Data.SqlClient.SqlDataAdapter();
            this.daEventMessageIFAActionNotifications = new System.Data.SqlClient.SqlDataAdapter();
            this.cmdEventMessageIFAActionNotificationsSelect = new System.Data.SqlClient.SqlCommand();
            this.cmdGetUserIFANotificationsSelect = new System.Data.SqlClient.SqlCommand();
            this.daGetUserIFANotifications = new System.Data.SqlClient.SqlDataAdapter();
            // 
            // cmdGetEventSubscribersSelect
            // 
            this.cmdGetEventSubscribersSelect.CommandText = resources.GetString("cmdGetEventSubscribersSelect.CommandText");
            this.cmdGetEventSubscribersSelect.Connection = this.Con;
            this.cmdGetEventSubscribersSelect.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@EventID", System.Data.SqlDbType.Int, 4, "EventID")});
            // 
            // Con
            // 
            this.Con.ConnectionString = "Data Source=efg-dbstg07;Initial Catalog=QA-EGY3;Integrated Security=True";
            this.Con.FireInfoMessageEventOnUserErrors = false;
            // 
            // daGetEventSubscribers
            // 
            this.daGetEventSubscribers.SelectCommand = this.cmdGetEventSubscribersSelect;
            this.daGetEventSubscribers.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "EventMessages", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("EventID", "EventID"),
                        new System.Data.Common.DataColumnMapping("EventTypeID", "EventTypeID"),
                        new System.Data.Common.DataColumnMapping("EventDescA", "EventDescA"),
                        new System.Data.Common.DataColumnMapping("EventDescE", "EventDescE"),
                        new System.Data.Common.DataColumnMapping("EventTempTable", "EventTempTable"),
                        new System.Data.Common.DataColumnMapping("IsMarketEvent", "IsMarketEvent"),
                        new System.Data.Common.DataColumnMapping("IsCompanyEvent", "IsCompanyEvent"),
                        new System.Data.Common.DataColumnMapping("CompanyColumn", "CompanyColumn"),
                        new System.Data.Common.DataColumnMapping("MarketColumn", "MarketColumn"),
                        new System.Data.Common.DataColumnMapping("EventMessageBody", "EventMessageBody"),
                        new System.Data.Common.DataColumnMapping("CreationDate", "CreationDate"),
                        new System.Data.Common.DataColumnMapping("EventSubscriberID", "EventSubscriberID"),
                        new System.Data.Common.DataColumnMapping("EventReciverID", "EventReciverID"),
                        new System.Data.Common.DataColumnMapping("EventReciverTypeID", "EventReciverTypeID"),
                        new System.Data.Common.DataColumnMapping("MarketID", "MarketID"),
                        new System.Data.Common.DataColumnMapping("CompanyCode", "CompanyCode"),
                        new System.Data.Common.DataColumnMapping("HubID", "HubID"),
                        new System.Data.Common.DataColumnMapping("EventNotificationMessageBody", "EventNotificationMessageBody"),
                        new System.Data.Common.DataColumnMapping("_EventNotificationTypeID", "_EventNotificationTypeID"),
                        new System.Data.Common.DataColumnMapping("SenderID", "SenderID")})});
            // 
            // cmdGetEventSubscriberContactsSelect
            // 
            this.cmdGetEventSubscriberContactsSelect.CommandText = "SELECT        Email, Mobile, Fax\r\nFROM            Groups";
            this.cmdGetEventSubscriberContactsSelect.Connection = this.Con;
            // 
            // daGetEventSubscriberContacts
            // 
            this.daGetEventSubscriberContacts.SelectCommand = this.cmdGetEventSubscriberContactsSelect;
            this.daGetEventSubscriberContacts.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "Groups", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Email", "Email"),
                        new System.Data.Common.DataColumnMapping("Mobile", "Mobile"),
                        new System.Data.Common.DataColumnMapping("Fax", "Fax")})});
            // 
            // cmdGetEventkeyValueSelect
            // 
            this.cmdGetEventkeyValueSelect.CommandText = "SELECT        EventkeyValueId, _EventKeyValueTypeID, EventKey, EventKeyValueExpre" +
    "sion, EventKeyValueParameters\r\nFROM            dbo.EventkeyValues\r\nWHERE        " +
    "(EventKey = @EventKey)";
            this.cmdGetEventkeyValueSelect.Connection = this.Con;
            this.cmdGetEventkeyValueSelect.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@EventKey", System.Data.SqlDbType.NChar, 50, "EventKey")});
            // 
            // daGetEventkeyValue
            // 
            this.daGetEventkeyValue.SelectCommand = this.cmdGetEventkeyValueSelect;
            this.daGetEventkeyValue.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "EventkeyValues", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("EventkeyValueId", "EventkeyValueId"),
                        new System.Data.Common.DataColumnMapping("_EventKeyValueTypeID", "_EventKeyValueTypeID"),
                        new System.Data.Common.DataColumnMapping("EventKey", "EventKey"),
                        new System.Data.Common.DataColumnMapping("EventKeyValueExpresion", "EventKeyValueExpresion"),
                        new System.Data.Common.DataColumnMapping("EventKeyValueParameters", "EventKeyValueParameters")})});
            // 
            // cmdGetEventNotificationMessagesSelect
            // 
            this.cmdGetEventNotificationMessagesSelect.CommandText = resources.GetString("cmdGetEventNotificationMessagesSelect.CommandText");
            this.cmdGetEventNotificationMessagesSelect.Connection = this.Con;
            this.cmdGetEventNotificationMessagesSelect.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@EventID", System.Data.SqlDbType.Int, 4, "EventMessageID")});
            // 
            // daGetEventNotificationMessages
            // 
            this.daGetEventNotificationMessages.SelectCommand = this.cmdGetEventNotificationMessagesSelect;
            this.daGetEventNotificationMessages.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "_EventNotificationMessage", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("_EventNotificationMessageID", "_EventNotificationMessageID"),
                        new System.Data.Common.DataColumnMapping("_EventTypeID", "_EventTypeID"),
                        new System.Data.Common.DataColumnMapping("_EventNotificationTypeID", "_EventNotificationTypeID"),
                        new System.Data.Common.DataColumnMapping("EventNotificationMessageBody", "EventNotificationMessageBody"),
                        new System.Data.Common.DataColumnMapping("EventNotificationMessageTitle", "EventNotificationMessageTitle"),
                        new System.Data.Common.DataColumnMapping("MarketID", "MarketID"),
                        new System.Data.Common.DataColumnMapping("CompanyCode", "CompanyCode"),
                        new System.Data.Common.DataColumnMapping("EventNotificationMessageSender", "EventNotificationMessageSender"),
                        new System.Data.Common.DataColumnMapping("_EventReciverTypeID", "_EventReciverTypeID")})});
            // 
            // cmdGetEventMessageParentSelect
            // 
            this.cmdGetEventMessageParentSelect.CommandText = resources.GetString("cmdGetEventMessageParentSelect.CommandText");
            this.cmdGetEventMessageParentSelect.Connection = this.Con;
            this.cmdGetEventMessageParentSelect.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@EventMessageID", System.Data.SqlDbType.Int, 4, "EventMessageID")});
            // 
            // daGetEventMessageParent
            // 
            this.daGetEventMessageParent.SelectCommand = this.cmdGetEventMessageParentSelect;
            this.daGetEventMessageParent.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "EventMessages", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("EventMessageID", "EventMessageID"),
                        new System.Data.Common.DataColumnMapping("_EventTypeID", "_EventTypeID"),
                        new System.Data.Common.DataColumnMapping("CreationDate", "CreationDate"),
                        new System.Data.Common.DataColumnMapping("MessageBody", "MessageBody"),
                        new System.Data.Common.DataColumnMapping("MarketID", "MarketID"),
                        new System.Data.Common.DataColumnMapping("CompanyCode", "CompanyCode"),
                        new System.Data.Common.DataColumnMapping("ExpiryDate", "ExpiryDate")})});
            // 
            // cmdEventIFANotificationsSelect
            // 
            this.cmdEventIFANotificationsSelect.CommandText = resources.GetString("cmdEventIFANotificationsSelect.CommandText");
            this.cmdEventIFANotificationsSelect.Connection = this.Con;
            this.cmdEventIFANotificationsSelect.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@EventIFANotificationID", System.Data.SqlDbType.Int, 4, "EventIFANotificationID")});
            // 
            // cmdEventIFANotificationsInsert
            // 
            this.cmdEventIFANotificationsInsert.CommandText = resources.GetString("cmdEventIFANotificationsInsert.CommandText");
            this.cmdEventIFANotificationsInsert.Connection = this.Con;
            this.cmdEventIFANotificationsInsert.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@EventMessageID", System.Data.SqlDbType.Int, 4, "EventMessageID"),
            new System.Data.SqlClient.SqlParameter("@EventIFANotificationBody", System.Data.SqlDbType.NVarChar, 2147483647, "EventIFANotificationBody"),
            new System.Data.SqlClient.SqlParameter("@EventIFANotificationTitle", System.Data.SqlDbType.NVarChar, 2147483647, "EventIFANotificationTitle"),
            new System.Data.SqlClient.SqlParameter("@EventIFASubscriberID", System.Data.SqlDbType.Int, 4, "EventIFASubscriberID"),
            new System.Data.SqlClient.SqlParameter("@EventIFASubscriberNotificationAddress", System.Data.SqlDbType.NVarChar, 250, "EventIFASubscriberNotificationAddress"),
            new System.Data.SqlClient.SqlParameter("@_EventMessagesStatusID", System.Data.SqlDbType.Int, 4, "_EventMessagesStatusID")});
            // 
            // cmdEventIFANotificationsUpdate
            // 
            this.cmdEventIFANotificationsUpdate.CommandText = resources.GetString("cmdEventIFANotificationsUpdate.CommandText");
            this.cmdEventIFANotificationsUpdate.Connection = this.Con;
            this.cmdEventIFANotificationsUpdate.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@EventMessageID", System.Data.SqlDbType.Int, 4, "EventMessageID"),
            new System.Data.SqlClient.SqlParameter("@EventIFANotificationBody", System.Data.SqlDbType.NVarChar, 2147483647, "EventIFANotificationBody"),
            new System.Data.SqlClient.SqlParameter("@EventIFASubscriberID", System.Data.SqlDbType.Int, 4, "EventIFASubscriberID"),
            new System.Data.SqlClient.SqlParameter("@EventIFASubscriberNotificationAddress", System.Data.SqlDbType.NVarChar, 250, "EventIFASubscriberNotificationAddress"),
            new System.Data.SqlClient.SqlParameter("@_EventMessagesStatusID", System.Data.SqlDbType.Int, 4, "_EventMessagesStatusID"),
            new System.Data.SqlClient.SqlParameter("@Original_EventIFANotificationID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EventIFANotificationID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@EventIFANotificationID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EventIFANotificationID", System.Data.DataRowVersion.Original, null)});
            // 
            // cmdEventIFANotificationsDelete
            // 
            this.cmdEventIFANotificationsDelete.CommandText = resources.GetString("cmdEventIFANotificationsDelete.CommandText");
            this.cmdEventIFANotificationsDelete.Connection = this.Con;
            this.cmdEventIFANotificationsDelete.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_EventIFANotificationID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EventIFANotificationID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_EventMessageID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EventMessageID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_EventIFASubscriberID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EventIFASubscriberID", System.Data.DataRowVersion.Original, null)});
            // 
            // daEventIFANotifications
            // 
            this.daEventIFANotifications.DeleteCommand = this.cmdEventIFANotificationsDelete;
            this.daEventIFANotifications.InsertCommand = this.cmdEventIFANotificationsInsert;
            this.daEventIFANotifications.SelectCommand = this.cmdEventIFANotificationsSelect;
            this.daEventIFANotifications.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "EventIFANotifications", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("EventIFANotificationID", "EventIFANotificationID"),
                        new System.Data.Common.DataColumnMapping("EventMessageID", "EventMessageID"),
                        new System.Data.Common.DataColumnMapping("EventIFANotificationBody", "EventIFANotificationBody"),
                        new System.Data.Common.DataColumnMapping("EventIFASubscriberID", "EventIFASubscriberID")})});
            this.daEventIFANotifications.UpdateCommand = this.cmdEventIFANotificationsUpdate;
            // 
            // cmdEventMessagesStatusSetpsSelect
            // 
            this.cmdEventMessagesStatusSetpsSelect.CommandText = resources.GetString("cmdEventMessagesStatusSetpsSelect.CommandText");
            this.cmdEventMessagesStatusSetpsSelect.Connection = this.Con;
            this.cmdEventMessagesStatusSetpsSelect.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@_EventTypeID", System.Data.SqlDbType.Int, 4, "_EventTypeID")});
            // 
            // daEventMessagesStatusSetps
            // 
            this.daEventMessagesStatusSetps.SelectCommand = this.cmdEventMessagesStatusSetpsSelect;
            this.daEventMessagesStatusSetps.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "_EventMessagesStatusSetps", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("_EventMessagesStatusSetpID", "_EventMessagesStatusSetpID"),
                        new System.Data.Common.DataColumnMapping("_EventTypeID", "_EventTypeID"),
                        new System.Data.Common.DataColumnMapping("_EventMessagesStatusID", "_EventMessagesStatusID"),
                        new System.Data.Common.DataColumnMapping("IsAllChildStatusRequired", "IsAllChildStatusRequired"),
                        new System.Data.Common.DataColumnMapping("CollateralEventType", "CollateralEventType"),
                        new System.Data.Common.DataColumnMapping("CollateralEventBody", "CollateralEventBody"),
                        new System.Data.Common.DataColumnMapping("IsChangeable", "IsChangeable")})});
            // 
            // cmdEventMessageSelect
            // 
            this.cmdEventMessageSelect.CommandText = resources.GetString("cmdEventMessageSelect.CommandText");
            this.cmdEventMessageSelect.Connection = this.Con;
            this.cmdEventMessageSelect.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@EventMessageID", System.Data.SqlDbType.Int, 4, "EventMessageID")});
            // 
            // cmdcmdEventMessageInsert
            // 
            this.cmdcmdEventMessageInsert.CommandText = resources.GetString("cmdcmdEventMessageInsert.CommandText");
            this.cmdcmdEventMessageInsert.Connection = this.Con;
            this.cmdcmdEventMessageInsert.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@_EventTypeID", System.Data.SqlDbType.Int, 0, "_EventTypeID"),
            new System.Data.SqlClient.SqlParameter("@CreationDate", System.Data.SqlDbType.DateTime, 0, "CreationDate"),
            new System.Data.SqlClient.SqlParameter("@MessageBody", System.Data.SqlDbType.NVarChar, 0, "MessageBody"),
            new System.Data.SqlClient.SqlParameter("@MarketID", System.Data.SqlDbType.SmallInt, 0, "MarketID"),
            new System.Data.SqlClient.SqlParameter("@CompanyCode", System.Data.SqlDbType.Char, 0, "CompanyCode"),
            new System.Data.SqlClient.SqlParameter("@ExpiryDate", System.Data.SqlDbType.DateTime, 0, "ExpiryDate"),
            new System.Data.SqlClient.SqlParameter("@_EventMessagesStatusID", System.Data.SqlDbType.Int, 0, "_EventMessagesStatusID")});
            // 
            // cmdcmdEventMessageUpdate
            // 
            this.cmdcmdEventMessageUpdate.CommandText = resources.GetString("cmdcmdEventMessageUpdate.CommandText");
            this.cmdcmdEventMessageUpdate.Connection = this.Con;
            this.cmdcmdEventMessageUpdate.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@_EventTypeID", System.Data.SqlDbType.Int, 4, "_EventTypeID"),
            new System.Data.SqlClient.SqlParameter("@CreationDate", System.Data.SqlDbType.DateTime, 8, "CreationDate"),
            new System.Data.SqlClient.SqlParameter("@MessageBody", System.Data.SqlDbType.NVarChar, 2147483647, "MessageBody"),
            new System.Data.SqlClient.SqlParameter("@MarketID", System.Data.SqlDbType.SmallInt, 2, "MarketID"),
            new System.Data.SqlClient.SqlParameter("@CompanyCode", System.Data.SqlDbType.Char, 3, "CompanyCode"),
            new System.Data.SqlClient.SqlParameter("@ExpiryDate", System.Data.SqlDbType.DateTime, 8, "ExpiryDate"),
            new System.Data.SqlClient.SqlParameter("@_EventMessagesStatusID", System.Data.SqlDbType.Int, 4, "_EventMessagesStatusID"),
            new System.Data.SqlClient.SqlParameter("@Original_EventMessageID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EventMessageID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@EventMessageID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EventMessageID", System.Data.DataRowVersion.Original, null)});
            // 
            // cmdEventMessageDelete
            // 
            this.cmdEventMessageDelete.CommandText = resources.GetString("cmdEventMessageDelete.CommandText");
            this.cmdEventMessageDelete.Connection = this.Con;
            this.cmdEventMessageDelete.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_EventMessageID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EventMessageID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original__EventTypeID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "_EventTypeID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_CreationDate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CreationDate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_CreationDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CreationDate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_MarketID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "MarketID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_MarketID", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "MarketID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_CompanyCode", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CompanyCode", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_CompanyCode", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CompanyCode", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ExpiryDate", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ExpiryDate", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ExpiryDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ExpiryDate", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull__EventMessagesStatusID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "_EventMessagesStatusID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original__EventMessagesStatusID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "_EventMessagesStatusID", System.Data.DataRowVersion.Original, null)});
            // 
            // daGetEventMessage
            // 
            this.daGetEventMessage.DeleteCommand = this.cmdEventMessageDelete;
            this.daGetEventMessage.InsertCommand = this.cmdcmdEventMessageInsert;
            this.daGetEventMessage.SelectCommand = this.cmdEventMessageSelect;
            this.daGetEventMessage.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "EventMessages", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("EventMessageID", "EventMessageID"),
                        new System.Data.Common.DataColumnMapping("_EventTypeID", "_EventTypeID"),
                        new System.Data.Common.DataColumnMapping("CreationDate", "CreationDate"),
                        new System.Data.Common.DataColumnMapping("MessageBody", "MessageBody"),
                        new System.Data.Common.DataColumnMapping("MarketID", "MarketID"),
                        new System.Data.Common.DataColumnMapping("CompanyCode", "CompanyCode"),
                        new System.Data.Common.DataColumnMapping("ExpiryDate", "ExpiryDate"),
                        new System.Data.Common.DataColumnMapping("_EventMessagesStatusID", "_EventMessagesStatusID")})});
            this.daGetEventMessage.UpdateCommand = this.cmdcmdEventMessageUpdate;
            // 
            // cmdMSMQEventTypesSelect
            // 
            this.cmdMSMQEventTypesSelect.CommandText = "SELECT        MSMQEventID, MSMQName, MSMQLabel, _EventTypeID, IsDefault\r\nFROM    " +
    "        MSMQEventTypes\r\nWHERE        (_EventTypeID = @_EventTypeID)";
            this.cmdMSMQEventTypesSelect.Connection = this.Con;
            this.cmdMSMQEventTypesSelect.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@_EventTypeID", System.Data.SqlDbType.Int, 4, "_EventTypeID")});
            // 
            // daMSMQEventTypes
            // 
            this.daMSMQEventTypes.SelectCommand = this.cmdMSMQEventTypesSelect;
            this.daMSMQEventTypes.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "MSMQEventTypes", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("MSMQEventID", "MSMQEventID"),
                        new System.Data.Common.DataColumnMapping("MSMQName", "MSMQName"),
                        new System.Data.Common.DataColumnMapping("MSMQLabel", "MSMQLabel"),
                        new System.Data.Common.DataColumnMapping("_EventTypeID", "_EventTypeID"),
                        new System.Data.Common.DataColumnMapping("IsDefault", "IsDefault")})});
            // 
            // cmdEventMessageIFANotificationsSelect
            // 
            this.cmdEventMessageIFANotificationsSelect.CommandText = resources.GetString("cmdEventMessageIFANotificationsSelect.CommandText");
            this.cmdEventMessageIFANotificationsSelect.Connection = this.Con;
            this.cmdEventMessageIFANotificationsSelect.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@EventMessageID", System.Data.SqlDbType.Int, 4, "EventMessageID")});
            // 
            // daEventMessageIFANotifications
            // 
            this.daEventMessageIFANotifications.SelectCommand = this.cmdEventMessageIFANotificationsSelect;
            this.daEventMessageIFANotifications.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "EventIFANotifications", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("EventIFANotificationID", "EventIFANotificationID"),
                        new System.Data.Common.DataColumnMapping("EventMessageID", "EventMessageID"),
                        new System.Data.Common.DataColumnMapping("EventIFANotificationBody", "EventIFANotificationBody"),
                        new System.Data.Common.DataColumnMapping("EventIFANotificationTitle", "EventIFANotificationTitle"),
                        new System.Data.Common.DataColumnMapping("EventIFASubscriberID", "EventIFASubscriberID"),
                        new System.Data.Common.DataColumnMapping("EventIFASubscriberNotificationAddress", "EventIFASubscriberNotificationAddress"),
                        new System.Data.Common.DataColumnMapping("_EventMessagesStatusID", "_EventMessagesStatusID")})});
            // 
            // daEventMessageIFAActionNotifications
            // 
            this.daEventMessageIFAActionNotifications.SelectCommand = this.cmdEventMessageIFAActionNotificationsSelect;
            this.daEventMessageIFAActionNotifications.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "EventIFANotifications", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("EventIFANotificationID", "EventIFANotificationID"),
                        new System.Data.Common.DataColumnMapping("EventMessageID", "EventMessageID"),
                        new System.Data.Common.DataColumnMapping("EventIFANotificationBody", "EventIFANotificationBody"),
                        new System.Data.Common.DataColumnMapping("EventIFANotificationTitle", "EventIFANotificationTitle"),
                        new System.Data.Common.DataColumnMapping("EventIFASubscriberID", "EventIFASubscriberID"),
                        new System.Data.Common.DataColumnMapping("EventIFASubscriberNotificationAddress", "EventIFASubscriberNotificationAddress"),
                        new System.Data.Common.DataColumnMapping("_EventMessagesStatusID", "_EventMessagesStatusID")})});
            // 
            // cmdEventMessageIFAActionNotificationsSelect
            // 
            this.cmdEventMessageIFAActionNotificationsSelect.CommandText = resources.GetString("cmdEventMessageIFAActionNotificationsSelect.CommandText");
            this.cmdEventMessageIFAActionNotificationsSelect.Connection = this.Con;
            this.cmdEventMessageIFAActionNotificationsSelect.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@EventMessageID", System.Data.SqlDbType.Int, 4, "EventMessageID"),
            new System.Data.SqlClient.SqlParameter("@_EventMessagesStatusSetpID", System.Data.SqlDbType.Int, 4, "_EventMessagesStatusSetpID")});
            // 
            // cmdGetUserIFANotificationsSelect
            // 
            this.cmdGetUserIFANotificationsSelect.CommandText = resources.GetString("cmdGetUserIFANotificationsSelect.CommandText");
            this.cmdGetUserIFANotificationsSelect.Connection = this.Con;
            this.cmdGetUserIFANotificationsSelect.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@EventIFASubscriberNotificationAddress", System.Data.SqlDbType.NVarChar, 250, "EventIFASubscriberNotificationAddress")});
            // 
            // daGetUserIFANotifications
            // 
            this.daGetUserIFANotifications.SelectCommand = this.cmdGetUserIFANotificationsSelect;
            this.daGetUserIFANotifications.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "EventIFANotifications", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("EventIFANotificationID", "EventIFANotificationID"),
                        new System.Data.Common.DataColumnMapping("EventMessageID", "EventMessageID"),
                        new System.Data.Common.DataColumnMapping("EventIFANotificationBody", "EventIFANotificationBody"),
                        new System.Data.Common.DataColumnMapping("EventIFANotificationTitle", "EventIFANotificationTitle"),
                        new System.Data.Common.DataColumnMapping("EventIFASubscriberID", "EventIFASubscriberID"),
                        new System.Data.Common.DataColumnMapping("EventIFASubscriberNotificationAddress", "EventIFASubscriberNotificationAddress"),
                        new System.Data.Common.DataColumnMapping("_EventMessagesStatusID", "_EventMessagesStatusID"),
                        new System.Data.Common.DataColumnMapping("ParentEventMessageID", "ParentEventMessageID"),
                        new System.Data.Common.DataColumnMapping("_EventMessagesStatusSetpID", "_EventMessagesStatusSetpID"),
                        new System.Data.Common.DataColumnMapping("ExpiryDate", "ExpiryDate"),
                        new System.Data.Common.DataColumnMapping("EventMessageStatus", "EventMessageStatus")})});

        }

        #endregion

        private System.Data.SqlClient.SqlCommand cmdGetEventSubscribersSelect;
        private System.Data.SqlClient.SqlConnection Con;
        private System.Data.SqlClient.SqlDataAdapter daGetEventSubscribers;
        private System.Data.SqlClient.SqlCommand cmdGetEventSubscriberContactsSelect;
        private System.Data.SqlClient.SqlDataAdapter daGetEventSubscriberContacts;
        private System.Data.SqlClient.SqlCommand cmdGetEventkeyValueSelect;
        private System.Data.SqlClient.SqlDataAdapter daGetEventkeyValue;
        private System.Data.SqlClient.SqlCommand cmdGetEventNotificationMessagesSelect;
        private System.Data.SqlClient.SqlDataAdapter daGetEventNotificationMessages;
        private System.Data.SqlClient.SqlCommand cmdGetEventMessageParentSelect;
        private System.Data.SqlClient.SqlDataAdapter daGetEventMessageParent;
        private System.Data.SqlClient.SqlCommand cmdEventIFANotificationsSelect;
        private System.Data.SqlClient.SqlCommand cmdEventIFANotificationsInsert;
        private System.Data.SqlClient.SqlCommand cmdEventIFANotificationsUpdate;
        private System.Data.SqlClient.SqlCommand cmdEventIFANotificationsDelete;
        private System.Data.SqlClient.SqlDataAdapter daEventIFANotifications;
        private System.Data.SqlClient.SqlCommand cmdEventMessagesStatusSetpsSelect;
        private System.Data.SqlClient.SqlDataAdapter daEventMessagesStatusSetps;
        private System.Data.SqlClient.SqlCommand cmdEventMessageSelect;
        private System.Data.SqlClient.SqlCommand cmdcmdEventMessageInsert;
        private System.Data.SqlClient.SqlCommand cmdcmdEventMessageUpdate;
        private System.Data.SqlClient.SqlCommand cmdEventMessageDelete;
        private System.Data.SqlClient.SqlDataAdapter daGetEventMessage;
        private System.Data.SqlClient.SqlCommand cmdMSMQEventTypesSelect;
        private System.Data.SqlClient.SqlDataAdapter daMSMQEventTypes;
        private System.Data.SqlClient.SqlCommand cmdEventMessageIFANotificationsSelect;
        private System.Data.SqlClient.SqlDataAdapter daEventMessageIFANotifications;
        private System.Data.SqlClient.SqlDataAdapter daEventMessageIFAActionNotifications;
        private System.Data.SqlClient.SqlCommand cmdEventMessageIFAActionNotificationsSelect;
        private System.Data.SqlClient.SqlCommand cmdGetUserIFANotificationsSelect;
        private System.Data.SqlClient.SqlDataAdapter daGetUserIFANotifications;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using EFG.OPS.NotificationEngineService.Host.Properties;

namespace EFG.OPS.NotificationEngineService.Host
{
    public partial class NotificationEngineServiceHost : ServiceBase
    {
        #region -- Local Varaibles --
        private ServiceHost m_host;
        string sSource = "Notification Engine Log";
        #endregion

        #region -- Constructor (s) --

        public NotificationEngineServiceHost()
        {
            InitializeComponent();
        }

        #endregion

        #region -- Private Methods --
        public void SendMail(bool async, string from, string to, string cc, string Subject, string Body)
        {
            SmtpClient client = new SmtpClient();
            MailAddress From = new MailAddress(from);
            MailMessage message = new MailMessage();

            message.From = From;

            if (to.Contains(","))
            {
                string[] col = to.Split(',');
                foreach (string item in col)
                    message.To.Add(item);
            }
            else
                message.To.Add(to);

            if (cc.Contains(","))
            {
                string[] col = cc.Split(',');
                foreach (string item in col)
                    message.CC.Add(item);
            }
            else
                message.CC.Add(cc);

            message.IsBodyHtml = true;
            message.Body = Body;
            message.Subject = Subject;
            if (async)
            {
                client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
                client.SendAsync(message, message.Body);
            }
            else
                client.Send(message);

        }

        private void client_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
                System.Diagnostics.EventLog.WriteEntry(sSource, string.Format("Failed To Send message with content : {0} \n {1}", e.UserState.ToString(), e.Error.ToString()), EventLogEntryType.Error);
            else
                System.Diagnostics.EventLog.WriteEntry(sSource, "Credit Service Mail Success.");
        }

        private void m_host_Opened(object sender, EventArgs e)
        {
            StringBuilder state = new StringBuilder();
            foreach (var item in m_host.Description.Endpoints)
            {
                state.Append(item.Address.ToString());
            }
            System.Diagnostics.EventLog.WriteEntry(sSource, string.Format(Resources.NotificationEngineStarted, state.ToString()));
            SendMail(true, ConfigurationManager.AppSettings["MailFrom"], ConfigurationManager.AppSettings["MailTo"], ConfigurationManager.AppSettings["MailCC"],
                Resources.MessageSubject, string.Format(Resources.MessageBody, string.Format(Resources.NotificationEngineStarted, state.ToString())));
        }

        void m_host_UnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
        {
            System.Diagnostics.EventLog.WriteEntry(sSource, string.Format(Resources.NotificationEngineUnknownMessage, e.Message.ToString()), EventLogEntryType.Warning);
            SendMail(true, ConfigurationManager.AppSettings["MailFrom"], ConfigurationManager.AppSettings["MailTo"], ConfigurationManager.AppSettings["MailCC"],
                Resources.MessageSubject, string.Format(Resources.MessageBody, string.Format(Resources.NotificationEngineUnknownMessage, e.Message.ToString())));
        }

        void m_host_Faulted(object sender, EventArgs e)
        {
            System.Diagnostics.EventLog.WriteEntry(sSource, Resources.NotificationEngineFaulted);
            SendMail(true, ConfigurationManager.AppSettings["MailFrom"], ConfigurationManager.AppSettings["MailTo"], ConfigurationManager.AppSettings["MailCC"],
                Resources.MessageSubject, string.Format(Resources.MessageBody, Resources.NotificationEngineFaulted));
        }

        void m_host_Closed(object sender, EventArgs e)
        {
            System.Diagnostics.EventLog.WriteEntry(sSource, Resources.NotificationEngineStopped);
            SendMail(false, ConfigurationManager.AppSettings["MailFrom"], ConfigurationManager.AppSettings["MailTo"], ConfigurationManager.AppSettings["MailCC"],
               Resources.MessageSubject, string.Format(Resources.MessageBody, Resources.NotificationEngineStopped));
        }
        #endregion

        #region -- Protected Methods --

        protected override void OnStart(string[] args)
        {
            try
            {

                if (!EventLog.SourceExists(sSource))
                    EventLog.CreateEventSource(sSource, "Notification Engine Log");

                m_host = new ServiceHost(typeof(EFG.OPS.NotificationEngineService.Service.NotificationEngineService));
                m_host.Opened += new EventHandler(m_host_Opened);
                m_host.Closed += new EventHandler(m_host_Closed);
                m_host.Faulted += new EventHandler(m_host_Faulted);
                m_host.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(m_host_UnknownMessageReceived);
                m_host.Open();

            }
            catch (Exception exp)
            {
                System.Diagnostics.EventLog.WriteEntry(sSource, string.Format(Resources.NotificationEngineStartError, exp.ToString()), EventLogEntryType.Error);
            }
        }

        protected override void OnStop()
        {
            try
            {
                if (m_host != null)
                    m_host.Close();

            }
            catch (Exception exp)
            {
                System.Diagnostics.EventLog.WriteEntry(sSource, string.Format(Resources.NotificationEngineStopError, exp.ToString()), EventLogEntryType.Error);
            }
        }

        #endregion
    }
}

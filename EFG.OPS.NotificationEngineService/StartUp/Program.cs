using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace StartUp
{
  public  class Program
    {
      
        static void Main(string[] args)
        {
            try
            {



                ServiceHost m_host;
                m_host = new ServiceHost(typeof(EFG.OPS.NotificationEngineService.Service.NotificationEngineService));
                //m_host.Opened += new EventHandler(m_host_Opened);
                //m_host.Closed += new EventHandler(m_host_Closed);
                //m_host.Faulted += new EventHandler(m_host_Faulted);
               // m_host.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(m_host_UnknownMessageReceived);
                m_host.Open();
                Console.WriteLine("Service running successfully.");
                Console.ReadLine();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                //System.Diagnostics.EventLog.WriteEntry(sSource, string.Format(Resources.NotificationEngineStartError, exp.ToString()), EventLogEntryType.Error);

                Console.ReadLine();
            }
        }
    }
}

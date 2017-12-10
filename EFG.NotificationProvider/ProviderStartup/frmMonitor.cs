using MonitorStorage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProviderStartUp
{
    public partial class frmMonitor : Form

    {
        private dsMonitor m_dsMonitor;
        public frmMonitor(dsMonitor dsmonitor)
        {
            InitializeComponent();


            m_dsMonitor = dsmonitor;//view notificaion msgs and subscriptions.

            ultraGridActiveSubscriptions.DataSource = m_dsMonitor.Subscription_Items;
            ultraGridSentNotificaions.DataSource = m_dsMonitor.SentNotificaions;
            ultraGridReceivedNotificaions.DataSource = m_dsMonitor.ReceivedNotificaions;
        }

        private void frmMonitor_Load(object sender, EventArgs e)
        {

        }
    }
}

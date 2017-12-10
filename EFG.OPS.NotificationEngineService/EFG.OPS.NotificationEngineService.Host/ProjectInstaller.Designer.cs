namespace EFG.OPS.NotificationEngineService.Host
{
    partial class ProjectInstaller
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
            this.NotificationEngineServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            this.NotificationEngineProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            // 
            // NotificationEngineServiceInstaller
            // 
            this.NotificationEngineServiceInstaller.Description = "Provides methods that hadles Notification Engine Service";
            this.NotificationEngineServiceInstaller.DisplayName = "EFG.OPS.NotificationEngineService";
            this.NotificationEngineServiceInstaller.ServiceName = "NotificationEngineServiceHost";
            // 
            // NotificationEngineProcessInstaller
            // 
            this.NotificationEngineProcessInstaller.Password = null;
            this.NotificationEngineProcessInstaller.Username = null;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.NotificationEngineServiceInstaller,
            this.NotificationEngineProcessInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceInstaller NotificationEngineServiceInstaller;
        private System.ServiceProcess.ServiceProcessInstaller NotificationEngineProcessInstaller;
    }
}
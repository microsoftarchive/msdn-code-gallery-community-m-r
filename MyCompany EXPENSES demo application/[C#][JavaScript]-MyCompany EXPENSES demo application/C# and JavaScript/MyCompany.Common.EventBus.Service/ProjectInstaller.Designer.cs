namespace MyCompany.Common.EventBus.Service
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
            this.EventsServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.EventsServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // EventsServiceProcessInstaller
            // 
            this.EventsServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.NetworkService;
            this.EventsServiceProcessInstaller.Password = null;
            this.EventsServiceProcessInstaller.Username = null;
            // 
            // EventsServiceInstaller
            // 
            this.EventsServiceInstaller.Description = "MyCompany Event Service";
            this.EventsServiceInstaller.ServiceName = "MyCompany Event Service";
            this.EventsServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.EventsServiceProcessInstaller,
            this.EventsServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller EventsServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller EventsServiceInstaller;
    }
}
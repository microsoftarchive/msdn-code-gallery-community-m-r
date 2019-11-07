namespace MyCompany.Expenses.Service.Host
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
            this.ExpensesHostProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ExpensesHostServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // ExpensesHostProcessInstaller
            // 
            this.ExpensesHostProcessInstaller.Account = System.ServiceProcess.ServiceAccount.NetworkService;
            this.ExpensesHostProcessInstaller.Password = null;
            this.ExpensesHostProcessInstaller.Username = null;
            // 
            // ExpensesHostServiceInstaller
            // 
            this.ExpensesHostServiceInstaller.Description = "MyCompany Expenses Host";
            this.ExpensesHostServiceInstaller.ServiceName = "MyCompany Expenses Host";
            this.ExpensesHostServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ExpensesHostProcessInstaller,
            this.ExpensesHostServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ExpensesHostProcessInstaller;
        private System.ServiceProcess.ServiceInstaller ExpensesHostServiceInstaller;
    }
}
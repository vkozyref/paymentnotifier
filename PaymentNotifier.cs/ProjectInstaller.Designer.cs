namespace PaymentNotifier
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
            this.PaymentNotifierServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.PaymentNotifierServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // PaymentNotifierServiceProcessInstaller
            // 
            this.PaymentNotifierServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.PaymentNotifierServiceProcessInstaller.Password = null;
            this.PaymentNotifierServiceProcessInstaller.Username = null;
            // 
            // PaymentNotifierServiceInstaller
            // 
            this.PaymentNotifierServiceInstaller.ServiceName = "PaymentNotifierService";
            this.PaymentNotifierServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.PaymentNotifierServiceProcessInstaller,
            this.PaymentNotifierServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller PaymentNotifierServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller PaymentNotifierServiceInstaller;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft_Office_365_Service_Health_Watcher {
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer {
        public ProjectInstaller() {
            InitializeComponent();
        }

        private void ServiceProcessInstaller_AfterInstall(object sender, InstallEventArgs e) {

        }

        private void ServiceInstaller_AfterInstall(object sender, InstallEventArgs e) {

        }
    }
}

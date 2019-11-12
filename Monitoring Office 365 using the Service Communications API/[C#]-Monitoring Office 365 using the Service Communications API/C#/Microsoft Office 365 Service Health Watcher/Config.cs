using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Microsoft_Office_365_Service_Health_Watcher {
    public class AppConfig : ConfigurationSection {
        [ConfigurationProperty("ServiceURL")]
        public string ServiceURL {
            get { return this["ServiceURL"] as string; }
            set { this["ServiceURL"] = value; }
        }

        [ConfigurationProperty("DomainNames")]
        public string DomainNames {
            get { return this["DomainNames"] as string; }
            set { this["DomainNames"] = value; }
        }

        [ConfigurationProperty("UserName")]
        public string UserName {
            get { return this["UserName"] as string; }
            set { this["UserName"] = value; }
        }

        [ConfigurationProperty("Password")]
        public string Password {
            get { return this["Password"] as string; }
            set { this["Password"] = value; }
        }

        [ConfigurationProperty("PastDays")]
        public string PastDays {
            get { return this["PastDays"] as string; }
            set { this["PastDays"] = value; }
        }

        [ConfigurationProperty("IsDebug")]
        public string IsDebug {
            get { return this["IsDebug"] as string; }
            set { this["IsDebug"] = value; }
        }

        [ConfigurationProperty("PollInterval")]
        public string PollInterval {
            get { return this["PollInterval"] as string; }
            set { this["PollInterval"] = value; }
        }

        [ConfigurationProperty("FreshnessThresholdDays")]
        public string FreshnessThresholdDays {
            get { return this["FreshnessThresholdDays"] as string; }
            set { this["FreshnessThresholdDays"] = value; }
        }

        [ConfigurationProperty("AlertMessageCenterEvents")]
        public string AlertMessageCenterEvents {
            get { return this["AlertMessageCenterEvents"] as string; }
            set { this["AlertMessageCenterEvents"] = value; }
        }

        [ConfigurationProperty("AlertPlannedMaintenanceEvents")]
        public string AlertPlannedMaintenanceEvents {
            get { return this["AlertPlannedMaintenanceEvents"] as string; }
            set { this["AlertPlannedMaintenanceEvents"] = value; }
        }

        [ConfigurationProperty("AlertServiceIncidentEvents")]
        public string AlertServiceIncidentEvents {
            get { return this["AlertServiceIncidentEvents"] as string; }
            set { this["AlertServiceIncidentEvents"] = value; }
        }
    }
}

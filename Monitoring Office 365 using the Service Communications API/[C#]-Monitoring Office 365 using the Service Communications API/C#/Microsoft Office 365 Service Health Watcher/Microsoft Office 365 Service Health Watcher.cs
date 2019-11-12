using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Net;
using System.IO;
using System.Threading;
using System.Security;
using Microsoft.Exchange.ServiceStatus.TenantCommunications.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml;
using Microsoft.Win32;

namespace Microsoft_Office_365_Service_Health_Watcher {
    public partial class O365ServiceHealthWatcher : ServiceBase {
        #region VARIABLES
        //Initial instantiation of some variables
        static string serviceUrl = "";
        static string domainName = "";
        static string userId = "";
        static string password = "";
        static string cookie = "";
        static bool isDebug = false;
        static int pastDays = 0;
        static double PollInterval = 0;
        static double freshnessThresholdDays = 0;
        static bool isFirstPollFlag = false;
        static bool EventOnServiceIncidentEvents = false;
        static bool EventOnPlannedMaintenanceEvents = false;
        static bool EventOnMessageCenterEvents = false;

        //Registry related variables
        RegistryKey BaseRegKey;
        RegistryKey TrackingKey;
        RegistryKey MessageTrackingKey;
        RegistryKey MessageEventSubKey;
        RegistryKey maintenanceTrackingKey;
        RegistryKey maintenanceEventSubKey;
        RegistryKey incidentTrackingKey;
        RegistryKey incidentEventSubKey;

        //Defines the Windows event IDs used by the service
        static int NetNewServiceIncidentEventID = 1001;
        static int StatusChangedServiceIncidentEventID = 1002;
        static int NoStatusChangedServiceIncidentEventID = 1003;
        static int NetNewPlannedMaintenanceEventID = 2001;
        static int StatusChangedPlannedMaintenanceEventID = 2002;
        static int NoStatusChangedPlannedMaintenanceEventID = 2003;
        static int NetNewMessageCenterStayInformedEventID = 3001;
        static int NetNewMessageCenterPlanForChangeEventID = 3002;
        static int NetNewMessageCenterPreventOrFixIssuesEventID = 3003;
        static int ServiceStartedEventID = 4001;
        static int ServiceStoppedEventID = 4002;
        static int UnsuccessfulConnectionAttemptEventID = 4003;
        static int SuccessfulConnectNoNewEventsEventID = 4004;
        //static int UnSuccessfulQueryEventID = 4005;
        static int UnhandledExceptionEventID = 4006;
        static int DebugEventID = 4007;
        static int DeleteRegistryKeyEventID = 4008;
        static int IncompleteServiceConfigEventID = 4009;
        static int ProcessingEventsStartedEventID = 4010;
        static int ProcessingEventsCompletedEventID = 4011;

        //Defines the registry paths used by the service
        static string RegTrackingPath = "SOFTWARE\\Microsoft\\Microsoft Office 365 Service Health Watcher";

        //Variables that make up the API call formats - do not modify
        private const string RegisterMethod = "Register";
        private const string RegistrationCookieProperty = "\"RegistrationCookie\":";
        private const string GetServiceInfoForTenantDomains = "GetServiceInformationForTenantDomains";
        private const string GetEventsForTenantDomainsMethod = "GetEventsForTenantDomains";
        private const string RegisterRequestData = "\"userName\":\"{0}\",\"password\":\"{1}\"";
        private const string GetServiceInfoForTenantDomainsRequestData = "\"lastCookie\":\"{0}\",\"tenantDomains\":[{1}],\"locale\":\"{2}\"";
        private const string GetEventsForTenantDomainsRequestData = "\"lastCookie\":\"{0}\",\"preferredEventTypes\":[{1}],\"tenantDomains\":[{2}],\"locale\":\"{3}\",\"pastDays\":\"{4}\"";
        private const string GetServiceInfoMethod = "GetServiceInformation";
        private const string GetEventsMethod = "GetEvents";
        private const string GetServiceInfoRequestData = "\"lastCookie\":\"{0}\",\"locale\":\"{1}\"";
        private const string GetEventsRequestData = "\"lastCookie\":\"{0}\",\"preferredEventTypes\":[{1}],\"locale\":\"{2}\",\"pastDays\":\"{3}\"";

        //Timer object that controls the query interval
        private System.Timers.Timer timer; 

        #endregion

        public O365ServiceHealthWatcher() {
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            double interval;
            //Load the app config settings
            AppConfig section = ConfigurationManager.GetSection("ConfigSettings") as AppConfig;
            userId = section.UserName;
            password = section.Password;
            serviceUrl = section.ServiceURL;
            domainName = section.DomainNames;
            isDebug = (section.IsDebug == "1");
            Int32.TryParse(section.PastDays, out pastDays);
            string[] domainList = { domainName };
            PollInterval = Convert.ToDouble(section.PollInterval); //How often (minutes) we should poll the SHD API
            freshnessThresholdDays = Convert.ToDouble(section.FreshnessThresholdDays); //Determines how long we should assume the data in the registry is 'fresh'
            EventOnMessageCenterEvents = (section.AlertMessageCenterEvents == "1");
            EventOnPlannedMaintenanceEvents = (section.AlertPlannedMaintenanceEvents == "1");
            EventOnServiceIncidentEvents = (section.AlertServiceIncidentEvents == "1");

            try {
                interval = System.Double.Parse(args[0]);
                interval = Math.Max(1000, interval);
            } catch {
                interval = PollInterval * 1000;
            }

            //Quick status message
            string message = "The Microsoft Office 365 Service Health Watcher has been started.";
            EventLog.WriteEntry(message, EventLogEntryType.Information, ServiceStartedEventID);

            //Create a timer and have it fire after 5 seconds to do an initial poll
            timer = new System.Timers.Timer();
            timer.Interval = 5000;
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(ProcessEvents);
            timer.Enabled = true;

            //Sleep for 6 seconds
            Thread.Sleep(6000);

            //After the first run, reset the interval to the specified queryInterval in the app.config
            timer.Interval = interval;
        }

        //Deletes the registry subkeys associated with a given event type i.e. deletes all Message Center events, or all Service Incidents, etc
        private void CleanseRegistrySubKeys(string RegistryPath) {
            string message = "";
            if (isDebug) {
                message = string.Format("DEBUG: Entering the CleanseRegistrySubKeys routine with a parameter value for RegistryPath of {0}", RegistryPath);
                EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID);
            }
            RegistryKey BaseRegKey = Registry.LocalMachine;
            RegistryKey TrackingKey = BaseRegKey.OpenSubKey(RegistryPath,true);
            if (TrackingKey.SubKeyCount >= 1) {
                message = string.Format("WARN: {0} stale registry keys were discovered under the tracking path at {1}", TrackingKey.SubKeyCount.ToString(), RegistryPath);
                EventLog.WriteEntry(message, EventLogEntryType.Warning, DeleteRegistryKeyEventID);
                string[] SubKeyNames = TrackingKey.GetSubKeyNames();
                foreach (string SubKeyName in SubKeyNames) {
                    message = string.Format("WARN: Deleting the {0} key under {1}", SubKeyName, RegistryPath);
                    EventLog.WriteEntry(message, EventLogEntryType.Warning, DeleteRegistryKeyEventID);
                    TrackingKey.DeleteSubKey(SubKeyName);
                }
            } else {
                if (isDebug) {
                    message = string.Format("INFO: No stale registry keys were discovered under the tracking path at {0}", RegistryPath);
                    EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID);
                }
            }
        }

        private void ProcessEvents(object sender, ElapsedEventArgs e) {
            string message = "";
            if (isDebug) {
                message = string.Format("DEBUG: Entering ProcessEvents routine...");
                EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID);
            }

            message = string.Format("Office 365 service health polling has started...");
            EventLog.WriteEntry(message, EventLogEntryType.Information, ProcessingEventsStartedEventID);
            //try {
            //Set a timestamp for the current polling cycle
            DateTime CurrentPollTime = DateTime.Now;

            //Determine some runtime environment variables
            string CurrentWorkingFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            CurrentWorkingFolder = CurrentWorkingFolder.Replace("file:\\", "");
            string ServiceConfigFileName = CurrentWorkingFolder + "\\Microsoft Office 365 Service Health Watcher.exe.config";

            if (userId == "" || password == "" || domainName == "") {
                message = string.Format("Unable to discover all the required service configuration information. Please ensure a value for UserName, Password and DomainNames have been defined in the service configuration file at {0}. Service health polling will not continue until the file is updated and the service restarted.", ServiceConfigFileName);
                EventLog.WriteEntry(message, EventLogEntryType.Warning, IncompleteServiceConfigEventID);
                return;
            }

            //Get the last poll time stored in the registry
            DateTime? RegPollTime = GetLastPollTime();
            if (RegPollTime == null) {
                message = string.Format("ERROR: There was apparently an error reading the registry tracking key at {0}. Registry access is required, service health polling will not continue.");
                EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID); //Should probably change the eventID used here...will not for now given that would be a change to the currently 'released' behavior
                return;
            } else {
                //Calculate the staleness threshold            
                DateTime LastPollTime = (DateTime)RegPollTime;
                DateTime stalePollTime = DateTime.Now.AddDays(-freshnessThresholdDays);
                isFirstPollFlag = IsFirstPoll(LastPollTime, stalePollTime);
                if (isDebug) {
                    message = string.Format("DEBUG: IsFirstPoll check returned a value of: {0}", isFirstPollFlag.ToString());
                    EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID);
                }

                if (isFirstPollFlag) { //We are executing under a first poll experience...which means any data that may be tracked in the registry is stale so we need to perform a little clean up
                      
                    //Check each tracking key for each message type and if it returns a subkey count then delete all subkeys
                    CleanseRegistrySubKeys(RegTrackingPath + "\\MessageCenterMessages");
                    CleanseRegistrySubKeys(RegTrackingPath + "\\PlannedMaintenance");
                    CleanseRegistrySubKeys(RegTrackingPath + "\\ServiceIncidents");

                }

                //Register an SHD connection
                try {
                    RegistrationInfo registrationInfo = RegisterForSHDAccess(userId, password);
                    cookie = registrationInfo.RegistrationCookie;
                } catch (Exception ex) {
                    message = string.Format("Error: There was a problem connecting to the Office 365 Service Health Dashboard (SHD) API. The connection will be attempted again during the next polling cycle, further processing during this cycle will cease. The connection error details are below.\nMessage:\n{0}\nTrace:\n{1}", ex.Message, ex.StackTrace);
                    EventLog.WriteEntry(message, EventLogEntryType.Error, UnsuccessfulConnectionAttemptEventID);
                    return;
                }

                // Get Event Data
                EventInfo eventInfo = GetEvents();

                //Open a handle to the registry tracking key
                try {
                    BaseRegKey = Registry.LocalMachine;
                    TrackingKey = BaseRegKey.OpenSubKey(RegTrackingPath);
                } catch (Exception ex) {
                    message = string.Format("Error: There was a problem opening the registry key at {0}. Service health processing will be attempted again during the next polling cycle. The error details are below.\nMessage:\n{1}\nTrace:\n{2}", RegTrackingPath, ex.Message, ex.StackTrace);
                    EventLog.WriteEntry(message, EventLogEntryType.Error, UnhandledExceptionEventID);
                    return;
                }
                //Now that we have event data, let's loop through what came back and compare it to the Registry tracked data, if any, and drop events
                if (eventInfo != null) {
                    foreach (Event eventObject in eventInfo.Events) {

                        string MessageType ="";
                        string previousEventState = "";
                        string eventAffectedServicesExpanded = "";
                        string eventAffectedFeaturesExpanded = "";
                        bool isEventInRegistry = false;
                        bool hasTrackedEventChangedState = false;

                        if (eventObject is MessageCenterMessage) { MessageType = "Message Center"; };
                        if (eventObject is PlannedMaintenance) { MessageType = "Planned Maintenance"; };
                        if (eventObject is Incident) { MessageType = "Service Incident";};

                        //Let's extract the common event level properties we want to track
                        string EventID = eventObject.Id;
                        DateTime EventStartTime = eventObject.StartTime;
                        DateTime? EventEndTime = eventObject.EndTime;
                        DateTime EventLastUpdated = eventObject.LastUpdatedTime;
                        string EventStatus = eventObject.Status;
                        string EventTitle = eventObject.Title;

                        //Process each event type a little differently
                        switch (MessageType) {
                            #region MESSAGECENTEREVENTS
                            case "Message Center":
                                //Initial test to see if we are tracking an event of the same ID
                                MessageCenterMessage messageCenterObject = (MessageCenterMessage) eventObject;

                                try {
                                    MessageTrackingKey = TrackingKey.OpenSubKey("MessageCenterMessages", true);
                                    MessageEventSubKey = MessageTrackingKey.OpenSubKey(eventObject.Id);
                                } catch (Exception ex) {
                                    message = string.Format("Error: There was a problem opening the registry, the details are below.\nMessage:\n{1}\nTrace:\n{1}", ex.Message, ex.StackTrace);
                                    EventLog.WriteEntry(message, EventLogEntryType.Error, UnhandledExceptionEventID);
                                }

                                if (MessageEventSubKey == null) { //We did not find a registry key tracking this event...it must be net new
                                    isEventInRegistry = false;
                                } else { //We are tracking something with this same event ID
                                    isEventInRegistry = true;
                                }

                                //Build up the message string used in the body of the Windows Event Log entry
                                message = string.Format("A new event was detected with the following properties:\n");
                                message += string.Format("Record Type: {0}\n", MessageType);
                                message += string.Format("Event ID: {0}\n",eventObject.Id);
                                message += string.Format("Title: {0}\n", eventObject.Title.Trim());
                                //MessageCenter specific properties
                                message += string.Format("Category: {0}\n", messageCenterObject.Category);
                                message += string.Format("ActionType: {0}\n", messageCenterObject.ActionType);
                                //ActionRequiredByDate isn't always applicable
                                if (messageCenterObject.ActionRequiredByDate == null || messageCenterObject.ActionRequiredByDate.ToString() == "") {
                                    message += string.Format("Action Required By Date: N/A\n");
                                } else {
                                    message += string.Format("Action Required By Date: {0}\n", messageCenterObject.ActionRequiredByDate.ToString());
                                }

                                message += string.Format("Start Time: {0}\n", eventObject.StartTime.ToString());
                                //EndTime isn't always applicable
                                if (eventObject.EndTime == null || eventObject.EndTime.ToString() == "") {
                                    message += string.Format("End Time: N/A");
                                } else {
                                    message += string.Format("End Time: {0}\n", eventObject.EndTime.ToString());
                                }
                                message += string.Format("Last Updated Time: {0}\n", eventObject.LastUpdatedTime.ToString());

                                //Let's extract those messages from the embedded event object
                                if (eventObject.Messages != null && eventObject.Messages.Length > 0) {
                                    message += string.Format("Event Messages:\n");
                                    foreach (Message msg in eventObject.Messages) {
                                        message += string.Format("{0} - {1}\n", msg.PublishedTime, msg.MessageText);
                                    }
                                }

                                //Drop an event log entry only if we want to see them for this event type
                                if (EventOnMessageCenterEvents) {
                                    if (!isEventInRegistry) { //We only event once for Message Center events so if it's tracked in the registry then we simply skip writing the Windows Event Log entry
                                        //Drop a Windows Event Log entry depending on the category of Message Center event
                                        switch (messageCenterObject.Category.ToLower()) {
                                            case "plan for change":
                                                EventLog.WriteEntry(message, EventLogEntryType.Information, NetNewMessageCenterPlanForChangeEventID);
                                                break;
                                            case "prevent or fix issues":
                                                EventLog.WriteEntry(message, EventLogEntryType.Warning, NetNewMessageCenterPreventOrFixIssuesEventID);
                                                break;
                                            case "stay informed":
                                                EventLog.WriteEntry(message, EventLogEntryType.Information, NetNewMessageCenterStayInformedEventID);
                                                break;
                                        }
                                    }
                                }

                                //Finally we write back the new WS sourced data to the registry so we have fresh data for the next poll
                                WriteRegEntryForMessage(RegTrackingPath, eventObject);
                                break;
                            #endregion
                            #region PLANNEDMAINTENANCEEVENTS
                            case "Planned Maintenance":
                                //Initial test to see if we are tracking an event of the same ID
                                PlannedMaintenance maintenanceMessage = (PlannedMaintenance) eventObject;
                                PlannedMaintenanceStatus eventMaintenanceStatus = maintenanceMessage.PlannedMaintenanceStatus;
                                    
                                //Extract the Affected Service info...comes back as a list of strings so we should treat it as such even though it should be just a single service string...
                                string[] eventAffectedServcies = maintenanceMessage.AffectedServices;
                                foreach (string eventAffectedService in eventAffectedServcies) { //right now this will return an extra comma on the end of the string
                                    eventAffectedServicesExpanded += eventAffectedService + ",";
                                }

                                //We'll be left with a trailing comma so let's clean it up
                                eventAffectedServicesExpanded = eventAffectedServicesExpanded.Remove(eventAffectedServicesExpanded.Length - 1);

                                try {
                                    maintenanceTrackingKey = TrackingKey.OpenSubKey("PlannedMaintenance", true);
                                    maintenanceEventSubKey = maintenanceTrackingKey.OpenSubKey(eventObject.Id);
                                } catch (Exception ex) {
                                    message = string.Format("Error: There was a problem opening the registry, the details are below.\nMessage:\n{0}\nTrace:\n{1}", ex.Message, ex.StackTrace);
                                    EventLog.WriteEntry(message, EventLogEntryType.Error, UnhandledExceptionEventID);
                                }

                                if (maintenanceEventSubKey == null) { //We did not find a registry key tracking this event...it must be net new
                                    isEventInRegistry = false;
                                } else { //We are tracking something with this same event ID
                                    isEventInRegistry = true;
                                        
                                    //Now we check to see if our tracked event has a different 'state' than that which the WS is reporting
                                    string regEventStatus = (string) maintenanceEventSubKey.GetValue("Status");
                                    if (regEventStatus.ToLower() != eventObject.Status.ToLower()) { //We don't have the same state
                                        hasTrackedEventChangedState = true;
                                        previousEventState = regEventStatus;
                                    } else {
                                        hasTrackedEventChangedState = false;
                                    }
                                }

                                //Build up the message string used in the body of the Windows Event Log entry
                                if (isEventInRegistry && !hasTrackedEventChangedState) { //If this is simply the same event we just drop a simple event that we found it
                                    message = string.Format("A Planned Maintenance event titled '{0}' was detected but has not changed state since the last service poll.",eventObject.Title);
                                    EventLog.WriteEntry(message, EventLogEntryType.Information, NoStatusChangedPlannedMaintenanceEventID);
                                } else {
                                    if (!isEventInRegistry) {
                                        message = string.Format("A new event was detected with the following properties:\n");
                                    } else if (isEventInRegistry && hasTrackedEventChangedState) {
                                        message = string.Format("The following event was detected to have changed state since the last poll.\n");
                                    }
                                    message += string.Format("Record Type: {0}\n", MessageType);
                                    message += string.Format("Event ID: {0}\n", eventObject.Id);
                                    message += string.Format("Title: {0}\n", eventObject.Title.Trim());
                                    //For Planned Maintenance events we care about status changes
                                    if (!isEventInRegistry) {
                                        message += string.Format("Status: {0}\n", eventObject.Status);
                                    } else if (isEventInRegistry && hasTrackedEventChangedState) {
                                        message += string.Format("New Status: {0}\n", eventObject.Status);
                                        message += string.Format("Previous Status: {0}\n", previousEventState);
                                    }

                                    //Fill in the rest of the Windows Event Log message body
                                    message += string.Format("Start Time: {0}\n", eventObject.StartTime.ToString());
                                    //EndTime isn't always applicable
                                    if (eventObject.EndTime == null || eventObject.EndTime.ToString() == "") {
                                        message += string.Format("End Time: N/A\n");
                                    } else {
                                        message += string.Format("End Time: {0}\n", eventObject.EndTime.ToString());
                                    }
                                    message += string.Format("Last Updated Time: {0}\n", eventObject.LastUpdatedTime.ToString());

                                    //Let's extract those messages from the embedded event object
                                    if (eventObject.Messages != null && eventObject.Messages.Length > 0) {
                                        message += string.Format("Event Messages:\n");
                                        foreach (Message msg in eventObject.Messages) {
                                            message += string.Format("{0} - {1}\n", msg.PublishedTime, msg.MessageText);
                                        }
                                    }

                                    //Drop an event log entry only if we want to see them for this event type
                                    if (EventOnPlannedMaintenanceEvents) {
                                        //Drop a Windows Event Log entry depending on whether or not there has been a state change with the event
                                        if (!isEventInRegistry) {
                                            EventLog.WriteEntry(message, EventLogEntryType.Warning, NetNewPlannedMaintenanceEventID);
                                        } else if (isEventInRegistry && hasTrackedEventChangedState) {
                                            EventLog.WriteEntry(message, EventLogEntryType.Warning, StatusChangedPlannedMaintenanceEventID);
                                        }
                                    }
                                }

                                //Finally we write back the new WS sourced data to the registry so we have fresh data for the next poll
                                WriteRegEntryForMessage(RegTrackingPath, eventObject);
                                break;
                            #endregion
                            #region SERVICEINCIDENTEVENTS
                            case "Service Incident":
                                //Initial test to see if we are tracking an event of the same ID
                                Incident serviceIncident = (Incident) eventObject;
                                ServiceHealthStatus[] eventAffectedServicesHealthStatusList = serviceIncident.AffectedServiceHealthStatus;

                                //Extract the Affected Service and Feature info...they come back as a list of strings so we should treat them as such even though it should be just a single string instance in each case...
                                foreach (ServiceHealthStatus serviceHealthStatusEntry in eventAffectedServicesHealthStatusList) {
                                    eventAffectedServicesExpanded += string.Format("{0},", serviceHealthStatusEntry.ServiceName);
                                    ServiceFeatureHealthStatus[] featureStatus = serviceHealthStatusEntry.ServiceFeatureStatus;
                                    foreach (ServiceFeatureHealthStatus featureHealthStatusEntry in featureStatus) {
                                        eventAffectedFeaturesExpanded += string.Format("{0},", featureHealthStatusEntry.FeatureName);
                                    }
                                }

                                //We'll be left with a trailing comma so let's clean it up
                                eventAffectedServicesExpanded = eventAffectedServicesExpanded.Remove(eventAffectedServicesExpanded.Length - 1);
                                eventAffectedFeaturesExpanded = eventAffectedFeaturesExpanded.Remove(eventAffectedFeaturesExpanded.Length - 1);

                                try {
                                    incidentTrackingKey = TrackingKey.OpenSubKey("ServiceIncidents", true);
                                    incidentEventSubKey = incidentTrackingKey.OpenSubKey(eventObject.Id);
                                } catch (Exception ex) {
                                    message = string.Format("Error: There was a problem opening the registry, the details are below.\nMessage:\n{0}\nTrace:\n{1}", ex.Message, ex.StackTrace);
                                    EventLog.WriteEntry(message, EventLogEntryType.Error, UnhandledExceptionEventID);
                                }

                                if (incidentEventSubKey == null) { //We did not find a registry key tracking this event...it must be net new
                                    isEventInRegistry = false;
                                } else { //We are tracking something with this same event ID
                                    isEventInRegistry = true;
                                        
                                    //Now we check to see if our tracked event has a different 'state' than that which the WS is reporting
                                    string regEventStatus = (string) incidentEventSubKey.GetValue("Status");
                                    if (regEventStatus.ToLower() != eventObject.Status.ToLower()) { //We don't have the same state
                                        hasTrackedEventChangedState = true;
                                        previousEventState = regEventStatus;
                                    } else {
                                        hasTrackedEventChangedState = false;
                                    }
                                }

                                //Build up the message string used in the body of the Windows Event Log entry
                                if (isEventInRegistry && !hasTrackedEventChangedState) { //If this is simply the same event we just drop a simple event that we found it
                                    message = string.Format("A Service Incident event titled '{0}' was detected but has not changed state since the last service poll.",eventObject.Title);
                                    EventLog.WriteEntry(message, EventLogEntryType.Information, NoStatusChangedServiceIncidentEventID);
                                } else {
                                    if (!isEventInRegistry) {
                                        message = string.Format("A new event was detected with the following properties:\n");
                                    } else if (isEventInRegistry && hasTrackedEventChangedState) {
                                        message = string.Format("The following event was detected to have changed state since the last poll.\n");
                                    }
                                    message += string.Format("Record Type: {0}\n", MessageType);
                                    message += string.Format("Event ID: {0}\n", eventObject.Id);
                                    message += string.Format("Title: {0}\n", eventObject.Title.Trim());
                                    //For Service Incidents events we care about status changes
                                    if (!isEventInRegistry) {
                                        message += string.Format("Status: {0}\n", eventObject.Status);
                                    } else if (isEventInRegistry && hasTrackedEventChangedState) {
                                        message += string.Format("New Status: {0}\n", eventObject.Status);
                                        message += string.Format("Previous Status: {0}\n", previousEventState);
                                    }

                                    //Fill in the rest of the Windows Event Log message body
                                    message += string.Format("Start Time: {0}\n", eventObject.StartTime.ToString());
                                    //EndTime isn't always applicable
                                    if (eventObject.EndTime == null || eventObject.EndTime.ToString() == "") {
                                        message += string.Format("End Time: N/A\n");
                                    } else {
                                        message += string.Format("End Time: {0}\n", eventObject.EndTime.ToString());
                                    }
                                    message += string.Format("Last Updated Time: {0}\n", eventObject.LastUpdatedTime.ToString());

                                    //Let's extract those messages from the embedded event object
                                    if (eventObject.Messages != null && eventObject.Messages.Length > 0) {
                                        message += string.Format("Event Messages:\n");
                                        foreach (Message msg in eventObject.Messages) {
                                            message += string.Format("{0} - {1}\n", msg.PublishedTime, msg.MessageText);
                                        }
                                    }

                                    if (EventOnServiceIncidentEvents) {
                                        //Drop a Windows Event Log entry depending on whether or not there has been a state change with the event
                                        if (!isEventInRegistry) {
                                            EventLog.WriteEntry(message, EventLogEntryType.Error, NetNewServiceIncidentEventID);
                                        } else if (isEventInRegistry && hasTrackedEventChangedState) {
                                            EventLog.WriteEntry(message, EventLogEntryType.Warning, StatusChangedServiceIncidentEventID);
                                        }
                                    }
                                }

                                //Finally we write back the new WS sourced data to the registry so we have fresh data for the next poll
                                WriteRegEntryForMessage(RegTrackingPath, eventObject);
                                break;
                            #endregion
                            default:
                                break;
                        }
                    }
                } else {
                    message = "Successfully connected to the Microsoft Office 365 Service Health API but no new event data was returned.";
                    EventLog.WriteEntry(message, EventLogEntryType.Information, SuccessfulConnectNoNewEventsEventID);
                }
            }
            WriteLastPollTime(RegTrackingPath, CurrentPollTime);
            message = string.Format("Office 365 service health polling has completed...");
            EventLog.WriteEntry(message, EventLogEntryType.Information, ProcessingEventsCompletedEventID);
        }

        private void WriteRegEntryForMessage(string AppRegPath, Event ev) {
            string message = "";
            RegistryKey BaseRegKey = Registry.LocalMachine;
            RegistryKey TrackingKey = BaseRegKey.OpenSubKey(AppRegPath);

            //Let's extract the common event level properties we want to track
            string EventID = ev.Id;
            DateTime EventStartTime = ev.StartTime;
            DateTime? EventEndTime = ev.EndTime;
            DateTime EventLastUpdated = ev.LastUpdatedTime;
            string EventStatus = ev.Status;
            string EventTitle = ev.Title;

            //Now we grab the type specific properties to track
            if (ev is MessageCenterMessage) {
                MessageCenterMessage m = (MessageCenterMessage)ev;
                DateTime? EventActionRequiredByDate = m.ActionRequiredByDate;
                string EventActionType = m.ActionType;
                string EventCategory = m.Category;
                string EventExternalLink = m.ExternalLink;

                RegistryKey MessageTrackingKey = TrackingKey.OpenSubKey("MessageCenterMessages", true);
                RegistryKey MessageEventSubKey = MessageTrackingKey.OpenSubKey(ev.Id);

                if (MessageEventSubKey != null) { //If we already have an existing key tracking this event let's get rid of it
                    if (isDebug) {
                        message = string.Format("DEBUG: Discovered an existing event tracking key in the registry at {0}, it will be deleted to track current event state.", MessageEventSubKey.ToString());
                        EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID);
                    }
                    MessageTrackingKey.DeleteSubKey(ev.Id);
                    MessageEventSubKey = null;
                }

                if (isDebug) {
                    message = string.Format("DEBUG: Attempting to create a subkey at {0}", MessageTrackingKey.ToString());
                    EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID);
                }

                try {
                    RegistryKey messageKey = MessageTrackingKey.CreateSubKey(EventID);
                    // Write the common event properties
                    if (EventStatus == "") {
                        messageKey.SetValue("Status", "N/A");
                    } else {
                        messageKey.SetValue("Status", EventStatus);
                    }
                    messageKey.SetValue("Title", EventTitle);
                    messageKey.SetValue("StartTime", EventStartTime);
                    if (EventEndTime != null) {
                        messageKey.SetValue("EndTime", EventEndTime.ToString());
                    } else {
                        messageKey.SetValue("EndTime", "N/A");
                    }
                    messageKey.SetValue("LastUpdatedTime", EventLastUpdated);

                    //Write the message center specific properties
                    if (EventActionRequiredByDate != null) {
                        messageKey.SetValue("ActionRequiredByDate", EventActionRequiredByDate);
                    } else {
                        messageKey.SetValue("ActionRequiredByDate", "N/A");
                    }
                    messageKey.SetValue("ActionType", EventActionType);
                    messageKey.SetValue("Category", EventCategory);
                    if (EventExternalLink != null) {
                        messageKey.SetValue("ExternalLink", EventExternalLink);
                    } else {
                        messageKey.SetValue("ExternalLink", "N/A");
                    }
                } catch (Exception ex) {
                    message = string.Format("Error: Message:\n{0}\nTrace:\n{1}", ex.Message, ex.StackTrace);
                    EventLog.WriteEntry(message, EventLogEntryType.Error, UnhandledExceptionEventID);
                }
            }

            if (ev is PlannedMaintenance) {
                PlannedMaintenance maintenanceMessage = (PlannedMaintenance)ev;
                string[] EventAffectedServcies = maintenanceMessage.AffectedServices;
                string EventAffectedServicesExpanded = "";
                foreach (string eventAffectedService in EventAffectedServcies) { //right now this will return an extra comma on the end of the string
                    EventAffectedServicesExpanded += eventAffectedService + ",";
                }

                //Clean up the extra comma
                EventAffectedServicesExpanded = EventAffectedServicesExpanded.Remove(EventAffectedServicesExpanded.Length - 1);

                PlannedMaintenanceStatus EventMaintenanceStatus = maintenanceMessage.PlannedMaintenanceStatus;
                DateTime EventScheduledEndTime = maintenanceMessage.ScheduledEndTime;

                RegistryKey MaintenanceTrackingKey = TrackingKey.OpenSubKey("PlannedMaintenance", true);
                RegistryKey MaintenanceEventSubKey = MaintenanceTrackingKey.OpenSubKey(ev.Id);

                if (MaintenanceEventSubKey != null) { //If we already have an existing key tracking this event let's get rid of it
                    if (isDebug) {
                        message = string.Format("DEBUG: Discovered an existing event tracking key in the registry at {0}, it will be deleted to track current event state.", MaintenanceEventSubKey.ToString());
                        EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID);
                    }
                    MaintenanceTrackingKey.DeleteSubKey(ev.Id);
                    MaintenanceEventSubKey = null;
                }

                if (isDebug) {
                    message = string.Format("DEBUG: Attempting to create a subkey at {0}", MaintenanceTrackingKey.ToString());
                    EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID);
                }

                try {
                    RegistryKey messageKey = MaintenanceTrackingKey.CreateSubKey(EventID);
                    // Write the common event properties
                    messageKey.SetValue("Status", EventStatus);
                    messageKey.SetValue("Title", EventTitle);
                    messageKey.SetValue("StartTime", EventStartTime);
                    if (EventEndTime != null) {
                        messageKey.SetValue("EndTime", EventEndTime.ToString());
                    } else {
                        messageKey.SetValue("EndTime", "N/A");
                    }
                    messageKey.SetValue("LastUpdatedTime", EventLastUpdated);

                    //Write the planned maintenance specific properties
                    messageKey.SetValue("ScheduledEndTime", EventScheduledEndTime);
                    messageKey.SetValue("AffectedServices", EventAffectedServicesExpanded);
                } catch (Exception ex) {
                    message = string.Format("Error: Message:\n{0}\nTrace:\n{1}", ex.Message, ex.StackTrace);
                    EventLog.WriteEntry(message, EventLogEntryType.Error, UnhandledExceptionEventID);
                }
            }

            if (ev is Incident) {
                //Let's get the affected service and feature info
                Incident serviceIncident = (Incident)ev;
                ServiceHealthStatus[] EventAffectedServicesHealthStatusList = serviceIncident.AffectedServiceHealthStatus;
                string EventAffectedServicesExpanded = "";
                string EventAffectedServiceFeaturesExpanded = "";
                foreach (ServiceHealthStatus EventAffectedServiceHealthStatusEntry in EventAffectedServicesHealthStatusList) {
                    EventAffectedServicesExpanded += string.Format("{0},", EventAffectedServiceHealthStatusEntry.ServiceName);
                    ServiceFeatureHealthStatus[] featureStatusList = EventAffectedServiceHealthStatusEntry.ServiceFeatureStatus;
                    foreach (ServiceFeatureHealthStatus featureStatusEntry in featureStatusList) {
                        EventAffectedServiceFeaturesExpanded += string.Format("{0},", featureStatusEntry.FeatureName);
                    }
                }

                //Clean up the extra comma
                EventAffectedServicesExpanded = EventAffectedServicesExpanded.Remove(EventAffectedServicesExpanded.Length - 1);
                EventAffectedServiceFeaturesExpanded = EventAffectedServiceFeaturesExpanded.Remove(EventAffectedServiceFeaturesExpanded.Length - 1);

                RegistryKey IncidentTrackingKey = TrackingKey.OpenSubKey("ServiceIncidents", true);
                RegistryKey IncidentEventSubKey = IncidentTrackingKey.OpenSubKey(ev.Id);

                if (IncidentEventSubKey != null) { //If we already have an existing key tracking this event let's get rid of it
                    if (isDebug) {
                        message = string.Format("DEBUG: Discovered an existing event tracking key in the registry at {0}, it will be deleted to track current event state.", IncidentEventSubKey.ToString());
                        EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID);
                    }
                    IncidentTrackingKey.DeleteSubKey(ev.Id);
                    IncidentEventSubKey = null;
                }

                if (isDebug) {
                    message = string.Format("DEBUG: Attempting to create a subkey at {0}", IncidentTrackingKey.ToString());
                    EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID);
                }

                try {
                    RegistryKey messageKey = IncidentTrackingKey.CreateSubKey(EventID);
                    // Write the common event properties
                    messageKey.SetValue("Status", EventStatus);
                    messageKey.SetValue("Title", EventTitle);
                    messageKey.SetValue("StartTime", EventStartTime);
                    if (EventEndTime != null) {
                        messageKey.SetValue("EndTime", EventEndTime.ToString());
                    } else {
                        messageKey.SetValue("EndTime", "N/A");
                    }
                    messageKey.SetValue("LastUpdatedTime", EventLastUpdated);
                    messageKey.SetValue("AffectedServices", EventAffectedServicesExpanded);
                    messageKey.SetValue("AffectedFeatures", EventAffectedServiceFeaturesExpanded);
                } catch (Exception ex) {
                    message = string.Format("Error: Message:\n{0}\nTrace:\n{1}", ex.Message, ex.StackTrace);
                    EventLog.WriteEntry(message, EventLogEntryType.Error, UnhandledExceptionEventID);
                }
            }
        }

        private static EventInfo GetEvents(string locale = "en-US") {
            string requestUri = string.Format("{0}/{1}", serviceUrl, GetEventsMethod);
            StringBuilder jsonDomainArray = new StringBuilder();
            string requestData = "{" + string.Format(GetEventsRequestData, cookie, "0,1,2", locale, pastDays) + "}";
            return GetResponse<EventInfo>(requestUri, requestData);
        }

        private static ServiceInformation[] GetServiceInformation(string locale = "en-US") {
            string requestUri = string.Format("{0}/{1}", serviceUrl, GetServiceInfoMethod);
            string requestData = "{" + string.Format(GetServiceInfoRequestData, cookie, locale) + "}";
            return GetResponse<ServiceInformation[]>(requestUri, requestData);
        }

        private static TenantDomainEventInfo GetEventsForDomain(string[] domainList, string locale = "en-US") {
            string requestUri = string.Format("{0}/{1}", serviceUrl, GetEventsForTenantDomainsMethod);
            StringBuilder jsonDomainArray = new StringBuilder();
            if (domainList != null && domainList.Length > 0) {
                for (int i = 0; i < domainList.Length; i++) {
                    if (i > 0) {
                        jsonDomainArray.Append(',');
                    }

                    jsonDomainArray.AppendFormat("\"{0}\"", domainList[i]);
                }
            }
            string requestData = "{" + string.Format(GetEventsForTenantDomainsRequestData, cookie, "0,1,2", jsonDomainArray.ToString(), locale, pastDays) + "}";
            return GetResponse<TenantDomainEventInfo>(requestUri, requestData);
        }

        private static Dictionary<string, ServiceInformation[]> GetServiceInformationForTenantDomains(string[] domainList, string locale) {
            string requestUri = string.Format("{0}/{1}", serviceUrl, GetServiceInfoForTenantDomains);
            StringBuilder jsonDomainArray = new StringBuilder();
            if (domainList != null && domainList.Length > 0) {
                for (int i = 0; i < domainList.Length; i++) {
                    if (i > 0) {
                        jsonDomainArray.Append(',');
                    }

                    jsonDomainArray.AppendFormat("\"{0}\"", domainList[i]);
                }
            }

            string requestData = "{" + string.Format(GetServiceInfoForTenantDomainsRequestData, cookie, jsonDomainArray.ToString(), locale) + "}";
            return GetResponse<Dictionary<String, ServiceInformation[]>>(requestUri, requestData);
        }

        private static T GetResponse<T>(string requestUri, string requestData) {
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => { return true; };
            T data = default(T);

            try {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(requestUri);
                byte[] postBytes = Encoding.UTF8.GetBytes(requestData);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json";

                IAsyncResult ar = webRequest.BeginGetRequestStream(ProcessWebRequest, null);
                while (!ar.IsCompleted) {
                    Thread.SpinWait(10);
                }

                DateTime startTime = DateTime.Now;

                using (Stream requestStream = webRequest.EndGetRequestStream(ar)) {
                    requestStream.Write(postBytes, 0, postBytes.Length);
                }

                ar = webRequest.BeginGetResponse(ProcessWebResponse, null);
                while (!ar.IsCompleted) {
                    Thread.SpinWait(10);
                }

                DateTime endTime = DateTime.Now;
                TimeSpan ts = endTime - startTime;
                using (WebResponse response = webRequest.EndGetResponse(ar)) {
                    Stream responseStream = response.GetResponseStream();
                    DataContractJsonSerializer serailizer = new DataContractJsonSerializer(typeof(T));
                    data = (T)serailizer.ReadObject(responseStream);
                }
            } catch (Exception ex) {
                EventLog Log = new EventLog("Application","localhost","Microsoft Office 365 Service Health Watcher");
                string message = string.Format("Error: Message:\n{0}\nTrace:\n{1}", ex.Message, ex.StackTrace);
                Log.WriteEntry(message, EventLogEntryType.Error, 123);
            }

            return data;
        }

        private static RegistrationInfo RegisterForSHDAccess(string userName, string password) {
            string responseData = string.Empty;
            StringBuilder jsonDomainArray = new StringBuilder();
            string requestUri = string.Format("{0}/{1}", serviceUrl, RegisterMethod);
            string requestData = "{" + string.Format(RegisterRequestData, userName, password) + "}";
            RegistrationInfo info = GetResponse<RegistrationInfo>(requestUri, requestData);
            return info;
        }

        private static void ProcessWebResponse(IAsyncResult ar) {
        }

        private static void ProcessWebRequest(IAsyncResult ar) {
        }

        protected override void OnStop() {
            string message = "The Microsoft Office 365 Service Health Watcher service has been stopped.";
            EventLog.WriteEntry(message, EventLogEntryType.Information, ServiceStoppedEventID);
            timer.Dispose();
            timer = null;
        }

        private void WriteLastPollTime(string RegistryPath, DateTime LastPollTime) {
            string message = "";
            if (isDebug) {
                message = string.Format("DEBUG: Entering the WriteLastPollTime routine with a parameter of {0}", RegistryPath);
                EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID);
            }
            RegistryKey BaseRegKey = Registry.LocalMachine;
            RegistryKey TrackingKey = BaseRegKey.OpenSubKey(RegistryPath, true);
            TrackingKey.SetValue("LastPollTime", LastPollTime);
        }

        //Reads the local registry value for LastPollTime and determines if this is the first time the service has run, or if we should 'act' as if its the first run
        private bool IsFirstPoll(DateTime RegLastPollTime, DateTime StaleThreshold) {
            string message = "";
            if (isDebug) {
                message = string.Format("DEBUG: Entering the IsFirstPoll routine with the following parameters:\n\tRegLastPollTime: {0}\n\tStaleThreshold: {1}...", RegLastPollTime, StaleThreshold);
                EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID);
            }

            //Compare the data from the registry and our staleness cutoff date to determine if we need to move back to the first run experience
            int result = DateTime.Compare(StaleThreshold, RegLastPollTime);
            if (result < 0) { //The registry tracked LastPollTime is within our threshold
                if (isDebug) {
                    message = string.Format("DEBUG: It was determined that RegLastPollTime ({0}) is newer than the StaleThreshold ({1}) with a comparison result value of {2}.", RegLastPollTime, StaleThreshold, result.ToString());
                    EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID);
                }
                return false;
            } else if (result == 0) { //The timestamps are exactly the same
                if (isDebug) {
                    message = string.Format("DEBUG: It was determined that RegLastPollTime ({0}) is the same as StaleThreshold ({1}) with a comparison result value of {2}.", RegLastPollTime, StaleThreshold, result.ToString());
                    EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID);
                }
                return true;
            } else { //We are within a regular cycle
                if (isDebug) {
                    message = string.Format("DEBUG: It was determined that RegLastPollTime ({0}) is older than StaleThreshold ({1}) with a comparison result value of {2}.", RegLastPollTime, StaleThreshold, result.ToString());
                    EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID);
                }
                return true;
            }
        }

        private DateTime? GetLastPollTime() {
            string message = "";
            try {
                RegistryKey BaseRegKey = Registry.LocalMachine;
                RegistryKey TrackingKey = BaseRegKey.OpenSubKey(RegTrackingPath);
                if (TrackingKey == null) {
                    //We can't read the key
                    if (isDebug) {
                        message = string.Format("DEBUG: Apparently the key at {0} does NOT exist...", RegTrackingPath);
                        EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID);
                    }
                } else { //We can read the key
                    if (isDebug) {
                        message = string.Format("DEBUG: Found the registry key at {0}...", RegTrackingPath);
                        EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID);
                    }

                    //Grab the value for LastPollTime
                    string RegLastPollTime = (string)TrackingKey.GetValue("LastPollTime");
                    DateTime LastPollTime = Convert.ToDateTime(RegLastPollTime);
                    if (isDebug) {
                        message = string.Format("DEBUG: Last poll time from reg was : {0}", LastPollTime.ToString());
                        EventLog.WriteEntry(message, EventLogEntryType.Information, DebugEventID);
                    }
                    return LastPollTime;
                }
            } catch (Exception ex) {
                message = string.Format("Error Reading the Registry at {0}.\nMessage:\n{1}\nTrace:\n{1}", RegTrackingPath, ex.Message, ex.StackTrace);
                EventLog.WriteEntry(message, EventLogEntryType.Error, UnhandledExceptionEventID);
                return null;
            }
            return null;
        }
    }
}

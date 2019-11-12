using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Business_Layer
{
    public sealed class EventLogger
    {
        /// <summary>
        /// It writes an exception error into the windows event log
        /// </summary>
        /// <param name="ex"></param>
        public static void Logger(Exception ex, string part)
        {
            try
            {
                if (!EventLog.Exists("FTP File Sharing", "."))
                {
                    EventLog.CreateEventSource(new EventSourceCreationData("FTP File Sharing", "FTP File Sharing"));
                }

                EventLog.WriteEntry("FTP File Sharing", part + " : " + ex.Message, EventLogEntryType.Error);
            }
            catch { }
        }
    }
}

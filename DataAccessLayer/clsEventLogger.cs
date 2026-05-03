using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class clsEventLogger
    {
        public static void LogException(Exception exception, EventLogEntryType eventLogEntryType = EventLogEntryType.Error)
        {
            string sourceName = exception.Source;

            if (!EventLog.SourceExists(sourceName)) // Need Admin access
            {
                EventLog.CreateEventSource(sourceName, "Application"); 
            }

            EventLog.WriteEntry(sourceName, exception.Message, eventLogEntryType); 
        }

    }
}

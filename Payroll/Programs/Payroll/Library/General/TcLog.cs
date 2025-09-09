using System;
using System.Diagnostics;

// Harshan Nishantha
// 2015-10-06

namespace Payroll.Library.General
{
    public class TcLog
    {
        private const string EVENT_LOG_SOURCE = "Payroll";

        public static void LogUnexpectedError(string message)
        {
            EventLog.WriteEntry(EVENT_LOG_SOURCE, message, EventLogEntryType.Error);
        }
    }
}

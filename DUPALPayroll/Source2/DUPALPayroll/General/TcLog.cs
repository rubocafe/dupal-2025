using System;
using System.Diagnostics;

// Harshan Nishantha
// 2013-08-22

namespace DUPALPayroll.General
{
    public class TcLog
    {
        private const string EVENT_LOG_SOURCE = "DUPAL Payroll";

        public static void LogUnexpectedError(string message)
        {
            EventLog.WriteEntry(EVENT_LOG_SOURCE, message, EventLogEntryType.Error);
        }
    }
}

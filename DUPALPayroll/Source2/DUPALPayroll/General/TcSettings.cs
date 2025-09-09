using DUPALPayroll.Library;
using DUPALPayroll.Library.Date;
using Microsoft.Win32;
using System;

// Harshan Nishantha
// 2013-09-23

namespace DUPALPayroll.General
{
    public class TcSettings
    {
        private static TcRegistryEntry rootDirectoryEntry       = GetDefaultRootEntry("RootDirectory", RegistryValueKind.String);
        private static TcRegistryEntry workingYearMonthEntry    = GetDefaultRootEntry("WorkingYearMonth", RegistryValueKind.String);

        private static TcRegistryEntry GetDefaultRootEntry(string key, RegistryValueKind kind)
        {
            return GetDefaultEntry(null, key, kind);
        }

        private static TcRegistryEntry GetDefaultEntry(string folder, string key, RegistryValueKind kind)
        {
            string subKey = "Software\\Lucid Consulting\\DU PAL Payroll";
            if (!string.IsNullOrEmpty(folder))
            {
                subKey = string.Format("{0}\\{1}", subKey, folder);
            }

            TcRegistryEntry entry = new TcRegistryEntry(RegistryHive.CurrentUser, subKey, key, kind);

            return entry;
        }

        public static string DuPalRootDirectory
        {
            get
            {
                string value = string.Empty;

                rootDirectoryEntry.Read();
                if (rootDirectoryEntry.Exists)
                {
                    value = (string)rootDirectoryEntry.Value;
                }
                else
                {
                    value = string.Format("{0}\\{1}", Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "DU PAL Payroll");
                    DuPalRootDirectory = value;
                }

                return value;
            }

            set
            {
                WriteString(rootDirectoryEntry, value);
            }
        }

        public static TcYearMonth WorkingYearMonth
        {
            get
            {
                TcYearMonth workingYearMonth = TcYearMonth.OfLastMonth();  // Default set last month

                workingYearMonthEntry.Read();
                if (workingYearMonthEntry.Exists)
                {
                    string value = (string)workingYearMonthEntry.Value;
                    workingYearMonth.LoadFromText(value);
                }

                return workingYearMonth;
            }

            set
            {
                workingYearMonthEntry.Value = value.ToString();
                workingYearMonthEntry.Write();
            }
        }

        private static void WriteString(TcRegistryEntry entry, string value)
        {
            entry.Value = value;
            entry.Write();
        }

        private static string ReadString(TcRegistryEntry entry, string defaultValue)
        {
            string value = defaultValue;

            entry.Read();
            if (entry.Exists)
            {
                value = (string)entry.Value;
            }

            return value;
        }
    }
}

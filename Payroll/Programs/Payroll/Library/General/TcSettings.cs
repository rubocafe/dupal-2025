using Microsoft.Win32;
using Payroll.Library.Date;
using Payroll.Library.General;
using System;

// Harshan Nishantha
// 2015-10-06

namespace Payroll.General
{
    public class TcSettings
    {
        private static TcRegistryEntry rootDirectoryEntry       = GetDefaultRootEntry("RootDirectory", RegistryValueKind.String);
        private static TcRegistryEntry workingYearMonthEntry    = GetDefaultRootEntry("WorkingYearMonth", RegistryValueKind.String);
        private static TcRegistryEntry companyEntry             = GetDefaultRootEntry("Company", RegistryValueKind.String);
        private static TcRegistryEntry customerEntry            = GetDefaultRootEntry("Customer", RegistryValueKind.String);

        private static TcRegistryEntry GetDefaultRootEntry(string key, RegistryValueKind kind)
        {
            return GetDefaultEntry(null, key, kind);
        }

        private static TcRegistryEntry GetDefaultEntry(string folder, string key, RegistryValueKind kind)
        {
            string subKey = "Software\\Lucid Consulting\\Payroll";
            if (!string.IsNullOrEmpty(folder))
            {
                subKey = string.Format("{0}\\{1}", subKey, folder);
            }

            TcRegistryEntry entry = new TcRegistryEntry(RegistryHive.CurrentUser, subKey, key, kind);

            return entry;
        }

        public static string RootDirectory
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
                    value = string.Format("{0}\\{1}", Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Payroll");
                    RootDirectory = value;
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

        public static string Company
        {
            get
            {
                var company = "None";

                companyEntry.Read();
                if (companyEntry.Exists)
                {
                    company = (string)companyEntry.Value;
                }

                return company;
            }

            set
            {
                companyEntry.Value = value.ToString();
                companyEntry.Write();
            }
        }

        public static string Customer
        {
            get
            {
                var customer = "None";

                customerEntry.Read();
                if (customerEntry.Exists)
                {
                    customer = (string)customerEntry.Value;
                }

                return customer;
            }

            set
            {
                customerEntry.Value = value.ToString();
                customerEntry.Write();
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

using DUPALPayroll.General;
using DUPALPayroll.Library.Date;
using System;
using System.IO;
using System.Reflection;

// Harshan Nishantha
// 2013-09-17

namespace DUPALPayroll.UI.Common
{
    public class TcPaths
    {
        public static string CallCenterInboundId          = "Call Center Inbound";
        public static string CallCenterOutboundId         = "Call Center Outbound";
        public static string CareTakersId                 = "Care Takers";
        public static string CommissionAgentsId           = "Commission Agents";
        public static string CustomerCareId               = "Customer Care";
        public static string PremierSalesId               = "Premier Sales";
        public static string SupervisorsAndBackOfficeId   = "Supervisors And Back Office";
        public static string AuditorsId                   = "Auditors";
        public static string OfficeStaffId                = "Office Staff";

        public static string GetCompressedId(string id)
        {
            string compressedId = id.Replace(" ", "");

            return compressedId;
        }

        public static string GetMonthFolder()
        {
            string dupalRootFolder          = TcSettings.DuPalRootDirectory;
            TcYearMonth workingYearMonth    = TcSettings.WorkingYearMonth;

            string monthFolder = GetMonthFolder(dupalRootFolder, workingYearMonth);

            return monthFolder;
        }

        public static string GetConfigFilePath()
        {
            string sharedFolderPath = GetSharedFolderPath(GetMonthFolder());
            string filePath = Path.Combine(sharedFolderPath, "Configuration.xml");

            return filePath;
        }

        public static string GetMonthFolder(string dupalRootFolder, TcYearMonth workingYearMonth)
        {
            string suffix = TcPaths.GetSuffix(workingYearMonth);
            string folderPath = Path.Combine(dupalRootFolder, suffix);

            return folderPath;
        }

        public static string GetSectionFolderPath(string dupalRootFolder, TcYearMonth workingYearMonth, string id)
        {
            string monthFolder = GetMonthFolder(dupalRootFolder, workingYearMonth);
            string folderPath = Path.Combine(monthFolder, id);

            return folderPath;
        }

        public static string AppTempPath()
        {
            string tempDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DU PAL Payroll");
            Directory.CreateDirectory(tempDirectory);

            return tempDirectory;
        }

        public static string TempZipPath()
        {
            string tempZipPath = Path.Combine(AppTempPath(), "TempZip");
            Directory.CreateDirectory(tempZipPath);

            return tempZipPath;
        }

        public static FileInfo GetZipFilePathToSave(string sourceDirectoryName, TcYearMonth salaryMonthDate)
        {
            string suffix = GetSuffix(salaryMonthDate);
            string fileName = string.Format("{0}_{1}.zip", suffix, sourceDirectoryName.Replace(" ", ""));
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            return new FileInfo(filePath);
        }

        public static FileInfo GetSalarySlipsPathToSave(string rootDirectory, string sourceDirectoryName, TcYearMonth workingYearMonth)
        {
            string suffix = GetSuffix(workingYearMonth);
            string fileName = string.Format("{0}_{1}SalarySlips.pdf", suffix, sourceDirectoryName.Replace(" ", ""));
            string filePath = Path.Combine(rootDirectory, fileName);

            return new FileInfo(filePath);
        }

        public static string GetSuffix(TcYearMonth workingYearMonth)
        {
            DateTime workingYearMonthDate = workingYearMonth.ToDate();
            string suffix = string.Format("{0}-{1}", workingYearMonthDate.ToString("yyyy"), workingYearMonthDate.ToString("MM"));

            return suffix;
        }

        public static string GetDuPalLogoPath()
        {
            FileInfo exeInfo = new FileInfo(Assembly.GetExecutingAssembly().FullName);
            string path = string.Format("{0}\\Resources\\DuPalLogo.jpg", exeInfo.Directory.FullName);

            return path;
        }

        public static string GetSharedFolderPath(string monthFolder)
        {
            string path = Path.Combine(monthFolder, "Shared");

            return path;
        }
    }
}

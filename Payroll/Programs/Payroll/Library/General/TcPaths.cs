using Payroll.General;
using Payroll.Library.Date;
using System;
using System.IO;
using System.Reflection;

// Harshan Nishantha
// 2013-09-17

namespace Payroll.Library.General
{
    public class TcPaths
    {
        public static string GetCutomerSettingsFolderName()
        {
            return "Employer.Settings";
        }

        public static string GetCustomerOutputFolderName()
        {
            return "Employer.Output";
        }

        public static string GetCompressedId(string id)
        {
            string compressedId = id.Replace(" ", "");

            return compressedId;
        }

        public static string GetMonthFolder()
        {
            string rootFolder               = TcSettings.RootDirectory;
            string company                  = TcSettings.Company;
            TcYearMonth workingYearMonth    = TcSettings.WorkingYearMonth;

            string monthFolder = GetMonthFolder(rootFolder, company, workingYearMonth);

            return monthFolder;
        }

        public static string GetCustomerFolder()
        {
            string rootFolder = TcSettings.RootDirectory;
            string company = TcSettings.Company;
            TcYearMonth workingYearMonth = TcSettings.WorkingYearMonth;
            string customer = TcSettings.Customer;

            string monthFolder = GetCustomerFolderPath(rootFolder, company, workingYearMonth, customer);

            return monthFolder;
        }

        public static string GetConfigFilePath()
        {
            string sharedFolderPath = GetSharedFolderPath(GetMonthFolder());
            string filePath = Path.Combine(sharedFolderPath, "Configuration.xml");

            return filePath;
        }

        public static string GetMonthFolder(string rootFolder, string company, TcYearMonth workingYearMonth)
        {
            string suffix = TcPaths.GetSuffix(workingYearMonth);
            string folderPath = Path.Combine(rootFolder, company, suffix);

            return folderPath;
        }

        public static string GetCustomerFolderPath(string rootFolder, string company, TcYearMonth workingYearMonth, string customer)
        {
            string monthFolder = GetMonthFolder(rootFolder, company, workingYearMonth);
            string folderPath = Path.Combine(monthFolder, customer);

            return folderPath;
        }

        public static string GetBusinessFolderPath(string rootFolder, string company, TcYearMonth workingYearMonth, string customer, string business)
        {
            string monthFolder = GetMonthFolder(rootFolder, company, workingYearMonth);
            string folderPath = Path.Combine(monthFolder, customer, business);

            return folderPath;
        }

        public static string GetEmployerDataFilePath(string rootDirectory, string company, TcYearMonth workingYearMonth, string customer)
        {
            string suffix = TcPaths.GetSuffix(workingYearMonth);
            string path = Path.Combine(GetCustomerFolderPath(rootDirectory, company, workingYearMonth, customer) ,
                TcPaths.GetCutomerSettingsFolderName(), string.Format("Employer_Data.xls"));

            return path;
        }

        public static string GetLogoFilePath(string rootDirectory, string company, TcYearMonth workingYearMonth, string customer)
        {
            string suffix = TcPaths.GetSuffix(workingYearMonth);
            string path = Path.Combine(GetCustomerFolderPath(rootDirectory, company, workingYearMonth, customer),
                string.Format(TcPaths.GetCutomerSettingsFolderName()), string.Format("Logo.jpg"));

            return path;
        }

        public static string AppTempPath()
        {
            string tempDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Payroll");
            Directory.CreateDirectory(tempDirectory);

            return tempDirectory;
        }

        public static string TempZipPath()
        {
            string tempZipPath = Path.Combine(AppTempPath(), "TempZip");
            Directory.CreateDirectory(tempZipPath);

            return tempZipPath;
        }

        public static FileInfo GetZipFilePathToSave(string customer, string business, TcYearMonth salaryMonthDate)
        {
            string suffix = GetSuffix(salaryMonthDate);
            string fileName = string.Format("{0}_{1}_{2}.zip", customer, business, suffix);
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            return new FileInfo(filePath);
        }

        public static FileInfo GetSalarySlipsPathToSave(string businessDirectory, string business, TcYearMonth workingYearMonth)
        {
            string suffix = GetSuffix(workingYearMonth);
            string fileName = string.Format("{0}_SalarySlips_{1}.pdf", business, suffix);
            string filePath = Path.Combine(businessDirectory, fileName);

            return new FileInfo(filePath);
        }

        public static string GetSuffix(TcYearMonth workingYearMonth)
        {
            DateTime workingYearMonthDate = workingYearMonth.ToDate();
            string suffix = string.Format("{0}-{1}", workingYearMonthDate.ToString("yyyy"), workingYearMonthDate.ToString("MM"));

            return suffix;
        }

        public static string GetSharedFolderPath(string monthFolder)
        {
            string path = Path.Combine(monthFolder, "Shared");

            return path;
        }
    }
}

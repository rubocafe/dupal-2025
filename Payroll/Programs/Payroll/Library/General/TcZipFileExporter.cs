using Payroll.Library.Zip;
using System.IO;

// Harshan Nishantha
// 2013-09-23

namespace Payroll.Library.General
{
    public class TcZipFileExporter
    {
        public string MonthDirectoryPath { get; set; }
        public string SourceDirectoryName { get; set; }
        public string BanksAndBranchesFilePath { get; set; }
        public string Customer { get; set; }
        public string Business { get; set; }

        public TcZipFileExporter(string monthDirectoryPath, string banksAndBranchesFilePath,
            string customer, string business)
        {
            MonthDirectoryPath          = monthDirectoryPath;
            BanksAndBranchesFilePath    = banksAndBranchesFilePath;
            Customer                    = customer;
            Business                    = business;
        }

        public void Export(string targetZipFilePath)
        {
            string tempZipDirectory = TcPaths.TempZipPath();
            TcDirectory.Clear(tempZipDirectory);

            CopyBanksAndBranchesFileToTarget(tempZipDirectory);
            CopyFilesToTarget(tempZipDirectory);

            TcZip.ZipFolder(tempZipDirectory, targetZipFilePath);
            TcDirectory.Delete(tempZipDirectory);
        }

        private void CopyFilesToTarget(string tempDirectory)
        {
            var businessDirectory = Path.Combine(MonthDirectoryPath, Customer, Business);
            string targetDirectoryPath = Path.Combine(tempDirectory, Customer, Business);
            TcDirectory.Copy(businessDirectory, targetDirectoryPath, true);

            var settingsFolderName = TcPaths.GetCutomerSettingsFolderName();
            var settingsDirectory = Path.Combine(MonthDirectoryPath, Customer, settingsFolderName);
            targetDirectoryPath = Path.Combine(tempDirectory, Customer, settingsFolderName);
            TcDirectory.Copy(settingsDirectory, targetDirectoryPath, true);
        }

        private void CopyBanksAndBranchesFileToTarget(string tempDirectory)
        {
            FileInfo banksAndBranchesFile = new FileInfo(BanksAndBranchesFilePath);
            string targetDirectory = Path.Combine(tempDirectory, "Shared");
            Directory.CreateDirectory(targetDirectory);

            string targetBanksAndBranchesFile = Path.Combine(targetDirectory, banksAndBranchesFile.Name);
            banksAndBranchesFile.CopyTo(targetBanksAndBranchesFile, true);
        }
    }
}

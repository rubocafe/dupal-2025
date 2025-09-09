using DUPALPayroll.Library.Sys;
using DUPALPayroll.Library.Zip;
using System.IO;

// Harshan Nishantha
// 2013-09-23

namespace DUPALPayroll.UI.Common
{
    public class TcZipFileExporter
    {
        public string RootDirectoryPath { get; set; }
        public string SourceDirectoryName { get; set; }
        public string BanksAndBranchesFilePath { get; set; }

        public TcZipFileExporter(string rootDirectoryPath, string sourceDirectoryName, string banksAndBranchesFilePath)
        {
            RootDirectoryPath           = rootDirectoryPath;
            SourceDirectoryName         = sourceDirectoryName;
            BanksAndBranchesFilePath    = banksAndBranchesFilePath;
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
            string targetDirectoryPath = Path.Combine(tempDirectory, SourceDirectoryName);
            TcDirectory.Copy(RootDirectoryPath, targetDirectoryPath, true);
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

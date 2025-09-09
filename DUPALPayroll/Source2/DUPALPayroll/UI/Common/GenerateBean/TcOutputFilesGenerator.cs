using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.Library.Date;
using DUPALPayroll.Library.Sys;
using DUPALPayroll.UI.Common.Epf;
using DUPALPayroll.UI.Common.Etf;
using DUPALPayroll.UI.Common.PayMaster;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-12-31

namespace DUPALPayroll.UI.Common.GenerateBean
{
    public class TcOutputFilesGenerator<T> where T : TiPayMasterDestination
    {
        public string RootFolder { get; set; }
        public TcPayMasterOriginData OriginData { get; set; }
        public TcYearMonth WorkingYearMonth { get; set; }

        private string salayFilePath;
        private string salaryCsvFilePath;
        private string epfCsvFilePath;
        private string etfCsvFilePath;

        private List<string> files;
        private string message;

        private string sectionName;
        private string sectionId;

        public TcOutputFilesGenerator(string rootFolder, 
            TcPayMasterOriginData originData, 
            TcYearMonth workingYearMonth)
        {
            RootFolder      = rootFolder;
            OriginData      = originData;
            WorkingYearMonth     = workingYearMonth;

            SetPaths();

            files = new List<string>();

            files.Add(salayFilePath);
            files.Add(salaryCsvFilePath);
            if (TcVersions.IsEpfEtfSupported(WorkingYearMonth))
            {
                files.Add(epfCsvFilePath);
                files.Add(etfCsvFilePath);
            }
        }

        private void SetPaths()
        {
            DirectoryInfo rootDirectory = new DirectoryInfo(RootFolder);
            sectionName = rootDirectory.Name;
            sectionId = sectionName.Replace(" ", "");

            salayFilePath       = Path.Combine(RootFolder, "Temp.dat");
            salaryCsvFilePath   = Path.Combine(RootFolder, "Salary.csv");
            epfCsvFilePath = Path.Combine(RootFolder, string.Format("{0}Epf_{1}.csv", sectionId, WorkingYearMonth.ToString()));
            etfCsvFilePath = Path.Combine(RootFolder, string.Format("{0}Etf_{1}.csv", sectionId, WorkingYearMonth.ToString()));
        }

        public void Generate(TcBindingList<T> paymasterDataList)
        {
            message = "";

            if (FilesShouldGenerate())
            {
                GeneratePayMasterFile(paymasterDataList);
                if (TcVersions.IsEpfEtfSupported(WorkingYearMonth))
                {
                    GenerateEpfFile(paymasterDataList);
                    GenerateEtfFile(paymasterDataList);
                }

                TcMessageBox.ShowInformation(message);
            }
        }

        private void GeneratePayMasterFile(TcBindingList<T> paymasterDataList)
        {
            TcPayMasterFileGenereator<T> generator = new TcPayMasterFileGenereator<T>(OriginData);
            generator.GeneratePaymaster(paymasterDataList, salayFilePath);

            message += string.Format("File [{0}] generated\n", salayFilePath);
            if (generator.InvalidRows.Count > 0)
            {
                message += string.Format("[{0}] invalid row(s) have not been included in PayMaster file\n\n", generator.InvalidRows.Count);
            }
            else
            {
                message += "\n";
            }
        }

        private void GenerateEpfFile(TcBindingList<T> paymasterDataList)
        {
            TcEpfFile file = new TcEpfFile();
            TcEpfOriginData origin = TcEpfOriginData.Fake(WorkingYearMonth);
            foreach (T row in paymasterDataList)
            {
                TcEpfDestinationData destination = row.GetEpfDestinationData();
                TcEpfRow epfRow = TcEpfRow.GetEpfRow(origin, destination);
                file.Rows.Add(epfRow);
            }

            TcEpfCsvFileWriter writer = new TcEpfCsvFileWriter(file, epfCsvFilePath);
            writer.Write();

            message += string.Format("File [{0}] generated\n", epfCsvFilePath);
        }

        private void GenerateEtfFile(TcBindingList<T> paymasterDataList)
        {
            TcEtfFile file = new TcEtfFile();
            TcEtfDetailOriginData origin = new TcEtfDetailOriginData(WorkingYearMonth);
            foreach (T row in paymasterDataList)
            {
                TcEtfDetailDestinationData destination = row.GetEtfDestinationData();
                TcEtfDetailRow etfDetailRow = TcEtfDetailRow.GetEtfDetailRow(origin, destination);
                file.Rows.Add(etfDetailRow);
            }

            TcEtfCsvFileWriter writer = new TcEtfCsvFileWriter(file, etfCsvFilePath);
            writer.Write();

            message += string.Format("File [{0}] generated\n", etfCsvFilePath);
        }

        private bool FilesShouldGenerate()
        {
            DialogResult result = DialogResult.Yes;

            string warning = "";
            foreach (string path in files)
            {
                if (File.Exists(path))
                {
                    warning += string.Format("File [{0}] already exists\n", path);
                }
            }

            if (!string.IsNullOrEmpty(warning))
            {
                warning += "\nDo you want to re-generate above file(s)?";

                result = TcMessageBox.ShowYesNoWarning(warning);
            }

            return result == DialogResult.Yes ? true : false;
        }
    }
}

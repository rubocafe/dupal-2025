using Payroll.Library.Date;
using Payroll.Library.Epf;
using Payroll.Library.Etf;
using Payroll.Library.Payments;
using Payroll.Library.Payments.ComBank;
using Payroll.UI.Controls;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2015-11-06

namespace Payroll.Library.General
{
    public class TcOutputFilesGenerator<T> where T : TiMemberData
    {
        public string RootFolder { get; set; }
        public TcEmployerData EmployerData { get; set; }
        public TcYearMonth WorkingYearMonth { get; set; }

        private string salaryFilePath;
        private string salaryCsvFilePath;
        private string epfCsvFilePath;
        private string etfCsvFilePath;

        private bool generateEPF;
        private bool generateETF;
        private string bankName;

        private List<string> files;
        private string message;

        private string sectionName;
        private string sectionId;

        public TcOutputFilesGenerator(string rootFolder, TcEmployerData employerData, TcYearMonth workingYearMonth,
            bool generateEPF, bool generateETF, string bankName)
        {
            RootFolder          = rootFolder;
            EmployerData        = employerData;
            WorkingYearMonth    = workingYearMonth;
            this.generateEPF    = generateEPF;
            this.generateETF    = generateETF;
            this.bankName       = bankName;

            SetPaths();

            files = new List<string>();

            files.Add(salaryFilePath);
            files.Add(salaryCsvFilePath);
            if (generateEPF)
            {
                files.Add(epfCsvFilePath);
            }
            if (generateETF)
            {
                files.Add(etfCsvFilePath);
            }
        }

        private void SetPaths()
        {
            DirectoryInfo rootDirectory = new DirectoryInfo(RootFolder);
            sectionName = rootDirectory.Name;
            sectionId = sectionName;

            // 2016-11-18: BANK.dat except for COM Bank
            //
            if (EmployerData.Bank == "COM")
            {
                salaryFilePath = Path.Combine(RootFolder, "Temp.dat");
            }
            else
            {
                salaryFilePath = Path.Combine(RootFolder, string.Format("{0}.dat", EmployerData.Bank));
            }

            salaryCsvFilePath   = Path.Combine(RootFolder, "Salary.csv");
            epfCsvFilePath = Path.Combine(RootFolder, string.Format("{0}_Epf_{1}.csv", sectionId, WorkingYearMonth.ToString()));
            etfCsvFilePath = Path.Combine(RootFolder, string.Format("{0}_Etf_{1}.csv", sectionId, WorkingYearMonth.ToString()));
        }

        public void Generate(TcBindingList<T> members)
        {
            message = "";

            if (FilesShouldGenerate())
            {
                GenerateBankPaymentsFile(members);

                if (generateEPF)
                {
                    GenerateEpfFile(members);
                }

                if (generateETF)
                {
                    GenerateEtfFile(members);
                }

                TcMessageBox.ShowInformation(message);
            }
        }

        private void GenerateBankPaymentsFile(TcBindingList<T> members)
        {
            TcBankPaymentsGenerator<T> generator = new TcBankPaymentsGenerator<T>(EmployerData, members);
            generator.GeneratePaymentsFile(salaryFilePath);

            message += string.Format("File [{0}] generated\n", salaryFilePath);
            if (generator.InvalidMembers.Count > 0)
            {
                message += string.Format("[{0}] invalid row(s) have not been included in Bank Payments file\n\n", generator.InvalidMembers.Count);
            }
            else
            {
                message += "\n";
            }
        }

        private void GenerateEpfFile(TcBindingList<T> paymasterDataList)
        {
            TcEpfFile file = new TcEpfFile();
            TcEpfEmployerData employer = TcEpfEmployerData.Fake(WorkingYearMonth);
            foreach (T row in paymasterDataList)
            {
                TcEpfMemberData member = row.EpfMemberData();
                TcEpfRow epfRow = new TcEpfRow(employer, member);
                file.Rows.Add(epfRow);
            }

            TcEpfCsvFileWriter writer = new TcEpfCsvFileWriter(file, epfCsvFilePath);
            writer.Write();

            message += string.Format("File [{0}] generated\n", epfCsvFilePath);
        }

        private void GenerateEtfFile(TcBindingList<T> paymasterDataList)
        {
            TcEtfFile file = new TcEtfFile();
            TcEtfEmployerData employer = new TcEtfEmployerData(WorkingYearMonth);
            foreach (T row in paymasterDataList)
            {
                TcEtfMemberData member = row.EtfMemberData();
                TcEtfDetailRow etfDetailRow = new TcEtfDetailRow(employer, member);
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

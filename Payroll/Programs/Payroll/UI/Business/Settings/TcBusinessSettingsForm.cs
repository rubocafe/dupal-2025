using Payroll.General;
using Payroll.Library.Date;
using Payroll.Library.General;
using Payroll.Library.MetaData;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-08-26

namespace Payroll.UI.Business.Settings
{
    public partial class TcBusinessSettingsForm : Form
    {
        private TcBusinessForm master;

        public string MonthDirectoryPath { get; set; }
        public string BusinessDirectoryPath { get; set; }
        public string EmployerDataFilePath { get; set; }
        public string LogoFilePath { get; set; }
        public string MetaDataFilePath { get; set; }
        public string MasterFilePath { get; set; }
        public string BanksAndBranchesFilePath { get; set; }
        public string SalaryFilePath { get; set; }

        public TcYearMonth WorkingYearMonth { get; private set; }

        public TcBusinessSettingsForm(TcBusinessForm master)
        {
            InitializeComponent();

            statusLabel.Text = "";

            this.master = master;
            companyTextBox.Text = TcSettings.Company;
            salaryMonthDateTimePicker.Value = TcSettings.WorkingYearMonth.ToDate();
        }

        private void SetPaths()
        {
            var company = companyTextBox.Text;
            TcYearMonth yearMonth = TcYearMonth.OfDateTime(salaryMonthDateTimePicker.Value);
            string suffix = TcPaths.GetSuffix(yearMonth);

            MonthDirectoryPath      = TcPaths.GetMonthFolder(payrollDirectoryTextBox.Text, company, yearMonth);
            BusinessDirectoryPath = TcPaths.GetBusinessFolderPath(payrollDirectoryTextBox.Text, company, yearMonth, master.Customer, master.Business);
            EmployerDataFilePath = TcPaths.GetEmployerDataFilePath(payrollDirectoryTextBox.Text, company, yearMonth, master.Customer);
            LogoFilePath = TcPaths.GetLogoFilePath(payrollDirectoryTextBox.Text, company, yearMonth, master.Customer);
            MetaDataFilePath        = Path.Combine(BusinessDirectoryPath, string.Format("{0}_MetaData_{1}.xls", master.Business, suffix));
            MasterFilePath          = Path.Combine(BusinessDirectoryPath, string.Format("{0}_Master_{1}.xls", master.Business, suffix));
            BanksAndBranchesFilePath = Path.Combine(payrollDirectoryTextBox.Text, string.Format("{0}\\{1}\\Shared\\BanksAndBranches_{1}.xls", company, suffix));
            SalaryFilePath          = Path.Combine(BusinessDirectoryPath, string.Format("{0}_Salary_{1}.xls", master.Business, suffix));
            WorkingYearMonth        = yearMonth;
        }

        private void TcSettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                payrollDirectoryTextBox.Text = TcSettings.RootDirectory;
                SetPaths();
                dataDirectoryTextBox.Text = BusinessDirectoryPath;
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void loadDataButton_Click(object sender, EventArgs e)
        {
            try
            {
                master.HideOtherTabs();

                if (IsValid())
                {
                    TcMetaData metaData = new TcMetaData(EmployerDataFilePath, MetaDataFilePath);
                    metaData.Load();

                    if (master.InitializeFormsAndShowOtherTabs(metaData))
                    {
                        TcMessageBox.ShowInformation("Data loaded successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private bool IsValid()
        {
            if (!Directory.Exists(BusinessDirectoryPath))
            {
                TcMessageBox.ShowWarning(
                    string.Format("Root folder [{0}] not found. Please create root folder and data files", BusinessDirectoryPath));
                return false;
            }

            if (!File.Exists(EmployerDataFilePath))
            {
                TcMessageBox.ShowWarning(string.Format("Employer Data file [{0}] not found. Please add employer data file", EmployerDataFilePath));
                return false;
            }

            if (!File.Exists(MetaDataFilePath))
            {
                TcMessageBox.ShowWarning(string.Format("MetaData file [{0}] not found. Please add meta data file", MetaDataFilePath));
                return false;
            }

            if (!File.Exists(MasterFilePath))
            {
                TcMessageBox.ShowWarning(string.Format("Master file [{0}] not found. Please add master file", MasterFilePath));
                return false;
            }

            if (!File.Exists(BanksAndBranchesFilePath))
            {
                TcMessageBox.ShowWarning(string.Format("Banks and Branches file [{0}] not found. Please add Banks and Branches file", 
                    BanksAndBranchesFilePath));
                return false;
            }

            if (!File.Exists(SalaryFilePath))
            {
                TcMessageBox.ShowWarning(string.Format("Salary file [{0}] not found. Please add Salary file", SalaryFilePath));
                return false;
            }

            return true;
        }

        public FileInfo GetZipFilePathToSave()
        {
            return TcPaths.GetZipFilePathToSave(master.Customer, master.Business, WorkingYearMonth);
        }

        public FileInfo GetSalarySlipsPathToSave()
        {
            return TcPaths.GetSalarySlipsPathToSave(master.SettingsForm.BusinessDirectoryPath, master.Business, WorkingYearMonth);
        }

        private void openFolderButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(BusinessDirectoryPath))
                {
                    TcMessageBox.ShowWarning(string.Format("Folder [{0}] does not exist", BusinessDirectoryPath));
                }
                else
                {
                    TcDirectory.Open(BusinessDirectoryPath);
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }
    }
}

using DUPALPayroll.General;
using DUPALPayroll.Library.Date;
using DUPALPayroll.Library.Sys;
using DUPALPayroll.UI.Common;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-09-17

namespace DUPALPayroll.UI.CustomerCare.Settings
{
    public partial class TcCustomerCareSettingsForm : Form
    {
        private TcCustomerCareForm master;

        public string RootDirectoryPath { get; set; }
        public string MasterFilePath { get; set; }
        public string BanksAndBranchesFilePath { get; set; }
        public string CustomerCareSalaryFilePath { get; set; }

        public TcYearMonth WorkingYearMonth { get; private set; }

        public TcCustomerCareSettingsForm(TcCustomerCareForm master)
        {
            InitializeComponent();

            statusLabel.Text = "";

            this.master = master;
            salaryMonthDateTimePicker.Value = TcSettings.WorkingYearMonth.ToDate();
        }

        private void TcSettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                payrollDirectoryTextBox.Text = TcSettings.DuPalRootDirectory;
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
                    if (master.InitializeFormsAndShowOtherTabs())
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
            TcYearMonth yearMonth = TcYearMonth.OfDateTime(salaryMonthDateTimePicker.Value);
            string suffix = TcPaths.GetSuffix(yearMonth);

            string compressedIdentifier = TcPaths.GetCompressedId(master.Identifier);

            RootDirectoryPath = TcPaths.GetSectionFolderPath(payrollDirectoryTextBox.Text, yearMonth, master.Identifier);
            if (!Directory.Exists(RootDirectoryPath))
            {
                TcMessageBox.ShowWarning(string.Format("Root folder [{0}] not found. Please create root folder and data files", RootDirectoryPath));
                return false;
            }

            MasterFilePath = Path.Combine(RootDirectoryPath, string.Format("{0}Master_{1}.csv", compressedIdentifier, suffix));
            if (!File.Exists(MasterFilePath))
            {
                TcMessageBox.ShowWarning(string.Format("Master file [{0}] not found. Please add master file", MasterFilePath));
                return false;
            }

            BanksAndBranchesFilePath = Path.Combine(payrollDirectoryTextBox.Text, string.Format("{0}\\Shared\\BanksAndBranches_{0}.csv", suffix));
            if (!File.Exists(BanksAndBranchesFilePath))
            {
                TcMessageBox.ShowWarning(string.Format("Banks and Branches file [{0}] not found. Please add Banks and Branches file", BanksAndBranchesFilePath));
                return false;
            }

            CustomerCareSalaryFilePath = Path.Combine(RootDirectoryPath, string.Format("{0}Salary_{1}.csv", compressedIdentifier, suffix));
            if (!File.Exists(CustomerCareSalaryFilePath))
            {
                TcMessageBox.ShowWarning(string.Format("{0} Salary file [{1}] not found. Please add Commissions file", master.Identifier, CustomerCareSalaryFilePath));
                return false;
            }

            WorkingYearMonth = yearMonth;

            return true;
        }

        public FileInfo GetZipFilePathToSave()
        {
            return TcPaths.GetZipFilePathToSave(master.Identifier, WorkingYearMonth);
        }

        public FileInfo GetSalarySlipsPathToSave()
        {
            return TcPaths.GetSalarySlipsPathToSave(RootDirectoryPath, master.Identifier, WorkingYearMonth);
        }

        private void openFolderButton_Click(object sender, EventArgs e)
        {
            try
            {
                TcYearMonth yearMonth = TcYearMonth.OfDateTime(salaryMonthDateTimePicker.Value);
                string rootDirectoryPath = TcPaths.GetSectionFolderPath(payrollDirectoryTextBox.Text, yearMonth, master.Identifier);
                if (!Directory.Exists(rootDirectoryPath))
                {
                    TcMessageBox.ShowWarning(string.Format("Folder [{0}] does not exist", rootDirectoryPath));
                }
                else
                {
                    TcDirectory.Open(rootDirectoryPath);
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }
    }
}

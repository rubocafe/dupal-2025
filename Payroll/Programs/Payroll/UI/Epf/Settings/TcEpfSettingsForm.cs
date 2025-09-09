using Payroll.General;
using Payroll.Library.Date;
using Payroll.Library.General;
using Payroll.Library.MetaData;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2015-11-06

namespace Payroll.UI.Epf.Settings
{
    public partial class TcEpfSettingsForm : Form
    {
        private TcEpfControlForm master;

        public string RootDirectoryPath { get; set; }
        public string Company { get; set; }
        public string Customer { get; set; }
        public TcYearMonth WorkingYearMonth { get; private set; }
        public string CustomerDirectoryPath { get; set; }
        public string ZoneCode { get; set; }
        public string EmployerNumber { get; set; }

        public TcEmployerMetaData EmployerData { get; set; }

        public TcEpfSettingsForm(TcEpfControlForm master)
        {
            InitializeComponent();

            statusLabel.Text = "";

            this.master = master;
        }

        private void TcSettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                Company                 = TcSettings.Company;
                Customer                = TcSettings.Customer;
                WorkingYearMonth        = TcSettings.WorkingYearMonth;
                RootDirectoryPath       = TcSettings.RootDirectory;
                CustomerDirectoryPath = TcPaths.GetCustomerFolderPath(RootDirectoryPath, Company, WorkingYearMonth, Customer);

                var path = TcPaths.GetEmployerDataFilePath(RootDirectoryPath, Company, WorkingYearMonth, Customer);
                EmployerData = new TcEmployerMetaData();
                EmployerData.Load(path);

                ZoneCode = EmployerData.ZoneCode;
                EmployerNumber = EmployerData.EmployerNumber;

                salaryMonthDateTimePicker.Value     = WorkingYearMonth.ToDate();
                payrollDirectoryTextBox.Text        = RootDirectoryPath;
                dataDirectoryTextBox.Text           = CustomerDirectoryPath;
                zoneCodeTextBox.Text                = ZoneCode;
                employerNumberTextBox.Text          = EmployerNumber;
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

            if (!Directory.Exists(CustomerDirectoryPath))
            {
                TcMessageBox.ShowWarning(string.Format(
                    "Data folder [{0}] not found. Please create root folder and data files", CustomerDirectoryPath));
                return false;
            }

            string zoneCode = zoneCodeTextBox.Text;
            if (TcValidator.IsValidZoneCode(zoneCode))
            {
                zoneCode = TcFormatter.GetFormattedZoneCode(zoneCode);
            }
            else
            {
                TcMessageBox.ShowWarning("Invalid Zone Code");
                return false;
            }

            ZoneCode        = zoneCode;
            EmployerNumber  = employerNumberTextBox.Text;
            WorkingYearMonth = yearMonth;

            return true;
        }

        private void openFolderButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(CustomerDirectoryPath))
                {
                    TcMessageBox.ShowWarning(string.Format("Folder [{0}] does not exists", CustomerDirectoryPath));
                }
                else
                {
                    TcDirectory.Open(CustomerDirectoryPath);
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }
    }
}

using DUPALPayroll.General;
using DUPALPayroll.Library.Date;
using DUPALPayroll.Library.Sys;
using DUPALPayroll.UI.Common;
using DUPALPayroll.UI.Configuration.ConfigFile;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2014-01-01

namespace DUPALPayroll.UI.Etf.Settings
{
    public partial class TcEtfSettingsForm : Form
    {
        private TcEtfControlForm master;

        public string RootDirectoryPath { get; set; }
        public string ZoneCode { get; set; }
        public string EmployerNumber { get; set; }
        public TcYearMonth WorkingYearMonth { get; private set; }

        public TcEtfSettingsForm(TcEtfControlForm master)
        {
            InitializeComponent();

            statusLabel.Text = "";

            this.master = master;
        }

        private void TcSettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                salaryMonthDateTimePicker.Value = TcSettings.WorkingYearMonth.ToDate();
                payrollDirectoryTextBox.Text = TcSettings.DuPalRootDirectory;

                TcConfigurationFile configFile = new TcConfigurationFile();
                if (configFile.Configurations != null)
                {
                    zoneCodeTextBox.Text = configFile.Configurations.ZoneCode;
                    employerNumberTextBox.Text = configFile.Configurations.EmployeeNumber;
                }
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

            RootDirectoryPath = TcPaths.GetMonthFolder(payrollDirectoryTextBox.Text, yearMonth);
            if (!Directory.Exists(RootDirectoryPath))
            {
                TcMessageBox.ShowWarning(string.Format("Root folder [{0}] not found. Please create root folder and data files", RootDirectoryPath));
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
                TcYearMonth yearMonth = TcYearMonth.OfDateTime(salaryMonthDateTimePicker.Value);
                string rootDirectoryPath = TcPaths.GetMonthFolder(payrollDirectoryTextBox.Text, yearMonth);
                if (!Directory.Exists(rootDirectoryPath))
                {
                    TcMessageBox.ShowWarning(string.Format("Folder [{0}] does not exists", rootDirectoryPath));
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

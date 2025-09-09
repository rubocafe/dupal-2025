using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library.Date;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2014-01-07

namespace DUPALPayroll.UI.Configuration.Settings
{
    public partial class TcConfigurationSettingsForm : TcForm
    {
        public TcConfigurationSettingsForm()
        {
            InitializeComponent();
        }

        public void Reload()
        {
            payrollDirectoryTextBox.Text    = TcSettings.DuPalRootDirectory;
            salaryMonthDateTimePicker.Value = TcSettings.WorkingYearMonth.ToDate();

            SetButtons();
        }

        private void SetButtons()
        {
            if (IsChanged())
            {
                saveButton.Enabled      = true;
                cancelButton.Enabled    = true;
            }
            else
            {
                saveButton.Enabled      = false;
                cancelButton.Enabled    = false;
            }
        }

        public bool IsChanged()
        {
            if (payrollDirectoryTextBox.Text != TcSettings.DuPalRootDirectory)
            {
                return true;
            }

            if (salaryMonthDateTimePicker.Value != TcSettings.WorkingYearMonth.ToDate())
            {
                return true;
            }

            return false;
        }

        private bool IsValid()
        {
            string directory = payrollDirectoryTextBox.Text;
            if (!Directory.Exists(directory))
            {
                string message = string.Format("Payroll Directory [{0}] does not exist", directory);
                TcMessageBox.ShowWarning(message);
                return false;
            }

            return true;
        }

        private void Save()
        {
            if (IsValid())
            {
                TcSettings.DuPalRootDirectory   = payrollDirectoryTextBox.Text;
                TcSettings.WorkingYearMonth     = TcYearMonth.OfDateTime(salaryMonthDateTimePicker.Value);

                TcControlForm.ResestForms();
                TcControlForm.UpdateWorkingYearMonthLabel();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                Reload();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                Reload();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            try
            {
                folderBrowserDialog.SelectedPath = payrollDirectoryTextBox.Text;
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string payRollDirectoryPath = folderBrowserDialog.SelectedPath;
                    payrollDirectoryTextBox.Text = payRollDirectoryPath;

                    SetButtons();
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void salaryMonthDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        public override bool CanLeave()
        {
            if (IsChanged())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

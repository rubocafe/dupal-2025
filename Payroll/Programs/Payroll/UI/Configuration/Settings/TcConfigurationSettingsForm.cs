using Payroll.General;
using Payroll.Library.Date;
using Payroll.Library.General;
using Payroll.Library.Model;
using Payroll.UI.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2014-01-07

namespace Payroll.UI.Configuration.Settings
{
    public partial class TcConfigurationSettingsForm : TcForm
    {
        public TcConfigurationSettingsForm()
        {
            InitializeComponent();
        }

        public void Reload()
        {
            try
            {
                payrollDirectoryTextBox.Text = TcSettings.RootDirectory;
                LoadCompanies();
                salaryMonthDateTimePicker.Value = TcSettings.WorkingYearMonth.ToDate();
                //customerComboBox.SelectedItem = TcSettings.Customer;
                LoadCustomers();
                SetButtons();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void LoadCompanies()
        {
            companyComboBox.Items.Clear();

            companyComboBox.Items.Add("None");

            var yearMonth = TcYearMonth.OfDateTime(salaryMonthDateTimePicker.Value);
            var rootDirectory = payrollDirectoryTextBox.Text;
            TcPayrollFolderStructure config = new TcPayrollFolderStructure(rootDirectory);
            var companies = config.GetCompanies();
            foreach (var item in companies)
            {
                companyComboBox.Items.Add(item);
            }

            var company = TcSettings.Company;
            if (companyComboBox.Items.Contains(company))
            {
                companyComboBox.SelectedItem = company;
            }
            else
            {
                if (companyComboBox.Items.Count > 1)
                {
                    companyComboBox.SelectedItem = companyComboBox.Items[1];
                }
                else
                {
                    companyComboBox.SelectedItem = "None";
                }
            }
        }

        private void LoadCustomers()
        {
            customerComboBox.Items.Clear();

            customerComboBox.Items.Add("None");
            var company = companyComboBox.SelectedItem as string;
            var yearMonth = TcYearMonth.OfDateTime(salaryMonthDateTimePicker.Value);
            var rootDirectory = payrollDirectoryTextBox.Text;
            TcPayrollFolderStructure config = new TcPayrollFolderStructure(rootDirectory);
            var customers = config.GetCustomers(company, yearMonth.ToString());
            foreach (var item in customers)
            {
                customerComboBox.Items.Add(item);
            }

            var customer = TcSettings.Customer;
            if (customerComboBox.Items.Contains(customer))
            {
                customerComboBox.SelectedItem = customer;
            }
            else
            {
                if (customerComboBox.Items.Count > 1)
                {
                    customerComboBox.SelectedItem = customerComboBox.Items[1];
                }
                else
                {
                    customerComboBox.SelectedItem = "None";
                }
            }
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
            if (payrollDirectoryTextBox.Text != TcSettings.RootDirectory)
            {
                return true;
            }

            if (salaryMonthDateTimePicker.Value != TcSettings.WorkingYearMonth.ToDate())
            {
                return true;
            }

            var company = companyComboBox.SelectedItem as string;
            if (company != TcSettings.Company)
            {
                return true;
            }

            var customer = customerComboBox.SelectedItem as string;
            if (customer != TcSettings.Customer)
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
                TcSettings.RootDirectory    = payrollDirectoryTextBox.Text;
                TcSettings.WorkingYearMonth = TcYearMonth.OfDateTime(salaryMonthDateTimePicker.Value);
                TcSettings.Company          = companyComboBox.Text;
                TcSettings.Customer         = customerComboBox.Text;

                TcMainForm.ResestForms();
                //TcControlForm.UpdateWorkingYearMonthLabel();
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

                    LoadCompanies();
                    LoadCustomers();
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
            try
            {
                LoadCustomers();
                SetButtons();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            try
            {
                Reload();
                TcMainForm.ResestForms();

                string message = string.Format("Reloaded Successfully");
                TcMessageBox.ShowInformation(message);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void companyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadCustomers();
                SetButtons();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void customerComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

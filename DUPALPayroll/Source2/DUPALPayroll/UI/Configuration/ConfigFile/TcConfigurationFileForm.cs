using DUPALPayroll.Controls;
using DUPALPayroll.General;
using System;
using System.Windows.Forms;

// Harshan Nishantha
// 2014-01-09

namespace DUPALPayroll.UI.Configuration.ConfigFile
{
    public partial class TcConfigurationFileForm : TcForm
    {
        public TcConfigurationFile ConfigFile { get; private set; }

        public TcConfigurationFileForm()
        {
            InitializeComponent();
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

        public void Reload()
        {
            try
            {
                ConfigFile = new TcConfigurationFile();

                string message = string.Empty;
                TcTheme.DisplayHighlightedLabel(statusLabel, message);

                if (ConfigFile.IsSupported)
                {
                    EnableControls(true);
                    if (!ConfigFile.Exists)
                    {
                        message = string.Format("Configuration file not found. Press SAVE to create new one...");
                        TcTheme.DisplayErrorLabel(statusLabel, message);
                    }
                } 
                else 
                {
                    EnableControls(false);
                }

                LoadValuesToControls();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void LoadValuesToControls()
        {
            if (ConfigFile != null && ConfigFile.Configurations != null)
            {
                branchCodeTextBox.Text              = ConfigFile.Configurations.BranchCode;
                accountNameTextBox.Text             = ConfigFile.Configurations.AccountName;
                accountNumberTextBox.Text           = ConfigFile.Configurations.AccountNumber;
                zoneCodeTextBox.Text                = ConfigFile.Configurations.ZoneCode;
                employerNumberTextBox.Text          = ConfigFile.Configurations.EmployeeNumber;
            }
            else
            {
                ClearControls();
            }

            SetMainButtons();
        }

        private void SetMainButtons()
        {
            EnableControls(false);

            if (ConfigFile != null && ConfigFile.Configurations != null)
            {
                if (ConfigFile.IsSupported)
                {
                    if (ConfigFile.Exists)
                    {
                        if (IsChanged())
                        {
                            EnableControls(true);
                            editButton.Enabled = false;
                        }
                        else
                        {
                            editButton.Enabled = true;
                        }
                    }
                    else
                    {
                        saveButton.Enabled = true;
                    }
                }
            }
        }

        private void ClearControls()
        {
            branchCodeTextBox.Text      = "";
            accountNumberTextBox.Text   = "";
            accountNameTextBox.Text     = "";
            zoneCodeTextBox.Text        = "";
            employerNumberTextBox.Text  = "";
        }

        private void EnableControls(bool enable)
        {
            branchCodeTextBox.Enabled       = enable;
            accountNumberTextBox.Enabled    = enable;
            accountNameTextBox.Enabled      = enable;
            zoneCodeTextBox.Enabled         = enable;
            employerNumberTextBox.Enabled   = enable;

            editButton.Enabled      = enable;
            saveButton.Enabled      = enable;
            cancelButton.Enabled    = enable;
        }

        private bool IsChanged()
        {
            if (ConfigFile != null && ConfigFile.Configurations != null)
            {
                TcConfigurations currentConfigs = GetCurrentConfig();

                if (ConfigFile.Configurations.BranchCode != currentConfigs.BranchCode)
                {
                    return true;
                }

                if (ConfigFile.Configurations.AccountNumber != currentConfigs.AccountNumber)
                {
                    return true;
                }

                if (ConfigFile.Configurations.AccountName != currentConfigs.AccountName)
                {
                    return true;
                }

                if (ConfigFile.Configurations.ZoneCode != currentConfigs.ZoneCode)
                {
                    return true;
                }

                if (ConfigFile.Configurations.EmployeeNumber != currentConfigs.EmployeeNumber)
                {
                    return true;
                }
            }

            return false;
        }

        private TcConfigurations GetCurrentConfig()
        {
            TcConfigurations config = new TcConfigurations();

            config.BranchCode       = branchCodeTextBox.Text;
            config.AccountName      = accountNameTextBox.Text;
            config.AccountNumber    = accountNumberTextBox.Text;
            config.ZoneCode         = zoneCodeTextBox.Text;
            config.EmployeeNumber   = employerNumberTextBox.Text;

            config.Clean();
            return config;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SetMainButtons();

                if (sender.GetType() == typeof(TextBox))
                {
                    ((TextBox)sender).Focus();
                }
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                TcConfigurations currentConfig = GetCurrentConfig();

                if (currentConfig.IsValid(true))
                {
                    ConfigFile.Configurations = currentConfig;
                    ConfigFile.Write();
                    Reload();

                    TcControlForm.ResestForms();
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            try
            {
                EnableControls(true);

                editButton.Enabled      = false;
                saveButton.Enabled      = false;
                cancelButton.Enabled    = false;
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }
    }
}

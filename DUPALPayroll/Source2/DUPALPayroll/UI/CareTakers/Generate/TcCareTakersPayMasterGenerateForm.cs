using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.Library.Sys;
using DUPALPayroll.UI.CareTakers.Analyze;
using DUPALPayroll.UI.Common;
using DUPALPayroll.UI.Common.GenerateBean;
using DUPALPayroll.UI.Common.PayMaster;
using DUPALPayroll.UI.Configuration.ConfigFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-10-31

namespace DUPALPayroll.UI.CareTakers.Generate
{
    public partial class TcCareTakersPayMasterGenerateForm : Form
    {
        private TcCareTakersForm master;
        private BindingSource source = new BindingSource();

        public TcCareTakersPayMasterGenerateForm(TcCareTakersForm master)
        {
            InitializeComponent();

            this.master = master;

            dataGridView.AutoGenerateColumns = false;

            source.DataSource = new List<TcMandatoryCondition>();
            dataGridView.DataSource = source;

            generateButton.Enabled  = false;
            statusLabel.Text        = string.Empty;
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (AnalyzeCompleted())
                {
                    if (InputValid())
                    {
                        generateSaveFileDialog.InitialDirectory = master.SettingsForm.RootDirectoryPath;
                        DialogResult result = generateSaveFileDialog.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            TcPayMasterOriginData originData = GetOriginData();

                            TcPayMasterFileGenereator<TcCareTakersAnalyzedRow> generator = new TcPayMasterFileGenereator<TcCareTakersAnalyzedRow>(originData);
                            generator.GeneratePaymaster(master.AnalyzeForm.AnalyzedRows, generateSaveFileDialog.FileName);

                            string message = string.Format("File [{0}] generated successfully", generateSaveFileDialog.FileName);
                            if (generator.InvalidRows.Count > 0)
                            {
                                message += string.Format("\n[{0}] invalid row(s) have not been included in PayMaster file", generator.InvalidRows.Count);
                                TcMessageBox.ShowWarning(message);
                            }
                            else
                            {
                                TcMessageBox.ShowInformation(message);
                            }
                        }
                    }
                }
                else
                {
                    TcMessageBox.ShowInformation("Please complete the analysis to generate PayMaster file");
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private TcPayMasterOriginData GetOriginData()
        {
            TcPayMasterOriginData originData = new TcPayMasterOriginData();

            originData.OriginatingBranch        = branchCodeTextBox.Text;
            originData.OriginatingAccount       = accountNumberTextBox.Text;
            originData.OriginatingAccountName   = accountNameTextBox.Text;
            originData.Reference                = referenceTextBox.Text;

            originData.SetValueDate(valueDateDateTimePicker.Value);

            return originData;
        }

        private bool InputValid()
        {
            if (string.IsNullOrEmpty(branchCodeTextBox.Text) || !TcString.IsNumeric(branchCodeTextBox.Text))
            {
                TcMessageBox.ShowWarning("Invalid branch code. Branch code must be a number");
                branchCodeTextBox.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(accountNumberTextBox.Text) || !TcString.IsNumeric(accountNumberTextBox.Text))
            {
                TcMessageBox.ShowWarning("Invalid account number. Account number must be a number");
                accountNumberTextBox.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(accountNameTextBox.Text))
            {
                TcMessageBox.ShowWarning("Please enter an account name");
                accountNameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(referenceTextBox.Text))
            {
                TcMessageBox.ShowWarning("Please enter an reference");
                referenceTextBox.Focus();
                return false;
            }

            bool conditionsAreSatisfied = true;
            List<TcMandatoryCondition> conditions = source.DataSource as List<TcMandatoryCondition>;
            foreach (TcMandatoryCondition condition in conditions)
            {
                if (!condition.Satisfied)
                {
                    conditionsAreSatisfied = false;
                }
            }

            if (!conditionsAreSatisfied)
            {
                DialogResult result = TcMessageBox.ShowYesNoWarning("Some conditions are not satisfied. Do you still want to generate PayMaster file?");
                if (result == DialogResult.No)
                {
                    return false;
                }
            }


            return true;
        }

        private bool AnalyzeCompleted()
        {
            if (master.AnalyzeForm.AnalyzedRows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ReInitialize()
        {
            try
            {
                if (AnalyzeCompleted())
                {
                    SetMandatoryConditions();

                    SetSummaryLabel();

                    SetReferenceField();

                    generateButton.Enabled = true;
                }
                else
                {
                    ResetForm();
                    ShowRunAnalysisMessage();
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void SetSummaryLabel()
        {
            TcPayMasterOriginData originData = GetOriginData();

            TcPayMasterRowsValidator<TcCareTakersAnalyzedRow> validator =
                new TcPayMasterRowsValidator<TcCareTakersAnalyzedRow>(originData, master.AnalyzeForm.AnalyzedRows);
            validator.Validate();
            validator.SetDisplaySummaryLabel(summaryLabel);
        }

        private void ShowRunAnalysisMessage()
        {
            string message = "Please run the analysis to generate PayMaster file";
            TcTheme.DisplayErrorLabel(statusLabel, message);
            TcMessageBox.ShowWarning(message);
        }

        private void ResetForm()
        {
            source.DataSource = new List<TcMandatoryCondition>();
            generateButton.Enabled = false;
        }

        private void SetMandatoryConditions()
        {
            TcCareTakersConditionsChecker checker = new TcCareTakersConditionsChecker(master);
            List<TcMandatoryCondition> list = checker.GetList();
            source.DataSource = list;

            statusLabel.Text = string.Empty;
        }

        private void SetReferenceField()
        {
            referenceTextBox.Text = string.Format("CT {0} '{1}",
                master.SettingsForm.WorkingYearMonth.ToDate().ToString("MMM").ToUpper(), master.SettingsForm.WorkingYearMonth.ToDate().ToString("yy"));
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                dataGridView.SelectedRows[0].Selected = false;
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo path = master.SettingsForm.GetZipFilePathToSave();
                zipSaveFileDialog.InitialDirectory = path.Directory.FullName;
                zipSaveFileDialog.FileName = path.Name;
                DialogResult result = zipSaveFileDialog.ShowDialog();
                
                if (result == DialogResult.OK)
                {
                    TcTheme.DisplayInfoLabel(statusLabel, "Creating zip file...");
                    Application.DoEvents();

                    TcZipFileExporter exporter = new TcZipFileExporter(
                        master.SettingsForm.RootDirectoryPath,
                        master.Identifier, 
                        master.SettingsForm.BanksAndBranchesFilePath);

                    exporter.Export(zipSaveFileDialog.FileName);

                    statusLabel.Text = string.Empty;
                    TcMessageBox.ShowInformation(string.Format("Zip file [{0}] saved successfully", zipSaveFileDialog.FileName));
                }
            }
            catch (Exception ex)
            {
                statusLabel.Text = string.Empty;
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            TcGenerateFormHelper.dataGridView_CellFormatting(sender, e);
        }

        private void openFolderbutton_Click(object sender, EventArgs e)
        {
            try
            {
                TcDirectory.Open(master.SettingsForm.RootDirectoryPath);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void TcCareTakersPayMasterGenerateForm_Load(object sender, EventArgs e)
        {
            try
            {
                TcConfigurationFile ConfigFile = new TcConfigurationFile();

                if (ConfigFile.Configurations != null)
                {
                    branchCodeTextBox.Text = ConfigFile.Configurations.BranchCode;
                    accountNameTextBox.Text = ConfigFile.Configurations.AccountName;
                    accountNumberTextBox.Text = ConfigFile.Configurations.AccountNumber;
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }
    }
}

using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.Library.Sys;
using DUPALPayroll.UI.Common;
using DUPALPayroll.UI.Common.Epf;
using DUPALPayroll.UI.Common.GenerateBean;
using DUPALPayroll.UI.Common.PayMaster;
using DUPALPayroll.UI.Configuration.ConfigFile;
using DUPALPayroll.UI.SupervisorsAndBackOffice.Analyze;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-10-01

namespace DUPALPayroll.UI.SupervisorsAndBackOffice.Generate
{
    public partial class TcSupervisorsAndBackOfficePayMasterGenerateForm : Form
    {
        private TcSupervisorsAndBackOfficeForm master;
        private BindingSource source = new BindingSource();

        public TcSupervisorsAndBackOfficePayMasterGenerateForm(TcSupervisorsAndBackOfficeForm master)
        {
            InitializeComponent();

            this.master = master;

            dataGridView.AutoGenerateColumns = false;

            source.DataSource = new List<TcMandatoryCondition>();
            dataGridView.DataSource = source;

            generateButton.Enabled  = false;
            statusLabel.Text        = string.Empty;

            TcTheme.DisplayErrorLabel(statusLabel, "");
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (AnalyzeCompleted())
                {
                    if (InputValid())
                    {
                        TcPayMasterOriginData originData = GetOriginData();

                        TcOutputFilesGenerator<TcSupervisorsAndBackOfficeAnalyzedRow> generator
                            = new TcOutputFilesGenerator<TcSupervisorsAndBackOfficeAnalyzedRow>
                                (master.SettingsForm.RootDirectoryPath, originData, 
                                master.SettingsForm.WorkingYearMonth);

                        generator.Generate(master.AnalyzeForm.PaymasterDataList);
                    }
                }
                else
                {
                    TcMessageBox.ShowInformation("Please complete the analysis to generate files");
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
            if (master.AnalyzeForm.PaymasterDataList.Count > 0)
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

            TcPayMasterRowsValidator<TcSupervisorsAndBackOfficeAnalyzedRow> validator =
                new TcPayMasterRowsValidator<TcSupervisorsAndBackOfficeAnalyzedRow>(originData, master.AnalyzeForm.PaymasterDataList);
            validator.Validate();
            validator.SetDisplaySummaryLabel(summaryLabel);
        }

        private void ShowRunAnalysisMessage()
        {
            string message = "Please run the analysis to generate PayMaster file";
            statusLabel.Text = message;
            TcMessageBox.ShowWarning(message);
        }

        private void ResetForm()
        {
            source.DataSource = new List<TcMandatoryCondition>();
            generateButton.Enabled = false;
        }

        private void SetMandatoryConditions()
        {
            TcSalaryConditionsChecker<TcSupervisorsAndBackOfficeAnalyzedRow> checker = new TcSalaryConditionsChecker<TcSupervisorsAndBackOfficeAnalyzedRow>(
                master.AnalyzeForm.PaymasterDataList,
                master.SettingsForm.WorkingYearMonth,
                AllEmployeesInSalaryFileAreUnique());

            List<TcMandatoryCondition> list = checker.GetList();
            source.DataSource = list;

            statusLabel.Text = string.Empty;
        }

        public bool AllEmployeesInSalaryFileAreUnique()
        {
            bool unique = !(master.SalaryForm.SalaryTable.HasNICDuplicates() || master.SalaryForm.SalaryTable.HasEmployeeNumberDuplicates());

            return unique;
        }

        private void SetReferenceField()
        {
            referenceTextBox.Text = string.Format("SBO SAL {0} '{1}",
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
                    EnableControls(false);
                    TcTheme.DisplayInfoLabel(statusLabel, "Creating zip file...");
                    Application.DoEvents();

                    TcZipFileExporter exporter = new TcZipFileExporter(
                        master.SettingsForm.RootDirectoryPath,
                        master.Identifier,
                        master.SettingsForm.BanksAndBranchesFilePath);

                    exporter.Export(zipSaveFileDialog.FileName);

                    statusLabel.Text = string.Empty;
                    TcMessageBox.ShowInformation(string.Format("Zip file [{0}] saved successfully", zipSaveFileDialog.FileName));
                    EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);

                statusLabel.Text = string.Empty;
                EnableControls(true);
            }
        }

        private void salarySlipsButton_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo salarySlipsFileInfo                = master.SettingsForm.GetSalarySlipsPathToSave();
                slarySlipSaveFileDialog.InitialDirectory    = salarySlipsFileInfo.Directory.FullName;
                slarySlipSaveFileDialog.FileName            = salarySlipsFileInfo.Name;

                DialogResult result = slarySlipSaveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    EnableControls(false);
                    TcTheme.DisplayInfoLabel(statusLabel, "Creating salary slips...");
                    Application.DoEvents();

                    NonArgumentFunction function = new NonArgumentFunction(GenerateSalarySlips);
                    TcBackgroundWorker worker = new TcBackgroundWorker();
                    worker.Execute(function);

                    if (!worker.Succeed)
                    {
                        TcMessageBox.ShowError(worker.Error);
                    }

                    statusLabel.Text = string.Empty;
                    EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);

                statusLabel.Text = string.Empty;
                EnableControls(true);
            }
        }

        private bool GenerateSalarySlips()
        {
            string path = slarySlipSaveFileDialog.FileName;

            TcSupervisorsAndBackOfficeSalarySlipsCreator creator = new TcSupervisorsAndBackOfficeSalarySlipsCreator(master.SettingsForm.WorkingYearMonth);

            creator.CreateAndSave(path, master.AnalyzeForm.PaymasterDataList);
            creator.ShowFile(path);

            return true;
        }

        private void EnableControls(bool enable)
        {
            branchCodeTextBox.Enabled       = enable;
            accountNumberTextBox.Enabled    = enable;
            accountNameTextBox.Enabled      = enable;
            referenceTextBox.Enabled        = enable;
            valueDateDateTimePicker.Enabled = enable;

            generateButton.Enabled      = enable;
            exportButton.Enabled        = enable;
            salarySlipsButton.Enabled   = enable;
            openFolderbutton.Enabled    = enable;
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

        private void TcSupervisorsAndBackOfficePayMasterGenerateForm_Load(object sender, EventArgs e)
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

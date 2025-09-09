using Payroll.Library;
using Payroll.Library.General;
using Payroll.Library.MetaData;
using Payroll.Library.Payments;
using Payroll.Library.Payments.ComBank;
using Payroll.UI.Business.Analyze;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2015-11-06

namespace Payroll.UI.Business.Generate
{
    public partial class TcBusinessPaymentsForm : Form
    {
        private TcBusinessForm master;
        private TcMetaData metaData;
        private BindingSource source = new BindingSource();

        public TcBusinessPaymentsForm(TcBusinessForm master, TcMetaData metaData)
        {
            InitializeComponent();

            this.master = master;
            this.metaData = metaData;

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
                        TcEmployerData employer = GetEmployerData();

                        TcOutputFilesGenerator<TcBusinessAnalyzedRow> generator
                            = new TcOutputFilesGenerator<TcBusinessAnalyzedRow>
                                (master.SettingsForm.BusinessDirectoryPath, employer, 
                                master.SettingsForm.WorkingYearMonth, metaData.Settings.HasEPF, metaData.Settings.HasETF, metaData.EmployerData.Bank);

                        generator.Generate(master.AnalyzeForm.AnalyzedRows);
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

        private TcEmployerData GetEmployerData()
        {
            TcEmployerData employer = new TcEmployerData();

            employer.BankCode       = bankCodeTextBox.Text; // 2016/10/25
            employer.BranchCode     = branchCodeTextBox.Text;
            employer.AccountNumber  = accountNumberTextBox.Text;
            employer.AccountName    = accountNameTextBox.Text;
            employer.Remarks        = referenceTextBox.Text;
            employer.ValueDate      = valueDateDateTimePicker.Value;
            employer.Bank           = bankTextBox.Text;

            return employer;
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
                DialogResult result = TcMessageBox.ShowYesNoWarning(
                    "Some conditions are not satisfied. Do you still want to generate PayMaster file?");
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

                    SetReferenceText();

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
            TcEmployerData employerData = GetEmployerData();

            TcPaymentsValidator<TcBusinessAnalyzedRow> validator = new TcPaymentsValidator<TcBusinessAnalyzedRow>(employerData, master.AnalyzeForm.AnalyzedRows);
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
            TcBusinessConditionsChecker checker = new TcBusinessConditionsChecker(master);
            List<TcMandatoryCondition> list = checker.GetList();
            source.DataSource = list;

            statusLabel.Text = string.Empty;
        }

        private void SetReferenceText()
        {
            referenceTextBox.Text = string.Format("SALARY {0} '{1}",
                master.SettingsForm.WorkingYearMonth.ToDate().ToString("MMM").ToUpper(), 
                master.SettingsForm.WorkingYearMonth.ToDate().ToString("yy"));
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
                        master.SettingsForm.MonthDirectoryPath,
                        master.SettingsForm.BanksAndBranchesFilePath,
                        master.Customer, master.Business);

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
                TcDirectory.Open(master.SettingsForm.BusinessDirectoryPath);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void TcBusinessPayMasterGenerateForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (metaData != null)
                {
                    accountNameTextBox.Text = metaData.EmployerData.AccountName;
                    bankTextBox.Text                = metaData.EmployerData.Bank;
                    bankCodeTextBox.Text            = metaData.EmployerData.BankCode;
                    branchCodeTextBox.Text          = metaData.EmployerData.BranchCode;
                    accountNumberTextBox.Text       = metaData.EmployerData.AccountNumber;
                    valueDateDateTimePicker.Value   = DateTime.Now;
                    SetReferenceText();
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
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

        private void EnableControls(bool enable)
        {
            branchCodeTextBox.Enabled           = enable;
            accountNumberTextBox.Enabled        = enable;
            accountNameTextBox.Enabled          = enable;
            referenceTextBox.Enabled            = enable;
            valueDateDateTimePicker.Enabled     = enable;

            generateButton.Enabled      = enable;
            exportButton.Enabled        = enable;
            salarySlipsButton.Enabled   = enable;
            openFolderbutton.Enabled    = enable;
        }

        private bool GenerateSalarySlips()
        {
            string path = slarySlipSaveFileDialog.FileName;

            TcBusinessSalarySlipsCreator creator = 
                new TcBusinessSalarySlipsCreator(master.SettingsForm.WorkingYearMonth, 
                    metaData.EmployerData,  master.Business);

            creator.CreateAndSave(path, master.SettingsForm.LogoFilePath, master.AnalyzeForm.AnalyzedRows);
            creator.ShowFile(path);

            return true;
        }
    }
}

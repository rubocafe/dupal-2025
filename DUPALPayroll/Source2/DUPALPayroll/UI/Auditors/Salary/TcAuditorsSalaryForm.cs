using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.Auditors.Salary
{
    public partial class TcAuditorsSalaryForm : Form
    {
        private TcAuditorsForm master;
        private string filePath = string.Empty;

        private TcBindingList<TcAuditorsSalaryRow> all = new TcBindingList<TcAuditorsSalaryRow>();

        private BindingSource source = new BindingSource();
        private BindingSource duplicatesSource = new BindingSource();

        public bool DataLoaded { get; set; }

        public TcAuditorsSalaryTable SalaryTable { get; private set; }

        public TcAuditorsSalaryForm(TcAuditorsForm master, string filePath)
        {
            InitializeComponent();

            this.filePath           = filePath;
            this.master             = master;
            SalaryTable             = new TcAuditorsSalaryTable();
            DataLoaded              = false;

            dataGridView.AutoGenerateColumns            = false;
            duplicatesDataGridView.AutoGenerateColumns  = false;

            source.DataSource           = new TcBindingList<TcAuditorsSalaryRow>();
            duplicatesSource.DataSource = new TcBindingList<TcAuditorsSalaryRow>();

            dataGridView.DataSource             = source;
            duplicatesDataGridView.DataSource   = duplicatesSource;

            statusLabel.Text = string.Empty;

            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeAuditorsSalaryFilter>(TeAuditorsSalaryFilter.All));

            ApplyTheme();
            SetFileInfo();
        }

        private void ApplyTheme()
        {
            TcTheme.FormatGrid(dataGridView);

            TcTheme.FormatGridCurrencyDisplayColumn(basicSalaryColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(braColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(travelAllowanceColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(grossSalaryColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(epfDeductionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(netSalaryColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(travelReimbursementColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(travelIncentiveColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(totalRemunerationColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(holdColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(bankTransferAmountColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(epfContributionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(etfContributionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(payeColumn);
            TcTheme.FormatGridDateDisplayColumn(dateOfJoinColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(daysWorkedColumn);

            TcTheme.FormatGrid(duplicatesDataGridView);

            TcTheme.FormatGridCurrencyDisplayColumn(dBasicSalaryColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dBRAColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dTravelAllowanceColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dGrossSalaryColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dEPFDeductionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dNetSalaryColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dTravelReimbursementColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dTravelIncentiveColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dTotalRemunerationColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dHoldColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dBankTransferAmountColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dEPFContributionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dETFContributionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dPayeColumn);
            TcTheme.FormatGridDateDisplayColumn(dDateOfJoinColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dDaysWorkedColumn);

            if (!TcVersions.IsEpfEtfSupported(master.SettingsForm.WorkingYearMonth))
            {
                memberStatusColumn.Visible  = false;
                daysWorkedColumn.Visible    = false;

                dMemberStatusColumn.Visible = false;
                dDaysWorkedColumn.Visible   = false;
            }

            if (!TcVersions.IsPayeSupported(master.SettingsForm.WorkingYearMonth))
            {
                payeColumn.Visible = false;
                dPayeColumn.Visible = false;
            }
        }

        private void reloadButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                searchTextBox.Text = string.Empty;
                ReloadData();
                master.ReloadAnalyzeForm();

                TcMessageBox.ShowInformation("Data reloaded successfully");
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        public void ReloadData()
        {
            if (File.Exists(filePath))
            {
                duplicatesSource.DataSource = new TcBindingList<TcAuditorsSalaryRow>();

                TcAuditorsSalaryLoader loader = new TcAuditorsSalaryLoader(master.SettingsForm.WorkingYearMonth);
                all = loader.LoadFromCSV(filePath);
                
                SalaryTable = new TcAuditorsSalaryTable();
                SalaryTable.Load(all);

                all = SalaryTable.All;
                source.DataSource = all;

                SetStatus();
                

                SetFilter();
                SetFileInfo();
                DataLoaded = true;

                TcAuditorsForm.ResetMasterFormFilter = true;
            }
            else
            {
                DataLoaded = false;
                string ex = string.Format("{0} salary file [{1}] does not exist", master.Identifier, filePath);
                fileInfoLabel.Text = ex;
                throw new Exception(ex);
            }
        }

        private void SetFilter()
        {
            SetFilterData(SalaryTable.HasEmployeeNumberDuplicates(), TcEnum.GetTextForEnum<TeAuditorsSalaryFilter>(TeAuditorsSalaryFilter.Employee_Number_Duplicates));
            SetFilterData(SalaryTable.HasNICDuplicates(), TcEnum.GetTextForEnum<TeAuditorsSalaryFilter>(TeAuditorsSalaryFilter.NIC_Duplicates));

            filterComboBox.Text = TcEnum.GetTextForEnum<TeAuditorsSalaryFilter>(TeAuditorsSalaryFilter.All);
        }

        private void SetFilterData(bool add, string item)
        {
            if (add)
            {
                if (!filterComboBox.Items.Contains(item))
                {
                    filterComboBox.Items.Add(item);
                }
            }
            else
            {
                if (filterComboBox.Items.Contains(item))
                {
                    filterComboBox.Items.Remove(item);
                }
            }
        }

        private void SetFileInfo()
        {
            if (filePath == null)
            {
                fileInfoLabel.Text = "Click Load data button to load Data";
            }
            else
            {
                FileInfo fileInfo = new FileInfo(filePath);
                fileInfoLabel.Text = string.Format("File: [{0}], Last Modified Date: [{1}]", fileInfo.FullName, fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        private void filterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterAndSearch();
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                TcAuditorsSalaryRow row = source.Current as TcAuditorsSalaryRow;

                if (row != null)
                {
                    TeAuditorsSalaryFilter filter = TcEnum.GetEnumForText<TeAuditorsSalaryFilter>(TeAuditorsSalaryFilter.All, filterComboBox.Text);
                    switch (filter)
                    {
                        case TeAuditorsSalaryFilter.All:
                            duplicatesSource.DataSource = SalaryTable.GetDuplicates(row.EmployeeNumber, row.NIC);
                            break;

                        case TeAuditorsSalaryFilter.Employee_Number_Duplicates:
                            duplicatesSource.DataSource = SalaryTable.GetEmployeeNumberDuplicates(row.EmployeeNumber);
                            break;

                        case TeAuditorsSalaryFilter.NIC_Duplicates:
                            duplicatesSource.DataSource = SalaryTable.GetNICDuplicates(row.NIC);
                            break;

                        default:
                            duplicatesSource.DataSource = new TcBindingList<TcAuditorsSalaryRow>();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            FilterAndSearch();
        }

        private void FilterAndSearch()
        {
            try
            {
                TeAuditorsSalaryFilter filter = TcEnum.GetEnumForText<TeAuditorsSalaryFilter>(TeAuditorsSalaryFilter.All, filterComboBox.Text);
                switch (filter)
                {
                    case TeAuditorsSalaryFilter.All:
                        source.DataSource = all;
                        break;

                    case TeAuditorsSalaryFilter.Employee_Number_Duplicates:
                        source.DataSource = SalaryTable.GetEmployeeNumberDuplicates();
                        break;

                    case TeAuditorsSalaryFilter.NIC_Duplicates:
                        source.DataSource = SalaryTable.GetNICDuplicates();
                        break;

                    default:
                        break;
                }

                Search();
                SetStatus();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void Search()
        {
            string searchText = searchTextBox.Text.Trim();
            TcSearchHelper<TcAuditorsSalaryRow> searchHelper = new TcSearchHelper<TcAuditorsSalaryRow>();

            source.DataSource = searchHelper.Search(source, searchText);
        }

        private void SetStatus()
        {
            statusLabel.Text = string.Format("{0} record(s) found", source.Count);
            TcBindingList<TcAuditorsSalaryRow> list = source.DataSource as TcBindingList<TcAuditorsSalaryRow>;

            decimal total = 0;

            foreach (TcAuditorsSalaryRow row in list)
            {
                if (row.BankTransferAmount > 0)
                {
                    total += row.BankTransferAmount;
                }
            }

            amountsLabel.Text = string.Format("Total Bank Transfer Amount: {0}", total.ToString("N2"));
        }
    }
}

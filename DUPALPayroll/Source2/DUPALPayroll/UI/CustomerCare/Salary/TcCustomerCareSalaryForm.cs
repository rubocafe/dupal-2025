using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-09-17

namespace DUPALPayroll.UI.CustomerCare.Salary
{
    public partial class TcCustomerCareSalaryForm : Form
    {
        private TcCustomerCareForm master;
        private string filePath = string.Empty;

        private TcBindingList<TcCustomerCareSalaryRow> all = new TcBindingList<TcCustomerCareSalaryRow>();

        private BindingSource source = new BindingSource();
        private BindingSource duplicatesSource = new BindingSource();

        public bool DataLoaded { get; set; }

        public TcCustomerCareSalaryTable SalaryTable { get; private set; }

        public TcCustomerCareSalaryForm(TcCustomerCareForm master, string filePath)
        {
            InitializeComponent();

            this.filePath           = filePath;
            this.master             = master;
            SalaryTable             = new TcCustomerCareSalaryTable();
            DataLoaded              = false;

            dataGridView.AutoGenerateColumns            = false;
            duplicatesDataGridView.AutoGenerateColumns  = false;

            source.DataSource           = new TcBindingList<TcCustomerCareSalaryRow>();
            duplicatesSource.DataSource = new TcBindingList<TcCustomerCareSalaryRow>();

            dataGridView.DataSource             = source;
            duplicatesDataGridView.DataSource   = duplicatesSource;

            statusLabel.Text = string.Empty;

            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeCustomerCareSalaryFilter>(TeCustomerCareSalaryFilter.All));

            ApplyTheme();
            SetFileInfo();
        }

        private void ApplyTheme()
        {
            TcTheme.FormatGrid(dataGridView);

            TcTheme.FormatGridCurrencyDisplayColumn(basicSalaryColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(braColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(noPayColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(grossSalaryColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(tbiColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(pbiColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(salesCommissionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(upsellingAndEbillingIncentiveColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(epfDeductionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(netSalaryColumn);
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
            TcTheme.FormatGridCurrencyDisplayColumn(dNoPayColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dGrossSalaryColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dTBIColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dPBIColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dSalesCommissionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dUpsellingAndEbillingIncentiveColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dEPFDeductionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dNetSalaryColumn);
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

            if (!TcVersions.IsCustomerCareFR001Supported(master.SettingsForm.WorkingYearMonth))
            {
                salesCommissionColumn.Visible = false;
                upsellingAndEbillingIncentiveColumn.Visible = false;

                dSalesCommissionColumn.Visible = false;
                dUpsellingAndEbillingIncentiveColumn.Visible = false;
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
                duplicatesSource.DataSource = new TcBindingList<TcCustomerCareSalaryRow>();

                TcCustomerCareSalaryLoader loader = new TcCustomerCareSalaryLoader(master.SettingsForm.WorkingYearMonth);
                all = loader.LoadFromCSV(filePath);
                
                SalaryTable = new TcCustomerCareSalaryTable();
                SalaryTable.Load(all);

                all = SalaryTable.All;
                source.DataSource = all;

                SetStatus();

                SetFilter();
                SetFileInfo();
                DataLoaded = true;

                TcCustomerCareForm.ResetMasterFormFilter = true;
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
            SetFilterData(SalaryTable.HasEmployeeNumberDuplicates(), TcEnum.GetTextForEnum<TeCustomerCareSalaryFilter>(TeCustomerCareSalaryFilter.Employee_Number_Duplicates));
            SetFilterData(SalaryTable.HasNICDuplicates(), TcEnum.GetTextForEnum<TeCustomerCareSalaryFilter>(TeCustomerCareSalaryFilter.NIC_Duplicates));

            filterComboBox.Text = TcEnum.GetTextForEnum<TeCustomerCareSalaryFilter>(TeCustomerCareSalaryFilter.All);
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
                TcCustomerCareSalaryRow row = source.Current as TcCustomerCareSalaryRow;

                if (row != null)
                {
                    TeCustomerCareSalaryFilter filter = TcEnum.GetEnumForText<TeCustomerCareSalaryFilter>(TeCustomerCareSalaryFilter.All, filterComboBox.Text);
                    switch (filter)
                    {
                        case TeCustomerCareSalaryFilter.All:
                            duplicatesSource.DataSource = SalaryTable.GetDuplicates(row.EmployeeNumber, row.NIC);
                            break;

                        case TeCustomerCareSalaryFilter.Employee_Number_Duplicates:
                            duplicatesSource.DataSource = SalaryTable.GetEmployeeNumberDuplicates(row.EmployeeNumber);
                            break;

                        case TeCustomerCareSalaryFilter.NIC_Duplicates:
                            duplicatesSource.DataSource = SalaryTable.GetNICDuplicates(row.NIC);
                            break;

                        default:
                            duplicatesSource.DataSource = new TcBindingList<TcCustomerCareSalaryRow>();
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
                TeCustomerCareSalaryFilter filter = TcEnum.GetEnumForText<TeCustomerCareSalaryFilter>(TeCustomerCareSalaryFilter.All, filterComboBox.Text);
                switch (filter)
                {
                    case TeCustomerCareSalaryFilter.All:
                        source.DataSource = all;
                        break;

                    case TeCustomerCareSalaryFilter.Employee_Number_Duplicates:
                        source.DataSource = SalaryTable.GetEmployeeNumberDuplicates();
                        break;

                    case TeCustomerCareSalaryFilter.NIC_Duplicates:
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
            TcSearchHelper<TcCustomerCareSalaryRow> searchHelper = new TcSearchHelper<TcCustomerCareSalaryRow>();

            source.DataSource = searchHelper.Search(source, searchText);
        }

        private void SetStatus()
        {
            statusLabel.Text = string.Format("{0} record(s) found", source.Count);
            TcBindingList<TcCustomerCareSalaryRow> list = source.DataSource as TcBindingList<TcCustomerCareSalaryRow>;

            decimal total = 0;

            foreach (TcCustomerCareSalaryRow row in list)
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

using Payroll.Library.General;
using Payroll.Library.MetaData;
using Payroll.UI.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

// Harshan Nishantha
// 2015-11-05

namespace Payroll.UI.Business.Analyze
{
    public partial class TcBusinessAnalyzeForm : Form
    {
        private TcBusinessForm master;
        private TcMetaData metaData;
        private BindingSource source = new BindingSource();

        public TcBindingList<TcBusinessAnalyzedRow> AnalyzedRows { get; set; }

        public TcBusinessAnalyzeForm(TcBusinessForm master, TcMetaData metaData)
        {
            InitializeComponent();

            this.master = master;
            this.metaData = metaData;
            AnalyzedRows = new TcBindingList<TcBusinessAnalyzedRow>();

            dataGridView.AutoGenerateColumns = false;

            source.DataSource = new TcBindingList<TcBusinessAnalyzedRow>();
            dataGridView.DataSource = source;
            statusLabel.Text = string.Empty;

            ApplyTheme();
        }

        private void ApplyTheme()
        {
            TcTheme.FormatGrid(dataGridView);

            TcTheme.FormatGridCurrencyDisplayColumn(netCommissionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(amountColumn);
            TcTheme.FormatGridDateDisplayColumn(dateOfBirthColumn);
            TcTheme.FormatGridDateDisplayColumn(dateOfJoinColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(daysWorkedColumn);

            TcTheme.FormatGridCurrencyDisplayColumn(epfContributionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(etfContributionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(daysWorkedColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(epfDeductionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(grossCommissionColumn);
        }

        public void Reset()
        {
            statusLabel.Text = string.Empty;

            source.DataSource = new TcBindingList<TcBusinessAnalyzedRow>();
            AnalyzedRows = new TcBindingList<TcBusinessAnalyzedRow>();

            reasonsRichTextBox.Text = "";
            SetFilter();
        }

        private void analyzeButton_Click(object sender, EventArgs e)
        {
            Analyze();
        }

        public void Analyze()
        {
            try
            {
                source.DataSource = new TcBindingList<TcBusinessAnalyzedRow>();
                AnalyzedRows = new TcBindingList<TcBusinessAnalyzedRow>();
                searchTextBox.Text = string.Empty;

                if (DataLoaded())
                {
                    TcBusinessAnalyzer loader = new TcBusinessAnalyzer();
                    AnalyzedRows = loader.Analyze(master, metaData);

                    source.DataSource = AnalyzedRows;

                    SetFilter();
                    SetStatus();
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void SetFilter()
        {
            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeBusinessAnalyzeFilter>(TeBusinessAnalyzeFilter.All));
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeBusinessAnalyzeFilter>(TeBusinessAnalyzeFilter.Valid));

            Dictionary<TeBusinessAnalyzeFilter, TeBusinessAnalyzeFilter> filters = 
                new Dictionary<TeBusinessAnalyzeFilter, TeBusinessAnalyzeFilter>();
            List<TeBusinessAnalyzeFilter> filtersList = new List<TeBusinessAnalyzeFilter>();

            foreach (TcBusinessAnalyzedRow data in AnalyzedRows)
            {
                foreach (KeyValuePair<TeBusinessAnalyzeFilter, string> pair in data.Errors)
                {
                    if (!filters.ContainsKey(pair.Key))
                    {
                        filters.Add(pair.Key, pair.Key);
                        filtersList.Add(pair.Key);
                    }
                }
            }

            filtersList.Sort();
            foreach (TeBusinessAnalyzeFilter item in filtersList)
            {
                filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeBusinessAnalyzeFilter>(item));
            }

            filterComboBox.Text = TcEnum.GetTextForEnum<TeBusinessAnalyzeFilter>(TeBusinessAnalyzeFilter.All);
        }

        private bool DataLoaded()
        {
            if (!master.MasterForm.DataLoaded)
            {
                TcMessageBox.ShowInformation("Please reload master data to analyze");
                return false;
            }
            else if (!master.BanksAndBranchesForm.DataLoaded)
            {
                TcMessageBox.ShowInformation("Please reload banks and branches data to analyze");
                return false;
            }
            else if (!master.SalaryForm.DataLoaded)
            {
                TcMessageBox.ShowInformation("Please load salary data to analyze");
                return false;
            }

            return true;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                TcBusinessAnalyzedRow data = source.Current as TcBusinessAnalyzedRow;
                if (data != null)
                {
                    reasonsRichTextBox.Text = data.GetErrors();
                }
                else
                {
                    reasonsRichTextBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void filterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterAndSearch();
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            FilterAndSearch();
        }

        private void FilterAndSearch()
        {
            try
            {
                TcBindingList<TcBusinessAnalyzedRow> filteredDataList = new TcBindingList<TcBusinessAnalyzedRow>();
                TeBusinessAnalyzeFilter filter = TcEnum.GetEnumForText<TeBusinessAnalyzeFilter>(TeBusinessAnalyzeFilter.All, filterComboBox.Text);

                if (filter == TeBusinessAnalyzeFilter.All)
                {
                    filteredDataList = AnalyzedRows;
                }
                else if (filter == TeBusinessAnalyzeFilter.Valid)
                {
                    foreach (TcBusinessAnalyzedRow data in AnalyzedRows)
                    {
                        if (!data.HasErrors)
                        {
                            filteredDataList.Add(data);
                        }
                    }
                }
                else
                {
                    foreach (TcBusinessAnalyzedRow data in AnalyzedRows)
                    {
                        if (data.HasError(filter))
                        {
                            filteredDataList.Add(data);
                        }
                    }
                }

                source.DataSource = filteredDataList;
                
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
            TcSearchHelper<TcBusinessAnalyzedRow> searchHelper = new TcSearchHelper<TcBusinessAnalyzedRow>();

            source.DataSource = searchHelper.Search(source, searchText);
        }

        private void SetStatus()
        {
            statusLabel.Text = string.Format("[{0}] record(s)", source.Count);
            SetPayMasterInfo();
            TcBindingList<TcBusinessAnalyzedRow> list = source.DataSource as TcBindingList<TcBusinessAnalyzedRow>;

            decimal netSalary = 0;
            decimal amount = 0;

            foreach (TcBusinessAnalyzedRow row in list)
            {
                netSalary   += row.NetSalary;

                if (row.BankTransferAmount > 0)
                {
                    amount += row.BankTransferAmount;
                }
            }

            amountsLabel.Text = string.Format("Net Salary: {0},  Amount: {1}",
                netSalary.ToString("N2"), amount.ToString("N2"));
        }

        private void SetPayMasterInfo()
        {
            TeBusinessAnalyzeFilter filter = TcEnum.GetEnumForText<TeBusinessAnalyzeFilter>(TeBusinessAnalyzeFilter.All, filterComboBox.Text);

            if (filter == TeBusinessAnalyzeFilter.Employee_Bank_and_Branch_Code_not_Found ||
                filter == TeBusinessAnalyzeFilter.Employee_Bank_Account_Number_Invalid ||
                filter == TeBusinessAnalyzeFilter.Employee_Bank_is_not_Supported_by_PayMaster)
            {
                statusLabel.Text += ". These record(s) will be excluded from PayMaster";
                TcTheme.DisplayErrorLabel(statusLabel, statusLabel.Text);
            }
            else
            {
                TcTheme.DisplayInfoLabel(statusLabel, statusLabel.Text);
            }
        }
    }
}

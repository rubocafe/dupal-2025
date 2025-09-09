using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.UI.Common.AnalyzeBean;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.Auditors.Analyze
{
    public partial class TcAuditorsAnalyzeForm : Form
    {
        private TcAuditorsForm master;
        private BindingSource source = new BindingSource();

        public TcBindingList<TcAuditorsAnalyzedRow> PaymasterDataList { get; set; }

        public TcAuditorsAnalyzeForm(TcAuditorsForm master)
        {
            InitializeComponent();

            this.master = master;
            PaymasterDataList = new TcBindingList<TcAuditorsAnalyzedRow>();

            dataGridView.AutoGenerateColumns = false;

            source.DataSource = new TcBindingList<TcAuditorsAnalyzedRow>();
            dataGridView.DataSource = source;
            statusLabel.Text = string.Empty;

            ApplyTheme();
        }

        private void ApplyTheme()
        {
            TcTheme.FormatGrid(dataGridView);

            TcTheme.FormatGridCurrencyDisplayColumn(totalRemunerationColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(basicSalaryColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(braColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(grossSalaryColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(epfDeductionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(netSalaryColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(travelAllowanceColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(travelReimbursementColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(travelIncentiveColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(holdColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(bankTransferAmountColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(epfContributionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(etfContributionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(payeColumn);
            TcTheme.FormatGridDateDisplayColumn(dateOfBirthColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(daysWorkedColumn);

            TcTheme.FormatGridDateDisplayColumn(dateOfJoinColumn);

            if (!TcVersions.IsEpfEtfSupported(master.SettingsForm.WorkingYearMonth))
            {
                initialsColumn.Visible      = false;
                lastNameColumn.Visible      = false;
                ocGradeColumn.Visible       = false;
                memberStatusColumn.Visible  = false;
                daysWorkedColumn.Visible    = false;
            }

            if (!TcVersions.IsPayeSupported(master.SettingsForm.WorkingYearMonth))
            {
                payeColumn.Visible = false;
            }
        }

        public void Reset()
        {
            statusLabel.Text = string.Empty;

            source.DataSource = new TcBindingList<TcAuditorsAnalyzedRow>();
            PaymasterDataList = new TcBindingList<TcAuditorsAnalyzedRow>();

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
                source.DataSource = new TcBindingList<TcAuditorsAnalyzedRow>();
                PaymasterDataList = new TcBindingList<TcAuditorsAnalyzedRow>();
                searchTextBox.Text = string.Empty;

                if (DataLoaded())
                {
                    TcAuditorsAnalyzer loader = new TcAuditorsAnalyzer();
                    PaymasterDataList = loader.Analyze(master);

                    source.DataSource = PaymasterDataList;

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
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeEmployeeAnalyzeFilter>(TeEmployeeAnalyzeFilter.All));
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeEmployeeAnalyzeFilter>(TeEmployeeAnalyzeFilter.Valid));

            Dictionary<TeEmployeeAnalyzeFilter, TeEmployeeAnalyzeFilter> filters = new Dictionary<TeEmployeeAnalyzeFilter, TeEmployeeAnalyzeFilter>();
            List<string> filtersList = new List<string>();

            foreach (TcAuditorsAnalyzedRow data in PaymasterDataList)
            {
                foreach (KeyValuePair<TeEmployeeAnalyzeFilter, string> pair in data.Errors)
                {
                    if (!filters.ContainsKey(pair.Key))
                    {
                        filters.Add(pair.Key, pair.Key);
                        filtersList.Add(TcEnum.GetTextForEnum<TeEmployeeAnalyzeFilter>(pair.Key));
                    }
                }
            }

            filtersList.Sort();
            foreach (string item in filtersList)
            {
                filterComboBox.Items.Add(item);
            }

            filterComboBox.Text = TcEnum.GetTextForEnum<TeEmployeeAnalyzeFilter>(TeEmployeeAnalyzeFilter.All);
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
                TcAuditorsAnalyzedRow data = source.Current as TcAuditorsAnalyzedRow;
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
                TcBindingList<TcAuditorsAnalyzedRow> filteredDataList = new TcBindingList<TcAuditorsAnalyzedRow>();
                TeEmployeeAnalyzeFilter filter = TcEnum.GetEnumForText<TeEmployeeAnalyzeFilter>(TeEmployeeAnalyzeFilter.All, filterComboBox.Text);

                if (filter == TeEmployeeAnalyzeFilter.All)
                {
                    filteredDataList = PaymasterDataList;
                }
                else if (filter == TeEmployeeAnalyzeFilter.Valid)
                {
                    foreach (TcAuditorsAnalyzedRow data in PaymasterDataList)
                    {
                        if (!data.HasErrors)
                        {
                            filteredDataList.Add(data);
                        }
                    }
                }
                else
                {
                    foreach (TcAuditorsAnalyzedRow data in PaymasterDataList)
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
            TcSearchHelper<TcAuditorsAnalyzedRow> searchHelper = new TcSearchHelper<TcAuditorsAnalyzedRow>();

            source.DataSource = searchHelper.Search(source, searchText);
        }

        private void SetStatus()
        {
            statusLabel.Text = string.Format("[{0}] record(s)", source.Count);
            TcAnalyzeFormHelper.AddPayMasterInfoToStatus(filterComboBox, statusLabel);
            TcBindingList<TcAuditorsAnalyzedRow> list = source.DataSource as TcBindingList<TcAuditorsAnalyzedRow>;

            decimal total = 0;

            foreach (TcAuditorsAnalyzedRow row in list)
            {
                if (row.BankTransferAmount > 0)
                {
                    total += row.BankTransferAmount;
                }
            }

            amountsLabel.Text = string.Format("Bank Transfer Amount: {0}", total.ToString("N2"));
        }
    }
}

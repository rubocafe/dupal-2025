using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-08-27

namespace DUPALPayroll.UI.CommissionAgents.Analyze
{
    public partial class TcCommissionAgentsAnalyzeForm : Form
    {
        private TcCommissionAgentsForm master;
        private BindingSource source = new BindingSource();

        public TcBindingList<TcCommissionAgentsAnalyzedRow> AnalyzedRows { get; set; }

        public TcCommissionAgentsAnalyzeForm(TcCommissionAgentsForm master)
        {
            InitializeComponent();

            this.master = master;
            AnalyzedRows = new TcBindingList<TcCommissionAgentsAnalyzedRow>();

            dataGridView.AutoGenerateColumns = false;

            source.DataSource = new TcBindingList<TcCommissionAgentsAnalyzedRow>();
            dataGridView.DataSource = source;
            statusLabel.Text = string.Empty;

            ApplyTheme();
        }

        private void ApplyTheme()
        {
            TcTheme.FormatGrid(dataGridView);

            TcTheme.FormatGridCurrencyDisplayColumn(netCommissionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(holdColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(amountColumn);
            TcTheme.FormatGridDateDisplayColumn(dateOfBirthColumn);
            TcTheme.FormatGridDateDisplayColumn(dateOfJoinColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(daysWorkedColumn);

            TcTheme.FormatGridCurrencyDisplayColumn(epfContributionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(etfContributionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(payeColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(daysWorkedColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(epfDeductionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(grossCommissionColumn);

            if (!TcVersions.IsEpfEtfSupported(master.SettingsForm.WorkingYearMonth))
            {
                initialsColumn.Visible          = false;
                lastNameColumn.Visible          = false;
                employeeNumberColumn.Visible    = false;
                ocGradeColumn.Visible           = false;

                employeeNumberColumn.Visible    = false;
                epfContributionColumn.Visible   = false;
                etfContributionColumn.Visible   = false;
                memberStatusColumn.Visible      = false;
                daysWorkedColumn.Visible        = false;
            }

            if (!TcVersions.IsPayeSupported(master.SettingsForm.WorkingYearMonth))
            {
                payeColumn.Visible = false;
            }
        }

        public void Reset()
        {
            statusLabel.Text = string.Empty;

            source.DataSource = new TcBindingList<TcCommissionAgentsAnalyzedRow>();
            AnalyzedRows = new TcBindingList<TcCommissionAgentsAnalyzedRow>();

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
                source.DataSource = new TcBindingList<TcCommissionAgentsAnalyzedRow>();
                AnalyzedRows = new TcBindingList<TcCommissionAgentsAnalyzedRow>();
                searchTextBox.Text = string.Empty;

                if (DataLoaded())
                {
                    TcCommissionAgentsAnalyzer loader = new TcCommissionAgentsAnalyzer();
                    AnalyzedRows = loader.Analyze(master);

                    source.DataSource = AnalyzedRows;

                    SetFilter();
                    SetStatus();

                    TcCommissionAgentsForm.ResetCommissionsHeldFilter = true;
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
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeCommissionAgentsAnalyzeFilter>(TeCommissionAgentsAnalyzeFilter.All));
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeCommissionAgentsAnalyzeFilter>(TeCommissionAgentsAnalyzeFilter.Valid));

            Dictionary<TeCommissionAgentsAnalyzeFilter, TeCommissionAgentsAnalyzeFilter> filters = new Dictionary<TeCommissionAgentsAnalyzeFilter, TeCommissionAgentsAnalyzeFilter>();
            List<TeCommissionAgentsAnalyzeFilter> filtersList = new List<TeCommissionAgentsAnalyzeFilter>();

            foreach (TcCommissionAgentsAnalyzedRow data in AnalyzedRows)
            {
                foreach (KeyValuePair<TeCommissionAgentsAnalyzeFilter, string> pair in data.Errors)
                {
                    if (!filters.ContainsKey(pair.Key))
                    {
                        filters.Add(pair.Key, pair.Key);
                        filtersList.Add(pair.Key);
                    }
                }
            }

            filtersList.Sort();
            foreach (TeCommissionAgentsAnalyzeFilter item in filtersList)
            {
                filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeCommissionAgentsAnalyzeFilter>(item));
            }

            filterComboBox.Text = TcEnum.GetTextForEnum<TeCommissionAgentsAnalyzeFilter>(TeCommissionAgentsAnalyzeFilter.All);
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
            else if (!master.CommissionsForm.DataLoaded)
            {
                TcMessageBox.ShowInformation("Please load salary data to analyze");
                return false;
            }
            else if (!master.CommissionHeldForm.DataLoaded)
            {
                TcMessageBox.ShowInformation("Please load salary hold data to analyze");
                return false;
            }

            return true;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                TcCommissionAgentsAnalyzedRow data = source.Current as TcCommissionAgentsAnalyzedRow;
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
                TcBindingList<TcCommissionAgentsAnalyzedRow> filteredDataList = new TcBindingList<TcCommissionAgentsAnalyzedRow>();
                TeCommissionAgentsAnalyzeFilter filter = TcEnum.GetEnumForText<TeCommissionAgentsAnalyzeFilter>(TeCommissionAgentsAnalyzeFilter.All, filterComboBox.Text);

                if (filter == TeCommissionAgentsAnalyzeFilter.All)
                {
                    filteredDataList = AnalyzedRows;
                }
                else if (filter == TeCommissionAgentsAnalyzeFilter.Valid)
                {
                    foreach (TcCommissionAgentsAnalyzedRow data in AnalyzedRows)
                    {
                        if (!data.HasErrors)
                        {
                            filteredDataList.Add(data);
                        }
                    }
                }
                else
                {
                    foreach (TcCommissionAgentsAnalyzedRow data in AnalyzedRows)
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
            TcSearchHelper<TcCommissionAgentsAnalyzedRow> searchHelper = new TcSearchHelper<TcCommissionAgentsAnalyzedRow>();

            source.DataSource = searchHelper.Search(source, searchText);
        }

        private void SetStatus()
        {
            statusLabel.Text = string.Format("[{0}] record(s)", source.Count);
            SetPayMasterInfo();
            TcBindingList<TcCommissionAgentsAnalyzedRow> list = source.DataSource as TcBindingList<TcCommissionAgentsAnalyzedRow>;

            decimal netCommission = 0;
            decimal hold = 0;
            decimal amount = 0;

            foreach (TcCommissionAgentsAnalyzedRow row in list)
            {
                netCommission   += row.NetCommission;
                hold            += row.Hold;

                if (row.Amount > 0)
                {
                    amount += row.Amount;
                }
            }

            amountsLabel.Text = string.Format("Net Commission: {0},  Hold: {1},  Amount: {2}",
                netCommission.ToString("N2"), hold.ToString("N2"), amount.ToString("N2"));
        }

        private void SetPayMasterInfo()
        {
            TeCommissionAgentsAnalyzeFilter filter = TcEnum.GetEnumForText<TeCommissionAgentsAnalyzeFilter>(TeCommissionAgentsAnalyzeFilter.All, filterComboBox.Text);

            if (filter == TeCommissionAgentsAnalyzeFilter.Agent_Bank_and_Branch_Code_not_Found ||
                filter == TeCommissionAgentsAnalyzeFilter.Agent_Bank_Account_Number_Invalid ||
                filter == TeCommissionAgentsAnalyzeFilter.Agent_Bank_is_not_Supported_by_PayMaster)
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

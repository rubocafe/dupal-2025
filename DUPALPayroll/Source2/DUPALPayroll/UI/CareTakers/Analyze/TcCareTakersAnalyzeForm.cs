using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-10-31

namespace DUPALPayroll.UI.CareTakers.Analyze
{
    public partial class TcCareTakersAnalyzeForm : Form
    {
        private TcCareTakersForm master;
        private BindingSource source = new BindingSource();

        public TcBindingList<TcCareTakersAnalyzedRow> AnalyzedRows { get; set; }

        public TcCareTakersAnalyzeForm(TcCareTakersForm master)
        {
            InitializeComponent();

            this.master = master;
            AnalyzedRows = new TcBindingList<TcCareTakersAnalyzedRow>();

            dataGridView.AutoGenerateColumns = false;

            source.DataSource = new TcBindingList<TcCareTakersAnalyzedRow>();
            dataGridView.DataSource = source;
            statusLabel.Text = string.Empty;

            ApplyTheme();
        }

        private void ApplyTheme()
        {
            TcTheme.FormatGrid(dataGridView);

            TcTheme.FormatGridCurrencyDisplayColumn(paymentColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(holdColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(amountColumn);
            TcTheme.FormatGridDateDisplayColumn(dateOfBirthColumn);
        }

        public void Reset()
        {
            statusLabel.Text = string.Empty;

            source.DataSource = new TcBindingList<TcCareTakersAnalyzedRow>();
            AnalyzedRows = new TcBindingList<TcCareTakersAnalyzedRow>();

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
                source.DataSource = new TcBindingList<TcCareTakersAnalyzedRow>();
                AnalyzedRows = new TcBindingList<TcCareTakersAnalyzedRow>();
                searchTextBox.Text = string.Empty;

                if (DataLoaded())
                {
                    TcCareTakersAnalyzer loader = new TcCareTakersAnalyzer();
                    AnalyzedRows = loader.Analyze(master);

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
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeCareTakersAnalyzeFilter>(TeCareTakersAnalyzeFilter.All));
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeCareTakersAnalyzeFilter>(TeCareTakersAnalyzeFilter.Valid));

            Dictionary<TeCareTakersAnalyzeFilter, TeCareTakersAnalyzeFilter> filters = new Dictionary<TeCareTakersAnalyzeFilter, TeCareTakersAnalyzeFilter>();
            List<TeCareTakersAnalyzeFilter> filtersList = new List<TeCareTakersAnalyzeFilter>();

            foreach (TcCareTakersAnalyzedRow data in AnalyzedRows)
            {
                foreach (KeyValuePair<TeCareTakersAnalyzeFilter, string> pair in data.Errors)
                {
                    if (!filters.ContainsKey(pair.Key))
                    {
                        filters.Add(pair.Key, pair.Key);
                        filtersList.Add(pair.Key);
                    }
                }
            }

            filtersList.Sort();
            foreach (TeCareTakersAnalyzeFilter item in filtersList)
            {
                filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeCareTakersAnalyzeFilter>(item));
            }

            filterComboBox.Text = TcEnum.GetTextForEnum<TeCareTakersAnalyzeFilter>(TeCareTakersAnalyzeFilter.All);
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
            else if (!master.PymentsForm.DataLoaded)
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
                TcCareTakersAnalyzedRow data = source.Current as TcCareTakersAnalyzedRow;
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
                TcBindingList<TcCareTakersAnalyzedRow> filteredDataList = new TcBindingList<TcCareTakersAnalyzedRow>();
                TeCareTakersAnalyzeFilter filter = TcEnum.GetEnumForText<TeCareTakersAnalyzeFilter>(TeCareTakersAnalyzeFilter.All, filterComboBox.Text);

                if (filter == TeCareTakersAnalyzeFilter.All)
                {
                    filteredDataList = AnalyzedRows;
                }
                else if (filter == TeCareTakersAnalyzeFilter.Valid)
                {
                    foreach (TcCareTakersAnalyzedRow data in AnalyzedRows)
                    {
                        if (!data.HasErrors)
                        {
                            filteredDataList.Add(data);
                        }
                    }
                }
                else
                {
                    foreach (TcCareTakersAnalyzedRow data in AnalyzedRows)
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
            TcSearchHelper<TcCareTakersAnalyzedRow> searchHelper = new TcSearchHelper<TcCareTakersAnalyzedRow>();

            source.DataSource = searchHelper.Search(source, searchText);
        }

        private void SetStatus()
        {
            statusLabel.Text = string.Format("[{0}] record(s)", source.Count);
            SetPayMasterInfo();
            TcBindingList<TcCareTakersAnalyzedRow> list = source.DataSource as TcBindingList<TcCareTakersAnalyzedRow>;

            decimal payment = 0;
            decimal hold = 0;
            decimal amount = 0;

            foreach (TcCareTakersAnalyzedRow row in list)
            {
                payment   += row.Payment;
                hold            += row.Hold;

                if (row.Amount > 0)
                {
                    amount += row.Amount;
                }
            }

            amountsLabel.Text = string.Format("Payment: {0},  Hold: {1},  Amount: {2}",
                payment.ToString("N2"), hold.ToString("N2"), amount.ToString("N2"));
        }

        private void SetPayMasterInfo()
        {
            TeCareTakersAnalyzeFilter filter = TcEnum.GetEnumForText<TeCareTakersAnalyzeFilter>(TeCareTakersAnalyzeFilter.All, filterComboBox.Text);

            if (filter == TeCareTakersAnalyzeFilter.Agent_Bank_and_Branch_Code_not_Found ||
                filter == TeCareTakersAnalyzeFilter.Agent_Bank_Account_Number_Invalid ||
                filter == TeCareTakersAnalyzeFilter.Agent_Bank_is_not_Supported_by_PayMaster)
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

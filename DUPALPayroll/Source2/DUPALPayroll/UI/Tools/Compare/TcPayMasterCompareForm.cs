using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.UI.CommissionAgents.Tools.Decode;
using DUPALPayroll.UI.Common.PayMaster;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-08-28

namespace DUPALPayroll.UI.CommissionAgents.Tools.Compare
{
    public partial class TcPayMasterCompareForm : Form
    {
        private TcBindingList<TcPayMasterComparedRow> allRows = new TcBindingList<TcPayMasterComparedRow>();

        private BindingSource primarySource = new BindingSource();
        private BindingSource secondrySource = new BindingSource();

        public TcPayMasterCompareForm()
        {
            InitializeComponent();

            statusLabel.Text = "";

            primarySource.DataSource    = new TcBindingList<TcPayMasterComparedRow>();
            secondrySource.DataSource   = new TcBindingList<TcPayMasterRow>();

            primaryDataGridView.AutoGenerateColumns     = false;
            secondryDataGridView.AutoGenerateColumns    = false;

            primaryDataGridView.DataSource  = primarySource;
            secondryDataGridView.DataSource = secondrySource;

            LoadFilterComboBox();

            ApplyTheme();
        }

        private void ApplyTheme()
        {
            TcTheme.FormatGrid(primaryDataGridView);
            TcTheme.FormatGrid(secondryDataGridView);

            TcTheme.FormatGridCurrencyDisplayColumn(amountDecimalColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dAmountDecimalColumn);
        }

        private void LoadFilterComboBox()
        {
            filterComboBox.Items.Clear();

            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TePayMasterCompareFilter>(TePayMasterCompareFilter.All));
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TePayMasterCompareFilter>(TePayMasterCompareFilter.Not_Matched));

            filterComboBox.Text = TcEnum.GetTextForEnum<TePayMasterCompareFilter>(TePayMasterCompareFilter.All);
        }

        private void primaryBrowseButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    primaryPathTextBox.Text = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void secondryBrowseButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    secondaryPathTextBox.Text = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void compareButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(primaryPathTextBox.Text))
                {
                    TcMessageBox.ShowInformation("Please select primary file to compare");

                    return;
                }

                if (string.IsNullOrEmpty(secondaryPathTextBox.Text))
                {
                    TcMessageBox.ShowInformation("Please select secondry file to compare");

                    return;
                }

                TcPayMasterFileDecorder primaryDecorder = new TcPayMasterFileDecorder(primaryPathTextBox.Text);
                TcBindingList<TcPayMasterRow> primaryList = primaryDecorder.Decode();

                TcPayMasterFileDecorder secondryDecorder = new TcPayMasterFileDecorder(secondaryPathTextBox.Text);
                TcBindingList<TcPayMasterRow> secondryList = secondryDecorder.Decode();

                TcBindingList<TcPayMasterComparedRow> comparedRows = new TcBindingList<TcPayMasterComparedRow>();
                for (int i = 0; i < primaryList.Count; i++)
                {
                    if (secondryList.Count > i)
                    {
                        TcPayMasterRow primaryRow = primaryList[i];
                        TcPayMasterRow secondryRow = secondryList[i];

                        TcPayMasterComparedRow comparedRow = TcPayMasterComparedRow.Create(primaryRow, secondryRow);
                        comparedRows.Add(comparedRow);
                    }
                }

                allRows = comparedRows;
                primarySource.DataSource = comparedRows;

                SetStatus();
                SetFilter();

                string countString = string.Format("Primary file has [{0}] row(s). Secondry file has [{1}] row(s)", primaryList.Count, secondryList.Count);
                TcMessageBox.ShowInformation(string.Format("Data loaded successfully\n{0}", countString));
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void SetFilter()
        {
            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TePayMasterCompareFilter>(TePayMasterCompareFilter.All));
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TePayMasterCompareFilter>(TePayMasterCompareFilter.Not_Matched));

            Dictionary<TePayMasterCompareFilter, TePayMasterCompareFilter> filters = new Dictionary<TePayMasterCompareFilter, TePayMasterCompareFilter>();
            List<TePayMasterCompareFilter> filtersList = new List<TePayMasterCompareFilter>();

            foreach (TcPayMasterComparedRow data in allRows)
            {
                foreach (KeyValuePair<TePayMasterCompareFilter, string> pair in data.Errors)
                {
                    if (!filters.ContainsKey(pair.Key))
                    {
                        filters.Add(pair.Key, pair.Key);
                        filtersList.Add(pair.Key);
                    }
                }
            }

            filtersList.Sort();
            foreach (TePayMasterCompareFilter item in filtersList)
            {
                filterComboBox.Items.Add(TcEnum.GetTextForEnum<TePayMasterCompareFilter>(item));
            }

            filterComboBox.Text = TcEnum.GetTextForEnum<TePayMasterCompareFilter>(TePayMasterCompareFilter.All);
        }

        private void filterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterAndSearch();
        }

        private void primaryDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                TcPayMasterComparedRow data = primarySource.Current as TcPayMasterComparedRow;
                if (data != null)
                {
                    reasonRichTextBox.Text = data.GetErrors();
                    TcBindingList<TcPayMasterRow> list = new TcBindingList<TcPayMasterRow>();
                    list.Add(data as TcPayMasterRow);
                    list.Add(data.SecondaryRow);

                    secondrySource.DataSource = list;
                }
                else
                {
                    reasonRichTextBox.Text = "";
                    secondrySource.DataSource = new TcBindingList<TcPayMasterRow>();
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterAndSearch();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void FilterAndSearch()
        {
            try
            {
                TePayMasterCompareFilter filter = TcEnum.GetEnumForText<TePayMasterCompareFilter>(TePayMasterCompareFilter.All, filterComboBox.Text);
                TcBindingList<TcPayMasterComparedRow> filteredDataList = new TcBindingList<TcPayMasterComparedRow>();

                if (filter == TePayMasterCompareFilter.All)
                {
                    filteredDataList = allRows;
                }
                else if (filter == TePayMasterCompareFilter.Not_Matched)
                {
                    foreach (TcPayMasterComparedRow data in allRows)
                    {
                        if (data.HasErrors)
                        {
                            filteredDataList.Add(data);
                        }
                    }
                }
                else
                {
                    foreach (TcPayMasterComparedRow data in allRows)
                    {
                        if (data.HasError(filter))
                        {
                            filteredDataList.Add(data);
                        }
                    }
                }

                primarySource.DataSource = filteredDataList;

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
            TcSearchHelper<TcPayMasterComparedRow> searchHelper = new TcSearchHelper<TcPayMasterComparedRow>();

            primarySource.DataSource = searchHelper.Search(primarySource, searchText);
        }

        private void SetStatus()
        {
            statusLabel.Text = string.Format("[{0}] record(s)", primarySource.Count);
        }
    }
}

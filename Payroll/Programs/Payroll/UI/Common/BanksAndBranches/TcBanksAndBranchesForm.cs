using Payroll.Library.General;
using Payroll.Library.MetaData;
using Payroll.UI.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-08-26

namespace Payroll.UI.Common.BanksAndBranches
{
    public partial class TcBanksAndBranchesForm : Form
    {
        private string filePath = string.Empty;
        public bool DataLoaded { get; set; }

        public TcBanksAndBranchesTable Table;
        public TcBanksAndBranchesEngine Engine;

        public TcBanksAndBranchesForm(string filePath)
        {
            InitializeComponent();

            this.filePath = filePath;
            DataLoaded = false;

            dataGridView.AutoGenerateColumns = false;
            duplicatesDataGridView.AutoGenerateColumns = false;

            statusLabel.Text = string.Empty;

            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeBanksAndBranchesFilter>(TeBanksAndBranchesFilter.All));


            Table = new TcBanksAndBranchesTable(filePath);
            Table.Load();

            Engine = new TcBanksAndBranchesEngine(Table.Rows);
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            TcTheme.FormatGrid(dataGridView);
            TcTheme.FormatGrid(duplicatesDataGridView);

            TcTheme.AddIndexColumnToGrid(dataGridView);
            TcTheme.AddIndexColumnToGrid(duplicatesDataGridView);

            var columns = Table.MetaData.GetColumnNames();
            foreach (var column in columns)
            {
                var key = column.ToUpper();
                var columnData = Table.MetaData.Data[key];
                string format = null;
                int width = 100;
                if (key == TcPropertyNames.BankCode)
                {
                    format = "D4";
                }
                else if (key == TcPropertyNames.BranchCode)
                {
                    format = "D3";
                }
                else if (key == TcPropertyNames.BankName)
                {
                    width = 200;
                }
                TcTheme.AddColumnToGrid(dataGridView, key, columnData.Name, columnData.Type, width: width, format: format);
                TcTheme.AddColumnToGrid(duplicatesDataGridView, key, columnData.Name, columnData.Type, width: width, format: format);
            }

            TcTheme.AddEmptyColumn(dataGridView);
            TcTheme.AddEmptyColumn(duplicatesDataGridView);
        }

        private void reloadDataButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                searchTextBox.Text = string.Empty;
                ReloadData();

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
                if (File.Exists(filePath))
                {
                    SetFilter();
                    FilterAndSearch();

                    SetStatus();
                    DataLoaded = true;

                    SetFileInfo();
                }
                else
                {
                    DataLoaded = false;
                    string ex = string.Format("Bank and Branches Data file [{0}] does not exist", filePath);
                    fileInfoLabel.Text = ex;
                    throw new Exception(ex);
                }
            }
            else
            {
                DataLoaded = false;
                string ex = string.Format("Bank and Branches Data file [{0}] does not exist", filePath);
                fileInfoLabel.Text = ex;
                throw new Exception(ex);
            }
        }

        private void SetFilter()
        {
            SetFilterData(Engine.HasDuplicates(),
                TcEnum.GetTextForEnum<TeBanksAndBranchesFilter>(TeBanksAndBranchesFilter.Duplicates));

            filterComboBox.Text = TcEnum.GetTextForEnum<TeBanksAndBranchesFilter>(TeBanksAndBranchesFilter.All);
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
            FileInfo fileInfo = new FileInfo(filePath);
            fileInfoLabel.Text = string.Format("File: [{0}], Last Modified Date: [{1}]", 
                fileInfo.FullName, fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private void filterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterAndSearch();
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    var selectedRow = dataGridView.SelectedRows[0];
                    string columnName = TcTheme.GridColumnNameFromkey(TcPropertyNames.BankName);
                    var bankName = (string)selectedRow.Cells[columnName].Value;

                    columnName = TcTheme.GridColumnNameFromkey(TcPropertyNames.Bank);
                    var bank= (string)selectedRow.Cells[columnName].Value;

                    columnName = TcTheme.GridColumnNameFromkey(TcPropertyNames.Branch);
                    var branch = (string)selectedRow.Cells[columnName].Value;

                    var key = Engine.GetKey(bankName, bank, branch);

                    List<TcBanksAndBranchesRow> data = new List<TcBanksAndBranchesRow>();

                    TeBanksAndBranchesFilter filter =
                        TcEnum.GetEnumForText<TeBanksAndBranchesFilter>(TeBanksAndBranchesFilter.All, filterComboBox.Text);
                    switch (filter)
                    {
                        case TeBanksAndBranchesFilter.All:
                        case TeBanksAndBranchesFilter.Duplicates:
                        default:
                            data = Engine.GetDuplicates(key);
                            break;
                    }

                    AddRowsToGrid(duplicatesDataGridView, data);
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
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

        private void Search()
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
                string filterText = filterComboBox.Text;
                string searchText = searchTextBox.Text.Trim();

                var data = Engine.FilterAndSearch(filterText, searchText);
                AddRowsToGrid(dataGridView, data);

                SetStatus();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void AddRowsToGrid(DataGridView grid, List<TcBanksAndBranchesRow> data)
        {
            grid.Rows.Clear();
            var results = Table.GetAsObjectArray(data);
            foreach (var row in results)
            {
                grid.Rows.Add(row);
            }
        }

        public void SetStatus()
        {
            statusLabel.Text = string.Format("{0} record(s) found", dataGridView.Rows.Count);
        }
    }
}

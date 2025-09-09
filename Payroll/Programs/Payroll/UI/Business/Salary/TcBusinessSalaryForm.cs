using Payroll.Library.General;
using Payroll.Library.MetaData;
using Payroll.UI.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2015-11-05

namespace Payroll.UI.Business.Salary
{
    public partial class TcBusinessSalaryForm : Form
    {
        private TcBusinessForm master;
        private string filePath = string.Empty;
        public bool DataLoaded { get; set; }

        private TcSalaryMetaData metaData;
        public TcBusinessSalaryTable Table;
        public TcBusinessSalaryEngine Engine;

        public TcBusinessSalaryForm(TcBusinessForm master, TcSalaryMetaData metaData, string filePath)
        {
            InitializeComponent();

            this.master = master;
            this.metaData = metaData;
            this.filePath = filePath;
            DataLoaded = false;

            dataGridView.AutoGenerateColumns = false;
            duplicatesDataGridView.AutoGenerateColumns = false;

            statusLabel.Text = string.Empty;

            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeBusinessSalaryFilter>(TeBusinessSalaryFilter.All));


            Table = new TcBusinessSalaryTable(metaData, filePath);
            Table.Load();

            Engine = new TcBusinessSalaryEngine(Table.Rows);
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            TcTheme.FormatGrid(dataGridView);
            TcTheme.FormatGrid(duplicatesDataGridView);

            TcTheme.AddIndexColumnToGrid(dataGridView);
            TcTheme.AddIndexColumnToGrid(duplicatesDataGridView);

            var columns = Table.MetaData.GetDataColumnNames();
            foreach (var column in columns)
            {
                var key = column.ToUpper();
                var columnData = Table.MetaData.Data[key];
                TcTheme.AddColumnToGrid(dataGridView, key, columnData.Name, columnData.Type);
                TcTheme.AddColumnToGrid(duplicatesDataGridView, key, columnData.Name, columnData.Type);
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
                SetFilter();
                FilterAndSearch();

                SetStatus();
                DataLoaded = true;

                SetFileInfo();
            }
            else
            {
                DataLoaded = false;
                string ex = string.Format("Salary file [{0}] does not exist", filePath);
                fileInfoLabel.Text = ex;
                throw new Exception(ex);
            }
        }

        private void SetFilter()
        {
            SetFilterData(Engine.HasNICDuplicates(),
                TcEnum.GetTextForEnum<TeBusinessSalaryFilter>(TeBusinessSalaryFilter.NIC_Duplicates));
            SetFilterData(Engine.HasEmptyNICRows(),
                TcEnum.GetTextForEnum<TeBusinessSalaryFilter>(TeBusinessSalaryFilter.Empty_NIC));

            filterComboBox.Text = TcEnum.GetTextForEnum<TeBusinessSalaryFilter>(TeBusinessSalaryFilter.All);
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
                fileInfoLabel.Text = "Click Load data button to load Salary Data";
            }
            else
            {
                FileInfo fileInfo = new FileInfo(filePath);
                fileInfoLabel.Text = string.Format("File: [{0}], Last Modified Date: [{1}]", 
                    fileInfo.FullName, fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));
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
                if (dataGridView.SelectedRows.Count > 0)
                {
                    var selectedRow = dataGridView.SelectedRows[0];
                    string columnName = TcTheme.GridColumnNameFromkey(TcPropertyNames.NIC);
                    var nic = (string)selectedRow.Cells[columnName].Value;
                    List<TcBusinessSalaryRow> data = new List<TcBusinessSalaryRow>();

                    TeBusinessSalaryFilter filter = 
                        TcEnum.GetEnumForText<TeBusinessSalaryFilter>(TeBusinessSalaryFilter.All, filterComboBox.Text);
                    switch (filter)
                    {
                        case TeBusinessSalaryFilter.Empty_NIC:
                            break;
                        default:
                            data = Engine.GetNICDuplicates(nic);
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

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            FilterAndSearch();
        }

        private void Search()
        {
            FilterAndSearch();
        }

        private void SetStatus(List<TcBusinessSalaryRow> data)
        {
            statusLabel.Text = string.Format("{0} record(s) found", dataGridView.Rows.Count);

            decimal grossSalary = 0;
            decimal epfDeduction    = 0;
            decimal netSalary   = 0;

            foreach (TcBusinessSalaryRow row in data)
            {
                grossSalary += row.GrossSalary;
                epfDeduction    += row.EpfDeduction;
                netSalary   += row.NetSalary;
            }

            amountsLabel.Text = string.Format("Gross Salary: {0},  EPF Deduction: {1},  Net Salary: {2}",
                grossSalary.ToString("N2"), epfDeduction.ToString("N2"), netSalary.ToString("N2"));
        }

        private void FilterAndSearch()
        {
            try
            {
                string filterText = filterComboBox.Text;
                string searchText = searchTextBox.Text.Trim();

                var data = Engine.FilterAndSearch(filterText, searchText);
                SetStatus(data);
                AddRowsToGrid(dataGridView, data);

                SetStatus();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void AddRowsToGrid(DataGridView grid, List<TcBusinessSalaryRow> data)
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

using Payroll.Library.General;
using Payroll.Library.MetaData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2015-11-04

namespace Payroll.UI.Business.MasterData
{
    public partial class TcBusinessMasterForm : Form
    {
        private TcBusinessForm master;
        private string filePath = string.Empty;
        public bool DataLoaded { get; set; }

        private TcMasterMetaData metaData;
        public TcBusinessMasterTable Table;
        public TcBusinessMasterEngine Engine;

        public TcBusinessMasterForm(TcBusinessForm master, TcMasterMetaData metaData, string filePath)
        {
            InitializeComponent();

            this.master     = master;
            this.metaData   = metaData;
            this.filePath   = filePath;
            DataLoaded      = false;

            dataGridView.AutoGenerateColumns = false;
            duplicatesDataGridView.AutoGenerateColumns = false;

            statusLabel.Text = string.Empty;

            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeBusinessMasterFilter>(TeBusinessMasterFilter.All));

            
            Table = new TcBusinessMasterTable(metaData, filePath);
            Table.Load();

            Engine = new TcBusinessMasterEngine(Table.Rows);
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

        private void reloadDataButton_Click(object sender, System.EventArgs e)
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
                string ex = string.Format("Master data file [{0}] does not exist", filePath);
                fileInfoLabel.Text = ex;
                throw new Exception(ex);
            }
        }

        public void SetFilter()
        {
            SetFilterData(Engine.HasNICDuplicates(), 
                TcEnum.GetTextForEnum<TeBusinessMasterFilter>(TeBusinessMasterFilter.NIC_Duplicates));
            SetFilterData(Engine.HasEmptyNIC(), 
                TcEnum.GetTextForEnum<TeBusinessMasterFilter>(TeBusinessMasterFilter.NIC_Empty));
            SetFilterData(Engine.HasDuplicateNICsForEmployeesInSalaryFile(master.SalaryForm.Table), 
                TcEnum.GetTextForEnum<TeBusinessMasterFilter>(TeBusinessMasterFilter.Duplicate_NICs_for_Employees_in_Salary_File));

            filterComboBox.Text = TcEnum.GetTextForEnum<TeBusinessMasterFilter>(TeBusinessMasterFilter.All);
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
                    string columnName = TcTheme.GridColumnNameFromkey(TcPropertyNames.NIC);
                    var nic = (string)selectedRow.Cells[columnName].Value;
                    List<TcBusinessMasterRow> data = new List<TcBusinessMasterRow>();

                    TeBusinessMasterFilter filter = TcEnum.GetEnumForText<TeBusinessMasterFilter>(TeBusinessMasterFilter.All, filterComboBox.Text);
                    switch (filter)
                    {
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

        private void FilterAndSearch()
        {
            try
            {
                string filterText = filterComboBox.Text;
                string searchText = searchTextBox.Text.Trim();

                var data = Engine.FilterAndSearch(filterText, searchText, master.SalaryForm.Table);
                AddRowsToGrid(dataGridView, data);

                SetStatus();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void AddRowsToGrid(DataGridView grid, List<TcBusinessMasterRow> data)
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

using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.UI.Auditors.Salary;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.Auditors.MasterData
{
    public partial class TcAuditorsMasterForm : Form
    {
        private TcAuditorsForm master;
        private string filePath = string.Empty;

        private BindingSource source = new BindingSource();
        private BindingSource duplicatesSource = new BindingSource();

        private TcBindingList<TcAuditorsMasterRow> all = new TcBindingList<TcAuditorsMasterRow>();

        public bool DataLoaded { get; set; }

        public TcAuditorsMasterTable MasterTable { get; private set; }

        public TcAuditorsMasterForm(TcAuditorsForm master, string filePath)
        {
            InitializeComponent();

            this.master     = master;
            this.filePath   = filePath;
            DataLoaded      = false;
            MasterTable     = new TcAuditorsMasterTable();

            dataGridView.AutoGenerateColumns = false;
            duplicatesDataGridView.AutoGenerateColumns = false;

            source.DataSource = new TcBindingList<TcAuditorsMasterRow>();
            duplicatesSource.DataSource = new TcBindingList<TcAuditorsMasterRow>();

            dataGridView.DataSource = source;
            duplicatesDataGridView.DataSource = duplicatesSource;

            statusLabel.Text = string.Empty;

            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeAuditorsMasterFilter>(TeAuditorsMasterFilter.All));

            ApplyTheme();
        }

        private void ApplyTheme()
        {
            TcTheme.FormatGrid(dataGridView);
            TcTheme.FormatGridDateDisplayColumn(dateOfJoinColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(basicSalaryColumn);

            TcTheme.FormatGrid(duplicatesDataGridView);
            TcTheme.FormatGridDateDisplayColumn(dDateOfJoinColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dBasicSalaryColumn);

            if (!TcVersions.IsEpfEtfSupported(master.SettingsForm.WorkingYearMonth))
            {
                initialsColumn.Visible  = false;
                lastNameColumn.Visible  = false;
                ocGradeColumn.Visible   = false;

                dInitialsColumn.Visible = false;
                dLastNameColumn.Visible = false;
                dOcGradeColumn.Visible  = false;
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
                duplicatesSource.DataSource = new TcBindingList<TcAuditorsMasterRow>();

                TcAuditorsMasterLoader loader = new TcAuditorsMasterLoader(master.SettingsForm.WorkingYearMonth);
                all = loader.LoadFromCSV(filePath);
                source.DataSource = all;

                MasterTable = new TcAuditorsMasterTable();
                MasterTable.Load(all);

                SetFilter();

                statusLabel.Text = string.Format("{0} record(s) found", source.Count);
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
            SetFilterData(MasterTable.HasEmployeeNumberDuplicates(), TcEnum.GetTextForEnum<TeAuditorsMasterFilter>(TeAuditorsMasterFilter.Employee_Number_Duplicates));
            SetFilterData(MasterTable.HasEmptyEmployeeNumbers(), TcEnum.GetTextForEnum<TeAuditorsMasterFilter>(TeAuditorsMasterFilter.Employee_Number_Empty));
            SetFilterData(HasDuplicateRowsForEmployeesWithEmployeeNumberInSalaryFile(), TcEnum.GetTextForEnum<TeAuditorsMasterFilter>(TeAuditorsMasterFilter.Duplicate_Employee_Numbers_for_Employees_in_Salary_File));

            SetFilterData(MasterTable.HasNICDuplicates(), TcEnum.GetTextForEnum<TeAuditorsMasterFilter>(TeAuditorsMasterFilter.NIC_Duplicates));
            SetFilterData(MasterTable.HasEmptyNIC(), TcEnum.GetTextForEnum<TeAuditorsMasterFilter>(TeAuditorsMasterFilter.NIC_Empty));
            SetFilterData(HasDuplicateRowsForEmployeesWithNICInSalaryFile(), TcEnum.GetTextForEnum<TeAuditorsMasterFilter>(TeAuditorsMasterFilter.Duplicate_NICs_for_Employees_in_Salary_File));

            filterComboBox.Text = TcEnum.GetTextForEnum<TeAuditorsMasterFilter>(TeAuditorsMasterFilter.All);
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
            fileInfoLabel.Text = string.Format("File: [{0}], Last Modified Date: [{1}]", fileInfo.FullName, fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private void filterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterAndSearch();
        }

        private bool HasDuplicateRowsForEmployeesWithEmployeeNumberInSalaryFile()
        {
            TcBindingList<TcAuditorsMasterRow> list = GetDuplicateRowsForEmployeesWithEmployeeNumberInSalaryFile();

            return (list.Count > 0);
        }

        private bool HasDuplicateRowsForEmployeesWithNICInSalaryFile()
        {
            TcBindingList<TcAuditorsMasterRow> list = GetDuplicateRowsForEmployeesWithNICInSalaryFile();

            return (list.Count > 0);
        }

        private TcBindingList<TcAuditorsMasterRow> GetDuplicateRowsForEmployeesWithEmployeeNumberInSalaryFile()
        {
            TcAuditorsSalaryTable table = master.SalaryForm.SalaryTable;
            TcBindingList<TcAuditorsMasterRow> list = MasterTable.GetEmployeeNumberDuplicateRowsForEmployeesInSalaryFile(table);

            return list;
        }

        private TcBindingList<TcAuditorsMasterRow> GetDuplicateRowsForEmployeesWithNICInSalaryFile()
        {
            TcAuditorsSalaryTable table = master.SalaryForm.SalaryTable;
            TcBindingList<TcAuditorsMasterRow> list = MasterTable.GetNICDuplicateRowsForEmployeesInSalaryFile(table);

            return list;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                TcAuditorsMasterRow masterRow = source.Current as TcAuditorsMasterRow;

                if (masterRow != null)
                {
                    TeAuditorsMasterFilter filter = TcEnum.GetEnumForText<TeAuditorsMasterFilter>(TeAuditorsMasterFilter.All, filterComboBox.Text);
                    switch (filter)
                    {
                        case TeAuditorsMasterFilter.All:
                            duplicatesSource.DataSource = MasterTable.GetDuplicates(masterRow.EmployeeNumber, masterRow.NIC);
                            break;

                        case TeAuditorsMasterFilter.NIC_Duplicates:
                        case TeAuditorsMasterFilter.Duplicate_NICs_for_Employees_in_Salary_File:
                            duplicatesSource.DataSource = MasterTable.GetNICDuplicates(masterRow.NIC);
                            break;

                        case TeAuditorsMasterFilter.Employee_Number_Duplicates:
                        case TeAuditorsMasterFilter.Duplicate_Employee_Numbers_for_Employees_in_Salary_File:
                            duplicatesSource.DataSource = MasterTable.GetEmployeeNumberDuplicates(masterRow.EmployeeNumber);
                            break;

                        case TeAuditorsMasterFilter.NIC_Empty:
                        case TeAuditorsMasterFilter.Employee_Number_Empty:
                        default:
                            duplicatesSource.DataSource = new TcBindingList<TcAuditorsMasterRow>();
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
                TeAuditorsMasterFilter filter = TcEnum.GetEnumForText<TeAuditorsMasterFilter>(TeAuditorsMasterFilter.All, filterComboBox.Text);
                switch (filter)
                {
                    case TeAuditorsMasterFilter.All:
                        source.DataSource = all;
                        break;

                    case TeAuditorsMasterFilter.NIC_Duplicates:
                        source.DataSource = MasterTable.GetNICDuplicates();
                        break;

                    case TeAuditorsMasterFilter.NIC_Empty:
                        source.DataSource = MasterTable.GetNICEmpltyList();
                        break;

                    case TeAuditorsMasterFilter.Employee_Number_Duplicates:
                        source.DataSource = MasterTable.GetEmployeeNumberDuplicates();
                        break;

                    case TeAuditorsMasterFilter.Employee_Number_Empty:
                        source.DataSource = MasterTable.GetEmployeeNumberEmpltyList();
                        break;

                    case TeAuditorsMasterFilter.Duplicate_NICs_for_Employees_in_Salary_File:
                        source.DataSource = GetDuplicateRowsForEmployeesWithNICInSalaryFile();
                        break;

                     case TeAuditorsMasterFilter.Duplicate_Employee_Numbers_for_Employees_in_Salary_File:
                        source.DataSource = GetDuplicateRowsForEmployeesWithEmployeeNumberInSalaryFile();
                        break;

                    default:
                        break;
                }

                Search();

                statusLabel.Text = string.Format("{0} record(s) found", source.Count);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void Search()
        {
            string searchText = searchTextBox.Text.Trim();
            TcSearchHelper<TcAuditorsMasterRow> searchHelper = new TcSearchHelper<TcAuditorsMasterRow>();

            source.DataSource = searchHelper.Search(source, searchText);
        }
    }
}

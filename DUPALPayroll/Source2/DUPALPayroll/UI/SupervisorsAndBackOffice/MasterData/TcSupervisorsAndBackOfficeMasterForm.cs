using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.UI.SupervisorsAndBackOffice.Salary;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-10-01

namespace DUPALPayroll.UI.SupervisorsAndBackOffice.MasterData
{
    public partial class TcSupervisorsAndBackOfficeMasterForm : Form
    {
        private TcSupervisorsAndBackOfficeForm master;
        private string filePath = string.Empty;

        private BindingSource source = new BindingSource();
        private BindingSource duplicatesSource = new BindingSource();

        private TcBindingList<TcSupervisorsAndBackOfficeMasterRow> all = new TcBindingList<TcSupervisorsAndBackOfficeMasterRow>();

        public bool DataLoaded { get; set; }

        public TcSupervisorsAndBackOfficeMasterTable MasterTable { get; private set; }

        public TcSupervisorsAndBackOfficeMasterForm(TcSupervisorsAndBackOfficeForm master, string filePath)
        {
            InitializeComponent();

            this.master     = master;
            this.filePath   = filePath;
            DataLoaded      = false;
            MasterTable     = new TcSupervisorsAndBackOfficeMasterTable();

            dataGridView.AutoGenerateColumns = false;
            duplicatesDataGridView.AutoGenerateColumns = false;

            source.DataSource = new TcBindingList<TcSupervisorsAndBackOfficeMasterRow>();
            duplicatesSource.DataSource = new TcBindingList<TcSupervisorsAndBackOfficeMasterRow>();

            dataGridView.DataSource = source;
            duplicatesDataGridView.DataSource = duplicatesSource;

            statusLabel.Text = string.Empty;

            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeSupervisorsAndBackOfficeMasterFilter>(TeSupervisorsAndBackOfficeMasterFilter.All));

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
                duplicatesSource.DataSource = new TcBindingList<TcSupervisorsAndBackOfficeMasterRow>();

                TcSupervisorsAndBackOfficeMasterLoader loader = new TcSupervisorsAndBackOfficeMasterLoader(master.SettingsForm.WorkingYearMonth);
                all = loader.LoadFromCSV(filePath);
                source.DataSource = all;

                MasterTable = new TcSupervisorsAndBackOfficeMasterTable();
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
            SetFilterData(MasterTable.HasEmployeeNumberDuplicates(), TcEnum.GetTextForEnum<TeSupervisorsAndBackOfficeMasterFilter>(TeSupervisorsAndBackOfficeMasterFilter.Employee_Number_Duplicates));
            SetFilterData(MasterTable.HasEmptyEmployeeNumbers(), TcEnum.GetTextForEnum<TeSupervisorsAndBackOfficeMasterFilter>(TeSupervisorsAndBackOfficeMasterFilter.Employee_Number_Empty));
            SetFilterData(HasDuplicateRowsForEmployeesWithEmployeeNumberInSalaryFile(), TcEnum.GetTextForEnum<TeSupervisorsAndBackOfficeMasterFilter>(TeSupervisorsAndBackOfficeMasterFilter.Duplicate_Employee_Numbers_for_Employees_in_Salary_File));

            SetFilterData(MasterTable.HasNICDuplicates(), TcEnum.GetTextForEnum<TeSupervisorsAndBackOfficeMasterFilter>(TeSupervisorsAndBackOfficeMasterFilter.NIC_Duplicates));
            SetFilterData(MasterTable.HasEmptyNIC(), TcEnum.GetTextForEnum<TeSupervisorsAndBackOfficeMasterFilter>(TeSupervisorsAndBackOfficeMasterFilter.NIC_Empty));
            SetFilterData(HasDuplicateRowsForEmployeesWithNICInSalaryFile(), TcEnum.GetTextForEnum<TeSupervisorsAndBackOfficeMasterFilter>(TeSupervisorsAndBackOfficeMasterFilter.Duplicate_NICs_for_Employees_in_Salary_File));

            filterComboBox.Text = TcEnum.GetTextForEnum<TeSupervisorsAndBackOfficeMasterFilter>(TeSupervisorsAndBackOfficeMasterFilter.All);
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
            TcBindingList<TcSupervisorsAndBackOfficeMasterRow> list = GetDuplicateRowsForEmployeesWithEmployeeNumberInSalaryFile();

            return (list.Count > 0);
        }

        private bool HasDuplicateRowsForEmployeesWithNICInSalaryFile()
        {
            TcBindingList<TcSupervisorsAndBackOfficeMasterRow> list = GetDuplicateRowsForEmployeesWithNICInSalaryFile();

            return (list.Count > 0);
        }

        private TcBindingList<TcSupervisorsAndBackOfficeMasterRow> GetDuplicateRowsForEmployeesWithEmployeeNumberInSalaryFile()
        {
            TcSupervisorsAndBackOfficeSalaryTable table = master.SalaryForm.SalaryTable;
            TcBindingList<TcSupervisorsAndBackOfficeMasterRow> list = MasterTable.GetEmployeeNumberDuplicateRowsForEmployeesInSalaryFile(table);

            return list;
        }

        private TcBindingList<TcSupervisorsAndBackOfficeMasterRow> GetDuplicateRowsForEmployeesWithNICInSalaryFile()
        {
            TcSupervisorsAndBackOfficeSalaryTable table = master.SalaryForm.SalaryTable;
            TcBindingList<TcSupervisorsAndBackOfficeMasterRow> list = MasterTable.GetNICDuplicateRowsForEmployeesInSalaryFile(table);

            return list;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                TcSupervisorsAndBackOfficeMasterRow masterRow = source.Current as TcSupervisorsAndBackOfficeMasterRow;

                if (masterRow != null)
                {
                    TeSupervisorsAndBackOfficeMasterFilter filter = TcEnum.GetEnumForText<TeSupervisorsAndBackOfficeMasterFilter>(TeSupervisorsAndBackOfficeMasterFilter.All, filterComboBox.Text);
                    switch (filter)
                    {
                        case TeSupervisorsAndBackOfficeMasterFilter.All:
                            duplicatesSource.DataSource = MasterTable.GetDuplicates(masterRow.EmployeeNumber, masterRow.NIC);
                            break;

                        case TeSupervisorsAndBackOfficeMasterFilter.NIC_Duplicates:
                        case TeSupervisorsAndBackOfficeMasterFilter.Duplicate_NICs_for_Employees_in_Salary_File:
                            duplicatesSource.DataSource = MasterTable.GetNICDuplicates(masterRow.NIC);
                            break;

                        case TeSupervisorsAndBackOfficeMasterFilter.Employee_Number_Duplicates:
                        case TeSupervisorsAndBackOfficeMasterFilter.Duplicate_Employee_Numbers_for_Employees_in_Salary_File:
                            duplicatesSource.DataSource = MasterTable.GetEmployeeNumberDuplicates(masterRow.EmployeeNumber);
                            break;

                        case TeSupervisorsAndBackOfficeMasterFilter.NIC_Empty:
                        case TeSupervisorsAndBackOfficeMasterFilter.Employee_Number_Empty:
                        default:
                            duplicatesSource.DataSource = new TcBindingList<TcSupervisorsAndBackOfficeMasterRow>();
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
                TeSupervisorsAndBackOfficeMasterFilter filter = TcEnum.GetEnumForText<TeSupervisorsAndBackOfficeMasterFilter>(TeSupervisorsAndBackOfficeMasterFilter.All, filterComboBox.Text);
                switch (filter)
                {
                    case TeSupervisorsAndBackOfficeMasterFilter.All:
                        source.DataSource = all;
                        break;

                    case TeSupervisorsAndBackOfficeMasterFilter.NIC_Duplicates:
                        source.DataSource = MasterTable.GetNICDuplicates();
                        break;

                    case TeSupervisorsAndBackOfficeMasterFilter.NIC_Empty:
                        source.DataSource = MasterTable.GetNICEmpltyList();
                        break;

                    case TeSupervisorsAndBackOfficeMasterFilter.Employee_Number_Duplicates:
                        source.DataSource = MasterTable.GetEmployeeNumberDuplicates();
                        break;

                    case TeSupervisorsAndBackOfficeMasterFilter.Employee_Number_Empty:
                        source.DataSource = MasterTable.GetEmployeeNumberEmpltyList();
                        break;

                    case TeSupervisorsAndBackOfficeMasterFilter.Duplicate_NICs_for_Employees_in_Salary_File:
                        source.DataSource = GetDuplicateRowsForEmployeesWithNICInSalaryFile();
                        break;

                     case TeSupervisorsAndBackOfficeMasterFilter.Duplicate_Employee_Numbers_for_Employees_in_Salary_File:
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
            TcSearchHelper<TcSupervisorsAndBackOfficeMasterRow> searchHelper = new TcSearchHelper<TcSupervisorsAndBackOfficeMasterRow>();

            source.DataSource = searchHelper.Search(source, searchText);
        }
    }
}

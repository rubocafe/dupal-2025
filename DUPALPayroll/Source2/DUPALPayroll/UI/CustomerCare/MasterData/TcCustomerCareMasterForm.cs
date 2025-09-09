using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.UI.CustomerCare.Salary;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-09-17

namespace DUPALPayroll.UI.CustomerCare.MasterData
{
    public partial class TcCustomerCareMasterForm : Form
    {
        private TcCustomerCareForm master;
        private string filePath = string.Empty;

        private BindingSource source = new BindingSource();
        private BindingSource duplicatesSource = new BindingSource();

        private TcBindingList<TcCustomerCareMasterRow> all = new TcBindingList<TcCustomerCareMasterRow>();

        public bool DataLoaded { get; set; }

        public TcCustomerCareMasterTable MasterTable { get; private set; }

        public TcCustomerCareMasterForm(TcCustomerCareForm master, string filePath)
        {
            InitializeComponent();

            this.master     = master;
            this.filePath   = filePath;
            DataLoaded      = false;
            MasterTable     = new TcCustomerCareMasterTable();

            dataGridView.AutoGenerateColumns = false;
            duplicatesDataGridView.AutoGenerateColumns = false;

            source.DataSource = new TcBindingList<TcCustomerCareMasterRow>();
            duplicatesSource.DataSource = new TcBindingList<TcCustomerCareMasterRow>();

            dataGridView.DataSource = source;
            duplicatesDataGridView.DataSource = duplicatesSource;

            statusLabel.Text = string.Empty;

            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeCustomerCareMasterFilter>(TeCustomerCareMasterFilter.All));

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
                duplicatesSource.DataSource = new TcBindingList<TcCustomerCareMasterRow>();

                TcCustomerCareMasterLoader loader = new TcCustomerCareMasterLoader(master.SettingsForm.WorkingYearMonth);
                all = loader.LoadFromCSV(filePath);
                source.DataSource = all;

                MasterTable = new TcCustomerCareMasterTable();
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
            SetFilterData(MasterTable.HasEmployeeNumberDuplicates(), TcEnum.GetTextForEnum<TeCustomerCareMasterFilter>(TeCustomerCareMasterFilter.Employee_Number_Duplicates));
            SetFilterData(MasterTable.HasEmptyEmployeeNumbers(), TcEnum.GetTextForEnum<TeCustomerCareMasterFilter>(TeCustomerCareMasterFilter.Employee_Number_Empty));
            SetFilterData(HasDuplicateRowsForEmployeesWithEmployeeNumberInSalaryFile(), TcEnum.GetTextForEnum<TeCustomerCareMasterFilter>(TeCustomerCareMasterFilter.Duplicate_Employee_Numbers_for_Employees_in_Salary_File));

            SetFilterData(MasterTable.HasNICDuplicates(), TcEnum.GetTextForEnum<TeCustomerCareMasterFilter>(TeCustomerCareMasterFilter.NIC_Duplicates));
            SetFilterData(MasterTable.HasEmptyNIC(), TcEnum.GetTextForEnum<TeCustomerCareMasterFilter>(TeCustomerCareMasterFilter.NIC_Empty));
            SetFilterData(HasDuplicateRowsForEmployeesWithNICInSalaryFile(), TcEnum.GetTextForEnum<TeCustomerCareMasterFilter>(TeCustomerCareMasterFilter.Duplicate_NICs_for_Employees_in_Salary_File));

            filterComboBox.Text = TcEnum.GetTextForEnum<TeCustomerCareMasterFilter>(TeCustomerCareMasterFilter.All);
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
            TcBindingList<TcCustomerCareMasterRow> list = GetDuplicateRowsForEmployeesWithEmployeeNumberInSalaryFile();

            return (list.Count > 0);
        }

        private bool HasDuplicateRowsForEmployeesWithNICInSalaryFile()
        {
            TcBindingList<TcCustomerCareMasterRow> list = GetDuplicateRowsForEmployeesWithNICInSalaryFile();

            return (list.Count > 0);
        }

        private TcBindingList<TcCustomerCareMasterRow> GetDuplicateRowsForEmployeesWithEmployeeNumberInSalaryFile()
        {
            TcCustomerCareSalaryTable table = master.SalaryForm.SalaryTable;
            TcBindingList<TcCustomerCareMasterRow> list = MasterTable.GetEmployeeNumberDuplicateRowsForEmployeesInSalaryFile(table);

            return list;
        }

        private TcBindingList<TcCustomerCareMasterRow> GetDuplicateRowsForEmployeesWithNICInSalaryFile()
        {
            TcCustomerCareSalaryTable table = master.SalaryForm.SalaryTable;
            TcBindingList<TcCustomerCareMasterRow> list = MasterTable.GetNICDuplicateRowsForEmployeesInSalaryFile(table);

            return list;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                TcCustomerCareMasterRow masterRow = source.Current as TcCustomerCareMasterRow;

                if (masterRow != null)
                {
                    TeCustomerCareMasterFilter filter = TcEnum.GetEnumForText<TeCustomerCareMasterFilter>(TeCustomerCareMasterFilter.All, filterComboBox.Text);
                    switch (filter)
                    {
                        case TeCustomerCareMasterFilter.All:
                            duplicatesSource.DataSource = MasterTable.GetDuplicates(masterRow.EmployeeNumber, masterRow.NIC);
                            break;

                        case TeCustomerCareMasterFilter.NIC_Duplicates:
                        case TeCustomerCareMasterFilter.Duplicate_NICs_for_Employees_in_Salary_File:
                            duplicatesSource.DataSource = MasterTable.GetNICDuplicates(masterRow.NIC);
                            break;

                        case TeCustomerCareMasterFilter.Employee_Number_Duplicates:
                        case TeCustomerCareMasterFilter.Duplicate_Employee_Numbers_for_Employees_in_Salary_File:
                            duplicatesSource.DataSource = MasterTable.GetEmployeeNumberDuplicates(masterRow.EmployeeNumber);
                            break;

                        case TeCustomerCareMasterFilter.NIC_Empty:
                        case TeCustomerCareMasterFilter.Employee_Number_Empty:
                        default:
                            duplicatesSource.DataSource = new TcBindingList<TcCustomerCareMasterRow>();
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
                TeCustomerCareMasterFilter filter = TcEnum.GetEnumForText<TeCustomerCareMasterFilter>(TeCustomerCareMasterFilter.All, filterComboBox.Text);
                switch (filter)
                {
                    case TeCustomerCareMasterFilter.All:
                        source.DataSource = all;
                        break;

                    case TeCustomerCareMasterFilter.NIC_Duplicates:
                        source.DataSource = MasterTable.GetNICDuplicates();
                        break;

                    case TeCustomerCareMasterFilter.NIC_Empty:
                        source.DataSource = MasterTable.GetNICEmpltyList();
                        break;

                    case TeCustomerCareMasterFilter.Employee_Number_Duplicates:
                        source.DataSource = MasterTable.GetEmployeeNumberDuplicates();
                        break;

                    case TeCustomerCareMasterFilter.Employee_Number_Empty:
                        source.DataSource = MasterTable.GetEmployeeNumberEmpltyList();
                        break;

                    case TeCustomerCareMasterFilter.Duplicate_NICs_for_Employees_in_Salary_File:
                        source.DataSource = GetDuplicateRowsForEmployeesWithNICInSalaryFile();
                        break;

                     case TeCustomerCareMasterFilter.Duplicate_Employee_Numbers_for_Employees_in_Salary_File:
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
            TcSearchHelper<TcCustomerCareMasterRow> searchHelper = new TcSearchHelper<TcCustomerCareMasterRow>();

            source.DataSource = searchHelper.Search(source, searchText);
        }
    }
}

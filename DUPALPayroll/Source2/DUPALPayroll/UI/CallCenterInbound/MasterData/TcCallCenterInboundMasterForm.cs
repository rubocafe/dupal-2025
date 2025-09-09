using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.UI.CallCenterInbound.Salary;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.CallCenterInbound.MasterData
{
    public partial class TcCallCenterInboundMasterForm : Form
    {
        private TcCallCenterInboundForm master;
        private string filePath = string.Empty;

        private BindingSource source = new BindingSource();
        private BindingSource duplicatesSource = new BindingSource();

        private TcBindingList<TcCallCenterInboundMasterRow> all = new TcBindingList<TcCallCenterInboundMasterRow>();

        public bool DataLoaded { get; set; }

        public TcCallCenterInboundMasterTable MasterTable { get; private set; }

        public TcCallCenterInboundMasterForm(TcCallCenterInboundForm master, string filePath)
        {
            InitializeComponent();

            this.master     = master;
            this.filePath   = filePath;
            DataLoaded      = false;
            MasterTable     = new TcCallCenterInboundMasterTable();

            dataGridView.AutoGenerateColumns = false;
            duplicatesDataGridView.AutoGenerateColumns = false;

            source.DataSource = new TcBindingList<TcCallCenterInboundMasterRow>();
            duplicatesSource.DataSource = new TcBindingList<TcCallCenterInboundMasterRow>();

            dataGridView.DataSource = source;
            duplicatesDataGridView.DataSource = duplicatesSource;

            statusLabel.Text = string.Empty;

            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeCallCenterInboundMasterFilter>(TeCallCenterInboundMasterFilter.All));

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
                initialsColumn.Visible = false;
                lastNameColumn.Visible = false;
                ocGradeColumn.Visible = false;

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
                duplicatesSource.DataSource = new TcBindingList<TcCallCenterInboundMasterRow>();

                TcCallCenterInboundMasterLoader loader = new TcCallCenterInboundMasterLoader(master.SettingsForm.WorkingYearMonth);
                all = loader.LoadFromCSV(filePath);
                source.DataSource = all;

                MasterTable = new TcCallCenterInboundMasterTable();
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
            SetFilterData(MasterTable.HasEmployeeNumberDuplicates(), TcEnum.GetTextForEnum<TeCallCenterInboundMasterFilter>(TeCallCenterInboundMasterFilter.Employee_Number_Duplicates));
            SetFilterData(MasterTable.HasEmptyEmployeeNumbers(), TcEnum.GetTextForEnum<TeCallCenterInboundMasterFilter>(TeCallCenterInboundMasterFilter.Employee_Number_Empty));
            SetFilterData(HasDuplicateRowsForEmployeesWithEmployeeNumberInSalaryFile(), TcEnum.GetTextForEnum<TeCallCenterInboundMasterFilter>(TeCallCenterInboundMasterFilter.Duplicate_Employee_Numbers_for_Employees_in_Salary_File));

            SetFilterData(MasterTable.HasNICDuplicates(), TcEnum.GetTextForEnum<TeCallCenterInboundMasterFilter>(TeCallCenterInboundMasterFilter.NIC_Duplicates));
            SetFilterData(MasterTable.HasEmptyNIC(), TcEnum.GetTextForEnum<TeCallCenterInboundMasterFilter>(TeCallCenterInboundMasterFilter.NIC_Empty));
            SetFilterData(HasDuplicateRowsForEmployeesWithNICInSalaryFile(), TcEnum.GetTextForEnum<TeCallCenterInboundMasterFilter>(TeCallCenterInboundMasterFilter.Duplicate_NICs_for_Employees_in_Salary_File));

            filterComboBox.Text = TcEnum.GetTextForEnum<TeCallCenterInboundMasterFilter>(TeCallCenterInboundMasterFilter.All);
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
            TcBindingList<TcCallCenterInboundMasterRow> list = GetDuplicateRowsForEmployeesWithEmployeeNumberInSalaryFile();

            return (list.Count > 0);
        }

        private bool HasDuplicateRowsForEmployeesWithNICInSalaryFile()
        {
            TcBindingList<TcCallCenterInboundMasterRow> list = GetDuplicateRowsForEmployeesWithNICInSalaryFile();

            return (list.Count > 0);
        }

        private TcBindingList<TcCallCenterInboundMasterRow> GetDuplicateRowsForEmployeesWithEmployeeNumberInSalaryFile()
        {
            TcCallCenterInboundSalaryTable table = master.SalaryForm.SalaryTable;
            TcBindingList<TcCallCenterInboundMasterRow> list = MasterTable.GetEmployeeNumberDuplicateRowsForEmployeesInSalaryFile(table);

            return list;
        }

        private TcBindingList<TcCallCenterInboundMasterRow> GetDuplicateRowsForEmployeesWithNICInSalaryFile()
        {
            TcCallCenterInboundSalaryTable table = master.SalaryForm.SalaryTable;
            TcBindingList<TcCallCenterInboundMasterRow> list = MasterTable.GetNICDuplicateRowsForEmployeesInSalaryFile(table);

            return list;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                TcCallCenterInboundMasterRow masterRow = source.Current as TcCallCenterInboundMasterRow;

                if (masterRow != null)
                {
                    TeCallCenterInboundMasterFilter filter = TcEnum.GetEnumForText<TeCallCenterInboundMasterFilter>(TeCallCenterInboundMasterFilter.All, filterComboBox.Text);
                    switch (filter)
                    {
                        case TeCallCenterInboundMasterFilter.All:
                            duplicatesSource.DataSource = MasterTable.GetDuplicates(masterRow.EmployeeNumber, masterRow.NIC);
                            break;

                        case TeCallCenterInboundMasterFilter.NIC_Duplicates:
                        case TeCallCenterInboundMasterFilter.Duplicate_NICs_for_Employees_in_Salary_File:
                            duplicatesSource.DataSource = MasterTable.GetNICDuplicates(masterRow.NIC);
                            break;

                        case TeCallCenterInboundMasterFilter.Employee_Number_Duplicates:
                        case TeCallCenterInboundMasterFilter.Duplicate_Employee_Numbers_for_Employees_in_Salary_File:
                            duplicatesSource.DataSource = MasterTable.GetEmployeeNumberDuplicates(masterRow.EmployeeNumber);
                            break;

                        case TeCallCenterInboundMasterFilter.NIC_Empty:
                        case TeCallCenterInboundMasterFilter.Employee_Number_Empty:
                        default:
                            duplicatesSource.DataSource = new TcBindingList<TcCallCenterInboundMasterRow>();
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
                TeCallCenterInboundMasterFilter filter = TcEnum.GetEnumForText<TeCallCenterInboundMasterFilter>(TeCallCenterInboundMasterFilter.All, filterComboBox.Text);
                switch (filter)
                {
                    case TeCallCenterInboundMasterFilter.All:
                        source.DataSource = all;
                        break;

                    case TeCallCenterInboundMasterFilter.NIC_Duplicates:
                        source.DataSource = MasterTable.GetNICDuplicates();
                        break;

                    case TeCallCenterInboundMasterFilter.NIC_Empty:
                        source.DataSource = MasterTable.GetNICEmpltyList();
                        break;

                    case TeCallCenterInboundMasterFilter.Employee_Number_Duplicates:
                        source.DataSource = MasterTable.GetEmployeeNumberDuplicates();
                        break;

                    case TeCallCenterInboundMasterFilter.Employee_Number_Empty:
                        source.DataSource = MasterTable.GetEmployeeNumberEmpltyList();
                        break;

                    case TeCallCenterInboundMasterFilter.Duplicate_NICs_for_Employees_in_Salary_File:
                        source.DataSource = GetDuplicateRowsForEmployeesWithNICInSalaryFile();
                        break;

                     case TeCallCenterInboundMasterFilter.Duplicate_Employee_Numbers_for_Employees_in_Salary_File:
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
            TcSearchHelper<TcCallCenterInboundMasterRow> searchHelper = new TcSearchHelper<TcCallCenterInboundMasterRow>();

            source.DataSource = searchHelper.Search(source, searchText);
        }
    }
}

using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.UI.CommissionAgents.Commissions;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-08-26

namespace DUPALPayroll.UI.CommissionAgents.MasterData
{
    public partial class TcCommissionAgentsMasterForm : Form
    {
        private TcCommissionAgentsForm master;
        private string filePath = string.Empty;

        private BindingSource source = new BindingSource();
        private BindingSource duplicatesSource = new BindingSource();

        private TcBindingList<TcCommissionAgentsMasterRow> all = new TcBindingList<TcCommissionAgentsMasterRow>();

        public bool DataLoaded { get; set; }

        public TcCommissionAgentsMasterTable MasterTable { get; private set; }

        public TcCommissionAgentsMasterForm(TcCommissionAgentsForm master, string filePath)
        {
            InitializeComponent();

            this.master     = master;
            this.filePath   = filePath;
            DataLoaded      = false;
            MasterTable     = new TcCommissionAgentsMasterTable();

            dataGridView.AutoGenerateColumns = false;
            duplicatesDataGridView.AutoGenerateColumns = false;

            source.DataSource = new TcBindingList<TcCommissionAgentsMasterRow>();
            duplicatesSource.DataSource = new TcBindingList<TcCommissionAgentsMasterRow>();

            dataGridView.DataSource = source;
            duplicatesDataGridView.DataSource = duplicatesSource;

            statusLabel.Text = string.Empty;

            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeCommissionAgentsMasterFilter>(TeCommissionAgentsMasterFilter.All));

            ApplyTheme();
        }

        private void ApplyTheme()
        {
            TcTheme.FormatGrid(dataGridView);
            TcTheme.FormatGridDateDisplayColumn(dateOfJoinColumn);

            TcTheme.FormatGrid(duplicatesDataGridView);
            TcTheme.FormatGridDateDisplayColumn(duplicatesDateOfJoinColumn);

            if (!TcVersions.IsEpfEtfSupported(master.SettingsForm.WorkingYearMonth))
            {
                initialsColumn.Visible          = false;
                lastNameColumn.Visible          = false;
                employeeNumberColumn.Visible    = false;
                ocGradeColumn.Visible           = false;

                dInitialsColumn.Visible         = false;
                dLastNameColumn.Visible         = false;
                dEmployeeNumberColumn.Visible   = false;
                dOcGradeColumn.Visible          = false;
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
                duplicatesSource.DataSource = new TcBindingList<TcCommissionAgentsMasterRow>();

                TcCommissionAgentsMasterDataLoader loader = new TcCommissionAgentsMasterDataLoader(master.SettingsForm.WorkingYearMonth);
                all = loader.LoadFromCSV(filePath);
                source.DataSource = all;

                MasterTable = new TcCommissionAgentsMasterTable();
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
            SetFilterData(MasterTable.HasNICDuplicates(), TcEnum.GetTextForEnum<TeCommissionAgentsMasterFilter>(TeCommissionAgentsMasterFilter.NIC_Duplicates));
            SetFilterData(MasterTable.HasEmptyNIC(), TcEnum.GetTextForEnum<TeCommissionAgentsMasterFilter>(TeCommissionAgentsMasterFilter.NIC_Empty));
            SetFilterData(MasterTable.HasVNDuplicates(), TcEnum.GetTextForEnum<TeCommissionAgentsMasterFilter>(TeCommissionAgentsMasterFilter.Virtual_Number_Duplicates));
            SetFilterData(MasterTable.HasEmptyVN(), TcEnum.GetTextForEnum<TeCommissionAgentsMasterFilter>(TeCommissionAgentsMasterFilter.Virtual_Number_Empty));
            SetFilterData(HasDuplicateRowsForAgentsWithVirtualNumberInCommissionsFile(), TcEnum.GetTextForEnum<TeCommissionAgentsMasterFilter>(TeCommissionAgentsMasterFilter.Duplicate_Virtual_Numbers_for_Agents_in_Commissions_File));
            SetFilterData(HasDuplicateRowsForAgentsWithNICInCommissionsFile(), TcEnum.GetTextForEnum<TeCommissionAgentsMasterFilter>(TeCommissionAgentsMasterFilter.Duplicate_NICs_for_Agents_in_Commissions_File));
            SetFilterData(HasDuplicateNICRowsForAgentsWithoutVNInCommissionsFile(), TcEnum.GetTextForEnum<TeCommissionAgentsMasterFilter>(TeCommissionAgentsMasterFilter.Duplicate_NICs_with_Empty_Virtual_Numbers_for_Agents_in_Commissions_File));

            filterComboBox.Text = TcEnum.GetTextForEnum<TeCommissionAgentsMasterFilter>(TeCommissionAgentsMasterFilter.All);
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

        private bool HasDuplicateRowsForAgentsWithVirtualNumberInCommissionsFile()
        {
            TcBindingList<TcCommissionAgentsMasterRow> list = GetDuplicateRowsForAgentsWithVirtualNumberInCommissionsFile();

            return (list.Count > 0);
        }

        private bool HasDuplicateRowsForAgentsWithNICInCommissionsFile()
        {
            TcBindingList<TcCommissionAgentsMasterRow> list = GetDuplicateRowsForAgentsWithNICInCommissionsFile();

            return (list.Count > 0);
        }

        private bool HasDuplicateNICRowsForAgentsWithoutVNInCommissionsFile()
        {
            TcBindingList<TcCommissionAgentsMasterRow> list = GetDuplicateNICRowsForAgentsWithoutVNInCommissionsFile();

            return (list.Count > 0);
        }

        private TcBindingList<TcCommissionAgentsMasterRow> GetDuplicateRowsForAgentsWithVirtualNumberInCommissionsFile()
        {
            TcCommissionsTable commissionTable = master.CommissionsForm.CommissionsTable;
            TcBindingList<TcCommissionAgentsMasterRow> list = MasterTable.GetVNDuplicateRowsForAgentsInCommissionsFile(commissionTable);

            return list;
        }

        private TcBindingList<TcCommissionAgentsMasterRow> GetDuplicateRowsForAgentsWithNICInCommissionsFile()
        {
            TcCommissionsTable commissionTable = master.CommissionsForm.CommissionsTable;
            TcBindingList<TcCommissionAgentsMasterRow> list = MasterTable.GetNICDuplicateRowsForAgentsInCommissionsFile(commissionTable);

            return list;
        }

        private TcBindingList<TcCommissionAgentsMasterRow> GetDuplicateNICRowsForAgentsWithoutVNInCommissionsFile()
        {
            TcCommissionsTable commissionTable = master.CommissionsForm.CommissionsTable;
            TcBindingList<TcCommissionAgentsMasterRow> list = MasterTable.GetNICDuplicateRowsForAgentsWithoutVNInCommissionsFile(commissionTable);

            return list;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                TcCommissionAgentsMasterRow masterRow = source.Current as TcCommissionAgentsMasterRow;

                if (masterRow != null)
                {
                    TeCommissionAgentsMasterFilter filter = TcEnum.GetEnumForText<TeCommissionAgentsMasterFilter>(TeCommissionAgentsMasterFilter.All, filterComboBox.Text);
                    switch (filter)
                    {
                        case TeCommissionAgentsMasterFilter.All:
                            duplicatesSource.DataSource = MasterTable.GetDuplicates(masterRow.VirtualNumber, masterRow.NIC);
                            break;

                        case TeCommissionAgentsMasterFilter.NIC_Duplicates:
                        case TeCommissionAgentsMasterFilter.Duplicate_NICs_for_Agents_in_Commissions_File:
                        case TeCommissionAgentsMasterFilter.Duplicate_NICs_with_Empty_Virtual_Numbers_for_Agents_in_Commissions_File:
                            duplicatesSource.DataSource = MasterTable.GetNICDuplicates(masterRow.NIC);
                            break;

                        case TeCommissionAgentsMasterFilter.Virtual_Number_Duplicates:
                        case TeCommissionAgentsMasterFilter.Duplicate_Virtual_Numbers_for_Agents_in_Commissions_File:
                            duplicatesSource.DataSource = MasterTable.GetVNDuplicates(masterRow.VirtualNumber);
                            break;

                        case TeCommissionAgentsMasterFilter.NIC_Empty:
                        case TeCommissionAgentsMasterFilter.Virtual_Number_Empty:
                        default:
                            duplicatesSource.DataSource = new TcBindingList<TcCommissionAgentsMasterRow>();
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
                TeCommissionAgentsMasterFilter filter = TcEnum.GetEnumForText<TeCommissionAgentsMasterFilter>(TeCommissionAgentsMasterFilter.All, filterComboBox.Text);
                switch (filter)
                {
                    case TeCommissionAgentsMasterFilter.All:
                        source.DataSource = all;
                        break;

                    case TeCommissionAgentsMasterFilter.NIC_Duplicates:
                        source.DataSource = MasterTable.GetNICDuplicates();
                        break;

                    case TeCommissionAgentsMasterFilter.Virtual_Number_Duplicates:
                        source.DataSource = MasterTable.GetVNDuplicates();
                        break;

                    case TeCommissionAgentsMasterFilter.NIC_Empty:
                        source.DataSource = MasterTable.GetNICEmpltyList();
                        break;

                    case TeCommissionAgentsMasterFilter.Virtual_Number_Empty:
                        source.DataSource = MasterTable.GetVNEmpltyList();
                        break;

                    case TeCommissionAgentsMasterFilter.Duplicate_Virtual_Numbers_for_Agents_in_Commissions_File:
                        source.DataSource = GetDuplicateRowsForAgentsWithVirtualNumberInCommissionsFile();
                        break;

                    case TeCommissionAgentsMasterFilter.Duplicate_NICs_for_Agents_in_Commissions_File:
                        source.DataSource = GetDuplicateRowsForAgentsWithNICInCommissionsFile();
                        break;

                    case TeCommissionAgentsMasterFilter.Duplicate_NICs_with_Empty_Virtual_Numbers_for_Agents_in_Commissions_File:
                        source.DataSource = GetDuplicateNICRowsForAgentsWithoutVNInCommissionsFile();
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
            TcSearchHelper<TcCommissionAgentsMasterRow> searchHelper = new TcSearchHelper<TcCommissionAgentsMasterRow>();

            source.DataSource = searchHelper.Search(source, searchText);
        }
    }
}

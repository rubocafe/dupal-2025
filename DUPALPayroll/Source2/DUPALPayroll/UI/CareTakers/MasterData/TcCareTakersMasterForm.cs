using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.UI.CareTakers.Payments;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-10-31

namespace DUPALPayroll.UI.CareTakers.MasterData
{
    public partial class TcCareTakersMasterForm : Form
    {
        private TcCareTakersForm master;
        private string filePath = string.Empty;

        private BindingSource source = new BindingSource();
        private BindingSource duplicatesSource = new BindingSource();

        private TcBindingList<TcCareTakersMasterRow> all = new TcBindingList<TcCareTakersMasterRow>();

        public bool DataLoaded { get; set; }

        public TcCareTakersMasterTable MasterTable { get; private set; }

        public TcCareTakersMasterForm(TcCareTakersForm master, string filePath)
        {
            InitializeComponent();

            this.master     = master;
            this.filePath   = filePath;
            DataLoaded      = false;
            MasterTable     = new TcCareTakersMasterTable();

            dataGridView.AutoGenerateColumns = false;
            duplicatesDataGridView.AutoGenerateColumns = false;

            source.DataSource = new TcBindingList<TcCareTakersMasterRow>();
            duplicatesSource.DataSource = new TcBindingList<TcCareTakersMasterRow>();

            dataGridView.DataSource = source;
            duplicatesDataGridView.DataSource = duplicatesSource;

            statusLabel.Text = string.Empty;

            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeCareTakersMasterFilter>(TeCareTakersMasterFilter.All));

            ApplyTheme();
        }

        private void ApplyTheme()
        {
            TcTheme.FormatGrid(dataGridView);

            TcTheme.FormatGrid(duplicatesDataGridView);
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
                duplicatesSource.DataSource = new TcBindingList<TcCareTakersMasterRow>();

                TcCareTakersMasterDataLoader loader = new TcCareTakersMasterDataLoader();
                all = loader.LoadFromCSV(filePath);
                source.DataSource = all;

                MasterTable = new TcCareTakersMasterTable();
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
            SetFilterData(MasterTable.HasNICDuplicates(), TcEnum.GetTextForEnum<TeCareTakersMasterFilter>(TeCareTakersMasterFilter.NIC_Duplicates));
            SetFilterData(MasterTable.HasEmptyNIC(), TcEnum.GetTextForEnum<TeCareTakersMasterFilter>(TeCareTakersMasterFilter.NIC_Empty));
            SetFilterData(HasDuplicateRowsForAgentsWithNICInCommissionsFile(), TcEnum.GetTextForEnum<TeCareTakersMasterFilter>(TeCareTakersMasterFilter.Duplicate_NICs_for_Agents_in_Commissions_File));

            filterComboBox.Text = TcEnum.GetTextForEnum<TeCareTakersMasterFilter>(TeCareTakersMasterFilter.All);
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

        private bool HasDuplicateRowsForAgentsWithNICInCommissionsFile()
        {
            TcBindingList<TcCareTakersMasterRow> list = GetDuplicateRowsForAgentsWithNICInCommissionsFile();

            return (list.Count > 0);
        }

        private TcBindingList<TcCareTakersMasterRow> GetDuplicateRowsForAgentsWithNICInCommissionsFile()
        {
            TcCareTakersPaymentsTable commissionTable = master.PymentsForm.CommissionsTable;
            TcBindingList<TcCareTakersMasterRow> list = MasterTable.GetNICDuplicateRowsForAgentsInCommissionsFile(commissionTable);

            return list;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                TcCareTakersMasterRow masterRow = source.Current as TcCareTakersMasterRow;

                if (masterRow != null)
                {
                    TeCareTakersMasterFilter filter = TcEnum.GetEnumForText<TeCareTakersMasterFilter>(TeCareTakersMasterFilter.All, filterComboBox.Text);
                    switch (filter)
                    {
                        case TeCareTakersMasterFilter.All:
                            duplicatesSource.DataSource = MasterTable.GetDuplicates(masterRow.NIC);
                            break;

                        case TeCareTakersMasterFilter.NIC_Duplicates:
                        case TeCareTakersMasterFilter.Duplicate_NICs_for_Agents_in_Commissions_File:
                            duplicatesSource.DataSource = MasterTable.GetNICDuplicates(masterRow.NIC);
                            break;

                        case TeCareTakersMasterFilter.NIC_Empty:
                        default:
                            duplicatesSource.DataSource = new TcBindingList<TcCareTakersMasterRow>();
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
                TeCareTakersMasterFilter filter = TcEnum.GetEnumForText<TeCareTakersMasterFilter>(TeCareTakersMasterFilter.All, filterComboBox.Text);
                switch (filter)
                {
                    case TeCareTakersMasterFilter.All:
                        source.DataSource = all;
                        break;

                    case TeCareTakersMasterFilter.NIC_Duplicates:
                        source.DataSource = MasterTable.GetNICDuplicates();
                        break;

                    case TeCareTakersMasterFilter.NIC_Empty:
                        source.DataSource = MasterTable.GetNICEmpltyList();
                        break;

                    case TeCareTakersMasterFilter.Duplicate_NICs_for_Agents_in_Commissions_File:
                        source.DataSource = GetDuplicateRowsForAgentsWithNICInCommissionsFile();
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
            TcSearchHelper<TcCareTakersMasterRow> searchHelper = new TcSearchHelper<TcCareTakersMasterRow>();

            source.DataSource = searchHelper.Search(source, searchText);
        }
    }
}

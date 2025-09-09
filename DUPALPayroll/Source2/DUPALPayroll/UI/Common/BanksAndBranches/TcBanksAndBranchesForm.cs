using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-08-26

namespace DUPALPayroll.UI.Common.BanksAndBranches
{
    public partial class TcBanksAndBranchesForm : Form
    {
        private string filePath = "C:\\DUPAL Payroll\\BanksAndBranchCodes.csv";

        private TcBindingList<TcBanksAndBranchesRow> all = new TcBindingList<TcBanksAndBranchesRow>();

        private BindingSource source = new BindingSource();
        private BindingSource duplicatesSource = new BindingSource();

        public bool DataLoaded { get; set; }
        public TcBanksAndBranchesTable BanksAndBranchesTable { get; private set; }

        public TcBanksAndBranchesForm(string filePath)
        {
            InitializeComponent();

            this.filePath           = filePath;
            BanksAndBranchesTable   = new TcBanksAndBranchesTable();
            DataLoaded              = false;

            dataGridView.AutoGenerateColumns = false;
            duplicatesDataGridView.AutoGenerateColumns = false;

            source.DataSource           = new TcBindingList<TcBanksAndBranchesRow>();
            duplicatesSource.DataSource = new TcBindingList<TcBanksAndBranchesRow>();

            dataGridView.DataSource             = source;
            duplicatesDataGridView.DataSource   = duplicatesSource;

            statusLabel.Text = string.Empty;

            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeBanksAndBranchesFilter>(TeBanksAndBranchesFilter.All));

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
                duplicatesSource.DataSource = new TcBindingList<TcBanksAndBranchesRow>();

                TcBanksAndBranchesLoader loader = new TcBanksAndBranchesLoader();
                all = loader.LoadFromCSV(filePath);
                source.DataSource = all;

                BanksAndBranchesTable = new TcBanksAndBranchesTable();
                BanksAndBranchesTable.Load(all);

                SetFilter();

                statusLabel.Text = string.Format("{0} record(s) found", source.Count);
                DataLoaded = true;
                //TcCommissionAgentsForm.ResetAnalyzeForm = true;

                SetFileInfo();
            }
            else
            {
                DataLoaded = false;
                string ex = string.Format("Bank and branchesData data file [{0}] does not exist", filePath);
                fileInfoLabel.Text = ex;
                throw new Exception(ex);
            }
        }

        private void SetFilter()
        {
            SetFilterData(BanksAndBranchesTable.HasDuplicates(), TcEnum.GetTextForEnum<TeBanksAndBranchesFilter>(TeBanksAndBranchesFilter.Duplicates));

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
            fileInfoLabel.Text = string.Format("File: [{0}], Last Modified Date: [{1}]", fileInfo.FullName, fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private void filterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterAndSearch();
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                TcBanksAndBranchesRow data = source.Current as TcBanksAndBranchesRow;
                if (data != null)
                {
                    TeBanksAndBranchesFilter filter = TcEnum.GetEnumForText<TeBanksAndBranchesFilter>(TeBanksAndBranchesFilter.All, filterComboBox.Text);
                    switch (filter)
                    {
                        case TeBanksAndBranchesFilter.All:
                        case TeBanksAndBranchesFilter.Duplicates:
                        default:
                            duplicatesSource.DataSource = BanksAndBranchesTable.GetDuplicates(data.Key);
                            break;
                    }
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

        private void FilterAndSearch()
        {
            try
            {
                TeBanksAndBranchesFilter filter = TcEnum.GetEnumForText<TeBanksAndBranchesFilter>(TeBanksAndBranchesFilter.All, filterComboBox.Text);
                switch (filter)
                {
                    case TeBanksAndBranchesFilter.All:
                        source.DataSource = all;
                        break;

                    case TeBanksAndBranchesFilter.Duplicates:
                        source.DataSource = BanksAndBranchesTable.GetDuplicatesList();
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
            TcSearchHelper<TcBanksAndBranchesRow> searchHelper = new TcSearchHelper<TcBanksAndBranchesRow>();

            source.DataSource = searchHelper.Search(source, searchText);
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
    }
}

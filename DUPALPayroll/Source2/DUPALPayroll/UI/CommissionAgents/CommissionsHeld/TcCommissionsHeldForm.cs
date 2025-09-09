using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.UI.CommissionAgents.Commissions;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-08-26

namespace DUPALPayroll.UI.CommissionAgents.CommissionsHeld
{
    public partial class TcCommissionsHeldForm : Form
    {
        private TcCommissionAgentsForm master;
        private string filePath = string.Empty;

        private TcBindingList<TcCommissionsHeldRow> all = new TcBindingList<TcCommissionsHeldRow>();

        private BindingSource source = new BindingSource();
        private BindingSource duplicatesSource = new BindingSource();

        public bool DataLoaded { get; set; }
        public TcCommissionsHeldTable CommissionsHeldTable { get; private set; }

        public TcCommissionsHeldForm(TcCommissionAgentsForm master, string filePath)
        {
            InitializeComponent();

            this.master             = master;
            this.filePath           = filePath;
            CommissionsHeldTable    = new TcCommissionsHeldTable();
            DataLoaded              = false;

            dataGridView.AutoGenerateColumns            = false;
            duplicatesDataGridView.AutoGenerateColumns  = false;

            source.DataSource           = new TcBindingList<TcCommissionsHeldRow>();
            duplicatesSource.DataSource = new TcBindingList<TcCommissionsHeldRow>();

            dataGridView.DataSource             = source;
            duplicatesDataGridView.DataSource   = duplicatesSource;

            statusLabel.Text = string.Empty;

            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeCommissionHeldFilter>(TeCommissionHeldFilter.All));

            ApplyTheme();
        }

        private void ApplyTheme()
        {
            TcTheme.FormatGrid(dataGridView);

            TcTheme.FormatGridCurrencyDisplayColumn(commissionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(holdColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(amountPaybleColumn);

            TcTheme.FormatGrid(duplicatesDataGridView);

            TcTheme.FormatGridCurrencyDisplayColumn(duplicatesCommissionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(duplicatesHoldColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(duplicatesAmountPaybleColumn);
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
                duplicatesSource.DataSource = new TcBindingList<TcCommissionsHeldRow>();

                TcCommissionsHeldLoader loader = new TcCommissionsHeldLoader();
                all = loader.LoadFromCSV(filePath);
                source.DataSource = all;

                CommissionsHeldTable = new TcCommissionsHeldTable();
                CommissionsHeldTable.Load(all);

                SetFilter();
                SetStatus();

                DataLoaded = true;

                SetFileInfo();
            }
            else
            {
                DataLoaded = false;
                string ex = string.Format("Commissions Held file [{0}] does not exist", filePath);
                fileInfoLabel.Text = ex;
                throw new Exception(ex);
            }
        }

        public void SetFilter()
        {
            SetFilterData(CommissionsHeldTable.HasDuplicates(), TcEnum.GetTextForEnum<TeCommissionHeldFilter>(TeCommissionHeldFilter.Virtual_Number_Duplicates));
            SetFilterData(CommissionsHeldTable.HasEmpty(), TcEnum.GetTextForEnum<TeCommissionHeldFilter>(TeCommissionHeldFilter.Virtual_Number_Empty));
            SetFilterData(HasCommissionsHeldRowsNotMappedWithCommissionsAgents(), TcEnum.GetTextForEnum<TeCommissionHeldFilter>(TeCommissionHeldFilter.No_Corresponding_Commissions_Agents));

            filterComboBox.Text = "All";
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
                fileInfoLabel.Text = "Click Load data button to load Commissions Held Data";
            }
            else
            {
                FileInfo fileInfo = new FileInfo(filePath);
                fileInfoLabel.Text = string.Format("File: [{0}], Last Modified Date: [{1}]", fileInfo.FullName, fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        private void filterComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private bool HasCommissionsHeldRowsNotMappedWithCommissionsAgents()
        {
            TcBindingList<TcCommissionsHeldRow> list = GetCommissionsHeldRowsNotMappedWithCommissionsAgents();

            return (list.Count > 0);
        }

        private TcBindingList<TcCommissionsHeldRow> GetCommissionsHeldRowsNotMappedWithCommissionsAgents()
        {
            TcCommissionsTable commissionsTable = master.CommissionsForm.CommissionsTable;
            TcBindingList<TcCommissionsHeldRow> list = CommissionsHeldTable.GetCommissionsHeldRowsNotMappedWithCommissionsAgents(commissionsTable);
            return list;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                TcCommissionsHeldRow data = source.Current as TcCommissionsHeldRow;
                if (data != null)
                {
                    TeCommissionHeldFilter filter = TcEnum.GetEnumForText<TeCommissionHeldFilter>(TeCommissionHeldFilter.All, filterComboBox.Text);
                    switch (filter)
                    {
                        case TeCommissionHeldFilter.All:
                        case TeCommissionHeldFilter.Virtual_Number_Duplicates:
                        case TeCommissionHeldFilter.No_Corresponding_Commissions_Agents:
                            duplicatesSource.DataSource = CommissionsHeldTable.GetDuplicates(data.VirtualNumber);
                            break;

                        case TeCommissionHeldFilter.Virtual_Number_Empty:
                        default:
                            duplicatesSource.DataSource = new TcBindingList<TcCommissionsHeldRow>();
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
                TeCommissionHeldFilter filter = TcEnum.GetEnumForText<TeCommissionHeldFilter>(TeCommissionHeldFilter.All, filterComboBox.Text);
                switch (filter)
                {
                    case TeCommissionHeldFilter.All:
                        source.DataSource = all;
                        break;

                    case TeCommissionHeldFilter.Virtual_Number_Empty:
                        source.DataSource = CommissionsHeldTable.GetVNEmptyList();
                        break;

                    case TeCommissionHeldFilter.Virtual_Number_Duplicates:
                        source.DataSource = CommissionsHeldTable.GetDuplicates();
                        break;

                    case TeCommissionHeldFilter.No_Corresponding_Commissions_Agents:
                        source.DataSource = GetCommissionsHeldRowsNotMappedWithCommissionsAgents();
                        break;

                    default:
                        break;
                }

                Search();
                SetStatus();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void Search()
        {
            string searchText = searchTextBox.Text.Trim();
            TcSearchHelper<TcCommissionsHeldRow> searchHelper = new TcSearchHelper<TcCommissionsHeldRow>();

            source.DataSource = searchHelper.Search(source, searchText);
        }

        private void SetStatus()
        {
            statusLabel.Text = string.Format("{0} record(s) found", source.Count);
            TcBindingList<TcCommissionsHeldRow> list = source.DataSource as TcBindingList<TcCommissionsHeldRow>;

            decimal holdAmount = 0;

            foreach (TcCommissionsHeldRow row in list)
            {
                holdAmount += row.Hold;
            }

            amountsLabel.Text = string.Format("Hold: {0}", holdAmount.ToString("N2"));
        }
    }
}

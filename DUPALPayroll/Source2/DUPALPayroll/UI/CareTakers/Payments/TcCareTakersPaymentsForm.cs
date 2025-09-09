using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-10-31

namespace DUPALPayroll.UI.CareTakers.Payments
{
    public partial class TcCareTakersPaymentsForm : Form
    {
        private TcCareTakersForm master;
        private string filePath = string.Empty;

        private TcBindingList<TcCareTakersPaymentsRow> all = new TcBindingList<TcCareTakersPaymentsRow>();

        private BindingSource source = new BindingSource();
        private BindingSource duplicatesSource = new BindingSource();

        public bool DataLoaded { get; set; }

        public TcCareTakersPaymentsTable CommissionsTable { get; private set; }

        public TcCareTakersPaymentsForm(TcCareTakersForm master, string filePath)
        {
            InitializeComponent();

            this.filePath       = filePath;
            this.master         = master;
            CommissionsTable    = new TcCareTakersPaymentsTable();
            DataLoaded          = false;

            dataGridView.AutoGenerateColumns            = false;
            duplicatesDataGridView.AutoGenerateColumns  = false;

            source.DataSource           = new TcBindingList<TcCareTakersPaymentsRow>();
            duplicatesSource.DataSource = new TcBindingList<TcCareTakersPaymentsRow>();

            dataGridView.DataSource             = source;
            duplicatesDataGridView.DataSource   = duplicatesSource;

            statusLabel.Text = string.Empty;

            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeCareTakersPaymentsFilter>(TeCareTakersPaymentsFilter.All));

            ApplyTheme();
            SetFileInfo();
        }

        private void ApplyTheme()
        {
            TcTheme.FormatGrid(dataGridView);

            TcTheme.FormatGridCurrencyDisplayColumn(paymentColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(holdColumn);

            TcTheme.FormatGrid(duplicatesDataGridView);

            TcTheme.FormatGridCurrencyDisplayColumn(dPaymentColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dHoldColumn);
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
                duplicatesSource.DataSource = new TcBindingList<TcCareTakersPaymentsRow>();

                TcCareTakersPaymentsLoader loader = new TcCareTakersPaymentsLoader();
                all = loader.LoadFromCSV(filePath);
                
                CommissionsTable = new TcCareTakersPaymentsTable();
                CommissionsTable.Load(all);

                all = CommissionsTable.All;
                source.DataSource = all;

                SetStatus();

                SetFilter();
                SetFileInfo();
                DataLoaded = true;

                TcCareTakersForm.ResetMasterFormFilter = true;
            }
            else
            {
                DataLoaded = false;
                string ex = string.Format("Payments file [{0}] does not exist", filePath);
                fileInfoLabel.Text = ex;
                throw new Exception(ex);
            }
        }

        private void SetFilter()
        {
            SetFilterData(CommissionsTable.HasNICDuplicates(), TcEnum.GetTextForEnum<TeCareTakersPaymentsFilter>(TeCareTakersPaymentsFilter.NIC_Duplicates));
            SetFilterData(CommissionsTable.HasEmptyNICRows(), TcEnum.GetTextForEnum<TeCareTakersPaymentsFilter>(TeCareTakersPaymentsFilter.Empty_NIC));

            filterComboBox.Text = TcEnum.GetTextForEnum<TeCareTakersPaymentsFilter>(TeCareTakersPaymentsFilter.All);
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
                fileInfoLabel.Text = "Click Load data button to load Commissions Data";
            }
            else
            {
                FileInfo fileInfo = new FileInfo(filePath);
                fileInfoLabel.Text = string.Format("File: [{0}], Last Modified Date: [{1}]", fileInfo.FullName, fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));
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
                TcCareTakersPaymentsRow row = source.Current as TcCareTakersPaymentsRow;

                if (row != null)
                {
                    TeCareTakersPaymentsFilter filter = TcEnum.GetEnumForText<TeCareTakersPaymentsFilter>(TeCareTakersPaymentsFilter.All, filterComboBox.Text);
                    switch (filter)
                    {
                        case TeCareTakersPaymentsFilter.All:
                            duplicatesSource.DataSource = CommissionsTable.GetDuplicates(row.NIC);
                            break;

                        case TeCareTakersPaymentsFilter.NIC_Duplicates:
                            duplicatesSource.DataSource = CommissionsTable.GetNICDuplicates(row.NIC);
                            break;

                        case TeCareTakersPaymentsFilter.Empty_NIC:
                        default:
                            duplicatesSource.DataSource = new TcBindingList<TcCareTakersPaymentsRow>();
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
                TeCareTakersPaymentsFilter filter = TcEnum.GetEnumForText<TeCareTakersPaymentsFilter>(TeCareTakersPaymentsFilter.All, filterComboBox.Text);
                switch (filter)
                {
                    case TeCareTakersPaymentsFilter.All:
                        source.DataSource = all;
                        break;

                    case TeCareTakersPaymentsFilter.NIC_Duplicates:
                        source.DataSource = CommissionsTable.GetNICDuplicates();
                        break;

                    case TeCareTakersPaymentsFilter.Empty_NIC:
                        source.DataSource = CommissionsTable.GetEmptyNICRows();
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
            TcSearchHelper<TcCareTakersPaymentsRow> searchHelper = new TcSearchHelper<TcCareTakersPaymentsRow>();

            source.DataSource = searchHelper.Search(source, searchText);
        }

        private void SetStatus()
        {
            statusLabel.Text = string.Format("{0} record(s) found", source.Count);
            TcBindingList<TcCareTakersPaymentsRow> list = source.DataSource as TcBindingList<TcCareTakersPaymentsRow>;

            decimal netCommission   = 0;

            foreach (TcCareTakersPaymentsRow row in list)
            {
                netCommission   += row.Payment;
            }

            amountsLabel.Text = string.Format("Payments: {0}", netCommission.ToString("N2"));
        }
    }
}

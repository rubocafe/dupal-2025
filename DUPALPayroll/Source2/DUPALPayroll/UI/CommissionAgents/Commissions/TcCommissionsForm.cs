using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-08-26

namespace DUPALPayroll.UI.CommissionAgents.Commissions
{
    public partial class TcCommissionsForm : Form
    {
        private TcCommissionAgentsForm master;
        private string filePath = string.Empty;

        private TcBindingList<TcCommissionsRow> all = new TcBindingList<TcCommissionsRow>();

        private BindingSource source = new BindingSource();
        private BindingSource duplicatesSource = new BindingSource();

        public bool DataLoaded { get; set; }

        public TcCommissionsTable CommissionsTable { get; private set; }

        public TcCommissionsForm(TcCommissionAgentsForm master, string filePath)
        {
            InitializeComponent();

            this.filePath       = filePath;
            this.master         = master;
            CommissionsTable    = new TcCommissionsTable();
            DataLoaded          = false;

            dataGridView.AutoGenerateColumns            = false;
            duplicatesDataGridView.AutoGenerateColumns  = false;

            source.DataSource           = new TcBindingList<TcCommissionsRow>();
            duplicatesSource.DataSource = new TcBindingList<TcCommissionsRow>();

            dataGridView.DataSource             = source;
            duplicatesDataGridView.DataSource   = duplicatesSource;

            statusLabel.Text = string.Empty;

            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeCommissionsFilter>(TeCommissionsFilter.All));

            ApplyTheme();
            SetFileInfo();
        }

        private void ApplyTheme()
        {
            TcTheme.FormatGrid(dataGridView);

            TcTheme.FormatGridCurrencyDisplayColumn(grossCommissionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(epfReductionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(netCommissionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(daysWorkedColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(epfContributionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(etfContributionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(payeColumn);

            TcTheme.FormatGridDateDisplayColumn(dateOfJoinColumn);

            TcTheme.FormatGrid(duplicatesDataGridView);

            TcTheme.FormatGridCurrencyDisplayColumn(duplicatesGrossCommissionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(duplicatesEPFDeductionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(duplicatesNetCommissionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dDaysWorkedColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dEpfContributionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dEtfContributionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(dPayeColumn);

            TcTheme.FormatGridDateDisplayColumn(duplicatesDateOfJoinColumn);

            if (!TcVersions.IsEpfEtfSupported(master.SettingsForm.WorkingYearMonth))
            {
                employeeNumberColumn.Visible    = false;
                epfContributionColumn.Visible   = false;
                etfContributionColumn.Visible   = false;
                memberStatusColumn.Visible      = false;
                daysWorkedColumn.Visible        = false;

                dEmployeeNumberColumn.Visible   = false;
                dEpfContributionColumn.Visible  = false;
                dEtfContributionColumn.Visible  = false;
                dMemberStatusColumn.Visible     = false;
                dDaysWorkedColumn.Visible       = false;
            }

            if (!TcVersions.IsPayeSupported(master.SettingsForm.WorkingYearMonth))
            {
                payeColumn.Visible = false;
                dPayeColumn.Visible = false;
            }
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
                duplicatesSource.DataSource = new TcBindingList<TcCommissionsRow>();

                TcCommissionsLoader loader = new TcCommissionsLoader(master.SettingsForm.WorkingYearMonth);
                all = loader.LoadFromCSV(filePath);
                
                CommissionsTable = new TcCommissionsTable();
                CommissionsTable.Load(all);

                all = CommissionsTable.All;
                source.DataSource = all;

                SetStatus();

                SetFilter();
                SetFileInfo();
                DataLoaded = true;

                TcCommissionAgentsForm.ResetMasterFormFilter = true;
            }
            else
            {
                DataLoaded = false;
                string ex = string.Format("Commissions file [{0}] does not exist", filePath);
                fileInfoLabel.Text = ex;
                throw new Exception(ex);
            }
        }

        private void SetFilter()
        {
            SetFilterData(CommissionsTable.HasNICDuplicates(), TcEnum.GetTextForEnum<TeCommissionsFilter>(TeCommissionsFilter.NIC_Duplicates));
            SetFilterData(CommissionsTable.HasVNDuplicates(), TcEnum.GetTextForEnum<TeCommissionsFilter>(TeCommissionsFilter.Virtual_Number_Duplicates));
            SetFilterData(CommissionsTable.HasEmptyVNAndNICRows(), TcEnum.GetTextForEnum<TeCommissionsFilter>(TeCommissionsFilter.Empty_Virtual_Number_and_NIC));

            filterComboBox.Text = TcEnum.GetTextForEnum<TeCommissionsFilter>(TeCommissionsFilter.All);
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
                TcCommissionsRow row = source.Current as TcCommissionsRow;

                if (row != null)
                {
                    TeCommissionsFilter filter = TcEnum.GetEnumForText<TeCommissionsFilter>(TeCommissionsFilter.All, filterComboBox.Text);
                    switch (filter)
                    {
                        case TeCommissionsFilter.All:
                            duplicatesSource.DataSource = CommissionsTable.GetDuplicates(row.VirtualNumber, row.NIC);
                            break;

                        case TeCommissionsFilter.NIC_Duplicates:
                            duplicatesSource.DataSource = CommissionsTable.GetNICDuplicates(row.NIC);
                            break;

                        case TeCommissionsFilter.Virtual_Number_Duplicates:
                            duplicatesSource.DataSource = CommissionsTable.GetVNDuplicates(row.VirtualNumber);
                            break;

                        case TeCommissionsFilter.Empty_Virtual_Number_and_NIC:
                        default:
                            duplicatesSource.DataSource = new TcBindingList<TcCommissionsRow>();
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
                TeCommissionsFilter filter = TcEnum.GetEnumForText<TeCommissionsFilter>(TeCommissionsFilter.All, filterComboBox.Text);
                switch (filter)
                {
                    case TeCommissionsFilter.All:
                        source.DataSource = all;
                        break;

                    case TeCommissionsFilter.NIC_Duplicates:
                        source.DataSource = CommissionsTable.GetNICDuplicates();
                        break;

                    case TeCommissionsFilter.Virtual_Number_Duplicates:
                        source.DataSource = CommissionsTable.GetVNDuplicates();
                        break;

                    case TeCommissionsFilter.Empty_Virtual_Number_and_NIC:
                        source.DataSource = CommissionsTable.GetEmptyVirtualNumberAndNICRows();
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
            TcSearchHelper<TcCommissionsRow> searchHelper = new TcSearchHelper<TcCommissionsRow>();

            source.DataSource = searchHelper.Search(source, searchText);
        }

        private void SetStatus()
        {
            statusLabel.Text = string.Format("{0} record(s) found", source.Count);
            TcBindingList<TcCommissionsRow> list = source.DataSource as TcBindingList<TcCommissionsRow>;

            decimal grossCommission = 0;
            decimal epfDeduction    = 0;
            decimal netCommission   = 0;

            foreach (TcCommissionsRow row in list)
            {
                grossCommission += row.GrossCommission;
                epfDeduction    += row.EPFDeduction;
                netCommission   += row.NetCommission;
            }

            amountsLabel.Text = string.Format("Gross Commission: {0},  EPF Deduction: {1},  Net Commission: {2}",
                grossCommission.ToString("N2"), epfDeduction.ToString("N2"), netCommission.ToString("N2"));
        }
    }
}

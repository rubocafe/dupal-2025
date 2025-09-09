using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.Library.Date;
using DUPALPayroll.Library.Sys;
using DUPALPayroll.UI.Common;
using DUPALPayroll.UI.Common.Etf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2014-01-02

namespace DUPALPayroll.UI.Etf.Etf
{
    public partial class TcEtfForm : Form
    {
        private const string AllString = "All";
        private const string ValidString = "Valid";

        private TcEtfControlForm master;
        private List<TcParsedEtfCsvFileInformation> files = new List<TcParsedEtfCsvFileInformation>();
        private BindingSource source = new BindingSource();
        private BindingSource etfSource = new BindingSource();

        public TcEtfForm(TcEtfControlForm master)
        {
            InitializeComponent();
            this.master = master;

            dataGridView.AutoGenerateColumns    = false;
            etfDataGridView.AutoGenerateColumns = false;

            TcTheme.FormatGrid(dataGridView);
            TcTheme.FormatGrid(etfDataGridView);

            TcTheme.FormatGridCurrencyDisplayColumn(validRowsTotalColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(invalidRowsTotalColumn);

            TcTheme.FormatGridCurrencyDisplayColumn(totalColumn);
            TcTheme.FormatGridFullDateDisplayColumn(modifiedDateColumn);

            TcTheme.FormatGridCurrencyDisplayColumn(totalContributionColumn);
            TcTheme.FormatGridDateMonthDisplayColumn(fromColumn);
            TcTheme.FormatGridDateMonthDisplayColumn(toColumn);
        }

        public bool Reload()
        {
            bool reloaded = false;

            try
            {
                source.DataSource = new List<TcParsedEtfCsvFileInformation>();

                files.Clear();

                AddFile(TcPaths.AuditorsId);
                AddFile(TcPaths.CommissionAgentsId);
                AddFile(TcPaths.CallCenterInboundId);
                AddFile(TcPaths.CallCenterOutboundId);
                AddFile(TcPaths.CustomerCareId);
                AddFile(TcPaths.OfficeStaffId);
                AddFile(TcPaths.PremierSalesId);
                AddFile(TcPaths.SupervisorsAndBackOfficeId);

                TcEtfDetailOriginData origin = GetOriginData();
                foreach (TcParsedEtfCsvFileInformation info in files)
                {
                    info.ReadEtfFile(origin);
                }

                source.DataSource = files;
                dataGridView.DataSource = source;

                reloaded = true;
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }

            return reloaded;
        }

        private void AddFile(string identifier)
        {
            string directoryPath = Path.Combine(master.SettingsForm.RootDirectoryPath, identifier);
            string fileName = string.Format("{0}Etf_{1}.csv", TcPaths.GetCompressedId(identifier), master.SettingsForm.WorkingYearMonth.ToString());

            string filePath = Path.Combine(directoryPath, fileName);
            TcParsedEtfCsvFileInformation info = new TcParsedEtfCsvFileInformation(identifier, filePath);
            files.Add(info);
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            ClearSubControls();

            if (dataGridView.SelectedRows.Count > 0)
            {
                TcParsedEtfCsvFileInformation info = dataGridView.SelectedRows[0].DataBoundItem as TcParsedEtfCsvFileInformation;

                if (info != null)
                {
                    LoadSubControls(info);
                }
            }
        }

        private void etfDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            reasonsRichTextBox.Text = "";

            if (etfDataGridView.SelectedRows.Count > 0)
            {
                TcEtfDetailRow row = etfDataGridView.SelectedRows[0].DataBoundItem as TcEtfDetailRow;

                if (row != null)
                {
                    reasonsRichTextBox.Text = row.GetErrors();
                }
            }
        }

        private void filterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                TcParsedEtfCsvFileInformation info = dataGridView.SelectedRows[0].DataBoundItem as TcParsedEtfCsvFileInformation;

                if (info != null)
                {
                    if (info.Validator != null)
                    {
                        TeEtfError error = TcEnum.GetEnumForText<TeEtfError>(TeEtfError.All, filterComboBox.Text);
                        TcBindingList<TcEtfDetailRow> rows = info.Validator.GetRowsWithError(error);
                        etfSource.DataSource = rows;
                        etfDataGridView.DataSource = etfSource;

                        SetStatusLabel(rows);
                    }
                }
            }
        }

        private void SetStatusLabel(TcBindingList<TcEtfDetailRow> rows)
        {
            decimal totalContribution = 0;

            foreach (TcEtfDetailRow row in rows)
            {
                totalContribution += row.TotalContribution;
            }

            statusLabel.Text = string.Format(
                "Member Count: [{0}], Total Contribution: [{1}]",
                rows.Count, totalContribution.ToString("N2"));
        }

        private void LoadSubControls(TcParsedEtfCsvFileInformation info)
        {
            groupBox.Text = string.Format("{0}", info.Identifier);
            LoadFilter(info.Validator);

            if (info.Validator != null && info.Validator.File != null)
            {
                TcBindingList<TcEtfDetailRow> rows = info.Validator.GetAllRows();
                etfSource.DataSource = rows;
                etfDataGridView.DataSource  = etfSource;

                groupBox.Enabled = true;
                SetStatusLabel(rows);
            }
            else
            {
                etfSource.DataSource = new TcBindingList<TcEtfDetailRow>(); 
                etfDataGridView.DataSource = etfSource;

                groupBox.Enabled = false;
                statusLabel.Text = "CSV File not found";
            }
        }

        private void LoadFilter(TcEtfFileValidator validator)
        {
            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(AllString);
            filterComboBox.Items.Add(ValidString);

            if (validator != null)
            {
                TcBindingList<TeEtfError> errors = validator.GetAllErrors();

                foreach (TeEtfError error in errors)
                {
                    filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeEtfError>(error));
                }
            }

            filterComboBox.Text = AllString;
        }

        private void ClearSubControls()
        {
            groupBox.Text = "--";
            filterComboBox.Text = AllString;

            etfSource.DataSource = new TcBindingList<TcEtfDetailRow>();
            etfDataGridView.DataSource = etfSource;

            statusLabel.Text = "--";
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            try
            {
                string sharedFolder = TcPaths.GetSharedFolderPath(master.SettingsForm.RootDirectoryPath);
                string finalEtfFilePath = Path.Combine(sharedFolder, 
                    string.Format("MEMTXT.txt", master.SettingsForm.ZoneCode, 
                    TcString.AppendZerosToFront(master.SettingsForm.EmployerNumber.ToString(), 6)));

                GenerateEtfFile(finalEtfFilePath);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void GenerateEtfFile(string finalEtfFilePath)
        {
            TcEtfFile etfFile = new TcEtfFile();
            foreach (TcParsedEtfCsvFileInformation file in files)
            {
                if (file.Exists)
                {
                    foreach (TcEtfDetailRow row in file.Reader.File.Rows)
                    {
                        etfFile.Rows.Add(row);
                    }
                }
            }

            //  Sort on employee number
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(TcEtfDetailRow));
            PropertyDescriptor property = properties.Find("MemberNumber", true);
            etfFile.Rows.SortAsInt(property, ListSortDirection.Ascending);

            etfFile.GenerateHeaderRow();

            TcEtfFileWriter writer = new TcEtfFileWriter(etfFile, finalEtfFilePath);
            writer.Write();

            decimal total = writer.File.GetTotal();

            string message = string.Format("File [{0}] generated\nTotal: {1}\n", finalEtfFilePath, total.ToString("N2"));
            if (writer.ErrorRows.Count > 0)
            {
                message += string.Format("[{0}] invalid row(s) have not been included in ETF file\n\n", writer.ErrorRows.Count);
            }

            if (writer.ErrorRows.Count > 0)
            {
                TcMessageBox.ShowWarning(message);
            }
            else
            {
                TcMessageBox.ShowInformation(message);
            }
        }

        private TcEtfDetailOriginData GetOriginData()
        {
            TcEtfDetailOriginData origin = new TcEtfDetailOriginData(master.SettingsForm.WorkingYearMonth);

            origin.EmployerNumber   = string.Format("{0}{1}", 
                TcString.AppendSpacesToEnd(master.SettingsForm.ZoneCode, 2), 
                TcString.AppendZerosToFront(master.SettingsForm.EmployerNumber.ToString(), 6));

            return origin;
        }

        private void openFolderButton_Click(object sender, EventArgs e)
        {
            try
            {
                TcYearMonth yearMonth = master.SettingsForm.WorkingYearMonth;
                string monthFolder = master.SettingsForm.RootDirectoryPath;
                string rootDirectoryPath = TcPaths.GetSharedFolderPath(monthFolder);
                if (!Directory.Exists(rootDirectoryPath))
                {
                    TcMessageBox.ShowWarning(string.Format("Folder [{0}] does not exists", rootDirectoryPath));
                }
                else
                {
                    TcDirectory.Open(rootDirectoryPath);
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }
    }
}

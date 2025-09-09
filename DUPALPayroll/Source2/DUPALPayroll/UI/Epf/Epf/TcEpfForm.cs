using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.Library.Date;
using DUPALPayroll.Library.Sys;
using DUPALPayroll.UI.Common;
using DUPALPayroll.UI.Common.Epf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2014-01-07

namespace DUPALPayroll.UI.Epf.Epf
{
    public partial class TcEpfForm : Form
    {
        private const string AllString = "All";
        private const string ValidString = "Valid";

        private TcEpfControlForm master;
        private List<TcParsedEpfCsvFileInformation> files = new List<TcParsedEpfCsvFileInformation>();
        private BindingSource source = new BindingSource();
        private BindingSource epfSource = new BindingSource();

        public TcEpfForm(TcEpfControlForm master)
        {
            InitializeComponent();
            this.master = master;

            dataGridView.AutoGenerateColumns    = false;
            epfDataGridView.AutoGenerateColumns = false;

            TcTheme.FormatGrid(dataGridView);
            TcTheme.FormatGrid(epfDataGridView);

            TcTheme.FormatGridCurrencyDisplayColumn(validRowsTotalColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(invalidRowsTotalColumn);

            TcTheme.FormatGridCurrencyDisplayColumn(totalColumn);
            TcTheme.FormatGridFullDateDisplayColumn(modifiedDateColumn);
            TcTheme.FormatGridNumberDisplayColumn(employerNumberColumn);
            TcTheme.FormatGridNumberDisplayColumn(memberAcNumberColumn);
            TcTheme.FormatGridNumberDisplayColumn(submitionIdColumn);
            TcTheme.FormatGridNumberDisplayColumn(ocGradeColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(daysOfWorkColumn);

            TcTheme.FormatGridCurrencyDisplayColumn(membersContributionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(employersContributionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(totalContributionColumn);
            TcTheme.FormatGridCurrencyDisplayColumn(totoalEarningsColumn);
            TcTheme.FormatGridDateMonthDisplayColumn(contributionPeriodColumn);
        }

        public bool Reload()
        {
            bool reloaded = false;

            try
            {
                source.DataSource = new List<TcParsedEpfCsvFileInformation>();

                files.Clear();

                AddFile(TcPaths.AuditorsId);
                AddFile(TcPaths.CommissionAgentsId);
                AddFile(TcPaths.CallCenterInboundId);
                AddFile(TcPaths.CallCenterOutboundId);
                AddFile(TcPaths.CustomerCareId);
                AddFile(TcPaths.OfficeStaffId);
                AddFile(TcPaths.PremierSalesId);
                AddFile(TcPaths.SupervisorsAndBackOfficeId);

                TcEpfOriginData origin = GetOriginData();
                foreach (TcParsedEpfCsvFileInformation info in files)
                {
                    info.ReadEpfFile(origin);
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
            string fileName = string.Format("{0}Epf_{1}.csv", TcPaths.GetCompressedId(identifier), master.SettingsForm.WorkingYearMonth.ToString());

            string filePath = Path.Combine(directoryPath, fileName);
            TcParsedEpfCsvFileInformation info = new TcParsedEpfCsvFileInformation(identifier, filePath);
            files.Add(info);
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            ClearSubControls();

            if (dataGridView.SelectedRows.Count > 0)
            {
                TcParsedEpfCsvFileInformation info = dataGridView.SelectedRows[0].DataBoundItem as TcParsedEpfCsvFileInformation;

                if (info != null)
                {
                    LoadSubControls(info);
                }
            }
        }

        private void epfDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            reasonsRichTextBox.Text = "";

            if (epfDataGridView.SelectedRows.Count > 0)
            {
                TcEpfRow row = epfDataGridView.SelectedRows[0].DataBoundItem as TcEpfRow;

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
                TcParsedEpfCsvFileInformation info = dataGridView.SelectedRows[0].DataBoundItem as TcParsedEpfCsvFileInformation;

                if (info != null)
                {
                    if (info.Validator != null)
                    {
                        TeEpfError error = TcEnum.GetEnumForText<TeEpfError>(TeEpfError.All, filterComboBox.Text);
                        TcBindingList<TcEpfRow> rows = info.Validator.GetRowsWithError(error);
                        epfSource.DataSource = rows;
                        epfDataGridView.DataSource = epfSource;

                        SetStatusLabel(rows);
                    }
                }
            }
        }

        private void SetStatusLabel(TcBindingList<TcEpfRow> rows)
        {
            decimal memberContribution = 0;
            decimal employerContribution = 0;
            decimal totalContribution = 0;

            foreach (TcEpfRow row in rows)
            {
                memberContribution += row.MembersContribution;
                employerContribution += row.EmployersContribution;
                totalContribution += row.TotalContribution;
            }

            statusLabel.Text = string.Format(
                "Member Count: [{0}], Member Contribution: [{1}], Employer Contribution: [{2}], Total Contribution: [{3}]",
                rows.Count, memberContribution.ToString("N2"), employerContribution.ToString("N2"), totalContribution.ToString("N2"));
        }

        private void LoadSubControls(TcParsedEpfCsvFileInformation info)
        {
            groupBox.Text = string.Format("{0}", info.Identifier);
            LoadFilter(info.Validator);

            if (info.Validator != null && info.Validator.File != null)
            {
                TcBindingList<TcEpfRow> rows = info.Validator.GetAllRows();
                epfSource.DataSource        = rows;
                epfDataGridView.DataSource  = epfSource;

                groupBox.Enabled = true;
                SetStatusLabel(rows);
            }
            else
            {
                epfSource.DataSource = new TcBindingList<TcEpfRow>(); 
                epfDataGridView.DataSource = epfSource;

                groupBox.Enabled = false;
                statusLabel.Text = "CSV File not found";
            }
        }

        private void LoadFilter(TcEpfFileValidator validator)
        {
            filterComboBox.Items.Clear();
            filterComboBox.Items.Add(AllString);
            filterComboBox.Items.Add(ValidString);

            if (validator != null)
            {
                TcBindingList<TeEpfError> errors = validator.GetAllErrors();

                foreach (TeEpfError error in errors)
                {
                    filterComboBox.Items.Add(TcEnum.GetTextForEnum<TeEpfError>(error));
                }
            }

            filterComboBox.Text = AllString;
        }

        private void ClearSubControls()
        {
            groupBox.Text = "--";
            filterComboBox.Text = AllString;

            epfSource.DataSource = new TcBindingList<TcEpfRow>();
            epfDataGridView.DataSource = epfSource;

            statusLabel.Text = "--";
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            try
            {
                string sharedFolder = TcPaths.GetSharedFolderPath(master.SettingsForm.RootDirectoryPath);
                string finalEpfFilePath = Path.Combine(sharedFolder, 
                    string.Format("{0}{1}C.txt", master.SettingsForm.ZoneCode, 
                    TcString.AppendZerosToFront(master.SettingsForm.EmployerNumber.ToString(), 6)));

                GenerateEpfFile(finalEpfFilePath);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void GenerateEpfFile(string finalEpfFilePath)
        {
            TcEpfFile epfFile = new TcEpfFile();
            foreach (TcParsedEpfCsvFileInformation file in files)
            {
                if (file.Exists)
                {
                    foreach (TcEpfRow row in file.Reader.File.Rows)
                    {
                        epfFile.Rows.Add(row);
                    }
                }
            }

            //  Sort on employee number
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(TcEpfRow));
            PropertyDescriptor property = properties.Find("MemberNumber", true);
            epfFile.Rows.SortAsInt(property, ListSortDirection.Ascending);

            TcEpfFileWriter writer = new TcEpfFileWriter(epfFile, finalEpfFilePath);
            writer.Write();

            decimal total = writer.File.GetTotal();

            string message = string.Format("File [{0}] generated\nTotal: {1}\n", finalEpfFilePath, total.ToString("N2"));
            if (writer.ErrorRows.Count > 0)
            {
                message += string.Format("[{0}] invalid row(s) have not been included in EPF file\n\n", writer.ErrorRows.Count);
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

        private TcEpfOriginData GetOriginData()
        {
            TcEpfOriginData origin = new TcEpfOriginData();

            origin.ContributionPeriod   = master.SettingsForm.WorkingYearMonth;
            origin.ZoneCode             = master.SettingsForm.ZoneCode;
            origin.EmployerNumber       = master.SettingsForm.EmployerNumber.ToString();
            origin.SubmitionId          = 1;

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

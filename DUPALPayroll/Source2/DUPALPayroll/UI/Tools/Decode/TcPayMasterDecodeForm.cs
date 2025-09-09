using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.UI.Common.PayMaster;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-08-28

namespace DUPALPayroll.UI.CommissionAgents.Tools.Decode
{
    public partial class TcPayMasterDecodeForm : Form
    {
        private TcBindingList<TcPayMasterRow> all = new TcBindingList<TcPayMasterRow>();
        private BindingSource source = new BindingSource();

        public TcPayMasterDecodeForm()
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            statusLabel.Text = "";
            fileInfoLabel.Text = "";

            source.DataSource = new TcBindingList<TcPayMasterRow>();
            dataGridView.DataSource = source;

            ApplyTheme();
        }

        private void ApplyTheme()
        {
            TcTheme.FormatGrid(dataGridView);

            TcTheme.FormatGridCurrencyDisplayColumn(amountDecimalColumn);
        }

        private void loadDataButton_Click(object sender, EventArgs e)
        {
            try
            {
                string dupalDirectory = TcSettings.DuPalRootDirectory;
                openFileDialog.InitialDirectory = dupalDirectory;

                DialogResult result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    source.DataSource = new TcBindingList<TcPayMasterRow>();
                    statusLabel.Text = "";

                    TcPayMasterFileDecorder decorder = new TcPayMasterFileDecorder(openFileDialog.FileName);
                    all = decorder.Decode();
                    source.DataSource = all;

                    TcMessageBox.ShowInformation("Data loaded successfully");

                    SetStatus();
                    SetFileInfo(openFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void SetFileInfo(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            fileInfoLabel.Text = string.Format("File: [{0}], Last Modified Date: [{1}]", fileInfo.FullName, fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Search();
                SetStatus();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void SetStatus()
        {
            statusLabel.Text = string.Format("[{0}] record(s) found", source.Count);
        }

        private void Search()
        {
            string searchText = searchTextBox.Text.Trim();
            TcSearchHelper<TcPayMasterRow> searchHelper = new TcSearchHelper<TcPayMasterRow>();

            BindingSource temp = new BindingSource();
            temp.DataSource = all;

            source.DataSource = searchHelper.Search(temp, searchText);
        }
    }
}

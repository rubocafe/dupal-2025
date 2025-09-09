using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.UI.Common;
using DUPALPayroll.UI.Configuration.ConfigFile;
using DUPALPayroll.UI.Etf.Etf;
using DUPALPayroll.UI.Etf.Settings;
using System;
using System.Windows.Forms;

// Harshan Nishantha
// 2014-01-07

namespace DUPALPayroll.UI.Etf
{
    public partial class TcEtfControlForm : TcForm
    {
        private TcEtfSettingsForm settingsForm;
        private TcEtfForm etfForm;

        public TcEtfSettingsForm SettingsForm
        {
            get { return settingsForm; }
        }

        public TcEtfControlForm()
        {
            InitializeComponent();

            settingsForm = new TcEtfSettingsForm(this);
            etfForm = new TcEtfForm(this);
        }

        public bool InitializeFormsAndShowOtherTabs()
        {
            bool succeed = TcConfigurationFile.ValidForWorkingYearMonth() &&
                            ReloadEtfForm();

            if (succeed)
            {
                ShowOtherTabs();
            }
            else
            {
                // TcMessageBox.ShowWarning("Please correct the errors and load data again"); // Seemes like redundency message
            }

            return succeed;
        }

        public override bool Loaded()
        {
            if (tabControl.Contains(etfTabPage))
            {
                return true;
            }

            return false;
        }

        private bool ReloadEtfForm()
        {
            try
            {
                return etfForm.Reload(); ;
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowWarning(string.Format("Failed to load Etf Form\n{0}", ex.Message));
                return false;
            }
        }

        private void LoadFormToTab(Form form, TabPage tabPage)
        {
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;

            tabPage.Controls.Clear();
            tabPage.Controls.Add(form);

            form.Show();
        }

        #region Tab Control - Forms

        private void settingsTabPage_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadFormToTab(settingsForm, settingsTabPage);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void etfTabPage_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadFormToTab(etfForm, etfTabPage);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        #endregion

        private void TcEtfControlForm_Load(object sender, EventArgs e)
        {
            try
            {
                settingsTabPage.Focus();

                HideOtherTabs();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        public void HideOtherTabs()
        {
            RemoveTabPage(etfTabPage);
        }

        private void ShowOtherTabs()
        {
            AddTabPage(etfTabPage);
        }

        private void AddTabPage(TabPage tabPage)
        {
            if (!tabControl.Contains(tabPage))
            {
                tabControl.Controls.Add(tabPage);
            }
        }

        private void RemoveTabPage(TabPage tabPage)
        {
            if (tabControl.Contains(tabPage))
            {
                tabControl.Controls.Remove(tabPage);
            }
        }
    }
}

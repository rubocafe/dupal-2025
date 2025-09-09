using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.UI.Common;
using DUPALPayroll.UI.Configuration.ConfigFile;
using DUPALPayroll.UI.Epf.Epf;
using DUPALPayroll.UI.Epf.Settings;
using System;
using System.Windows.Forms;

// Harshan Nishantha
// 2014-01-07

namespace DUPALPayroll.UI.Epf
{
    public partial class TcEpfControlForm : TcForm
    {
        private TcEpfSettingsForm settingsForm;
        private TcEpfForm epfForm;

        public TcEpfSettingsForm SettingsForm
        {
            get { return settingsForm; }
        }

        public TcEpfControlForm()
        {
            InitializeComponent();

            settingsForm = new TcEpfSettingsForm(this);
            epfForm = new TcEpfForm(this);
        }

        public bool InitializeFormsAndShowOtherTabs()
        {
            bool succeed = TcConfigurationFile.ValidForWorkingYearMonth() &&
                            ReloadEpfForm();

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
            if (tabControl.Contains(epfTabPage))
            {
                return true;
            }

            return false;
        }

        private bool ReloadEpfForm()
        {
            try
            {
                return epfForm.Reload(); ;
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowWarning(string.Format("Failed to load Epf Form\n{0}", ex.Message));
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

        private void epfTabPage_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadFormToTab(epfForm, epfTabPage);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        #endregion

        private void TcEpfControlForm_Load(object sender, EventArgs e)
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
            RemoveTabPage(epfTabPage);
        }

        private void ShowOtherTabs()
        {
            AddTabPage(epfTabPage);
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

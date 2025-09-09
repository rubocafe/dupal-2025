using Payroll.UI.Configuration.Settings;
using Payroll.Library.General;
using Payroll.UI.Controls;
using System;
using System.Windows.Forms;

// Harshan Nishantha
// 2014-01-08

namespace Payroll.UI.Configuration
{
    public partial class TcConfigurationForm : TcTabControlForm
    {
        private TcConfigurationSettingsForm settingsForm;

        public TcConfigurationForm()
        {
            InitializeComponent();
        }

        private void TcConfigurationForm_Load(object sender, EventArgs e)
        {
            settingsTabPage.Focus();
        }

        private void settingsTabPage_Enter(object sender, EventArgs e)
        {
            try
            {
                if (SelectedTab != settingsTabPage)
                {
                    if (CanLeave())
                    {
                        if (settingsForm == null)
                        {
                            settingsForm = new TcConfigurationSettingsForm();
                        }

                        settingsForm.Reload();
                        LoadFormToTab(settingsForm, settingsTabPage);
                    }
                    else
                    {
                        tabControl.SelectedTab = SelectedTab;
                        SelectedTab.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        public override bool CanLeave()
        {
            if (!base.CanLeave())
            {
                string question = "There are unsaved changes in form. Do you want to leave without saving changes?";
                DialogResult result = TcMessageBox.ShowYesNoQuestion(question);
                if (result == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}

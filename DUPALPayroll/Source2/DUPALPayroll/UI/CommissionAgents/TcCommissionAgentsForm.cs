using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.UI.CommissionAgents.Analyze;
using DUPALPayroll.UI.CommissionAgents.Commissions;
using DUPALPayroll.UI.CommissionAgents.CommissionsHeld;
using DUPALPayroll.UI.CommissionAgents.Generate;
using DUPALPayroll.UI.CommissionAgents.MasterData;
using DUPALPayroll.UI.CommissionAgents.Settings;
using DUPALPayroll.UI.Common;
using DUPALPayroll.UI.Common.BanksAndBranches;
using DUPALPayroll.UI.Configuration.ConfigFile;
using System;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-08-26

namespace DUPALPayroll.UI.CommissionAgents
{
    public partial class TcCommissionAgentsForm : TcForm
    {
        public string Identifier = TcPaths.CommissionAgentsId;

        private TcCommissionAgentsSettingsForm              settingsForm;
        private TcCommissionAgentsMasterForm                masterForm;
        private TcBanksAndBranchesForm                      banksAndBranchesForm;
        private TcCommissionsForm                           commissionsForm;
        private TcCommissionsHeldForm                       commissionsHeldForm;
        private TcCommissionAgentsPayMasterGenerateForm     payMasterGenerateForm;
        private TcCommissionAgentsAnalyzeForm               analyzeForm;

        public static bool ResetMasterFormFilter { get; set; }
        public static bool ResetCommissionsHeldFilter { get; set; }

        public TcCommissionAgentsSettingsForm SettingsForm
        {
            get { return settingsForm; }
        }

        public TcCommissionAgentsMasterForm MasterForm
        {
            get { return masterForm; }
        }

        public TcBanksAndBranchesForm BanksAndBranchesForm
        {
            get { return banksAndBranchesForm; }
        }

        public TcCommissionsForm CommissionsForm
        {
            get { return commissionsForm; }
        }

        public TcCommissionsHeldForm CommissionHeldForm
        {
            get { return commissionsHeldForm; }
        }

        public TcCommissionAgentsAnalyzeForm AnalyzeForm
        {
            get { return analyzeForm; }
        }

        public TcCommissionAgentsForm()
        {
            InitializeComponent();

            settingsForm = new TcCommissionAgentsSettingsForm(this);
        }

        public bool InitializeFormsAndShowOtherTabs()
        {
            masterForm              = new TcCommissionAgentsMasterForm(this, settingsForm.MasterFilePath);
            banksAndBranchesForm    = new TcBanksAndBranchesForm(settingsForm.BanksAndBranchesFilePath);
            commissionsForm         = new TcCommissionsForm(this, settingsForm.CommissionsFilePath);
            commissionsHeldForm     = new TcCommissionsHeldForm(this, settingsForm.CommissionsHeldFilePath);
            analyzeForm             = new TcCommissionAgentsAnalyzeForm(this);
            payMasterGenerateForm   = new TcCommissionAgentsPayMasterGenerateForm(this);

            bool succeed = TcConfigurationFile.ValidForWorkingYearMonth() &&
                            ReloadMasterDataForm() &&
                            ReloadBanksAndBranchesForm() &&
                            ReloadCommissionsForm() &&
                            ReloadCommissionsHeldForm();

            if (succeed)
            {
                ShowOtherTabs();
                ReloadAnalyzeForm();
            }

            return succeed;
        }

        public override bool Loaded()
        {
            if (tabControl.Contains(masterDataTabPage))
            {
                return true;
            }

            return false;
        }

        public void ReloadAnalyzeForm()
        {
            if (analyzeForm != null)
            {
                analyzeForm.Analyze();
            }
        }

        private bool ReloadBanksAndBranchesForm()
        {
            try
            {
                banksAndBranchesForm.ReloadData();
                return true;
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowWarning(string.Format("Failed to load Banks and Branches Data\n{0}", ex.Message));
                return false;
            }
        }

        private bool ReloadCommissionsForm()
        {
            try
            {
                commissionsForm.ReloadData();
                return true;
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowWarning(string.Format("Failed to load Commissions Data\n{0}", ex.Message));
                return false;
            }
        }

        private bool ReloadMasterDataForm()
        {
            try
            {
                masterForm.ReloadData();
                return true;
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowWarning(string.Format("Failed to load Master Data\n{0}", ex.Message));
                return false;
            }
        }

        private bool ReloadCommissionsHeldForm()
        {
            try
            {
                commissionsHeldForm.ReloadData();
                return true;
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowWarning(string.Format("Failed to load Commissions Held Data\n{0}", ex.Message));
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

        private void masterDataTabPage_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadFormToTab(masterForm, masterDataTabPage);

                if (ResetMasterFormFilter)
                {
                    masterForm.SetFilter();
                    ResetMasterFormFilter = false;
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void banksAndBranchesTabPage_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadFormToTab(banksAndBranchesForm, banksAndBranchesTabPage);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void commissionsTabPage_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadFormToTab(commissionsForm, commissionsTabPage);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void commissionHeldTabPage_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadFormToTab(commissionsHeldForm, commissionsHeldTabPage);

                if (ResetCommissionsHeldFilter)
                {
                    commissionsHeldForm.SetFilter();
                    ResetCommissionsHeldFilter = false;
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void analyzeTabPage_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadFormToTab(analyzeForm, analyzeTabPage);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void payMasterTabPage_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadFormToTab(payMasterGenerateForm, payMasterTabPage);

                payMasterGenerateForm.ReInitialize();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        #endregion

        private void TcCommissionAgentsForm_Load(object sender, EventArgs e)
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
            RemoveTabPage(masterDataTabPage);
            RemoveTabPage(banksAndBranchesTabPage);
            RemoveTabPage(commissionsTabPage);
            RemoveTabPage(commissionsHeldTabPage);
            RemoveTabPage(analyzeTabPage);
            RemoveTabPage(payMasterTabPage);
        }

        private void ShowOtherTabs()
        {
            AddTabPage(masterDataTabPage);
            AddTabPage(banksAndBranchesTabPage);
            AddTabPage(commissionsTabPage);
            AddTabPage(commissionsHeldTabPage);
            AddTabPage(analyzeTabPage);
            AddTabPage(payMasterTabPage);
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

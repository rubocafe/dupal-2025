using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.UI.CareTakers.Analyze;
using DUPALPayroll.UI.CareTakers.Payments;
using DUPALPayroll.UI.CareTakers.Generate;
using DUPALPayroll.UI.CareTakers.MasterData;
using DUPALPayroll.UI.CareTakers.Settings;
using DUPALPayroll.UI.Common.BanksAndBranches;
using System;
using System.Windows.Forms;
using DUPALPayroll.UI.Common;
using DUPALPayroll.UI.Configuration.ConfigFile;

// Harshan Nishantha
// 2013-10-31

namespace DUPALPayroll.UI.CareTakers
{
    public partial class TcCareTakersForm : TcForm
    {
        public string Identifier = TcPaths.CareTakersId;

        private TcCareTakersSettingsForm              settingsForm;
        private TcCareTakersMasterForm                masterForm;
        private TcBanksAndBranchesForm                banksAndBranchesForm;
        private TcCareTakersPaymentsForm              paymentsForm;
        private TcCareTakersPayMasterGenerateForm     payMasterGenerateForm;
        private TcCareTakersAnalyzeForm               analyzeForm;

        public static bool ResetMasterFormFilter { get; set; }

        public TcCareTakersSettingsForm SettingsForm
        {
            get { return settingsForm; }
        }

        public TcCareTakersMasterForm MasterForm
        {
            get { return masterForm; }
        }

        public TcBanksAndBranchesForm BanksAndBranchesForm
        {
            get { return banksAndBranchesForm; }
        }

        public TcCareTakersPaymentsForm PymentsForm
        {
            get { return paymentsForm; }
        }

        public TcCareTakersAnalyzeForm AnalyzeForm
        {
            get { return analyzeForm; }
        }

        public TcCareTakersForm()
        {
            InitializeComponent();

            settingsForm = new TcCareTakersSettingsForm(this);
        }

        public bool InitializeFormsAndShowOtherTabs()
        {
            masterForm              = new TcCareTakersMasterForm(this, settingsForm.MasterFilePath);
            banksAndBranchesForm    = new TcBanksAndBranchesForm(settingsForm.BanksAndBranchesFilePath);
            paymentsForm            = new TcCareTakersPaymentsForm(this, settingsForm.PaymentsFilePath);
            analyzeForm             = new TcCareTakersAnalyzeForm(this);
            payMasterGenerateForm   = new TcCareTakersPayMasterGenerateForm(this);

            bool succeed =  TcConfigurationFile.ValidForWorkingYearMonth() &&
                            ReloadMasterDataForm() &&
                            ReloadBanksAndBranchesForm() &&
                            ReloadCommissionsForm();

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
                paymentsForm.ReloadData();
                return true;
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowWarning(string.Format("Failed to load Payments Data\n{0}", ex.Message));
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

        private void paymentsTabPage_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadFormToTab(paymentsForm, paymentsTabPage);
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
            RemoveTabPage(paymentsTabPage);
            RemoveTabPage(analyzeTabPage);
            RemoveTabPage(payMasterTabPage);
        }

        private void ShowOtherTabs()
        {
            AddTabPage(masterDataTabPage);
            AddTabPage(banksAndBranchesTabPage);
            AddTabPage(paymentsTabPage);
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

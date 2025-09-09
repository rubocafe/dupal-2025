using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.UI.CallCenterOutbound.Analyze;
using DUPALPayroll.UI.CallCenterOutbound.Generate;
using DUPALPayroll.UI.CallCenterOutbound.MasterData;
using DUPALPayroll.UI.CallCenterOutbound.Salary;
using DUPALPayroll.UI.CallCenterOutbound.Settings;
using DUPALPayroll.UI.Common;
using DUPALPayroll.UI.Common.BanksAndBranches;
using DUPALPayroll.UI.Configuration.ConfigFile;
using System;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.CallCenterOutbound
{
    public partial class TcCallCenterOutboundForm : TcForm
    {
        public string Identifier = TcPaths.CallCenterOutboundId;

        private TcCallCenterOutboundSettingsForm          settingsForm;
        private TcCallCenterOutboundMasterForm            masterForm;
        private TcBanksAndBranchesForm                    banksAndBranchesForm;
        private TcCallCenterOutboundSalaryForm            salaryForm;
        private TcCallCenterOutboundPayMasterGenerateForm payMasterGenerateForm;
        private TcCallCenterOutboundAnalyzeForm           analyzeForm;

        public static bool ResetMasterFormFilter { get; set; }

        public TcCallCenterOutboundSettingsForm SettingsForm
        {
            get { return settingsForm; }
        }

        public TcCallCenterOutboundMasterForm MasterForm
        {
            get { return masterForm; }
        }

        public TcBanksAndBranchesForm BanksAndBranchesForm
        {
            get { return banksAndBranchesForm; }
        }

        public TcCallCenterOutboundSalaryForm SalaryForm
        {
            get { return salaryForm; }
        }

        public TcCallCenterOutboundAnalyzeForm AnalyzeForm
        {
            get { return analyzeForm; }
        }

        public TcCallCenterOutboundForm()
        {
            InitializeComponent();

            settingsForm = new TcCallCenterOutboundSettingsForm(this);
        }

        public bool InitializeFormsAndShowOtherTabs()
        {
            masterForm              = new TcCallCenterOutboundMasterForm(this, settingsForm.MasterFilePath);
            banksAndBranchesForm    = new TcBanksAndBranchesForm(settingsForm.BanksAndBranchesFilePath);
            salaryForm              = new TcCallCenterOutboundSalaryForm(this, settingsForm.SalaryFilePath);
            analyzeForm             = new TcCallCenterOutboundAnalyzeForm(this);
            payMasterGenerateForm   = new TcCallCenterOutboundPayMasterGenerateForm(this);

            bool succeed =  TcConfigurationFile.ValidForWorkingYearMonth() &&
                            ReloadMasterDataForm() &&
                            ReloadBanksAndBranchesForm() &&
                            ReloadSalaryForm();

            if (succeed)
            {
                ShowOtherTabs();
                ReloadAnalyzeForm();
            }
            else
            {
                // TcMessageBox.ShowWarning("Please correct the errors and load data again"); // Seemes like redundency message
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

        private bool ReloadSalaryForm()
        {
            try
            {
                salaryForm.ReloadData();
                return true;
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowWarning(string.Format("Failed to load {0} Salary Data\n{1}", Identifier, ex.Message));
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

        private void salaryTabPage_Enter(object sender, EventArgs e)
        {
            try
            {
                LoadFormToTab(salaryForm, salaryTabPage);
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

        private void TcCallCenterOutboundForm_Load(object sender, EventArgs e)
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
            RemoveTabPage(salaryTabPage);
            RemoveTabPage(analyzeTabPage);
            RemoveTabPage(payMasterTabPage);
        }

        private void ShowOtherTabs()
        {
            AddTabPage(masterDataTabPage);
            AddTabPage(banksAndBranchesTabPage);
            AddTabPage(salaryTabPage);
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

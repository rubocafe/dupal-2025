using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.UI.Common;
using DUPALPayroll.UI.Common.BanksAndBranches;
using DUPALPayroll.UI.Configuration.ConfigFile;
using DUPALPayroll.UI.SupervisorsAndBackOffice.Analyze;
using DUPALPayroll.UI.SupervisorsAndBackOffice.Generate;
using DUPALPayroll.UI.SupervisorsAndBackOffice.MasterData;
using DUPALPayroll.UI.SupervisorsAndBackOffice.Salary;
using DUPALPayroll.UI.SupervisorsAndBackOffice.Settings;
using System;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-10-01

namespace DUPALPayroll.UI.SupervisorsAndBackOffice
{
    public partial class TcSupervisorsAndBackOfficeForm : TcForm
    {
        public string Identifier = TcPaths.SupervisorsAndBackOfficeId;

        private TcSupervisorsAndBackOfficeSettingsForm          settingsForm;
        private TcSupervisorsAndBackOfficeMasterForm            masterForm;
        private TcBanksAndBranchesForm                          banksAndBranchesForm;
        private TcSupervisorsAndBackOfficeSalaryForm            salaryForm;
        private TcSupervisorsAndBackOfficePayMasterGenerateForm payMasterGenerateForm;
        private TcSupervisorsAndBackOfficeAnalyzeForm           analyzeForm;

        public static bool ResetMasterFormFilter { get; set; }

        public TcSupervisorsAndBackOfficeSettingsForm SettingsForm
        {
            get { return settingsForm; }
        }

        public TcSupervisorsAndBackOfficeMasterForm MasterForm
        {
            get { return masterForm; }
        }

        public TcBanksAndBranchesForm BanksAndBranchesForm
        {
            get { return banksAndBranchesForm; }
        }

        public TcSupervisorsAndBackOfficeSalaryForm SalaryForm
        {
            get { return salaryForm; }
        }

        public TcSupervisorsAndBackOfficeAnalyzeForm AnalyzeForm
        {
            get { return analyzeForm; }
        }

        public TcSupervisorsAndBackOfficeForm()
        {
            InitializeComponent();

            settingsForm = new TcSupervisorsAndBackOfficeSettingsForm(this);
        }

        public bool InitializeFormsAndShowOtherTabs()
        {
            masterForm              = new TcSupervisorsAndBackOfficeMasterForm(this, settingsForm.MasterFilePath);
            banksAndBranchesForm    = new TcBanksAndBranchesForm(settingsForm.BanksAndBranchesFilePath);
            salaryForm              = new TcSupervisorsAndBackOfficeSalaryForm(this, settingsForm.SalaryFilePath);
            analyzeForm             = new TcSupervisorsAndBackOfficeAnalyzeForm(this);
            payMasterGenerateForm   = new TcSupervisorsAndBackOfficePayMasterGenerateForm(this);

            bool succeed = TcConfigurationFile.ValidForWorkingYearMonth() &&
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

        private void TcPremierSalesForm_Load(object sender, EventArgs e)
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

using Payroll.Library.General;
using Payroll.Library.MetaData;
using Payroll.UI.Business.Analyze;
using Payroll.UI.Business.Generate;
//using Payroll.UI.Business.Analyze;
//using Payroll.UI.Business.Generate;
using Payroll.UI.Business.MasterData;
using Payroll.UI.Business.Salary;
//using Payroll.UI.Business.Salary;
using Payroll.UI.Business.Settings;
using Payroll.UI.Common.BanksAndBranches;
using Payroll.UI.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-08-26

namespace Payroll.UI.Business
{
    public partial class TcBusinessForm : TcForm
    {
        public string Customer = "Customer";
        public string Business = "Business";

        private TcBusinessSettingsForm              settingsForm;
        private TcBusinessMasterForm                masterForm;
        private TcBanksAndBranchesForm              banksAndBranchesForm;
        private TcBusinessSalaryForm                salaryForm;
        private TcBusinessAnalyzeForm               analyzeForm;
        private TcBusinessPaymentsForm              paymentsForm;

        public static bool ResetMasterFormFilter { get; set; }

        public TcBusinessSettingsForm SettingsForm
        {
            get { return settingsForm; }
        }

        public TcBusinessMasterForm MasterForm
        {
            get { return masterForm; }
        }

        public TcBanksAndBranchesForm BanksAndBranchesForm
        {
            get { return banksAndBranchesForm; }
        }

        public TcBusinessSalaryForm SalaryForm
        {
            get { return salaryForm; }
        }

        public TcBusinessAnalyzeForm AnalyzeForm
        {
            get { return analyzeForm; }
        }

        public TcBusinessForm(string customer = "Customer", string business = "Business")
        {
            InitializeComponent();

            Customer = customer;
            Business = business;

            settingsForm = new TcBusinessSettingsForm(this);
        }

        public bool InitializeFormsAndShowOtherTabs(TcMetaData metaData)
        {
            masterForm              = new TcBusinessMasterForm(this, metaData.MasterData, settingsForm.MasterFilePath);
            banksAndBranchesForm    = new TcBanksAndBranchesForm(settingsForm.BanksAndBranchesFilePath);
            salaryForm              = new TcBusinessSalaryForm(this, metaData.Salary, settingsForm.SalaryFilePath);
            analyzeForm             = new TcBusinessAnalyzeForm(this, metaData);
            paymentsForm   = new TcBusinessPaymentsForm(this, metaData);

            bool succeed = 
                ReloadMasterDataForm() &&
                ReloadBanksAndBranchesForm() &&
                ReloadSalaryForm();

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

        private bool ReloadSalaryForm()
        {
            try
            {
                salaryForm.ReloadData();
                return true;
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowWarning(string.Format("Failed to load Salary Data\n{0}", ex.Message));
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
                LoadFormToTab(paymentsForm, paymentsTabPage);

                paymentsForm.ReInitialize();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        #endregion

        private void TcBusinessForm_Load(object sender, EventArgs e)
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
            RemoveTabPage(banksAndBranchesTabPage);
            RemoveTabPage(masterDataTabPage);
            RemoveTabPage(salaryTabPage);
            RemoveTabPage(analyzeTabPage);
            RemoveTabPage(paymentsTabPage);
        }

        private void ShowOtherTabs()
        {
            AddTabPage(banksAndBranchesTabPage);
            AddTabPage(masterDataTabPage);
            AddTabPage(salaryTabPage);
            AddTabPage(analyzeTabPage);
            AddTabPage(paymentsTabPage);
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

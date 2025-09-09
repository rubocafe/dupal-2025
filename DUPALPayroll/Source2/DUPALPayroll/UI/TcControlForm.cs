using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Auditors;
using DUPALPayroll.UI.CallCenterInbound;
using DUPALPayroll.UI.CallCenterOutbound;
using DUPALPayroll.UI.CareTakers;
using DUPALPayroll.UI.CommissionAgents;
using DUPALPayroll.UI.Common;
using DUPALPayroll.UI.Configuration;
using DUPALPayroll.UI.CustomerCare;
using DUPALPayroll.UI.Epf;
using DUPALPayroll.UI.Etf;
using DUPALPayroll.UI.OfficeStaff;
using DUPALPayroll.UI.PremierSales;
using DUPALPayroll.UI.SupervisorsAndBackOffice;
using DUPALPayroll.UI.Tools;
using System;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-08-22

namespace DUPALPayroll.UI
{
    public partial class TcControlForm : Form
    {
        private static Panel                staticContentPanel;
        private static TcForm               currentForm;
        private static TcNavigationButton   activeButton;

        public TcConfigurationForm              configurationForm;
        public TcCommissionAgentsForm           CommissionAgentsForm;
        public TcCareTakersForm                 CareTakersForm;
        public TcAuditorsForm                   AuditorsForm;
        public TcCallCenterInboundForm          CallCenterInboundForm;
        public TcCallCenterOutboundForm         CallCenterOutboundForm;
        public TcCustomerCareForm               CustomerCareForm;
        public TcOfficeStaffForm                OfficeStaffForm;
        public TcPremierSalesForm               PremierSalesForm;
        public TcToolsForm                      ToolsForm;
        public TcSupervisorsAndBackOfficeForm   SupervisorsAndBackOfficeForm;
        public TcEpfControlForm                 EpfControlForm;
        public TcEtfControlForm                 EtfControlForm;

        public static TcControlForm Self { get; set; }

        public TcControlForm()
        {
            InitializeComponent();

            staticContentPanel = contentPanel;

            Self = this;
        }

        private TcNavigationFormsController GetCurrentNavigationFormsController()
        {
            TcNavigationFormsController navigationFormsController = new TcNavigationFormsController(Self);

            return navigationFormsController;
        }

        public static bool Show(TcForm form)
        {
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;

            staticContentPanel.Controls.Clear();
            staticContentPanel.Controls.Add(form);

            form.Show();
            currentForm = form;

            return true;
        }

        private static void ShowFormAndActivateButton(TcForm form, TcNavigationButton button)
        {
            if (currentForm == null || 
                (currentForm != null && currentForm.CanLeave()))
            {
                if (TcControlForm.Show(form))
                {
                    ActivateButton(button);
                }
            }
        }

        private static void ActivateButton(TcNavigationButton button)
        {
            if (activeButton != null)
            {
                activeButton.Inactivate();
            }

            activeButton = button;
            button.Activate();
        }

        private void TcControlForm_Load(object sender, EventArgs e)
        {
            try
            {
                configurationNavigationButton_Click(configurationNavigationButton, EventArgs.Empty);
                UpdateWorkingYearMonthLabel();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        #region Public Methods

        public static void ResestForms()
        {
            TcNavigationFormsController navigationFormsController = Self.GetCurrentNavigationFormsController();
            navigationFormsController.ResestForms();
        }

        public static void UpdateWorkingYearMonthLabel()
        {
            Self.SetWorkingYearMonthLabel();
        }

        private void SetWorkingYearMonthLabel()
        {
            TcYearMonth workingYearMonth = TcSettings.WorkingYearMonth;
            workingYearMonthLabel.Text = workingYearMonth.ToDate().ToString("MMMM yyyy");

            if (TcVersions.IsEpfEtfSupported(workingYearMonth))
            {
                epfNavigationButton.Enabled = true;
                etfNavigationButton.Enabled = true;
            }
            else
            {
                epfNavigationButton.Enabled = false;
                etfNavigationButton.Enabled = false;
            }
        }

        #endregion

        #region Navigation Button Events

        private void configurationNavigationButton_Click(object sender, EventArgs e)
        {
            try
            {
                configurationForm = new TcConfigurationForm();
                TcControlForm.ShowFormAndActivateButton(configurationForm, configurationNavigationButton);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void commissionAgentsNavigationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (CommissionAgentsForm == null)
                {
                    CommissionAgentsForm = new TcCommissionAgentsForm();
                }

                TcControlForm.ShowFormAndActivateButton(CommissionAgentsForm, commissionAgentsNavigationButton);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void careTakersNavigationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (CareTakersForm == null)
                {
                    CareTakersForm = new TcCareTakersForm();
                }

                TcControlForm.ShowFormAndActivateButton(CareTakersForm, careTakersNavigationButton);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void auditorsNavigationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (AuditorsForm == null)
                {
                    AuditorsForm = new TcAuditorsForm();
                }

                TcControlForm.ShowFormAndActivateButton(AuditorsForm, auditorsNavigationButton);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void callCenterInboundNavigationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (CallCenterInboundForm == null)
                {
                    CallCenterInboundForm = new TcCallCenterInboundForm();
                }

                TcControlForm.ShowFormAndActivateButton(CallCenterInboundForm, callCenterInboundNavigationButton);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void callCenterOutboundNavigationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (CallCenterOutboundForm == null)
                {
                    CallCenterOutboundForm = new TcCallCenterOutboundForm();
                }
                
                TcControlForm.ShowFormAndActivateButton(CallCenterOutboundForm, callCenterOutboundNavigationButton);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void customerCareNavigationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (CustomerCareForm == null)
                {
                    CustomerCareForm = new TcCustomerCareForm();
                }

                TcControlForm.ShowFormAndActivateButton(CustomerCareForm, customerCareNavigationButton);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void officeStaffNavigationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (OfficeStaffForm == null)
                {
                    OfficeStaffForm = new TcOfficeStaffForm();
                }

                TcControlForm.ShowFormAndActivateButton(OfficeStaffForm, officeStaffNavigationButton);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void premierSalesNavigationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (PremierSalesForm == null)
                {
                    PremierSalesForm = new TcPremierSalesForm();
                }

                TcControlForm.ShowFormAndActivateButton(PremierSalesForm, premierSalesNavigationButton);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void supervisorsAndBackOfficeNavigationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (SupervisorsAndBackOfficeForm == null)
                {
                    SupervisorsAndBackOfficeForm = new TcSupervisorsAndBackOfficeForm();
                }

                TcControlForm.ShowFormAndActivateButton(SupervisorsAndBackOfficeForm, supervisorsAndBackOfficeNavigationButton);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void epfNavigationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (EpfControlForm == null)
                {
                    EpfControlForm = new TcEpfControlForm();
                }

                TcControlForm.ShowFormAndActivateButton(EpfControlForm, epfNavigationButton);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void etfNavigationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (EtfControlForm == null)
                {
                    EtfControlForm = new TcEtfControlForm();
                }

                TcControlForm.ShowFormAndActivateButton(EtfControlForm, etfNavigationButton);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void toolsNavigationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ToolsForm == null)
                {
                    ToolsForm = new TcToolsForm();
                }

                TcControlForm.ShowFormAndActivateButton(ToolsForm, toolsNavigationButton);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        #endregion

        #region Menu Events

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = TcMessageBox.ShowYesNoWarning("Do you want to exit?");
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TcAboutBox about = new TcAboutBox();
                about.ShowDialog();
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }

        #endregion

    }
}

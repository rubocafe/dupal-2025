
// Harshan Nishantha
// 2014-01-06

namespace DUPALPayroll.UI.Common
{
    public class TcNavigationFormsController
    {
        private TcControlForm controlForm;

        public TcNavigationFormsController(TcControlForm controlForm)
        {
            this.controlForm = controlForm;
        }

        public void ResestForms()
        {
            controlForm.AuditorsForm                    = null;
            controlForm.CommissionAgentsForm            = null;
            controlForm.CareTakersForm                  = null;
            controlForm.CallCenterInboundForm           = null;
            controlForm.CallCenterOutboundForm          = null;
            controlForm.CustomerCareForm                = null;
            controlForm.OfficeStaffForm                 = null;
            controlForm.PremierSalesForm                = null;
            controlForm.ToolsForm                       = null;
            controlForm.SupervisorsAndBackOfficeForm    = null;
            controlForm.EpfControlForm                  = null;
            controlForm.EtfControlForm                  = null;
        }

        public void ResestUnloadedForms()
        {
            if (controlForm.AuditorsForm != null && !controlForm.AuditorsForm.Loaded())
            {
                controlForm.AuditorsForm = null;
            }

            if (controlForm.CommissionAgentsForm != null && !controlForm.CommissionAgentsForm.Loaded())
            {
                controlForm.CommissionAgentsForm = null;
            }

            if (controlForm.CareTakersForm != null && !controlForm.CareTakersForm.Loaded())
            {
                controlForm.CareTakersForm = null;
            }

            if (controlForm.CallCenterInboundForm != null && !controlForm.CallCenterInboundForm.Loaded())
            {
                controlForm.CallCenterInboundForm = null;
            }

            if (controlForm.CallCenterOutboundForm != null && !controlForm.CallCenterOutboundForm.Loaded())
            {
                controlForm.CallCenterOutboundForm = null;
            }

            if (controlForm.CustomerCareForm != null && !controlForm.CustomerCareForm.Loaded())
            {
                controlForm.CustomerCareForm = null;
            }

            if (controlForm.OfficeStaffForm != null && !controlForm.OfficeStaffForm.Loaded())
            {
                controlForm.OfficeStaffForm = null;
            }

            if (controlForm.PremierSalesForm != null && !controlForm.PremierSalesForm.Loaded())
            {
                controlForm.PremierSalesForm = null;
            }

            if (controlForm.ToolsForm != null && !controlForm.ToolsForm.Loaded())
            {
                controlForm.ToolsForm = null;
            }

            if (controlForm.SupervisorsAndBackOfficeForm != null && !controlForm.SupervisorsAndBackOfficeForm.Loaded())
            {
                controlForm.SupervisorsAndBackOfficeForm = null;
            }

            if (controlForm.EpfControlForm != null && !controlForm.EpfControlForm.Loaded())
            {
                controlForm.EpfControlForm = null;
            }

            if (controlForm.EtfControlForm != null && !controlForm.EtfControlForm.Loaded())
            {
                controlForm.EtfControlForm = null;
            }
        }
    }
}

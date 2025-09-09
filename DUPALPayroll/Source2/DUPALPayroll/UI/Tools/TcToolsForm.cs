using DUPALPayroll.Controls;
using DUPALPayroll.UI.CommissionAgents.Tools.Compare;
using DUPALPayroll.UI.CommissionAgents.Tools.Decode;
using System;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-08-26

namespace DUPALPayroll.UI.Tools
{
    public partial class TcToolsForm : TcForm
    {
        private TcPayMasterDecodeForm decodeForm;
        private TcPayMasterCompareForm compareForm;

        public TcToolsForm()
        {
            InitializeComponent();
        }

        private void LoadFormToTab(Form form, TabPage tabPage)
        {
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;

            tabPage.Controls.Clear();
            tabPage.Controls.Add(form);

            form.Show();
        }

        public override bool Loaded()
        {
            return true;
        }

        #region Tab Control - Forms

        private void decodePayMasterTabPage_Enter(object sender, EventArgs e)
        {
            if (decodeForm == null)
            {
                decodeForm = new TcPayMasterDecodeForm();
            }

            LoadFormToTab(decodeForm, decodePayMasterTabPage);
        }

        private void comparePayMasterTabPage_Enter(object sender, EventArgs e)
        {
            if (compareForm == null)
            {
                compareForm = new TcPayMasterCompareForm();
            }

            LoadFormToTab(compareForm, comparePayMasterTabPage);
        }

        #endregion

        private void TcToolsForm_Load(object sender, EventArgs e)
        {
            decodePayMasterTabPage_Enter(null, EventArgs.Empty);
            decodePayMasterTabPage.Focus();
        }

    }
}

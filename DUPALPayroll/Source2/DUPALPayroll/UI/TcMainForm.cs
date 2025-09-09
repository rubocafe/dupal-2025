using DUPALPayroll.General;
using DUPALPayroll.Properties;
using System;
using System.IO;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-08-22

namespace DUPALPayroll.UI
{
    public partial class TcMainForm : Form
    {
        private static Panel staticContentPanel;

        public TcMainForm()
        {
            InitializeComponent();

            staticContentPanel = this.contentPanel;
        }

        public static void Show(Form form)
        {
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;

            staticContentPanel.Controls.Clear();
            staticContentPanel.Controls.Add(form);

            form.Show();
        }

        private void TcMainForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                TcControlForm form = new TcControlForm();
                TcMainForm.Show(form);
            }
            catch (Exception ex)
            {
                TcMessageBox.ShowAndLogUnexpectedError(ex);
            }
        }
    }
}

using System.Windows.Forms;

// Harshan Nishantha
// 2015-10-06

namespace Payroll.UI.Controls
{
    public partial class TcTabControlForm : TcForm
    {
        public Form SelectedForm { get; set; }
        public TabPage SelectedTab { get; set; }

        public TcTabControlForm()
        {
            InitializeComponent();
        }

        public void LoadFormToTab(Form form, TabPage tabPage)
        {
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;

            tabPage.Controls.Clear();
            tabPage.Controls.Add(form);

            SelectedForm    = form;
            SelectedTab     = tabPage;

            form.Show();
        }

        public void AddTabPage(TabPage tabPage)
        {
            if (!tabControl.Contains(tabPage))
            {
                tabControl.Controls.Add(tabPage);
            }
        }

        public void RemoveTabPage(TabPage tabPage)
        {
            if (tabControl.Contains(tabPage))
            {
                tabControl.Controls.Remove(tabPage);
            }
        }

        public override bool CanLeave()
        {
            if (SelectedForm != null)
            {
                if (SelectedForm is TcForm)
                {
                    TcForm form = SelectedForm as TcForm;
                    return form.CanLeave();
                }
            }

            return true;
        }
    }
}

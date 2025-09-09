using System.Drawing;
using System.Windows.Forms;

// Harshan Nishantha
// 2015-10-06

namespace Payroll.UI.Controls
{
    public partial class TcNavigationButton : Button
    {
        public bool Active { get; private set; }
        public TcForm BindedForm { get; set; }

        public TcNavigationButton()
        {
            InitializeComponent();

            Inactivate();
        }

        public void Activate()
        {
            this.Font       = new Font(this.Font, FontStyle.Bold);
            this.ForeColor  = Color.White;
            this.BackColor  = Color.Navy;
        }

        public void Inactivate()
        {
            this.Font       = new Font(this.Font, FontStyle.Regular);
            this.ForeColor  = Color.Black;
            this.BackColor  = Color.Transparent;
        }
    }
}

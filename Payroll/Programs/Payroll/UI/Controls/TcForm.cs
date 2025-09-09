using System.Windows.Forms;

// Harshan Nishantha
// 2015-10-06

namespace Payroll.UI.Controls
{
    public partial class TcForm : Form
    {
        public TcForm()
        {
            InitializeComponent();
        }

        public virtual bool Loaded()
        {
            return false;
        }

        public virtual bool CanLeave()
        {
            return true;
        }
    }
}

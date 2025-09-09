using System.Windows.Forms;

// Harshan Nishantha
// 2013-08-22

namespace DUPALPayroll.Controls
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

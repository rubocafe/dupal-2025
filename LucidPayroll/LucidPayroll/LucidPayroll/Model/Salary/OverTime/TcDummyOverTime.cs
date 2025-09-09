using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2014-01-24

namespace LucidPayroll.Model.Salary.OverTime
{
    public class TcDummyOverTime : TcOverTime
    {
        public TcDummyOverTime(decimal rate, decimal hoursWorked)
            : base("Dummy OT", rate, hoursWorked)
        {
            Calculate();
        }
    }
}

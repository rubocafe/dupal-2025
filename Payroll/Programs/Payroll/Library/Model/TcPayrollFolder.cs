using Payroll.Library.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-10-06

namespace Payroll.Library.Model
{
    public class TcPayrollFolder
    {
        public string Company { get; set; }
        public string YearMonth { get; set; }
        public string Customer { get; set; }
        public string Business { get; set; }

        public TcPayrollFolder(string company, string yearMonth, string customer, string business)
        {
            this.Company    = company;
            this.YearMonth  = yearMonth;
            this.Business   = business;
            this.Customer   = customer;
        }
    }
}

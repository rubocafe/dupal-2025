using Payroll.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2013-09-17

namespace Payroll.Library.Payments
{
    public enum TeRowDisplaytype
    {
        Normal,
        Error
    }

    public class TcMandatoryCondition
    {
        public string   SatisfiedDescription { get; set; }
        public string   UnsatisfiedDescription { get; set; }
        public bool     Mandatory { get; set; }
        public bool     Satisfied { get; set; }
        public TeRowDisplaytype RowDisplayType { get; set; }

        public string Description
        {
            get
            {
                if (Satisfied)
                {
                    return SatisfiedDescription;
                }
                else
                {
                    return UnsatisfiedDescription;
                }
            }
        }

        public Image Image
        {
            get
            {
                if (Satisfied)
                {
                    return Resources.Tick;
                }
                else
                {
                    if (Mandatory)
                    {
                        return Resources.Cross;
                    }
                    else
                    {
                        return Resources.CrossAmber;
                    }
                }
            }
        }

        public TcMandatoryCondition(string satisfiedDescription, string unatisfiedDescription, bool mandatory, bool satisfied) :
            this(satisfiedDescription, unatisfiedDescription, mandatory, satisfied, TeRowDisplaytype.Normal)
        {
        }

        public TcMandatoryCondition(string satisfiedDescription, string unatisfiedDescription, bool mandatory, bool satisfied, TeRowDisplaytype type)
        {
            SatisfiedDescription    = satisfiedDescription;
            UnsatisfiedDescription  = unatisfiedDescription;
            Mandatory               = mandatory;
            Satisfied               = satisfied;
            RowDisplayType          = type;
        }
    }
}

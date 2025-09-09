
// Harshan Nishantha
// 2014-01-23

namespace LucidPayroll.Model.Salary.Commission
{
    public class TcCommission
    {
        public string Name { get; set; }
        public decimal Value { get; set; }

        public TcCommission(string name) : this(name, 0)
        {
        }

        public TcCommission(string name, decimal value)
        {
            Name    = name;
            Value   = value;
        }
    }
}

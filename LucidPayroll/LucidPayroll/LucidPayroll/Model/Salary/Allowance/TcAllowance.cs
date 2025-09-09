
// Harshan Nishantha
// 2014-01-23

namespace LucidPayroll.Model.Salary.Allowance
{
    public class TcAllowance
    {
        public string Name { get; set; }
        public decimal Value { get; set; }

        public TcAllowance(string name) : this(name, 0)
        {
        }

        public TcAllowance(string name, decimal value)
        {
            Name    = name;
            Value   = value;
        }
    }
}

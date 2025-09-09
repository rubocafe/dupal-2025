
// Harshan Nishantha
// 2014-01-23

namespace LucidPayroll.Model.Salary.Deduction
{
    public class TcDeduction
    {
        public string Name { get; set; }
        public decimal Value { get; set; }

        public TcDeduction(string name) : this(name, 0)
        {
        }

        public TcDeduction(string name, decimal value)
        {
            Name    = name;
            Value   = value;
        }
    }
}

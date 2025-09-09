
// Harshan Nishantha
// 2014-01-23

namespace LucidPayroll.Model.Salary.Incentive
{
    public class TcIncentive
    {
        public string Name { get; set; }
        public decimal Value { get; set; }

        public TcIncentive(string name) : this(name, 0)
        {
        }

        public TcIncentive(string name, decimal value)
        {
            Name = name;
            Value = value;
        }
    }
}

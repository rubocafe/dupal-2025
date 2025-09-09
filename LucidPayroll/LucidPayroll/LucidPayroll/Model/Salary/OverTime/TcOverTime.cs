
// Harshan Nishantha
// 2014-01-23

namespace LucidPayroll.Model.Salary.OverTime
{
    public class TcOverTime
    {
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal Value { get; set; }

        public TcOverTime(string name, decimal rate) : this(name, rate, 0)
        {
        }

        public TcOverTime(string name, decimal rate, decimal hoursWorked)
        {
            Name            = name;
            Rate            = rate;
            HoursWorked     = hoursWorked;
        }

        public virtual void Calculate()
        {
            Value = Rate * HoursWorked;
        }
    }
}

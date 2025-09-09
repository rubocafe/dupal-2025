using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-11-04

namespace Payroll.Library.Model
{
    public class TcTuple<T>
    {
        public string Name { get; set; }
        public T Value { get; set; }

        public TcTuple(string name, T value)
        {
            Name    = name;
            Value   = value;
        }
    }
}

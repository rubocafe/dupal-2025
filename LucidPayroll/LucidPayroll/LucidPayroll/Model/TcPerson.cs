using System;

// Harshan Nishantha
// 2014-01-23

namespace LucidPayroll.Model
{
    public class TcPerson
    {
        public string NIC { get; set; }
        public string Initials { get; set; }
        public string LastName { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public Nullable<DateTime> DateOfJoin { get; set; }
        public string EmployeeNumber { get; set; }

        public TcPerson()
        {
        }
    }
}

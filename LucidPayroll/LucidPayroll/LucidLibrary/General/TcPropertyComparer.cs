using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

// Harshan Nishantha
// 2013-08-22

namespace LucidPayroll.General
{
    public class TcPropertyComparer<T> : IComparer<T>
    {
        private PropertyInfo        propertyInfo;
        private ListSortDirection   sortDirection;

        public TcPropertyComparer(string propertyName, ListSortDirection sortDirection)
        {
            propertyInfo = typeof(T).GetProperty(propertyName);
            this.sortDirection = sortDirection;
        }

        public int Compare(T x, T y)
        {
            int returnValue = 0;

            try
            {
                if (sortDirection == ListSortDirection.Ascending)
                {
                    returnValue = Comparer.Default.Compare(propertyInfo.GetValue(x, null), propertyInfo.GetValue(y, null));
                }
                else
                {
                    returnValue = Comparer.Default.Compare(propertyInfo.GetValue(y, null), propertyInfo.GetValue(x, null));
                }
            }
            catch (Exception)
            {
            }
            

            return returnValue;
        }
    }
}

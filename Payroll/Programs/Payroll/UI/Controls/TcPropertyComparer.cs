using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

// Harshan Nishantha
// 2015-10-06

namespace Payroll.UI.Controls
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

        public int CompareAsInt(T x, T y)
        {
            int returnValue = 0;

            int intX = 0;
            int intY = 0;

            try
            {
                int.TryParse((string)propertyInfo.GetValue(x, null), out intX);
                int.TryParse((string)propertyInfo.GetValue(y, null), out intY);

                if (sortDirection == ListSortDirection.Ascending)
                {
                    returnValue = Comparer.Default.Compare(intX, intY);
                }
                else
                {
                    returnValue = Comparer.Default.Compare(intY, intX);
                }
            }
            catch (Exception)
            {
            }


            return returnValue;
        }
    }

    public class TcExplicitIntPropertyComparer<T> : IComparer<T>
    {
        private PropertyInfo propertyInfo;
        private ListSortDirection sortDirection;

        public TcExplicitIntPropertyComparer(string propertyName, ListSortDirection sortDirection)
        {
            propertyInfo = typeof(T).GetProperty(propertyName);
            this.sortDirection = sortDirection;
        }

        public int Compare(T x, T y)
        {
            int returnValue = 0;

            int intX = 0;
            int intY = 0;

            try
            {
                int.TryParse((string)propertyInfo.GetValue(x, null), out intX);
                int.TryParse((string)propertyInfo.GetValue(y, null), out intY);

                if (sortDirection == ListSortDirection.Ascending)
                {
                    returnValue = Comparer.Default.Compare(intX, intY);
                }
                else
                {
                    returnValue = Comparer.Default.Compare(intY, intX);
                }
            }
            catch (Exception)
            {
            }


            return returnValue;
        }
    }
}

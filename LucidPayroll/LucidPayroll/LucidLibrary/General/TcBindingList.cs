using System.Collections.Generic;
using System.ComponentModel;

// Harshan Nishantha
// 2013-08-22

namespace LucidPayroll.General
{
    public class TcBindingList<T> : BindingList<T>
    {
        private bool isSorted = false;
        private PropertyDescriptor  propertyDescriptor;
        private ListSortDirection   listSortDirection;

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        protected override bool IsSortedCore
        {
            get { return isSorted; }
        }

        protected override void RemoveSortCore()
        {
            isSorted = false;
        }

        protected override ListSortDirection SortDirectionCore
        {
            get
            {
                return listSortDirection;
            }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get
            {
                return propertyDescriptor;
            }
        }

        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            List<T> items = this.Items as List<T>;

            if (items != null)
            {
                TcPropertyComparer<T> pc = new TcPropertyComparer<T>(property.Name, direction);
                items.Sort(pc);
                isSorted = true;
                propertyDescriptor  = property;
                listSortDirection   = direction;
            }
            else
            {
                isSorted = false;
            }

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        public void Sort(PropertyDescriptor descriptor, ListSortDirection direction)
        {
            ApplySortCore(descriptor, direction);
        }
    }
}

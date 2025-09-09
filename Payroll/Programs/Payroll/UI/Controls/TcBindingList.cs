using System.Collections.Generic;
using System.ComponentModel;

// Harshan Nishantha
// 2015-10-06

namespace Payroll.UI.Controls
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
                if (sortAsInt)
                {
                    TcExplicitIntPropertyComparer<T> pc = new TcExplicitIntPropertyComparer<T>(property.Name, direction);
                    items.Sort(pc);
                    sortAsInt = false;
                } else {
                    TcPropertyComparer<T> pc = new TcPropertyComparer<T>(property.Name, direction);
                    items.Sort(pc);
                }

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

        private bool sortAsInt = false;
        public void SortAsInt(PropertyDescriptor descriptor, ListSortDirection direction)
        {
            sortAsInt = true;
            ApplySortCore(descriptor, direction);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace My2Cents.HTC.AHPilotStats
{
    public class SortableBindingList<T> : BindingList<T>
    {
        private bool _isSorted;
        private ListSortDirection _SortDirection;
        private PropertyDescriptor _SortProperty;


        public void SortList(string propName, ListSortDirection direction)
        {
            List<T> items = this.Items as List<T>;
            PropertyDescriptorCollection propCollection = TypeDescriptor.GetProperties(items[0]);

            PropertyDescriptor propDesc = null;
            foreach (PropertyDescriptor currDesc in propCollection)
            {
                if (currDesc.Name == propName)
                {
                    propDesc = currDesc;
                    break;
                }

            }

            if (propDesc != null)
                ApplySortCore(propDesc, direction);
        }


        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            // Get list to sort
            List<T> items = this.Items as List<T>;

            _SortDirection = direction;
            _SortProperty = property;

            // Apply and set the sort, if items to sort
            if (items != null)
            {
                PropertyComparer<T> pc = new PropertyComparer<T>(property, direction);
                items.Sort(pc);
                _isSorted = true;
            }
            else
            {
                _isSorted = false;
            }

            // Let bound controls know they should refresh their views
            this.OnListChanged( new ListChangedEventArgs(ListChangedType.Reset, -1) );
        }

        protected override bool IsSortedCore
        {
            get { return _isSorted; }
        }

        protected override void RemoveSortCore()
        {
            _isSorted = false;
        }


        protected override bool SupportsSortingCore
        {
            get { return true; }
        }


        protected override ListSortDirection SortDirectionCore
        {
            get { return _SortDirection; }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get { return _SortProperty; }
        }

    }

}

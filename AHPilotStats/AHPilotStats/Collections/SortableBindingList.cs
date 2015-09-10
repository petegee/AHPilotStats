using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace My2Cents.HTC.AHPilotStats.Collections
{
    public class SortableBindingList<T> : BindingList<T>
    {
        public SortableBindingList() { }

        public SortableBindingList(IList<T> items)
            : base(items)
        {
        }

        private bool _isSorted;
        private ListSortDirection _sortDirection;
        private PropertyDescriptor _sortProperty;

        protected override bool IsSortedCore
        {
            get { return _isSorted; }
        }

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get { return _sortDirection; }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get { return _sortProperty; }
        }


        public void SortList(string propName, ListSortDirection direction)
        {
            var items = Items as List<T>;
            var propCollection = TypeDescriptor.GetProperties(items[0]);

            var propDesc = propCollection.Cast<PropertyDescriptor>()
                .FirstOrDefault(currDesc => currDesc.Name == propName);

            if (propDesc != null)
                ApplySortCore(propDesc, direction);
        }


        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            // Get list to sort
            var items = Items as List<T>;

            _sortDirection = direction;
            _sortProperty = property;

            // Apply and set the sort, if items to sort
            if (items != null)
            {
                var pc = new PropertyComparer<T>(property, direction);
                items.Sort(pc);
                _isSorted = true;
            }
            else
            {
                _isSorted = false;
            }

            // Let bound controls know they should refresh their views
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override void RemoveSortCore()
        {
            _isSorted = false;
        }
    }
}
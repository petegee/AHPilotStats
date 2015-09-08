using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace My2Cents.HTC.AHPilotStats
{
    public class SortableList<T> : List<T>
    {
        protected bool IsSortedCore { get; private set; }

        protected bool SupportsSortingCore
        {
            get { return true; }
        }

        protected ListSortDirection SortDirectionCore { get; private set; }

        protected PropertyDescriptor SortPropertyCore { get; private set; }


        public void SortList(string propName, ListSortDirection direction)
        {
            var propCollection = TypeDescriptor.GetProperties(this[0]);

            var propDesc = propCollection.Cast<PropertyDescriptor>()
                .FirstOrDefault(currDesc => currDesc.Name == propName);

            if (propDesc != null)
                ApplySortCore(propDesc, direction);
        }


        protected void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            SortDirectionCore = direction;
            SortPropertyCore = property;
            var pc = new PropertyComparer<T>(property, direction);
            Sort(pc);
            IsSortedCore = true;
        }

        protected void RemoveSortCore()
        {
            IsSortedCore = false;
        }
    }


    public class PropertyComparer<T> : Comparer<T>
    {
        private readonly ListSortDirection _direction;
        private readonly PropertyDescriptor _property;

        public PropertyComparer(PropertyDescriptor prop, ListSortDirection direction)
        {
            _property = prop;
            _direction = direction;
        }


        public override int Compare(T xWord, T yWord)
        {
            // Get property values
            var xValue = GetPropertyValue(xWord, _property.Name);
            var yValue = GetPropertyValue(yWord, _property.Name);

            // Determine sort order
            return _direction == ListSortDirection.Ascending 
                ? CompareAscending(xValue, yValue) 
                : CompareDescending(xValue, yValue);
        }


        public bool Equals(T xWord, T yWord)
        {
            return xWord.Equals(yWord);
        }


        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }


        private int CompareAscending(object xValue, object yValue)
        {
            if (xValue == null || yValue == null)
                return 0;

            int result;

            // If values implement IComparer`
            var value = xValue as IComparable;
            if (value != null)
            {
                result = value.CompareTo(yValue);
            }
            else
            {
                result = xValue.Equals(yValue) 
                    ? 0 
                    : CastNumberAndCompareTo(xValue, yValue);
            }

            return result;
        }


        private int CastNumberAndCompareTo(object objX, object objY)
        {
            if (objX == null || objY == null)
                return 0;

            var strX = objX.ToString();
            var strY = objY.ToString();

            // if its a numeric form, it should be coercable into a double and compared.
            if (!IsNumeric(strX) || !IsNumeric(strY)) 
                return string.Compare(strX, strY, StringComparison.Ordinal);

            double x, y;
            var xParseSuccess = double.TryParse(strX, out x);
            var yParseSuccess = double.TryParse(strY, out y);

            if (!xParseSuccess || !yParseSuccess) 
                return string.Compare(strX, strY, StringComparison.Ordinal);

            return x > y ? 1 : -1;

            // if all fails, then fall back on comparing as string (alphabetical).
        }


        private int CompareDescending(object xValue, object yValue)
        {
            // Return result adjusted for ascending or descending sort order ie
            // multiplied by 1 for ascending or -1 for descending
            return CompareAscending(xValue, yValue)*-1;
        }


        private object GetPropertyValue(T value, string prop)
        {
            // Get property
            var propertyInfo = value.GetType().GetProperty(prop);
            // Return value
            return propertyInfo.GetValue(value, null);
        }


        private bool IsNumeric(string str)
        {
            // attempt to coerce to a double, if we can't then, its likely not a valid number.
            double throwAwayValue = 0; // only used to keep compiler happy.
            return double.TryParse(str, out throwAwayValue);
        }
    }
}
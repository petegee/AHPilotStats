using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace My2Cents.HTC.AHPilotStats
{
    public class SortableList<T> : List<T>// BindingList<T>
    {
        private bool _isSorted;
        private ListSortDirection _SortDirection; 
        private PropertyDescriptor _SortProperty;


        public void SortList(string propName, ListSortDirection direction)
        {
            //List<T> items = this.Items as List<T>;
            //PropertyDescriptorCollection propCollection = TypeDescriptor.GetProperties(items[0]);
            PropertyDescriptorCollection propCollection = TypeDescriptor.GetProperties(this[0]);

            PropertyDescriptor propDesc=null;
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


        protected /*override*/ void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            // Get list to sort
            //List<T> items = this.Items as List<T>;

            _SortDirection = direction;
            _SortProperty = property;

            // Apply and set the sort, if items to sort
            //if (items != null)
            if (this != null)
            {
                PropertyComparer<T> pc = new PropertyComparer<T>(property, direction);
                this.Sort(pc);
                //items.Sort(pc);
                _isSorted = true;
            }
            else
            {
                _isSorted = false;
            }

            // Let bound controls know they should refresh their views
            //this.OnListChanged( new ListChangedEventArgs(ListChangedType.Reset, -1) );
        }

        protected /*override*/ bool IsSortedCore
        {
            get { return _isSorted; }
        }

        protected /*override*/ void RemoveSortCore()
        {
            _isSorted = false;
        }


        protected /*override*/ bool SupportsSortingCore
        {
            get { return true; }
        }


        protected /*override*/ ListSortDirection SortDirectionCore
        {
            get { return _SortDirection; }
        }

        protected /*override*/ PropertyDescriptor SortPropertyCore
        {
            get { return _SortProperty; }
        }

    }


    public class PropertyComparer<T> : Comparer<T>
    {
        private PropertyDescriptor _property; 
        private ListSortDirection _direction;

        public PropertyComparer(PropertyDescriptor prop, ListSortDirection direction)
        {
            _property = prop;
            _direction = direction;   
        }


        public override int Compare(T xWord, T yWord)
        {
            // Get property values
            object xValue = GetPropertyValue(xWord, _property.Name);
            object yValue = GetPropertyValue(yWord, _property.Name);

            // Determine sort order
            if(_direction == ListSortDirection.Ascending)
                return CompareAscending(xValue, yValue);
            else
                return CompareDescending(xValue, yValue);
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

            // If values implement IComparer
            if (xValue.GetType() is IComparable)
            {
                result = ((IComparable)xValue).CompareTo(yValue);
            }
            else
            {
                if (xValue.Equals(yValue))
                    result = 0; // If values don't implement IComparer but are equivalent
                else
                    result = CastNumberAndCompareTo(xValue, yValue);      
            }

            return result;
        }


        private int CastNumberAndCompareTo(object objX, object objY)
        {
            if (objX == null || objY == null)
                return 0;

            string strX = objX.ToString();
            string strY = objY.ToString();

            // if its a numeric form, it should be coercable into a double and compared.
            if (IsNumeric(strX) && IsNumeric(strY))
            {
                double x, y = 0;
                bool xParseSuccess = double.TryParse(strX, out x);
                bool yParseSuccess = double.TryParse(strY, out y);

                if (xParseSuccess && yParseSuccess)
                { 
                    if (x > y)
                        return 1;
                    else
                        return -1;               
                }
            }

            // if all fails, then fall back on comparing as string (alphabetical).
            return strX.CompareTo(strY);
        }


        private int CompareDescending(object xValue, object yValue)
        {
            // Return result adjusted for ascending or descending sort order ie
            // multiplied by 1 for ascending or -1 for descending
            return CompareAscending(xValue, yValue) * -1;
        }


        private object GetPropertyValue(T value, string prop)
        {
             // Get property
            PropertyInfo propertyInfo = value.GetType().GetProperty(prop);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2Cents.HTC.AHPilotStats.ExtensionMethods
{
    static class EnumerableExtensions
    {
        /// <summary>
        /// Add a more usefull Distinct Linq extension method for selecting distinct items 
        /// based on an expression which identifies the key to compare items in the collection.
        /// </summary>
        /// <typeparam name="TSource">The type of the source IEnumerable to select distinct from</typeparam>
        /// <typeparam name="TKey">the type of the key for comparison</typeparam>
        /// <param name="source">The source IEnumerable to select distinct from</param>
        /// <param name="keySelector">the func to select the key to use to compare</param>
        /// <returns>An enumerable collection with distinct items based on the key identified by <c ref="keySelector"/></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }
    }
}

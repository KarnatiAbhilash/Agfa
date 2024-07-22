using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agfa.Common.ExtensionMethods
{
    public static partial class IEnumerableExtensions
    {

        /// <summary>
        /// Convert an items to a Delimited String
        /// </summary>
        /// <typeparam name="T">Datatype of the items</typeparam>
        /// <param name="items">items to convert to string</param>
        /// <param name="Delimitor">Delimiter to use</param>
        /// <returns>
        /// String delimited by the supplied Delimitor
        /// </returns>
        /// <see cref="CollectionsExtensions"/>
        public static String ToDelimitedString<T>(this IEnumerable<T> items, String Delimitor)
        {
            return CollectionsExtensions.CollectionsExtensions.ToDelimitedString<T>(items, Delimitor).Trim(Delimitor);
        }

        /// <summary>
        /// Convert an items to a Delimited String.
        /// Assumes a comma for the Delimitor
        /// </summary>
        /// <typeparam name="T">Datatype of the items</typeparam>
        /// <param name="items">items to convert to string</param>
        /// <returns>A Comma delimited String</returns>
        /// <see cref="CollectionsExtensions"/>
        public static String ToDelimitedString<T>(this IEnumerable<T> items)
        {
            return CollectionsExtensions.CollectionsExtensions.ToDelimitedString<T>(items);
        }


        /// <summary>
        /// Converts an items of DateTime to a comma Delimited string using the specified Format
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="Format">The format.</param>
        /// <param name="Delimitor">The delimitor.</param>
        /// <returns></returns>
        public static String ToDelimitedString(this IEnumerable<DateTime> items, String Format, String Delimitor)
        {
            return CollectionsExtensions.CollectionsExtensions.ToDelimitedString(items, Format, Delimitor).Trim(Delimitor);
        }


        /// <summary>
        /// Converts an items of DateTime to a comma Delimited string using the specified Format
        /// </summary>
        /// <param name="items"></param>
        /// <param name="Format"></param>
        /// <returns></returns>
        public static String ToDelimitedString(this IEnumerable<DateTime> items, String Format)
        {
            return CollectionsExtensions.CollectionsExtensions.ToDelimitedString(items, Format);
        }

        /// <summary>
        /// Converts an items of DateTime to a comma Delimited string using the specified Format
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static String ToDelimitedString(this IEnumerable<DateTime> items)
        {
            return CollectionsExtensions.CollectionsExtensions.ToDelimitedString(items);
        }

        /// <summary>
        /// Converts the Date Time Collection to a String items
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="Format">The format.</param>
        /// <returns></returns>
        public static String[] ToStringitems(this IEnumerable<DateTime> items, String Format)
        {
            return CollectionsExtensions.CollectionsExtensions.ToArray(items, Format);
        }

        /// <summary>
        /// Converts the Date Time Collection to a String items
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static String[] ToStringitems(this IEnumerable<DateTime> items)
        {
            return CollectionsExtensions.CollectionsExtensions.ToArray(items);
        }



        /// <summary>
        /// Returns an item at a specific index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static T ItemAt<T>(this IEnumerable<T> items, int index)
        {
            return CollectionsExtensions.CollectionsExtensions.ItemAt(items, index);
        }

        /// <summary>
        /// Reverses a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<T> Reverse<T>(this IEnumerable<T> items)
        {
            return CollectionsExtensions.CollectionsExtensions.Reverse(items);
        }


        /// <summary>
        /// Slices a list of items at the a specified item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static IEnumerable<T> Slice<T>(this IEnumerable<T> items, T item)
        {
            foreach (T t in items)
            {
                if (t.Equals(item))
                    yield return t;
            }
            yield return default(T);
        }

        /// <summary>
        /// Checks if a collection is empty or null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns>
        /// 	<c>true</c> if the specified items is empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsEmpty<T>(this IEnumerable<T> items)
        {
            return CollectionsExtensions.CollectionsExtensions.IsEmpty(items);
        }
        /// <summary>
        /// Checks if a collection is Not empty and not null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns>
        /// 	<c>true</c> if [is not empty] [the specified items]; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsNotEmpty<T>(this IEnumerable<T> items)
        {
            return !items.IsEmpty();
        }

        /// <summary>
        /// For each looping construct for IEnumerable allowing the person to perform an
        /// action on each item in the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">The values.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (T v in values)
            {
                action(v);
            }
            return values;
        }
        /// <summary>
        /// Check if a value exists in a collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">The values.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static Boolean Exists<T>(this IEnumerable<T> values, T value)
        {
            foreach (T v in values)
            {
                if (v.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Paginates the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> list, int pageSize, int page)
        {
            return list.Skip(pageSize * (page)).Take(pageSize);
        }

        /// <summary>
        /// Paginates the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public static IQueryable<T> Paginate<T>(this IQueryable<T> list, int pageSize, int page)
        {
            return list.Skip(pageSize * (page)).Take(pageSize);
        }


        /// <summary>
        /// Converts a single dimension collection to a string array equivalent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<String> ToStringArray<T>(this IEnumerable<T> items)
        {
            List<String> lstStrings = new List<String>();

            foreach (T t in items)
            {
                lstStrings.Add(t.ToString());
            }
            return lstStrings.ToArray();
        }




        /// <summary>
        /// Add several ActiveDirectoryUsers to the list
        /// </summary>
        /// <param name="items"></param>
        public static IEnumerable<T> AddRange<T>(this IList<T> items, params T[] itemsToAdd)
        {
            if (itemsToAdd != null & itemsToAdd.Count() > 0)
            {
                foreach (var item in itemsToAdd)
                {
                    items.Add(item);
                }
            }
            return items;
        }


        /// <summary>
        /// Converts an items of dates to an items of date strings
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static String[] ToArray(IEnumerable<DateTime> items)
        {
            return ToArray(items);
        }

    }
}

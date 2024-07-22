using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Agfa.Common.CollectionsExtensions
{
    public partial class CollectionsExtensions
    {
        #region Dictionary
        /// <summary>
        /// Copies two dictionaries. If a duplicate key exists, it will be replaced by the value supplied in the source dictionary
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="Source">The source.</param>
        /// <param name="Destination">The destination.</param>
        /// <remarks></remarks>
        public static void DictionaryClone<TKey, TValue>(Dictionary<TKey, TValue> Source, Dictionary<TKey, TValue> Destination)
        {

            if ((Source != null))
            {
                if ((Destination == null))
                {
                    Destination = new Dictionary<TKey, TValue>();
                }

                foreach (KeyValuePair<TKey, TValue> kvp in Source)
                {
                    if ((Destination.ContainsKey(kvp.Key)))
                    {
                        Destination[kvp.Key] = kvp.Value;
                    }
                    else
                    {
                        Destination.Add(kvp.Key, kvp.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Converts Returns a List of Keys from the Dictionary
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static TKey[] ToArray<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        {
            List<TKey> lstKeys = new List<TKey>();
            foreach (KeyValuePair<TKey, TValue> kvp in dictionary)
            {
                lstKeys.Add(kvp.Key);
            }
            return lstKeys.ToArray();
        }
        /// <summary>
        /// Returns a List of Values from a Dictionary
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="BuildValueArray">if set to <c>true</c> [build value array].</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static TValue[] ToArray<TKey, TValue>(Dictionary<TKey, TValue> dictionary, bool BuildValueArray)
        {
            List<TValue> lstKeys = new List<TValue>();
            foreach (KeyValuePair<TKey, TValue> kvp in dictionary)
            {
                lstKeys.Add(kvp.Value);
            }
            return lstKeys.ToArray();
        }

        /// <summary>
        /// Toes the delimited string.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="Delimitor">The delimitor.</param>
        /// <returns></returns>
        public static string ToDelimitedString<TKey, TValue>(Dictionary<TKey, TValue> dictionary, string Delimitor)
        {
            return ToDelimitedString<TKey>(ToArray<TKey, TValue>(dictionary), Delimitor);
        }

        /// <summary>
        /// Toes the delimited string.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns></returns>
        public static string ToDelimitedString<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        {
            return ToDelimitedString<TKey>(ToArray<TKey, TValue>(dictionary), ",");
        }

        /// <summary>
        /// Toes the delimited string.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="BuildValueArray">if set to <c>true</c> [build value array].</param>
        /// <param name="Delimitor">The delimitor.</param>
        /// <returns></returns>
        public static string ToDelimitedString<TKey, TValue>(Dictionary<TKey, TValue> dictionary, bool BuildValueArray, string Delimitor)
        {
            return ToDelimitedString<TValue>(ToArray<TKey, TValue>(dictionary, BuildValueArray), Delimitor);
        }
        /// <summary>
        /// Toes the delimited string.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="BuildValueArray">if set to <c>true</c> [build value array].</param>
        /// <returns></returns>
        public static string ToDelimitedString<TKey, TValue>(Dictionary<TKey, TValue> dictionary, bool BuildValueArray)
        {
            return ToDelimitedString<TValue>(ToArray<TKey, TValue>(dictionary, BuildValueArray), ",");
        }
        #endregion

        

        /// <summary>
        /// Returns the first item in a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static T FirstItem<T>(IEnumerable<T> items)
        {
            using (IEnumerator<T> iter = items.GetEnumerator())
            {
                iter.MoveNext();
                return iter.Current;
            }
        }
        /// <summary>
        /// Returns an item at a specific index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static T ItemAt<T>(IEnumerable<T> items, int index)
        {
            using (IEnumerator<T> iter = items.GetEnumerator())
            {
                for (int i = 0; i <= index; i++, iter.MoveNext()) ;
                return iter.Current;
            }
        }

        /// <summary>
        /// Reverses a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<T> Reverse<T>(IEnumerable<T> items)
        {
            List<T> lstList = new List<T>(items);
            lstList.Reverse();
            return lstList.ToArray();
        }

        /// <summary>
        /// Returns the last item in a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static T LastItem<T>(IEnumerable<T> items)
        {
            using (IEnumerator<T> iter = CollectionsExtensions.Reverse(items).GetEnumerator())
            {
                iter.MoveNext();
                return iter.Current;
            }
        }

        /// <summary>
        /// Slices a list of items at the a specified item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static IEnumerable<T> Slice<T>(IEnumerable<T> items, T item)
        {
            foreach (T t in items)
            {
                if (t.Equals(item))
                    yield return t;
            }
            yield return default(T);
        }
        /// <summary>
        /// Returns a count of the IEnumerable Collection. Use only
        /// when the .Net implementation is not present
        /// I.e. do not use for List, items, or dataview
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static int Count<T>(IEnumerable<T> items)
        {
            int intCount = 0;
            foreach (T item in items)
            {
                intCount++;
            }
            return intCount;

        }
        

        /// <summary>
        /// Convert an items to a Delimited String
        /// </summary>
        /// <typeparam name="T">Datatype of the items</typeparam>
        /// <param name="items">items to convert to string</param>
        /// <param name="Delimitor">Delimiter to use</param>
        /// <returns>
        /// String delimited by the supplied Delimitor
        /// </returns>
        public static String ToDelimitedString<T>(IEnumerable<T> items, String Delimitor)
        {
            if (items == null || CollectionsExtensions.Count(items) == 0)
            {
                return String.Empty;
            }
            if (String.IsNullOrEmpty(Delimitor))
            {
                Delimitor = ",";
            }
            StringBuilder strb = new StringBuilder();
            foreach (T item in items)
            {
                strb.Append(item).Append(Delimitor);
            }
            if (strb.Length > Delimitor.Length)
            {
                strb = strb.Remove(strb.ToString().LastIndexOf(Delimitor), Delimitor.Length);
            }

            return strb.ToString().Trim();
        }
        /// <summary>
        /// Convert an items to a Delimited String.
        /// Assumes a comma for the Delimitor
        /// </summary>
        /// <typeparam name="T">Datatype of the items</typeparam>
        /// <param name="items">items to convert to string</param>
        /// <returns>A Comma delimited String</returns>
        public static String ToDelimitedString<T>(IEnumerable<T> items)
        {

            return ToDelimitedString<T>(items, ",");
        }
        /// <summary>
        /// Check if a collection is empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns>
        /// 	<c>true</c> if the specified items is empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsEmpty<T>(IEnumerable<T> items)
        {
            if (items == null)
            {
                return true;
            }

            using (IEnumerator<T> enumerator = items.GetEnumerator())
            {
                return !enumerator.MoveNext();
            }
        }

        /// <summary>
        /// Toes the delimited string.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="Format">The format.</param>
        /// <param name="Delimitor">The delimitor.</param>
        /// <returns></returns>
        public static String ToDelimitedString(IEnumerable<DateTime> items, String Format, String Delimitor)
        {

            StringBuilder strbDates = new StringBuilder();
            if (IsEmpty(items))
            {
                return null;
            }
            else
            {
                foreach (DateTime d in items)
                {
                    if (!String.IsNullOrEmpty(Format))
                    {
                        strbDates.Append(d.ToString(Format)).Append(Delimitor);

                    }
                    else
                    {
                        strbDates.Append(d.ToString()).Append(Delimitor);
                    }

                }
            }
            return strbDates.ToString().Trim(Delimitor.ToCharArray());
        }

        /// <summary>
        /// Toes the delimited string.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="Format">The format.</param>
        /// <returns></returns>
        public static String ToDelimitedString(IEnumerable<DateTime> items, String Format)
        {
            return ToDelimitedString(items, Format, ",");
        }
        /// <summary>
        /// Toes the delimited string.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static String ToDelimitedString(IEnumerable<DateTime> items)
        {
            return ToDelimitedString(items, null, ",");
        }

        /// <summary>
        /// Converts an items of dates to an items of date strings in the specified format
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="Format">The format.</param>
        /// <returns></returns>
        public static String[] ToArray(IEnumerable<DateTime> items, String Format)
        {
            List<String> lstDates = new List<String>();
            if (IsEmpty(items))
            {
                return null;
            }
            else
            {
                foreach (DateTime d in items)
                {
                    if (!String.IsNullOrEmpty(Format))
                    {
                        lstDates.Add(d.ToString(Format));


                    }
                    else
                    {
                        lstDates.Add(d.ToString());
                    }

                }
            }
            return lstDates.ToArray();
        }

      


    }
}

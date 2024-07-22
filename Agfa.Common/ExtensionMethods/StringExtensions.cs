using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Agfa.Common.ExtensionMethods
{
    public static partial class Extensions
    {
        /// <summary>
        /// Test if a String is Null or Empty
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns>
        /// 	<c>true</c> if [is null or empty] [the specified STR]; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsNullOrEmpty(this String str)
        {
            return (String.IsNullOrEmpty(str));
        }

        /// <summary>
        /// Trims the end.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="StringToRemove">The String to remove.</param>
        /// <returns></returns>
        public static String TrimEnd(this String str, String StringToRemove)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }
            StringToRemove.ThrowIfArgumentIsNull("StringToRemove");
            if (str.EndsWith(StringToRemove))
            {
                str = str.Remove(str.LastIndexOf(StringToRemove), StringToRemove.Length);
            }
            return str;
        }

        /// <summary>
        /// Trims the start.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="StringToRemove">The String to remove.</param>
        /// <returns></returns>
        public static String TrimStart(this String str, String StringToRemove)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }
            StringToRemove.ThrowIfArgumentIsNull("StringToRemove");
            if (str.StartsWith(StringToRemove))
            {
                str = str.Remove(0, StringToRemove.Length);
            }
            return str;
        }
        /// <summary>
        /// Trims the specified STR.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="StringToRemove">The String to remove.</param>
        /// <returns></returns>
        public static String Trim(this String str, String StringToRemove)
        {
            return str.TrimEnd(StringToRemove).TrimStart(StringToRemove);
        }

        /// <summary>
        /// Appends the value to the end of the String if it does not exist
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static String AppendIfMissing(this String str, String value)
        {
            if (!str.EndsWith(value))
            {
                return str + value;
            }
            return str;
        }

        /// <summary>
        /// Replaces the invalid file name characters.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="ReplaceWith">The replace with.</param>
        /// <returns></returns>
        public static String ReplaceInvalidFileNameCharacters(this String str, String ReplaceWith)
        {
            String strReplacePattern = @"[\\/:?""<>|]";
            str = Regex.Replace(str, strReplacePattern, ReplaceWith);
            return str;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agfa.Common.ExtensionMethods
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Throw an Argument Null Exception if it this is null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="ParameterName">Name of the parameter.</param>
        /// <returns></returns>
        public static T ThrowIfArgumentIsNull<T>(this T obj, String ParameterName) where T : class
        {
            if (obj == null) throw new ArgumentNullException(ParameterName + " cannot be null");

            if (obj is String)
            {
                if (String.IsNullOrEmpty(obj.ToString()))
                {
                    throw new ArgumentNullException(ParameterName + " cannot be null or empty");
                }
            }
            return obj;
        }

        /// <summary>
        /// Returns the Default value if the current Object if it is null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str">The STR.</param>
        /// <param name="Defaultvalue">Default value for the Object if this String is null or empty</param>
        /// <returns></returns>
        public static T Default<T>(this T str, T Defaultvalue) where T : class
        {
            if (str == null)
            {
                return Defaultvalue;
            }
            return str;
        }
    }
}

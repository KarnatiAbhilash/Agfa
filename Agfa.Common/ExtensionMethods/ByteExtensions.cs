using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Agfa.Common.ExtensionMethods
{
    public static partial class ByteExtensions
    {
        /// <summary>
        /// Convert the Byte array to a Base64 String
        /// </summary>
        /// <param name="byt">The byt.</param>
        /// <returns></returns>
        public static String ToBase64String(this Byte[] byt)
        {
            if (byt.Length == 0)
                return null;
            return Convert.ToBase64String(byt);
        }
        /// <summary>
        /// Convert the byte array to a Unicode String
        /// </summary>
        /// <param name="byt">The byt.</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public static String ToString(this Byte[] byt)
        {
            if (byt.Length == 0)
                return null;
            return Encoding.Unicode.GetString(byt);
        }

        /// <summary>
        /// Converts a Byte array to a Stream
        /// </summary>
        /// <param name="byt">The byt.</param>
        /// <returns></returns>
        public static Stream ToStream(this Byte[] byt)
        {
            MemoryStream ms = new MemoryStream(byt);
            ms.Position = 0;
            return ms;
        }


    }
}

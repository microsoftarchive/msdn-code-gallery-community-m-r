using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvents.Client.Organizer.Desktop.Converters
{
    /// <summary>
    /// string extension class
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// string extension method ToUpperFirstLetter
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToUpperFirstLetter(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            char[] letters = source.ToCharArray();
            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }
    }
}

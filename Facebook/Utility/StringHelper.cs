using System.Collections.Generic;
using System.Text;

namespace Facebook.Utility
{
    /// <summary>
    /// Helper functions for string manipulation
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Convert a collection of strings to a comma separated list.
        /// </summary>
        /// <param name="items">The array to convert to a comma separated list.</param>
        /// <returns>comma separated string.</returns>
        public static string ConvertToCommaSeparated<T>(T[] items)
        {
            // assumed that the average string length is 10 and double the buffer multiplying by 2
            // if this does not fit in your case, please change the values
            int preAllocation = items.Length * 10 * 2;
            var sb = new StringBuilder(preAllocation);
            int i = 0;
            bool isString = typeof(T) == typeof(string);

            foreach (T key in items)
            {
                if (isString)
                {
                    sb.Append('\'');
                }

                sb.Append(key.ToString());

                if (isString)
                {
                    sb.Append('\'');
                }
                if (i < items.Length - 1)
                    sb.Append(",");

                i++;
            }
            return sb.ToString();
        }
        /// <summary>
        /// Convert a collection of strings to a comma separated list.
        /// </summary>
        /// <param name="collection">The collection to convert to a comma separated list.</param>
        /// <returns>comma separated string.</returns>
        public static string ConvertToCommaSeparated<T>(IList<T> collection)
        {
            // assumed that the average string length is 10 and double the buffer multiplying by 2
            // if this does not fit in your case, please change the values
            int preAllocation = collection.Count * 10 * 2;
            var sb = new StringBuilder(preAllocation);
            int i = 0;
            bool isString = typeof(T) == typeof(string);

            foreach (T key in collection)
            {
                if (isString)
                {
                    sb.Append('\'');
                }

                sb.Append(key.ToString());

                if (isString)
                {
                    sb.Append('\'');
                }
                if (i < collection.Count - 1)
                    sb.Append(",");

                i++;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Strip Non Valid XML Characters.
        /// </summary>
        public static string StripNonValidXMLCharacters(string s)
        {
            StringBuilder _validXML = new StringBuilder(s.Length, s.Length); // Used to hold the output.
            char[] charArray = s.ToCharArray();

            if (string.IsNullOrEmpty(s)) return string.Empty; // vacancy test.

            for (int i = 0; i < charArray.Length; i++)
            {
                char current = charArray[i];
                if ((current == 0x9) ||
                (current == 0xA) ||
                (current == 0xD) ||
                ((current >= 0x20) && (current <= 0xD7FF)) ||
                ((current >= 0xE000) && (current <= 0xFFFD)))
                    _validXML.Append(current);
            }
            return _validXML.ToString();
        }

    }
}

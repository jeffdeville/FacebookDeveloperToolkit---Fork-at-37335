using System.Collections.Generic;
using System.Text;

namespace Facebook.Utility
{
    /// <summary>
    /// JSON (JavaScript Object Notation) Utility Methods.
    /// </summary>
    public static class JSONHelper
    {
        ///<summary>
        /// Converts a Dictionary to a JSON-formatted Associative Array.
        ///</summary>
        ///<param name="dict">Source Dictionary collection [string|string].</param>
        ///<returns>JSON Associative Array string.</returns>
        public static string ConvertToJSONAssociativeArray(Dictionary<string, string> dict)
        {
            var elements = new List<string>();

            foreach (var pair in dict)
            {
                if(!string.IsNullOrEmpty(pair.Value))
                {
                    elements.Add(string.Format("\"{0}\":{2}{1}{2}", EscapeJSONString(pair.Key), EscapeJSONString(pair.Value), IsJSONArray(pair.Value) || IsBoolean(pair.Value) ? string.Empty : "\""));
                }
            }
            return "{" + string.Join(",", elements.ToArray()) + "}";
        }

        /// <summary>
        /// Determines if input string is a formatted JSON Array.
        /// </summary>
        /// <param name="test">string</param>
        /// <returns>bool</returns>
        public static bool IsJSONArray(string test)
        {
            return test.StartsWith("{") && !test.StartsWith("{*") || test.StartsWith("[");
        }

        /// <summary>
        /// Determines if input string is a boolean value.
        /// </summary>
        /// <param name="test">string</param>
        /// <returns>bool</returns>
        public static bool IsBoolean(string test)
        {
            return test.Equals("false") || test.Equals("true");
        }

        /// <summary>
        /// Converts a List collection of type string to a JSON Array.
        /// </summary>
        /// <param name="list">List of strings</param>
        /// <returns>string</returns>
        public static string ConvertToJSONArray(List<string> list)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            foreach (var item in list)
            {
                builder.Append(string.Format("{0}{1}{0},", IsJSONArray(item) || IsBoolean(item) ? string.Empty : "\"", EscapeJSONString(item)));
            }
            builder.Replace(",", "]", builder.Length - 1, 1);
            return builder.ToString();
        }

        /// <summary>
        /// Converts a List collection of type long to a JSON Array.
        /// </summary>
        /// <param name="list">List of longs</param>
        /// <returns>string</returns>
        public static string ConvertToJSONArray(List<long> list)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            foreach (var item in list)
            {
                builder.Append(string.Format("{0}{1}{0},", IsJSONArray(item.ToString()) || IsBoolean(item.ToString()) ? string.Empty : "\"", EscapeJSONString(item.ToString())));
            }
            builder.Replace(",", "]", builder.Length - 1, 1);
            return builder.ToString();
        }

        /// <summary>
        /// Converts a JSON Array string to a List collection of type string.
        /// </summary>
        /// <param name="array">JSON Array string</param>
        /// <returns>List of strings</returns>
        public static List<string> ConvertFromJSONArray(string array)
        {
            if (!string.IsNullOrEmpty(array))
            {
                array = array.Replace("[", "").Replace("]", "").Replace("\"", "");
                return new List<string>(array.Split(','));
            }
           
            return new List<string>();
        }

        /// <summary>
        /// Converts a JSON Array string to a Dictionary collection of type string, string.
        /// </summary>
        /// <param name="array">JSON Array string</param>
        /// <returns>Dictionary of string, string</returns>
        public static Dictionary<string, string> ConvertFromJSONAssoicativeArray(string array)
        {
            var dict = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(array))
            {
                array = array.Replace("{", "").Replace("}", "").Replace("\":", "|").Replace("\"", "").Replace("\\/", "/");
                var pairs = new List<string>(array.Split(','));
                foreach (var pair in pairs)
                {
                    if (!string.IsNullOrEmpty(pair))
                    {
                        var pairArray = pair.Split('|');
                        dict.Add(pairArray[0], pairArray[1]);
                    }
                }
                return dict;
            }

            return new Dictionary<string, string>();
        }

        /// <summary>
        /// Escape backslashes and double quotes of valid JSON content string.
        /// </summary>
        /// <param name="originalString">string</param>
        /// <returns>string</returns>
        public static string EscapeJSONString(string originalString)
        {
            return IsJSONArray(originalString) ? originalString : originalString.Replace("\\/", "/").Replace("/", "\\/").Replace("\\\"", "\"").Replace("\"", "\\\"").Replace("\r", "\\r").Replace("\n", "\\n");
        }
    }
}

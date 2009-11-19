using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
using System.ComponentModel;
using System.Xml.Serialization;
using Facebook.Rest;
using Facebook.Schema;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace Facebook.Utility
{
    ///<summary>
    ///</summary>
    public static class Utilities
    {
        ///<summary>
        ///</summary>
        public static string SessionNotAvailableError =
            "Session not established. This can be called only after logging to Facebook";

        ///// <summary>
        ///// Adds a list of objects to parameter collection
        ///// </summary>
        ///// <typeparam name="T">type of list</typeparam>
        ///// <param name="dict">parameter collection</param>
        ///// <param name="key">key to add the value</param>
        ///// <param name="values">List of values to add</param>
        //public static void AddList<T>(IDictionary<string, string> dict, string key, IList<T> values)
        //{
        //    if (values != null)
        //    {
        //        dict.Add(key, StringHelper.ConvertToCommaSeparated(values));
        //    }
        //}

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="value"></param>
        ///<exception cref="ArgumentException"></exception>
        public static void AddRequiredParameter(IDictionary<string, string> dict, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                dict.Add(key, value);
            }
            else
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Parameter '{0}' is required",
                                                          key));
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="value"></param>
        ///<exception cref="ArgumentException"></exception>
        public static void AddRequiredParameter(IDictionary<string, string> dict, string key, long value)
        {
            if (value >= 0)
            {
                dict.Add(key, value.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Parameter '{0}' is required",
                                                          key));
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="value"></param>
        ///<exception cref="ArgumentException"></exception>
        public static void AddRequiredParameter(IDictionary<string, string> dict, string key, double value)
        {
            if (value >= 0)
            {
                dict.Add(key, value.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Parameter '{0}' is required",
                                                          key));
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="value"></param>
        ///<exception cref="Exception"></exception>
        public static void AddRequiredParameter(IDictionary<string, string> dict, string key, int value)
        {
            if (value >= 0)
            {
                dict.Add(key, value.ToString());
            }
            else
            {
                throw new Exception(string.Format("Error: Parameter '{0}' is required.", key));
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="value"></param>
        ///<exception cref="Exception"></exception>
        public static void AddRequiredParameter(IDictionary<string, string> dict, string key, float value)
        {
            if (value >= 0.0)
            {
                dict.Add(key, value.ToString());
            }
            else
            {
                throw new Exception(string.Format("Error: Parameter '{0}' is required.", key));
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="assoc_info"></param>
        ///<exception cref="Exception"></exception>
        public static void AddRequiredParameter(IDictionary<string, string> dict, string key,
                                                assoc_object_type assoc_info)
        {
            if (!(assoc_info == null))
            {
                var assoc = new Dictionary<string, string>
                                {
                                    {"alias", assoc_info.alias},
                                    {"object_type", assoc_info.object_type},
                                    {"unique", assoc_info.unique.ToString()}
                                };
                AddJSONAssociativeArray(dict, key, assoc);
            }
            else
            {
                throw new Exception(string.Format("Error: Parameter '{0}' is required.", key));
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="assocs"></param>
        ///<exception cref="Exception"></exception>
        public static void AddRequiredParameter(IDictionary<string, string> dict, string key,
                                                IList<DataAssociation> assocs)
        {
            if (!(assocs == null))
            {
                StringBuilder sb = new StringBuilder(assocs.Count*200);
                foreach (var da in assocs)
                {
                    Dictionary<string, string> tempDict = new Dictionary<string, string>
                                              {
                                                  {"assoc_time", DateHelper.ConvertDateToDouble(da.assoc_time).ToString()},
                                                  {"data", da.data},
                                                  {"id1", da.id1.ToString()},
                                                  {"id2", da.id2.ToString()},
                                                  {"name", da.name}
                                              };
                    sb.Append(JSONHelper.ConvertToJSONAssociativeArray(tempDict));
                }
                AddRequiredParameter(dict, key, sb.ToString());
            }
            else
            {
                throw new Exception(string.Format("Error: Parameter '{0}' is required.", key));
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="data"></param>
        public static void AddOptionalParameter(IDictionary<string, string> dict, string key, string data)
        {
            if (!String.IsNullOrEmpty(data))
            {
                dict.Add(key, data);
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="data"></param>
        public static void AddOptionalParameter(IDictionary<string, string> dict, string key, double? data)
        {
            if (data != null)
            {
                dict.Add(key, data.Value.ToString(CultureInfo.InvariantCulture));
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="value"></param>
        public static void AddOptionalParameter(IDictionary<string, string> dict, string key, long value)
        {
            if (value >= 0)
            {
                dict.Add(key, value.ToString());
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="value"></param>
        public static void AddOptionalParameter(IDictionary<string, string> dict, string key, long? value)
        {
            if (value != null)
            {
                AddOptionalParameter(dict, key, value.Value);
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="value"></param>
        public static void AddOptionalParameter(IDictionary<string, string> dict, string key, int value)
        {
            if (value >= 0)
            {
                dict.Add(key, value.ToString());
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="value"></param>
        public static void AddOptionalParameter(IDictionary<string, string> dict, string key, int? value)
        {
            if (value != null)
            {
                AddOptionalParameter(dict, key, value.Value);
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="value"></param>
        public static void AddOptionalParameter(IDictionary<string, string> dict, string key, double value)
        {
            if (value >= 0)
            {
                dict.Add(key, value.ToString());
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="value"></param>
        public static void AddFBMLParameter(IDictionary<string, string> dict, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                dict.Add(key, string.Format(CultureInfo.CurrentUICulture, value.Replace("{", "{{").Replace("}", "}}")));
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="value"></param>
        public static void AddCultureParameter(IDictionary<string, string> dict, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                dict.Add(key, string.Format(CultureInfo.CurrentUICulture, value));
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="value"></param>
        public static void AddParameter(IDictionary<string, string> dict, string key, bool value)
        {
            dict.Add(key, value.ToString());
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="value"></param>
        public static void AddJSONAssociativeArray(IDictionary<string, string> dict, string key,
                                                   Dictionary<string, string> value)
        {
            if (value != null && value.Count > 0)
            {
                dict.Add(key, JSONHelper.ConvertToJSONAssociativeArray(value));
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="value"></param>
        public static void AddJSONArray(IDictionary<string, string> dict, string key, List<string> value)
        {
            if (value != null && value.Count > 0)
            {
                dict.Add(key, JSONHelper.ConvertToJSONArray(value));
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="value"></param>
        public static void AddJSONArray(IDictionary<string, string> dict, string key, List<long> value)
        {
            if (value != null && value.Count > 0)
            {
                dict.Add(key, JSONHelper.ConvertToJSONArray(value));
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="values"></param>
        public static void AddCollection(IDictionary<string, string> dict, string key, Collection<string> values)
        {
            if (!Equals(values, null) && values.Count > 0)
            {
                dict.Add(key, StringHelper.ConvertToCommaSeparated(values));
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="values"></param>
        public static void AddList(IDictionary<string, string> dict, string key, List<string> values)
        {
            if (!Equals(values, null) && values.Count > 0)
            {
                dict.Add(key, StringHelper.ConvertToCommaSeparated(values));
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="values"></param>
        public static void AddList(IDictionary<string, string> dict, string key, List<long> values)
        {
            if (!Equals(values, null) && values.Count > 0)
            {
                dict.Add(key, StringHelper.ConvertToCommaSeparated(values));
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="dict"></param>
        ///<param name="key"></param>
        ///<param name="values"></param>
        public static void AddList(IDictionary<string, string> dict, string key, List<int> values)
        {
            if (!Equals(values, null) && values.Count > 0)
            {
                dict.Add(key, StringHelper.ConvertToCommaSeparated(values));
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="obj"></param>
        ///<typeparam name="T"></typeparam>
        ///<returns></returns>
        public static string SerializeToJSONObject<T>(T obj)
        {
            var serializer = new DataContractJsonSerializer(typeof (T));
            var ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            return Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int) ms.Length);
        }

        ///<summary>
        ///</summary>
        ///<param name="obj"></param>
        ///<typeparam name="T"></typeparam>
        ///<returns></returns>
        public static string SerializeToXml<T>(T obj)
        {
            var xs = new XmlSerializer(typeof (T));
            var ms = new MemoryStream();
            xs.Serialize(ms, obj);
            return Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int) ms.Length);
        }

        internal static List<string> ParameterDictionaryToList(
            IEnumerable<KeyValuePair<string, string>> parameterDictionary)
        {
            var parameters = new List<string>();

            foreach (var kvp in parameterDictionary)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "{0}", kvp.Key));
            }
            return parameters;
        }

        ///<summary>
        ///</summary>
        ///<param name="response"></param>
        ///<typeparam name="T"></typeparam>
        ///<returns></returns>
        ///<exception cref="FacebookException"></exception>
        public static T DeserializeJSONObject<T>(string response)
        {
            var serializer = new DataContractJsonSerializer(typeof (T));

            using (var mo = new MemoryStream(Encoding.UTF8.GetBytes(response)))
            {
                try
                {
                    var o = serializer.ReadObject(mo);

                    if (o is T)
                        return (T) o;

                    return default(T);
                }
                catch (SerializationException e)
                {
                    throw new FacebookException("Could not deserialize data returned from server", e);
                }
                catch (InvalidDataContractException e)
                {
                    throw new FacebookException("Could not deserialize data returned from server", e);
                }
            }
        }
        private static string SwapRoot<T>(string xmlInput)
        {
            return SwapRoot(xmlInput, typeof(T));
        }
        private static string SwapRoot(string xmlInput, Type t)
        {
            return xmlInput.Replace("fql_query_response", t.Name).Replace("fql_result_set", t.Name);
        }
        ///<summary>
        ///</summary>
        ///<param name="xmlInput"></param>
        ///<typeparam name="T"></typeparam>
        ///<returns></returns>
        ///<exception cref="FacebookException"></exception>
        public static T DeserializeXML<T>(string xmlInput)
        {
            if (string.IsNullOrEmpty(xmlInput)) return default(T);
            var xs = new XmlSerializer(typeof (T));
            
            // Without adding the xsi:nil="true" attribute, XmlSerializer is unable to deserialize an element like <notes_count/> into a System.Nullable<int>.
            // For example, significant_other_id in users.getInfo will cause this problem if the user has no significant other.
            xmlInput = Regex.Replace(xmlInput, @"(?<!xsi:nil=\""true\"")/\>", " xsi:nil=\"true\"/>");

            if (xmlInput.Contains("fql_query_response"))
            {
                xmlInput = SwapRoot<T>(xmlInput);
            }
            var memoryStream = new MemoryStream(StringToUTF8ByteArray(xmlInput));

            try
            {
                return (T) xs.Deserialize(memoryStream);
            }
            catch (InvalidOperationException e)
            {
                throw new FacebookException("Could not deserialize data returned from server", e);
            }
        }
        private static string AddXsiNil(Match match)
        {
            return match.Value.Substring(0, match.Value.Length - 2) + " xsi:nil=\"true\"/>";
        }

        ///<summary>
        /// Used to deserialize xml to a dynamic type.  Similar to generic version but this is used when type is only known at run type (for fql queries)
        ///</summary>
        ///<param name="xmlInput">xml string to deserialize</param>
        ///<param name="t">the type to deserialize into</param>
        public static object DeserializeXML(string xmlInput, Type t)
        {
            if (string.IsNullOrEmpty(xmlInput)) return null;
            var xs = new XmlSerializer(t);
            if (xmlInput.Contains("fql_query_response") || (xmlInput.Contains("fql_result_set") && !xmlInput.Contains("multiquery")))
            {
                xmlInput = SwapRoot(xmlInput, t);
            }
            var memoryStream = new MemoryStream(StringToUTF8ByteArray(xmlInput));

            try
            {
                return xs.Deserialize(memoryStream);
            }
            catch (InvalidOperationException e)
            {
                throw new FacebookException("Could not deserialize data returned from server", e);
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="characters"></param>
        ///<returns></returns>
        public static String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();

#if !SILVERLIGHT
            String constructedString = encoding.GetString(characters);
#else
            String constructedString = encoding.GetString(characters, 0, characters.Length);
#endif

            return (constructedString);
        }

        ///<summary>
        ///</summary>
        ///<param name="pXmlString"></param>
        ///<returns></returns>
        public static Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        /// <summary>
        /// Checks if response has any error and throws an exception if it does
        /// </summary>
        /// <param name="response">xml data to parse</param>
        /// <param name="JSONFormat"></param>
        public static void ParseException(string response, bool JSONFormat)
        {
            if (JSONFormat)
            {
                if (string.Compare("{\"error_code\"", 0, response, 0, 13, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    var fbex = DeserializeJSONObject<FacebookApiException>(response);
                    if (fbex != null)
                    {
                        throw new FacebookException(fbex.error_code, fbex.error_msg, fbex.request_args ?? null);
                    }
                }
            }
            else
            {
                try
                {
                    XDocument doc = XDocument.Load(new StringReader(response));
                    if (doc.Elements().FirstOrDefault().Name.LocalName == "error_response")
                    {
                        var ex = FacebookObject<FacebookApiException>.Deserialize(response);
                        if (ex != null)
                        {
                            throw new FacebookException(ex.error_code, ex.error_msg, ex.request_args ?? null);
                        }
                    }
                }
                catch (FacebookException)
                {
                    // Rethrow deserialized FB Exception, ignore others
                    throw;
                }
                catch(Exception)
                {
                }
            }
        }

        /// <summary>
        /// Converts email address to Facebook Connect-formatted email hash using Facebook-specified hashing rules.
        /// </summary>
        /// <param name="email">The email address to apply hash.</param>
        /// <returns>A Facebook email hash.</returns>
        /// <remarks>
        /// Compute the email_hash property as follows:
        /// Normalize the email address. Trim leading and trailing whitespace, and convert all characters to lowercase.
        /// Compute the CRC32 value for the normalized email address and use the unsigned integer representation of this value. (Note that some implementations return signed integers, in which case you will need to convert that result to an unsigned integer.)
        /// Compute the MD5 value for the normalized email address and use the hex representation of this value (using lowercase for A through F).
        /// Combine these two value with an underscore.
        /// For example, the address mary@example.com converts to 4228600737_c96da02bba97aedfd26136e980ae3761.
        /// </remarks>
        public static string GenerateEmailHash(string email)
        {
            // Normalize the email address. Trim leading and trailing whitespace, and convert all characters to lowercase.
            var normalizedEmail = email.Trim().ToLower();

            // Compute the CRC32 value for the normalized email address and use the unsigned integer representation of this value. (Note that some implementations return signed integers, in which case you will need to convert that result to an unsigned integer.)
            var hashHelper = new HashHelper();
            UInt32 crcResult = hashHelper.GetCrc32(normalizedEmail);
            
            // Compute the MD5 value for the normalized email address and use the hex representation of this value (using lowercase for A through F).
            string md5Result = MD5Core.GetHashString(normalizedEmail);

            // Combine these two value with an underscore.
            string emailHash = string.Format("{0}_{1}", crcResult, md5Result.ToLower());

            return emailHash;
        }

        /// <summary>
        /// Used to generically support property changed notification by BindingHelper classes
        /// </summary>
        public static void NotifyPropertyChanged<T, R>(this T sender, PropertyChangedEventHandler handler, Expression<Func<T, R>> expr) where T : INotifyPropertyChanged
        {
#if !SILVERLIGHT
            if (System.Windows.Application.Current != null)
            {
                if (System.Threading.Thread.CurrentThread == System.Windows.Application.Current.Dispatcher.Thread)
                {
                    if (handler != null)
                    {
                        handler(sender, new PropertyChangedEventArgs(sender.GetPropertySymbol(expr)));
                    }
                }
                else
                {
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            if (handler != null)
                            {
                                handler(sender, new PropertyChangedEventArgs(sender.GetPropertySymbol(expr)));
                            }
                        }));
                }
            }

#else
            if (Deployment.Current.Dispatcher.CheckAccess())
            {
                if (handler != null)
                {
                    handler(sender, new PropertyChangedEventArgs(sender.GetPropertySymbol(expr)));
                }
            }
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    if (handler != null)
                    {
                        handler(sender, new PropertyChangedEventArgs(sender.GetPropertySymbol(expr)));
                    }
                });
            }
#endif
        }
        ///<summary>
        /// Gets the property name of a property
        ///</summary>
        public static string GetPropertySymbol<T, R>(this T obj, Expression<Func<T, R>> expr)
        {
            return ((MemberExpression)expr.Body).Member.Name;
        }
        ///<summary>
        ///</summary>
        ///<param name="sender"></param>
        ///<param name="handler"></param>
        ///<param name="propertyName"></param>
        public static void NotifyPropertyChanged(Object sender, PropertyChangedEventHandler handler, string propertyName)
        {
            if (handler != null)
            {
#if !SILVERLIGHT
                if (System.Threading.Thread.CurrentThread == System.Windows.Application.Current.Dispatcher.Thread)
                {
                    handler(sender, new PropertyChangedEventArgs(propertyName));
                }
                else
                {
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(
                        new Action(() => handler(sender, new PropertyChangedEventArgs(propertyName))));
                }

#else
                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(
                    () => handler(sender, new PropertyChangedEventArgs(propertyName)));
#endif
            }
        }
    }
}
using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;
using System.Runtime.Serialization;
using System.Text;

namespace Facebook.Rest
{
	/// <summary>
	/// Facebook Fql API methods.
	/// </summary>
	public class Fql : RestBase, Facebook.Rest.IFql
	{
		#region Private Members
		
		private bool _useJson;

		#endregion Private Members

        #region Public Properties

        /// <summary>
        /// Determines what format the query results will be returned in: JSON if true; XML otherwise.
        /// </summary>
        public bool UseJson
        {
            get { return _useJson; }
            set { _useJson = value; }
        }

        #endregion Public Properties

        #region Methods

        #region Constructor

        /// <summary>
		/// Public constructor for facebook.Fql
		/// </summary>
		/// <param name="session">Needs a connected Facebook Session object for making requests</param>
		public Fql(IFacebookSession session)
			: base(session)
		{
		}

		#endregion Constructor

		#region Public Methods

#if !SILVERLIGHT

		#region Synchronous Methods

        /// <summary>
        /// Evaluates an FQL (Facebook Query Language) query.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var query = string.Format("SELECT uid, name FROM user WHERE uid IN ({0})", Constants.UserId);
        /// var result = api.Fql.Query(query);
        /// </code>
        /// </example>
        /// <param name="query">The query to perform, as described in the FQL documentation.</param>
        /// <returns>This method returns data that very closely resembles the returns of other API calls like users.getInfo. This is not a coincidence - in fact, many of the other API functions are simply wrappers for FQL queries. Note that it preserves the order of the fields in your SELECT clause and that it can contain multiple elements with the same name depending on how you structure the query.</returns>
        public string Query(string query)
		{
			return Query(query, false, null, null);
		}

        /// <summary>
        /// Evaluates an FQL (Facebook Query Language) query.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var query = string.Format("SELECT uid, name FROM user WHERE uid IN ({0})", Constants.UserId);
        /// user result = api.Fql.Query&lt;user&gt;(query);
        /// </code>
        /// </example>
        /// <param name="query">The query to perform, as described in the FQL documentation.</param>
        /// <returns>This method returns data that very closely resembles the returns of other API calls like users.getInfo. This is not a coincidence - in fact, many of the other API functions are simply wrappers for FQL queries. Note that it preserves the order of the fields in your SELECT clause and that it can contain multiple elements with the same name depending on how you structure the query.</returns>
		public T Query<T>(string query)
		{
		    return Query<T>(query, false, null, null);
		}

        /// <summary>
        /// Evaluates a series of FQL (Facebook Query Language) queries in one call and returns the data at one time.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var query1 = string.Format("SELECT uid, name FROM user WHERE uid IN ({0})", Constants.UserId);
        /// var query2 = string.Format("SELECT uid, name FROM user WHERE uid IN ({0})", Constants.UserId);
        /// var queries = new Dictionary&lt;string, string&gt;();
        /// queries.Add("firstQuery", query1);
        /// queries.Add("secondQuery", query2);
        /// var results = api.Fql.Multiquery(queries);
        /// </code>
        /// </example>
        /// <param name="queries">A collection of the queries to perform. The array contains a set of key/value pairs. Each key is a query name, which can contain only alphanumeric characters and optional underscores. Each key maps to a value containing a traditional FQL query.</param>
        /// <returns>This call returns a List of query results. The keys returned are the names of the queries made.  As with fql.query, the data returned from each query very closely resembles the returns of other API calls like users.getInfo, as many API functions are simply wrappers for FQL queries.</returns>
        public IList<fql_result> Multiquery(Dictionary<string, string> queries)
		{
			return Multiquery(queries, false, null, null);
        }
        /// <summary>
        /// Evaluates a series of FQL (Facebook Query Language) queries in one call and returns the data at one time.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var query1 = string.Format("SELECT uid, name FROM user WHERE uid IN ({0})", Constants.UserId);
        /// var query2 = string.Format("SELECT uid, name FROM user WHERE uid IN ({0})", Constants.UserId);
        /// var queries = new Dictionary&lt;string, string&gt;();
        /// queries.Add("firstQuery", query1);
        /// queries.Add("secondQuery", query2);
        /// var results = api.Fql.Multiquery(queries);
        /// </code>
        /// </example>
        /// <param name="queries">A collection of the queries to perform. The array contains a set of key/value pairs. Each key is a query name, which can contain only alphanumeric characters and optional underscores. Each key maps to a value containing a traditional FQL query.</param>
        /// <returns>This call returns a List of query results. The keys returned are the names of the queries made.  As with fql.query, the data returned from each query very closely resembles the returns of other API calls like users.getInfo, as many API functions are simply wrappers for FQL queries.</returns>
        public IList<fql_result> Multiquery(FqlMultiQueryInfo[] queries)
        {
            var qdict = new Dictionary<string, string>();

            foreach (FqlMultiQueryInfo query in queries)
            {
                qdict.Add(query.Key, query.Query);
            }

            return Multiquery(qdict, false, null, null);
        }

        #endregion Synchronous Methods

#endif

        #region Asynchronous Methods

        /// <summary>
        /// Evaluates an FQL (Facebook Query Language) query.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     var query = string.Format("SELECT uid, name FROM user WHERE uid IN ({0})", Constants.UserId);
        ///     api.Fql.QueryAsync(query, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(string result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="query">The query to perform, as described in the FQL documentation.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns data that very closely resembles the returns of other API calls like users.getInfo. This is not a coincidence - in fact, many of the other API functions are simply wrappers for FQL queries. Note that it preserves the order of the fields in your SELECT clause and that it can contain multiple elements with the same name depending on how you structure the query.</returns>
        public void QueryAsync(string query, QueryCallback callback, Object state)
		{
			Query(query, true, callback, state);
		}
        /// <summary>
        /// Evaluates an FQL (Facebook Query Language) query.  
        /// </summary>
        /// <typeparam name="T">The generic type should be an object that wraps the returned type.  Some like users_getInfo_response</typeparam>
        /// <param name="query">The query to perform, as described in the FQL documentation.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns data that very closely resembles the returns of other API calls like users.getInfo. This is not a coincidence - in fact, many of the other API functions are simply wrappers for FQL queries. Note that it preserves the order of the fields in your SELECT clause and that it can contain multiple elements with the same name depending on how you structure the query.</returns>
        public void QueryAsync<T>(string query, QueryCallback<T> callback, Object state)
        {
            Query<T>(query, true, callback, state);
        }


        /// <summary>
        /// Evaluates a series of FQL (Facebook Query Language) queries in one call and returns the data at one time.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     var query1 = string.Format("SELECT uid, name FROM user WHERE uid IN ({0})", Constants.UserId);
        ///     var query2 = string.Format("SELECT uid, name FROM user WHERE uid IN ({0})", Constants.UserId);
        ///     var queries = new Dictionary&lt;string, string&gt;();
        ///     queries.Add("firstQuery", query1);
        ///     queries.Add("secondQuery", query2);
        ///     api.Fql.MultiqueryAsync(queries, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;fql_result&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="queries">A collection of the queries to perform. The array contains a set of key/value pairs. Each key is a query name, which can contain only alphanumeric characters and optional underscores. Each key maps to a value containing a traditional FQL query.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This call returns a List of query results. The keys returned are the names of the queries made.  As with fql.query, the data returned from each query very closely resembles the returns of other API calls like users.getInfo, as many API functions are simply wrappers for FQL queries.</returns>
		public void MultiqueryAsync(Dictionary<string, string> queries, MultiqueryCallback callback, Object state)
		{
			Multiquery(queries, true, callback, state);
        }
        /// <summary>
        /// Evaluates a series of FQL (Facebook Query Language) queries in one call and returns the data at one time.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     var query1 = string.Format("SELECT uid, name FROM user WHERE uid IN ({0})", Constants.UserId);
        ///     var query2 = string.Format("SELECT uid, name FROM user WHERE uid IN ({0})", Constants.UserId);
        ///     var queries = new Dictionary&lt;string, string&gt;();
        ///     queries.Add("firstQuery", query1);
        ///     queries.Add("secondQuery", query2);
        ///     api.Fql.MultiqueryAsync(queries, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;fql_result&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="queries">A collection of the queries to perform. The array contains a set of key/value pairs. Each key is a query name, which can contain only alphanumeric characters and optional underscores. Each key maps to a value containing a traditional FQL query.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This call returns a List of query results. The keys returned are the names of the queries made.  As with fql.query, the data returned from each query very closely resembles the returns of other API calls like users.getInfo, as many API functions are simply wrappers for FQL queries.</returns>
        public void MultiqueryAsync(FqlMultiQueryInfo[] queries, MultiqueryParsedCallback callback, Object state)
        {
            MultiqueryParsed(queries, true, callback, state);
        }

        #endregion Asynchronous Methods

        #endregion Public Methods
        
		#region Private Methods

		private string Query(string fqlQuery, bool isAsync, QueryCallback callback, Object state)
		{
			var parameterList = CreateParameterList(fqlQuery, true);

			if (isAsync)
			{
				SendRequestAsync(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new AsyncResult(XmlResultOperationCompleted, new FacebookCallCompleted<string>(callback), state));
				return null;
			}

			return SendRequest(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
		}

		// TODO: Fix this method. Currently, because of various XML issues, it crashes when trying to deserialize the response.
		//       See work 13747 on Codeplex for more information and suggestions on this issue.
		private T Query<T>(string fqlQuery, bool isAsync, QueryCallback<T> callback, Object state)
		{
		    var parameterList = CreateParameterList(fqlQuery, false);

		    if (isAsync)
		    {
		        SendRequestAsync(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<T>(callback), state);
		        return default(T);
		    }

		    return SendRequest<T>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
		}

		private IList<fql_result> Multiquery(Dictionary<string, string> queries, bool isAsync, MultiqueryCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.fql.multiquery" } };
			Utilities.AddJSONAssociativeArray(parameterList, "queries", queries);

			if (isAsync)
			{
				SendRequestAsync<fql_multiquery_response, IList<fql_result>>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<IList<fql_result>>(callback), state, "fql_result");
				return null;
			}

            var response = SendRequest<fql_multiquery_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? null : response.fql_result;
		}
        private IList<object> MultiqueryParsed(FqlMultiQueryInfo[] queries, bool isAsync, MultiqueryParsedCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.fql.multiquery" } };
            var qdict = new Dictionary<string, string>();
            MultiQueryInfoState qstate = new MultiQueryInfoState()
            {
                Queries = queries,
                Callback = callback,
            };

            foreach (FqlMultiQueryInfo query in queries)
            {
                qdict.Add(query.Key, query.Query);
            }
            Utilities.AddJSONAssociativeArray(parameterList, "queries", qdict);

            if (isAsync)
            {
                AsyncResult result = new AsyncResult(OnMultiQueryCompleted, qstate, state);
                SendRequestAsync(parameterList, !string.IsNullOrEmpty(Session.SessionKey), result);
                return null;
            }

            var response = SendRequest<IList<object>>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
            return response == null ? null : response;
        }
        void OnMultiQueryCompleted(IAsyncResult ar)
        {
            AsyncResult asyncResult = (AsyncResult)ar;
            MultiQueryInfoState qstate = (MultiQueryInfoState)asyncResult.AsyncState;
            Object[] results = new Object[qstate.Queries.Length];
            FacebookException[] exceptions = new FacebookException[qstate.Queries.Length];
            FacebookException ex = asyncResult.Exception;


            if (ex == null)
            {
                try
                {
                    IList<fql_result> parsedResults = Utilities.DeserializeXML<fql_multiquery_response>(asyncResult.Result).fql_result;

                    for(int i = 0; i < qstate.Queries.Length; i++)
                    {
                        string result = null;
                        foreach (var parsedResult in parsedResults)
                        {
                            if (qstate.Queries[i].Key == parsedResult.name)
                            {
                                result = parsedResult.fql_result_set.ToString();
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(result))
                        {
                            exceptions[i] = new FacebookException("Could not deserialize data returned from server");
                        }
                        else
                        {
                            try
                            {
                                results[i] = Utilities.DeserializeXML(result, qstate.Queries[i].Type);
                            }
                            catch (System.Reflection.TargetInvocationException e)
                            {
                                exceptions[i] = new FacebookException("Could not deserialize data", e.InnerException);
                            }
                            catch (FacebookException e)
                            {
                                exceptions[i] = e;
                            }
                        }
                    }
                }
                catch (InvalidDataContractException e)
                {
                    ex = new FacebookException("Could not deserialize data returned from server", e);
                }
            }

            if (ex != null)
            {
                for (int i = 0; i < exceptions.Length; i++)
                {
                    exceptions[i] = asyncResult.Exception;
                }
            }

            if (qstate.Callback != null)
            {
                qstate.Callback(results, asyncResult.AsyncExternalState, exceptions);
            }
        }

		private Dictionary<string, string> CreateParameterList(string fqlQuery, bool jsonAvailable)
		{
			var parameterList = new Dictionary<string, string>(3) { { "method", "facebook.fql.query" } };
			Utilities.AddRequiredParameter(parameterList, "query", fqlQuery);

			if (_useJson && jsonAvailable)
			{
				Utilities.AddRequiredParameter(parameterList, "format", "json");
			}

			return parameterList;
		}

		#endregion Private Methods

		#endregion Methods

		#region Delegates
        
        /// <summary>
        /// Delegate called when Query call completed
        /// </summary>
        /// <param name="result">result of operation</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void QueryCallback(string result, Object state, FacebookException e);

		// TODO: fix the code for the related Query<T>() method.
		/// <summary>
		/// Delegate called when Query call completed
		/// </summary>
		/// <param name="result">result of operation</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>
		/// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void QueryCallback<T>(T result, Object state, FacebookException e);

		/// <summary>
		/// Delegate called when Multiquery call completed
		/// </summary>
		/// <param name="queryResults">A list of FQL query results.</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>
		/// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void MultiqueryCallback(IList<fql_result> queryResults, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when Multiquery call completed
        /// </summary>
        /// <param name="queryResults">A list of FQL query results.</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="exceptions">Array of Exception objects, if any call resulted in exception.</param>
        public delegate void MultiqueryParsedCallback(IList<object> queryResults, Object state, FacebookException[] exceptions);

		#endregion Delegates
	}
    /// <summary>
    /// Represent a query used in Fql multi query
    /// </summary>
    public class FqlMultiQueryInfo
    {
        /// <summary>
        /// Name of key for query
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Query string
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Type of result object
        /// </summary>
        public Type Type { get; set; }
    }

    /// <summary>
    /// Class used to hold multi query info
    /// </summary>
    class MultiQueryInfoState
    {
        public FqlMultiQueryInfo[] Queries { get; set; }
        public Facebook.Rest.Fql.MultiqueryParsedCallback Callback { get; set; }

    }

    /// <summary>
    /// Enumeration for Fql o/p format
    /// </summary>
    public enum QueryOutputFormt
    {
        /// <summary>
        /// output the fql results in xml format
        /// </summary>
        Xml,
        /// <summary>
        /// output the fql results in json format
        /// </summary>
        JSON,
    }

    class MultiQueryResult
    {
        Dictionary<string, string> results = new Dictionary<string, string>();

        public void Deserialize(string query)
        {
            char[] charArray = query.ToCharArray();

            if (charArray[0] != '[')
            {
                return;
            }

            for (int index = 0; index < charArray.Length; index++)
            {
                switch (charArray[index])
                {
                    case '{':
                        ReadObject(charArray, ref index);
                        break;
                    case ',':
                    case '[':
                        continue;
                    case ']':
                        break;
                    default:
                        throw new InvalidDataContractException();
                }
            }
        }

        public string GetResult(string name)
        {
            string value;

            if (results.TryGetValue(name, out value))
            {
                return value;
            }

            return null;
        }

        void ReadObject(char[] charArray, ref int index)
        {
            string key = string.Empty;
            string value = string.Empty;

            if (charArray[index++] != '{')
            {
                throw new InvalidDataContractException();
            }

            while (index < charArray.Length)
            {
                switch (charArray[index])
                {
                    case '"':
                        string s = ReadString(charArray, ref index);

                        if (charArray[index++] != ':')
                        {
                            break;
                        }
                        if (s == "name")
                        {
                            key = ReadString(charArray, ref index);
                        }
                        else if (s == "fql_result_set")
                        {
                            results.Add(key, ReadObjectDataAsString(charArray, ref index));
                        }
                        else
                        {
                            throw new InvalidDataContractException();
                        }

                        break;
                    case ',':
                        index++;
                        continue;
                    case '}':
                        return;
                    default:
                        break;
                }
            }
        }

        protected static string ReadString(char[] charArray, ref int index)
        {
            char c;
            StringBuilder sb = new StringBuilder();
            bool success = false;

            index++; //skip "

            while (index < charArray.Length)
            {
                c = charArray[index++];
                if (c == '"')
                {
                    success = true;
                    break;
                }
                else if (c == '\\')
                {
                    c = charArray[index++];
                    if (c == '"')
                    {
                        sb.Append('"');
                    }
                    else if (c == '\\')
                    {
                        sb.Append('\\');
                    }
                    else if (c == '/')
                    {
                        sb.Append('/');
                    }
                    else if (c == 'b')
                    {
                        sb.Append('\b');
                    }
                    else if (c == 'f')
                    {
                        sb.Append('\f');
                    }
                    else if (c == 'n')
                    {
                        sb.Append('\n');
                    }
                    else if (c == 'r')
                    {
                        sb.Append('\r');
                    }
                    else if (c == 't')
                    {
                        sb.Append('\t');
                    }
                    else if (c == 'u')
                    {
                        if (charArray.Length - index < 4)
                            break;

                        byte[] b = new byte[4];
                        Array.Copy(charArray, index, b, 0, 4);
                        sb.Append(Encoding.Convert(Encoding.Unicode, Encoding.UTF8, b, index, 4));
                        index += 4;
                    }

                }
                else
                {
                    sb.Append(c);
                }

            }

            if (!success)
            {
                throw new InvalidDataContractException();
            }

            return sb.ToString();
        }


        protected static string ReadObjectDataAsString(char[] charArray, ref int index)
        {
            StringBuilder sb = new StringBuilder();
            int count = 1;

            while (index < charArray.Length)
            {
                switch (charArray[index])
                {
                    case '{':
                        count++;
                        sb.Append(charArray[index++]);
                        break;
                    case '}':
                        count--;
                        if (count == 0)
                        {
                            return sb.ToString();
                        }
                        else
                        {
                            sb.Append(charArray[index++]);
                        }
                        break;
                    case '"':
                        CopyString(charArray, sb, ref index);
                        break;
                    default:
                        sb.Append(charArray[index++]);
                        break;
                }

            }

            throw new InvalidDataContractException();
        }

        protected static void CopyString(char[] charArray, StringBuilder sb, ref int index)
        {
            sb.Append(charArray[index++]);

            while (index < charArray.Length)
            {
                char c = charArray[index++];
                sb.Append(c);
                if (c == '"')
                {
                    break;
                }
                else if (c == '\\' && index < charArray.Length)
                {
                    c = charArray[index++];
                    sb.Append(c);

                    if (c == 'u')
                    {
                        if (index + 4 < charArray.Length)
                        {
                            sb.Append(charArray, index, 4);
                            index += 4;
                        }
                        else
                        {
                            break;
                        }
                    }

                }

            }

        }
    }
}
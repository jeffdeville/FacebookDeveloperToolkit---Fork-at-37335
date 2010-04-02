using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Facebook.Schema;
using Facebook.Utility;

namespace Facebook.Rest
{
	public class FacebookNetworkWrapper : IFacebookNetworkWrapper, IBatch
	{
		public FacebookNetworkWrapper(){}

		/// <summary>
		/// Sends a request through the REST API to Facebook. This is what the other methods all use to communicate with Facebook.
		/// You should only use this method if you need to call a method that is not defined in this toolkit. This method
		/// will always use the session key in the request.
		/// </summary>
		/// <param name="parameterDictionary">
		/// A dictionary of parameters for the method being called, as specified in the documentation for the method. You will need
		/// to make sure to have a parameter named "method" corresponding to the method name. The toolkit will populate
		/// the api_key, call_id, sig, v, and session_key parameters.
		/// </param>
		/// <returns>The XML response returned by Facebook.</returns>
		public virtual string SendRequest(IDictionary<string, string> parameterDictionary, bool compressHttp)
		{
			return SendRequestSynchronous(parameterDictionary, compressHttp);
		}

		/// <summary>
		/// Makes a synchronous call to facebook server and returns result.
		/// </summary>
		/// <typeparam name="T">Output Type</typeparam>
		/// <param name="parameterDictionary">parameters that needs to be passed to call</param>
		/// <returns>This method returns an object (of type T) resulting from the request.</returns>
		public virtual T SendRequest<T>(IDictionary<string, string> parameterDictionary, bool compressHttp)
		{
			var result = SendRequestSynchronous(parameterDictionary, compressHttp);
			return Utilities.DeserializeXML<T>(result);
		}

		public string SendRequestSynchronous(IDictionary<string, string> parameterDictionary, bool compressHttp)
		{
			string requestUrl = GetRequestUrl(MethodRequiresSsl(parameterDictionary));
			string parameters = parameterDictionary.ToEncodedString();
			if (IsBatchActive)
			{
				CallList.Add(parameters);
				return null;
			}
			string result = null;
#if !SILVERLIGHT
			if (compressHttp)
				result = processCompressedResponse(postRequest(requestUrl, parameters, true));
			else
				result = processResponse(postRequest(requestUrl, parameters, false));
#else
                result = processResponse(postRequest(requestUrl, parameters, false));
#endif
			return string.IsNullOrEmpty(result) ? null : result;
		}

		public void SendRequestAsync(IDictionary<string, string> parameterList, AsyncResult ar, bool compressHttp)
		{
			throw new NotImplementedException();
		}

		public void SendRequestAsync<T>(Dictionary<string, string> parameterList, FacebookCallCompleted<T> callback, object state, bool compressHttp)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Makes a call to facebook server and returns the result in callback specified
		/// </summary>
		/// <typeparam name="T">Type of object to which result should be deserialized</typeparam>
		/// <param name="parameterList">parameters that needs to be passed to call</param>
		/// <param name="callback">Callback which will be called on response.</param>
		/// <param name="state">User defined object</param>
		public void SendRequestAsync<T>(Dictionary<string, string> parameterList, FacebookCallCompleted<T> callback, Object state)
		{
			SendRequestAsync(parameterList, callback, state);
		}

		public void SendRequestAsync<TObject, TResult>(Dictionary<string, string> parameterList, FacebookCallCompleted<TResult> callback, object state, bool compressHttp)
		{
			throw new NotImplementedException();
		}

		public void SendRequestAsync<TObject, TResult>(Dictionary<string, string> parameterList, FacebookCallCompleted<TResult> callback, object state, string propertyName, bool compressHttp)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Makes a call to facebook server and returns the result in callback specified
		/// </summary>
		/// <param name="parameterList">parameters that needs to be passed to call</param>
		/// <param name="callback">Callback which will be called on response.</param>
		/// <param name="state">User defined object</param>
		public void SendRequestAsync<TObject, TResult>(Dictionary<string, string> parameterList, FacebookCallCompleted<TResult> callback, Object state)
		{
			SendRequestAsync<TObject, TResult>(parameterList, callback, state);
		}

		/// <summary>
		/// Makes a call to facebook server and returns the result in callback specified
		/// </summary>
		/// <param name="parameterList">parameters that needs to be passed to call.</param>
		/// <param name="callback">Callback which will be called on response.</param>
		/// <param name="state">User defined object.</param>
		/// <param name="propertyName">The name of the property to retreive.</param>
		public void SendRequestAsync<TObject, TResult>(Dictionary<string, string> parameterList, FacebookCallCompleted<TResult> callback, Object state, string propertyName)
		{
			SendRequestAsync<TObject, TResult>(parameterList, callback, state, propertyName);
		}


		/// <summary>
		/// Makes a REST call to facebook server. 
		/// </summary>
		/// <param name="parameterList">Parameters for REST call</param>
		/// <param name="useSession">indicator if session should be used or not</param>
		/// <param name="ar"></param>
		/// <remarks>Function made virtual for unittesting</remarks>
		public virtual void SendRequestAsync(IDictionary<string, string> parameterList, AsyncResult ar)
		{
			string postData = parameterList.ToEncodedString();

			if (IsBatchActive)
			{
			    CallListAsync.Add(new BatchRecord(ar, postData));
			}
			else
			{
				WebClientHelper client = new WebClientHelper(ar);
				UriBuilder uriBd = new UriBuilder(Constants.FacebookRESTUrl);

				client.Method = "POST";
				client.RequestCompleted += OnRequestCompleted;
				client.SendRequest(uriBd.Uri, postData);
			}
		}

		internal static WebResponse postRequest(string requestUrl, string postString, bool compressHttp)
		{
#if !SILVERLIGHT
			var webRequest = WebRequest.Create(requestUrl);
			webRequest.Method = "POST";
			webRequest.ContentType = "application/x-www-form-urlencoded";
			if (compressHttp)
			{
				webRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
			}
			if (!String.IsNullOrEmpty(postString))
			{
				var parameterString = Encoding.UTF8.GetBytes(postString);
				webRequest.ContentLength = parameterString.Length;

				using (var buffer = webRequest.GetRequestStream())
				{
					buffer.Write(parameterString, 0, parameterString.Length);
					buffer.Close();
				}
			}

			return webRequest.GetResponse();
#else
            // SL version of postRequest
            throw new Exception("Silverlight applications cannot issue Synchronous calls.");
#endif
		}

		internal static string processResponse(WebResponse webResponse)
		{
			string xmlResponse;

			using (var streamReader = new StreamReader(webResponse.GetResponseStream()))
			{
				xmlResponse = streamReader.ReadToEnd();
			}

			Utilities.ParseException(xmlResponse, false);

			return xmlResponse;
		}
#if !SILVERLIGHT
		// Gaetano 'FiR3N3T' Padalino Optimization - gaetano.padalino@gmail.com
		internal static byte[] DecompressGzip(System.IO.Stream streamInput)
		{
			using (System.IO.Stream streamOutput = new System.IO.MemoryStream())
			{
				int iOutputLength = 0;

				byte[] readBuffer = new byte[4096];

				using (System.IO.Compression.GZipStream streamGZip = new System.IO.Compression.GZipStream(streamInput, System.IO.Compression.CompressionMode.Decompress))
				{
					int i;
					while ((i = streamGZip.Read(readBuffer, 0, readBuffer.Length)) != 0)
					{
						streamOutput.Write(readBuffer, 0, i);
						iOutputLength = iOutputLength + i;
					}
				}

				byte[] buffer = new byte[iOutputLength];
				streamOutput.Position = 0;
				streamOutput.Read(buffer, 0, buffer.Length);
				return buffer;
			}
		}

		// Gaetano 'FiR3N3T' Padalino Optimization - gaetano.padalino@gmail.com
		internal static byte[] DecompressDeflate(System.IO.Stream streamInput)
		{
			using (System.IO.Stream streamOutput = new System.IO.MemoryStream())
			{
				int iOutputLength = 0;

				byte[] readBuffer = new byte[4096];

				using (System.IO.Compression.DeflateStream streamDeflate = new System.IO.Compression.DeflateStream(streamInput, System.IO.Compression.CompressionMode.Decompress))
				{
					int i;
					while ((i = streamDeflate.Read(readBuffer, 0, readBuffer.Length)) != 0)
					{
						streamOutput.Write(readBuffer, 0, i);
						iOutputLength = iOutputLength + i;
					}
				}

				byte[] buffer = new byte[iOutputLength];
				streamOutput.Position = 0;
				streamOutput.Read(buffer, 0, buffer.Length);
				return buffer;
			}
		}

		// Gaetano 'FiR3N3T' Padalino Optimization - gaetano.padalino@gmail.com
		internal static string processCompressedResponse(WebResponse webResponse)
		{
			string xmlResponse = string.Empty;
			string sResponseHeader = webResponse.Headers["Content-Encoding"];

			if (!string.IsNullOrEmpty(sResponseHeader))
			{
				using (StreamReader webReader = new System.IO.StreamReader(webResponse.GetResponseStream()))
				{
					if (sResponseHeader.ToLower().Contains("gzip"))
					{
						byte[] b = DecompressGzip(webReader.BaseStream);
						xmlResponse = Encoding.ASCII.GetString(b);
					}
					else if (sResponseHeader.ToLower().Contains("deflate"))
					{
						byte[] b = DecompressDeflate(webReader.BaseStream);
						xmlResponse = Encoding.ASCII.GetString(b);
					}
				}
			}
			else
			{
				using (var streamReader = new StreamReader(webResponse.GetResponseStream()))
				{
					xmlResponse = streamReader.ReadToEnd();
				}

			}

			Utilities.ParseException(xmlResponse, false);

			return xmlResponse;
		}
#endif


		internal static string GetRequestUrl(bool useSSL)
		{
			return useSSL ? Constants.FacebookRESTUrl.Replace("http", "https") : Constants.FacebookRESTUrl;
		}

		private bool MethodRequiresSsl(IDictionary<string, string> parameterDictionary)
		{
			return parameterDictionary["method"] == "facebook.auth.getSession";
		}

		#region Private Methods

		/// <summary>
		/// Server returns empty arrays in format not understood by our deserializer. This function fixs up
		/// such known instances.
		/// </summary>
		/// <param name="result"></param>
		/// <param name="type"></param>
		/// <returns>This method returns a cleaned JSON string.</returns>
		private static string FixupJSONString(string result, Type type)
		{
			if (type == typeof(FacebookStreamData)
				|| type == typeof(FacebookStreamPostCollection))
			{
				result = result.Replace("\"media\":{}", "\"media\":[]");
				result = result.Replace("\"friends\":{}", "\"friends\":[]");
				result = result.Replace("\"albums\":{}", "\"albums\":[]");
				result = result.Replace("\"posts\":{}", "\"posts\":[]");
				result = result.Replace("\"profiles\":{}", "\"profiles\":[]");
				result = result.Replace("\"comment_list\":{}", "\"comment_list\":[]");
			}
			else if (type == typeof(event_members))
			{
				result = result.Replace("\"attending\":{}", "\"attending\":[]");
				result = result.Replace("\"unsure\":{}", "\"unsure\":[]");
				result = result.Replace("\"declined\":{}", "\"declined\":[]");
				result = result.Replace("\"not_replied\":{}", "\"not_replied\":[]");
			}

			return result;
		}

		/// <summary>
		/// Callback when http request is completed 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		internal static void OnRequestCompleted(object sender, RequestCompletedEventArgs e)
		{
			FacebookException exception = (FacebookException)e.Error;
			AsyncResult ar = (AsyncResult)e.UserState;
			string response = string.Empty;

			if (exception == null)
			{
				try
				{
					using (StreamReader streamReader = new StreamReader(e.Response))
					{
						response = streamReader.ReadToEnd();
					}

					Utilities.ParseException(response, ar.JSONFormat);
				}
				catch (FacebookException ex)
				{
					exception = ex;
				}
			}

			ar.SetComplete(response, exception);
		}
		#endregion Private Methods

		#region Batch
		#region Private Members
		
		private readonly List<string> _callList = new List<string>();
        private readonly List<BatchRecord> _callListAsync = new List<BatchRecord>();
		private bool _isBatchActive;
        
        #endregion Private Members

		#region Public Properties

		///<summary>
		///</summary>
		public bool IsBatchActive
		{
			get { return _isBatchActive; }
			set { _isBatchActive = value; }
		}

		///<summary>
		///</summary>
		public List<string> CallList
		{
			get { return _callList; }
		}

		///<summary>
		///</summary>
		public List<BatchRecord> CallListAsync
		{
			get { return _callListAsync; }
		}

		#endregion Public Properties

		#region Methods

		#region Public Methods

#if !SILVERLIGHT

		#region Synchronous Methods

		public IList<object> ExecuteBatch()
		{
			throw new NotImplementedException();
		}

		/// <summary>
        /// Execute a list of individual API calls in a single batch.
        /// </summary>
        /// <returns>An array of individual results as objects.</returns>
        public IList<Object> ExecuteBatch(bool compressHttp)
		{
			return ExecuteBatch(false,compressHttp);
		}

        /// <summary>
        /// Execute a list of individual API calls in a single batch.
        /// </summary>
        /// <param name="isSerial">n optional parameter to indicate whether the methods in the method_feed must be executed in order. The default value is false.</param>
        /// <returns>An array of individual results as objects.</returns>
		public IList<Object> ExecuteBatch(bool isSerial, bool compressHttp)
		{
			IList<Object> ret = new List<Object>();
			string response = Run(_callList, isSerial,compressHttp);
			_callList.Clear();

			var responses = SplitResponses(response);			
			var responseObjects = new List<object>();

			foreach (var r in responses)
			{
				Type t = TypeHelper.getResponseObjectType(r);
                var method = typeof(Utilities).GetMethods().First(m => m.Name=="DeserializeXML");
				MethodInfo methodInfo = method.MakeGenericMethod(t);
				responseObjects.Add(methodInfo.Invoke(null, new object[] { r }));
			}			

			if (responseObjects.Count > 0)
			{
				return responseObjects;
			}
			else
			{
				return null;
			}
		}

        #endregion Synchronous Methods

#endif

        #region Asynchronous Methods

        /// <summary>
        /// Execute a list of individual API calls in a single batch.
        /// </summary>
        /// <returns>An array of individual results as objects.</returns>
        public void ExecuteBatchAsync()
		{
			ExecuteBatchAsync(false);
		}

        /// <summary>
        /// Execute a list of individual API calls in a single batch.
        /// </summary>
        /// <param name="isSerial">n optional parameter to indicate whether the methods in the method_feed must be executed in order. The default value is false.</param>
        /// <returns>An array of individual results as objects.</returns>
        public void ExecuteBatchAsync(bool isSerial)
		{
			var callList = _callListAsync.Select(record => record.PostData).ToList();
			Run(callList, isSerial, true);
        }

        #endregion Asynchronous Methods

        /// <summary>
        /// Sets the current Batch state of the Batch object to Active.
        /// </summary>
        public void BeginBatch()
		{
			_isBatchActive = true;
		}

		#endregion Public Methods

		#region Private Methods

		private string Run(List<string> callList, bool isSerial, bool compressHttp)
		{
			return Run(callList, isSerial, false, compressHttp);
		}

		private string Run(List<string> callList, bool isSerial, bool isAsync, bool compressHttp)
		{
			_isBatchActive = false;
			var parameterList = new Dictionary<string, string> { { "method", "facebook.batch.run" } };
			Utilities.AddJSONArray(parameterList, "method_feed", callList);
			if (isSerial)
			{
				Utilities.AddRequiredParameter(parameterList, "serial_only", "1");
			}

			if (isAsync)
			{
                SendRequestAsync(parameterList, new AsyncResult(OnRunCompleted, null, null));
				return null;
			}

            return SendRequest(parameterList, compressHttp);
		}

		/// <summary>
		/// Completes the async operation that was started by BeginExecuteBatch
		/// </summary>
		/// <param name="ar">An IAsyncResult that references a pending request for token</param>
		private void OnRunCompleted(IAsyncResult ar)
		{
			AsyncResult asyncResult = (AsyncResult)ar;
			FacebookException exception = asyncResult.Exception;

			if (exception != null)
			{
				foreach (BatchRecord record in _callListAsync)
				{
					record.AsyncResult.SetComplete(null, asyncResult.Exception);
				}

				_callListAsync.Clear();
				return;
			}

			var responses = SplitResponses(asyncResult.Result).ToList();

			for (int i = 0; i < _callListAsync.Count; i++)
			{
				_callListAsync[i].AsyncResult.SetComplete(responses[i], null);
			}

			_callListAsync.Clear();
		}

		private IEnumerable<string> SplitResponses(string xmlResponse)
		{
			XDocument doc = XDocument.Parse(xmlResponse);
			XNamespace xname = XNamespace.Get(Constants.FacebookNamespace);
			return from element in doc.Descendants(xname + "batch_run_response_elt")
				   select element.Value;
		}

		#endregion Private Methods

		#endregion Methods
	}

	///<summary>
	///</summary>
	public class BatchRecord
	{
		///<summary>
		///</summary>
		public AsyncResult AsyncResult
		{
			get;
			set;
		}

		///<summary>
		///</summary>
		public string PostData { get; set; }
        
		///<summary>
		///</summary>
		///<param name="ar"></param>
		///<param name="postData"></param>
		public BatchRecord(AsyncResult ar, string postData)
		{
			AsyncResult = ar;
			PostData = postData;
		}
		#endregion

	}
}
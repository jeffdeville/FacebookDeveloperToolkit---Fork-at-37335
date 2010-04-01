using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
	/// <summary>
    /// Represents object responsible for facebook REST calls.
    /// </summary>
    public class AuthorizedRestBase : RestBase, IAuthorizedRestBase
    {
       
        #region Public Properties

        ///<summary>
        /// Gets or sets the Batch object.
        ///</summary>
        public Batch Batch { get; set; }

        ///<summary>
        /// Gets or Sets Permissions object.
        ///</summary>
        public Permissions Permissions { get; set; }

        #endregion Public Properties

        #region Protected Properties

        /// <summary>
        /// Gets the FacebookSession object.
        /// </summary>
        public IFacebookSession Session { get; set; }

        #endregion Protected Properties

        #region Methods

        #region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="session">Session object</param>
		public AuthorizedRestBase(ApplicationInfo appInfo, IFacebookSession session)
			: base(appInfo)
		{
			Session = session;
		}


        #endregion Constructor

        #region Public Methods

        #region Synchronous Methods

       
        
        /// <summary>
        /// Sends a request through the REST API to Facebook. This is what the other methods all use to communicate with Facebook.
        /// You should only use this method if you need to call a method that is not defined in this toolkit.
        /// </summary>
        /// <param name="parameterDictionary">
        /// A dictionary of parameters for the method being called, as specified in the documentation for the method. You will need
        /// to make sure to have a parameter named "method" corresponding to the method name. The toolkit will populate
        /// the api_key, call_id, sig, v, and session_key parameters.
        /// </param>
        /// <param name="useSession">Whether or not to use the session key in the request.</param>
        /// <returns>The XML response returned by Facebook.</returns>
        public string SendRequest(IDictionary<string, string> parameterDictionary, bool useSession)
        {
        	if (useSession)
        		parameterDictionary.Add("session_key", Session.SessionKey);
        	
			var result = SendRequestSynchronous(parameterDictionary);
            return string.IsNullOrEmpty(result) ? null : result;
        }

		

		/// <summary>
		/// Makes a synchronous call to facebook server and returns result.
		/// </summary>
		/// <typeparam name="T">Output Type</typeparam>
		/// <param name="parameterDictionary">parameters that needs to be passed to call</param>
		/// <param name="useSession"></param>
        /// <returns>This method returns an object (of type T) resulting from the request.</returns>
        public T SendRequest<T>(IDictionary<string, string> parameterDictionary, bool useSession)
		{
			if (useSession)
				parameterDictionary.Add("session_key", Session.SessionKey);
			var result = SendRequestSynchronous(parameterDictionary);
			return Utilities.DeserializeXML<T>(result);
        }

        #endregion Synchronous Methods

        #region Asynchronous Methods

        

		/// <summary>
		/// Makes a call to facebook server and returns the result in callback specified
		/// </summary>
		/// <typeparam name="T">Type of object to which result should be deserialized</typeparam>
		/// <param name="parameterList">parameters that needs to be passed to call</param>
        /// <param name="useSession">whether the call should use the facebook session or not</param>
        /// <param name="callback">Callback which will be called on response.</param>
		/// <param name="state">User defined object</param>
		public void SendRequestAsync<T>(Dictionary<string, string> parameterList, bool useSession, FacebookCallCompleted<T> callback, Object state)
		{
			if (useSession)
				parameterList.Add("session_key", Session.SessionKey);
			AsyncResult result = new AsyncResult(OnFacebookCallCompleted<T>, callback, state);
			SendRequestAsync(parameterList, result);
		}

       

		/// <summary>
		/// Makes a call to facebook server and returns the result in callback specified
		/// </summary>
		/// <param name="parameterList">parameters that needs to be passed to call</param>
        /// <param name="useSession">indicator if session should be used or not</param>
        /// <param name="callback">Callback which will be called on response.</param>
		/// <param name="state">User defined object</param>
		public void SendRequestAsync<TObject, TResult>(Dictionary<string, string> parameterList, bool useSession, FacebookCallCompleted<TResult> callback, Object state)
		{
			if (useSession)
				parameterList.Add("session_key", Session.SessionKey);
			SendRequestAsync<TObject, TResult>(parameterList, callback, state, "TypedValue");
		}

        

		/// <summary>
		/// Makes a REST call to facebook server. 
		/// </summary>
		/// <param name="parameterList">Parameters for REST call</param>
        /// <param name="useSession">indicator if session should be used or not</param>
        /// <param name="ar"></param>
		/// <remarks>Function made virtual for unittesting</remarks>
		public void SendRequestAsync(IDictionary<string, string> parameterList, bool useSession, AsyncResult ar)
		{
			if (useSession && Session.SessionKey != null)
			{
				parameterList.Add("session_key", Session.SessionKey);
			}

			if (Permissions != null && Permissions.IsPermissionsModeActive)
			{
				parameterList.Add("call_as_apikey", Permissions.CallAsApiKey);
			}
			SendRequestAsync(parameterList, ar);
			//string postData = CreatePostData(parameterList);

			//if (Batch != null && Batch.IsActive)
			//{
			//    Batch.CallListAsync.Add(new BatchRecord(ar, postData));
			//}
			//else
			//{
			//    WebClientHelper client = new WebClientHelper(ar);
			//    UriBuilder uriBd = new UriBuilder(Constants.FacebookRESTUrl);

			//    client.Method = "POST";
			//    client.RequestCompleted += OnRequestCompleted;
			//    client.SendRequest(uriBd.Uri, postData);
			//}
		}

        #endregion Asynchronous Methods

        #endregion Public Methods

        #region Internal Methods

        /// <summary>
        /// Makes a REST call to upload a video to the facebook server. 
        /// </summary>
        /// <param name="parameters">Parameters for REST call</param>
        /// <param name="data">the bytes of the file</param>
        /// <param name="contentType"></param>
        /// <param name="ar"></param>
        /// <remarks>Function made virtual for unittesting</remarks>
        public void UploadVideoFile(IDictionary<string, string> parameters, byte[] data, string contentType, AsyncResult ar)
		{
			UploadFile(parameters, data, contentType, new Uri(Constants.FacebookVideoUploadUrl), ar);
		}

        /// <summary>
        /// Makes a REST call to upload a video to the facebook server. 
        /// </summary>
        /// <param name="parameters">Parameters for REST call</param>
        /// <param name="data">the bytes of the file</param>
        /// <param name="contentType"></param>
        /// <param name="ar"></param>
        /// <remarks>Function made virtual for unittesting</remarks>
        public void UploadFile(IDictionary<string, string> parameters, byte[] data, string contentType, AsyncResult ar)
		{
			UploadFile(parameters, data, contentType, new Uri(Constants.FacebookRESTUrl), ar);
		}

        /// <summary>
        /// Makes a REST call to upload a video to the facebook server. 
        /// </summary>
        /// <param name="parameters">Parameters for REST call</param>
        /// <param name="data">the bytes of the file</param>
        /// <param name="contentType"></param>
        /// <param name="uploadUrl">The url to upload the file to</param>
        /// <param name="ar"></param>
        /// <remarks>Function made virtual for unittesting</remarks>
        public void UploadFile(IDictionary<string, string> parameters, byte[] data, string contentType, Uri uploadUrl, AsyncResult ar)
		{
			string boundary = DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture);
			byte[] postData = CreatePostData(parameters, data, contentType, boundary);
			WebClientHelper client = new WebClientHelper(ar) { Method = "POST" };
			client.RequestCompleted += OnRequestCompleted;
			client.SendRequest(uploadUrl, postData, String.Concat("multipart/form-data; boundary=", boundary));
		}

#if !SILVERLIGHT

        /// <summary>
        /// Makes a REST call to upload an image to the facebook server. 
        /// </summary>
        /// <param name="uploadFile">pointer to the image file on the local drive</param>
        /// <param name="parameterList">Parameters for REST call</param>
        public string ExecuteApiImageUpload(FileSystemInfo uploadFile, IDictionary<string, string> parameterList)
        {
            parameterList.Add("api_key", Session.ApplicationKey);
            parameterList.Add("session_key", Session.SessionKey);
            parameterList.Add("v", Constants.VERSION);
            parameterList.Add("call_id", DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture));
            if (Session.SessionSecret != null)
            {
                parameterList.Add("ss", "1");
            }
            parameterList.Add("sig", GenerateSignature(parameterList));

            return GetFileQueryResponse(parameterList, uploadFile, "image/jpeg");
        }

        /// <summary>
        /// Makes a REST call to upload an video to the facebook server. 
        /// </summary>
        /// <param name="uploadFile">pointer to the image file on the local drive</param>
        /// <param name="parameterList">Parameters for REST call</param>
        public string ExecuteApiVideoUpload(FileSystemInfo uploadFile, IDictionary<string, string> parameterList)
        {
            parameterList.Add("api_key", Session.ApplicationKey);
            parameterList.Add("session_key", Session.SessionKey);
            parameterList.Add("v", Constants.VERSION);
            parameterList.Add("call_id", DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture));
            if (Session.SessionSecret != null)
            {
                parameterList.Add("ss", "1");
            }
            parameterList.Add("sig", GenerateSignature(parameterList));

            return GetFileQueryResponse(parameterList, uploadFile, "video/avi");
        }

        /// <summary>
        /// Get the string returned from a post to Facebook
        /// </summary>
        /// <param name="parameterDictionary">key value pairs to post as part of the request</param>
        /// <param name="uploadFile">The file to upload</param>
        /// <param name="contentType">video/avi or image/jpeg</param>
        public static string GetFileQueryResponse(IEnumerable<KeyValuePair<string, string>> parameterDictionary,
                                                    FileSystemInfo uploadFile, string contentType)
        {
            string responseData;

            string boundary = DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture);
            string sRequestUrl = Constants.FacebookRESTUrl;
            if (contentType == "video/avi")
            {
                sRequestUrl = sRequestUrl.Replace("api.", "api-video.");
            }

            // Build up the post message header
            var sb = new StringBuilder();
            foreach (var kvp in parameterDictionary)
            {
                sb.Append(Constants.PREFIX).Append(boundary).Append(Constants.NEWLINE);
                sb.Append("Content-Disposition: form-data; name=\"").Append(kvp.Key).Append("\"");
                sb.Append(Constants.NEWLINE);
                sb.Append(Constants.NEWLINE);
                sb.Append(kvp.Value);
                sb.Append(Constants.NEWLINE);
            }
            sb.Append(Constants.PREFIX).Append(boundary).Append(Constants.NEWLINE);
            sb.Append("Content-Disposition: form-data; filename=\"").Append(uploadFile.Name).Append("\"").Append(Constants.NEWLINE);
            sb.Append("Content-Type: ").Append(contentType).Append(Constants.NEWLINE).Append(Constants.NEWLINE);

            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sb.ToString());
            byte[] fileData = File.ReadAllBytes(uploadFile.FullName);

            byte[] boundaryBytes = Encoding.UTF8.GetBytes(String.Concat(Constants.NEWLINE, Constants.PREFIX, boundary, Constants.PREFIX, Constants.NEWLINE));

            var webrequest = (HttpWebRequest)WebRequest.Create(sRequestUrl);
            webrequest.ContentLength = postHeaderBytes.Length + fileData.Length + boundaryBytes.Length;
            webrequest.ContentType = String.Concat("multipart/form-data; boundary=", boundary);
            webrequest.Method = "POST";

            using (System.IO.Stream requestStream = webrequest.GetRequestStream())
            {
                requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                requestStream.Write(fileData, 0, fileData.Length);
                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            }
            var response = (HttpWebResponse)webrequest.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                responseData = streamReader.ReadToEnd();
            }
            Utilities.ParseException(responseData, false);
            return responseData;
        }
#endif

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
		/// <summary>
		/// Makes a call to facebook server and returns the result in callback specified
		/// </summary>
		/// <param name="parameterList">parameters that needs to be passed to call.</param>
		/// <param name="useSession">indicator if session should be used or not</param>
		/// <param name="callback">Callback which will be called on response.</param>
		/// <param name="state">User defined object.</param>
		/// <param name="propertyName">The name of the property to retreive.</param>
		public void SendRequestAsync<TObject, TResult>(Dictionary<string, string> parameterList, bool useSession, FacebookCallCompleted<TResult> callback, Object state, string propertyName)
		{
			SendRequestAsync(parameterList, useSession, new AsyncResult(ar => OnFacebookCallCompleted<TObject, TResult>(ar, propertyName), callback, state));
		}

		protected byte[] CreatePostData(IDictionary<string, string> parameters, byte[] data, string contentType, string boundary)
		{
			if (!_mimeTypes.ContainsKey(contentType))
			{
				throw new FacebookException("unsupported content type");
			}
			parameters.Add("api_key", Session.ApplicationKey);
			parameters.Add("session_key", Session.SessionKey);
			parameters.Add("v", Constants.VERSION);
			parameters.Add("call_id", DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture));
			if (Session.SessionSecret != null)
			{
				parameters.Add("ss", "1");
			}
			parameters.Add("sig", GenerateSignature(parameters));


			// Build the query
			var sb = new StringBuilder();
			foreach (var kvp in parameters)
			{
				sb.Append(Constants.PREFIX).Append(boundary).Append(Constants.NEWLINE);
				sb.Append("Content-Disposition: form-data; name=\"").Append(kvp.Key).Append("\"");
				sb.Append(Constants.NEWLINE);
				sb.Append(Constants.NEWLINE);
				sb.Append(kvp.Value);
				sb.Append(Constants.NEWLINE);
			}

			sb.Append(Constants.PREFIX).Append(boundary).Append(Constants.NEWLINE);
			sb.Append("Content-Disposition: form-data; filename=\"dummyFileName." + _mimeTypes[contentType].ToString() + "\"").Append(Constants.NEWLINE);
			sb.Append("Content-Type: ").Append(contentType).Append(Constants.NEWLINE).Append(Constants.NEWLINE);

			byte[] boundaryBytes = Encoding.UTF8.GetBytes(String.Concat(Constants.NEWLINE, Constants.PREFIX, boundary, Constants.PREFIX, Constants.NEWLINE));

			byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sb.ToString());

			using (MemoryStream stream = new MemoryStream(postHeaderBytes.Length + data.Length + boundaryBytes.Length))
			{
				stream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
				stream.Write(data, 0, data.Length);
				stream.Write(boundaryBytes, 0, boundaryBytes.Length);
				return stream.GetBuffer();
			}
		}

       

        

        

        internal bool IsSessionActive()
        {
            return !String.IsNullOrEmpty(Session.SessionKey);
        }

        #endregion Internal Methods

        #region Protected Methods

        /// <summary>
        /// Internal call back when xml deserialization completes. 
        /// </summary>
        /// <param name="result"></param>
        protected void XmlResultOperationCompleted(IAsyncResult result)
        {
            AsyncResult ar = (AsyncResult)result;
            FacebookCallCompleted<string> callback = (FacebookCallCompleted<string>)ar.AsyncState;
            if (callback != null)
            {
                callback(ar.Result, ar.AsyncExternalState, ar.Exception);
            }
        }

		/// <summary>
		/// Internal call back when facebook call completes. 
		/// </summary>
		/// <param name="result"></param>
		protected void OnFacebookCallCompleted<T>(IAsyncResult result)
		{
			OnFacebookCallCompleted<T, T>(result, null);
		}

		/// <summary>
		/// Deserializes facebook response object if there is no exception and calls the callback with result
		/// </summary>
		/// <typeparam name="TObject"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="result"></param>
		/// <param name="propName"></param>
		private static void OnFacebookCallCompleted<TObject, TResult>(IAsyncResult result, string propName)
		{
			AsyncResult ar = (AsyncResult)result;
			FacebookCallCompleted<TResult> callback = (FacebookCallCompleted<TResult>)ar.AsyncState;

			if (callback == null)
			{
				return;
			}

			TResult value = default(TResult);
			FacebookException exception = ar.Exception;

			if (exception == null)
			{
				try
				{
					// XML Serialization
					TObject response = Utilities.DeserializeXML<TObject>(ar.Result);

					// JSON Serialization
					//string resultString = FixupJSONString(ar.Result, types[0]);
					//MethodInfo methodInfo = typeof(Utilities).GetMethod("DeserializeJSONObject").MakeGenericMethod(types[0]);
					//resultObject = methodInfo.Invoke(null, new object[] { resultString });

					if (!string.IsNullOrEmpty(propName))
					{
					   value = (TResult)response.GetType().GetProperty(propName).GetValue(response, null);
					}
					else
					{
						value = (TResult)(object)response;
					}
				}
				catch (FacebookException e)
				{
					exception = e;
				}

			}

			callback(value, ar.AsyncExternalState, exception);
		}

        #endregion Protected Methods


        #endregion Methods
    }

    #region Delegates

    /// <summary>
    /// Delegate called when facebook call completes
    /// </summary>
    /// <typeparam name="T">Type of object to which result should be deserialized</typeparam>
    /// <param name="result">desrialized result object</param>
    /// <param name="state">Object passed in the initial facebook call</param>
    /// <param name="e">Exception if any</param>
    public delegate void FacebookCallCompleted<T>(T result, Object state, FacebookException e);

    /// <summary>
    /// Delegate called when facebook call completes
    /// </summary>
    public delegate void FacebookCallCompleted(AsyncCallback callback, Object state, Object externalstate);

    #endregion Delegates
}

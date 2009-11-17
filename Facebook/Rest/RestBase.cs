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
    public class RestBase : IRestBase
    {
        #region Private Members
        private static Dictionary<string, Enums.FileType> _mimeTypes = new Dictionary<string, Enums.FileType> {
            {"video/x-ms-asf", Enums.FileType.asf},
            {"video/avi", Enums.FileType.avi},
            {"video/x-flv", Enums.FileType.flv},
            {"video/x-matroska", Enums.FileType.mkv},
            {"video/quicktime",Enums.FileType.mov},
            {"video/mp4",Enums.FileType.mp4},
            {"video/mpeg",Enums.FileType.mpeg},
            {"application/x-winamp",Enums.FileType.nsv},
            {"video/ogg",Enums.FileType.ogm},
            {"video/dvd",Enums.FileType.vob},
            {"video/x-ms-wmv",Enums.FileType.wmv},
            {"image/gif",Enums.FileType.gif},
            {"image/jpeg",Enums.FileType.jpg},
            {"image/png",Enums.FileType.png},
            {"image/psd",Enums.FileType.psd},
            {"image/tiff",Enums.FileType.tiff},
            {"image/jp2",Enums.FileType.jp2},
            {"image/iff",Enums.FileType.iff},
            {"image/vnd.wap.wbmp",Enums.FileType.wbmp},
            {"image/x-xbitmap",Enums.FileType.xbm},
            {"image/bmp",Enums.FileType.bmp},
            };
        #endregion
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
		public RestBase(IFacebookSession session)
		{
			Session = session;
		}

		public RestBase(){}



        #endregion Constructor

        #region Public Methods

        #region Synchronous Methods

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
        public string SendRequest(IDictionary<string, string> parameterDictionary)
        {
            return SendRequest(parameterDictionary, true);
        }
        
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
            var result = SendRequestSynchronous(parameterDictionary, useSession);
            return string.IsNullOrEmpty(result) ? null : result;
        }

		/// <summary>
		/// Makes a synchronous call to facebook server and returns result.
		/// </summary>
		/// <typeparam name="T">Output Type</typeparam>
		/// <param name="parameterDictionary">parameters that needs to be passed to call</param>
		/// <returns>This method returns an object (of type T) resulting from the request.</returns>
		public T SendRequest<T>(IDictionary<string, string> parameterDictionary)
		{
			var result = SendRequestSynchronous(parameterDictionary, true);
			return Utilities.DeserializeXML<T>(result);
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
			var result = SendRequestSynchronous(parameterDictionary, useSession);
			return Utilities.DeserializeXML<T>(result);
        }

        #endregion Synchronous Methods

        #region Asynchronous Methods

        /// <summary>
		/// Makes a call to facebook server and returns the result in callback specified
		/// </summary>
		/// <typeparam name="T">Type of object to which result should be deserialized</typeparam>
		/// <param name="parameterList">parameters that needs to be passed to call</param>
		/// <param name="callback">Callback which will be called on response.</param>
		/// <param name="state">User defined object</param>
		public void SendRequestAsync<T>(Dictionary<string, string> parameterList, FacebookCallCompleted<T> callback, Object state)
		{
			SendRequestAsync(parameterList, true, callback, state);
		}

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
			AsyncResult result = new AsyncResult(OnFacebookCallCompleted<T>, callback, state);
			SendRequestAsync(parameterList, useSession, result);
		}

        /// <summary>
        /// Makes a call to facebook server and returns the result in callback specified
        /// </summary>
        /// <param name="parameterList">parameters that needs to be passed to call</param>
        /// <param name="callback">Callback which will be called on response.</param>
        /// <param name="state">User defined object</param>
        public void SendRequestAsync<TObject, TResult>(Dictionary<string, string> parameterList, FacebookCallCompleted<TResult> callback, Object state)
		{
            SendRequestAsync<TObject, TResult>(parameterList, true, callback, state);
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
			SendRequestAsync<TObject, TResult>(parameterList, useSession, callback, state, "TypedValue");
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
			SendRequestAsync<TObject, TResult>(parameterList, true, callback, state, propertyName);
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

		/// <summary>
		/// Makes a REST call to facebook server. 
		/// </summary>
		/// <param name="parameterList">Parameters for REST call</param>
		/// <param name="ar"></param>
		/// <remarks>Function made virtual for unittesting</remarks>
		public void SendRequestAsync(IDictionary<string, string> parameterList, AsyncResult ar)
		{
			SendRequestAsync(parameterList, true, ar);
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

			string postData = CreatePostData(parameterList);

			if (Batch != null && Batch.IsActive)
			{
				Batch.CallListAsync.Add(new BatchRecord(ar, postData));
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
                using (System.IO.StreamReader webReader = new System.IO.StreamReader(webResponse.GetResponseStream()))
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
        /// <summary>
        /// This method generates the signature based on parameters supplied
        /// </summary>
        /// <param name="parameters">List of paramenters</param>
        /// <returns>Generated signature</returns>
        internal string GenerateSignature(IDictionary<string, string> parameters)
        {
            return GenerateSignature(false, parameters);
        }

        /// <summary>
        /// This method generates the signature based on parameters supplied
        /// </summary>
        /// <param name="forceApplicationSecret">Flag to force use of Application, not User Session secret.</param>
        /// <param name="parameters">List of paramenters</param>
        /// <returns>Generated signature</returns>
        internal string GenerateSignature(bool forceApplicationSecret, IDictionary<string, string> parameters)
        {
            var signatureBuilder = new StringBuilder();

            // Sort the keys of the method call in alphabetical order
            var keyList = Utilities.ParameterDictionaryToList(parameters);
            keyList.Sort();

            // Append all the parameters to the signature input paramaters
            foreach (string key in keyList)
                signatureBuilder.Append(String.Format(CultureInfo.InvariantCulture, "{0}={1}", key, parameters[key]));

            // Append the secret to the signature builder
            signatureBuilder.Append(forceApplicationSecret ? Session.ApplicationSecret : Session.Secret);

            // Compute the MD5 hash of the signature builder
            byte[] hash = MD5Core.GetHash(signatureBuilder.ToString().Trim());

            //var md5 = MD5.Create();
            // Compute the MD5 hash of the signature builder
            //byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(signatureBuilder.ToString().Trim()));

            // Reinitialize the signature builder to store the actual signature
            signatureBuilder = new StringBuilder();

            // Append the hash to the signature
            foreach (var hashByte in hash)
                signatureBuilder.Append(hashByte.ToString("x2", CultureInfo.InvariantCulture));

            return signatureBuilder.ToString();
        }

        internal string CreateHTTPParameterList(IDictionary<string, string> parameterList)
        {
            var queryBuilder = new StringBuilder();

            parameterList.Add("api_key", Session.ApplicationKey);
            parameterList.Add("v", Constants.VERSION);
            parameterList.Add("call_id", DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture));

            if (Session.SessionSecret != null)
            {
                parameterList.Add("ss", "1");
            }
            parameterList.Add("sig", GenerateSignature(parameterList));


            // Build the query
            foreach (var kvp in parameterList)
            {
                queryBuilder.Append(kvp.Key);
                queryBuilder.Append("=");
#if !SILVERLIGHT
                queryBuilder.Append(System.Web.HttpUtility.UrlEncode(kvp.Value));
#else
                queryBuilder.Append(System.Windows.Browser.HttpUtility.UrlEncode(kvp.Value));
#endif
                queryBuilder.Append("&");
            }
            queryBuilder.Remove(queryBuilder.Length - 1, 1);
            return queryBuilder.ToString();
        }

        internal static string GetRequestUrl(bool useSSL)
        {
            return useSSL ? Constants.FacebookRESTUrl.Replace("http", "https") : Constants.FacebookRESTUrl;
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
        private static void OnRequestCompleted(object sender, RequestCompletedEventArgs e)
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

        /// <summary>
        /// Creates a POST data from parameters list
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>This method returns a formatted Post string.</returns>
        private string CreatePostData(IDictionary<string, string> parameters)
        {
            parameters.Add("api_key", Session.ApplicationKey);
            parameters.Add("v", Constants.VERSION);
            parameters.Add("call_id", DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture));
            if (Session.SessionSecret != null)
            {
                parameters.Add("ss", "1");
            }
            parameters.Add("sig", GenerateSignature(parameters));


            // Build the query
            var queryBuilder = new StringBuilder();
            foreach (var kvp in parameters)
            {
                queryBuilder.Append(kvp.Key);
                queryBuilder.Append("=");
#if !SILVERLIGHT
                queryBuilder.Append(System.Web.HttpUtility.UrlEncode(kvp.Value));
#else
                queryBuilder.Append(System.Windows.Browser.HttpUtility.UrlEncode(kvp.Value));
#endif
                queryBuilder.Append("&");
            }
            queryBuilder.Remove(queryBuilder.Length - 1, 1);

            return queryBuilder.ToString();
        }

        private byte[] CreatePostData(IDictionary<string, string> parameters, byte[] data, string contentType, string boundary)
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

        private string SendRequestSynchronous(IDictionary<string, string> parameterDictionary, bool useSession)
        {
            if (useSession)
            {
                parameterDictionary.Add("session_key", Session.SessionKey);
            }

            string requestUrl = GetRequestUrl(parameterDictionary["method"] == "facebook.auth.getSession");
            if (Permissions.IsPermissionsModeActive)
            {
                parameterDictionary.Add("call_as_apikey", Permissions.CallAsApiKey);
            }
            string parameters = CreateHTTPParameterList(parameterDictionary);
            if (Batch.IsActive)
            {
                Batch.CallList.Add(parameters);
                return null;
            }
            string result = null;
#if !SILVERLIGHT
            if (Session.CompressHttp)
            {
                result = processCompressedResponse(postRequest(requestUrl, parameters, true));
            }
            else
            {
                result = processResponse(postRequest(requestUrl, parameters, false));
            }
#else
                result = processResponse(postRequest(requestUrl, parameters, false));
#endif
            return string.IsNullOrEmpty(result) ? null : result;
        }
        
        #endregion Private Methods

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

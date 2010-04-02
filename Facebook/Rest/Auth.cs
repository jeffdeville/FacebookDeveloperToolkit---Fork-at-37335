using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Utility;

namespace Facebook.Rest
{
	/// <summary>
    /// Facebook Auth API methods.
    /// </summary>
    public class Auth //: RestBase, IAuth
    {
        #region Methods

        #region Constructor

        /// <summary>
        /// Public constructor for facebook.Auth
        /// </summary>
        /// <param name="session">Needs a connected Facebook Session object for making requests</param>
		public Auth(IFacebookNetworkWrapper networkWrapper, ApplicationInfo appInfo)
			: base(clientHelper, appInfo) { }

    	#endregion Constructor

        #region Public Methods

#if !SILVERLIGHT
        
        #region Synchronous Methods

        /// <summary>
        /// Creates an auth_token to be passed in as a parameter to GetSession after the user has logged in.
		/// (Intended for desktop applications only)
		/// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// string result = api.Auth.CreateToken();
        /// </code>
        /// </example>
        /// <returns>This method creates an auth_token to be passed in as a parameter to Facebook Login url and then to Auth.GetSession after the user has logged in.</returns>
        /// <remarks>That this function does not require a session_key or call_id (although specifying a call_id will not cause any problems). The values returned from this call are storable, but expire on their first use in facebook.auth.getSession.</remarks>
        public string CreateToken()
		{
            return CreateToken(false, null, null);
        }

        /// <summary>
        /// Returns the session key bound to an auth_token, as returned by CreateToken or in the callback URL.
		/// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// // Note see GetSession(string auth_token) method for a more complete (creating token, authenticating) example flow
        /// var result = api.Auth.GetSession();
        /// </code>
        /// </example>
        /// <returns>If the user has successfully logged in, this will return valid values for each field. The expires element is a Unix time that indicates when the given session will expire. If the value is 0, the session will never expire. See the authentication guide for more information.</returns>
		/// <remarks>For desktop applications this method must be called at the https endpoint instead of the http endpoint, and its return value is slightly different (as noted below). Also, this function does not require a session_key or call_id (although specifying a call_id will not cause any problems). The session key is storable for the duration of the session, and the uid is storable indefinitely. For desktop applications, the top-level element will have an additional element named secret that should be used as the session's secret key as described in the facebook authentication guide.</remarks>
		public session_info GetSession()
		{
			return GetSession(CreateToken());
		}

		/// <summary>
        /// Returns the session key bound to an auth_token, as returned by CreateToken or in the callback URL.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///
        /// // Create auth token
        /// var token = api.Auth.CreateToken();
        ///
        /// // Note user must be navigated to Facebook login.php to validate token
        /// const string facebookLoginUrl = "https://login.facebook.com/login.php?api_key={0}&amp;auth_token={1}&amp;v=1.0&amp;popup";
        /// var loginurl = String.Format(facebookLoginUrl, Constants.WebApplicationKey, token);
        /// System.Diagnostics.Process.Start(loginurl);
        ///
        /// // After login.php visited and login completed:
        /// var result = api.Auth.GetSession(token);
        /// </code>
        /// </example>
        /// <param name="auth_token"></param>
		/// <returns>If the user has successfully logged in, this will return valid values for each field. The expires element is a Unix time that indicates when the given session will expire. If the value is 0, the session will never expire. See the authentication guide for more information.</returns>
		/// <remarks>For desktop applications this method must be called at the https endpoint instead of the http endpoint, and its return value is slightly different (as noted below). Also, this function does not require a session_key or call_id (although specifying a call_id will not cause any problems). The session key is storable for the duration of the session, and the uid is storable indefinitely. For desktop applications, the top-level element will have an additional element named secret that should be used as the session's secret key as described in the facebook authentication guide.</remarks>
		public session_info GetSession(string auth_token)
		{
		    return GetSession(auth_token, false, null, null);
		}

        

		/// <summary>
        /// Returns a temporary session secret associated to the current existing session, for use in a client-side component to an application.
		/// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Auth.PromoteSession();
        /// </code>
        /// </example>
        /// <returns>This method creates a temporary session secret for the current (non-infinite) session of a Web application.</returns>
		public string PromoteSession()
		{
		    return PromoteSession(false, null, null);
        }

#endregion Synchronous Methods
        
#endif

        #region Asynchronous Methods

        /// <summary>
        /// Creates an auth_token to be passed in as a parameter to GetSession after the user has logged in.
        /// (Intended for desktop applications only)
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Auth.CreateTokenAsync(AsyncDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncDemoCompleted(string result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method creates an auth_token to be passed in as a parameter to Facebook Login url and then to Auth.GetSession after the user has logged in.</returns>
        public void CreateTokenAsync(CreateTokenCallback callback, Object state)
        {
            CreateToken(true, callback, state);
        }

        /// <summary>
        /// Returns the session key bound to an auth_token, as returned by CreateToken or in the callback URL.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///
        ///     // Create auth token (using synchronous for sample brevity only)
        ///     var token = api.Auth.CreateToken();
        ///
        ///     // Note user must be navigated to Facebook login.php to validate token
        ///     const string facebookLoginUrl = "https://login.facebook.com/login.php?api_key={0}&amp;auth_token={1}&amp;v=1.0&amp;popup";
        ///     var loginurl = String.Format(facebookLoginUrl, Constants.WebApplicationKey, token);
        ///     System.Diagnostics.Process.Start(loginurl);
        ///
        ///     // After login.php visited and 
        ///     api.Auth.GetSessionAsync(token, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(session_info result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="authToken">Token retrieved from prev CreateToken call</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>If the user has successfully logged in, this will return valid values for each field. The expires element is a Unix time that indicates when the given session will expire. If the value is 0, the session will never expire. See the authentication guide for more information.</returns>
        public void GetSessionAsync(string authToken, GetSessionCallback callback, Object state)
        {
            GetSession(authToken, true, callback, state);
        }
        
        
        
        /// <summary>
        /// Returns a temporary session secret associated to the current existing session, for use in a client-side component to an application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Auth.PromoteSessionAsync(AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(string result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method creates a temporary session secret for the current (non-infinite) session of a Web application.</returns>
        public void PromoteSessionAsync(PromoteSessionCallback callback, Object state)
        {
            PromoteSession(true, callback, state);
        }

       

        #endregion Asynchronous Methods

        #endregion Public Methods

        #region Private Methods

        private string CreateToken(bool isAsync, CreateTokenCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.auth.createToken" } };
            if(isAsync)
            {
                SendRequestAsync<auth_createToken_response, string>(parameterList, new FacebookCallCompleted<string>(callback), state);
                return null;
            }

            var response = SendRequest<auth_createToken_response>(parameterList);
            return response == null ? null : response.TypedValue;
        }

        

        private session_info GetSession(string auth_token, bool isAsync, GetSessionCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.auth.getSession" }, 
            { "auth_token", auth_token } };
                
            if (isAsync)
            {
        		SendRequestAsync(parameterList,  new FacebookCallCompleted<auth_getSession_response>(callback), state);
                return null;
            }

			return SendRequest<auth_getSession_response>(parameterList);
        }

        private string PromoteSession(bool isAsync, PromoteSessionCallback callback, Object state)
        {

            var parameterList = new Dictionary<string, string> { { "method", "facebook.auth.promoteSession" } };

            if (isAsync)
            {
                SendRequestAsync<auth_promoteSession_response, string>(parameterList, new FacebookCallCompleted<string>(callback), state);
                return null;
            }

			var response = SendRequest<auth_promoteSession_response>(parameterList);
			return response == null ? null : response.TypedValue;
        }

        

        public string ProxyGetSession(string authtoken, string generate_session_secret)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.auth.getSession" } };
            Utilities.AddOptionalParameter(parameterList, "auth_token", authtoken);
            Utilities.AddOptionalParameter(parameterList, "generate_session_secret", generate_session_secret);
            return SendRequest(parameterList);
        }

        #endregion Private Methods
        
        #endregion Methods

        #region Delegates

        /// <summary>
        /// Delegate called when CreateToken call completed
        /// </summary>
        /// <param name="token">Token that was created</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void CreateTokenCallback(string token, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetSession call completed
        /// </summary>
        /// <param name="sessionInfo">session info of current session</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetSessionCallback(session_info sessionInfo, Object state, FacebookException e);

       
         /// <summary>
        /// Delegate called when PromoteSession call completed
        /// </summary>
        /// <param name="token">Token represeting a session secret</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void PromoteSessionCallback(string token, Object state, FacebookException e);

       

        #endregion Delegates
    }
}

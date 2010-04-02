using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
	/// <summary>
	/// Facebook Status API methods.
	/// </summary>
	public class Status : AuthorizedRestBase, Facebook.Rest.IStatus
	{
		#region Methods

		#region Constructor

		/// <summary>
		/// Public constructor for facebook.Status
		/// </summary>
		/// <param name="session">Needs a connected Facebook Session object for making requests</param>
		public Status(SessionInfo session)
			: base(session)
		{
		}

		#endregion Constructor

		#region Public Methods

#if !SILVERLIGHT

		#region Synchronous Methods

        /// <summary>
        /// Returns the user's current and most recent statuses.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Status.Get();
        /// </code>
        /// </example>
        /// <returns>This method returns an List of user_status objects.</returns>
        /// <remarks>Pass in 0 if you want the logged in user or the default of 100 messages</remarks>
        public IList<user_status> Get()
		{
			return Get(0, 0);
		}

        /// <summary>
        /// Returns the user's current and most recent statuses.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Status.Get(Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="uid">The user ID of the user whose status messages you want to retrieve.</param>
        /// <returns>This method returns an List of user_status objects.</returns>
        /// <remarks>Pass in 0 if you want the logged in user or the default of 100 messages</remarks>
        public IList<user_status> Get(long uid)
		{
			return Get(uid, 0);
		}

        /// <summary>
        /// Returns the user's current and most recent statuses.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Status.Get(Constants.UserId, 20);
        /// </code>
        /// </example>
        /// <param name="uid">The user ID of the user whose status messages you want to retrieve.</param>
        /// <param name="limit">The number of status messages you want to return. (Default value is 100.)</param>
        /// <returns>This method returns an List of user_status objects.</returns>
        /// <remarks>Pass in 0 if you want the logged in user or the default of 100 messages</remarks>
        public IList<user_status> Get(long uid, int limit)
		{
			return Get(uid, limit, false, null, null);
		}

        /// <summary>
        /// Updates a user's Facebook status through your application. This is a streamlined version of users.setStatus.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Status.Set("Setting a test status.");
        /// </code>
        /// </example>
        /// <param name="status">The status message to set.  Note: The maximum message length is 255 characters; messages longer than that limit will be truncated and appended with '...'.</param>
        /// <returns>This method returns true on success, false on failure, or an error code.</returns>
        /// <remarks>Pass in 0 if you want the logged in user or the default of 100 messages</remarks>
        public bool Set(string status)
		{
			return Set(0, status);
		}

        /// <summary>
        /// Updates a user's Facebook status through your application. This is a streamlined version of users.setStatus.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Status.Set(Constants.UserId, "Setting a test status.");
        /// </code>
        /// </example>
        /// <param name="uid">The user ID of the user whose status you are setting. Note: This parameter applies only to Web applications. Facebook ignores this parameter if it is passed by a desktop application.</param>
        /// <param name="status">The status message to set.  Note: The maximum message length is 255 characters; messages longer than that limit will be truncated and appended with '...'.</param>
        /// <returns>This method returns true on success, false on failure, or an error code.</returns>
        /// <remarks>Pass in 0 if you want the logged in user or the default of 100 messages</remarks>
        public bool Set(long uid, string status)
		{
			return Set(uid, status, false, null, null);
		}

		#endregion

#endif

		#region Asynchronous Methods

        /// <summary>
        /// Returns the user's current and most recent statuses.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Status.GetAsync(AsyncDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncDemoCompleted(IList&lt;user_status&gt; result, Object state, FacebookException e)
        /// {
        ///    var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns an List of user_status objects.</returns>
        /// <remarks>Pass in 0 if you want the logged in user or the default of 100 messages</remarks>
        public void GetAsync(GetCallback callback, Object state)
		{
			GetAsync(0, 0, callback, state);
		}

        /// <summary>
        /// Returns the user's current and most recent statuses.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Status.GetAsync(Constants.UserId, AsyncDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncDemoCompleted(IList&lt;user_status&gt; result, Object state, FacebookException e)
        /// {
        ///    var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">The user ID of the user whose status messages you want to retrieve.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns an List of user_status objects.</returns>
        /// <remarks>Pass in 0 if you want the logged in user or the default of 100 messages</remarks>
        public void GetAsync(long uid, GetCallback callback, Object state)
		{
			GetAsync(uid, 0, callback, state);
		}

        /// <summary>
        /// Returns the user's current and most recent statuses.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Status.GetAsync(Constants.UserId, 20, AsyncDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncDemoCompleted(IList&lt;user_status&gt; result, Object state, FacebookException e)
        /// {
        ///    var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">The user ID of the user whose status messages you want to retrieve.</param>
        /// <param name="limit">The number of status messages you want to return. (Default value is 100.)</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns an List of user_status objects.</returns>
        /// <remarks>Pass in 0 if you want the logged in user or the default of 100 messages</remarks>
        public void GetAsync(long uid, int limit, GetCallback callback, Object state)
		{
			Get(uid, limit, true, callback, state);
		}

        /// <summary>
        /// Updates a user's Facebook status through your application. This is a streamlined version of users.setStatus.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Status.SetAsync("Setting a test async status.", AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="status">The status message to set.  Note: The maximum message length is 255 characters; messages longer than that limit will be truncated and appended with '...'.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true on success, false on failure, or an error code.</returns>
        /// <remarks>Pass in 0 if you want the logged in user or the default of 100 messages</remarks>
        public void SetAsync(string status, SetCallback callback, Object state)
		{
			SetAsync(0, status, callback, state);
		}

        /// <summary>
        /// Updates a user's Facebook status through your application. This is a streamlined version of users.setStatus.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Status.SetAsync(Constants.UserId, "Setting a test async status.", AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">The user ID of the user whose status you are setting. Note: This parameter applies only to Web applications. Facebook ignores this parameter if it is passed by a desktop application.</param>
        /// <param name="status">The status message to set.  Note: The maximum message length is 255 characters; messages longer than that limit will be truncated and appended with '...'.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true on success, false on failure, or an error code.</returns>
        /// <remarks>Pass in 0 if you want the logged in user or the default of 100 messages</remarks>
        public void SetAsync(long uid, string status, SetCallback callback, Object state)
		{
			Set(uid, status, true, callback, state);
		}

		#endregion

		#endregion Public Methods
        
		#region Private Methods		

		private IList<user_status> Get(long uid, int limit, bool isAsync, GetCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.status.get" } };
			Utilities.AddOptionalParameter(parameterList, "uid", uid);
			Utilities.AddOptionalParameter(parameterList, "limit", limit);

			if (isAsync)
			{
				SendRequestAsync<status_get_response, IList<user_status>>(parameterList, new FacebookCallCompleted<IList<user_status>>(callback), state, "user_status");
                return null;
			}

			var response = SendRequest<status_get_response>(parameterList);
			return response == null ? null : response.user_status;
		}

		/// <summary>
		/// pass in 0 if you want the logged in user or the default of 100 messages
		/// </summary>
        /// <returns>This method returns true on success, false on failure, or an error code.</returns>
        private bool Set(long uid, string status, bool isAsync, SetCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.status.set" } };
			Utilities.AddOptionalParameter(parameterList, "uid", uid);
			Utilities.AddOptionalParameter(parameterList, "status", status);

			if (isAsync)
			{
                SendRequestAsync<status_set_response, bool>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey), new FacebookCallCompleted<bool>(callback), state);
				return true;
			}
#if !SILVERLIGHT
            var response = SendRequest<status_set_response>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey));
#else
            var response = SendRequest<status_set_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
#endif


            return response == null ? true : response.TypedValue;
		}
        
		#endregion Private Methods
        
		#endregion Methods

		#region Delegates
        
        /// <summary>
        /// Delegate called when GetStatus call completed
        /// </summary>
        /// <param name="userstatus">List of user_status objects</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void GetCallback(IList<user_status> userstatus, Object state, FacebookException e);
        
        /// <summary>
        /// Delegate called when SetStatus call completed
        /// </summary>
        /// <param name="result">result of operation</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void SetCallback(bool result, Object state, FacebookException e);
        
		#endregion Delegates
	}
}

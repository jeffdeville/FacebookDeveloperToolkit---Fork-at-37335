using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
	/// <summary>
	/// Facebook Permissions API methods.
	/// </summary>
	public class Permissions : RestBase, Facebook.Rest.IPermissions
	{
        #region Internal Properties

        internal bool IsPermissionsModeActive
        {
            get;
            set;
        }

        internal string CallAsApiKey
	    {
            get;
            set;
        }

	    #endregion Internal Properties

        #region Methods

        #region Constructor

        /// <summary>
		/// Public constructor for facebook.Permissions
		/// </summary>
		/// <param name="session">Needs a connected Facebook Session object for making requests</param>
		public Permissions(FacebookSession session)
			: base(session)
		{
		}

		#endregion Constructor

		#region Public Methods

#if !SILVERLIGHT

		#region Synchronous Methods

        /// <summary>
        /// This method gives another application access to certain API calls on behalf of the application calling it. The application granted access is specified by permissions_apikey. Which methods or namespaces can be called are specified in method_arr.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// var method_arr = new List&lt;string&gt; { "admin" };
        /// var result = api.Permissions.GrantApiAccess(Constants.WebApplicationKey2, method_arr);
        /// </code>
        /// </example>
        /// <param name="apiKeyGrantedAccess">The API key for the application that is being granted access.</param>
        /// <param name="method_arr">JSON array of methods and/or namespaces for which the access is granted. If this is not specified, access to all allowed methods is granted.</param>
        /// <returns>The method returns a bool value indicating whether the call succeeded or failed. </returns>
        /// <remarks>The only namespace that can be granted access at this time is admin. </remarks>
        public bool GrantApiAccess(string apiKeyGrantedAccess, List<string> method_arr)
		{
			return GrantApiAccess(apiKeyGrantedAccess, method_arr, false, null, null);
		}

        /// <summary>
        /// This method returns the API methods to which access has been granted by the specified application. 
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// var result = api.Permissions.CheckAvailableApiAccess(Constants.WebApplicationKey2);
        /// </code>
        /// </example>
        /// <param name="permissions_apikey">The API key of the application for which the check is being done.</param>
        /// <returns>The method returns an array of strings listing all methods/namespaces for which access is available.</returns>
        public IList<string> CheckAvailableApiAccess(string permissions_apikey)
		{
			return CheckAvailableApiAccess(permissions_apikey, false, null, null);
		}

        /// <summary>
        /// This method revokes the API access granted to the specified application.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// var result = api.Permissions.RevokeApiAccess(Constants.WebApplicationKey2);
        /// </code>
        /// </example>
        /// <param name="permissions_apikey">The API key for the target application.</param>
        /// <returns>The method returns a bool value indicating whether the call succeeded or failed.</returns>
        public bool RevokeApiAccess(string permissions_apikey)
		{
			return RevokeApiAccess(permissions_apikey, false, null, null);
		}

        /// <summary>
        /// This method returns the API methods to which the specified application has been given access.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// var result = api.Permissions.CheckGrantedApiAccess(Constants.WebApplicationKey2);
        /// </code>
        /// </example>
        /// <param name="permissions_apikey">The API key of the application for which the check is being done.</param>
        /// <returns>The method returns a List of strings listing all methods/namespaces for which access has been granted.</returns>
        public IList<string> CheckGrantedApiAccess(string permissions_apikey)
		{
			return CheckGrantedApiAccess(permissions_apikey, false, null, null);
        }

        #endregion Synchronous Methods

#endif

        #region Asynchronous Methods

        /// <summary>
        /// This method gives another application access to certain API calls on behalf of the application calling it. The application granted access is specified by permissions_apikey. Which methods or namespaces can be called are specified in method_arr.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     api.Session.UserId = Constants.UserId;
        ///     var method_arr = new List&lt;string&gt; { "admin" };
        ///     api.Permissions.GrantApiAccessAsync(Constants.WebApplicationKey2, method_arr, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="apiKeyGrantedAccess">The API key for the application that is being granted access.</param>
        /// <param name="method_arr">JSON array of methods and/or namespaces for which the access is granted. If this is not specified, access to all allowed methods is granted.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The method returns a bool value indicating whether the call succeeded or failed. </returns>
        /// <remarks>The only namespace that can be granted access at this time is admin. </remarks>
        public bool GrantApiAccessAsync(string apiKeyGrantedAccess, List<string> method_arr, GrantApiAccessCallback callback, Object state)
		{
			return GrantApiAccess(apiKeyGrantedAccess, method_arr, true, callback, state);
		}

        /// <summary>
        /// This method returns the API methods to which access has been granted by the specified application. 
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     api.Permissions.CheckAvailableApiAccessAsync(Constants.WebApplicationKey2, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;string&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="permissions_apikey">The API key of the application for which the check is being done.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The method returns an array of strings listing all methods/namespaces for which access is available.</returns>
        public IList<string> CheckAvailableApiAccessAsync(string permissions_apikey, CheckAvailableApiAccessCallback callback, Object state)
		{
			return CheckAvailableApiAccess(permissions_apikey, true, callback, state);
		}

        /// <summary>
        /// This method revokes the API access granted to the specified application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     api.Permissions.RevokeApiAccessAsync(Constants.WebApplicationKey2, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="permissions_apikey">The API key for the target application.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The method returns a bool value indicating whether the call succeeded or failed.</returns>
        public bool RevokeApiAccessAsync(string permissions_apikey, RevokeApiAccessCallback callback, Object state)
		{
			return RevokeApiAccess(permissions_apikey, true, callback, state);
		}

        /// <summary>
        /// This method returns the API methods to which the specified application has been given access.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     api.Permissions.CheckGrantedApiAccessAsync(Constants.WebApplicationKey2, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;string&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="permissions_apikey">The API key of the application for which the check is being done.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The method returns a List of strings listing all methods/namespaces for which access has been granted.</returns>
        public IList<string> CheckGrantedApiAccessAsync(string permissions_apikey, CheckGrantedApiAccessCallback callback, Object state)
		{
			return CheckGrantedApiAccess(permissions_apikey, true, callback, state);
        }

        #endregion Asynchronous Methods

        /// <summary>
        /// Toggles Permissions Mode to Active.
        /// </summary>
        /// <param name="callAsApiKey"></param>
        public void BeginPermissionsMode(string callAsApiKey)
		{
			IsPermissionsModeActive = true;
			CallAsApiKey = callAsApiKey;
		}

        /// <summary>
        /// /// Toggles Permissions Mode to Inactive.
        /// </summary>
        /// <param name="callAsApiKey"></param>
		public void EndPermissionsMode(string callAsApiKey)
		{
			IsPermissionsModeActive = false;
			CallAsApiKey = null;
		}

		#endregion Public Methods
        
		#region Private Methods

		private bool GrantApiAccess(string apiKeyGrantedAccess, List<string> method_arr, bool isAsync, GrantApiAccessCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.permissions.grantApiAccess" } };
			Utilities.AddRequiredParameter(parameterList, "permissions_apikey", apiKeyGrantedAccess);
			Utilities.AddJSONArray(parameterList, "method_arr", method_arr);

			if (isAsync)
			{
                SendRequestAsync<permissions_grantApiAccess_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
				return true;
			}

            var response = SendRequest<permissions_grantApiAccess_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? true : response.TypedValue;
		}

		private IList<string> CheckAvailableApiAccess(string permissions_apikey, bool isAsync, CheckAvailableApiAccessCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.permissions.checkAvailableApiAccess" } };
			Utilities.AddRequiredParameter(parameterList, "permissions_apikey", permissions_apikey);

			if (isAsync)
			{
                SendRequestAsync<permissions_checkAvailableApiAccess_response, IList<string>>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<IList<string>>(callback), state, "permissions_checkAvailableApiAccess_response_elt");
				return null;
			}

            var response = SendRequest<permissions_checkAvailableApiAccess_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? null : response.permissions_checkAvailableApiAccess_response_elt;
		}

		private bool RevokeApiAccess(string permissions_apikey, bool isAsync, RevokeApiAccessCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.permissions.revokeApiAccess" } };
			Utilities.AddRequiredParameter(parameterList, "permissions_apikey", permissions_apikey);

			if (isAsync)
			{
                SendRequestAsync<permissions_revokeApiAccess_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
				return true;
			}

            var response = SendRequest<permissions_revokeApiAccess_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? true : response.TypedValue;
		}

		private IList<string> CheckGrantedApiAccess(string permissions_apikey, bool isAsync, CheckGrantedApiAccessCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.permissions.checkGrantedApiAccess" } };
			Utilities.AddRequiredParameter(parameterList, "permissions_apikey", permissions_apikey);

			if (isAsync)
			{
                SendRequestAsync<permissions_checkGrantedApiAccess_response, IList<string>>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<IList<string>>(callback), state, "permissions_checkGrantedApiAccess_response_elt");
				return null;
			}

            var response = SendRequest<permissions_checkGrantedApiAccess_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? null : response.permissions_checkGrantedApiAccess_response_elt;
		}

		#endregion Private Methods

		#endregion Methods

		#region Delegates

        /// <summary>
        /// Delegate called when GrantApiAccess call is completed.
        /// </summary>
        /// <param name="result">boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void GrantApiAccessCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when CheckAvailableApiAccess call is completed.
        /// </summary>
        /// <param name="accessibleMethods">IList of accessible method strings.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void CheckAvailableApiAccessCallback(IList<string> accessibleMethods, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when RevokeApiAccess call is completed.
        /// </summary>
        /// <param name="result">boolean result.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void RevokeApiAccessCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when CheckGrantedApiAccess call is completed.
        /// </summary>
        /// <param name="accessibleMethods">IList of accessible method strings.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void CheckGrantedApiAccessCallback(IList<string> accessibleMethods, Object state, FacebookException e);			

		#endregion Delegates
	}
}
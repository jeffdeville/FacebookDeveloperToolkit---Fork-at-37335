using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
	/// <summary>
	/// Facebook Application API methods.
	/// </summary>
	public class Application : RestBase, Facebook.Rest.IApplication
	{
		#region Methods

		#region Constructor

		/// <summary>
		/// Public constructor for facebook.Application
		/// </summary>
		/// <param name="session">Needs a connected Facebook Session object for making requests</param>
		public Application(IFacebookSession session)
			: base(session)
		{
		}

		#endregion Constructor

		#region Public Methods

#if !SILVERLIGHT

		#region Synchronous Methods

        /// <summary>
        /// Returns public information about a given application (not necessarily your own).
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// app_info result = api.Application.GetPublicInfo();
        /// </code>
        /// </example>
        /// <returns>This method returns public information for an application.</returns>
		public app_info GetPublicInfo()
		{
			return GetPublicInfo(null, Session.ApplicationKey, null);
		}

        /// <summary>
        /// Returns public information about a given application (not necessarily your own).
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// app_info result = api.Application.GetPublicInfo(null, Constants.WebApplicationKey, null);
        /// </code>
        /// </example>
        /// <param name="application_api_key">API key of the desired application. You must specify exactly one of application_id, application_api_key or application_canvas_name.</param>
        /// <param name="application_canvas_name">Canvas page name of the desired application. You must specify exactly one of application_id, application_api_key or application_canvas_name.</param>
        /// <param name="application_id">Application ID of the desired application. You must specify exactly one of application_id, application_api_key or application_canvas_name.</param>
        /// <returns>This method returns public information for an application.</returns>
        public app_info GetPublicInfo(long? application_id, string application_api_key, string application_canvas_name)
		{
			return GetPublicInfo(application_id, application_api_key, application_canvas_name, false, null, null);
		}

		#endregion

#endif

		#region Asynchronous Methods

        /// <summary>
        /// Returns public information about a given application (not necessarily your own).
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     api.Application.GetPublicInfoAsync(AsyncDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncDemoCompleted(app_info result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns public information for an application.</returns>
        public app_info GetPublicInfoAsync(GetPublicInfoCallback callback, Object state)
		{
			return GetPublicInfoAsync(null, Session.ApplicationKey, null, callback, state);
		}

        /// <summary>
        /// Returns public information about a given application (not necessarily your own).
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     api.Application.GetPublicInfoAsync(null, Constants.WebApplicationKey, null, AsyncDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncDemoCompleted(app_info result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="application_api_key">API key of the desired application. You must specify exactly one of application_id, application_api_key or application_canvas_name.</param>
        /// <param name="application_canvas_name">Canvas page name of the desired application. You must specify exactly one of application_id, application_api_key or application_canvas_name.</param>
        /// <param name="application_id">Application ID of the desired application. You must specify exactly one of application_id, application_api_key or application_canvas_name.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns public information for an application.</returns>
        public app_info GetPublicInfoAsync(long? application_id, string application_api_key, string application_canvas_name, GetPublicInfoCallback callback, Object state)
		{
			return GetPublicInfo(application_id, application_api_key, application_canvas_name, true, callback, state);
		}

		#endregion

		#endregion Public Methods
        
		#region Private Methods

		/// <summary>
		/// Returns public information for an application (as shown in the application directory) by either application ID, API key, or canvas page name. 
		/// </summary>
		/// <param name="application_id">Application ID of the desired application. You must specify exactly one of application_id, application_api_key or application_canvas_name. </param>
		/// <param name="application_api_key">API key of the desired application. You must specify exactly one of application_id, application_api_key or application_canvas_name. </param>
		/// <param name="application_canvas_name">Canvas page name of the desired application. You must specify exactly one of application_id, application_api_key or application_canvas_name. </param>
        /// <param name="isAsync">Indicator if current call is async or sync</param>
        /// <param name="callback">The async callback to use if the call is async</param>
        /// <param name="state">Object state to populate for use by async callback</param>
        /// <returns>app_info object</returns>
		private app_info GetPublicInfo(long? application_id, string application_api_key, string application_canvas_name, bool isAsync, GetPublicInfoCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.application.getPublicInfo" } };
			Utilities.AddOptionalParameter(parameterList, "application_id", application_id);
			Utilities.AddOptionalParameter(parameterList, "application_api_key", application_api_key);
			Utilities.AddOptionalParameter(parameterList, "application_canvas_name", application_canvas_name);

			if (isAsync)
			{
                SendRequestAsync<application_getPublicInfo_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<application_getPublicInfo_response>(callback), state);
				return null;
			}

            return SendRequest<application_getPublicInfo_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
		}

		#endregion Private Methods
        
		#endregion Methods

		#region Delegates

        /// <summary>
        /// Call back used with Async call to GetProfileInfo completes
        /// </summary>
        public delegate void GetPublicInfoCallback(app_info info, Object state, FacebookException e);

		#endregion Delegates
	}
}
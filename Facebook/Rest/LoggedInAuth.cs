using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
	public class LoggedInAuth : AuthorizedRestBase, ILoggedInAuth
	{
		public LoggedInAuth(ApplicationInfo appInfo, SessionInfo session) : base(appInfo, session)
		{
		}

		/// <summary>
		/// Expires the session indicated in the API call, for your application.
		/// </summary>
		/// <example>
		/// <code>
		/// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
		/// var result = api.Auth.ExpireSession();
		/// </code>
		/// </example>
		/// <returns>If the invalidation is successful, this will return true.</returns>
		public bool ExpireSession()
		{
			return ExpireSession(false, null, null);
		}

		/// <summary>
		/// If this method is called for the logged in user, then no further API calls can be made on that user's behalf until the user decides to authorize the application again.
		/// </summary>
		/// <example>
		/// <code>
		/// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
		/// api.Auth.RevokeAuthorization();
		/// </code>
		/// </example>
		/// <returns>If the revoke is successful, this will return true.</returns>
		public void RevokeAuthorization()
		{
			RevokeAuthorization(false, null, null);
		}

		/// <summary>
		/// Removes a specific extended permission that a user explicitly granted to your application.
		/// </summary>
		/// <example>
		/// <code>
		/// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
		/// api.Auth.RevokeExtendedPermission(Enums.ExtendedPermissions.create_event);
		/// </code>
		/// </example>
		/// <param name="ext_perm">The extended permission to revoke.</param>
		/// <returns>This method returns true upon success.</returns>
		public bool RevokeExtendedPermission(Enums.ExtendedPermissions ext_perm)
		{
			return RevokeExtendedPermission(ext_perm, 0);
		}

		/// <summary>
		/// Removes a specific extended permission that a user explicitly granted to your application.
		/// </summary>
		/// <example>
		/// <code>
		/// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
		/// api.Auth.RevokeExtendedPermission(Enums.ExtendedPermissions.create_event, Constants.UserId);
		/// </code>
		/// </example>
		/// <param name="ext_perm">The extended permission to revoke.</param>
		/// <param name="uid">The user ID of the user whose extended permission you want to revoke. If you don't specify this parameter, then you must have a valid session for the current user, and that session's user will have the specified permission revoked.</param>
		/// <returns>This method returns true upon success.</returns>
		public bool RevokeExtendedPermission(Enums.ExtendedPermissions ext_perm, long uid)
		{
			return RevokeExtendedPermission(ext_perm, uid, false, null, null);
		}

		/// <summary>
		/// Expires the session indicated in the API call, for your application.
		/// </summary>
		/// <example>
		/// <code>
		/// private static void RunDemoAsync()
		/// {
		///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
		///     api.Auth.ExpireSessionAsync(AsyncDemoCompleted, null);
		/// }
		///
		/// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
		/// {
		///     var actual = result;
		/// }
		/// </code>
		/// </example>
		/// <param name="callback">The AsyncCallback delegate</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>        
		/// <returns>If the invalidation is successful, this will return true.</returns>
		public void ExpireSessionAsync(ExpireSessionCallback callback, Object state)
		{
			ExpireSession(true, callback, state);
		}

		/// <summary>
		/// If this method is called for the logged in user, then no further API calls can be made on that user's behalf until the user decides to authorize the application again.
		/// </summary>
		/// <example>
		/// <code>
		/// private static void RunDemoAsync()
		/// {
		///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
		///     api.Auth.RevokeAuthorizationAsync(AsyncDemoCompleted, null);
		/// }
		/// 
		/// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
		/// {
		///     var actual = result;
		/// }
		/// </code>
		/// </example>
		/// <param name="callback">The AsyncCallback delegate</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>        
		/// <returns>If the revoke is successful, this will return true.</returns>
		public void RevokeAuthorizationAsync(RevokeAuthorizationCallback callback, Object state)
		{
			RevokeAuthorization(true, callback, state);
		}

		/// <summary>
		/// Removes a specific extended permission that a user explicitly granted to your application.
		/// </summary>
		/// <example>
		/// <code>
		/// private static void RunDemoAsync()
		/// {
		///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
		///     api.Auth.RevokeExtendedPermissionAsync(Enums.ExtendedPermissions.create_event, AsyncDemoCompleted, null);
		/// }
		///
		/// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
		/// {
		///     var actual = result;
		/// }
		/// </code>
		/// </example>
		/// <param name="ext_perm">The extended permission to revoke.</param>
		/// <param name="callback">The AsyncCallback delegate</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>
		/// <returns>This method returns true upon success.</returns>
		public void RevokeExtendedPermissionAsync(Enums.ExtendedPermissions ext_perm, RevokeExtendedPermissionCallback callback, Object state)
		{
			RevokeExtendedPermissionAsync(ext_perm, -1, callback, state);
		}

		/// <summary>
		/// Begins an async request to to revoke extended permission. See the facebook 
		/// guide for more information
		/// </summary>
		/// <example>
		/// <code>
		/// private static void RunDemoAsync()
		/// {
		///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
		///     api.Auth.RevokeExtendedPermissionAsync(Enums.ExtendedPermissions.create_event, Constants.UserId, AsyncDemoCompleted, null);
		/// }
		///
		/// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
		/// {
		///     var actual = result;
		/// }
		/// </code>
		/// </example>
		/// <param name="ext_perm">The extended permission to revoke.</param>
		/// <param name="uid">The user ID of the user whose extended permission you want to revoke. If you don't specify this parameter, then you must have a valid session for the current user, and that session's user will have the specified permission revoked.</param>
		/// <param name="callback">The AsyncCallback delegate</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>        
		/// <returns>This method returns true upon success.</returns>
		public void RevokeExtendedPermissionAsync(Enums.ExtendedPermissions ext_perm, long uid, RevokeExtendedPermissionCallback callback, Object state)
		{
			RevokeExtendedPermission(ext_perm, uid, true, callback, state);
		}

		private bool ExpireSession(bool isAsync, ExpireSessionCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.auth.expireSession" } };

			if (isAsync)
			{
				SendRequestAsync<auth_expireSession_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return true;
			}

			var response = SendRequest<auth_expireSession_response>(parameterList);
			return response == null ? true : response.TypedValue;
		}

		private void RevokeAuthorization(bool isAsync, RevokeAuthorizationCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.auth.revokeAuthorization" } };

			if (isAsync)
			{
				SendRequestAsync<auth_revokeAuthorization_response, bool>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey), new FacebookCallCompleted<bool>(callback), state);
				return;
			}

			SendRequest(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey));
		}

		private bool RevokeExtendedPermission(Enums.ExtendedPermissions ext_perm, long uid, bool isAsync, RevokeExtendedPermissionCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.auth.revokeExtendedPermission" } };
			Utilities.AddRequiredParameter(parameterList, "perm", ext_perm.ToString());
			Utilities.AddOptionalParameter(parameterList, "uid", uid);

			if (isAsync)
			{
				SendRequestAsync<auth_revokeExtendedPermission_response, bool>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey), new FacebookCallCompleted<bool>(callback), state);
				return true;
			}

			var response = SendRequest<auth_revokeExtendedPermission_response>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey));
			return response == null ? true : response.TypedValue;
		}

		/// <summary>
		/// Delegate called when ExpireSession call completed
		/// </summary>
		/// <param name="result">result of operation</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>
		/// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void ExpireSessionCallback(bool result, Object state, FacebookException e);

		/// <summary>
		/// Delegate called when RevokeAuthorization call completed
		/// </summary>
		/// <param name="result">result of operation</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>
		/// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void RevokeAuthorizationCallback(bool result, Object state, FacebookException e);

		/// <summary>
		/// Delegate called when RevokeExtendedPermission call completed
		/// </summary>
		/// <param name="result">result of operation</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>
		/// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void RevokeExtendedPermissionCallback(bool result, Object state, FacebookException e);

	}
}
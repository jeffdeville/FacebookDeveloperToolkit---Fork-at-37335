using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
    /// <summary>
    /// Facebook Users API methods.
    /// </summary>
    public class Users : RestBase, Facebook.Rest.IUsers
    {
        #region Methods

        #region Constructor

        /// <summary>
        /// Public constructor for facebook.Users
        /// </summary>
        /// <param name="session">Needs a connected Facebook Session object for making requests</param>
        public Users(IFacebookSession session)
            : base(session)
        {
        }

        #endregion Constructor

        #region Public Methods
    
#if !SILVERLIGHT

        #region Synchronous Methods
        
        /// <summary>
        /// Returns a wide array of user-specific information for each user identifier passed, limited by the view of the current user.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// api.Users.GetInfo();
        /// </code>
        /// </example>
        /// <returns>The user info elements returned are those friends visible to the Facebook Platform. If no visible users are found from the passed uids argument, the method will return an empty result element.</returns>
        public user GetInfo()
        {
            return GetInfo(Session.UserId);
        }

    	/// <summary>
        /// Returns a wide array of user-specific information for each user identifier passed, limited by the view of the current user.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var actual = api.Users.GetInfo(Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="uid">A user ID.</param>
        /// <returns>The user info elements returned are those friends visible to the Facebook Platform. If no visible users are found from the passed uids argument, the method will return an empty result element.</returns>
        public user GetInfo(long uid)
        {
            var users = GetInfo(uid.ToString());
            return users.Count > 0 ? users[0] : null;
        }

        /// <summary>
        /// Returns a wide array of user-specific information for each user identifier passed, limited by the view of the current user.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var uid = new List&lt;long&gt;() { 658517591, 824555570 };
        /// var actual = api.Users.GetInfo(uid);
        /// </code>
        /// </example>
        /// <param name="uids">This is a List of user IDs.</param>
        /// <returns>The user info elements returned are those friends visible to the Facebook Platform. If no visible users are found from the passed uids argument, the method will return an empty result element.</returns>
        public IList<user> GetInfo(IList<long> uids)
        {
            return GetInfo(StringHelper.ConvertToCommaSeparated(uids));
        }

        /// <summary>
        /// Returns an array of user-specific information for use by the application itself.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// string uids = Constants.UserId.ToString();
        /// var result = api.Users.GetStandardInfo(uids);
        /// </code>
        /// </example>
        /// <param name="uids">A comma-separated list of user IDs.</param>
        /// <returns>The user info elements returned are those friends visible to the Facebook Platform.</returns>
        public IList<user> GetStandardInfo(string uids)
        {
            return GetStandardInfo(uids, null);
        }

        /// <summary>
        /// Returns an array of user-specific information for use by the application itself.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// List&lt;long&gt; uids = new List&lt;long&gt; { Constants.UserId };
        /// var result = api.Users.GetStandardInfo(uids);
        /// </code>
        /// </example>
        /// <param name="uids">A List of user IDs.</param>
        /// <returns>The user info elements returned are those friends visible to the Facebook Platform.</returns>
		public IList<user> GetStandardInfo(List<long> uids)
        {
            return GetStandardInfo(StringHelper.ConvertToCommaSeparated(uids), null);
        }

        /// <summary>
        /// Returns an array of user-specific information for use by the application itself.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// List&lt;long&gt; uids = new List&lt;long&gt; { Constants.UserId };
        /// List&lt;string&gt; fields = new List&lt;string&gt; { "uid", "first_name", "last_name", "name", "profile_url", "proxied_email" };
        /// var result = api.Users.GetStandardInfo(uids, fields);
        /// </code>
        /// </example>
        /// <param name="uids">A List of user IDs.</param>
        /// <param name="fields">List of desired fields in return. This is a List of field strings and is limited to these entries only: uid, first_name, last_name, name, timezone, birthday, sex, affiliations (regional type only), locale, profile_url, proxied_email.</param>
        /// <returns>The user info elements returned are those friends visible to the Facebook Platform.</returns>
        public IList<user> GetStandardInfo(List<long> uids, IList<string> fields)
        {
            return GetStandardInfo(StringHelper.ConvertToCommaSeparated(uids), fields);
        }

        /// <summary>
        /// Returns an array of user-specific information for use by the application itself.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// string uids = Constants.UserId.ToString();
        /// List&lt;string&gt; fields = new List&lt;string&gt; { "uid", "first_name", "last_name", "name", "profile_url", "proxied_email" };
        /// var result = api.Users.GetStandardInfo(uids, fields);
        /// </code>
        /// </example>
        /// <param name="uids">A comma-separated list of user IDs.</param>
        /// <param name="fields">List of desired fields in return. This is a List of field strings and is limited to these entries only: uid, first_name, last_name, name, timezone, birthday, sex, affiliations (regional type only), locale, profile_url, proxied_email.</param>
        /// <returns>The user info elements returned are those friends visible to the Facebook Platform.</returns>
        public IList<user> GetStandardInfo(string uids, IList<string> fields)
        {
            return GetStandardInfo(uids, fields, false, null, null);
        }

        /// <summary>
        /// Gets the user ID (uid) associated with the current session.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Users.GetLoggedInUser();
        /// </code>
        /// </example>
        /// <returns>This method returns the user ID (uid) associated with the current session.</returns>
        public long GetLoggedInUser()
        {
            return GetLoggedInUser(false, null, null);
        }

        /// <summary>
        /// Checks whether the user has opted in to an extended application permission.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Users.HasAppPermission(Enums.ExtendedPermissions.email);
        /// </code>
        /// </example>
        /// <param name="ext_perm">String identifier for the extended permission that is being checked for. Must be one of email, read_stream, publish_stream, offline_access, status_update, photo_upload, {create_event, rsvp_event, sms, video_upload, create_note, share_item.</param>
        /// <returns>Returns true or false.</returns>
		public bool HasAppPermission(Enums.ExtendedPermissions ext_perm)
        {
            return HasAppPermission(ext_perm, 0);
        }

        /// <summary>
        /// Checks whether the user has opted in to an extended application permission.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Users.HasAppPermission(Enums.ExtendedPermissions.email, Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="ext_perm">String identifier for the extended permission that is being checked for. Must be one of email, read_stream, publish_stream, offline_access, status_update, photo_upload, {create_event, rsvp_event, sms, video_upload, create_note, share_item.</param>
        /// <param name="uid">The user ID of the user whose permissions you are checking. If this parameter is not specified, then it defaults to the session user.  Note: This parameter applies only to Web applications. Facebook ignores this parameter if it is passed by a desktop application.</param>
        /// <returns>Returns true or false.</returns>
        public bool HasAppPermission(Enums.ExtendedPermissions ext_perm, long uid)
        {
            return HasAppPermission(ext_perm, uid, false, null, null);
        }

        /// <summary>
        /// Returns whether the user (either the session user or user specified by UID) has authorized the calling application.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Users.IsAppUser(Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="uid">The user ID of the user who may have authorized the application. If this parameter is not specified, then it defaults to the session user.  Note: This parameter applies only to Web applications. Facebook ignores this parameter if it is passed by a desktop application.</param>
        /// <returns>Returns true or false.</returns>
        public bool IsAppUser(long uid)
        {
            return IsAppUser(uid, false, null, null);
        }

        /// <summary>
        /// Returns whether the user is a verified Facebook user.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Users.IsVerified(Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="uid">The user ID to verify.</param>
        /// <returns>This call returns true for a verified user; false for an non-verified user.</returns>
        /// <remarks>This method is currently broken on Facebook's side (it only returns an empty XML element). A bug report has been filed here: http://bugs.developers.facebook.com/show_bug.cgi?id=5129 .</remarks>
		public bool IsVerified(long uid)
        {
            return IsVerified(uid, false, null, null);
        }

        /// <summary>
        /// Updates a user's Facebook status.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Users.SetStatus("I'm a code sample.");
        /// </code>
        /// </example>
        /// <param name="status">The status message to set.  Note: The maximum message length is 255 characters; messages longer than that limit will be truncated and appended with '...'.</param>
        /// <returns>This method returns true on success, false on failure, or an error code.</returns>
        public bool SetStatus(string status)
        {
            return SetStatus(status, false, 0);
        }

        /// <summary>
        /// Updates a user's Facebook status.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Users.SetStatus("I'm a code sample.", Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="status">The status message to set.  Note: The maximum message length is 255 characters; messages longer than that limit will be truncated and appended with '...'.</param>
        /// <param name="uid">The user ID of the user whose status you are setting. If this parameter is not specified, then it defaults to the session user.  Note: This parameter applies only to Web applications.  Facebook ignores this parameter if it is passed by a desktop application.</param>
        /// <returns>This method returns true on success, false on failure, or an error code.</returns>
        public bool SetStatus(string status, long uid)
        {
            return SetStatus(status, false, uid);
        }

        /// <summary>
        /// Updates a user's Facebook status.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Users.SetStatus("I'm a code sample.", true);
        /// </code>
        /// </example>
        /// <param name="status">The status message to set.  Note: The maximum message length is 255 characters; messages longer than that limit will be truncated and appended with '...'.</param>
        /// <param name="status_includes_verb">If set to true, the word "is" will not be prepended to the status message.</param>
        /// <returns>This method returns true on success, false on failure, or an error code.</returns>
        public bool SetStatus(string status, bool status_includes_verb)
        {
            return SetStatus(status, status_includes_verb, 0);
        }

        /// <summary>
        /// Updates a user's Facebook status.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Users.SetStatus("I'm a code sample.", true, Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="status">The status message to set.  Note: The maximum message length is 255 characters; messages longer than that limit will be truncated and appended with '...'.</param>
        /// <param name="status_includes_verb">If set to true, the word "is" will not be prepended to the status message.</param>
        /// <param name="uid">The user ID of the user whose status you are setting. If this parameter is not specified, then it defaults to the session user.  Note: This parameter applies only to Web applications.  Facebook ignores this parameter if it is passed by a desktop application.</param>
        /// <returns>This method returns true on success, false on failure, or an error code.</returns>
        public bool SetStatus(string status, bool status_includes_verb, long uid)
        {
        	return SetStatus(status, status_includes_verb, uid, false, null, null);
        }

        #endregion Synchronous Methods

#endif

        #region Asynchronous Methods

        /// <summary>
        /// Returns a wide array of user-specific information for each user identifier passed, limited by the view of the current user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Users.GetInfoAsync(AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;user&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The user info elements returned are those friends visible to the Facebook Platform. If no visible users are found from the passed uids argument, the method will return an empty result element.</returns>
        public void GetInfoAsync(GetInfoCallback callback, Object state)
        {
            GetInfoAsync(Session.UserId, callback, state);
        }

        /// <summary>
        /// Returns a wide array of user-specific information for each user identifier passed, limited by the view of the current user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Users.GetInfoAsync(Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;user&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">A user ID.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The user info elements returned are those friends visible to the Facebook Platform. If no visible users are found from the passed uids argument, the method will return an empty result element.</returns>
        public void GetInfoAsync(long uid, GetInfoCallback callback, Object state)
		{
			GetInfoAsync(uid.ToString(), callback, state);
		}

        /// <summary>
        /// Returns a wide array of user-specific information for each user identifier passed, limited by the view of the current user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     var uids = new List&lt;long&gt; { 658517591, 824555570 }; 
        ///     api.Users.GetInfoAsync(AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;user&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uids">This is a List of user IDs.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The user info elements returned are those friends visible to the Facebook Platform. If no visible users are found from the passed uids argument, the method will return an empty result element.</returns>
        public void GetInfoAsync(IList<long> uids, GetInfoCallback callback, Object state)
        {
            GetInfo(StringHelper.ConvertToCommaSeparated(uids), true, callback, state);
        }

        /// <summary>
        /// Returns a wide array of user-specific information for each user identifier passed, limited by the view of the current user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     string uids = "658517591,824555570"; 
        ///     api.Users.GetInfoAsync(uids, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;user&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uids">A comma-separated list of user IDs.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The user info elements returned are those friends visible to the Facebook Platform. If no visible users are found from the passed uids argument, the method will return an empty result element.</returns>
        public void GetInfoAsync(string uids, GetInfoCallback callback, Object state)
		{
			GetInfo(uids, true, callback, state);
		}

        ///  <summary>
        ///  Gets the user ID (uid) associated with the current session.
        ///  </summary>
        ///  <example>
        ///  <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Users.GetLoggedInUserAsync(AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(long result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns the user ID (uid) associated with the current session.</returns>
        public void GetLoggedInUserAsync(GetLoggedInUserCallback callback, Object state)
        {
            GetLoggedInUser(true, callback, state);
        }

        /// <summary>
        /// Returns an array of user-specific information for use by the application itself.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     List&lt;long&gt; uids = new List&lt;long&gt; { Constants.UserId };
        ///     api.Users.GetStandardInfoAsync(uids, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;user&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uids">A List of user IDs.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The user info elements returned are those friends visible to the Facebook Platform.</returns>
        public void GetStandardInfoAsync(List<long> uids, GetStandardInfoCallback callback, Object state)
        {
            GetStandardInfoAsync(uids, null, callback, state);
        }

        /// <summary>
        /// Returns an array of user-specific information for use by the application itself.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     List&lt;long&gt; uids = new List&lt;long&gt; { Constants.UserId };
        ///     List&lt;string&gt; fields = new List&lt;string&gt; { "uid", "first_name", "last_name", "name", "profile_url", "proxied_email" };
        ///     api.Users.GetStandardInfoAsync(uids, fields, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;user&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uids">A List of user IDs.</param>
        /// <param name="fields">List of desired fields in return. This is a List of field strings and is limited to these entries only: uid, first_name, last_name, name, timezone, birthday, sex, affiliations (regional type only), locale, profile_url, proxied_email.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The user info elements returned are those friends visible to the Facebook Platform.</returns>
        public void GetStandardInfoAsync(List<long> uids, IList<string> fields, GetStandardInfoCallback callback, Object state)
		{
			GetStandardInfo(StringHelper.ConvertToCommaSeparated(uids), fields, true, callback, state);
		}

        /// <summary>
        /// Returns an array of user-specific information for use by the application itself.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     string uids = Constants.UserId.ToString();
        ///     api.Users.GetStandardInfoAsync(uids, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;user&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uids">A comma-separated list of user IDs.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The user info elements returned are those friends visible to the Facebook Platform.</returns>
        public void GetStandardInfoAsync(string uids, GetStandardInfoCallback callback, Object state)
		{
			GetStandardInfoAsync(uids, null, callback, state);
		}

        /// <summary>
        /// Returns an array of user-specific information for use by the application itself.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     string uids = Constants.UserId.ToString();
        ///     List&lt;string&gt; fields = new List&lt;string&gt; { "uid", "first_name", "last_name", "name", "profile_url", "proxied_email" };
        ///     api.Users.GetStandardInfoAsync(uids, fields, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;user&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uids">A comma-separated list of user IDs.</param>
        /// <param name="fields">List of desired fields in return. This is a List of field strings and is limited to these entries only: uid, first_name, last_name, name, timezone, birthday, sex, affiliations (regional type only), locale, profile_url, proxied_email.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The user info elements returned are those friends visible to the Facebook Platform.</returns>
        public void GetStandardInfoAsync(string uids, IList<string> fields, GetStandardInfoCallback callback, Object state)
		{
			GetStandardInfo(uids, fields, true, callback, state);
		}

        /// <summary>
        /// Checks whether the user has opted in to an extended application permission.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Users.HasAppPermissionAsync(Enums.ExtendedPermissions.email, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="ext_perm">String identifier for the extended permission that is being checked for. Must be one of email, read_stream, publish_stream, offline_access, status_update, photo_upload, {create_event, rsvp_event, sms, video_upload, create_note, share_item.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>Returns true or false.</returns>
        public void HasAppPermissionAsync(Enums.ExtendedPermissions ext_perm, HasAppPermissionCallback callback, Object state)
        {
            HasAppPermissionAsync(ext_perm, 0, callback, state);
        }

        /// <summary>
        /// Checks whether the user has opted in to an extended application permission.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Users.HasAppPermissionAsync(Enums.ExtendedPermissions.email, Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="ext_perm">String identifier for the extended permission that is being checked for. Must be one of email, read_stream, publish_stream, offline_access, status_update, photo_upload, {create_event, rsvp_event, sms, video_upload, create_note, share_item.</param>
        /// <param name="uid">The user ID of the user whose permissions you are checking. If this parameter is not specified, then it defaults to the session user.  Note: This parameter applies only to Web applications. Facebook ignores this parameter if it is passed by a desktop application.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>Returns true or false.</returns>
        public void HasAppPermissionAsync(Enums.ExtendedPermissions ext_perm, long uid, HasAppPermissionCallback callback, Object state)
		{
			HasAppPermission(ext_perm, uid, true, callback, state);
		}

        /// <summary>
        /// Returns whether the user (either the session user or user specified by UID) has authorized the calling application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Users.IsAppUserAsync(Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">The user ID of the user who may have authorized the application. If this parameter is not specified, then it defaults to the session user.  Note: This parameter applies only to Web applications. Facebook ignores this parameter if it is passed by a desktop application.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>Returns true or false.</returns>
        public void IsAppUserAsync(long uid, IsAppUserCallback callback, Object state)
        {
            IsAppUser(uid, true, callback, state);
        }

        /// <summary>
        /// Returns whether the user is a verified Facebook user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Users.IsVerifiedAsync(Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">The user ID to verify.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This call returns true for a verified user; false for an non-verified user.</returns>
        /// <remarks>This method is currently broken on Facebook's side (it only returns an empty XML element). A bug report has been filed here: http://bugs.developers.facebook.com/show_bug.cgi?id=5129 .</remarks>
		public void IsVerifiedAsync(long uid, IsVerifiedCallback callback, Object state)
		{
			IsVerified(uid, true, callback, state);
		}

        /// <summary>
        /// Updates a user's Facebook status.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Users.SetStatusAsync("I'm an async code sample.", AsyncDemoCompleted, null);
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
        public bool SetStatusAsync(string status, SetStatusCallback callback, Object state)
		{
			return SetStatusAsync(status, false, callback, state);
		}

        /// <summary>
        /// Updates a user's Facebook status.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Users.SetStatusAsync("I'm an async code sample.", Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="status">The status message to set.  Note: The maximum message length is 255 characters; messages longer than that limit will be truncated and appended with '...'.</param>
        /// <param name="uid">The user ID of the user whose status you are setting. If this parameter is not specified, then it defaults to the session user.  Note: This parameter applies only to Web applications.  Facebook ignores this parameter if it is passed by a desktop application.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true on success, false on failure, or an error code.</returns>
        public bool SetStatusAsync(string status, long uid, SetStatusCallback callback, Object state)
		{
			return SetStatusAsync(status, false, uid, callback, state);
		}

        /// <summary>
        /// Updates a user's Facebook status.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Users.SetStatusAsync("I'm an async code sample.", true, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="status">The status message to set.  Note: The maximum message length is 255 characters; messages longer than that limit will be truncated and appended with '...'.</param>
        /// <param name="status_includes_verb">If set to true, the word "is" will not be prepended to the status message.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true on success, false on failure, or an error code.</returns>
        public bool SetStatusAsync(string status, bool status_includes_verb, SetStatusCallback callback, Object state)
		{
			return SetStatusAsync(status, status_includes_verb, 0, callback, state);
		}

        /// <summary>
        /// Updates a user's Facebook status.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Users.SetStatusAsync("I'm an async code sample.", true, Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="status">The status message to set.  Note: The maximum message length is 255 characters; messages longer than that limit will be truncated and appended with '...'.</param>
        /// <param name="status_includes_verb">If set to true, the word "is" will not be prepended to the status message.</param>
        /// <param name="uid">The user ID of the user whose status you are setting. If this parameter is not specified, then it defaults to the session user.  Note: This parameter applies only to Web applications.  Facebook ignores this parameter if it is passed by a desktop application.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true on success, false on failure, or an error code.</returns>
        public bool SetStatusAsync(string status, bool status_includes_verb, long uid, SetStatusCallback callback, Object state)
		{
			return SetStatus(status, status_includes_verb, uid, true, callback, state);
		}

        #endregion Asynchronous Methods

        #endregion Public Methods

        #region Internal Methods

        /// <summary>
        /// Returns a wide array of user-specific information for each user identifier passed, limited by the view of the current user.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// string uids = "658517591,824555570";
        /// var actual = api.Users.GetInfo(uid);
        /// </code>
        /// </example>
        /// <param name="uids">This is a string of user IDs.</param>
        /// <returns>The user info elements returned are those friends visible to the Facebook Platform. If no visible users are found from the passed uids argument, the method will return an empty result element.</returns>
        internal IList<user> GetInfo(string uids)
        {
            return GetInfo(uids, false, null, null);
        }

        #endregion Internal Methods
        
        #region Private Methods
        
        private IList<user> GetInfo(string uids, bool isAsync, GetInfoCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.users.getInfo" } };
            Utilities.AddOptionalParameter(parameterList, "uids", uids);
            if (string.IsNullOrEmpty(Session.SessionKey))
            {
                Utilities.AddRequiredParameter(parameterList, "fields",
                                  "first_name, last_name, name, pic_square, affiliations, locale");
            }
            else
            {
                Utilities.AddRequiredParameter(parameterList, "fields",
                                  "uid, about_me, activities, affiliations, birthday, birthday_date, books, current_location, education_history, email_hashes, first_name, hometown_location, hs_info, interests, is_app_user, last_name, locale, meeting_for, meeting_sex, movies, music, name, notes_count, pic, pic_with_logo, pic_big, pic_big_with_logo, pic_small, pic_small_with_logo, pic_square, pic_square_with_logo, political, profile_blurb, profile_update_time, profile_url, proxied_email, quotes, relationship_status, religion, sex, significant_other_id, status, timezone, tv, wall_count, work_history");
            }
            
            if (isAsync)
            {
                SendRequestAsync<users_getInfo_response, IList<user>>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<IList<user>>(callback), state, "user");
                return null;
            }

			var response = SendRequest<users_getInfo_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? null : response.user;
        }

        private IList<user> GetStandardInfo(string uids, IList<string> fields, bool isAsync, GetStandardInfoCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.users.getStandardInfo" } };
            Utilities.AddOptionalParameter(parameterList, "uids", uids);
            if (fields != null)
            {
                Utilities.AddRequiredParameter(parameterList, "fields", StringHelper.ConvertToCommaSeparated(fields));
            }
            else
            {
                Utilities.AddRequiredParameter(parameterList, "fields", "uid, first_name, last_name, name, timezone, birthday, sex, affiliations, proxied_email");
            }

            if (isAsync)
            {
                SendRequestAsync<users_getStandardInfo_response, IList<user>>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<IList<user>>(callback), state, "standard_user_info");
                return null;
            }

            var response = SendRequest<users_getStandardInfo_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? null : response.standard_user_info;
        }

        private long GetLoggedInUser(bool isAsync, GetLoggedInUserCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.users.getLoggedInUser" } };

            if (isAsync)
            {
                SendRequestAsync<users_getLoggedInUser_response, long>(parameterList, new FacebookCallCompleted<long>(callback), state); 
               return 0;
            }

			var response = SendRequest<users_getLoggedInUser_response>(parameterList);
			return response == null ? 0 : response.TypedValue;
        }

        private bool HasAppPermission(Enums.ExtendedPermissions ext_perm, long uid, bool isAsync, HasAppPermissionCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string>
			                    	{
			                    		{"method", "facebook.users.hasAppPermission"},
			                    		{"ext_perm", ext_perm.ToString()}
			                    	};
            Utilities.AddOptionalParameter(parameterList, "uid", uid);

            if (isAsync)
            {
                SendRequestAsync<users_hasAppPermission_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
                return false;
            }

            var response = SendRequest<users_hasAppPermission_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? true : response.TypedValue;
        }

        private bool IsAppUser(long uid, bool isAsync, IsAppUserCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.users.isAppUser" } };
            Utilities.AddOptionalParameter(parameterList, "uid", uid);

            if (isAsync)
            {
                SendRequestAsync<users_isAppUser_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
                return false;
            }

            var response = SendRequest<users_isAppUser_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? true : response.TypedValue;
        }

        private bool IsVerified(long uid, bool isAsync, IsVerifiedCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.users.isVerified" } };
            Utilities.AddOptionalParameter(parameterList, "uid", uid);

            if (isAsync)
            {
				SendRequestAsync<users_isVerified_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
                return false;
            }

			var response = SendRequest<users_isVerified_response>(parameterList);
			return response == null ? true : response.TypedValue;
        }

        private bool SetStatus(string status, bool status_includes_verb, long uid, bool isAsync, SetStatusCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string>
			                    	{
			                    		{"method", "facebook.users.setStatus"},
			                    		{"status", status},
			                    		{"status_includes_verb", status_includes_verb.ToString()}
			                    	};
            Utilities.AddOptionalParameter(parameterList, "uid", uid);

            if (isAsync)
            {
				SendRequestAsync<users_setStatus_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
                return true;
            }

            var response = SendRequest<users_setStatus_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? true : response.TypedValue;
        }

        #endregion Private Methods
        
        #endregion Methods

        #region Delegates

        /// <summary>
        /// Delegate called when GetUserInfo call is completed.
        /// </summary>
        /// <param name="users">List of user objects</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetInfoCallback(IList<user> users, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetUserInfo call is completed.
        /// </summary>
        /// <param name="userId">user ID of the logged in user</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetLoggedInUserCallback(long userId, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetStandardInfo call is completed.
        /// </summary>
        /// <param name="users">List of user objects</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetStandardInfoCallback(IList<user> users, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when HasAppPermission call is completed.
        /// </summary>
        /// <param name="hasPermission">set to true if user has permission</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void HasAppPermissionCallback(bool hasPermission, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when IsAppUser call is completed.
        /// </summary>
        /// <param name="isAppUser">set to true if user is app user</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void IsAppUserCallback(bool isAppUser, Object state, FacebookException e);

		/// <summary>
		/// Delegate called when IsVerified call is completed.
		/// </summary>
		/// <param name="isVerified">True if the user has been verified</param>
		/// <param name="state">An object containing state informatoin for this asynchronous request.</param>
		/// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void IsVerifiedCallback(bool isVerified, Object state, FacebookException e);

		/// <summary>
		/// Delegate called when SetStatus call is completed.
		/// </summary>
		/// <param name="result">True if the operation completed, false otherwise.</param>
		/// <param name="state">An object containing state informatoin for this asynchronous request.</param>
		/// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void SetStatusCallback(bool result, Object state, FacebookException e);

        #endregion Delegates
    }
}

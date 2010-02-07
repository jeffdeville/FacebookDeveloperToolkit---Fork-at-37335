using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
	/// <summary>
	/// Facebook Comments API methods.
	/// </summary>
	public class Comments : RestBase, Facebook.Rest.IComments
	{
		#region Methods

		#region Constructor

		/// <summary>
		/// Public constructor for facebook.Comments
		/// </summary>
		/// <param name="session">Needs a connected Facebook Session object for making requests</param>
		public Comments(IFacebookSession session)
			: base(session)
		{
		}

		#endregion Constructor

		#region Public Methods

#if !SILVERLIGHT

		#region Synchronous Methods

		/// <summary>
        /// Returns all comments for a given xid posted through fb:comments. This method is a wrapper for the FQL query on the comment FQL table.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Comments.Get("xid_test");
        /// </code>
        /// </example>
        /// <param name="xid">The comment xid that you want to retrieve. For a Comments Box, you can determine the xid on the admin panel or in the application settings editor in the Facebook Developer application.</param>
        /// <returns>This method returns all comments for a given xid.</returns>
        public IList<comment> Get(string xid)
		{
			return Get(xid, false, null, null);
		}

        /// <summary>
        /// Adds a comment for a given xid on behalf of a user. Calls with a session secret may only act on behalf of the session user.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Comments.Add("xid_test", "test");
        /// </code>
        /// </example>
        /// <param name="xid">The xid of a particular Comments Box or fb:comments.</param>
        /// <param name="text">The comment/text to be added, as inputted by a user.</param>
        /// <returns>This call returns a comment_id if the comment was added successfully, or an error code if the call was unsuccessful.</returns>
		public int Add(string xid, string text)
		{
			return Add(xid, text, -1);
		}

        /// <summary>
        /// Adds a comment for a given xid on behalf of a user. Calls with a session secret may only act on behalf of the session user.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Comments.Add("xid_test", "test", Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="xid">The xid of a particular Comments Box or fb:comments.</param>
        /// <param name="text">The comment/text to be added, as inputted by a user.</param>
        /// <param name="uid">The user ID to add a comment on behalf of. This defaults to the session user and must only be the session user if using a session secret (example: Desktop and JSCL apps).</param>
        /// <returns>This call returns a comment_id if the comment was added successfully, or an error code if the call was unsuccessful.</returns>
        public int Add(string xid, string text, long uid)
		{
			return Add(xid, text, uid, null, null, false);
		}

        /// <summary>
        /// Adds a comment for a given xid on behalf of a user. Calls with a session secret may only act on behalf of the session user.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Comments.Add("xid_test", "test", Constants.UserId, "comment title", "http://www.facebook.com", false);
        /// </code>
        /// </example>
        /// <param name="xid">The xid of a particular Comments Box or fb:comments.</param>
        /// <param name="text">The comment/text to be added, as inputted by a user.</param>
        /// <param name="uid">The user ID to add a comment on behalf of. This defaults to the session user and must only be the session user if using a session secret (example: Desktop and JSCL apps).</param>
        /// <param name="title">The title associated with the item the user is commenting on. This is required if publishing a feed story as it provides the text of the permalink to give context to the user's comment.</param>
        /// <param name="url">The url associated with the item the user is commenting on. This is required if publishing a feed story as it is the permalink associated with the comment.</param>
        /// <param name="publish_to_stream">Whether a feed story should be published about this comment. This defaults to false and can only be 'true' if the user has granted the publish_stream extended permission.</param>
        /// <returns>This call returns a comment_id if the comment was added successfully, or an error code if the call was unsuccessful.</returns>
        public int Add(string xid, string text, long uid, string title, string url, bool publish_to_stream)
		{
			return Add(xid, text, uid, title, url, publish_to_stream, false, null, null);
		}

        /// <summary>
        /// Removes a comment for a given xid by comment_id. Calls with a session secret may only act on behalf of the session user.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Comments.Remove("xid_test", 174967);
        /// </code>
        /// </example>
        /// <param name="xid">The xid of a particular Comments Box or fb:comments.</param>
        /// <param name="comment_id">The comment_id, as returned by Comments.add or Comments.get, to be removed.</param>
        /// <returns>This call returns true if the comment was removed successfully, or an error code if the call was unsuccessful.</returns>
		public bool Remove(string xid, int comment_id)
		{
			return Remove(xid, comment_id, true);
		}

        /// <summary>
        /// Removes a comment for a given xid by comment_id. Calls with a session secret may only act on behalf of the session user.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Comments.Remove("xid_test", 174967, true);
        /// </code>
        /// </example>
        /// <param name="xid">The xid of a particular Comments Box or fb:comments.</param>
        /// <param name="comment_id">The comment_id, as returned by Comments.add or Comments.get, to be removed.</param>
        /// <param name="useSession">Flag to determine if current session should be passed to API.</param>
        /// <returns>This call returns true if the comment was removed successfully, or an error code if the call was unsuccessful.</returns>
		public bool Remove(string xid, int comment_id, bool useSession)
		{
			return Remove(xid, comment_id, useSession, false, null, null);
        }

        #endregion Synchronous Methods

#endif

        #region Asynchronous Methods

        /// <summary>
        /// Returns all comments for a given xid posted through fb:comments. This method is a wrapper for the FQL query on the comment FQL table.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Comments.GetAsync("xid_test", AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;comment&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="xid">The comment xid that you want to retrieve. For a Comments Box, you can determine the xid on the admin panel or in the application settings editor in the Facebook Developer application.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns all comments for a given xid.</returns>
        public void GetAsync(string xid, GetCallback callback, Object state)
		{
			Get(xid, true, callback, state);
		}

        /// <summary>
        /// Adds a comment for a given xid on behalf of a user. Calls with a session secret may only act on behalf of the session user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Comments.AddAsync("xid_test", "test", AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(int result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="xid">The xid of a particular Comments Box or fb:comments.</param>
        /// <param name="text">The comment/text to be added, as inputted by a user.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This call returns a comment_id if the comment was added successfully, or an error code if the call was unsuccessful.</returns>
        public void AddAsync(string xid, string text, AddCallback callback, Object state)
		{
			AddAsync(xid, text, 0, callback, state);
		}

        /// <summary>
        /// Adds a comment for a given xid on behalf of a user. Calls with a session secret may only act on behalf of the session user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Comments.AddAsync("xid_test", "test", Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(int result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="xid">The xid of a particular Comments Box or fb:comments.</param>
        /// <param name="text">The comment/text to be added, as inputted by a user.</param>
        /// <param name="uid">The user ID to add a comment on behalf of. This defaults to the session user and must only be the session user if using a session secret (example: Desktop and JSCL apps).</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This call returns a comment_id if the comment was added successfully, or an error code if the call was unsuccessful.</returns>
        public void AddAsync(string xid, string text, long uid, AddCallback callback, Object state)
		{
			AddAsync(xid, text, uid, null, null, true, callback, state);
		}

        /// <summary>
        /// Adds a comment for a given xid on behalf of a user. Calls with a session secret may only act on behalf of the session user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Comments.AddAsync("xid_test", "test", Constants.UserId, "comment title", "http://www.facebook.com", false, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(int result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="xid">The xid of a particular Comments Box or fb:comments.</param>
        /// <param name="text">The comment/text to be added, as inputted by a user.</param>
        /// <param name="uid">The user ID to add a comment on behalf of. This defaults to the session user and must only be the session user if using a session secret (example: Desktop and JSCL apps).</param>
        /// <param name="title">The title associated with the item the user is commenting on. This is required if publishing a feed story as it provides the text of the permalink to give context to the user's comment.</param>
        /// <param name="url">The url associated with the item the user is commenting on. This is required if publishing a feed story as it is the permalink associated with the comment.</param>
        /// <param name="publish_to_stream">Whether a feed story should be published about this comment. This defaults to false and can only be 'true' if the user has granted the publish_stream extended permission.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This call returns a comment_id if the comment was added successfully, or an error code if the call was unsuccessful.</returns>
        public void AddAsync(string xid, string text, long uid, string title, string url, bool publish_to_stream, AddCallback callback, Object state)
		{
			Add(xid, text, uid, title, url, publish_to_stream, true, callback, state);
		}

        /// <summary>
        /// Removes a comment for a given xid by comment_id. Calls with a session secret may only act on behalf of the session user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Comments.RemoveAsync("xid_test", 174967, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="xid">The xid of a particular Comments Box or fb:comments.</param>
        /// <param name="comment_id">The comment_id, as returned by Comments.add or Comments.get, to be removed.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This call returns true if the comment was removed successfully, or an error code if the call was unsuccessful.</returns>
		public void RemoveAsync(string xid, int comment_id, RemoveCallback callback, Object state)
		{
			RemoveAsync(xid, comment_id, true, callback, state);
		}

        /// <summary>
        /// Removes a comment for a given xid by comment_id. Calls with a session secret may only act on behalf of the session user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Comments.RemoveAsync("xid_test", 174967, true, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="xid">The xid of a particular Comments Box or fb:comments.</param>
        /// <param name="comment_id">The comment_id, as returned by Comments.add or Comments.get, to be removed.</param>
        /// <param name="useSession">Flag to determine if current session should be passed to API.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This call returns true if the comment was removed successfully, or an error code if the call was unsuccessful.</returns>
		public void RemoveAsync(string xid, int comment_id, bool useSession, RemoveCallback callback, Object state)
		{
			Remove(xid, comment_id, useSession, true, callback, state);
        }

        #endregion Asynchronous Methods

        #endregion Public Methods

        #region Private Methods
        
		private IList<comment> Get(string xid, bool isAsync, GetCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.comments.get" } };
			Utilities.AddRequiredParameter(parameterList, "xid", xid);

			if (isAsync)
			{
				SendRequestAsync<comments_get_response, IList<comment>>(parameterList, new FacebookCallCompleted<IList<comment>>(callback), state, "comment");
				return null;
			}

			var response = SendRequest<comments_get_response>(parameterList);
			return response == null ? null : response.comment;
		}
		
		private int Add(string xid, string text, long uid, string title, string url, bool publish_to_stream, bool isAsync, AddCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.comments.add" } };
			Utilities.AddRequiredParameter(parameterList, "xid", xid);
			Utilities.AddRequiredParameter(parameterList, "text", text);
			Utilities.AddOptionalParameter(parameterList, "uid", uid);
			Utilities.AddOptionalParameter(parameterList, "title", title);
			Utilities.AddOptionalParameter(parameterList, "url", url);
			if (publish_to_stream)
			{
				Utilities.AddOptionalParameter(parameterList, "publish_to_stream", publish_to_stream.ToString());
			}

			if (isAsync)
			{
				SendRequestAsync<comments_add_response, int>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<int>(callback), state);
				return 0;
			}

            var response = SendRequest<comments_add_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? 0 : response.TypedValue;
		}
		
		private bool Remove(string xid, int comment_id, bool useSession, bool isAsync, RemoveCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.comments.remove" } };
			Utilities.AddRequiredParameter(parameterList, "xid", xid);
			Utilities.AddRequiredParameter(parameterList, "comment_id", comment_id);

			if (isAsync)
			{
				SendRequestAsync<comments_remove_response, bool>(parameterList, useSession, new FacebookCallCompleted<bool>(callback), state);
				return true;
			}

			var response = SendRequest<comments_remove_response>(parameterList, useSession);
			return response == null ? true : response.TypedValue;
			//return string.IsNullOrEmpty(response) || XmlSerializer.Deserialize<comments_remove_response>(response).TypedValue;
		}

		#endregion Private Methods

		#endregion Methods

		#region Delegates

		///<summary>
		///</summary>
		///<param name="comments"></param>
		///<param name="state"></param>
		///<param name="e"></param>
		public delegate void GetCallback(IList<comment> comments, Object state, FacebookException e);
		
        ///<summary>
		///</summary>
		///<param name="comment_id"></param>
		///<param name="state"></param>
		///<param name="e"></param>
		public delegate void AddCallback(int comment_id, Object state, FacebookException e);
		
        ///<summary>
		///</summary>
		///<param name="result"></param>
		///<param name="state"></param>
		///<param name="e"></param>
		public delegate void RemoveCallback(bool result, Object state, FacebookException e);

		#endregion Delegates
	}
}
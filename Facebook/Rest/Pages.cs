using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
	/// <summary>
	/// Facebook Pages API methods.
	/// </summary>
	public class Pages : RestBase, Facebook.Rest.IPages
	{
		#region Methods

		#region Constructor

		/// <summary>
		/// Public constructor for facebook.Pages
		/// </summary>
		/// <param name="session">Needs a connected Facebook Session object for making requests</param>
		public Pages(IFacebookSession session)
			: base(session)
		{
		}

		#endregion Constructor

		#region Public Methods

#if !SILVERLIGHT

		#region Synchronous Methods

        /// <summary>
        /// Returns all visible pages to the filters specified.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// List&lt;string&gt; fields = api.Pages.GetFields();
        /// List&lt;long&gt; pageIds = new List&lt;long&gt; { Constants.PageId };
        /// var result = api.Pages.GetInfo(fields, pageIds, Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="fields">List of desired fields in return.</param>
        /// <param name="page_ids">List of page IDs.</param>
        /// <param name="uid">The ID of the user. Defaults to the logged in user if the session_key is valid, and no page_ids are passed. Used to get the pages a given user is a fan of.</param>
        /// <returns>The page info elements returned are those visible to the Facebook Platform.</returns>
        public IList<page> GetInfo(List<string> fields, List<long> page_ids, long? uid)
		{
			return GetInfo(fields, page_ids, uid, false, null, null);
		}

        /// <summary>
        /// Checks whether the Page has added the application.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Pages.IsAppAdded(Constants.PageId);
        /// </code>
        /// </example>
        /// <param name="page_id">The ID of the Facebook Page.</param>
        /// <returns>Returns true or false.</returns>
        public bool IsAppAdded(long page_id)
		{
			return IsAppAdded(page_id, false, null, null);
		}

        /// <summary>
        /// Checks whether the logged-in user is the admin for a given Page.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Pages.IsAdmin(Constants.PageId);
        /// </code>
        /// </example>
        /// <param name="page_id">The ID of the Facebook Page.</param>
        /// <returns>Returns true or false.</returns>
        public bool IsAdmin(long page_id)
		{
			return IsAdmin(page_id, false, null, null);
		}

        /// <summary>
        /// Checks whether a user is a fan of a given Page.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Pages.IsFan(Constants.PageId);
        /// </code>
        /// </example>
        /// <param name="page_id">The ID of the page.</param>
        /// <returns>Returns true or false.</returns>
        public bool IsFan(long page_id)
		{
			return IsFan(page_id, Session.UserId, false, null, null);
		}

        /// <summary>
        /// Checks whether a user is a fan of a given Page.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Pages.IsFan(Constants.PageId, Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="page_id">The ID of the page.</param>
        /// <param name="uid">The ID of the user. Defaults to the logged-in user if not set.</param>
        /// <returns>Returns true or false.</returns>
        public bool IsFan(long page_id, long uid)
		{
			return IsFan(page_id, uid, false, null, null);
        }

        #endregion Synchronous Methods

#endif

        #region Asynchronous Methods

        /// <summary>
        /// Returns all visible pages to the filters specified.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     List&lt;string&gt; fields = api.Pages.GetFields();
        ///     List&lt;long&gt; pageIds = new List&lt;long&gt; { Constants.PageId };
        ///     api.Pages.GetInfoAsync(fields, pageIds, Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;page&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="fields">List of desired fields in return.</param>
        /// <param name="page_ids">List of page IDs.</param>
        /// <param name="uid">The ID of the user. Defaults to the logged in user if the session_key is valid, and no page_ids are passed. Used to get the pages a given user is a fan of.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The page info elements returned are those visible to the Facebook Platform.</returns>
        public void GetInfoAsync(List<string> fields, List<long> page_ids, long? uid, GetInfoCallback callback, Object state)
		{
			GetInfo(fields, page_ids, uid, true, callback, state);
		}

        /// <summary>
        /// Checks whether the logged-in user is the admin for a given Page.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Pages.IsAdminAsync(Constants.PageId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="page_id">The ID of the Facebook Page.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>Returns true or false.</returns>
        public void IsAdminAsync(long page_id, IsAdminCallback callback, Object state)
		{
			IsAdmin(page_id, true, callback, state);
		}

        /// <summary>
        /// Checks whether the Page has added the application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Pages.IsAppAddedAsync(Constants.PageId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="page_id">The ID of the Facebook Page.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>Returns true or false.</returns>
        public void IsAppAddedAsync(long page_id, IsAppAddedCallback callback, Object state)
		{
			IsAppAdded(page_id, true, callback, state);
		}

        /// <summary>
        /// Checks whether a user is a fan of a given Page.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Pages.IsFanAsync(Constants.PageId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="page_id">The ID of the page.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>Returns true or false.</returns>
        public void IsFanAsync(long page_id, IsFanCallback callback, Object state)
		{
			IsFanAsync(page_id, Session.UserId, callback, state);
		}

        /// <summary>
        /// Checks whether a user is a fan of a given Page.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Pages.IsFanAsync(Constants.PageId, Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="page_id">The ID of the page.</param>
        /// <param name="uid">The ID of the user. Defaults to the logged-in user if not set.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>Returns true or false.</returns>
        public void IsFanAsync(long page_id, long uid, IsFanCallback callback, Object state)
		{
			IsFan(page_id, uid, true, callback, state);
		}

        #endregion Asynchronous Methods

        /// <summary>
        /// Retrieves hardcoded List of fields names.
        /// </summary>
        /// <returns>This method returns a List of field strings.</returns>
        public List<string> GetFields()
		{
			return new List<string>
            {
                "page_id",
                "name",
                "pic_small",
                "pic_square",
                "pic_big",
                "pic",
                "pic_large",
                "type",
                "website",
                "location",
                "hours",
                "band_members",
                "bio",
                "hometown",
                "genre",
                "record_label",
                "influences",
                "has_added_app",
                "founded",
                "company_overview",
                "mission",
                "products",
                "release_date",
                "starring",
                "written_by",
                "directed_by",
                "produced_by",
                "studio",
                "awards",
                "plot_outline",
                "network",
                "season",
                "schedule"};

		}

		#endregion Public Methods
        
		#region Private Methods

		private IList<page> GetInfo(List<string> fields, List<long> page_ids, long? uid, bool isAsync, GetInfoCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.pages.getInfo" } };
			Utilities.AddList(parameterList, "fields", fields);
			Utilities.AddList(parameterList, "page_ids", page_ids);
			Utilities.AddOptionalParameter(parameterList, "uid", uid);

			if (isAsync)
			{
            	SendRequestAsync<pages_getInfo_response, IList<page>>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<IList<page>>(callback), state, "page");
				return null;
			}

			var response = SendRequest<pages_getInfo_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? null : response.page;
		}

		private bool IsAppAdded(long page_id, bool isAsync, IsAppAddedCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.pages.isAppAdded" } };
			Utilities.AddOptionalParameter(parameterList, "page_id", page_id);

			if (isAsync)
			{
                SendRequestAsync<pages_isAppAdded_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
				return true;
			}

            var response = SendRequest<pages_isAppAdded_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? true : response.TypedValue;
		}

		private bool IsAdmin(long page_id, bool isAsync, IsAdminCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.pages.isAdmin" } };
			Utilities.AddRequiredParameter(parameterList, "page_id", page_id);

			if (isAsync)
			{
				SendRequestAsync<pages_isAdmin_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return true;
			}

			var response = SendRequest<pages_isAdmin_response>(parameterList);
			return response == null ? true : response.TypedValue;
		}

		private bool IsFan(long page_id, long uid, bool isAsync, IsFanCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.pages.isFan" } };
			Utilities.AddOptionalParameter(parameterList, "page_id", page_id);
			Utilities.AddOptionalParameter(parameterList, "uid", uid);

			if (isAsync)
			{
				SendRequestAsync<pages_isFan_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return true;
			}

			var response = SendRequest<pages_isFan_response>(parameterList);
			return response == null ? true : response.TypedValue;
		}

		#endregion Private Methods

		#endregion Methods

		#region Delegates

        /// <summary>
        /// Delegate called when GetInfo call is completed.
        /// </summary>
        /// <param name="pages">list of page objects</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetInfoCallback(IList<page> pages, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when IsAppAdded call is completed.
        /// </summary>
        /// <param name="result">boolean result</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void IsAppAddedCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when IsAdmin call is completed.
        /// </summary>
        /// <param name="result">boolean result</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void IsAdminCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when IsFan call is completed.
        /// </summary>
        /// <param name="result">boolean result</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void IsFanCallback(bool result, Object state, FacebookException e);

		#endregion Delegates
	}
}
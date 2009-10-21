using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
	/// <summary>
	/// Facebook Links API methods.
	/// </summary>
	public class Links : RestBase, Facebook.Rest.ILinks
	{
		#region Methods

		#region Constructor

		/// <summary>
		/// Public constructor for facebook.Links
		/// </summary>
		/// <param name="session">Needs a connected Facebook Session object for making requests</param>
		public Links(IFacebookSession session)
			: base(session)
		{
		}

		#endregion Constructor

		#region Public Methods

#if !SILVERLIGHT

		#region Synchronous Methods

        /// <summary>
        /// Returns all links the user has posted on their profile through your application.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Links.Get();
        /// </code>
        /// </example>
        /// <returns>This method returns an List of link data.</returns>
        /// <remarks>For desktop applications, don't specify a uid; keep the default. (Default value is the logged-in user.)</remarks>
        public IList<link> Get()
        {
            return Get(0, false, null, null);
        }

        /// <summary>
        /// Returns all links the user has posted on their profile through your application.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Links.Get(Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="uid">he user ID of the user whose links you want to retrieve.</param>
        /// <returns>This method returns an List of link data.</returns>
        /// <remarks>For desktop applications, don't specify a uid; keep the default. (Default value is the logged-in user.)</remarks>
        public IList<link> Get(long uid)
		{
			return Get(uid, false, null, null);
		}

        /// <summary>
        /// Lets a user post a link on their Wall through your application.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Links.Post(Constants.UserId, new Uri("http://www.bing.com"), "I'm checking out Bing.");
        /// </code>
        /// </example>
        /// <param name="uid">The user ID of the user posting the link.</param>
        /// <param name="url">The URL for the link.</param>
        /// <param name="comment">The comment the user included with the link.</param>
        /// <returns>This method returns a link_id if successful.</returns>
        public long Post(long uid, Uri url, string comment)
		{
			return Post(uid, url, comment, false, null, null);
        }

        #endregion Synchronous Methods

#endif

        #region Asynchronous Methods

        /// <summary>
        /// Returns all links the user has posted on their profile through your application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Links.GetAsync(AsyncDemoCompleted, null);
        /// } 
        ///
        /// private static void AsyncDemoCompleted(IList&lt;link&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns an List of link data.</returns>
        /// <remarks>For desktop applications, don't specify a uid; keep the default. (Default value is the logged-in user.)</remarks>
        public void GetAsync(GetCallback callback, Object state)
        {
            Get(0, true, callback, state);
        }

        /// <summary>
        /// Returns all links the user has posted on their profile through your application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Links.GetAsync(Constants.UserId, AsyncDemoCompleted, null);
        /// } 
        ///
        /// private static void AsyncDemoCompleted(IList&lt;link&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">he user ID of the user whose links you want to retrieve.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns an List of link data.</returns>
        /// <remarks>For desktop applications, don't specify a uid; keep the default. (Default value is the logged-in user.)</remarks>
        public void GetAsync(long uid, GetCallback callback, Object state)
		{
			Get(uid, true, callback, state);
		}

        /// <summary>
        /// Lets a user post a link on their Wall through your application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Links.PostAsync(Constants.UserId, new Uri("http://www.bing.com"), "I'm checking out Bing.", AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(long result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">The user ID of the user posting the link.</param>
        /// <param name="url">The URL for the link.</param>
        /// <param name="comment">The comment the user included with the link.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns a link_id if successful.</returns>
		public long PostAsync(long uid, Uri url, string comment, PostCallback callback, Object state)
		{
			return Post(uid, url, comment, true, callback, state);
        }

        #endregion Asynchronous Methods

        #endregion Public Methods

        #region Private Methods

		private IList<link> Get(long uid, bool isAsync, GetCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.links.get" } };
			Utilities.AddOptionalParameter(parameterList, "uid", uid);

			if (isAsync)
			{
			    SendRequestAsync<links_get_response, IList<link>>(parameterList, new FacebookCallCompleted<IList<link>>(callback), state, "link");
                return null;
            }

			var response = SendRequest<links_get_response>(parameterList);
			return response == null ? null : response.link;
		}

		private long Post(long uid, Uri url, string comment, bool isAsync, PostCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.links.post" } };
			Utilities.AddRequiredParameter(parameterList, "uid", uid);
			Utilities.AddRequiredParameter(parameterList, "url", url.ToString());
			Utilities.AddRequiredParameter(parameterList, "comment", comment);

			if (isAsync)
			{
				SendRequestAsync<links_post_response, long>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<long>(callback), state);
				return 0;
			}

			var response = SendRequest<links_post_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? -1 : response.TypedValue;
		}

		#endregion Private Methods

		#endregion Methods

		#region Delegates

		/// <summary>
		/// Delegate called when GetLinks call is completed.
		/// </summary>
		/// <param name="links">array of links object</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>
		/// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void GetCallback(IList<link> links, Object state, FacebookException e);

		/// <summary>
		/// Delegate called when PostLinks call is completed.
		/// </summary>
		/// <param name="linkId">ID of the link posted</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>
		/// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void PostCallback(long linkId, Object state, FacebookException e);

		#endregion Delegates
	}
}

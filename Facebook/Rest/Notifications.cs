using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
	/// <summary>
	/// Facebook Notifications API methods.
	/// </summary>
	public class Notifications : RestBase, INotifications
	{
		#region Methods

		#region Constructor

		/// <summary>
		/// Public constructor for facebook.Notifications
		/// </summary>
		/// <param name="session">Needs a connected Facebook Session object for making requests</param>
		public Notifications(IFacebookSession session)
			: base(session)
		{
		}

		#endregion Constructor

		#region Public Methods

#if !SILVERLIGHT

		#region Synchronous Methods

		/// <summary>
		/// Returns a list of all of the visible notes written by the specified user.
		/// </summary>
		/// <example>
		/// <code>
		/// FacebookApi api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
		/// var result = api.Notifications.Get();
		/// </code>
		/// </example>
		/// <returns>This method returns the same set of subelements, whether or not there are outstanding notifications in any area. Note that if the unread subelement value is 0 for any of the pokes or shares elements, the most_recent element is also 0. Otherwise, the most_recent element contains an identifier for the most recent notification of the enclosing type.</returns>
		public notifications Get()
		{
			return Get(false, null, null);
		}

		/// <summary>
		/// Sends a notification to a set of users.
		/// </summary>
		/// <example>
		/// <code>
		/// List&lt;long&gt; notificationList = new List&lt;long&gt;();
		/// notificationList.Add(Constants.UserId);
		/// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
		/// var result = api.Notifications.Send(notificationList, "my notification from samples app");
		/// </code>
		/// </example>
		/// <param name="to_ids">List recipient IDs. These must be either friends of the logged-in user or people who have added your application. To send a notification to the current logged-in user without a name prepended to the message, set to_ids to empty List.</param>
		/// <param name="notification">The content of the notification. The notification uses a stripped down version of FBML and HTML, allowing only text and links (see the list of allowed tags). The notification can contain up to 2,000 characters.</param>
		/// <returns>This returns a list of user ids for whom notifications were successfully sent. We will throw an error if an error occurred.</returns>
		/// <remarks>The notification parameter is a very stripped-down set of FBML which allows only tags that result in just text and links.</remarks>
		public string Send(List<long> to_ids, string notification)
		{
			return Send(to_ids, notification, null);
		}

		/// <summary>
		/// Sends a notification to a set of users.
		/// </summary>
		/// <example>
		/// <code>
		/// List&lt;long&gt; notificationList = new List&lt;long&gt;();
		/// notificationList.Add(Constants.UserId);
		/// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
		/// var result = api.Notifications.Send(notificationList, "my notification from samples app", "app_to_user");
		/// </code>
		/// </example>
		/// <param name="to_ids">List recipient IDs. These must be either friends of the logged-in user or people who have added your application. To send a notification to the current logged-in user without a name prepended to the message, set to_ids to empty List.</param>
		/// <param name="notification">The content of the notification. The notification uses a stripped down version of FBML and HTML, allowing only text and links (see the list of allowed tags). The notification can contain up to 2,000 characters.</param>
		/// <param name="type">Specify whether the notification is a user_to_user one or an app_to_user. (Default value is user_to_user.)</param>
		/// <returns>This returns a list of user ids for whom notifications were successfully sent. We will throw an error if an error occurred.</returns>
		/// <remarks>The notification parameter is a very stripped-down set of FBML which allows only tags that result in just text and links.</remarks>
		public string Send(List<long> to_ids, string notification, string type)
		{
			return Send(StringHelper.ConvertToCommaSeparated(to_ids), notification, type, false, null, null);
		}

		/// <summary>
		/// Sends a notification to a set of users.
		/// </summary>
		/// <example>
		/// <code>
		/// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
		/// var result = api.Notifications.Send(Constants.UserId.ToString(), "my notification from samples app");
		/// </code>
		/// </example>
		/// <param name="to_ids">Comma-separated list of recipient IDs. These must be either friends of the logged-in user or people who have added your application. To send a notification to the current logged-in user without a name prepended to the message, set to_ids to empty List.</param>
		/// <param name="notification">The content of the notification. The notification uses a stripped down version of FBML and HTML, allowing only text and links (see the list of allowed tags). The notification can contain up to 2,000 characters.</param>
		/// <returns>This returns a list of user ids for whom notifications were successfully sent. We will throw an error if an error occurred.</returns>
		/// <remarks>The notification parameter is a very stripped-down set of FBML which allows only tags that result in just text and links.</remarks>
		public string Send(string to_ids, string notification)
		{
			return Send(to_ids, notification, null);
		}

		/// <summary>
		/// Sends a notification to a set of users.
		/// </summary>
		/// <example>
		/// <code>
		/// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
		/// var result = api.Notifications.Send(Constants.UserId.ToString(), "my notification from samples app", "app_to_user");
		/// </code>
		/// </example>
		/// <param name="to_ids">List recipient IDs. These must be either friends of the logged-in user or people who have added your application. To send a notification to the current logged-in user without a name prepended to the message, set to_ids to empty List.</param>
		/// <param name="notification">The content of the notification. The notification uses a stripped down version of FBML and HTML, allowing only text and links (see the list of allowed tags). The notification can contain up to 2,000 characters.</param>
		/// <param name="type">Specify whether the notification is a user_to_user one or an app_to_user. (Default value is user_to_user.)</param>
		/// <returns>This returns a list of user ids for whom notifications were successfully sent. We will throw an error if an error occurred.</returns>
		/// <remarks>The notification parameter is a very stripped-down set of FBML which allows only tags that result in just text and links.</remarks>
		public string Send(string to_ids, string notification, string type)
		{
			return Send(to_ids, notification, type, false, null, null);
		}

		/// <summary>
		/// This method gets all the current session user's notifications, as well as data for the applications that generated those notifications. It is a wrapper around the notification and application FQL tables; you can achieve more fine-grained control by using those two FQL tables in conjunction with the fql.multiquery API call. 
		/// Applications must pass a valid session key. 
		/// </summary>
		/// <param name="start_time">Indicates the earliest time to return a notification. This equates to the updated_time field in the notification FQL table. If not specified, this call returns all available notifications. </param>
		/// <param name="include_read">Indicates whether to include notifications that have already been read. By default, notifications a user has read are not included. </param>
		/// <returns>alerts and apps.</returns>
		public notification_data GetList(DateTime? start_time, bool include_read)
		{
			return GetList(start_time, include_read, false, null, null);
		}

		/// <summary>
		/// This method marks one or more notifications as read. You return the notifications by calling notifications.getList or querying the notification FQL table. 
		/// Applications must pass a valid session key, and can only mark the notifications of the current session user. 
		/// </summary>
		/// <param name="notification_ids">List of notification ids</param>
		/// <returns>if successful</returns>
		public bool MarkRead(List<long> notification_ids)
		{
			return MarkRead(notification_ids, false, null, null);
		}

		/// <summary>
		/// Sends an email to the specified users who have the application.
		/// </summary>
		/// <example>
		/// <code>
		/// List&lt;long&gt; notificationList = new List&lt;long&gt;();
		/// notificationList.Add(Constants.UserId);
		/// string fbml = string.Format("&lt;fb:switch&gt;&lt;fb:profile-pic uid=\"{0}\" /&gt;&lt;fb:default&gt;Unable to show profile pic&lt;/fb:default&gt;&lt;/fb:switch&gt;", Constants.UserId);
		/// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
		/// 
		/// // This action will require the user to have granted this app "email" Extended Permission.
		/// //var result = api.Notifications.SendEmail(notificationList, "email test", "here is plain text", string.Empty);
		/// var result = api.Notifications.SendEmail(notificationList, "email test", string.Empty, fbml);
		/// </code>
		/// </example>
		/// <param name="recipients">A List of recipient IDs. The recipients must be people who have already added your application. You can email up to 100 people at a time.</param>
		/// <param name="subject">The subject of the email message. As of 10/28/2008, the subject will accept a limited set of FBML tags, including names, and tags related to internationalization.</param>
		/// <param name="plainText">The plain text version of the email content. You must include a non-empty value for at least one of either the fbml or text parameters. The FBML input takes precedence, but if the given FBML value is invalid or cannot be rendered, then the text will be used instead. There is currently no character limit on the length of either the text or FBML body.</param>
		/// <param name="fbml">The FBML version of the email. You must include a non-empty value for at least one of either the fbml or text parameters. The fbml parameter is a stripped-down set of FBML that allows only HTML/FBML tags that result in text, links, linebreaks, as well as tags related to internationalization.</param>
		/// <returns>This returns a comma-separated list of user ids for whom notifications were successfully sent. We will throw an error if an error occurred.</returns>
		public string SendEmail(List<long> recipients, string subject, string plainText, string fbml)
		{
			return SendEmail(StringHelper.ConvertToCommaSeparated(recipients), subject, plainText, fbml);
		}

		/// <summary>
		/// Sends an email to the specified users who have the application.
		/// </summary>
		/// <example>
		/// <code>
		/// string fbml = string.Format("&lt;fb:switch&gt;&lt;fb:profile-pic uid=\"{0}\" /&gt;&lt;fb:default&gt;Unable to show profile pic&lt;/fb:default&gt;&lt;/fb:switch&gt;", Constants.UserId);
		/// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
		///
		/// // This action will require the user to have granted this app "email" Extended Permission.
		/// //var result = api.Notifications.SendEmail(Constants.UserId.ToString(), "email test", "here is plain text", string.Empty);
		/// var result = api.Notifications.SendEmail(Constants.UserId.ToString(), "email test", string.Empty, fbml);
		/// </code>
		/// </example>
		/// <param name="recipients">A comma-separated list of recipient IDs. The recipients must be people who have already added your application. You can email up to 100 people at a time.</param>
		/// <param name="subject">The subject of the email message. As of 10/28/2008, the subject will accept a limited set of FBML tags, including names, and tags related to internationalization.</param>
		/// <param name="plainText">The plain text version of the email content. You must include a non-empty value for at least one of either the fbml or text parameters. The FBML input takes precedence, but if the given FBML value is invalid or cannot be rendered, then the text will be used instead. There is currently no character limit on the length of either the text or FBML body.</param>
		/// <param name="fbml">The FBML version of the email. You must include a non-empty value for at least one of either the fbml or text parameters. The fbml parameter is a stripped-down set of FBML that allows only HTML/FBML tags that result in text, links, linebreaks, as well as tags related to internationalization.</param>
		/// <returns>This returns a comma-separated list of user ids for whom notifications were successfully sent. We will throw an error if an error occurred.</returns>
		public string SendEmail(string recipients, string subject, string plainText, string fbml)
		{
			return SendEmail(recipients, subject, plainText, fbml, false, null, null);
		}

		#endregion Synchronous Methods

#endif

		#region Asynchronous Methods

		/// <summary>
		/// Returns a list of all of the visible notes written by the specified user.
		/// </summary>
		/// <example>
		/// <code>
		/// private static void RunDemoAsync()
		/// {
		///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
		///     api.Notifications.GetAsync(AsyncDemoCompleted, null);
		/// }
		///
		/// private static void AsyncDemoCompleted(notifications result, Object state, FacebookException e)
		/// {
		///     var actual = result;
		/// }
		/// </code>
		/// </example>
		/// <param name="callback">The AsyncCallback delegate</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>        
		/// <returns>This method returns the same set of subelements, whether or not there are outstanding notifications in any area. Note that if the unread subelement value is 0 for any of the pokes or shares elements, the most_recent element is also 0. Otherwise, the most_recent element contains an identifier for the most recent notification of the enclosing type.</returns>
		public void GetAsync(GetCallback callback, Object state)
		{
			Get(true, callback, state);
		}

		/// <summary>
		/// Sends a notification to a set of users.
		/// </summary>
		/// <example>
		/// <code>
		/// private static void RunDemoAsync()
		/// {
		///     List&lt;long&gt; notificationList = new List&lt;long&gt;();
		///     notificationList.Add(Constants.UserId);
		///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
		///     api.Notifications.SendAsync(notificationList, "my notification from samples app", AsyncDemoCompleted, null);
		/// }
		///
		/// private static void AsyncDemoCompleted(string result, Object state, FacebookException e)
		/// {
		///     var actual = result;
		/// }
		/// </code>
		/// </example>
		/// <param name="to_ids">List recipient IDs. These must be either friends of the logged-in user or people who have added your application. To send a notification to the current logged-in user without a name prepended to the message, set to_ids to empty List.</param>
		/// <param name="notification">The content of the notification. The notification uses a stripped down version of FBML and HTML, allowing only text and links (see the list of allowed tags). The notification can contain up to 2,000 characters.</param>
		/// <param name="callback">The AsyncCallback delegate</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>        
		/// <returns>This returns a list of user ids for whom notifications were successfully sent. We will throw an error if an error occurred.</returns>
		/// <remarks>The notification parameter is a very stripped-down set of FBML which allows only tags that result in just text and links.</remarks>
		public void SendAsync(List<long> to_ids, string notification, SendCallback callback, Object state)
		{
			SendAsync(to_ids, notification, null, callback, state);
		}

		/// <summary>
		/// Sends a notification to a set of users.
		/// </summary>
		/// <example>
		/// <code>
		/// private static void RunDemoAsync()
		/// {
		///     List&lt;long&gt; notificationList = new List&lt;long&gt;();
		///     notificationList.Add(Constants.UserId);
		///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
		///     api.Notifications.SendAsync(notificationList, "my notification from samples app", "app_to_user", AsyncDemoCompleted, null);
		/// }
		///
		/// private static void AsyncDemoCompleted(string result, Object state, FacebookException e)
		/// {
		///     var actual = result;
		/// }
		/// </code>
		/// </example>
		/// <param name="to_ids">List recipient IDs. These must be either friends of the logged-in user or people who have added your application. To send a notification to the current logged-in user without a name prepended to the message, set to_ids to empty List.</param>
		/// <param name="notification">The content of the notification. The notification uses a stripped down version of FBML and HTML, allowing only text and links (see the list of allowed tags). The notification can contain up to 2,000 characters.</param>
		/// <param name="type">Specify whether the notification is a user_to_user one or an app_to_user. (Default value is user_to_user.)</param>
		/// <param name="callback">The AsyncCallback delegate</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>        
		/// <returns>This returns a list of user ids for whom notifications were successfully sent. We will throw an error if an error occurred.</returns>
		/// <remarks>The notification parameter is a very stripped-down set of FBML which allows only tags that result in just text and links.</remarks>
		public void SendAsync(List<long> to_ids, string notification, string type, SendCallback callback, Object state)
		{
			SendAsync(StringHelper.ConvertToCommaSeparated(to_ids), notification, type, callback, state);
		}

		/// <summary>
		/// Sends a notification to a set of users.
		/// </summary>
		/// <example>
		/// <code>
		/// private static void RunDemoAsync()
		/// {
		///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
		///     api.Notifications.SendAsync(Constants.UserId.ToString(), "my notification from samples app", AsyncDemoCompleted, null);
		/// }
		///
		/// private static void AsyncDemoCompleted(string result, Object state, FacebookException e)
		/// {
		///     var actual = result;
		/// }
		/// </code>
		/// </example>
		/// <param name="to_ids">Comma-separated list of recipient IDs. These must be either friends of the logged-in user or people who have added your application. To send a notification to the current logged-in user without a name prepended to the message, set to_ids to empty List.</param>
		/// <param name="notification">The content of the notification. The notification uses a stripped down version of FBML and HTML, allowing only text and links (see the list of allowed tags). The notification can contain up to 2,000 characters.</param>
		/// <param name="callback">The AsyncCallback delegate</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>        
		/// <returns>This returns a list of user ids for whom notifications were successfully sent. We will throw an error if an error occurred.</returns>
		/// <remarks>The notification parameter is a very stripped-down set of FBML which allows only tags that result in just text and links.</remarks>
		public void SendAsync(string to_ids, string notification, SendCallback callback, Object state)
		{
			SendAsync(to_ids, notification, null, callback, state);
		}

		/// <summary>
		/// Sends a notification to a set of users.
		/// </summary>
		/// <example>
		/// <code>
		/// private static void RunDemoAsync()
		/// {
		///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
		///     api.Notifications.SendAsync(Constants.UserId.ToString(), "my notification from samples app", "app_to_user", AsyncDemoCompleted, null);
		/// }
		///
		/// private static void AsyncDemoCompleted(string result, Object state, FacebookException e)
		/// {
		///     var actual = result;
		/// }
		/// </code>
		/// </example>
		/// <param name="to_ids">List recipient IDs. These must be either friends of the logged-in user or people who have added your application. To send a notification to the current logged-in user without a name prepended to the message, set to_ids to empty List.</param>
		/// <param name="notification">The content of the notification. The notification uses a stripped down version of FBML and HTML, allowing only text and links (see the list of allowed tags). The notification can contain up to 2,000 characters.</param>
		/// <param name="type">Specify whether the notification is a user_to_user one or an app_to_user. (Default value is user_to_user.)</param>
		/// <param name="callback">The AsyncCallback delegate</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>        
		/// <returns>This returns a list of user ids for whom notifications were successfully sent. We will throw an error if an error occurred.</returns>
		/// <remarks>The notification parameter is a very stripped-down set of FBML which allows only tags that result in just text and links.</remarks>
		public void SendAsync(string to_ids, string notification, string type, SendCallback callback, Object state)
		{
			Send(to_ids, notification, type, true, callback, state);
		}

		/// <summary>
		/// This method gets all the current session user's notifications, as well as data for the applications that generated those notifications. It is a wrapper around the notification and application FQL tables; you can achieve more fine-grained control by using those two FQL tables in conjunction with the fql.multiquery API call. 
		/// Applications must pass a valid session key. 
		/// </summary>
		/// <param name="start_time">Indicates the earliest time to return a notification. This equates to the updated_time field in the notification FQL table. If not specified, this call returns all available notifications. </param>
		/// <param name="include_read">Indicates whether to include notifications that have already been read. By default, notifications a user has read are not included. </param>
		/// <param name="callback">The AsyncCallback delegate</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>        
		/// <returns>alerts and apps.</returns>
		public void GetListAsync(DateTime? start_time, bool include_read, GetListCallback callback, Object state)
		{
			GetList(start_time, include_read, true, callback, state);
		}

		/// <summary>
		/// This method marks one or more notifications as read. You return the notifications by calling notifications.getList or querying the notification FQL table. 
		/// Applications must pass a valid session key, and can only mark the notifications of the current session user. 
		/// </summary>
		/// <param name="notification_ids">List of notification ids</param>
		/// <param name="callback">The AsyncCallback delegate</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>        
		/// <returns>alerts and apps.</returns>
		public void MarkReadAsync(List<long> notification_ids, MarkReadCallback callback, Object state)
		{
			MarkRead(notification_ids, true, callback, state);
		}

		/// <summary>
		/// Sends an email to the specified users who have the application.
		/// </summary>
		/// <example>
		/// <code>
		/// private static void RunDemoAsync()
		/// {
		///     List&lt;long&gt; notificationList = new List&lt;long&gt;();
		///     notificationList.Add(Constants.UserId);
		///     string fbml = string.Format("&lt;fb:switch&gt;&lt;fb:profile-pic uid=\"{0}\" /&gt;&lt;fb:default&gt;Unable to show profile pic&lt;/fb:default&gt;&lt;/fb:switch&gt;", Constants.UserId);
		///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
		///
		///     // This action will require the user to have granted this app "email" Extended Permission.
		///     //var result = api.Notifications.SendEmail(notificationList, "email test", "here is plain text", string.Empty);
		///     api.Notifications.SendEmailAsync(notificationList, "email test", string.Empty, fbml, AsyncDemoCompleted, null);
		/// }
		///
		/// private static void AsyncDemoCompleted(string result, Object state, FacebookException e)
		/// {
		///     var actual = result;
		/// }
		/// </code>
		/// </example>
		/// <param name="recipients">A List of recipient IDs. The recipients must be people who have already added your application. You can email up to 100 people at a time.</param>
		/// <param name="subject">The subject of the email message. As of 10/28/2008, the subject will accept a limited set of FBML tags, including names, and tags related to internationalization.</param>
		/// <param name="plainText">The plain text version of the email content. You must include a non-empty value for at least one of either the fbml or text parameters. The FBML input takes precedence, but if the given FBML value is invalid or cannot be rendered, then the text will be used instead. There is currently no character limit on the length of either the text or FBML body.</param>
		/// <param name="fbml">The FBML version of the email. You must include a non-empty value for at least one of either the fbml or text parameters. The fbml parameter is a stripped-down set of FBML that allows only HTML/FBML tags that result in text, links, linebreaks, as well as tags related to internationalization.</param>
		/// <param name="callback">The AsyncCallback delegate</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>        
		/// <returns>This returns a comma-separated list of user ids for whom notifications were successfully sent. We will throw an error if an error occurred.</returns>
		public void SendEmailAsync(List<long> recipients, string subject, string plainText, string fbml, SendCallback callback,
		                           Object state)
		{
			SendEmailAsync(StringHelper.ConvertToCommaSeparated(recipients), subject, plainText, fbml, callback, state);
		}

		/// <summary>
		/// Sends an email to the specified users who have the application.
		/// </summary>
		/// <example>
		/// <code>
		/// private static void RunDemoAsync()
		/// {
		///     string fbml = string.Format("&lt;fb:switch&gt;&lt;fb:profile-pic uid=\"{0}\" /&gt;&lt;fb:default&gt;Unable to show profile pic&lt;/fb:default&gt;&lt;/fb:switch&gt;", Constants.UserId);
		///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
		///
		///     // This action will require the user to have granted this app "email" Extended Permission.
		///     //var result = api.Notifications.SendEmail(Constants.UserId.ToString(), "email test", "here is plain text", string.Empty);
		///     api.Notifications.SendEmailAsync(Constants.UserId.ToString(), "email test", string.Empty, fbml, AsyncDemoCompleted, null);
		/// }
		///
		/// private static void AsyncDemoCompleted(string result, Object state, FacebookException e)
		/// {
		///     var actual = result;
		/// }
		/// </code>
		/// </example>
		/// <param name="recipients">A comma-separated list of recipient IDs. The recipients must be people who have already added your application. You can email up to 100 people at a time.</param>
		/// <param name="subject">The subject of the email message. As of 10/28/2008, the subject will accept a limited set of FBML tags, including names, and tags related to internationalization.</param>
		/// <param name="plainText">The plain text version of the email content. You must include a non-empty value for at least one of either the fbml or text parameters. The FBML input takes precedence, but if the given FBML value is invalid or cannot be rendered, then the text will be used instead. There is currently no character limit on the length of either the text or FBML body.</param>
		/// <param name="fbml">The FBML version of the email. You must include a non-empty value for at least one of either the fbml or text parameters. The fbml parameter is a stripped-down set of FBML that allows only HTML/FBML tags that result in text, links, linebreaks, as well as tags related to internationalization.</param>
		/// <param name="callback">The AsyncCallback delegate</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>        
		/// <returns>This returns a comma-separated list of user ids for whom notifications were successfully sent. We will throw an error if an error occurred.</returns>
		public void SendEmailAsync(string recipients, string subject, string plainText, string fbml, SendCallback callback,
		                           Object state)
		{
			SendEmail(recipients, subject, plainText, fbml, true, callback, state);
		}

		#endregion Asynchronous Methods

		#endregion Public Methods

		#region Private Methods

		private notifications Get(bool isAsync, GetCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> {{"method", "facebook.notifications.get"}};

			if (isAsync)
			{
				SendRequestAsync(parameterList, new FacebookCallCompleted<notifications>(callback), state);
				return null;
			}

			return SendRequest<notifications_get_response>(parameterList);
		}

		private string Send(string to_ids, string notification, string type, bool isAsync, SendCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string>
			                    	{
			                    		{"method", "facebook.notifications.send"},
			                    		{"to_ids", to_ids}
			                    	};
			Utilities.AddFBMLParameter(parameterList, "notification", notification);
			Utilities.AddOptionalParameter(parameterList, "type", type);

			if (isAsync)
			{
				SendRequestAsync<notifications_send_response, string>(parameterList, !string.IsNullOrEmpty(Session.SessionKey),
				                                                      new FacebookCallCompleted<string>(callback), state);
				return null;
			}

			var response = SendRequest<notifications_send_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? null : response.TypedValue;
		}

		private string SendEmail(string recipients, string subject, string text, string fbml, bool isAsync,
		                         SendCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string>
			                    	{
			                    		{"method", "facebook.notifications.sendEmail"},
			                    		{"recipients", recipients},
			                    		{"subject", subject},
			                    		{"text", text}
			                    	};

			Utilities.AddFBMLParameter(parameterList, "fbml", fbml);

			if (isAsync)
			{
				SendRequestAsync<notifications_sendEmail_response, string>(parameterList, !string.IsNullOrEmpty(Session.SessionKey),
				                                                           new FacebookCallCompleted<string>(callback), state);
				return null;
			}

			var response = SendRequest<notifications_sendEmail_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? null : response.TypedValue;
		}

		private notification_data GetList(DateTime? start_time, bool include_read, bool isAsync, GetListCallback callback,
		                                  Object state)
		{
			var parameterList = new Dictionary<string, string>
			                    	{
			                    		{"method", "facebook.notifications.GetList"},
			                    	};
			Utilities.AddOptionalParameter(parameterList, "start_time", DateHelper.ConvertDateToDouble(start_time));
			Utilities.AddParameter(parameterList, "include_read", include_read);

			if (isAsync)
			{
				SendRequestAsync(parameterList, new FacebookCallCompleted<notification_data>(callback), state);
				return null;
			}

			return SendRequest<notifications_GetList_response>(parameterList);
		}

		private bool MarkRead(List<long> notification_ids, bool isAsync, MarkReadCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string>
			                    	{
			                    		{"method", "facebook.notifications.markRead"},
			                    		{"notification_ids", StringHelper.ConvertToCommaSeparated(notification_ids)}
			                    	};

			if (isAsync)
			{
				SendRequestAsync<notifications_markRead_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback),
				                                                        state);
				return true;
			}

			var response = SendRequest<notifications_markRead_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? false : response.TypedValue;
		}

		#endregion Private Methods

		#endregion Methods

		#region Delegates

		/// <summary>
		/// Delegate called when GetNotifications call is completed.
		/// </summary>
		/// <param name="notifications">notifications object</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>
		/// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void GetCallback(notifications notifications, Object state, FacebookException e);

		/// <summary>
		/// Delegate called when SendNotification or SendEmail call is completed.
		/// </summary>
		/// <param name="receipientIds">Ids of receipients who received the notification</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>
		/// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void SendCallback(string receipientIds, Object state, FacebookException e);

		/// <summary>
		/// Delegate called when GetList call is completed.
		/// </summary>
		/// <param name="notifications">notifications</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>
		/// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void GetListCallback(notification_data notifications, Object state, FacebookException e);

		/// <summary>
		/// Delegate called when MarkRead call is completed.
		/// </summary>
		/// <param name="isSuccessful">indicator if notifications are successful</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>
		/// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void MarkReadCallback(bool isSuccessful, Object state, FacebookException e);

		#endregion Delegates
	}
}
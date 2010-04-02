using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
    /// <summary>
    /// Facebook Admin API methods.
    /// </summary>
    public class Admin : AuthorizedRestBase, Facebook.Rest.IAdmin
    {
        #region Enumerations

        /// <summary>
        /// The type of period over which metrics are collected.
        /// </summary>
        public enum Period
        {           
            ///<summary>
            ///</summary>
            Day = 86400,
            
            ///<summary>
            ///</summary>
            Week = 604800,
            
            ///<summary>
            ///</summary>
            Month = 2592000
        }

        ///<summary>
        ///</summary>
        public enum IntegrationPointName
        {
            ///<summary>
            ///</summary>
            notifications_per_day,

            ///<summary>
            ///</summary>
            requests_per_day,

            ///<summary>
            ///</summary>
            emails_per_day,

            ///<summary>
            ///</summary>
            email_disable_message_location
        }

        #endregion Enumerations
        
        #region Methods

        #region Constructor

        /// <summary>
        /// Public constructor for Facebook.ExampleObject
        /// </summary>
        /// <param name="session">Needs a connected Facebook Session object for making requests</param>
        public Admin(SessionInfo session)
            : base(session)
        {
        }

        #endregion Constructor

        #region Public Methods

#if !SILVERLIGHT

#region Synchronous Methods

        /// <summary>
        /// Returns specified metrics for your application, given a time period.
		/// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// DateTime startDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0));
        /// DateTime endDate = DateTime.Now;
        /// Admin.Period period = Admin.Period.Day;
        /// IList&lt;metrics&gt; actual = api.Admin.GetMetrics(startDate, endDate, period);
        /// </code>
        /// </example>
        /// <param name="startDate">A DateTime for the start of the range (inclusive).</param>
        /// <param name="endDate">A DateTime time for the end of the range (inclusive). The end_time cannot be more than 30 days after the start_time.</param>
        /// <param name="period">The length of the period, in seconds, during which the metrics were collected. Currently, the only supported periods are 86400 (1 day), 604800 (7-days), and 2592000 (30 days).</param>
        /// <returns>This method returns the metrics specified for the given range and time period.</returns>
        public IList<metrics> GetMetrics(DateTime startDate, DateTime endDate, Period period)
        {
            return GetMetrics(GetMetricNames(period), startDate, endDate, period);
        }

        /// <summary>
        /// Returns specified metrics for your application, given a time period.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// DateTime startDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0));
        /// DateTime endDate = DateTime.Now;
        /// Admin.Period period = Admin.Period.Day;
        /// List&lt;string&gt; metrics = new List&lt;string&gt; { "active_users", "canvas_page_views" };
        /// IList&lt;metrics&gt; actual = api.Admin.GetMetrics(metrics, startDate, endDate, period);
        /// </code>
        /// </example>
        /// <param name="metrics">A List of metrics to retrieve (e.g. "active_users", "canvas_page_views")</param>
        /// <param name="startDate">A DateTime for the start of the range (inclusive).</param>
        /// <param name="endDate">A DateTime time for the end of the range (inclusive). The end_time cannot be more than 30 days after the start_time.</param>
        /// <param name="period">The length of the period, in seconds, during which the metrics were collected. Currently, the only supported periods are 86400 (1 day), 604800 (7-days), and 2592000 (30 days).</param>
        /// <returns>This method returns the metrics specified for the given range and time period.</returns>
        public IList<metrics> GetMetrics(List<string> metrics, DateTime startDate, DateTime endDate, Period period)
        {
            return GetMetrics(metrics, startDate, endDate, period, false, null, null);
        }

        /// <summary>
        /// Returns the current allocation limit for your application for the specified integration point.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// int requests = api.Admin.GetAllocation(Admin.IntegrationPointName.requests_per_day);
        /// </code>
        /// </example>
        /// <param name="name">Integration point name on which the allocation limit is requested</param>
        /// <returns>This method returns the allocation limit for your application for the specified integration point.</returns>
        public int GetAllocation(IntegrationPointName name)
        {
            return GetAllocation(name, false, null, null);
        }

        /// <summary>
        /// Sets values for properties for your applications in the Facebook Developer application.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// Dictionary&lt;string, string&gt; properties = new Dictionary&lt;string, string&gt; { { "privacy_url", "http://localhost/" } };
        /// bool result = api.Admin.SetAppProperties(properties);
        /// </code>
        /// </example>
        /// <param name="properties">A Dictionary of property names to new values. This call will fail if values have the wrong type.</param>
        /// <returns>This method returns true if the set is successful.</returns>
        public bool SetAppProperties(Dictionary<string,string> properties)
        {
            return SetAppProperties(properties, false, null, null);
        }
        
        /// <summary>
        /// Returns values of properties for your applications from the Facebook Developer application.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// Dictionary&lt;string, string&gt; result = api.Admin.GetAppProperties();
        /// </code>
        /// </example>
        /// <returns>This method returns the app properties as a Dictionary collection of property name to value.</returns>
        public Dictionary<string, string> GetAppProperties()
        {
            return GetAppProperties(GetApplicationPropertyNames());
        }

        /// <summary>
        /// Returns values of properties for your applications from the Facebook Developer application.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// List&lt;string&gt; properties = new List&lt;string&gt; { "privacy_url" };
        /// Dictionary&lt;string, string&gt; result = api.Admin.GetAppProperties(properties);
        /// </code>
        /// </example>
        /// <param name="properties">A list of property names that you want to view.</param>
        /// <returns>This method returns the app properties as a Dictionary collection of property name to value.</returns>
        public Dictionary<string, string> GetAppProperties(List<string> properties)
        {
            return GetAppProperties(properties, false, null, null);
        }

        /// <summary>
        /// Returns the demographic restrictions for the application.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// string result = api.Admin.GetRestrictionInfo();
        /// </code>
        /// </example>
        /// <returns>This method returns a string containing the demographic restrictions for the application.</returns>
        public string GetRestrictionInfo()
        {
            return GetRestrictionInfo(false, null, null);
        }

        /// <summary>
        /// Sets the demographic restrictions for the application.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// bool result = api.Admin.SetRestrictionInfo();
        /// </code>
        /// </example>
        /// <returns>This method returns true if the restrictions are successfully set.</returns>
        public bool SetRestrictionInfo()
        {
            return SetRestrictionInfo(null);
        }

        /// <summary>
        /// Sets the demographic restrictions for the application.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// Dictionary&lt;string, string&gt; restrictions = new Dictionary&lt;string, string&gt; {{"age_distribution", "10-99"}};
        /// bool result = api.Admin.SetRestrictionInfo(restrictions);
        /// </code>
        /// </example>
        /// <param name="restriction">A Dictionary of the restricting attributes. Restrictions include age, location, age_distribution, and type.</param>
        /// <returns>This method returns true if the restrictions are successfully set.</returns>
        public bool SetRestrictionInfo(Dictionary<string, string> restriction)
        {
            return SetRestrictionInfo(restriction, false, null, null);
        }

        /// <summary>
        /// Prevents users from accessing an application's canvas page and its forums.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// List&lt;long&gt; uids = new List&lt;long&gt; { 1000001, 1000002 };
        /// bool result = api.Admin.BanUsers(uids);
        /// </code>
        /// </example>
        /// <param name="uids">A List of user IDs to ban.</param>
        /// <returns>This method returns true if the users were successfully banned.</returns>
        public bool BanUsers(List<long> uids)
        {
            return BanUsers(uids, false, null, null);
        }

        /// <summary>
        /// Unbans users previously banned with admin.banUsers.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// List&lt;long&gt; uids = new List&lt;long&gt; { 1000001, 1000002 };
        /// bool result = api.Admin.UnbanUsers(uids);
        /// </code>
        /// </example>
        /// <param name="uids">A List of user IDs to unban.</param>
        /// <returns>This method returns true if the users were successfully unbanned.</returns>
        public bool UnbanUsers(List<long> uids)
        {
            return UnbanUsers(uids, false, null, null);
        }

        /// <summary>
        /// Returns the list of users who have been banned from the application.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// IList&lt;long&gt; result = api.Admin.GetBannedUsers();
        /// </code>
        /// </example>
        /// <returns>An List of user IDs from banned users.</returns>
        public IList<long> GetBannedUsers()
        {
            return GetBannedUsers(null);
        }

        /// <summary>
        /// Returns the list of users who have been banned from the application.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// List&lt;long&gt; uids = new List&lt;long&gt; { 1000001, 1000002 };
        /// IList&lt;long&gt; result = api.Admin.GetBannedUsers(uids);
        /// </code>
        /// </example>
        /// <param name="uids">A List of user IDs used to filter the result. The only user IDs that are returned in uids are those of banned users.</param>
        /// <returns>An array of user IDs from banned users.</returns>
        public IList<long> GetBannedUsers(List<long> uids)
        {
            return GetBannedUsers(uids, false, null, null);
        }

#endregion Synchronous Methods

#endif

#region Asynchronous Methods
        
        /// <summary>
        /// Returns the current allocation limit for your application for the specified integration point.
        /// </summary>
        /// <example>
        /// <code>
        /// private void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Admin.GetAllocationAsync(Admin.IntegrationPointName.requests_per_day, AsyncDemoCompleted, null);
        /// }
        /// 
        /// private void AsyncDemoCompleted(int result, Object state, FacebookException e)
        /// {
        ///     int actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="name">Integration point name on which the allocation limit is requested</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <returns>This method returns the allocation limit for your application for the specified integration point.</returns>
        public void GetAllocationAsync(IntegrationPointName name, GetAllocationCallback callback, Object state)
        {
            GetAllocation(name, true, callback, state);
        }

        /// <summary>
        /// Returns values of properties for your applications from the Facebook Developer application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunWebDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     api.Admin.GetAppPropertiesAsync(AsyncWebDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncWebDemoCompleted(Dictionary&lt;string, string&gt; result, Object state, FacebookException e)
        /// {
        ///     Dictionary&lt;string, string&gt; properties = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <returns>This method returns the app properties as a Dictionary collection of property name to value.</returns>
        public void GetAppPropertiesAsync(GetAppPropertiesCallback callback, Object state)
        {
            GetAppPropertiesAsync(GetApplicationPropertyNames(), callback, state);
        }

        /// <summary>
        /// Returns values of properties for your applications from the Facebook Developer application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunWebDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     List&lt;string&gt; properties = new List&lt;string&gt; { "privacy_url" };
        ///     api.Admin.GetAppPropertiesAsync(properties, AsyncWebDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncWebDemoCompleted(Dictionary&lt;string, string&gt; result, Object state, FacebookException e)
        /// {
        ///     Dictionary&lt;string, string&gt; properties = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="properties">A list of property names that you want to view.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <returns>This method returns the app properties as a Dictionary collection of property name to value.</returns>
        public void GetAppPropertiesAsync(List<string> properties, GetAppPropertiesCallback callback, Object state)
        {
            GetAppProperties(properties, true, callback, state);
        }

        /// <summary>
        /// Returns specified metrics for your application, given a time period.
        /// </summary>
        /// <example>
        /// <code>
        /// private void RunDemo()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     DateTime startDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0));
        ///     DateTime endDate = DateTime.Now;
        ///     Admin.Period period = Admin.Period.Day;
        ///     api.Admin.GetMetricsAsync(startDate, endDate, period, DemoCompleted, null);
        ///}
        ///
        /// private void DemoCompleted(IList&lt;metrics&gt; status, Object state, FacebookException e)
        /// {
        ///     IList&lt;metrics&gt; actual = status;
        /// }
        /// </code>
        /// </example>
        /// <param name="startDate">A DateTime for the start of the range (inclusive).</param>
        /// <param name="endDate">A DateTime time for the end of the range (inclusive). The end_time cannot be more than 30 days after the start_time.</param>
        /// <param name="period">The length of the period, in seconds, during which the metrics were collected. Currently, the only supported periods are 86400 (1 day), 604800 (7-days), and 2592000 (30 days).</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns the metrics specified for the given range and time period.</returns>
        public void GetMetricsAsync(DateTime startDate, DateTime endDate, Period period, GetMetricsCallback callback, Object state)
		{
            GetMetrics(GetMetricNames(period), startDate, endDate, period, true, callback, state);
		}

        /// <summary>
        /// Returns specified metrics for your application, given a time period.
        /// </summary>
        /// <example>
        /// <code>
        /// private void RunDemo()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     DateTime startDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0));
        ///     DateTime endDate = DateTime.Now;
        ///     Admin.Period period = Admin.Period.Day;
        ///     List&lt;string&gt; metrics = new List&lt;string&gt; { "active_users", "canvas_page_views" };
        ///     api.Admin.GetMetricsAsync(startDate, endDate, period, DemoCompleted, null);
        ///}
        ///
        /// private void DemoCompleted(IList&lt;metrics&gt; status, Object state, FacebookException e)
        /// {
        ///     IList&lt;metrics&gt; actual = status;
        /// }
        /// </code>
        /// </example>
        /// <param name="metrics">A List of metrics to retrieve (e.g. "active_users", "canvas_page_views")</param>
        /// <param name="startDate">A DateTime for the start of the range (inclusive).</param>
        /// <param name="endDate">A DateTime time for the end of the range (inclusive). The end_time cannot be more than 30 days after the start_time.</param>
        /// <param name="period">The length of the period, in seconds, during which the metrics were collected. Currently, the only supported periods are 86400 (1 day), 604800 (7-days), and 2592000 (30 days).</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns the metrics specified for the given range and time period.</returns>
        public void GetMetricsAsync(List<string> metrics, DateTime startDate, DateTime endDate, Period period, GetMetricsCallback callback, Object state)
        {
            GetMetrics(metrics, startDate, endDate, period, true, callback, state);
        }
        
        /// <summary>
        /// Sets values for properties for your applications in the Facebook Developer application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunWebDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     Dictionary&lt;string, string&gt; properties = new Dictionary&lt;string, string&gt; { { "privacy_url", "http://localhost/" } };
        ///     api.Admin.SetAppPropertiesAsync(properties, AsyncWebDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncWebDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     bool actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="properties">A Dictionary of property names to new values. This call will fail if values have the wrong type.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true if the set is successful.</returns>
        public void SetAppPropertiesAsync(Dictionary<string, string> properties, SetAppPropertiesCallback callback, Object state)
        {
            SetAppProperties(properties, true, callback, state);
        }

        /// <summary>
        /// Returns the demographic restrictions for the application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     api.Admin.GetRestrictionInfoAsync(AsyncDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncDemoCompleted(string result, Object state, FacebookException e)
        /// {
        ///     string actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns a string containing the demographic restrictions for the application.</returns>
        public void GetRestrictionInfoAsync(GetRestrictionInfoCallback callback, Object state)
        {
            GetRestrictionInfo(true, callback, state);
        }

        /// <summary>
        /// Sets the demographic restrictions for the application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     api.Admin.SetRestrictionInfoAsync(AsyncDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     bool actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true if the restrictions are successfully set.</returns>
        public void SetRestrictionInfoAsync(SetRestrictionInfoCallback callback, Object state)
		{
			SetRestrictionInfoAsync(null, callback, state);
		}

        /// <summary>
        /// Sets the demographic restrictions for the application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     Dictionary&lt;string, string&gt; restrictions = new Dictionary&lt;string, string&gt; { { "age_distribution", "10-99" } };
        ///     api.Admin.SetRestrictionInfoAsync(restrictions, AsyncDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     bool actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="restriction">A Dictionary of the restricting attributes. Restrictions include age, location, age_distribution, and type.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true if the restrictions are successfully set.</returns>
        public void SetRestrictionInfoAsync(Dictionary<string, string> restriction, SetRestrictionInfoCallback callback, Object state)
        {
            SetRestrictionInfo(restriction, true, callback, state);
        }

        /// <summary>
        /// Prevents users from accessing an application's canvas page and its forums.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     List&lt;long&gt; uids = new List&lt;long&gt; { 1000001, 1000002 };
        ///     api.Admin.BanUsersAsync(uids, AsyncDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncDemoCompleted(string result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uids">A List of user IDs to ban.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true if the users were successfully banned.</returns>
        public void BanUsersAsync(List<long> uids, BanUsersCallback callback, Object state)
        {
            BanUsers(uids, true, callback, state);
        }

        /// <summary>
        /// Unbans users previously banned with admin.banUsers.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///    FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///    api.Session.UserId = Constants.UserId;
        ///    List&lt;long&gt; uids = new List&lt;long&gt; { 1000001, 1000002 };
        ///    api.Admin.UnbanUsersAsync(uids, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///    var actual = result;
        ///}
        /// </code>
        /// </example>
        /// <param name="uids">A List of user IDs to unban.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true if the users were successfully unbanned.</returns>
        public void UnbanUsersAsync(List<long> uids, UnBanUsersCallback callback, Object state)
        {
            UnbanUsers(uids, true, callback, state);
        }

        /// <summary>
        /// Returns the list of users who have been banned from the application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     api.Admin.GetBannedUsersAsync(AsyncDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncDemoCompleted(IList&lt;long&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>An List of user IDs from banned users.</returns>
        public void GetBannedUsersAsync(GetBannedUsersCallback callback, Object state)
		{
			GetBannedUsersAsync(null, callback, state);
		}

        /// <summary>
        /// Returns the list of users who have been banned from the application.
        /// </summary>
        /// <example>
        /// <code>
        /// /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     List&lt;long&gt; uids = new List&lt;long&gt; { 1000001, 1000002 };
        ///     api.Admin.GetBannedUsersAsync(uids, AsyncDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncDemoCompleted(IList&lt;long&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uids">A List of user IDs used to filter the result. The only user IDs that are returned in uids are those of banned users.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>An List of user IDs from banned users.</returns>
        public void GetBannedUsersAsync(List<long> uids, GetBannedUsersCallback callback, Object state)
        {
            GetBannedUsers(uids, true, callback, state);
        }

        #endregion Asynchronous Methods

        #region Lookup Methods

        ///<summary>
        ///</summary>
        ///<returns></returns>
        public List<string> GetApplicationPropertyNames()
        {
            return new List<string>
            {
                "about_url",
                "app_id",
                "application_name",
                "authorize_url",
                "base_domain",
                "callback_url",
                "canvas_name",
                "connect_logo_url",
                "connect_reclaim_url",
                "connect_url",
                "contact_email",
                "dashboard_url",
                "default_column",
                "description",
                "desktop",
                "dev_mode",
                "edit_info_url",
                "edit_url",
                "email",
                "help_url",
                "icon_url",
                "iframe_enable_util",
                "ignore_ip_whitelist_for_ss",
                "info_changed_url",
                "installable",
                "ip_list",
                "is_mobile",
                "logo_url",
                "message_action",
                "message_url",
                "post_authorize_redirect_url",
                "preload_fql",
                "privacy_url",
                "private_install",
                "profile_tab_url",
                "publish_action",
                "publish_self_action",
                "publish_self_url",
                "publish_url",
                //"publisher_mode",
                "quick_transitions",
                "tab_default_name",
                "targeted",
                "tos_url",
                "uninstall_url",
                "use_iframe",
                "video_rentals",
                "wide_mode"
            };
        }

        ///<summary>
        ///</summary>
        ///<param name="period"></param>
        ///<returns></returns>
        public List<string> GetMetricNames(Period period)
        {
            //TODO: Use reflection against the schema to get these values
            var types = new List<string>
                       {
                          "active_users",
                          "api_calls",
                            "unique_api_calls",
                            "canvas_page_views",
                            "unique_canvas_page_views",
                            "canvas_http_request_time_avg",
                            "canvas_fbml_render_time_avg"
                       };
            //These are only valid for day-based values
            if (period == Period.Day)
            {
                types.AddRange(new List<string>
                               {
                                   "unique_adds",
                                   "unique_removes",
                                   "unique_blocks",
                                   "unique_unblocks"
                               });

            }
            return types;
        }

        ///<summary>
        ///</summary>
        ///<returns></returns>
        public List<string> GetDailyMetricNames()
        {
            //TODO: Use reflection against the schema to get these values
            return new List<string>
                       {
                           "date",
                           "daily_active_users",
                           "unique_adds",
                           "unique_removes",
                           "unique_blocks",
                           "unique_unblocks",
                           "api_calls",
                           "unique_api_calls",
                           "canvas_page_views",
                           "unique_canvas_page_views",
                           "canvas_http_request_time_avg",
                           "canvas_fbml_render_time_avg"
                       };
        }

        #endregion Lookup Methods

        #endregion Public Methods
        
        #region Private Methods

        private bool BanUsers (List<long> uids, bool isAsync, BanUsersCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.admin.banUsers" } };
            Utilities.AddJSONArray(parameterList, "uids", uids);
            
            if (isAsync)
            {
                SendRequestAsync<admin_banUsers_response, bool>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey), new FacebookCallCompleted<bool>(callback), state);
                return true;
            }

            var response = SendRequest<admin_banUsers_response>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey));
            return response == null ? true : response.TypedValue;
        }

        private int GetAllocation(IntegrationPointName name, bool isAsync, GetAllocationCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> {{"method", "facebook.admin.getAllocation"}};
            Utilities.AddRequiredParameter(parameterList, "integration_point_name", name.ToString());

            if (isAsync)
            {
                SendRequestAsync<admin_getAllocation_response, int>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey), new FacebookCallCompleted<int>(callback), state);
                return 0;
            }

            var response = SendRequest<admin_getAllocation_response>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey));
			return response == null ? default(int) : response.TypedValue;
        }

        private Dictionary<string, string> GetAppProperties(List<string> applicationProperties, bool isAsync, GetAppPropertiesCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.admin.getAppProperties" } };
            Utilities.AddJSONArray(parameterList, "properties", applicationProperties);

			var newState = new Object[2];
			newState[0] = callback;
			newState[1] = state;
       
            if (isAsync)
            {
                SendRequestAsync<admin_getAppProperties_response, string>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey), new FacebookCallCompleted<string>(OnGetAppPropertiesCompleted), newState);
                return null;
            }

            var response = SendRequest<admin_getAppProperties_response>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey));
			return response == null ? null : JSONHelper.ConvertFromJSONAssoicativeArray(response.TypedValue);
        }

        private IList<long> GetBannedUsers(List<long> uids, bool isAsync, GetBannedUsersCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.admin.getBannedUsers" } };
            Utilities.AddJSONArray(parameterList, "uids", uids);
            
            if (isAsync)
            {
           		SendRequestAsync<admin_getBannedUsers_response, IList<long>>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey), new FacebookCallCompleted<IList<long>>(callback), state, "uid");
                return null;
            }

			var response = SendRequest<admin_getBannedUsers_response>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey));
			return response == null ? null : response.uid;
        }

        private IList<metrics> GetMetrics(List<string> metrics, DateTime startDate, DateTime endDate, Period period, 
            bool isAsync, GetMetricsCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> {{"method", "facebook.admin.getMetrics"}};
            Utilities.AddRequiredParameter(parameterList, "start_time", DateHelper.ConvertDateToFacebookDate(startDate).ToString());
            Utilities.AddRequiredParameter(parameterList, "end_time", DateHelper.ConvertDateToFacebookDate(endDate).ToString());
            Utilities.AddRequiredParameter(parameterList, "period", period.ToString("D"));
            Utilities.AddJSONArray(parameterList, "metrics", metrics);
                
            if (isAsync)
            {
                SendRequestAsync<admin_getMetrics_response, IList<metrics>>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey), new FacebookCallCompleted<IList<metrics>>(callback), state, "metrics");
                return null;
            }

            var response = SendRequest<admin_getMetrics_response>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey));
			return response == null ? null : response.metrics;
        }
        
        private string GetRestrictionInfo(bool isAsync, GetRestrictionInfoCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.admin.getRestrictionInfo" } };
            
            if (isAsync)
            {
                SendRequestAsync<admin_getRestrictionInfo_response, string>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey), new FacebookCallCompleted<string>(callback), state);
                return null;
            }

            var response = SendRequest<admin_getRestrictionInfo_response>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey));
			return response == null ? null : response.TypedValue;
        }

        private bool SetAppProperties(Dictionary<string, string> properties, bool isAsync, SetAppPropertiesCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.admin.setAppProperties" } };
			Utilities.AddJSONAssociativeArray(parameterList, "properties", properties);
            
            if (isAsync)
            {
                SendRequestAsync<admin_getAppProperties_response, bool>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey), new FacebookCallCompleted<bool>(callback), state);
                return true;
            }

            var response = SendRequest<admin_setAppProperties_response>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey));
			return response == null ? true : response.TypedValue;
        }

        private bool SetRestrictionInfo(Dictionary<string, string> restrictionDictionary, bool isAsync, SetRestrictionInfoCallback callback, Object state)
        {
			string restriction = JSONHelper.ConvertToJSONAssociativeArray(restrictionDictionary);
            var parameterList = new Dictionary<string, string> { { "method", "facebook.admin.setRestrictionInfo" } };
            Utilities.AddOptionalParameter(parameterList, "restriction_str", restriction);
            
            if (isAsync)
            {
                SendRequestAsync<admin_setRestrictionInfo_response, bool>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey), new FacebookCallCompleted<bool>(callback), state);
               return true;
            }

            var response = SendRequest<admin_setRestrictionInfo_response>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey));
			return response == null ? true : response.TypedValue;
        }

        private bool UnbanUsers(List<long> uids, bool isAsync, UnBanUsersCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.admin.unbanUsers" } };
            Utilities.AddJSONArray(parameterList, "uids", uids);
            
            if (isAsync)
            {
                SendRequestAsync<admin_unbanUsers_response, bool>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey), new FacebookCallCompleted<bool>(callback), state);
                return true;
            }

            var response = SendRequest<admin_unbanUsers_response>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey));
			return response == null ? true : response.TypedValue;
        }

		private static void OnGetAppPropertiesCompleted(string result, Object state, FacebookException e)
		{
		    Object[] stateArray = state != null ? (Object[]) state : null;
			GetAppPropertiesCallback callback = stateArray != null && stateArray.Length > 0 ? (GetAppPropertiesCallback)stateArray[0] : null;
            var originalState = stateArray != null && stateArray.Length > 1 ? stateArray[1] : null;

			if (e == null)
			{
                if(callback != null)
				    callback(JSONHelper.ConvertFromJSONAssoicativeArray(result), originalState, e);
			}
			else
			{
                if(callback != null)
				    callback(null, originalState, e);
			}
		}
        
        #endregion Private Methods
   
        #endregion Methods

        #region Delegates

        /// <summary>
    /// Delegate called when GetAllocation call is completed.
    /// </summary>
    /// <param name="allocation">current allocation limit</param>
    /// <param name="state">An object containing state information for this asynchronous request</param>
    /// <param name="e">Exception object, if the call resulted in exception.</param>
    public delegate void GetAllocationCallback(int allocation, Object state, FacebookException e);

    /// <summary>
    /// Delegate called when GetAppProperties call completed
    /// </summary>
    /// <param name="result">result of operation</param>
    /// <param name="state">An object containing state information for this asynchronous request</param>
    /// <param name="e">Exception object, if the call resulted in exception.</param>
    public delegate void GetAppPropertiesCallback(Dictionary<string, string> result, Object state, FacebookException e);

    /// <summary>
    /// Delegate called when GetMetrics call completed
    /// </summary>
    /// <param name="metrics">metrics information</param>
    /// <param name="state">An object containing state information for this asynchronous request</param>
    /// <param name="e">Exception object, if the call resulted in exception.</param>
    public delegate void GetMetricsCallback(IList<metrics> metrics, Object state, FacebookException e);

    /// <summary>
    /// Delegate called when SetAppProperties call completed
    /// </summary>
    /// <param name="result">result of operation</param>
    /// <param name="state">An object containing state information for this asynchronous request</param>
    /// <param name="e">Exception object, if the call resulted in exception.</param>
    public delegate void SetAppPropertiesCallback(bool result, Object state, FacebookException e);

    /// <summary>
    /// Delegate called when GetRestrictionInfo call completed
    /// </summary>
    /// <param name="result">result of operation</param>
    /// <param name="state">An object containing state information for this asynchronous request</param>
    /// <param name="e">Exception object, if the call resulted in exception.</param>
    public delegate void GetRestrictionInfoCallback(string result, Object state, FacebookException e);

    /// <summary>
    /// Delegate called when SetRestrictionInfo call completed
    /// </summary>
    /// <param name="result">result of operation</param>
    /// <param name="state">An object containing state information for this asynchronous request</param>
    /// <param name="e">Exception object, if the call resulted in exception.</param>
    public delegate void SetRestrictionInfoCallback(bool result, Object state, FacebookException e);

    /// <summary>
    /// Delegate called when BanUsers call completed
    /// </summary>
    /// <param name="result">result of operation</param>
    /// <param name="state">An object containing state information for this asynchronous request</param>
    /// <param name="e">Exception object, if the call resulted in exception.</param>
    public delegate void BanUsersCallback(bool result, Object state, FacebookException e);

    /// <summary>
    /// Delegate called when UnBanUsers call completed
    /// </summary>
    /// <param name="result">result of operation</param>
    /// <param name="state">An object containing state information for this asynchronous request</param>
    /// <param name="e">Exception object, if the call resulted in exception.</param>
    public delegate void UnBanUsersCallback(bool result, Object state, FacebookException e);

    /// <summary>
    /// Delegate called when AreFriends call completed
    /// </summary>
    /// <param name="users">List of users objects</param>
    /// <param name="state">An object containing state information for this asynchronous request</param>
    /// <param name="e">Exception object, if the call resulted in exception.</param>
    public delegate void GetBannedUsersCallback(IList<long> users, Object state, FacebookException e);
    
        #endregion Delegates
    }
}









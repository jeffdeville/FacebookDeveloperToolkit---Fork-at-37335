using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;
using System.Reflection;

namespace Facebook.Rest
{
	/// <summary>
	/// Facebook Connect API methods.
	/// </summary>
	public class Connect : RestBase, Facebook.Rest.IConnect
	{
		#region Methods

		#region Constructor

		/// <summary>
		/// Public constructor for facebook.Connect
		/// </summary>
		/// <param name="session">Needs a connected Facebook Session object for making requests</param>
		public Connect(IFacebookSession session)
			: base(session)
		{
		}

		#endregion Constructor

		#region Public Methods

#if !SILVERLIGHT

		#region Synchronous Methods

        /// <summary>
        /// Unregisters a previously registered account (using connect.registerUsers). You should call this method if the user deletes his or her account on your site. (for Facebook Connect).
        /// </summary>
        /// <example>
        /// <code>
        /// ConnectSession _connectSession = new ConnectSession(Constants.ApplicationKey, Constants.ApplicationSecret);
        /// 
        /// NOTE: This is shortened for brevity; would require a postback after completing Facebook Connect authentication.
        /// 
        /// if (_connectSession.IsConnected())
        /// {
        ///     Api _api = new Api(_connectSession);
        ///     List&lt;string&gt; hashes = new List&lt;string&gt;();
        ///     hashes.Add(_currentUser.email_hashes.email_hashes_elt[0]);
        ///     var result = _api.Connect.UnregisterUsers(hashes);
        /// }
        /// </code>
        /// </example>
        /// <param name="email_hashes">An array of email_hashes to unregister.</param>
        /// <returns>This method returns a List of unregistered email hashes. If any email hashes are missing, we recommend that you try unregistering the account again later.</returns>
        public IList<string> UnregisterUsers(List<string> email_hashes)
		{
			return UnregisterUsers(email_hashes, false, null, null);
		}

		/// <summary>
        /// Creates an association between an existing user account on your site and that user's Facebook account, provided the user has not connected accounts before (for Facebook Connect).
		/// </summary>
		/// <example>
        /// <code>
        /// ConnectSession _connectSession = new ConnectSession(Constants.ApplicationKey, Constants.ApplicationSecret);
        /// 
        /// NOTE: This is shortened for brevity; would require a postback after completing Facebook Connect authentication.
        /// 
        /// if (_connectSession.IsConnected())
        /// {
        ///     _api = new Api(_connectSession);
        ///     var registerList = new List&lt;ConnectAccountMap&gt;();
        ///     registerList.Add(new ConnectAccountMap
        ///     {
        ///         EmailAddress = "facebook@claritycon.com",
        ///         AccountId = "10001"
        ///     });
        ///     var result = _api.Connect.RegisterUsers(registerList);
        /// }
        /// </code>
        /// </example>
        /// <param name="accounts">An array of up to 1,000 arrays, or "maps," where each map represent a connected account.</param>
        /// <returns>This method returns a List email hashes that have been successfully registered. If any email hashes are missing, we recommend that you try registering them again later.</returns>
		public IList<string> RegisterUsers(List<ConnectAccountMap> accounts)
		{
			return RegisterUsers(accounts, false, null, null);
		}

		/// <summary>
        /// Returns the number of friends of the current user who have accounts on your site, but have not yet connected their accounts. (for [{Facebook Connect]]).
		/// </summary>
		/// <example>
        /// <code>
        /// ConnectSession connectSession = new ConnectSession(Constants.ApplicationKey, Constants.ApplicationSecret);
        /// /// 
        /// /// NOTE: This is shortened for brevity; would require a postback after completing Facebook Connect authentication.
        /// /// 
        /// /// if(connectSession.IsConnected())
        /// {
        ///     Api api = new Api(connectSession);
        ///     var result = api.Connect.GetUnconnectedFriends();
        /// }
        /// </code>
        /// </example>
        /// <returns>This method returns an int that indicates the number of users who have not yet connected their accounts.</returns>
		public int GetUnconnectedFriends()
		{
			return GetUnconnectedFriends(false, null, null);
		}
		
		#endregion

#endif

		#region Asynchronous Methods

        /// <summary>
        /// Unregisters a previously registered account (using connect.registerUsers). You should call this method if the user deletes his or her account on your site. (for Facebook Connect).
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemo()
        /// {
        ///     ConnectSession connectSession = new ConnectSession(Constants.ApplicationKey, Constants.ApplicationSecret);
        ///
        ///     /// 
        ///     /// NOTE: This is shortened for brevity; would require a postback after completing Facebook Connect authentication.
        ///     /// 
        ///    
        ///     if (connectSession.IsConnected())
        ///     {
        ///         Api _api = new Api(connectSession);
        ///         List&lt;string&gt; hashes = new List&lt;string&gt;();
        ///         hashes.Add(_currentUser.email_hashes.email_hashes_elt[0]);
        ///         _api.Connect.UnregisterUsersAsync(hashes, AsyncDemoCompleted, null); 
        ///     }
        /// }
        /// 
        /// private static void AsyncDemoCompleted(bool result, object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="email_hashes">An array of email_hashes to unregister.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns a List of unregistered email hashes. If any email hashes are missing, we recommend that you try unregistering the account again later.</returns>
        public void UnregisterUsersAsync(List<string> email_hashes, UnregisterUsersCallback callback, Object state)
		{
			UnregisterUsers(email_hashes, true, callback, state);
		}

        /// <summary>
        /// Creates an association between an existing user account on your site and that user's Facebook account, provided the user has not connected accounts before (for Facebook Connect).
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemo()
        /// {
        ///     ConnectSession connectSession = new ConnectSession(Constants.ApplicationKey, Constants.ApplicationSecret);
        ///
        ///     /// 
        ///     /// NOTE: This is shortened for brevity; would require a postback after completing Facebook Connect authentication.
        ///     /// 
        ///    
        ///     if (connectSession.IsConnected())
        ///     {
        ///         Api _api = new Api(connectSession);
        ///
        ///         var registerList = new List&lt;ConnectAccountMap&gt;();
        ///         registerList.Add(new ConnectAccountMap
        ///                              {
        ///                                  EmailAddress = "facebook@claritycon.com",
        ///                                  AccountId = "10001"
        ///                              });
        ///         _api.Connect.RegisterUsersAsync(registerList, AsyncDemoCompleted, null);
        ///     }
        /// }
        /// 
        /// private static void AsyncDemoCompleted(bool result, object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="accounts">An array of up to 1,000 arrays, or "maps," where each map represent a connected account.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns a List email hashes that have been successfully registered. If any email hashes are missing, we recommend that you try registering them again later.</returns>
        public void RegisterUsersAsync(List<ConnectAccountMap> accounts, RegisterUsersCallback callback, Object state)
		{
			RegisterUsers(accounts, true, callback, state);
		}

        /// <summary>
        /// Returns the number of friends of the current user who have accounts on your site, but have not yet connected their accounts. (for [{Facebook Connect]]).
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     ConnectSession connectSession = new ConnectSession(Constants.ApplicationKey, Constants.ApplicationSecret);
        ///
        ///     /// 
        ///     /// NOTE: This is shortened for brevity; would require a postback after completing Facebook Connect authentication.
        ///     /// 
        ///    
        ///     if (connectSession.IsConnected())
        ///     {
        ///         Api api = new Api(connectSession);
        ///         api.Connect.GetUnconnectedFriendsAsync(AsyncDemoCompleted, null);
        ///     }
        /// }
        ///
        /// private static void AsyncDemoCompleted(int result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns an int that indicates the number of users who have not yet connected their accounts.</returns>
        public void GetUnconnectedFriendsAsync(GetUnconnectedFriendsCallback callback, Object state)
		{
			GetUnconnectedFriends(true, callback, state);
		}

		#endregion

		#endregion Public Methods
        
		#region Private Methods

		private IList<string> UnregisterUsers(List<string> email_hashes, bool isAsync, UnregisterUsersCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.connect.unregisterUsers" } };
			Utilities.AddRequiredParameter(parameterList, "email_hashes", JSONHelper.ConvertToJSONArray(email_hashes));

			if (isAsync)
			{
                SendRequestAsync<connect_unregisterUsers_response, IList<string>>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<IList<string>>(callback), state);
				return null;
			}
            var response = SendRequest<connect_unregisterUsers_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response.connect_unregisterUsers_response_elt;
		}

        private IList<string> RegisterUsers(ICollection<ConnectAccountMap> accountMaps, bool isAsync, RegisterUsersCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.connect.registerUsers" } };
            var itemList = new List<string>();
            
            foreach (var accountMap in accountMaps)
            {
                var mappingDictionary = new Dictionary<string, string>();

                // Compute the email_hash
                mappingDictionary.Add("email_hash", Utilities.GenerateEmailHash(accountMap.EmailAddress));

                // If populated set AccountId
                if (!string.IsNullOrEmpty(accountMap.AccountId))
                {
                    mappingDictionary.Add("account_id", accountMap.AccountId);
                }

                // If populated set AccountUrl
                if (!string.IsNullOrEmpty(accountMap.AccountUrl))
                {
                    mappingDictionary.Add("account_url", accountMap.AccountUrl);
                }
                
                itemList.Add(JSONHelper.ConvertToJSONAssociativeArray(mappingDictionary));
            }
            Utilities.AddRequiredParameter(parameterList, "accounts", JSONHelper.ConvertToJSONArray(itemList));

            if (isAsync)
            {
                SendRequestAsync<connect_registerUsers_response, IList<string>>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<IList<string>>(callback), state);
                return null;
            }

            var response = SendRequest<connect_registerUsers_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response.connect_registerUsers_response_elt;
        }

		private int GetUnconnectedFriends(bool isAsync, GetUnconnectedFriendsCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.connect.getUnconnectedFriendsCount" } };

			if (isAsync)
			{
				SendRequestAsync(parameterList, new FacebookCallCompleted<int>(callback), state);
				return 0;
			}

			var response = SendRequest<connect_getUnconnectedFriendsCount_response>(parameterList);
			return response == null ? 0 : response.TypedValue;
		}

		#endregion Private Methods
        
		#endregion Methods

		#region Delegates

        /// <summary>
        /// Delegate called when UnregisterUsers call completed
        /// </summary>
        /// <param name="emailHashes">Hashes of the emails to unregister.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void UnregisterUsersCallback(IList<string> emailHashes, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when RegisterUsers call completed
        /// </summary>
        /// <param name="emailHashes">Hases of the emails of the users to unregister.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void RegisterUsersCallback(IList<string> emailHashes, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetUnconnectedFriends call completed
        /// </summary>
        /// <param name="numFriends">The count of unconnected friends.</param>
        /// <param name="state">An object containing state information for this asynchronous request.</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetUnconnectedFriendsCallback(int numFriends, Object state, FacebookException e);

		#endregion Delegates
	}

    /// <summary>
    /// A helper object to store one Facebook Connect account map, where each map represents one Facebook Connect site email address and one of two
    /// optional Account identifiers.  This object is used to register third-party accounts with existing Facebook accounts.
    /// </summary>
    public class ConnectAccountMap
    {
        /// <summary>
        /// The email account of the remote (Connect site) account.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Optional: The user's account ID on the Facebook Connect site. If you specify the AccountId property, then you must also set a 
        /// Connect Preview URL in your application's settings in order to generate a full user URL. The Connect Preview URL contains an 
        /// AccountId parameter, such as http://www.example.com/profile.php?user=AccountId. 
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Optional:  The URL to the user's account on the Facebook Connect site.  If you specify the AccountUrl property, that URL will be used directly.
        /// Facebook recommends that you specify at least one of either the AccountId or the AccountUrl properties.
        /// </summary>
        public string AccountUrl { get; set; }
    }
}
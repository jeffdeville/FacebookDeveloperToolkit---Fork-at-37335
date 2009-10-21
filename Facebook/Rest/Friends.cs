using System;
using System.Collections.Generic;
using System.Linq;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
    /// <summary>
    /// Facebook Friends API methods.
    /// </summary>
    public class Friends : RestBase, Facebook.Rest.IFriends
    {
        #region Private Members

        private readonly IUsers _users;

        #endregion Private Members

        #region Methods

        #region Constructor

        /// <summary>
        /// Public constructor for facebook.Friends
        /// </summary>
        /// <param name="users"></param>
        /// <param name="session">Needs a connected Facebook Session object for making requests</param>
        public Friends(IUsers users, IFacebookSession session)
            : base(session)
        {
            _users = users;
        }

        #endregion Constructor

        #region Public Methods

#if !SILVERLIGHT
        
        #region Synchronous Methods

        /// <summary>
        /// Returns whether or not each pair of specified users is friends with each other.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// List&lt;user&gt; u1 = new List&lt;user&gt;();
        /// List&lt;user&gt; u2 = new List&lt;user&gt;();
        /// u1.Add(api.Users.GetInfo(Constants.Friend_UserId1));
        /// u2.Add(api.Users.GetInfo(Constants.Friend_UserId2));
        /// var result = api.Friends.AreFriends(u1, u2);
        /// </code>
        /// </example>
        /// <param name="uids1">A list of user ids matched with uids2.</param>
        /// <param name="uids2">A list of user ids matched with uids1.</param>
        /// <returns>Returns a list of friend_info elements corresponding to the lists passed. The are_friends subelement of each friend_info element is 0 or false if the users are not friends, and 1 or true if they are friends. Note that, for each pair, this function is symmetric. That is, it does not matter which user is in uids1 and which is in uids2.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public IList<friend_info> AreFriends(List<user> uids1, List<user> uids2)
		{
            var u1 = (from u in uids1 select u.uid.Value).ToList();
            var u2 = (from u in uids2 select u.uid.Value).ToList();
			return AreFriends(u1, u2);
		}

        /// <summary>
        /// Returns whether or not each pair of specified users is friends with each other.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// user u1 = api.Users.GetInfo(Constants.Friend_UserId1);
        /// user u2 = api.Users.GetInfo(Constants.Friend_UserId2);
        /// var result = api.Friends.AreFriends(u1, u2);/// </code>
        /// </example>
        /// <param name="user1">A user to match against user2.</param>
        /// <param name="user2">A user to match against user1.</param>
        /// <returns>Returns a list of friend_info elements corresponding to the lists passed. The are_friends subelement of each friend_info element is 0 or false if the users are not friends, and 1 or true if they are friends. Note that, for each pair, this function is symmetric. That is, it does not matter which user is in uids1 and which is in uids2.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public IList<friend_info> AreFriends(user user1, user user2)
        {
            var uids1 = new List<long>();
            var uids2 = new List<long>();
            uids1.Add(user1.uid.Value);
            uids2.Add(user2.uid.Value);
            return AreFriends(uids1, uids2);
        }

        /// <summary>
        /// Returns whether or not each pair of specified users is friends with each other.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Friends.AreFriends(Constants.Friend_UserId1, Constants.Friend_UserId2);
        /// </code>
        /// </example>
        /// <param name="uid1">A user id to match against uid2.</param>
        /// <param name="uid2">A user id to match against uid1.</param>
        /// <returns>Returns a list of friend_info elements corresponding to the lists passed. The are_friends subelement of each friend_info element is 0 or false if the users are not friends, and 1 or true if they are friends. Note that, for each pair, this function is symmetric. That is, it does not matter which user is in uids1 and which is in uids2.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public IList<friend_info> AreFriends(long uid1, long uid2)
		{
			var uids1 = new List<long>();
			var uids2 = new List<long>();
			uids1.Add(uid1);
			uids2.Add(uid2);
			return AreFriends(uids1, uids2);
		}

        /// <summary>
        /// Returns whether or not each pair of specified users is friends with each other.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// List&lt;long&gt; u1 = new List&lt;long&gt; { Constants.Friend_UserId1 };
        /// List&lt;long&gt; u2 = new List&lt;long&gt; { Constants.Friend_UserId2 };
        /// var result = api.Friends.AreFriends(u1, u2);
        /// </code>
        /// </example>
        /// <param name="uids1">A list of user ids matched with uids2.</param>
        /// <param name="uids2">A list of user ids matched with uids1.</param>
        /// <returns>Returns a list of friend_info elements corresponding to the lists passed. The are_friends subelement of each friend_info element is 0 or false if the users are not friends, and 1 or true if they are friends. Note that, for each pair, this function is symmetric. That is, it does not matter which user is in uids1 and which is in uids2.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public IList<friend_info> AreFriends(List<long> uids1, List<long> uids2)
		{
		    return AreFriends(uids1, uids2, false, null, null);
	    }

        /// <summary>
        /// Returns the identifiers for the current user's Facebook friends.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Friends.Get();
        /// </code>
        /// </example>
        /// <returns>The List of friend IDs returned are the friends that are visible to the Facebook Platform. If no friends are found, the method returns an empty friends_get_response element.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public IList<long> Get()
        {
            return Get(0, 0);
        }

        /// <summary>
        /// Returns the identifiers for the current user's Facebook friends.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Friends.Get(Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="uid">The user ID for the user whose friends you want to return.</param>
        /// <returns>The List of friend IDs returned are the friends that are visible to the Facebook Platform. If no friends are found, the method returns an empty friends_get_response element.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public IList<long> Get(long uid)
        {
            return Get(uid, 0);
        }

        /// <summary>
        /// Returns the identifiers for the current user's Facebook friends.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Friends.Get(Constants.UserId, Constants.FriendListId);
        /// </code>
        /// </example>
        /// <param name="uid">The user ID for the user whose friends you want to return.</param>
        /// <param name="flid">Returns the friends in a friend list.</param>
        /// <returns>The List of friend IDs returned are the friends that are visible to the Facebook Platform. If no friends are found, the method returns an empty friends_get_response element.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public IList<long> Get(long uid, long flid)
		{
            return Get(uid, flid, false, null, null);
		}

        /// <summary>
        /// Returns the identifiers for the current user's Facebook friends who have authorized the specific calling application.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Friends.GetAppUsers();
        /// </code>
        /// </example>
        /// <returns>The friend IDs returned are those friends who have authorized the calling application, which is a subset of the friends returned from the friends.get method. If no friends are found, the method returns an empty friends_getAppUsers_response element.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public IList<long> GetAppUsers()
		{
            return GetAppUsers(false, null, null);
		}

        /// <summary>
        /// Returns the user objects for the current user's Facebook friends.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Friends.GetUserObjects();
        /// </code>
        /// </example>
        /// <returns>This method returns the user objects for the current user's friends.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public IList<user> GetUserObjects()
        {
            return GetUserObjects(0, 0);
        }

        /// <summary>
        /// Returns the user objects for the specified user's Facebook friends.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Friends.GetUserObjects(Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="uid">The user ID for the user whose friends you want to return.</param>
        /// <returns>This method returns the user objects for the specified user's friends.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public IList<user> GetUserObjects(long uid)
        {
            return GetUserObjects(uid, 0);
        }

        /// <summary>
        /// Returns the user objects for the specified user's Facebook friends.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Friends.GetUserObjects(Constants.UserId, Constants.FriendListId);
        /// </code>
        /// </example>
        /// <param name="uid">The user ID for the user whose friends you want to return.</param>
        /// <param name="flid">Returns the friends in a friend list.</param>
        /// <returns>This method returns the user objects for the specified user's friends.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public IList<user> GetUserObjects(long uid, long flid)
        {
            return GetUserObjects(uid, flid, false, null, null);
        }

        /// <summary>
        /// Returns the user objects for the current user's Facebook friends who have authorized the specific calling application.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Friends.GetAppUsersObjects();
        /// </code>
        /// </example>
        /// <returns>The friend user objects returned are those friends who have authorized the calling application, which is a subset of the friends returned from the friends.get method.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public IList<user> GetAppUsersObjects()
        {
            return GetAppUsersObjects(false, null, null);
        }

        /// <summary>
        /// Returns the identifiers for the current user's Facebook friend lists.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Friends.GetLists();
        /// </code>
        /// </example>
        /// <returns>The friend list IDs returned are the lists associated with the subject user.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public IList<friendlist> GetLists()
        {
            return GetLists(false, null, null);
        }

        /// <summary>
        /// Returns the identifiers for the requested users' Mutual Facebook friends.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Friends.GetMutualFriends(Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="target_uid">The user ID of one of the target user whose mutual friends you want to retrieve.</param>
        /// <returns>This method returns an List of user IDs of the mutual friends, or an error code.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public List<long> GetMutualFriends(long target_uid)
        {
            return GetMutualFriends(target_uid, null);
        }

        /// <summary>
        /// Returns the identifiers for the requested users' Mutual Facebook friends.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Friends.GetMutualFriends(Constants.Friend_UserId1, Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="target_uid">The user ID of one of the target user whose mutual friends you want to retrieve.</param>
        /// <param name="source_uid">The user ID of the other user for which you are getting mutual friends of. Defaults to the current session user.</param>
        /// <returns>This method returns an List of user IDs of the mutual friends, or an error code.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public List<long> GetMutualFriends(long target_uid, long? source_uid)
        {
            return GetMutualFriends(target_uid, source_uid, false, null, null);
        }

#endregion Synchronous Methods

#endif

        #region Asynchronous Methods

        /// <summary>
        /// Returns whether or not each pair of specified users is friends with each other.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     List&lt;user&gt; u1 = new List&lt;user&gt;();
        ///     List&lt;user&gt; u2 = new List&lt;user&gt;();
        ///     u1.Add(api.Users.GetInfo(Constants.Friend_UserId1));
        ///     u2.Add(api.Users.GetInfo(Constants.Friend_UserId2));
        ///     api.Friends.AreFriendsAsync(u1, u2, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;friend_info&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uids1">A list of user ids matched with uids2.</param>
        /// <param name="uids2">A list of user ids matched with uids1.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>Returns a list of friend_info elements corresponding to the lists passed. The are_friends subelement of each friend_info element is 0 or false if the users are not friends, and 1 or true if they are friends. Note that, for each pair, this function is symmetric. That is, it does not matter which user is in uids1 and which is in uids2.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public void AreFriendsAsync(List<user> uids1, List<user> uids2, AreFriendsCallback callback, Object state)
		{
            var u1 = (from u in uids1 select u.uid.Value).ToList();
            var u2 = (from u in uids2 select u.uid.Value).ToList();
			AreFriendsAsync(u1, u2, callback, state);
		}

        /// <summary>
        /// Returns whether or not each pair of specified users is friends with each other.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     user u1 = api.Users.GetInfo(Constants.Friend_UserId1);
        ///     user u2 = api.Users.GetInfo(Constants.Friend_UserId2);
        ///     api.Friends.AreFriendsAsync(u1, u2, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;friend_info&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }/// </code>
        /// </example>
        /// <param name="user1">A user to match against user2.</param>
        /// <param name="user2">A user to match against user1.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>Returns a list of friend_info elements corresponding to the lists passed. The are_friends subelement of each friend_info element is 0 or false if the users are not friends, and 1 or true if they are friends. Note that, for each pair, this function is symmetric. That is, it does not matter which user is in uids1 and which is in uids2.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public void AreFriendsAsync(user user1, user user2, AreFriendsCallback callback, Object state)
		{
			var u1 = new List<long>();
			var u2 = new List<long>();
            u1.Add(user1.uid.Value);
            u2.Add(user2.uid.Value);
            AreFriendsAsync(u1, u2, callback, state);
		}

        /// <summary>
        /// Returns whether or not each pair of specified users is friends with each other.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     api.Friends.AreFriendsAsync(Constants.Friend_UserId1, Constants.Friend_UserId2, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;friend_info&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid1">A user id to match against uid2.</param>
        /// <param name="uid2">A user id to match against uid1.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>Returns a list of friend_info elements corresponding to the lists passed. The are_friends subelement of each friend_info element is 0 or false if the users are not friends, and 1 or true if they are friends. Note that, for each pair, this function is symmetric. That is, it does not matter which user is in uids1 and which is in uids2.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public void AreFriendsAsync(long uid1, long uid2, AreFriendsCallback callback, Object state)
		{
			var u1 = new List<long>();
			var u2 = new List<long>();
            u1.Add(uid1);
            u2.Add(uid2);
            AreFriendsAsync(u1, u2, callback, state);
		}

        /// <summary>
        /// Returns whether or not each pair of specified users is friends with each other.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     List&lt;long&gt; u1 = new List&lt;long&gt; { Constants.Friend_UserId1 };
        ///     List&lt;long&gt; u2 = new List&lt;long&gt; { Constants.Friend_UserId2 };
        ///     api.Friends.AreFriendsAsync(u1, u2, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;friend_info&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uids1">A list of user ids matched with uids2.</param>
        /// <param name="uids2">A list of user ids matched with uids1.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>Returns a list of friend_info elements corresponding to the lists passed. The are_friends subelement of each friend_info element is 0 or false if the users are not friends, and 1 or true if they are friends. Note that, for each pair, this function is symmetric. That is, it does not matter which user is in uids1 and which is in uids2.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public void AreFriendsAsync(List<long> uids1, List<long> uids2, AreFriendsCallback callback, Object state)
        {
            AreFriends(uids1, uids2, true, callback, state);
        }

        /// <summary>
        /// Returns the identifiers for the current user's Facebook friends.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     api.Friends.GetAsync(AsyncDemoCompleted, null);
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
        /// <returns>The List of friend IDs returned are the friends that are visible to the Facebook Platform. If no friends are found, the method returns an empty friends_get_response element.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public void GetAsync(GetFriendsCallback callback, Object state)
        {
            GetAsync(0, 0, callback, state);
        }

        /// <summary>
        /// Returns the identifiers for the current user's Facebook friends.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     api.Friends.GetAsync(Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;long&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">The user ID for the user whose friends you want to return.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The List of friend IDs returned are the friends that are visible to the Facebook Platform. If no friends are found, the method returns an empty friends_get_response element.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public void GetAsync(long uid, GetFriendsCallback callback, Object state)
        {
            GetAsync(uid, 0, callback, state);
        }

        /// <summary>
        /// Returns the identifiers for the current user's Facebook friends.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     api.Friends.GetAsync(Constants.UserId, Constants.FriendListId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;long&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">The user ID for the user whose friends you want to return.</param>
        /// <param name="flid">Returns the friends in a friend list.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The List of friend IDs returned are the friends that are visible to the Facebook Platform. If no friends are found, the method returns an empty friends_get_response element.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public void GetAsync(long uid, long flid, GetFriendsCallback callback, Object state)
        {
            Get(uid, flid, true, callback, state);
        }

        /// <summary>
        /// Returns the identifiers for the current user's Facebook friends who have authorized the specific calling application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     api.Friends.GetAppUsersAsync(AsyncDemoCompleted, null);
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
        /// <returns>The friend IDs returned are those friends who have authorized the calling application, which is a subset of the friends returned from the friends.get method. If no friends are found, the method returns an empty friends_getAppUsers_response element.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public void GetAppUsersAsync(GetAppUsersCallback callback, Object state)
        {
            GetAppUsers(true, callback, state);
        }

        /// <summary>
        /// Returns the user objects for the current user's Facebook friends who have authorized the specific calling application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     api.Friends.GetAppUsersObjectsAsync(AsyncDemoCompleted, null);
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
        /// <returns>The friend user objects returned are those friends who have authorized the calling application, which is a subset of the friends returned from the friends.get method.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public void GetAppUsersObjectsAsync(Users.GetInfoCallback callback, Object state)
		{
			GetAppUsersObjects(true, callback, state);
		}

        /// <summary>
        /// Returns the user objects for the current user's Facebook friends.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     api.Friends.GetUserObjectsAsync(AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;user&gt; result, Object state, FacebookException e)
        /// {
        ///    var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns the user objects for the current user's friends.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public void GetUserObjectsAsync(Users.GetInfoCallback callback, Object state)
		{
			GetUserObjectsAsync(-1, -1, callback, state);
		}

        /// <summary>
        /// Returns the user objects for the specified user's Facebook friends.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     api.Friends.GetUserObjectsAsync(Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;user&gt; result, Object state, FacebookException e)
        /// {
        ///    var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">The user ID for the user whose friends you want to return.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns the user objects for the specified user's friends.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public void GetUserObjectsAsync(long uid, Users.GetInfoCallback callback, Object state)
		{
			GetUserObjectsAsync(uid, -1, callback, state);
		}

        /// <summary>
        /// Returns the user objects for the specified user's Facebook friends.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     api.Friends.GetUserObjectsAsync(Constants.UserId, Constants.FriendListId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;user&gt; result, Object state, FacebookException e)
        /// {
        ///    var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">The user ID for the user whose friends you want to return.</param>
        /// <param name="flid">Returns the friends in a friend list.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns the user objects for the specified user's friends.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public void GetUserObjectsAsync(long uid, long flid, Users.GetInfoCallback callback, Object state)
		{
			GetUserObjects(uid, flid, true, callback, state);
		}

        /// <summary>
        /// Returns the identifiers for the current user's Facebook friend lists.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     api.Friends.GetListsAsync(AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;friendlist&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The friend list IDs returned are the lists associated with the subject user.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public void GetListsAsync(GetListsCallback callback, Object state)
        {
            GetLists(true, callback, state);
        }

        /// <summary>
        /// Returns the identifiers for the requested users' Mutual Facebook friends.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     api.Friends.GetMutualFriendsAsync(Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;long&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="target_uid">The user ID of one of the target user whose mutual friends you want to retrieve.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns an List of user IDs of the mutual friends, or an error code.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public void GetMutualFriendsAsync(long target_uid, GetMutualFriendsCallback callback, Object state)
		{
			GetMutualFriendsAsync(target_uid, null, callback, state);
		}

        /// <summary>
        /// Returns the identifiers for the requested users' Mutual Facebook friends.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     api.Friends.GetMutualFriendsAsync(Constants.Friend_UserId1, Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;long&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="target_uid">The user ID of one of the target user whose mutual friends you want to retrieve.</param>
        /// <param name="source_uid">The user ID of the other user for which you are getting mutual friends of. Defaults to the current session user.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns an List of user IDs of the mutual friends, or an error code.</returns>
        /// <remarks>The first array specifies one half of each pair, the second array the other half; therefore, they must be of equal size.</remarks>
        public void GetMutualFriendsAsync(long target_uid, long? source_uid, GetMutualFriendsCallback callback, Object state)
		{
			GetMutualFriends(target_uid, source_uid, true, callback, state);
		}

        #endregion Asynchronous Methods

        #endregion Public Methods
        
        #region Private Methods

        private IList<friend_info> AreFriends(List<long> uids1, List<long> uids2, bool isAsync, AreFriendsCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string>
			                    	{
			                    		{"method", "facebook.friends.areFriends"}
			                    	};
			Utilities.AddList(parameterList, "uids1", uids1);
            Utilities.AddList(parameterList, "uids2", uids2);

            if(isAsync)
            {
                SendRequestAsync<friends_areFriends_response, IList<friend_info>>(parameterList, new FacebookCallCompleted<IList<friend_info>>(callback), state, "friend_info");
                return null;
            }

			var response = SendRequest<friends_areFriends_response>(parameterList);
			return response == null ? null : response.friend_info;
		}

		private IList<long> Get(long uid, long flid, bool isAsync, GetFriendsCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> {{"method", "facebook.friends.get"}};
            Utilities.AddOptionalParameter(parameterList, "flid", flid);
            Utilities.AddOptionalParameter(parameterList, "uid", uid);

             if(isAsync)
            {
                SendRequestAsync<friends_get_response, IList<long>>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<IList<long>>(callback), state, "uid");
                return null;
            }

			 var response = SendRequest<friends_get_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			 return response == null ? null : response.uid;
   		}

		private IList<long> GetAppUsers(bool isAsync, GetAppUsersCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> {{"method", "facebook.friends.getAppUsers"}};

             if(isAsync)
            {
                SendRequestAsync<friends_getAppUsers_response, IList<long>>(parameterList, new FacebookCallCompleted<IList<long>>(callback), state, "uid");
                return null;
            }

			 var response = SendRequest<friends_getAppUsers_response>(parameterList);
			 return response == null ? null : response.uid;
    	}

        private IList<user> GetAppUsersObjects(bool isAsync, Users.GetInfoCallback callback, Object state)
        {
            if (Batch.IsActive)
            {
                throw new Exception("Extended API methods are not supported within a batch");
            }

			if (isAsync)
			{
				GetAppUsersAsync(new GetAppUsersCallback(OnGetAppUsersForObjectsCompleted), new object[] { callback, state });
				return null;
			}

            var users = GetAppUsers(false, null, null);
            return _users.GetInfo(new List<long>(users));// StringHelper.ConvertToCommaSeparated(users));
        }

		private IList<friendlist> GetLists(bool isAsync, GetListsCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.friends.getLists" } };

			if (isAsync)
			{
				SendRequestAsync<friends_getLists_response, IList<friendlist>>(parameterList, new FacebookCallCompleted<IList<friendlist>>(callback), state, "friendlist");
				return null;
			}

			var response = SendRequest<friends_getLists_response>(parameterList);
			return response == null ? null : response.friendlist;
		}

		private List<long> GetMutualFriends(long target_uid, long? source_uid, bool isAsync, GetMutualFriendsCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.friends.getMutualFriends" } };
			Utilities.AddRequiredParameter(parameterList, "target_uid", target_uid);
			Utilities.AddOptionalParameter(parameterList, "source_uid", source_uid);

			if (isAsync)
			{
				SendRequestAsync<friends_getMutualFriends_response, IList<long>>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<IList<long>>(callback), state, "uid");
				return null;
			}

#if !SILVERLIGHT
			var response = SendRequest<friends_getMutualFriends_response>(parameterList, Session is DesktopSession || source_uid == null);
#else
            var response = SendRequest<friends_getMutualFriends_response>(parameterList, source_uid == null);
#endif
			return response == null ? null : response.uid;
		}

        private IList<user> GetUserObjects(long uid, long flid, bool isAsync, Users.GetInfoCallback callback, Object state)
        {
            if (Batch.IsActive)
            {
                throw new Exception("Extended API methods are not supported within a batch");
            }

			// TODO: change this method to use a FQL query instead

            var parameterList = new Dictionary<string, string> { { "method", "facebook.friends.get" } };
            Utilities.AddOptionalParameter(parameterList, "flid", flid);
            Utilities.AddOptionalParameter(parameterList, "uid", uid);

            if (isAsync)
            {
				SendRequestAsync<friends_get_response, IList<long>>(parameterList, (friends, state2, e) => OnGetFriendsCompleted(friends, callback, state2, e), state, "uid");
                return null;
            }

            var response = SendRequest<friends_get_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey)).uid;
			return _users.GetInfo(response);// StringHelper.ConvertToCommaSeparated(response));
        }

		private void OnGetFriendsCompleted(IList<long> friends, Users.GetInfoCallback callback, Object state, FacebookException e)
		{
			_users.GetInfoAsync(StringHelper.ConvertToCommaSeparated(friends), callback, state);
		}

		private void OnGetAppUsersForObjectsCompleted(IList<long> users, Object state, FacebookException e)
		{
			Object[] stateObjects = (Object[])state;
			var callback = (Users.GetInfoCallback)stateObjects[0];
			var originalState = stateObjects[1];
			_users.GetInfoAsync(callback, originalState);
		}

        #endregion Private Methods
        
        #endregion Methods

        #region Delegates

        /// <summary>
        /// Delegate called when AreFriends call completed
        /// </summary>
        /// <param name="friendInfo">List of friend_info objects</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void AreFriendsCallback(IList<friend_info> friendInfo, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetFriends call completed
        /// </summary>
        /// <param name="friends">List of friends</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        //public delegate void GetFriendsCallback(IList<long> friends, Object state, FacebookException e);
        public delegate void GetFriendsCallback(IList<long> friends, Object state, FacebookException e);
        
        /// <summary>
        /// Delegate called when GetAppUsers call completed
        /// </summary>
        /// <param name="users">List of users</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetAppUsersCallback(IList<long> users, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetFriendLists call completed
        /// </summary>
        /// <param name="list">List of friends lists</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetListsCallback(IList<friendlist> list, Object state, FacebookException e);

		/// <summary>
		/// Delegate called when GetMutualFriends call completed
		/// </summary>
		/// <param name="friends">List of friends</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>
		/// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void GetMutualFriendsCallback(IList<long> friends, Object state, FacebookException e);

		/// <summary>
		/// Delegate called when GetUserObjects call completed
		/// </summary>
		/// <param name="users">List of user objects</param>
		/// <param name="state">An object containing state information for this asynchronous request</param>
		/// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void GetUserObjectsCallback(IList<user> users, Object state, FacebookException e);

        #endregion Delegates
    }
}

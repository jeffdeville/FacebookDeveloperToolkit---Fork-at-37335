using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
    /// <summary>
    /// Facebook Groups API methods.
    /// </summary>
    public class Groups : RestBase, Facebook.Rest.IGroups
    {
        #region Methods

        #region Constructor

        /// <summary>
        /// Public constructor for facebook.Groups
        /// </summary>
        /// <param name="session">Needs a connected Facebook Session object for making requests</param>
        public Groups(IFacebookSession session)
            : base(session)
        {
        }

        #endregion Constructor

        #region Public Methods

#if !SILVERLIGHT
        
        #region Synchronous Methods

        /// <summary>
        /// Returns all visible groups according to the filters specified.  
        /// This may be used to find all groups of which a user is as member, or to query specific gids.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Groups.Get();
        /// </code>
        /// </example>
        /// <returns>This method returns all groups satisfying the filters specified. The method can be used to return all groups associated with user, or query a specific set of events by a list of gids. If both the uid and gids parameters are provided, the method returns all groups in the set of gids, with which the user is associated. If the gids parameter is omitted, the method returns all groups associated with the provided user.</returns>
        /// <remarks>Group creators will be visible to an application only if the creator has not turned off access to the Platform or used the application'; If the creator has opted out, the creator element will appear as nil=true.</remarks>
        public IList<group> Get()
        {
            return Get(Session.UserId, null);
        }

        /// <summary>
        /// Returns all visible groups according to the filters specified.  
        /// This may be used to find all groups of which a user is as member, or to query specific gids.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Groups.Get(Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="uid">Filter by groups associated with a user with this uid</param>
        /// <returns>This method returns all groups satisfying the filters specified. The method can be used to return all groups associated with user, or query a specific set of events by a list of gids. If both the uid and gids parameters are provided, the method returns all groups in the set of gids, with which the user is associated. If the gids parameter is omitted, the method returns all groups associated with the provided user.</returns>
        /// <remarks>Group creators will be visible to an application only if the creator has not turned off access to the Platform or used the application'; If the creator has opted out, the creator element will appear as nil=true.</remarks>
        public IList<group> Get(long uid)
        {
            return Get(uid, null);
        }

        /// <summary>
        /// Returns all visible groups according to the filters specified.  
        /// This may be used to find all groups of which a user is as member, or to query specific gids.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// List&lt;long&gt; gids = new List&lt;long&gt; { Constants.GroupId };
        /// var result = api.Groups.Get(gids);
        /// </code>
        /// </example>
        /// <param name="gids">Filter by this list of group ids. This is a List of gids.</param>
        /// <returns>This method returns all groups satisfying the filters specified. The method can be used to return all groups associated with user, or query a specific set of events by a list of gids. If both the uid and gids parameters are provided, the method returns all groups in the set of gids, with which the user is associated. If the gids parameter is omitted, the method returns all groups associated with the provided user.</returns>
        /// <remarks>Group creators will be visible to an application only if the creator has not turned off access to the Platform or used the application'; If the creator has opted out, the creator element will appear as nil=true.</remarks>
        public IList<group> Get(List<long> gids)
        {
            return Get(0, gids);
        }

        /// <summary>
        /// Returns all visible groups according to the filters specified.  
        /// This may be used to find all groups of which a user is as member, or to query specific gids.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// List&lt;long&gt; gids = new List&lt;long&gt; { Constants.GroupId };
        /// var result = api.Groups.Get(Constants.UserId, gids);
        /// </code>
        /// </example>
        /// <param name="uid">Filter by groups associated with a user with this uid</param>
        /// <param name="gids">Filter by this list of group ids. This is a List of gids.</param>
        /// <returns>This method returns all groups satisfying the filters specified. The method can be used to return all groups associated with user, or query a specific set of events by a list of gids. If both the uid and gids parameters are provided, the method returns all groups in the set of gids, with which the user is associated. If the gids parameter is omitted, the method returns all groups associated with the provided user.</returns>
        /// <remarks>Group creators will be visible to an application only if the creator has not turned off access to the Platform or used the application'; If the creator has opted out, the creator element will appear as nil=true.</remarks>
        public IList<group> Get(long uid, List<long> gids)
        {
            return Get(uid, gids, false, null, null);
        }

        /// <summary>
        /// Returns membership list data associated with a group.
        /// </summary>
        /// <param name="gid">Group ID for which to retrieve member list.</param>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Groups.GetMembers(Constants.GroupId);
        /// </code>
        /// </example>
        /// <returns>This method returns four (possibly empty) lists of users associated with a group, keyed on their associations. The members list will contain the officers and admins lists, but will not overlap with the not_replied list.</returns>
        public group_members GetMembers(long gid)
        {
            return GetMembers(gid, false, null, null);
        }

        #endregion Synchronous Methods

#endif

        #region Asynchronous Methods

        /// <summary>
        /// Returns all visible groups according to the filters specified.  
        /// This may be used to find all groups of which a user is as member, or to query specific gids.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Groups.GetAsync(AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;group&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns all groups satisfying the filters specified. The method can be used to return all groups associated with user, or query a specific set of events by a list of gids. If both the uid and gids parameters are provided, the method returns all groups in the set of gids, with which the user is associated. If the gids parameter is omitted, the method returns all groups associated with the provided user.</returns>
        /// <remarks>Group creators will be visible to an application only if the creator has not turned off access to the Platform or used the application'; If the creator has opted out, the creator element will appear as nil=true.</remarks>
        public void GetAsync(GetCallback callback, Object state)
		{
			GetAsync(Session.UserId, null, callback, state);
		}

        /// <summary>
        /// Returns all visible groups according to the filters specified.  
        /// This may be used to find all groups of which a user is as member, or to query specific gids.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Groups.GetAsync(Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;group&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">Filter by groups associated with a user with this uid</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns all groups satisfying the filters specified. The method can be used to return all groups associated with user, or query a specific set of events by a list of gids. If both the uid and gids parameters are provided, the method returns all groups in the set of gids, with which the user is associated. If the gids parameter is omitted, the method returns all groups associated with the provided user.</returns>
        /// <remarks>Group creators will be visible to an application only if the creator has not turned off access to the Platform or used the application'; If the creator has opted out, the creator element will appear as nil=true.</remarks>
        public void GetAsync(long uid, GetCallback callback, Object state)
		{
			GetAsync(uid, null, callback, state);
		}

        /// <summary>
        /// Returns all visible groups according to the filters specified.  
        /// This may be used to find all groups of which a user is as member, or to query specific gids.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     List&lt;long&gt; gids = new List&lt;long&gt; { Constants.GroupId };
        ///     api.Groups.GetAsync(gids, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;group&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="gids">Filter by this list of group ids. This is a List of gids.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns all groups satisfying the filters specified. The method can be used to return all groups associated with user, or query a specific set of events by a list of gids. If both the uid and gids parameters are provided, the method returns all groups in the set of gids, with which the user is associated. If the gids parameter is omitted, the method returns all groups associated with the provided user.</returns>
        /// <remarks>Group creators will be visible to an application only if the creator has not turned off access to the Platform or used the application'; If the creator has opted out, the creator element will appear as nil=true.</remarks>
        public void GetAsync(List<long> gids, GetCallback callback, Object state)
        {
            GetAsync(Session.UserId, gids, callback, state);
        }

        /// <summary>
        /// Returns all visible groups according to the filters specified.  
        /// This may be used to find all groups of which a user is as member, or to query specific gids.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     List&lt;long&gt; gids = new List&lt;long&gt; { Constants.GroupId };
        ///     api.Groups.GetAsync(Constants.UserId, gids, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;group&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">Filter by groups associated with a user with this uid</param>
        /// <param name="gids">Filter by this list of group ids. This is a List of gids.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns all groups satisfying the filters specified. The method can be used to return all groups associated with user, or query a specific set of events by a list of gids. If both the uid and gids parameters are provided, the method returns all groups in the set of gids, with which the user is associated. If the gids parameter is omitted, the method returns all groups associated with the provided user.</returns>
        /// <remarks>Group creators will be visible to an application only if the creator has not turned off access to the Platform or used the application'; If the creator has opted out, the creator element will appear as nil=true.</remarks>
        public void GetAsync(long uid, List<long> gids, GetCallback callback, Object state)
        {
            Get(uid, gids, true, callback, state);
        }

        /// <summary>
        /// Returns membership list data associated with a group.
        /// </summary>
        /// <param name="gid">Group ID for which to retrieve member list.</param>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Groups.GetMembersAsync(Constants.GroupId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(group_members result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns four (possibly empty) lists of users associated with a group, keyed on their associations. The members list will contain the officers and admins lists, but will not overlap with the not_replied list.</returns>
        public void GetMembersAsync(long gid, GetMembersCallback callback, Object state)
        {
            GetMembers(gid, true, callback, state);
        }
        
        #endregion Asynchronous Methods

        #endregion Public Methods
        
        #region Private Methods

        private IList<group> Get(long uid, List<long> gids, bool isAsync, GetCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.groups.get" } };
            Utilities.AddOptionalParameter(parameterList, "uid", uid);
            Utilities.AddList(parameterList, "gids", gids);

            if (isAsync)
            {
				SendRequestAsync<groups_get_response, IList<group>>(parameterList, new FacebookCallCompleted<IList<group>>(callback), state, "group");
                return null;
            }

			var response = SendRequest<groups_get_response>(parameterList);
			return response == null ? null : response.group;
        }

        private group_members GetMembers(long gid, bool isAsync, GetMembersCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.groups.getMembers" } };
            Utilities.AddRequiredParameter(parameterList, "gid", gid);

            if (isAsync)
            {
                SendRequestAsync(parameterList, new FacebookCallCompleted<group_members>(callback), state);
                return null;
            }

			return SendRequest<groups_getMembers_response>(parameterList);
        }

        #endregion Private Methods

        #endregion Methods

        #region Delegates

        /// <summary>
        /// Delegate called when GetGroups call is completed.
        /// </summary>
        /// <param name="groups">List of user objects</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetCallback(IList<group> groups, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetGroupMembers call is completed.
        /// <param name="members">List of group members</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        /// </summary>
        public delegate void GetMembersCallback(group_members members, Object state, FacebookException e);

        #endregion Delegates
    }
}
using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
    /// <summary>
    /// Facebook Profile API methods.
    /// </summary>
    public class Profile : RestBase, Facebook.Rest.IProfile
    {
        #region Methods

        #region Constructor

        /// <summary>
        /// Public constructor for facebook.Profile
        /// </summary>
        /// <param name="session">Needs a connected Facebook Session object for making requests</param>
        public Profile(IFacebookSession session)
            : base(session)
        {
        }

        #endregion Constructor

        #region Public Methods

#if !SILVERLIGHT

        #region Synchronous Methods

        /// <summary>
        /// Gets the FBML that is currently set for a user's profile.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// var result = api.Profile.GetFBML(Constants.UserId, 1);
        /// </code>
        /// </example>
        /// <param name="uid">The user whose profile FBML is to be fetched, or the page ID in case of a Page. This parameter applies only to Web applications and is required by them only if the session_key is not specified. Facebook ignores this parameter if it is passed by a desktop application. </param>
        /// <param name="type">The type of profile box to retrieve. Specify 1 for the original style (wide and narrow column boxes), 2 for profile_main box. (default value is 1)</param>
        /// <returns>The FBML markup from the user's profile.</returns>
        /// <remarks>It is not a violation of Facebook Privacy policy if you use this method to retrieve content originally rendered by your application from a user's profile, even if Facebook privacy restrictions would otherwise keep you from seeing that user’s profile (for example, you are not friends with the user in question). Cases where this would arise include verifying content posted by one user of your application to another user’s profile complies with the Facebook Developer Terms of Service. </remarks>
        public string GetFBML(long uid, int type)
        {
            return GetFBML(uid, type, false, null, null);
        }

        /// <summary>
        /// Sets the FBML for a user's profile, including the content for both the profile box and the profile actions.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// api.Session.UserId = Constants.UserId;
        /// string fbml = string.Format("&lt;fb:name uid=\"{0}\"&gt; is testing setFBML.&lt;/fb:name&gt;", Constants.UserId);
        /// var result = api.Profile.SetFBML(Constants.UserId, fbml, fbml, fbml);
        /// </code>
        /// </example>
        /// <param name="uid">The user whose profile is to be updated. Not allowed for desktop applications (since the application secret is essentially public).</param>
        /// <param name="profile">The FBML intended for the application profile box on the user's profile. </param>
        /// <param name="profile_main">The FBML intended for the narrow profile box on the Wall tab of the user's profile.</param>
        /// <param name="mobile_profile">The FBML intended for mobile devices. </param>
        /// <returns>This method returns true if FBML was set for a profile.</returns>
        /// <remarks>The FBML is cached on Facebook's server for that particular user and that particular application. To change it, profile.setFBML must be called through a canvas page or some other script (such as a cron job) that makes use of the Facebook API. </remarks>
        public bool SetFBML(long uid, string profile, string profile_main, string mobile_profile)
        {
            return SetFBML(uid, profile, profile_main, mobile_profile, false, null, null);
        }

        /// <summary>
        /// Returns the specified user's application info section for the calling application.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// var result = api.Profile.GetInfo(Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="uid">The user ID of the user who added the application info section.</param>
        /// <returns>This method returns a user_info object for the specified uid.</returns>
        public user_info GetInfo(long uid)
        {
            return GetInfo(uid, false, null, null);
        }

        /// <summary>
        /// Returns the options associated with the specified field for an application info section.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// api.Session.UserId = Constants.UserId;
        /// var result = api.Profile.GetInfoOptions("Test Field");
        /// </code>
        /// </example>
        /// <param name="field">The title of the field.</param>
        /// <returns>This method returns a List of info_item objects (typeahead options) for the specified field.</returns>
        public IList<info_item> GetInfoOptions(string field)
        {
            return GetInfoOptions(field, false, null, null);
        }

        /// <summary>
        /// Configures an application info section that the specified user can install on the Info tab of her profile.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// api.Session.UserId = Constants.UserId;
        /// var item = new info_item();
        /// item.label = "Test info item";
        /// item.link = "www.claritycon.com";
        /// var field = new info_field();
        /// field.items = new info_fieldItems();
        /// field.items.info_item.Add(item);
        /// field.field = "Test field";
        /// var result = api.Profile.SetInfo("Test Field", 5, new List&lt;info_field&gt;() { field }, Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="title">The title or header of the application info section. </param>
        /// <param name="type">Specify 1 for a text-only field-item configuration or 5 for a thumbnail configuration.</param>
        /// <param name="info_fields">A List of elements comprising an application info section, including the field (the title of the field) and an array of info_item objects (each object has a label and a link, and optionally contains image, description, and sublabel fields).</param>
        /// <param name="uid">The user ID of the user adding the application info section. </param>
        /// <returns>This method returns true if the application info section was successfully set.</returns>
        public bool SetInfo(string title, int type, List<info_field> info_fields, long uid)
        {
            return SetInfo(title, type, info_fields, uid, false, null, null);
        }

        /// <summary>
        /// Specifies the objects for a field for an application info section.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// api.Session.UserId = Constants.UserId;
        /// var item1 = new info_item {link = "www.claritycon.com", label = "SetInfoOptions test 1"};
        /// var result = api.Profile.SetInfoOptions("Test Field", new List&lt;info_item&gt;() { item1 });
        /// </code>
        /// </example>
        /// <param name="field">The title of the field.</param>
        /// <param name="options">A List of items for a thumbnail, including a label and a link, and optionally contains image, description, and sublabel items.</param>
        /// <returns>This method returns true if the available items for a filed in an application info section was successfully set.</returns>
        public bool SetInfoOptions(string field, List<info_item> options)
        {
            return SetInfoOptions(field, options, false, null, null);
        }

        #endregion Synchronous Methods

#endif

        #region Asynchronous Methods

        /// <summary>
        /// Gets the FBML that is currently set for a user's profile.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     api.Profile.GetFBMLAsync(Constants.UserId, 1, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(string result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">The user whose profile FBML is to be fetched, or the page ID in case of a Page. This parameter applies only to Web applications and is required by them only if the session_key is not specified. Facebook ignores this parameter if it is passed by a desktop application. </param>
        /// <param name="type">The type of profile box to retrieve. Specify 1 for the original style (wide and narrow column boxes), 2 for profile_main box. (default value is 1)</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>The FBML markup from the user's profile.</returns>
        /// <remarks>It is not a violation of Facebook Privacy policy if you use this method to retrieve content originally rendered by your application from a user's profile, even if Facebook privacy restrictions would otherwise keep you from seeing that user’s profile (for example, you are not friends with the user in question). Cases where this would arise include verifying content posted by one user of your application to another user’s profile complies with the Facebook Developer Terms of Service. </remarks>
        public void GetFBMLAsync(long uid, int type, GetFBMLCallback callback, Object state)
        {
            GetFBML(uid, type, true, callback, state);
        }

        /// <summary>
        /// Sets the FBML for a user's profile, including the content for both the profile box and the profile actions.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Session.UserId = Constants.UserId;
        ///     string fbml = string.Format("&lt;fb:name uid=\"{0}\"&gt; is testing setFBML.&lt;/fb:name&gt;", Constants.UserId);
        ///     api.Profile.SetFBMLAsync(Constants.UserId, fbml, fbml, fbml, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">The user whose profile is to be updated. Not allowed for desktop applications (since the application secret is essentially public).</param>
        /// <param name="profile">The FBML intended for the application profile box on the user's profile. </param>
        /// <param name="profile_main">The FBML intended for the narrow profile box on the Wall tab of the user's profile.</param>
        /// <param name="mobile_profile">The FBML intended for mobile devices. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true if FBML was set for a profile.</returns>
        /// <remarks>The FBML is cached on Facebook's server for that particular user and that particular application. To change it, profile.setFBML must be called through a canvas page or some other script (such as a cron job) that makes use of the Facebook API. </remarks>
        public void SetFBMLAsync(long uid, string profile, string profile_main, string mobile_profile, SetFBMLCallback callback, Object state)
        {
            SetFBML(uid, profile, profile_main, mobile_profile, true, callback, state);
        }

        /// <summary>
        /// Returns the specified user's application info section for the calling application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     api.Profile.GetInfoAsync(Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(user_info result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">The user ID of the user who added the application info section.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns a user_info object for the specified uid.</returns>
        public void GetInfoAsync(long uid, GetInfoCallback callback, Object state)
        {
            GetInfo(uid,true, callback, state);
        }

        /// <summary>
        /// Returns the options associated with the specified field for an application info section.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Session.UserId = Constants.UserId;
        ///     api.Profile.GetInfoOptionsAsync("Test Field", AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;info_item&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="field">The title of the field.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns a List of info_item objects (typeahead options) for the specified field.</returns>
        public void GetInfoOptionsAsync(string field, GetInfoOptionsCallback callback, Object state)
        {
            GetInfoOptions(field, true, callback, state);
        }

        /// <summary>
        /// Configures an application info section that the specified user can install on the Info tab of her profile.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Session.UserId = Constants.UserId;
        ///     var item = new info_item();
        ///     item.label = "Test info item";
        ///     item.link = "www.claritycon.com";
        ///     var field = new info_field();
        ///     field.items = new info_fieldItems();
        ///     field.items.info_item.Add(item);
        ///     field.field = "Test field";
        ///     api.Profile.SetInfoAsync("Test Field", 5, new List&lt;info_field&gt;() { field }, Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="title">The title or header of the application info section. </param>
        /// <param name="type">Specify 1 for a text-only field-item configuration or 5 for a thumbnail configuration.</param>
        /// <param name="info_fields">A List of elements comprising an application info section, including the field (the title of the field) and an array of info_item objects (each object has a label and a link, and optionally contains image, description, and sublabel fields).</param>
        /// <param name="uid">The user ID of the user adding the application info section. </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true if the application info section was successfully set.</returns>
        public void SetInfoAsync(string title, int type, List<info_field> info_fields, long uid, SetInfoCallback callback, Object state)
        {
            SetInfo(title, type, info_fields, uid, true, callback, state);
        }

        /// <summary>
        /// Specifies the objects for a field for an application info section.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Session.UserId = Constants.UserId;
        ///     var item1 = new info_item { link = "www.claritycon.com", label = "SetInfoOptions test 1" };
        ///     api.Profile.SetInfoOptionsAsync("Test Field", new List&lt;info_item&gt;() { item1 }, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="field">The title of the field.</param>
        /// <param name="options">A List of items for a thumbnail, including a label and a link, and optionally contains image, description, and sublabel items.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true if the available items for a filed in an application info section was successfully set.</returns>
        public void SetInfoOptionsAsync(string field, List<info_item> options, SetInfoCallback callback, Object state)
        {
            SetInfoOptions(field, options, true, callback, state);
        }

        #endregion Asynchronous Methods

        #endregion Public Methods
        
        #region Private Methods
        
        private string GetFBML(long uid, int type, bool isAsync, GetFBMLCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.profile.getFBML" } };
            Utilities.AddOptionalParameter(parameterList, "uid", uid);
            Utilities.AddOptionalParameter(parameterList, "type", type);

            if (isAsync)
            {
                SendRequestAsync<profile_getFBML_response, string>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<string>(callback), state);
                return null;
            }

            var response = SendRequest<profile_getFBML_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? null : response.TypedValue;
        }

        private bool SetFBML(long uid, string profile, string profile_main, string mobile_profile,
            bool isAsync, SetFBMLCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.profile.setFBML" } };
            Utilities.AddOptionalParameter(parameterList, "uid", uid);
            Utilities.AddOptionalParameter(parameterList, "profile", profile);
            Utilities.AddOptionalParameter(parameterList, "profile_main", profile_main);
            Utilities.AddOptionalParameter(parameterList, "mobile_profile", mobile_profile);

            if (isAsync)
            {
                SendRequestAsync<profile_setFBML_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
                return true;
            }

            var response = SendRequest<profile_setFBML_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? true : response.TypedValue;
        }

        private user_info GetInfo(long uid, bool isAsync, GetInfoCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.profile.getInfo" } };
            Utilities.AddRequiredParameter(parameterList, "uid", uid);

            if (isAsync)
            {
                SendRequestAsync(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<profile_getInfo_response>(callback), state);
                return null;
            }

            return SendRequest<profile_getInfo_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
        }

        private IList<info_item> GetInfoOptions(string field, bool isAsync, GetInfoOptionsCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.profile.getInfoOptions" } };
            Utilities.AddRequiredParameter(parameterList, "field", field);

            if (isAsync)
            {
                SendRequestAsync<profile_getInfoOptions_response, IList<info_item>>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<IList<info_item>>(callback), state, "info_item");
                return null;
            }

            var response = SendRequest<profile_getInfoOptions_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? null : response.info_item;
        }

        private bool SetInfo(string title, int type, List<info_field> info_fields, long uid, bool isAsync, SetInfoCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.profile.setInfo" } };
            Utilities.AddRequiredParameter(parameterList, "title", title);
            Utilities.AddRequiredParameter(parameterList, "type", type);

            var fieldList = new List<string>();
            foreach (var field in info_fields)
            {
                var itemList = new List<string>();
                foreach (var item in field.items.info_item)
                {
                    var itemDict = new Dictionary<string, string>{
						{"label", item.label},
						{"sublabel", item.sublabel},
						{"link", item.link},
						{"image", item.image},
						{"description", item.description}
					};
                    itemList.Add(JSONHelper.ConvertToJSONAssociativeArray(itemDict));
                }

                var fieldDict = new Dictionary<string, string>{
					{"field", field.field},
					{"items", JSONHelper.ConvertToJSONArray(itemList)}
				};
                fieldList.Add(JSONHelper.ConvertToJSONAssociativeArray(fieldDict));
            }

            Utilities.AddJSONArray(parameterList, "info_fields", fieldList);
            Utilities.AddRequiredParameter(parameterList, "uid", uid);

            if (isAsync)
            {
                SendRequestAsync<profile_setInfo_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
                return true;
            }

            var response = SendRequest<profile_setInfo_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? false : response.TypedValue;
        }

        private bool SetInfoOptions(string field, List<info_item> options, bool isAsync, SetInfoCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.profile.setInfoOptions" } };
            Utilities.AddRequiredParameter(parameterList, "field", field);

            var list = new List<string>();
            foreach (var item in options)
            {
                var dict = new Dictionary<string, string>{
                    {"label", item.label},
                    {"sublabel", item.sublabel},
                    {"link", item.link},
                    {"image", item.image},
                    {"description", item.description}
                };
                list.Add(JSONHelper.ConvertToJSONAssociativeArray(dict));
            }

            Utilities.AddJSONArray(parameterList, "options", list);

            if (isAsync)
            {
                SendRequestAsync<profile_setInfoOptions_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
                return true;
            }

            var response = SendRequest<profile_setInfoOptions_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? false : response.TypedValue;
        }

        #endregion Private Methods
        
        #endregion Methods

        #region Delegates

        /// <summary>
        /// Delegate called when GetFbml call completed
        /// </summary>
        /// <param name="result">result of operation</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetFBMLCallback(string result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when SetFbml call completed
        /// </summary>
        /// <param name="result">result of operation</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void SetFBMLCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetProfileInfo call completed
        /// </summary>
        /// <param name="profileInfo">list of profile info</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetInfoCallback(user_info profileInfo, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetProfileInfoOptions call completed
        /// </summary>
        /// <param name="result">result of operation</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetInfoOptionsCallback(IList<info_item> result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when SetProfileInfo call completed
        /// </summary>
        /// <param name="result">result of operation</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void SetInfoCallback(bool result, Object state, FacebookException e);
        
        #endregion Delegates
    }
}
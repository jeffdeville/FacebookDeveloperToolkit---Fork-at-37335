#if !SILVERLIGHT

using System;
using System.Web;
using System.Web.Configuration;
using Facebook.Rest;
using Facebook.Schema;
using Facebook.Utility;
using System.Collections.Generic;

namespace Facebook.Session
{
	internal static class QueryParameters
	{
		public const string AuthToken = "auth_token";
		public const string InCanvas = "fb_sig_in_canvas";
        public const string InIframe = "fb_sig_in_iframe";
        public const string InProfileTab = "fb_sig_in_profile_tab";
		public const string SessionKey = "fb_sig_session_key";
		public const string User = "fb_sig_user";
		public const string ProfileSessionKey = "fb_sig_profile_session_key";
		public const string ProfileUser = "fb_sig_profile_user";
		public const string Expires = "fb_sig_expires";
	}

	internal class CachedSessionInfo
	{
		public CachedSessionInfo(string sessionKey, long userId, DateTime expiryTime)
		{
			SessionKey = sessionKey;
			UserId = userId;
			ExpiryTime = expiryTime;
		}

		public string SessionKey { get; set; }
		public long UserId { get; set; }
		public DateTime ExpiryTime { get; set; }
	}

	/// <summary>
	/// Represents session object for desktop apps
	/// </summary>
	public abstract class CanvasSession : FacebookSession
	{
		#region Public Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appKey">Application Key</param>
        /// <param name="appSecret">Application Secret</param>
        public CanvasSession(string appKey, string appSecret) : this(appKey, appSecret, true)
        {}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appKey">Application Key</param>
        /// <param name="appSecret">Application Secret</param>
        /// <param name="permissions">list of extended permissions to prompt for upon login</param>
        public CanvasSession(string appKey, string appSecret, List<Enums.ExtendedPermissions> permissions) : this(appKey, appSecret, permissions, true)
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appKey">Application Key</param>
        /// <param name="appSecret">Application Secret</param>
        /// <param name="readRequest">Whether to read the request or not</param>
        public CanvasSession(string appKey, string appSecret, bool readRequest) : this(appKey, appSecret, null, readRequest)
        {}
        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="appKey">Application Key</param>
		/// <param name="appSecret">Application Secret</param>
        /// <param name="permissions">list of extended permissions to prompt for upon login</param>
        /// <param name="readRequest">Whether to read the request or not</param>
        public CanvasSession(string appKey, string appSecret, List<Enums.ExtendedPermissions> permissions, bool readRequest)
		{
            RequiredPermissions = permissions;
			if (appKey == null || appSecret == null)
			{
				ApplicationKey = WebConfigurationManager.AppSettings["ApiKey"];
				ApplicationSecret = WebConfigurationManager.AppSettings["Secret"];
			}
			else
			{
				ApplicationKey = appKey;
				ApplicationSecret = appSecret;
			}
            if(readRequest)
                LoadFromRequest();
		}

        private void LoadFromRequest()
        {
            if (string.IsNullOrEmpty(ApplicationKey) || string.IsNullOrEmpty(ApplicationSecret))
            {
                throw new Exception(
                    "Session must have application key and secret before logging in." + Environment.NewLine +
                    "To set them in your web.config, use something like the following:" + Environment.NewLine +
                    "<appSettings>" + Environment.NewLine +
                    "   <add key=\"ApiKey\" value =\"YOURApiKEY\"/>" + Environment.NewLine +
                    "   <add key=\"Secret\" value =\"YOURSECRET\"/>" + Environment.NewLine +
                    "</appSettings>\"");
            }

            if (HttpContext.Current.Response == null || HttpContext.Current.Request == null)
            {
                throw new Exception("Session must have both an HttpRequest object and an HttpResponse object to login.");
            }

            bool inProfileTab = HttpContext.Current.Request[QueryParameters.InProfileTab] == "1";
            string sessionKeyFromRequest = inProfileTab ? HttpContext.Current.Request[QueryParameters.ProfileSessionKey] : HttpContext.Current.Request[QueryParameters.SessionKey];
            string authToken = HttpContext.Current.Request[QueryParameters.AuthToken];
            CachedSessionInfo cachedSessionInfo = LoadCachedSession();

            if (!string.IsNullOrEmpty(sessionKeyFromRequest))
            {
                SetSessionProperties(
                    sessionKeyFromRequest,
                    long.Parse(inProfileTab ? HttpContext.Current.Request[QueryParameters.ProfileUser] : HttpContext.Current.Request[QueryParameters.User]),
                    DateHelper.ConvertUnixTimeToDateTime(long.Parse(HttpContext.Current.Request[QueryParameters.Expires])));
            }
            else if (cachedSessionInfo != null && (HttpContext.Current.Request.HttpMethod == "POST" || !string.IsNullOrEmpty(authToken))) // only use cached info if user hasn't removed the app
            {
                SetSessionProperties(cachedSessionInfo.SessionKey, cachedSessionInfo.UserId, cachedSessionInfo.ExpiryTime);
            }
            else if (!string.IsNullOrEmpty(authToken))
            {
                session_info sessionInfo = new Api(this).Auth.GetSession(authToken);
                SetSessionProperties(sessionInfo.session_key, sessionInfo.uid, DateHelper.ConvertUnixTimeToDateTime(sessionInfo.expires));
            }

        }
		/// <summary>
		/// Logs in user
		/// </summary>
		public override void Login()
		{
            if (string.IsNullOrEmpty(SessionKey))
            {
                RedirectToLogin();
            }
            else
            {
                var permissionsString = CheckPermissions();
                if (!string.IsNullOrEmpty(permissionsString))
                {
                    PromptPermissions(GetPermissionsRedirect(GetPermissionUrl(permissionsString, GetNextUrl())));
                }
            }

		}
        /// <summary>
        /// Logs in user
        /// </summary>
        public string GetNextUrl()
        {
            var props = new Api(this).Admin.GetAppProperties(new List<string>() { "callback_url", "canvas_name" });
            if (props.ContainsKey("callback_url") && props.ContainsKey("canvas_name") && !string.IsNullOrEmpty(props["callback_url"]) && !string.IsNullOrEmpty(props["callback_url"]))
            {
                return HttpContext.Current.Request.Url.ToString().Replace(props["callback_url"], string.Format("http://apps.facebook.com/{0}/", props["canvas_name"]));
            }
            return null;
                    
        }

		/// <summary>
		/// Logs out user
		/// </summary>
		public override void Logout()
		{
		}

		#endregion

		#region Absract Methods

		internal abstract void RedirectToLogin();

        /// <summary>
        /// Prompt for permissions
        /// </summary>
        protected void PromptPermissions(string permissionsRedirect)
        {
            HttpContext.Current.Response.Write(permissionsRedirect);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// Get string for redirect response
        /// </summary>
        public abstract string GetRedirect();
        /// <summary>
        /// Get permissions redirect
        /// </summary>
        public abstract string GetPermissionsRedirect(string permissionsUrl);

		internal abstract void CacheSession();

		internal abstract CachedSessionInfo LoadCachedSession();
		
		#endregion

		#region Protected Methods

        /// <summary>
        /// Returns the login url for a canvas page including the api_key query param
        /// </summary>
        protected string GetLoginUrl()
		{
            string canvasParam = HttpContext.Current.Request[QueryParameters.InCanvas] == "1" || HttpContext.Current.Request[QueryParameters.InIframe] == "1" ? "&canvas" : string.Empty;
			return string.Format("http://www.facebook.com/login.php?api_key={0}&v=1.0{1}", ApplicationKey, canvasParam);
		}

		#endregion

		#region Private Methods

		private void SetSessionProperties(string sessionKey, long userId, DateTime expiryTime)
		{
			SessionKey = sessionKey;
			UserId = userId;
			ExpiryTime = expiryTime;

			CacheSession();
		}

		#endregion
	}
}

#endif
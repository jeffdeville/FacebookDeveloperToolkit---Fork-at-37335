using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Facebook.Schema;

namespace Facebook.Session
{
    /// <summary>
    /// Session object used by iframe canvas applications
    /// </summary>
    public class IFrameCanvasSession : CanvasSession
	{
		private const string SESSION_INFO_SESSION_KEY = "SessionInfo";
		private const string SESSION_KEY_COOKIE = "SessionKey";
		private const string USER_ID_COOKIE = "UserId";
		private const string EXPIRY_TIME_COOKIE = "ExpiryTime";

        /// <summary>
        /// Constructor
        /// </summary>
        public IFrameCanvasSession(string appKey, string appSecret)
			: base(appKey, appSecret)
		{
			UseHttpSession = true;
		}
        /// <summary>
        /// Constructor
        /// </summary>
        public IFrameCanvasSession(string appKey, string appSecret, List<Enums.ExtendedPermissions> permissions)
            : base(appKey, appSecret, permissions)
        {
            UseHttpSession = true;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public IFrameCanvasSession(string appKey, string appSecret, bool readRequest)
            : base(appKey, appSecret, readRequest)
        {
            UseHttpSession = true;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public IFrameCanvasSession(string appKey, string appSecret, List<Enums.ExtendedPermissions> permissions, bool readRequest)
            : base(appKey, appSecret, permissions, readRequest)
        {
            UseHttpSession = true;
        }

        /// <summary>
        /// Whether the Session just leverage HTTPSession or not
        /// </summary>
        public bool UseHttpSession { get; set; }

        /// <summary>
        /// the Session object
        /// </summary>
        public HttpSessionState HttpSession { get; set; }

		internal override void RedirectToLogin()
		{
            RedirectTopFrame(GetRedirect());
		}

		private void RedirectTopFrame(string redirect)
		{
            HttpContext.Current.Response.Write(redirect);
            HttpContext.Current.Response.End();
		}
        /// <summary>
        /// Get string for permissions redirect response
        /// </summary>
        public override string GetPermissionsRedirect(string permissionsUrl)
        {
            return "<script type=\"text/javascript\">\n" +
                           "if (parent != self) \n" +
                           "top.location.href = \"" + permissionsUrl + @"&v=1.0" + "\";\n" +
                           "else self.location.href = \"" + permissionsUrl + @"&v=1.0" + "\";\n" +
                           "</script>";
        }

        /// <summary>
        /// Get string for redirect response
        /// </summary>
        public override string GetRedirect()
        {
            string url = GetLoginUrl();
            return string.Format("<script type=\"text/javascript\">\n" +
                           "if (parent != self) \n" +
                           "top.location.href = \"" + url + @"&v=1.0" + "\";\n" +
                           "else self.location.href = \"" + url + @"&v=1.0" + "\";\n" +
                           "</script>");
        }

		internal override void CacheSession()
		{
			var sessionInfo = new CachedSessionInfo(SessionKey, UserId, ExpiryTime);

			if (UseHttpSession)
			{
				HttpSession[SESSION_INFO_SESSION_KEY] = sessionInfo;
			}
			else
			{
                HttpContext.Current.Response.Cookies.Set(new HttpCookie(SESSION_KEY_COOKIE, sessionInfo.SessionKey));
                HttpContext.Current.Response.Cookies.Set(new HttpCookie(USER_ID_COOKIE, sessionInfo.UserId.ToString()));
                HttpContext.Current.Response.Cookies.Set(new HttpCookie(EXPIRY_TIME_COOKIE, sessionInfo.ExpiryTime.ToString()));
			}
		}

		internal override CachedSessionInfo LoadCachedSession()
		{
			if (UseHttpSession)
			{
				return (CachedSessionInfo)HttpSession[SESSION_INFO_SESSION_KEY];
			}
			else
			{
                var sessionKeyCookie = HttpContext.Current.Request.Cookies[SESSION_KEY_COOKIE];
                var userIdCookie = HttpContext.Current.Request.Cookies[USER_ID_COOKIE];
                var expiryTimeCookie = HttpContext.Current.Request.Cookies[EXPIRY_TIME_COOKIE];

				if (sessionKeyCookie == null || userIdCookie == null || expiryTimeCookie == null)
				{
					return null;
				}
				else
				{
					return new CachedSessionInfo(sessionKeyCookie.Value, long.Parse(userIdCookie.Value), DateTime.Parse(expiryTimeCookie.Value));
				}	
			}
		}
	}
}

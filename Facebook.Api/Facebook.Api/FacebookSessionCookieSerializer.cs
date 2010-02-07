using System;
using System.Web;
using Facebook.Session;

namespace Facebook.Api
{
    public class FacebookSessionCookieSerializer
    {
        private const string SESSION_INFO_SESSION_KEY = "SessionInfo";
		private const string SESSION_KEY_COOKIE = "SessionKey";
		private const string USER_ID_COOKIE = "UserId";
		private const string EXPIRY_TIME_COOKIE = "ExpiryTime";

        public void CacheSession(IFacebookSession session, HttpCookieCollection responseCookies)
        {
            var sessionInfo = new CachedSessionInfo(session.SessionKey, session.UserId, session.ExpiryTime);

            responseCookies.Set(new HttpCookie(SESSION_KEY_COOKIE, sessionInfo.SessionKey));
            responseCookies.Set(new HttpCookie(USER_ID_COOKIE, sessionInfo.UserId.ToString()));
            responseCookies.Set(new HttpCookie(EXPIRY_TIME_COOKIE, sessionInfo.ExpiryTime.ToString()));
        }

        public CachedSessionInfo LoadCachedSession(HttpCookieCollection requestCookies)
        {
            var sessionKeyCookie = requestCookies[SESSION_KEY_COOKIE];
            var userIdCookie = requestCookies[USER_ID_COOKIE];
            var expiryTimeCookie = requestCookies[EXPIRY_TIME_COOKIE];

            if (sessionKeyCookie == null || userIdCookie == null || expiryTimeCookie == null)
                return null;
            else
                return new CachedSessionInfo(sessionKeyCookie.Value, long.Parse(userIdCookie.Value),
                                             DateTime.Parse(expiryTimeCookie.Value));
        }
    }
}

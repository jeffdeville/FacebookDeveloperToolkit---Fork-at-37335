using System;
using System.Collections.Specialized;
using System.Web;
using Facebook.Rest;

namespace Facebook.Session
{
	/// <summary>
	/// The only difference between this provider and the FBML one is that this one can cache session info
	/// in cookies.
	/// </summary>
	public class IFrameSessionProvider : FbmlSessionProvider
	{
		public const string SESSION_KEY_COOKIE = "SessionKey";
	    public const string USER_ID_COOKIE = "UserId";
	    public const string EXPIRY_TIME_COOKIE = "ExpiryTime";

		private readonly HttpCookieCollection _requestCookies;
		private readonly HttpCookieCollection _responseCookies;

		public IFrameSessionProvider(HttpCookieCollection requestCookies, HttpCookieCollection responseCookies,
									 NameValueCollection inputParams, IAuth auth)
			: base(inputParams, auth)
		{
			if (responseCookies == null || requestCookies == null)
				throw new ArgumentNullException("the request and response cookies are required");

			_responseCookies = responseCookies;
			_requestCookies = requestCookies;
		}

        protected override string SessionKeyFromRequest
        {
            get { return _inputParams[QueryParameters.SessionKey]; }
        }

		/// <summary>
		/// This version of GetSession looks for a sessionkey in the request.  If it can't find one, it looks 
		/// for a cached session in the cookies.  Failing that, it looks for an auth token, and exchanges it
		/// for a session.  Any time a session is created/found, it is cached, for the next lookup.
		/// </summary>
		/// <returns></returns>
		public override IFacebookSession GetSession()
		{
			IFacebookSession session = null;
			var cachedSessionInfo = LoadCachedSession();

			// The logic here, is to look for a sessionkey that already exists, look for one that was cached,
			// or exchange an authtoken for a sessionkey if that exists instead.
			
			if (!string.IsNullOrEmpty(SessionKeyFromRequest))
				session = CreateSession(SessionKeyFromRequest, UserId, ExpirationTime);
			else if (cachedSessionInfo != null)
				// && (HttpContext.Current.Request.HttpMethod == "POST" || !string.IsNullOrEmpty(authToken))) 
				// only use cached info if user hasn't removed the app
				session = CreateSession(cachedSessionInfo.SessionKey, cachedSessionInfo.UserId, cachedSessionInfo.ExpiryTime);
			else if (!string.IsNullOrEmpty(AuthToken))
				session = ExchangeAuthTokenForSession();

			CacheSession(session);
			return session;
		}

		private void CacheSession(IFacebookSession session)
		{
            if (session == null) return;
            var sessionInfo = new CachedSessionInfo(session.SessionKey, session.UserId, session.ExpiryTime);

			_responseCookies.Set(new HttpCookie(SESSION_KEY_COOKIE, sessionInfo.SessionKey));
			_responseCookies.Set(new HttpCookie(USER_ID_COOKIE, sessionInfo.UserId.ToString()));
			_responseCookies.Set(new HttpCookie(EXPIRY_TIME_COOKIE, sessionInfo.ExpiryTime.ToString()));
		}

		internal CachedSessionInfo LoadCachedSession()
		{
			var sessionKeyCookie = _requestCookies[SESSION_KEY_COOKIE];
			var userIdCookie = _requestCookies[USER_ID_COOKIE];
			var expiryTimeCookie = _requestCookies[EXPIRY_TIME_COOKIE];
			if (sessionKeyCookie == null || userIdCookie == null || expiryTimeCookie == null) return null;
			
			return new CachedSessionInfo(sessionKeyCookie.Value, long.Parse(userIdCookie.Value),
			                             DateTime.Parse(expiryTimeCookie.Value));
		}
	}
}
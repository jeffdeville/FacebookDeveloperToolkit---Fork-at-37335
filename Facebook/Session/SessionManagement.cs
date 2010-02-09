using System;
using System.Collections.Specialized;
using System.Web;

namespace Facebook.Session
{
    /// <summary>
    /// Currently, there is not implementation, so I don't need this.
    /// </summary>
    public abstract class SessionManagement
    {

        /// <summary>
        /// Logs in user
        /// </summary>
        public abstract void Login();

        /// <summary>
        /// Logs out user
        /// </summary>
        public abstract void Logout();
    }

    public enum FacebookPageType
    {
        Connect,
        IFrame,
        Fbml
    }


	public interface IFacebookSessionFactory
	{
		IFacebookSession GetSession(FacebookPageType pageType);

	}

	/// <summary>
    /// You can't create the factory until you're ready to provide it 
    /// the inputs that it needs.
    /// </summary>
    public class FacebookSessionFactory : IFacebookSessionFactory
	{
        private readonly HttpCookieCollection _requestCookies;
        private readonly HttpCookieCollection _responseCookies;
        private readonly NameValueCollection _inputParams;
        // Todo - I want to be able to test this, so I need to remove
        // the dependency on the HttpContext, but accepting the collections
        // I need from it instead.

        public FacebookSessionFactory(HttpContextBase context)
            : this(context.Request.Cookies, context.Response.Cookies, context.Request.Params) {}

        public FacebookSessionFactory(HttpCookieCollection requestCookies, HttpCookieCollection responseCookies, NameValueCollection inputParams)
        {
            _requestCookies = requestCookies;
            _responseCookies = responseCookies;
            _inputParams = inputParams;
        }

        public IFacebookSession GetSession(FacebookPageType pageType)
        {
            switch (pageType)
            {
                case FacebookPageType.Connect:
					return new ConnectSessionProvider(_requestCookies).GetSession();
                case FacebookPageType.IFrame:
            		return new IFrameSessionProvider(_requestCookies, _responseCookies, _inputParams).GetSession();
                case FacebookPageType.Fbml:
					return new FBMLSessionProvider(_inputParams).GetSession();
                default:
                    throw new ArgumentOutOfRangeException("pageType");
            }
        }

		//private void LoadFromRequest()
		//{


		//    if (HttpContext.Current.Response == null || HttpContext.Current.Request == null)
		//    {
		//        throw new Exception("Session must have both an HttpRequest object and an HttpResponse object to login.");
		//    }

		//    bool inProfileTab = HttpContext.Current.Request[QueryParameters.InProfileTab] == "1";
		//    string sessionKeyFromRequest = inProfileTab ? HttpContext.Current.Request[QueryParameters.ProfileSessionKey] : HttpContext.Current.Request[QueryParameters.SessionKey];
		//    string authToken = HttpContext.Current.Request[QueryParameters.AuthToken];
		//    CachedSessionInfo cachedSessionInfo = LoadCachedSession();

		//    if (!string.IsNullOrEmpty(sessionKeyFromRequest))
		//    {
		//        SetSessionProperties(
		//            sessionKeyFromRequest,
		//            long.Parse(inProfileTab ? HttpContext.Current.Request[QueryParameters.ProfileUser] : HttpContext.Current.Request[QueryParameters.User]),
		//            DateHelper.ConvertUnixTimeToDateTime(long.Parse(HttpContext.Current.Request[QueryParameters.Expires])));
		//    }
		//    // The code below was commented out, because w/ it, you must include all of the fbsig parameters in every link.  If you don't include them, the the application
		//    // will bust out of the chrome, because you have no session.  Removing this code forces it to use the cached session info from the cookie.  This should
		//    // be fine anyway, because manually adding it to the querystring param of every link would still permit expired sessions.
		//    else if (cachedSessionInfo != null)// && (HttpContext.Current.Request.HttpMethod == "POST" || !string.IsNullOrEmpty(authToken))) // only use cached info if user hasn't removed the app
		//    {
		//        SetSessionProperties(cachedSessionInfo.SessionKey, cachedSessionInfo.UserId, cachedSessionInfo.ExpiryTime);
		//    }
		//    else if (!string.IsNullOrEmpty(authToken))
		//    {
		//        session_info sessionInfo = new FacebookApi().Initialize(this).Auth.GetSession(authToken);
		//        SetSessionProperties(sessionInfo.session_key, sessionInfo.uid, DateHelper.ConvertUnixTimeToDateTime(sessionInfo.expires));
		//    }

		//}
    }
}
using System;
using System.Collections.Specialized;
using System.Web;
using Facebook.Rest;

namespace Facebook.Session
{
	/// <summary>
    /// You can't create the factory until you're ready to provide it 
    /// the inputs that it needs.
    /// </summary>
    public class SessionProviderFactory : ISessionProviderFactory
	{
        private readonly HttpCookieCollection _requestCookies;
        private readonly HttpCookieCollection _responseCookies;
        private readonly NameValueCollection _inputParams;		
		public IAuth Auth { get; set; }

        public SessionProviderFactory(HttpContextBase context, IAuth auth)
            : this(context.Request.Cookies, context.Response.Cookies, context.Request.Params, auth) {}

        public SessionProviderFactory(HttpCookieCollection requestCookies, HttpCookieCollection responseCookies, NameValueCollection inputParams, IAuth auth)
        {
            _requestCookies = requestCookies;
            _responseCookies = responseCookies;
            _inputParams = inputParams;
			Auth = auth;
        }

		public ISessionProvider GetSessionProvider(FacebookPageType pageType)
        {
            switch (pageType)
            {
                case FacebookPageType.Connect:
					return new ConnectSessionProvider(_requestCookies);
                case FacebookPageType.IFrame:
					return new IFrameSessionProvider(_requestCookies, _responseCookies, _inputParams, Auth);
                case FacebookPageType.Fbml:
					return new FbmlSessionProvider(_inputParams, Auth);
                default:
                    throw new ArgumentOutOfRangeException("pageType");
            }
        }
    }
}
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Web;
using System.Web.SessionState;

namespace Facebook.Tests.Mvc.MvcFakes
{
    public class FakeHttpContext : HttpContextBase
    {
        private IPrincipal _principal;
        private readonly NameValueCollection _formParams;
        private readonly NameValueCollection _queryStringParams;
        private readonly HttpCookieCollection _requestCookies;
        private readonly HttpCookieCollection _responseCookies;
        private readonly SessionStateItemCollection _sessionItems;

    	public FakeHttpContext()
    	{
    		_formParams = new NameValueCollection();
			_queryStringParams = new NameValueCollection();
			_requestCookies = new HttpCookieCollection();
			_responseCookies = new HttpCookieCollection();
			_sessionItems = new SessionStateItemCollection();
    	}
		
		public FakeHttpContext(FakePrincipal principal, NameValueCollection formParams, NameValueCollection queryStringParams, HttpCookieCollection requestCookies, 
            SessionStateItemCollection sessionItems) : this(principal, formParams, queryStringParams, requestCookies,null, sessionItems)
        {
        }

        public FakeHttpContext(FakePrincipal principal, NameValueCollection formParams, NameValueCollection queryStringParams, HttpCookieCollection requestCookies, 
            HttpCookieCollection responseCookies, SessionStateItemCollection sessionItems )
        {
            _principal = principal;
            _formParams = formParams;
            _queryStringParams = queryStringParams;
            _requestCookies = requestCookies;
            _responseCookies = responseCookies;
            _sessionItems = sessionItems;
        }



    	private HttpRequestBase _request;
        public override HttpRequestBase Request
        {
            get
            {
                return _request = _request ?? new FakeHttpRequest(_formParams, _queryStringParams, _requestCookies);
            }
        }

        private HttpResponseBase _response;
        public override HttpResponseBase Response
        {
            get
            {
                return _response = _response ?? new FakeHttpResponse(_responseCookies);
            }
        }

        public override IPrincipal User
        {
            get
            {
                return _principal;
            }
            set
            {
                _principal = value;
            }
        }

        public override HttpSessionStateBase Session
        {
            get
            {
                return new FakeHttpSessionState(_sessionItems);
            }
        }

        private Dictionary<string, object> _items;
        public Dictionary<string, object> Items
        {
            get { return _items = _items ?? new Dictionary<string, object>(); }
        }
    }
}

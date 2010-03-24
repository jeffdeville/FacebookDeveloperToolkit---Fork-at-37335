using System.Collections.Specialized;
using System.Web;

namespace Facebook.Tests.Mvc.MvcFakes
{
    public class FakeHttpResponse : HttpResponseBase
    {
        private HttpCookieCollection _cookies;
        public override HttpCookieCollection Cookies
        {
            get { return _cookies = _cookies ?? new HttpCookieCollection(); }
        }

        public FakeHttpResponse(){}
        
        public FakeHttpResponse(HttpCookieCollection cookies)
        {
            _cookies = cookies;
        }

		public override void AppendHeader(string name, string value)
		{
			Headers.Add(name, value);
		}

    	private NameValueCollection _headers;
		public override NameValueCollection Headers { get { return _headers = _headers ?? new NameValueCollection(); } }
    }
}
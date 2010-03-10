using System;
using System.Collections.Specialized;
using System.Web;

namespace Facebook.Tests.Mvc.MvcFakes
{

	public class FakeHttpRequest : HttpRequestBase
	{
		private readonly NameValueCollection _formParams;
		private readonly NameValueCollection _queryStringParams;
		private readonly HttpCookieCollection _cookies;
		private readonly HttpFileCollectionBase _files;

		public FakeHttpRequest(NameValueCollection formParams, NameValueCollection queryStringParams, HttpCookieCollection cookies)
		{
			_formParams = formParams;
			_queryStringParams = queryStringParams;
			_cookies = cookies;
			_files = new FakeHttpFileCollection();
		}

	    public FakeHttpRequest()
	    {
            _formParams = null;
            _queryStringParams = null;
            _cookies = null;
            _files = new FakeHttpFileCollection();
	    }

		public override NameValueCollection Form
		{
			get
			{
				return _formParams;
			}
		}

		public override NameValueCollection QueryString
		{
			get
			{
				return _queryStringParams;
			}
		}

		public override HttpCookieCollection Cookies
		{
			get
			{
				return _cookies;
			}
		}

		public override HttpFileCollectionBase Files
		{

			get
			{
				return _files;
			}
		}

	    private Uri _url;
        public Uri Url
        {
            get
            {
                return _url;
            }
            set { _url = value; }
        }
	}


}

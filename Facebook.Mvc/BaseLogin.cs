using System;
using System.Collections.Specialized;
using Facebook.Session;

namespace Facebook.Mvc
{
	public class BaseLogin
	{
		private readonly NameValueCollection _parameters;
		protected internal string _applicationKey;

		public BaseLogin(NameValueCollection requestParameters)
		{
			if (requestParameters == null) throw new ArgumentNullException("requestParameters");

			_parameters = requestParameters;
			_applicationKey = new FacebookConfiguration().ApiKey;
		}

		/// <summary>
		/// Returns the login url for a canvas page including the api_key query param
		/// </summary>
		protected internal string GetLoginUrl()
		{
			string canvasParam =
				_parameters[QueryParameters.InCanvas] == "1"
				|| _parameters[QueryParameters.InIframe] == "1"
					? "&canvas"
					: string.Empty;
			return string.Format("http://www.facebook.com/login.php?api_key={0}&v=1.0{1}", _applicationKey, canvasParam);
		}
	}
}
using System;
using System.Web;
using System.Web.Mvc;
using Facebook.Session;

namespace Facebook.Mvc
{
	public class ConnectLogin :ILoginHandler
	{
		private readonly Uri _currentUrl;

		public ConnectLogin(Uri currentUrl)
		{
			_currentUrl = currentUrl;
		}

		#region Implementation of ILoginHandler

		/// <summary>
		/// Get string for redirect response
		/// </summary>
		public ActionResult GetRedirect()
		{
			return new RedirectResult(string.Format(new FacebookConfiguration().ConnectLogonUrl, Uri.EscapeUriString(_currentUrl.ToString())));
		}

		#endregion
	}
}
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace Facebook.Session
{
	public class ConnectSessionProvider : ISessionProvider
	{
		internal const string USER = "user";
		internal const string SESSION_KEY = "session_key";
		internal const string SS = "ss";
		internal const string EXPIRES = "expires";
		internal const string SECRET_SESSION_KEY = "";

		private readonly string _applicationKey, _applicationSecret;
		private readonly NameValueCollection _inputParams;
		private readonly HttpCookieCollection _requestCookies;

		public ConnectSessionProvider(HttpCookieCollection requestCookies, NameValueCollection inputParams)
			: this(requestCookies, inputParams, null, null)
		{
		}

		public ConnectSessionProvider(HttpCookieCollection requestCookies, NameValueCollection inputParams,
		                              string applicationKey, string applicationSecret)
		{
			_requestCookies = requestCookies;
			_inputParams = inputParams;
			_applicationKey = applicationKey ?? WebConfigurationManager.AppSettings["ApiKey"];
			_applicationSecret = applicationSecret ?? WebConfigurationManager.AppSettings["Secret"];
		}

		#region ISessionProvider Members

		public IFacebookSession GetSession()
		{
			if (IsConnected())
				return new FacebookSession {SessionKey = GetCookie("session_key"), UserId = GetUserID()};
			return null;
		}

		#endregion

		///<summary>
		///</summary>
		///<param name="cookieName"></param>
		///<returns></returns>
		public string GetCookie(string cookieName)
		{
			string fullCookieName = string.Format("{0}_{1}", _applicationKey, cookieName);
			if (HttpContext.Current != null
			    && HttpContext.Current.Request != null
			    && HttpContext.Current.Request.Cookies != null
			    && HttpContext.Current.Request.Cookies[fullCookieName] != null)
			{
				return HttpContext.Current.Request.Cookies[fullCookieName].Value;
			}

			return null;
		}

		///<summary>
		///</summary>
		///<returns></returns>
		public bool IsConnected()
		{
			if (!AllCookiesExist(HttpContext.Current.Request.Cookies))
				return false;
			return ResponseWasNotTamperedWith(HttpContext.Current.Request.Cookies);
		}

		internal string GetUnencodedValueHash(HttpCookieCollection cookies)
		{
			var expiresCookie = cookies[GetCookieName(EXPIRES)];
			var expires = expiresCookie == null ? "" : expiresCookie.Value;
			
			var sessionKeyCookie = cookies[GetCookieName(SESSION_KEY)];
			var sessionKey = sessionKeyCookie == null ? "" : sessionKeyCookie.Value;
			
			var ssCookie = cookies[GetCookieName(SS)];
			var ss = ssCookie == null ? "" : ssCookie.Value;
			
			var userCookie = cookies[GetCookieName(USER)];
			var user = userCookie == null ? "" : userCookie.Value;


			return string.Format("{0}={1}", EXPIRES, expires)
			       + string.Format("{0}={1}", SESSION_KEY, sessionKey)
			       + string.Format("{0}={1}", SS, ss)
			       + string.Format("{0}={1}", USER, user)
			       + _applicationSecret;
		}

		private string GetCookieName(string cookieName)
		{
			if (string.IsNullOrEmpty(cookieName))
				return _applicationKey;
			return string.Format("{0}_{1}", _applicationKey, cookieName);
		}

		private bool AllCookiesExist(HttpCookieCollection cookies)
		{
			if (cookies == null || cookies.Count == 0)
				return false;
			if (cookies[GetCookieName(EXPIRES)] == null)
				return false;
			if (cookies[GetCookieName(SESSION_KEY)] == null)
				return false;
			if (cookies[GetCookieName(SS)] == null)
				return false;
			if (cookies[GetCookieName(SECRET_SESSION_KEY)] == null)
				return false;
			if (cookies[GetCookieName(USER)] == null)
				return false;

			return true;
		}

		public bool ResponseWasNotTamperedWith(HttpCookieCollection cookies)
		{
			string computedHashKey = ComputeHash(GetUnencodedValueHash(cookies));
			return cookies[GetCookieName(SECRET_SESSION_KEY)].Value == computedHashKey;
		}

		private long GetUserID()
		{
			long userID;
			long.TryParse(GetCookie("user"), out userID);
			return userID;
		}

		#region ComputeHash

		/// <summary>
		/// Computes an MD5 Hash from the given clear text string.
		/// </summary>
		/// <param name="clearTextString">Clear text string used to compute the hash.</param>
		/// <returns>Returns an MD5 computed hash.</returns>
		/// <remarks>Uses <see cref="Encoding.UTF8">UTF-8</see> encoding by default.</remarks>
		public static string ComputeHash(string clearTextString)
		{
			return ComputeHash(clearTextString, Encoding.UTF8);
		}


		/// <summary>
		/// Computes an MD5 Hash from the given clear text string.
		/// </summary>
		/// <param name="clearTextString">Clear text string used to compute the hash.</param>
		/// <param name="encoding">Encoding to be used.</param>
		/// <returns>Returns an MD5 computed hash.</returns>
		public static string ComputeHash(string clearTextString, Encoding encoding)
		{
			var result = new StringBuilder(32);
			byte[] inputBytes = encoding.GetBytes(clearTextString);
			var md5 = new MD5CryptoServiceProvider();
			byte[] sum = md5.ComputeHash(inputBytes);
			foreach (byte b in sum)
			{
				result.Append(b.ToString("x2"));
			}
			return result.ToString();
		}

		#endregion
	}
}
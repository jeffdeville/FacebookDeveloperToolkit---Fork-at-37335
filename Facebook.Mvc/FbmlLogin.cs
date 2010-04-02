using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using Facebook.Rest;

namespace Facebook.Mvc
{
	public class FbmlLogin : BaseLogin, ILoginHandler
	{
		//private readonly IFacebookApi _api;
		public FbmlLogin(NameValueCollection requestParameters) : base(requestParameters)
		{
			//_api = api;
		}

		///// <summary>
		///// Logs in user
		///// </summary>
		//public string GetNextUrl()
		//{
		//    var props = _api.Admin.GetAppProperties(new List<string>() { "callback_url", "canvas_name" });
		//    if (props.ContainsKey("callback_url") && props.ContainsKey("canvas_name") && !string.IsNullOrEmpty(props["callback_url"]) && !string.IsNullOrEmpty(props["callback_url"]))
		//    {
		//        return HttpContext.Current.Request.Url.ToString().Replace(props["callback_url"], string.Format("http://apps.facebook.com/{0}/", props["canvas_name"]));
		//    }
		//    return null;
		//}

		/// <summary>
		/// Get string for redirect response
		/// </summary>
		public ActionResult GetRedirect()
		{
			return new ContentResult() {Content = string.Format("<fb:redirect url=\"{0}\"/>", GetLoginUrl())};
		}
	}
}
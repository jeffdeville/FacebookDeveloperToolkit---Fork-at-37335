using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Mvc
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
	public class FacebookAttribute : ActionFilterAttribute, IAuthorizationFilter
	{
		public FacebookPageType PageType { get; set; }
		public IFacebookApi Api { get; set; }
		public IFacebookSessionFactory SessionFactory { get; set; }

		public FacebookAttribute() {
            //throw new NotImplementedException(
            //    "There's no way to get dependencies in this way, w/out directly referencing StructureMap.");
        }

		public FacebookAttribute(IFacebookApi api, IFacebookSessionFactory sessionFactory)
		{
			Api = api;
			SessionFactory = sessionFactory;
		}

		public override void OnResultExecuted(ResultExecutedContext c)
		{
			base.OnResultExecuted(c);
			if (PageType != FacebookPageType.Fbml)
				c.HttpContext.Response.AppendHeader("P3P", "CP=\"CAO PSA OUR\"");
		}

	    public void OnAuthorization(AuthorizationContext filterContext)
	    {
            var session = SessionFactory.GetSession(PageType);
            if (session == null)
            {
                switch (PageType)
                {
                    case FacebookPageType.Connect:
                        filterContext.Result = new RedirectResult("~/account/logon?returnUrl=" + Uri.EscapeUriString(HttpContext.Current.Request.Url.ToString()));
                        break;                        
                    case FacebookPageType.IFrame:
                        filterContext.Result = new ContentResult { Content = new IFrameLogin().GetRedirect() };
                        break;
                    case FacebookPageType.Fbml:
                        filterContext.Result = new ContentResult { Content = new FBMLLogin(Api).GetRedirect() };
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                Api.Initialize(session);
                filterContext.HttpContext.User = new GenericPrincipal(new GenericIdentity(Api.Session.UserId.ToString()),null);
            }
	    }
	}

	public interface ILoginHandler
	{
		/// <summary>
		/// Get string for redirect response
		/// </summary>
		string GetRedirect();
	}

	public abstract class BaseLogin
	{
		protected string _applicationKey;

		public BaseLogin() : this(null){}

		public BaseLogin(string applicationKey)
		{
			_applicationKey = applicationKey ?? WebConfigurationManager.AppSettings["ApiKey"];
		}
		/// <summary>
		/// Returns the login url for a canvas page including the api_key query param
		/// </summary>
		protected string GetLoginUrl()
		{
			string canvasParam = HttpContext.Current.Request[QueryParameters.InCanvas] == "1" || HttpContext.Current.Request[QueryParameters.InIframe] == "1" ? "&canvas" : string.Empty;
			return string.Format("http://www.facebook.com/login.php?api_key={0}&v=1.0{1}", _applicationKey, canvasParam);
		}
	}

	public class FBMLLogin : BaseLogin, ILoginHandler
	{
		private readonly IFacebookApi _api;
		public FBMLLogin(IFacebookApi api)
		{
			_api = api;
		}

		/// <summary>
		/// Logs in user
		/// </summary>
		public string GetNextUrl()
		{
			var props = _api.Admin.GetAppProperties(new List<string>() { "callback_url", "canvas_name" });
			if (props.ContainsKey("callback_url") && props.ContainsKey("canvas_name") && !string.IsNullOrEmpty(props["callback_url"]) && !string.IsNullOrEmpty(props["callback_url"]))
			{
				return HttpContext.Current.Request.Url.ToString().Replace(props["callback_url"], string.Format("http://apps.facebook.com/{0}/", props["canvas_name"]));
			}
			return null;
		}

		/// <summary>
		/// Get string for redirect response
		/// </summary>
		public string GetRedirect()
		{
			return string.Format("<fb:redirect url=\"{0}\"/>", GetLoginUrl());
		}
	}

	public class IFrameLogin : BaseLogin, ILoginHandler
	{
		/// <summary>
		/// Get string for redirect response
		/// </summary>
		public string GetRedirect()
		{
			string url = GetLoginUrl();
			return string.Format("<script type=\"text/javascript\">\n" +
						   "if (parent != self) \n" +
						   "top.location.href = \"" + url + @"&v=1.0" + "\";\n" +
						   "else self.location.href = \"" + url + @"&v=1.0" + "\";\n" +
						   "</script>");
		}
	}
}

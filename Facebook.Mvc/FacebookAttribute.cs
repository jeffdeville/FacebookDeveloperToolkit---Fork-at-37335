using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Facebook.Rest;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Mvc
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
	public class FacebookAttribute : ActionFilterAttribute, IAuthorizationFilter, IExceptionFilter
	{
		public FacebookPageType PageType { get; set; }
		public IFacebookApi Api { get; set; }
		public ISessionProviderFactory SessionProviderFactory { get; set; }
		public IFacebookLoginFactory LoginFactory { get; set; }
		public FacebookAttribute() {}

		public FacebookAttribute(IFacebookApi api, ISessionProviderFactory sessionProviderFactory, IFacebookLoginFactory loginFactory)
		{
			Api = api;
			SessionProviderFactory = sessionProviderFactory;
			LoginFactory = loginFactory;
		}

		public override void OnResultExecuted(ResultExecutedContext c)
		{
			base.OnResultExecuted(c);
			if (PageType != FacebookPageType.Fbml)
				c.HttpContext.Response.AppendHeader("P3P", "CP=\"CAO PSA OUR\"");
		}

	    public void OnAuthorization(AuthorizationContext filterContext)
	    {
            var session = SessionProviderFactory.GetSessionProvider(PageType).GetSession();
	    	if (session == null)
				filterContext.Result = LoginFactory.GetLoginHandler(PageType).GetRedirect();
	    	else
	    	{
				// 
				// THIS IS NOT OBVIOUS!!!  I'm setting the session object on the Api here.
				// Is it clear that that should happen?  No. Not in the slightest, but I can't
				// do this work before newing the Api up, and this is where the session 
				// is pulled.
				// 
	    		Api.Initialize(session);
	    		filterContext.HttpContext.User = new GenericPrincipal(new GenericIdentity(session.UserId.ToString()), null);
	    	}
	    }

		private ActionResult RedirectToLogin()
		{
			var loginHandler = LoginFactory.GetLoginHandler(PageType);

			throw new NotImplementedException("Should be handled by the factory.");
			//switch (PageType)
			//{
			//    case FacebookPageType.Connect:
			//        return new RedirectResult("~/account/logon?returnUrl=" + Uri.EscapeUriString(HttpContext.Current.Request.Url.ToString()));
			//    case FacebookPageType.IFrame:
			//        return new ContentResult { Content = new IFrameLogin().GetRedirect() };
			//    case FacebookPageType.Fbml:
			//        return new ContentResult { Content = new FbmlLogin(Api).GetRedirect() };
			//    default:
			//        throw new ArgumentOutOfRangeException();
			//}
		}

		private const string SessionKeyCookie = "SessionKey";
		private const string UserIDCookie = "UserId";
		private const string ExpiryTimeCookie = "ExpiryTime";
		
		public void OnException(ExceptionContext filterContext)
		{
			var exception = filterContext.Exception as FacebookException;
			if (exception == null || exception.ErrorType != ErrorType.Timeout)
				return;

			filterContext.Result = RedirectToLogin();
			filterContext.ExceptionHandled = true;

			//Expire the cached session cookies
			// ReSharper disable PossibleNullReferenceException
			filterContext.HttpContext.Response.Cookies[SessionKeyCookie].Expires = DateTime.UtcNow.AddDays(-30);
			filterContext.HttpContext.Response.Cookies[UserIDCookie].Expires = DateTime.UtcNow.AddDays(-30);
			filterContext.HttpContext.Response.Cookies[ExpiryTimeCookie].Expires = DateTime.UtcNow.AddDays(-30);
			// ReSharper restore PossibleNullReferenceException            
		}
	}
}

using System;
using System.Web.Configuration;
using System.Web.Mvc;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class HandleFacebookSessionTimeoutAttribute : FilterAttribute, IExceptionFilter
    {
        private const string SessionKeyCookie = "SessionKey";
        private const string UserIDCookie = "UserId";
        private const string ExpiryTimeCookie = "ExpiryTime";

        private readonly string DefaultApiKey = WebConfigurationManager.AppSettings["ApiKey"];
        private readonly string DefaultSecret = WebConfigurationManager.AppSettings["Secret"];

        public bool IsFbml { get; set; }
        public string ApiKey { get; set; }
        public string Secret { get; set; }

        public void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception as FacebookException;
            if (exception == null || exception.ErrorType != ErrorType.Timeout)
                return;

            var session = IsFbml
                              ? new FBMLCanvasSession(ApiKey ?? DefaultApiKey, Secret ?? DefaultSecret)
                              : (CanvasSession)
                                new IFrameCanvasSession(ApiKey ?? DefaultApiKey, Secret ?? DefaultSecret);

            filterContext.Result = new ContentResult {Content = session.GetRedirect()};
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
using System;
using System.Web.Configuration;
using System.Web.Mvc;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class FacebookAuthorizationAttribute2 : ActionFilterAttribute
    {
        private readonly IFacebookApi _api;

        public FacebookAuthorizationAttribute2(IFacebookApi api)
        {
            _api = api;
        }

        public bool IsFbml { get; set; }
        public const string FACEBOOK_SESSION = "FacebookSession";
        public override void OnActionExecuting(ActionExecutingContext c)
        {
            // 
            // Get the FacebookApi instance from structuremap.
            // Then check to see if the user is logged in or not.
            // 
            //CanvasSession session = null;
            //if (IsFbml)
            //{
            //    if (!string.IsNullOrEmpty(RequiredPermissions))
            //    {
            //        session = new FBMLCanvasSession(ApiKey ?? WebConfigurationManager.AppSettings["ApiKey"], Secret ?? WebConfigurationManager.AppSettings["Secret"], ParsePermissions(RequiredPermissions));
            //    }
            //    else
            //    {
            //        session = new FBMLCanvasSession(ApiKey ?? WebConfigurationManager.AppSettings["ApiKey"], Secret ?? WebConfigurationManager.AppSettings["Secret"]);
            //    }
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(RequiredPermissions))
            //    {
            //        session = new IFrameCanvasSession(ApiKey ?? WebConfigurationManager.AppSettings["ApiKey"], Secret ?? WebConfigurationManager.AppSettings["Secret"], ParsePermissions(RequiredPermissions));
            //    }
            //    else
            //    {
            //        session = new IFrameCanvasSession(ApiKey ?? WebConfigurationManager.AppSettings["ApiKey"], Secret ?? WebConfigurationManager.AppSettings["Secret"]);
            //    }
            //}
            if (string.IsNullOrEmpty(_api.Session.SessionKey))
            {
                c.Result = new ContentResult { Content = _api.Session.GetRedirect() };
                return;
            }
            else
            {
                var permissionsString = session.CheckPermissions();
                if (!string.IsNullOrEmpty(permissionsString))
                {
                    c.Result = new ContentResult { Content = session.GetPermissionsRedirect(session.GetPermissionUrl(permissionsString, session.GetNextUrl())) };
                    return;
                }
                // 
                // If this is an instance of IFacebookController, then initialize the API
                // object if it has been newed up.
                // 
                c.HttpContext.Items.Add(FACEBOOK_SESSION, session);
                //var facebookController = c.Controller as IFacebookController;
                //if (facebookController != null && facebookController.Facebook != null)
                //    facebookController.Facebook.Initialize(session);
            }

        }

        public override void OnResultExecuted(ResultExecutedContext c)
        {
            base.OnResultExecuted(c);
            if (!IsFbml)
            {
                c.HttpContext.Response.AppendHeader("P3P", "CP=\"CAO PSA OUR\"");
            }

        }
        private List<Enums.ExtendedPermissions> ParsePermissions(string permissions)
        {
            if (string.IsNullOrEmpty(permissions))
                return null;
            string[] input = permissions.Split(',');
            List<Enums.ExtendedPermissions> output = new List<Enums.ExtendedPermissions>();
            foreach (var item in input)
            {
                output.Add((Enums.ExtendedPermissions)Enum.Parse(typeof(Enums.ExtendedPermissions), item.Trim(), true));
            }
            return output;

        }
    }
}
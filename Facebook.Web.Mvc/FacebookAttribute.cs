using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Facebook.Session;
using System.Web.Configuration;
using Facebook.Schema;
using Facebook.Rest;

namespace Facebook.Web.Mvc
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class FacebookAttribute : FilterAttribute, IAuthorizationFilter
	{
		public const string FACEBOOK_CANVAS_SESSION = "facebook-canvas-session";

		public FacebookPageType PageType { get; set; }
		/// <summary> 
		/// The comma separated list of extended permissions
		/// </summary>
		public string RequiredPermissions { get; set; }

		//public bool MustBeAppUser { get; set; }
		/// <summary> 
		/// The APi key for this application given by facebook
		/// </summary>
		public string ApiKey { get; set; }

		/// <summary> 
		/// The APi Secret for this application given by facebook
		/// </summary>
		public string Secret { get; set; }

		public void OnAuthorization(AuthorizationContext filterContext)
		{
			FacebookSession session = null;
			switch (PageType)
			{
				case FacebookPageType.Connect:
					session = new ConnectSession(ApiKey ?? WebConfigurationManager.AppSettings["ApiKey"],
					    Secret ?? WebConfigurationManager.AppSettings["Secret"]);
					break;
				case FacebookPageType.IFrame:
					session = new IFrameCanvasSession(ApiKey ?? WebConfigurationManager.AppSettings["ApiKey"],
						Secret ?? WebConfigurationManager.AppSettings["Secret"], 
						ParsePermissions(RequiredPermissions));
					break;
				case FacebookPageType.FBML:
					session = new FBMLCanvasSession(ApiKey ?? WebConfigurationManager.AppSettings["ApiKey"], 
						Secret ?? WebConfigurationManager.AppSettings["Secret"], 
						ParsePermissions(RequiredPermissions));
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			if (string.IsNullOrEmpty(session.SessionKey))
			{
				AddFormValuesToCookie(filterContext.HttpContext.Response, filterContext.HttpContext.Request.Form);
				if(session is CanvasSession)
				{
					filterContext.Result = new ContentResult { Content = ((CanvasSession)session).GetRedirect() };
				}
				else
				{					
					var returnUrl = HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.ToString());
					filterContext.Result = new RedirectResult("/account/logon/?returnUrl=" + returnUrl);
				}
				return;
			}

			var permissionsString = session.CheckPermissions();
			if (!string.IsNullOrEmpty(permissionsString))
			{
				AddFormValuesToCookie(filterContext.HttpContext.Response, filterContext.HttpContext.Request.Form);
				if (session is CanvasSession)
				{
					filterContext.Result = new ContentResult
                   	{
                   		Content = ((CanvasSession) session).GetPermissionsRedirect(
                   			session.GetPermissionUrl(permissionsString, ((CanvasSession) session).GetNextUrl()))
                   	};
				}
				return;
			}

			// Everything is ok, so I need to stash this session somewhere for use.
			filterContext.HttpContext.Items.Add(FACEBOOK_CANVAS_SESSION, session);

			// Now I need to set the user context
			var currentUserId = session.UserId;
			if (currentUserId != 0)
			{
				var identity = new GenericIdentity(currentUserId.ToString());
				filterContext.HttpContext.User = new GenericPrincipal(identity, null);
			}	
		}

		private void AddFormValuesToCookie(HttpResponseBase response, NameValueCollection formValues)
		{
			if(formValues == null || formValues.Count == 0)
				return;
			
			// Add form values to the cookie in batches of 20.
			//var cookieIndex = 0;

			var cookie = new HttpCookie("prelogin-form-params");
            foreach (var key in formValues.AllKeys)
				cookie.Values.Add(key, HttpUtility.UrlEncode(formValues[key]));
			response.Cookies.Add(cookie);		
			response.AppendHeader("P3P", "CP=\"CAO PSA OUR\"");
		}

		private List<Enums.ExtendedPermissions> ParsePermissions(string permissions)
		{
			if (string.IsNullOrEmpty(permissions))
				return null;
			string[] input = permissions.Split(',');
			var output = new List<Enums.ExtendedPermissions>();
			foreach (var item in input)
			{
				output.Add((Enums.ExtendedPermissions)Enum.Parse(typeof(Enums.ExtendedPermissions), item.Trim(), true));
			}
			return output;
		}
	}
}

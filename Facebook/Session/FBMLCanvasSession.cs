using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Web;
using Facebook.Schema;

namespace Facebook.Session
{
    /// <summary>
    /// Session object used by fbml canvas applications
    /// </summary>
    public class FBMLCanvasSession : CanvasSession
	{
        /// <summary>
        /// Constructor
        /// </summary>
        public FBMLCanvasSession(string appKey, string appSecret)
			: base(appKey, appSecret)
		{
		}
        /// <summary>
        /// Constructor
        /// </summary>
        public FBMLCanvasSession(string appKey, string appSecret, List<Enums.ExtendedPermissions> permissions)
            : base(appKey, appSecret, permissions)
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public FBMLCanvasSession(string appKey, string appSecret, bool readRequest)
            : base(appKey, appSecret, readRequest)
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public FBMLCanvasSession(string appKey, string appSecret, List<Enums.ExtendedPermissions> permissions, bool readRequest)
            : base(appKey, appSecret, permissions, readRequest)
        {
        }
		
		internal override void RedirectToLogin()
		{
            HttpContext.Current.Response.Write(GetRedirect());
            HttpContext.Current.Response.End();
		}

        /// <summary>
        /// Get string for redirect response
        /// </summary>
        public override string GetRedirect()
        {
            return string.Format("<fb:redirect url=\"{0}\"/>", GetLoginUrl());
        }
        /// <summary>
        /// Get string for redirect response
        /// </summary>
        public override string GetPermissionsRedirect(string permissionsUrl)
        {
            return string.Format("<fb:redirect url=\"{0}\"/>", permissionsUrl);
        }

		internal override void CacheSession()
		{
			return;
		}

		internal override CachedSessionInfo LoadCachedSession()
		{
			return null;
		}
	}
}

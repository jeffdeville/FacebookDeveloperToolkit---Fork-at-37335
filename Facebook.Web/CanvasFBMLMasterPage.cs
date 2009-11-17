using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.UI;
using Facebook.Rest;
using System.Web.Configuration;
using Facebook.Session;
using System.Collections.Generic;
using Facebook.Schema;

namespace Facebook.Web
{
    /// <summary> 
    /// Masterpage that can used by FBML Canvas application to handle the authentication handshake with FB
    /// </summary>
    public class CanvasFBMLMasterPage : MasterPage
	{
		private readonly IFacebookApi _api = new Api().Initialize(new FBMLCanvasSession(null, null));
	    private bool _requireLogin = false;

        /// <summary> 
        /// The APi key for this application given by facebook
        /// </summary>
        public string ApiKey { get { return _api.Session.ApplicationKey; } set { _api.Session.ApplicationKey = value; } }

        /// <summary> 
        /// The APi Secret for this application given by facebook
        /// </summary>
        public string Secret { get { return _api.Session.ApplicationSecret; } set { _api.Session.ApplicationSecret = value; } }

        /// <summary>
        /// List of extended permissions required by this application
        /// </summary>
        public List<Enums.ExtendedPermissions> RequiredPermissions { get; set; }
        /// <summary> 
        /// The application suffix for this application 
        /// </summary>
        public string suffix
        {
            get { return WebConfigurationManager.AppSettings["Suffix"]; }
        }
        /// <summary> 
        /// The callback url
        /// </summary>
        public string callback
        {
            get { return WebConfigurationManager.AppSettings["Callback"]; }
        }
        /// <summary> 
        /// cssVersion used to override browser caching
        /// </summary>
        public string cssVersion
        {
            get { return WebConfigurationManager.AppSettings["CSSVersion"]; }
        }
        /// <summary> 
        /// jsVersion used to override browser caching
        /// </summary>
        public string jsVersion
        {
            get { return WebConfigurationManager.AppSettings["JSVersion"]; }
        }
        
		/// <summary>
		/// Determines whether or not the page being displayed requires that the user be logged into
		/// the application before viewing the page. If this is true, and the user is not logged in,
		/// they will be redirected to the login/allow access page. The default is false.
		/// 
		/// This property must be set before the Page_Init method is called. To set it in a content page
		/// of this master page, set it in that page's Page_PreInit method.
		/// </summary>
        public bool RequireLogin
		{
			get { return _requireLogin; }
            set { _requireLogin = value; }
		}

        /// <summary> 
        /// instance of the api that can be used to make rest calls
        /// </summary>
		public IFacebookApi Api
		{
			get { return _api; }
		}


        /// <summary> 
        /// perform the handshake
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            var apiKey = WebConfigurationManager.AppSettings["ApiKey"];
            var secret = WebConfigurationManager.AppSettings["Secret"];
            _api.Session.ApplicationKey = ApiKey ?? apiKey;
            _api.Session.ApplicationSecret = Secret ?? secret;
			if (RequireLogin)
			{
				var session = (CanvasSession)_api.Session;
                session.RequiredPermissions = RequiredPermissions;
                session.Login();
			}
		}
	}
}
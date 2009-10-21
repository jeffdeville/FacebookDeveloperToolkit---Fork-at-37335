using System;
using System.Web.UI;
using Facebook.Rest;
using System.Web.Configuration;
using Facebook.Session;
using Facebook.Schema;
using System.Collections.Generic;

namespace Facebook.Web
{
    /// <summary> 
    /// Basepage that can used by FBML Canvas application to handle the authentication handshake with FB
    /// </summary>
    public class CanvasFBMLBasePage : Page
	{
		private readonly IFacebookApi _api = new Api().Initialize(new FBMLCanvasSession(null, null));
        private bool _requireLogin = false;

        /// <summary> 
        /// The APi key for this application given by facebook
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary> 
        /// The APi Secret for this application given by facebook
        /// </summary>
        public string Secret { get; set; }
        /// <summary> 
        /// The application suffix for this application 
        /// </summary>
        public string suffix
        {
            get { return WebConfigurationManager.AppSettings["Suffix"]; }
        }
        /// <summary>
        /// List of extended permissions required by this application
        /// </summary>
        public List<Enums.ExtendedPermissions> RequiredPermissions { get; set; }
        /// <summary> 
        /// The callback url
        /// </summary>
        public string callback
        {
            get { return WebConfigurationManager.AppSettings["Callback"]; }
        }

		/// <summary>
		/// Determines whether or not the page being displayed requires that the user be logged into
		/// the application before viewing the page. If this is true, and the user is not logged in,
		/// they will be redirected to the login/allow access page. The default is false.
		/// 
		/// This property must be set before the Page_Init method is called on this class. To set it in
		/// the page that inherits from this class, set it in that page's Page_Init method.
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
            // ApplicationKey and Secret are acquired when you sign up for 
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
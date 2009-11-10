using System;
using System.Web.UI;
using Facebook.Rest;
using Facebook.Session;
using System.Web.Configuration;
using Facebook.Web.FbmlControls;
using System.Collections.Generic;
using Facebook.Schema;

namespace Facebook.Web
{
    /// <summary> 
    /// Baspage that can used by IFrame Canvas application to handle the authentication handshake with FB
    /// </summary>
    public class CanvasIFrameBasePage : Page
	{
		private readonly IFacebookApi _api = new Api().Initialize(new IFrameCanvasSession(null, null));
		private bool _requireLogin = false;

        /// <summary> 
        /// The Api Key for the application given by Facebook
        /// </summary>
        public string ApiKey
		{
            get { return _api.Session.ApplicationKey; }
            set { _api.Session.ApplicationKey = value; }
		}

        /// <summary> 
        /// The Api Secret for the application given by Facebook
        /// </summary>
        public string Secret
		{
            get { return _api.Session.ApplicationSecret; }
            set { _api.Session.ApplicationSecret = value; }
		}
        /// <summary>
        /// List of extended permissions required by this application
        /// </summary>
        public List<Enums.ExtendedPermissions> RequiredPermissions { get; set; }

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
        ///update the response header
        /// </summary>
        protected override void OnPreRender(EventArgs e)
		{
			Response.AppendHeader("P3P", "CP=\"CAO PSA OUR\"");
            CheckForFbmlControls(Controls);
			base.OnPreRender(e);
		}
        private void CheckForFbmlControls(ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                FbmlControl fc = c as FbmlControl;
                if (fc != null)
                {
                    fc.Xfbml = true;
                }
                CheckForFbmlControls(c.Controls);
            }
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
				var session = (IFrameCanvasSession)_api.Session;
				session.HttpSession = Session;
                session.RequiredPermissions = RequiredPermissions;
                session.Login();
			}
		}
	}
}
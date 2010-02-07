using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Facebook.Rest;
using Facebook.Session;
using Facebook.Schema;

namespace Facebook.Web
{
    /// <summary> 
    /// A Control that can be used on any fbml page to handle the FB authentication handshake
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:CanvasFBMLLoginControl runat=server></{0}:CanvasFBMLLoginControl>")]
    public class CanvasFBMLLoginControl : WebControl
    {
		private readonly IFacebookApi _api = new FacebookApi().Initialize(new FBMLCanvasSession(null, null));
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
        public string RequiredPermissions { get; set; }
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
             var apiKey = WebConfigurationManager.AppSettings["ApiKey"];
             var secret = WebConfigurationManager.AppSettings["Secret"];
             _api.Session.ApplicationKey = ApiKey ?? apiKey;
             _api.Session.ApplicationSecret = Secret ?? secret;

             if (RequireLogin)
             {
                 var session = (CanvasSession)_api.Session;
                 session.RequiredPermissions = ParsePermissions(RequiredPermissions);
                 session.Login();
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

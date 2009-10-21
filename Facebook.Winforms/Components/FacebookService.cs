using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Facebook.Winforms.Forms;
using Facebook.Rest;
using Facebook.Schema;
using Facebook.Session;
using System.Threading;
using Facebook.Utility;
using System.Collections.Generic;

namespace Facebook.Winforms.Components
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(true), ToolboxBitmap(typeof (FacebookService)), Designer(typeof (FacebookServiceDesigner))]
    public partial class FacebookService : Component, Facebook.Winforms.Components.IFacebookService
    {
        private readonly IApi api;

        #region Accessors

        /// <summary>
        /// An object used to call various marketplace-related methods in Facebook's API.
        /// </summary>
        public IMarketplace Marketplace
        {
            get { return api.Marketplace; }
        }
        /// <summary>
        /// An object used to call various video methods in Facebook's API.
        /// </summary>
        public IVideo Video
        {
            get { return api.Video; }
        }


        /// <summary>
        /// An object used to call various stream methods in Facebook's API.
        /// </summary>
        public IStream Stream
        {
            get { return api.Stream; }
        }

        /// <summary>
        /// An object used to call various photo-related methods in Facebook's API.
        /// </summary>
        public IPhotos Photos
        {
            get { return api.Photos; }
        }

        /// <summary>
        /// An object used to call various user-related methods in Facebook's API.
        /// </summary>
        public IUsers Users
        {
            get { return api.Users; }
        }
        /// <summary>
        /// An object used to call various comments-related methods in Facebook's API.
        /// </summary>
        public IComments Comments
        {
            get { return api.Comments; }
        }

        /// <summary>
        /// An object used to call various data-related methods in Facebook's API.
        /// </summary>
        public IData Data
        {
            get { return api.Data; }
        }
        /// <summary>
        /// An object used to call various status-related methods in Facebook's API.
        /// </summary>
        public IStatus Status
        {
            get { return api.Status; }
        }

        /// <summary>
        /// An object used to call various data-related methods in Facebook's API.
        /// </summary>
        public ILinks Links
        {
            get { return api.Links; }
        }
        /// <summary>
        /// An object used to call various friend-related methods in Facebook's API.
        /// </summary>
        public IFriends Friends
        {
            get { return api.Friends; }
        }

        /// <summary>
        /// An object used to call various event-related methods in Facebook's API.
        /// </summary>
        new public IEvents Events
        {
            get { return api.Events; }
        }

        /// <summary>
        /// An object used to call various group-related methods in Facebook's API.
        /// </summary>
        public IGroups Groups
        {
            get { return api.Groups; }
        }

        /// <summary>
        /// An object used to call various admin-related methods in Facebook's API.
        /// </summary>
        public IAdmin Admin
        {
            get { return api.Admin; }
        }

        /// <summary>
        /// An object used to call various profile-related methods in Facebook's API.
        /// </summary>
        public Facebook.Rest.IProfile Profile
        {
            get { return api.Profile; }
        }

        /// <summary>
        /// An object used to call various notification-related methods in Facebook's API.
        /// </summary>
        public IPermissions Permissions
        {
            get { return api.Permissions; }
        }

        /// <summary>
        /// An object used to call various notification-related methods in Facebook's API.
        /// </summary>
        public INotifications Notifications
        {
            get { return api.Notifications; }
        }

        /// <summary>
        /// An object used to call various FBML-related methods in Facebook's API.
        /// </summary>
        public IFbml Fbml
        {
            get { return api.Fbml; }
        }

        /// <summary>
        /// An object used to call various Message-related methods in Facebook's API.
        /// </summary>
        public Facebook.Rest.IMessage Message
        {
            get { return api.Message; }
        }

        /// <summary>
        /// An object used to call various Intl-related methods in Facebook's API.
        /// </summary>
        public IIntl Intl
        {
            get { return api.Intl; }
        }

        /// <summary>
        /// An object used to call various feed-related methods in Facebook's API.
        /// </summary>
        public IFeed Feed
        {
            get { return api.Feed; }
        }

        /// <summary>
        /// An object used to call various FQL-related methods in Facebook's API.
        /// </summary>
        public IFql Fql
        {
            get { return api.Fql; }
        }

        /// <summary>
        /// An object used to call various live message-related methods in Facebook's API.
        /// </summary>
        public ILiveMessage LiveMessage
        {
            get { return api.LiveMessage; }
        }

        /// <summary>
        /// An object used to call various batch-related methods in Facebook's API.
        /// </summary>
        public IBatch Batch
        {
            get { return api.Batch; }
        }

        /// <summary>
        /// An object used to call various page-related methods in Facebook's API.
        /// </summary>
        public IPages Pages
        {
            get { return api.Pages; }
        }

        /// <summary>
        /// An object used to call various application-related methods in Facebook's API.
        /// </summary>
        public Facebook.Rest.IApplication Application
        {
            get { return api.Application; }
        }

        /// <summary>
        /// The application key for your application.
        /// </summary>
        [Category("Facebook"), Description("Access Key required to use the API")]
        public string ApplicationKey
        {
            get { return api.Session.ApplicationKey; }
            set { api.Session.ApplicationKey = value; }
        }

        /// <summary>
        /// The underlying API client object to use to communicate with Facebook.
        /// </summary>
        public IApi Api
        {
            get { return api; }
	        
        }

        /// <summary>
        /// The secret for your Facebook application.
        /// </summary>
        [Category("Facebook"), Description("Secret Word")]
        public string Secret
        {
            get { return api.Session.ApplicationSecret; }
            set { api.Session.ApplicationSecret = value; }
        }

        /// <summary>
        /// The Facebook ID of the current user.
        /// </summary>
        [Browsable(false)]
        public long uid
        {
            get { return api.Session.UserId; }
            set { api.Session.UserId = value; }
        }

        /// <summary>
        /// The current session key to use.
        /// </summary>
        [Browsable(false)]
        public string SessionKey
        {
            get { return api.Session.SessionKey; }
            set { api.Session.SessionKey = value; }
        }

        /// <summary>
        /// Whether or not the session expires.
        /// </summary>
        [Browsable(false)]
        public bool SessionExpires
        {
            get { return api.Session.SessionExpires; }
        }

        /// <summary>
        /// The URL used to login to Facebok. Constructed using the application key, auth token, and a URL template.
        /// </summary>
        [Browsable(false)]
        private string LoginUrl
        {
            get
            {
                return api.LoginUrl;
            }
        }

        /// <summary>
        /// The URL used to log off of Facebook. Constructed using the application key, auth token, and a URL template.
        /// </summary>
        [Browsable(false)]
        private string LogOffUrl
        {
            get
            {
                return api.LogOffUrl;
            }
        }

        /// <summary>
        /// Constructs the URL to use to redirect a user to enable a given extended permission for your application.
        /// </summary>
        /// <param name="permission">The specific permission to enable.</param>
        /// <returns>The URL to redirect users to.</returns>
        [Browsable(false)]
        private string ExtendedPermissionUrl(Enums.ExtendedPermissions permission)
        {
            return api.ExtendedPermissionUrl(permission);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public FacebookService()
        {
            api = new Api(new DesktopSession(null, null, null));
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public FacebookService(IContainer container)
        {
            if (container != null) container.Add(this);
            
            api = new Api(new DesktopSession(null, null, null));
            InitializeComponent();
        }

        #endregion Constuctors
        /// <summary>
        /// Displays an integrated browser to allow the user to log on to the
        /// Facebook web page.
        /// </summary>
        public void ConnectToFacebook() 
        {
            ConnectToFacebook(null);
        }
        /// <summary>
        /// Displays an integrated browser to allow the user to log on to the
        /// Facebook web page.
        /// </summary>
        public void ConnectToFacebook(List<Enums.ExtendedPermissions> permissions)
        {
            if (string.IsNullOrEmpty(api.Session.SessionKey))
            {
                ((DesktopSession)api.Session).RequiredPermissions = permissions;
                api.Session.Login();
            }
        }
        

        /// <summary>
        /// 
        /// </summary>
        public void LogOff()
        {
            api.Session.Logout();
        }
    }

}
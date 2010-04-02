using System;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Facebook.Session;
using Facebook.Schema;
using Facebook.Utility;

namespace Facebook.Rest
{
    /// <summary>
    /// Provides various methods to utilize the Facebook Platform API.
    /// </summary>
    public class Api : BaseAuthenticatedService, IFacebookApi
    {
        #region Public Properties

        ///<summary>
        /// Gets or sets the Connect REST API object instance.
        ///</summary>
        public IConnect Connect { get; private set; }

        ///<summary>
        /// Gets or sets the Marketplace REST API object instance.
        ///</summary>
        public IMarketplace Marketplace { get; private set; }

        ///<summary>
        /// Gets or sets the Comments REST API object instance.
        ///</summary>
        public IComments Comments { get; private set; }

        ///<summary>
        /// Gets or sets the Photos REST API object instance.
        ///</summary>
        public IPhotos Photos { get; private set; }

        ///<summary>
        /// Gets or sets the Users REST API object instance.
        ///</summary>
		public IUsers Users { get; private set; }

        ///<summary>
        /// Gets or sets the Friends REST API object instance.
        ///</summary>
		public IFriends Friends { get; private set; }

        ///<summary>
        /// Gets or sets the Intl REST API object instance.
        ///</summary>
		public IIntl Intl { get; private set; }

        ///<summary>
        /// Gets or sets the Events REST API object instance.
        ///</summary>
		public IEvents Events { get; private set; }

        ///<summary>
        /// Gets or sets the Groups REST API object instance.
        ///</summary>
		public IGroups Groups { get; private set; }
		
    	///<summary>
        /// Gets or sets the Admin REST API object instance.
        ///</summary>
		public IAdmin Admin { get; private set; }

        ///<summary>
        /// Gets or sets the Profile REST API object instance.
        ///</summary>
		public IProfile Profile { get; private set; }

        ///<summary>
        /// Gets or sets the Notifications REST API object instance.
        ///</summary>
		public INotifications Notifications { get; private set; }

        ///<summary>
        /// Gets or sets the Fbml REST API object instance.
        ///</summary>
		public IFbml Fbml { get; private set; }

        ///<summary>
        /// Gets or sets the Feed REST API object instance.
        ///</summary>
		public IFeed Feed { get; private set; }

        ///<summary>
        /// Gets or sets the Fql REST API object instance.
        ///</summary>
		public IFql Fql { get; private set; }

        ///<summary>
        /// Gets or sets the Livemessage REST API object instance.
        ///</summary>
		public ILiveMessage LiveMessage { get; private set; }

        ///<summary>
        /// Gets or sets the Livemessage REST API object instance.
        ///</summary>
		public IMessage Message { get; private set; }

        ///<summary>
        /// Gets or sets the Pages REST API object instance.
        ///</summary>
		public IPages Pages { get; private set; }

        ///<summary>
        /// Gets or sets the Application REST API object instance.
        ///</summary>
		public IApplication Application { get; private set; }

		public ILoggedInAuth LoggedInAuth
		{
			get; private set;
		}

    	///<summary>
        /// Gets or sets the Data REST API object instance.
        ///</summary>
		public IData Data { get; private set; }

        ///<summary>
        /// Gets or sets the Stream REST API object instance.
        ///</summary>
		public IStream Stream { get; private set; }

        ///<summary>
        /// Gets or sets the Status REST API object instance.
        ///</summary>
		public IStatus Status { get; private set; }

        ///<summary>
        /// Gets or sets the Video REST API object instance.
        ///</summary>
		public IVideo Video { get; private set; }

        ///<summary>
        /// Gets or sets the Links REST API object instance.
        ///</summary>
		public ILinks Links { get; private set; }

        ///<summary>
        /// Gets or sets the Notes REST API object instance.
        ///</summary>
		public INotes Notes { get; private set; }

        ///<summary>
        /// Gets or sets the AuthToken string.
        ///</summary>
        public string AuthToken { get; set; }

        ///<summary>
        /// Gets or sets the LoginUrl sring.
        ///</summary>
        public string LoginUrl
        {
            get
            {
                var args = new object[2];
                args[0] = Session.ApplicationKey;
                args[1] = AuthToken;

                return String.Format(CultureInfo.InvariantCulture, Constants.FacebookLoginUrl, args);
            }
        }

        ///<summary>
        /// Gets or sets the LogOffUrl string.
        ///</summary>
        public string LogOffUrl
        {
            get
            {
                var args = new object[2];
                args[0] = Session.ApplicationKey;
                args[1] = AuthToken;

                return String.Format(CultureInfo.InvariantCulture, Constants.FacebookLogoutUrl, args);
            }
        }

        #endregion Public Properties

        #region Internal Properties

        /// <summary>
        /// Gets the InstalledCulture CultureInfo object.
        /// </summary>
        internal CultureInfo InstalledCulture { get; private set; }

        #endregion Internal Properties

        #region Methods

        #region Constructor

		///// <summary>
		///// Facebook API Instance
		///// </summary>
		///// <param name="session"></param>
		//public Api(FacebookSession session) : base(networkWrapper, session)
		//{
		//    Initialize(NetworkWrapper, Session);
		//}
		

		public Api(IFacebookNetworkWrapper networkWrapper, IFacebookSession session)
			: base(networkWrapper, session)
		{
		    Initialize(Session);
		}

        private const string FACEBOOK_SESSION = "FACEBOOK_SESSION";
		//public Api()
		//{
		//    IFacebookSession session;
		//    if (HttpContext.Current == null)
		//        session = HttpContext.Current.Items[FACEBOOK_SESSION] as IFacebookSession;
		//    else
		//        session = CallContext.GetData(FACEBOOK_SESSION) as IFacebookSession;
		//    Initialize(NetworkWrapper, Session);
		//    //if (session == null)
		//    //    throw new ArgumentNullException("FacebookSession", "The facebook session must exist");
		//}

		public IFacebookApi Initialize(IFacebookSession session)
		{
			AuthToken = string.Empty;

#if !SILVERLIGHT
			InstalledCulture = CultureInfo.InstalledUICulture;
#else
            InstalledCulture = CultureInfo.CurrentUICulture;
#endif

			Session = session;

			
			Video = new Video(NetworkWrapper, Session);
			Marketplace = new Marketplace(NetworkWrapper, Session);
			Admin = new Admin(NetworkWrapper, Session);
			Photos = new Photos(NetworkWrapper, Session);
			Users = new Users(NetworkWrapper, Session);
			Friends = new Friends(NetworkWrapper, Users, Session);
			Events = new Events(NetworkWrapper, Session);
			Groups = new Groups(NetworkWrapper, Session);
			Notifications = new Notifications(NetworkWrapper, Session);
			Profile = new Profile(NetworkWrapper, Session);
			Fbml = new Fbml(NetworkWrapper, Session);
			Feed = new Feed(NetworkWrapper, Session);
			Fql = new Fql(NetworkWrapper, Session);
			LiveMessage = new LiveMessage(NetworkWrapper, Session);
			Message = new Message(NetworkWrapper, Session);
			//Batch = new Batch(NetworkWrapper, Session);
			Pages = new Pages(NetworkWrapper, Session);
			Application = new Application(NetworkWrapper, Session);
			Data = new Data(NetworkWrapper, Session);
			Permissions = new Permissions(NetworkWrapper, Session);
			Connect = new Connect(NetworkWrapper, Session);
			Comments = new Comments(NetworkWrapper, Session);
			Stream = new Stream(NetworkWrapper, Session);
			Status = new Status(NetworkWrapper, Session);
			Links = new Links(NetworkWrapper, Session);
			Notes = new Notes(NetworkWrapper, Session);
			Intl = new Intl(NetworkWrapper, Session);

			//Batch.Batch = Batch;
			Permissions.Permissions = Permissions;
			//Batch.Permissions = Permissions;
			//Permissions.Batch = Batch;

			foreach (IAuthenticatedService restBase in new IAuthenticatedService[] {LoggedInAuth, Video, Marketplace, Admin, Photos, Users, Friends, Events,
				Groups, Notifications, Profile, Fbml, Feed, Fql, LiveMessage, Message, Pages, Application, Data, Connect, Comments,
				Stream, Status, Links, Notes})
			{
				//restBase.Batch = Batch;
				restBase.Permissions = Permissions;
			}

			return this;
		}

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Constructs the URL to use to redirect a user to enable a given extended permission for your application.
        /// </summary>
        /// <param name="permission">The specific permission to enable.</param>
        /// <returns>The URL to redirect users to.</returns>
        public string ExtendedPermissionUrl(Enums.ExtendedPermissions permission)
        {
            var args = new object[2];
            args[0] = Session.ApplicationKey;
            args[1] = permission;

            return String.Format(CultureInfo.InvariantCulture, Constants.FacebookRequestExtendedPermissionUrl, args);
        }

        #endregion Public Methods

        #endregion Methods
    }
}

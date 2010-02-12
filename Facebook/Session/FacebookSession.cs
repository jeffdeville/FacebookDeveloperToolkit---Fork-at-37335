using System;
using System.ComponentModel;
using System.Web.Configuration;
using Facebook.Rest;

namespace Facebook.Session
{
    /// <summary>
    /// Base class for session object
    /// </summary>
    public class FacebookSession : IFacebookSession
    {
        private FacebookConfiguration _facebookConfig;

        public FacebookSession() 
        {
        	_facebookConfig = new FacebookConfiguration();
        }
		//public FacebookSession() : this(null, null) { }
		public FacebookSession(FacebookConfiguration facebookConfig)
		{
			_facebookConfig = facebookConfig;
		}

        /// <summary>
        /// Application key
        /// </summary>
        public string ApplicationKey
        {
            get { return _facebookConfig.ApiKey; }
        }

        /// <summary>
        /// Application secret
        /// </summary>
        public string ApplicationSecret
        {
            get { return _facebookConfig.Secret; }
        }

        /// <summary>
        /// Session key
        /// </summary>
        public string SessionKey
        {
            get;
            set;
        }

        /// <summary>
        /// Session secret
        /// </summary>
        public string SessionSecret
        {
            get;
            set;
        }

        /// <summary>
        /// Expiry time
        /// </summary>
        public DateTime ExpiryTime
        {
            get;
            protected internal set;
        }

        /// <summary>
        /// Whether or not the session expires
        /// </summary>
        public bool SessionExpires
        {
            get; 
            set;
        }

        /// <summary>
        /// User Id
        /// </summary>
        public long UserId
        {
            get;
            set;
        }
        ///<summary>
        /// Whether the Http Post and Response should be compressed
        ///</summary>
        public bool CompressHttp
        {
            get;
            set;
        }

        /// <summary>
        /// Secret key that needs to be used to encrypt requests
        /// </summary>
        public virtual string Secret
        {
            get
            {
                return SessionSecret ?? ApplicationSecret;
            }
        }


    }
}

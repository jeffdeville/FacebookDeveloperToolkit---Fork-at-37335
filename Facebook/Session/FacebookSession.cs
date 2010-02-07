using System;
using System.ComponentModel;
using System.Web.Configuration;
using Facebook.Rest;

namespace Facebook.Session
{
    /// <summary>
    /// Base class for session object
    /// </summary>
    public abstract class FacebookSession : IFacebookSession
    {
        public FacebookSession() : this(null, null){}

        public FacebookSession(string appKey, string appSecret)
        {
            if (appKey == null || appSecret == null)
            {
                ApplicationKey = WebConfigurationManager.AppSettings["ApiKey"];
                ApplicationSecret = WebConfigurationManager.AppSettings["Secret"];
            }
            else
            {
                ApplicationKey = appKey;
                ApplicationSecret = appSecret;
            }
        }

        public void VerifyKeyAndSecretExist()
        {
            if (string.IsNullOrEmpty(ApplicationKey) || string.IsNullOrEmpty(ApplicationSecret))
            {
                throw new Exception(
                    "Session must have application key and secret before logging in." + Environment.NewLine +
                    "To set them in your web.config, use something like the following:" + Environment.NewLine +
                    "<appSettings>" + Environment.NewLine +
                    "   <add key=\"ApiKey\" value =\"YOURApiKEY\"/>" + Environment.NewLine +
                    "   <add key=\"Secret\" value =\"YOURSECRET\"/>" + Environment.NewLine +
                    "</appSettings>\"");
            }
        }

        /// <summary>
        /// Application key
        /// </summary>
        public string ApplicationKey
        {
            get;
            set;
        }

        /// <summary>
        /// Application secret
        /// </summary>
        public string ApplicationSecret
        {
            get;
            set;
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
            protected set;
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

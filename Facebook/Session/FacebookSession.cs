using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Web.Configuration;
using Facebook.Rest;
using Facebook.Schema;

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
		/// List of extended permissions required by this application
		/// </summary>
		public List<Enums.ExtendedPermissions> RequiredPermissions { get; set; }

		/// <summary>
		/// Check if user has the proper permissions for this app
		/// </summary>
		public string CheckPermissions()
		{
#if !SILVERLIGHT
			if (RequiredPermissions != null)
			{
				List<Enums.ExtendedPermissions> permissionsToApprove = new List<Enums.ExtendedPermissions>();
				string query = string.Format("select {0} from permissions where uid = {1}", PermissionsToString(RequiredPermissions), this.UserId); ;

				var fql = new Api(this).Fql;

				var permission = fql.Query<permissions_response>(query);

				foreach (Enums.ExtendedPermissions p in this.RequiredPermissions)
				{
					FieldInfo f = permission.permissions.GetType().GetField(p.ToString());
					if (f == null) continue;
					var hasPermission = (bool)f.GetValue(permission.permissions);
					if (!hasPermission) permissionsToApprove.Add(p);
				}

				if (permissionsToApprove.Count != 0)
					return PermissionsToString(permissionsToApprove);
			}
#endif
			return null;
		}

		/// <summary>
		/// Convert permission list to "read_stream, status_update, photo_upload, publish_stream" format
		/// </summary>
		/// <param name="permissions"></param>
		/// <returns>This method returns a string of permissions.</returns>
		protected string PermissionsToString(List<Enums.ExtendedPermissions> permissions)
		{
			StringBuilder sb = new StringBuilder();

			int i = 0;
			foreach (Enums.ExtendedPermissions permission in permissions)
			{
				sb.Append(permission.ToString());
				i++;
				if (i < permissions.Count)
				{
					sb.Append(",");
				}
			}

			return sb.ToString();
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

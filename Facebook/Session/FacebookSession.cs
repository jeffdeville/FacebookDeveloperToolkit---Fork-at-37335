using System;
using System.ComponentModel;
using Facebook.Schema;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using Facebook.Rest;

namespace Facebook.Session
{
    /// <summary>
    /// Base class for session object
    /// </summary>
    public abstract class FacebookSession : IFacebookSession
    {
        const string _promptPermissionsUrl = "http://www.facebook.com/connect/prompt_permissions.php?api_key={0}&v=1.0&next={1}&display=popup&ext_perm={2}&enable_profile_selector=1&profile_selector_ids={3}";
        const string _promptPermissionsNextUrl = "http://www.facebook.com/connect/login_success.html?xxRESULTTOKENxx";
        /// <summary>
        /// List of extended permissions required by this application
        /// </summary>
        public List<Enums.ExtendedPermissions> RequiredPermissions { get; set; }

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
        
        /// <summary>
        /// Logs in user
        /// </summary>
        public abstract void Login();

        /// <summary>
        /// Logs out user
        /// </summary>
        public abstract void Logout();

        /// <summary>
        /// Login completed event
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> LoginCompleted;

        /// <summary>
        /// Logout completed event
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> LogoutCompleted;

        /// <summary>
        /// Called when log in completed
        /// </summary>
        /// <param name="e"></param>
        internal protected void OnLoggedIn(Exception e)
        {
            if (LoginCompleted != null)
            {
                LoginCompleted(this, new AsyncCompletedEventArgs(e, false, null));
            }
        }

        /// <summary>
        /// Called when log out completes
        /// </summary>
        /// <param name="e"></param>
        internal protected void OnLoggedOut(Exception e)
        {
            if (LogoutCompleted != null)
            {
                LogoutCompleted(this, new AsyncCompletedEventArgs(e, false, null));
            }
        }

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

                var fql = new Api().Initialize(this).Fql;

                var permission = fql.Query<permissions_response>(query);

                foreach (Enums.ExtendedPermissions p in this.RequiredPermissions)
                {
                    FieldInfo f = permission.permissions.GetType().GetField(p.ToString());
                    if (f != null)
                    {
                        bool hasPermission = (bool)f.GetValue(permission.permissions);
                        if (!hasPermission)
                        {
                            permissionsToApprove.Add(p);
                        }
                    }
                }

                if (permissionsToApprove.Count != 0)
                {
                    return PermissionsToString(permissionsToApprove);
                }
            }
#endif
            return null;

        }
        /// <summary>
        /// Gets login url which can be used to login to facebook server
        /// </summary>
        /// <returns>This method returns the Facebook Login URL.</returns>
        public string GetPermissionUrl(string permissionString)
        {
            return string.Format(_promptPermissionsUrl, this.ApplicationKey, _promptPermissionsNextUrl, permissionString, this.UserId);
        }
        /// <summary>
        /// Gets login url which can be used to login to facebook server
        /// </summary>
        /// <returns>This method returns the Facebook Login URL.</returns>
        public string GetPermissionUrl(string permissionString, string nextUrl)
        {
            return string.Format(_promptPermissionsUrl, this.ApplicationKey, nextUrl, permissionString, this.UserId);
        }
        /// <summary>
        /// Convert permission list to "read_stream, status_update, photo_upload, publish_stream" format
        /// </summary>
        /// <param name="permissions"></param>
        /// <returns>This method returns a string of permissions.</returns>
        private string PermissionsToString(List<Enums.ExtendedPermissions> permissions)
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

    }
}

using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Facebook.Schema;

namespace Facebook.Session
{
    public class FacebookAuthorization : IFacebookAuthorization
    {
        const string _promptPermissionsUrl = "http://www.facebook.com/connect/prompt_permissions.php?api_key={0}&v=1.0&next={1}&display=popup&ext_perm={2}&enable_profile_selector=1&profile_selector_ids={3}";
        const string _promptPermissionsNextUrl = "http://www.facebook.com/connect/login_success.html?xxRESULTTOKENxx";

        /// <summary>
        /// Check if user has the proper permissions for this app
        /// </summary>
        public string CheckPermissions(List<Enums.ExtendedPermissions> requiredPermissions)
        {
#if !SILVERLIGHT
            if (requiredPermissions != null)
            {
                List<Enums.ExtendedPermissions> permissionsToApprove = new List<Enums.ExtendedPermissions>();
                string query = string.Format("select {0} from permissions where uid = {1}", PermissionsToString(requiredPermissions), this.UserId); ;

                var fql = new FacebookApi().Initialize(this).Fql;

                var permission = fql.Query<permissions_response>(query);

                foreach (Enums.ExtendedPermissions p in requiredPermissions)
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
    }
}
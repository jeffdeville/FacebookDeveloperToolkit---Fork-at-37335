#if SILVERLIGHT

using System;
using System.Runtime.Serialization;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Browser;
using Facebook.BindingHelper;
using Facebook.Rest;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Session
{
    /// <summary>
    /// Represents session object for pages hosted in browser
    /// </summary>
    public class BrowserSession : FacebookSession
    {
        Enums.ExtendedPermissions[] _permissions;

        /// <summary>
        /// Occurs when permission dialog is dismissed by user
        /// </summary>
        public event EventHandler<PermissionCheckCompletedEventArgs> ShowPermissionDialogCompleted;
        /// <summary>
        /// Occurs when permission dialog is dismissed by user
        /// </summary>
        public event EventHandler<GetStatusCompletedEventArgs> GetStatusCompleted;


        #region Public Methods

        /// <summary>
        /// Creates new browsersession object
        /// </summary>
        /// <param name="appKey">Application Key</param>
        /// <param name="appSecret">Application Secret key</param>
        public BrowserSession(string appKey) 
            : this(appKey, null)
        {
        }

        /// <summary>
        /// Creates new browsersession object
        /// </summary>
        /// <param name="appKey">Application Key</param>
        /// <param name="appSecret">Application Secret key</param>
        /// <param name="permissions">Extended permissions to be requested from serve</param>
        public BrowserSession(string appKey, Enums.ExtendedPermissions[] permissions)
        {
            this.ApplicationKey = appKey;
            this._permissions = permissions;

            HtmlPage.RegisterScriptableObject("FacebookLoginControl", this);
            InvokeScriptCall("facebook_init", ApplicationKey);
            InvokeScriptCall("isUserConnected");
        }

        internal BrowserSession()
        {
        }
        /// <summary>
        /// Logs user to this session
        /// </summary>
        public override void Login()
        {
            InvokeScriptCall("facebook_login");
        }

        /// <summary>
        /// Logs user out of this session
        /// </summary>
        public override void Logout()
        {
            InvokeScriptCall("facebook_logout");
        }


        #endregion

        #region Script Helpers

        void InvokeScriptCall(string method, params Object[] args)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                try
                {
                    HtmlPage.Window.Invoke(method, args);
                }
                catch (InvalidOperationException e)
                {
                    string message = string.Format("Could not invoke script function {0}. Please ensure fblogin.js script is included OR this function exist in your script file", method);
                    throw new FacebookException(message, e);
                }
            });
        }
        /// <summary>
        /// Status FB.ConnectState.connected : 1
        /// Status FB.ConnectState.userNotLoggedIn : 2
        /// Status FB.ConnectState.appNotAuthorized : ?
        /// </summary>
        /// <param name="Status"></param>
        [ScriptableMember]
        public void GetStatus(int Status)
        {
            if (GetStatusCompleted != null)
                GetStatusCompleted(this, new GetStatusCompletedEventArgs(Status, null));

            if (Status == 1)
            {
                InvokeScriptCall("facebook_onlogin");
            }
        }


        /// <summary>
        /// Called from script hosting the control when login completes.
        /// </summary>
        /// <param name="sessionKey">session key obtained from server</param>
        /// <param name="secret">secret key obtained from server</param>
        /// <param name="expires">expiry time obtained from server</param>
        /// <param name="userId">userId obtained from server</param>
        [ScriptableMember]
        public void LoggedIn(string sessionKey, string secret, int expires, long userId)
        {
            this.SessionKey = sessionKey;
            this.UserId = userId;
            this.ExpiryTime = DateHelper.ConvertUnixTimeToDateTime(expires);
            this.SessionSecret = secret;

            if (this._permissions != null && this._permissions.Length != 0)
            {
                string query = string.Format("select {0} from permissions where uid = {1}", PermissionsToString(this._permissions), this.UserId); ;

                Fql fql = new Fql(this);
                
				fql.QueryAsync(query,
				            new Fql.QueryCallback<permissions_response>(PermissionCheckCallBack),
				            null);
            }
            else
            {
                this.OnLoggedIn(null);
            }
        }

        /// <summary>
        /// Called from script hosting the control when logout completes
        /// </summary>
        [ScriptableMember]
        public void LoggedOut()
        {
            this.OnLoggedOut(null);
        }

        /// <summary>
        /// Called from script hosting the control when permission dialog is dismissed
        /// </summary>
        /// <param name="accepted"></param>
        [ScriptableMember]
        public void PermissionCallback(string accepted)
        {
            OnPermissionCheckCompleted(accepted, null);
        }


        #endregion


        #region Helpers

        /// <summary>
        /// Convert permission list to "read_stream, status_update, photo_upload, publish_stream" format
        /// </summary>
        /// <param name="permissions"></param>
        /// <returns>This method returns a string of permissions.</returns>
        private string PermissionsToString(Enums.ExtendedPermissions[] permissions)
        {
            StringBuilder sb = new StringBuilder();

            int i = 0;
            foreach (Enums.ExtendedPermissions permission in permissions)
            {
                sb.Append(permission.ToString());
                i++;
                if (i < permissions.Length)
                {
                    sb.Append(", ");
                }
            }

            return sb.ToString();
        }

        void PermissionCheckCallBack(permissions_response permission, Object state, FacebookException ex)
        {
            List<Enums.ExtendedPermissions> permissionsToApprove = new List<Enums.ExtendedPermissions>();

            if (ex == null)
            {
                foreach (Enums.ExtendedPermissions p in this._permissions)
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
                    string permString = PermissionsToString(permissionsToApprove.ToArray());
                    InvokeScriptCall("facebook_prompt_permission", permString);
                }
                else
                {
                    OnPermissionCheckCompleted(PermissionsToString(this._permissions), null);
                }
            }
            else
            {
                OnPermissionCheckCompleted(null, ex);
            }
        }

        private void OnPermissionCheckCompleted(string accepted, Exception e)
        {
            this.OnLoggedIn(null);
            if (ShowPermissionDialogCompleted != null)
            {
                ShowPermissionDialogCompleted(this, new PermissionCheckCompletedEventArgs(accepted, e));
            }
        }

        #endregion
    }

    /// <summary>
    /// This is used to parse JSON.
    /// parse "[{\"read_stream\":0,\"status_update\":0,\"photo_upload\":0,\"publish_stream\":0}]"
    /// </summary>
    [DataContract]
    internal class JSONPermission
    {
        [DataMember]
        public bool read_stream;
        [DataMember]
        public bool status_update;
        [DataMember]
        public bool photo_upload;
        [DataMember]
        public bool publish_stream;
        [DataMember]
        public bool email;
        [DataMember]
        public bool create_listing;
        [DataMember]
        public bool offline_access;
        [DataMember]
        public bool rsvp_event;
        [DataMember]
        public bool sms;
        [DataMember]
        public bool export_stream;
    }

    /// <summary>
    /// Provides data to ShowPermissionDialogCompleted event 
    /// </summary>
    public class PermissionCheckCompletedEventArgs : AsyncCompletedEventArgs
    {
        /// <summary>
        /// Result of permission dialog
        /// </summary>
        public string Accepted
        {
            get;
            private set;
        }

        internal PermissionCheckCompletedEventArgs(string accepted, Exception e) 
            : base(e, false, null)            
        {
            this.Accepted = accepted;
        }
    }

    /// <summary>
    /// Provides data to ShowPermissionDialogCompleted event 
    /// </summary>
    public class GetStatusCompletedEventArgs : AsyncCompletedEventArgs
    {
        /// <summary>
        /// Result of permission dialog
        /// </summary>
        public int Status
        {
            get;
            private set;
        }

        internal GetStatusCompletedEventArgs(int status, Exception e)
            : base(e, false, null)
        {
            this.Status = status;
        }
    }

}
#endif
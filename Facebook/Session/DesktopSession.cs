using System;
using System.ComponentModel;
using Facebook.Rest;
#if !SILVERLIGHT
using Facebook.Session;
using Facebook.Utility;
using Facebook.Schema;
using System.Windows.Forms;
using Facebook.Session.DesktopPopup;
using System.Text;
using System.Collections.Generic;
using System.Reflection;

namespace Facebook.Session
{
    /// <summary>
    /// Represents session object for desktop apps
    /// </summary>
    public class DesktopSession : FacebookSession
    {
        const string _loginUrl = "http://www.facebook.com/login.php?api_key={0}&connect_display=popup&v=1.0&next=http://www.facebook.com/connect/login_success.html&cancel_url=http://www.facebook.com/connect/login_failure.html&fbconnect=true&return_session=true";
        bool _isWPF = false;


        #region Public Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appKey">Application Key</param>
        /// <param name="isWPF">true for WPF, false for winform</param>
        public DesktopSession(string appKey, bool isWPF)
            : this(appKey, null, null, isWPF)
        { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appKey">Application Key</param>
        /// <param name="isWPF">true for WPF, false for winform</param>
        /// <param name="permissions">list of extended permissions to prompt for upon login</param>
        public DesktopSession(string appKey, bool isWPF, List<Enums.ExtendedPermissions> permissions)
            : this(appKey, null, null, isWPF, permissions)
        { }

        /// <summary>
        /// Constructor - You should not need login function when using this constructor as this is a previously cached session
        /// </summary>
        /// <param name="appKey">Application Key</param>
        /// <param name="sessionSecret">Session secret - If previously cached</param>
        /// <param name="sessionKey">Session key - If previously cached</param>
        public DesktopSession(string appKey, string sessionSecret, string sessionKey)
            : this(appKey, sessionSecret, sessionKey, false)
        { }
        /// <summary>
        /// Constructor - You should not need login function when using this constructor as this is a previously cached session
        /// </summary>
        /// <param name="appKey">Application Key</param>
        /// <param name="sessionSecret">Session secret - If previously cached</param>
        /// <param name="sessionKey">Session key - If previously cached</param>
        /// <param name="isWPF">true for WPF, false for winform</param>
        public DesktopSession(string appKey, string sessionSecret, string sessionKey, bool isWPF)
            : this(appKey, sessionSecret, sessionKey, isWPF, null)
        { }
        /// <summary>
        /// Constructor - You should not need login function when using this constructor as this is a previously cached session
        /// </summary>
        /// <param name="appKey">Application Key</param>
        /// <param name="sessionSecret">Session secret - If previously cached</param>
        /// <param name="sessionKey">Session key - If previously cached</param>
        /// <param name="isWPF">true for WPF, false for winform</param>
        /// <param name="permissions">list of extended permissions to prompt for upon login</param>
        public DesktopSession(string appKey, string sessionSecret, string sessionKey, bool isWPF, List<Enums.ExtendedPermissions> permissions)
        {
            this.ApplicationKey = appKey;
            this.SessionSecret = sessionSecret;
            this.SessionKey = sessionKey;
            _isWPF = isWPF;
            RequiredPermissions = permissions;
        }
        /// <summary>
        /// Logs into session
        /// </summary>
        public override void Login()
        {
            if (_isWPF)
            {
                var formLogin = new FacebookWPFBrowser(GetLoginUrl());
                formLogin.Title = "Facebook: Login";
                formLogin.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                formLogin.Width = 626;
                formLogin.Height = 431;
                formLogin.WindowStyle = System.Windows.WindowStyle.ToolWindow;
                bool? dialogResult = formLogin.ShowDialog();
                if (dialogResult.HasValue && dialogResult.Value)
                {
                    CompleteLogin(formLogin.SessionProperties);
                }
                else
                {
                    OnLoggedIn(new FacebookException("Login attempt failed"));
                }

            }
            else
            {
                DialogResult result;
                using (var formLogin = new FacebookWinformBrowser(GetLoginUrl()))
                {
                    result = formLogin.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        CompleteLogin(formLogin.SessionProperties);
                    }
                    else
                    {
                        OnLoggedIn(new FacebookException("Login attempt failed"));
                    }
                }

            }
        }
        private void CompleteLogin(Dictionary<string, string> sessionProperties)
        {
            this.SessionExpires = sessionProperties["expires"] != "0";
            this.ExpiryTime = DateHelper.ConvertUnixTimeToDateTime(long.Parse(sessionProperties["expires"]));
            this.SessionKey = sessionProperties["session_key"];
            this.SessionSecret = sessionProperties["secret"];

            if (sessionProperties["uid"] != null)
            {
                this.UserId = long.Parse(sessionProperties["uid"]);
            }
            OnLoggedIn(null);

        }

        /// <summary>
        /// logs out of session
        /// </summary>
        public override void Logout()
        {
            OnLoggedOut(null);
        }


        /// <summary>
        /// Gets login url which can be used to login to facebook server
        /// </summary>
        /// <returns>This method returns the Facebook Login URL.</returns>
        public string GetLoginUrl()
        {
            string loginUrl = String.Format(_loginUrl, ApplicationKey);

            if (RequiredPermissions != null)
            {
                loginUrl += "&req_perms=" + PermissionsToString(RequiredPermissions);
            }
            return loginUrl;
        }

        #endregion

    }
}
#endif

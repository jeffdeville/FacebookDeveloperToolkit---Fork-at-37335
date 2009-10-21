using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Browser;
using System.ComponentModel;
using System.Xml.Linq;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Reflection;

using Facebook;
using Facebook.Session;
using Facebook.Utility;
using Facebook.Rest;

namespace Facebook.Controls
{
    public partial class LoginControl : UserControl
    {

        enum LoginStatus
        {
            Unknown,
            IsLoggedin,
            LoginInProgress,
            IsLoggedout,
            LogoutinProgress,
        }

        int timerClickCount;

        bool _bRunningOffline = false;
        List<Facebook.Schema.Enums.ExtendedPermissions> _PermissionsRequired;
        List<Facebook.Schema.Enums.ExtendedPermissions> _PermissionsToApprove = new List<Facebook.Schema.Enums.ExtendedPermissions>();

        FacebookSession _session;

        // To store session key for offline usage.
        private IsolatedCache cache = new IsolatedCache();

        DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// Occurs when Login is completed
        /// </summary>
        public event EventHandler<LoginCompletedEventArgs> LoginCompleted;

        /// <summary>
        /// Occurs when Logout is completed
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> LogoutCompleted;

        /// <summary>
        /// Occurs when permission dialog is dismissed by user
        /// </summary>
        public event EventHandler<PermissionCompletedEventArgs> ShowPermissionDialogCompleted;

        /// <summary>
        /// Constructor
        /// </summary>
        public LoginControl()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(LoginControl_Loaded);

            _bRunningOffline = System.Windows.Application.Current.IsRunningOutOfBrowser;
        }

        void LoginControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ApplicationKey"></param>
        /// <param name="ApplicationSecret"></param>
        /// <param name="RequiredPermissions">Comma separated permission like this: "read_stream, status_update, photo_upload, publish_stream"</param>
        public void Initialize(string ApplicationKey, string ApplicationSecret, List<Facebook.Schema.Enums.ExtendedPermissions> RequiredPermissions)
        {
            _PermissionsRequired = RequiredPermissions;

            if (_bRunningOffline)
            {
                string sessionKey = this.cache.GetData(IsolatedCache.SessionKey);
                string sessionSecret = this.cache.GetData(IsolatedCache.SessionSecret);

                // For the offline Uri, set the Uri to the start.html

                String originUri = System.Windows.Application.Current.Host.Source.OriginalString;
                // Strip the last two "/".
                originUri = originUri.Substring(0, originUri.LastIndexOf('/'));
                originUri = originUri.Substring(0, originUri.LastIndexOf('/'));

                Uri webloginUri = new Uri(new Uri(originUri), "SilverFaceStart.aspx?state=oobLogin");

                this.Login.NavigateUri = webloginUri;

                LogSignInEvent("Initialize: set login uril " + webloginUri.OriginalString);
                _session = new CachedSession(ApplicationKey, null, sessionKey, sessionSecret);
                _session.LoginCompleted += new EventHandler<AsyncCompletedEventArgs>(session_LoginCompleted);
                _session.LogoutCompleted += new EventHandler<AsyncCompletedEventArgs>(session_LogoutCompleted);


                if (sessionKey != null)
                {
                    LogSignInEvent("Initialize: Session key found in cache" + sessionKey);

                    Users users = new Users(_session);
                    users.GetLoggedInUserAsync(new Facebook.Rest.Users.GetLoggedInUserCallback(OnGetLoggedInUser), false);

                    SetVisualStatus(LoginStatus.LoginInProgress);
                }
                else
                {
                    SetVisualStatus(LoginStatus.IsLoggedout);
                }

            }
            else
            {
                BrowserSession session = new BrowserSession(ApplicationKey, null, RequiredPermissions == null? null : RequiredPermissions.ToArray());
                _session = session;
                session.LoginCompleted += new EventHandler<AsyncCompletedEventArgs>(session_LoginCompleted);
                session.LogoutCompleted += new EventHandler<AsyncCompletedEventArgs>(session_LogoutCompleted);
                session.ShowPermissionDialogCompleted += new EventHandler<PermissionCheckCompletedEventArgs>(session_ShowPermissionDialogCompleted);
                session.GetStatusCompleted += new EventHandler<GetStatusCompletedEventArgs>(session_GetStatusCompleted);
            }
        }

        private void SetVisualStatus(LoginStatus  status)
        {
            switch (status)
            {
                case LoginStatus.IsLoggedin:
                    myloginAnimation.Stop();
                    Login.Visibility = Visibility.Collapsed;
                    Logout.Visibility = Visibility.Visible;
                    break;

                case LoginStatus.IsLoggedout:
                    myloginAnimation.Stop();
                    Login.Visibility = Visibility.Visible;
                    Logout.Visibility = Visibility.Collapsed;
                    break;

                case LoginStatus.LoginInProgress:
                    myloginAnimation.Start(null);
                    Login.Visibility = Visibility.Collapsed;
                    Logout.Visibility = Visibility.Collapsed;
                    break;

                case LoginStatus.LogoutinProgress:
                    myloginAnimation.Start(null);
                    Login.Visibility = Visibility.Collapsed;
                    Logout.Visibility = Visibility.Collapsed;
                    break;

                default:
                    myloginAnimation.Stop();
                    Login.Visibility = Visibility.Collapsed;
                    Logout.Visibility = Visibility.Collapsed;
                    break;
            
            }
        }

        /// <summary>
        /// Login button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            SetVisualStatus(LoginStatus.LoginInProgress);

            if (_bRunningOffline)
            {
                // Start the timer to query whether user has started to login
                timer.Interval = new TimeSpan(0, 0, 0, 2);
                timer.Tick += new EventHandler(timer_Tick);
                timer.Start();

                timerClickCount = 0;
                LogSignInEvent("Login_Click: start timer to look for session key");
            }
            else
            {
                _session.Login();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            LogSignInEvent("timer_Tick: timer_Tick is called");

            string sessionKey = this.cache.GetData(IsolatedCache.SessionKey);
            string sessionSecret = this.cache.GetData(IsolatedCache.SessionSecret);
            timerClickCount++;

            if (sessionKey != null)
            {
                _session = new CachedSession(_session.ApplicationKey, null, sessionKey, sessionSecret);
                _session.LoginCompleted += new EventHandler<AsyncCompletedEventArgs>(session_LoginCompleted);
                _session.LogoutCompleted += new EventHandler<AsyncCompletedEventArgs>(session_LogoutCompleted);

                Users users = new Users(_session);
                users.GetLoggedInUserAsync(new Facebook.Rest.Users.GetLoggedInUserCallback(OnGetLoggedInUser), true);

                timer.Stop();

                LogSignInEvent("timer_Tick: sessionKey found " + sessionKey);
            }
            else
            {
                // Waiter longer enough, let stop login animation, but not stopping timer.
                if (timerClickCount > 30)
                {
                    SetVisualStatus(LoginStatus.IsLoggedout);
                }
                // Long enough, stop timer
                if (timerClickCount > 150)
                {
                    timer.Stop();
                }
            }

        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="o">boolean, true means if fail, restart timer</param>
        /// <param name="e"></param>
        void OnGetLoggedInUser(long uid, Object o, FacebookException e)
        {
            if (e == null)
            {
                // Valid key start loggin process.
                _session.UserId = uid;
                Dispatcher.BeginInvoke(() =>
                {
                    SetVisualStatus(LoginStatus.IsLoggedin);

                    LoggedIn();

                    if (_PermissionsRequired != null && _PermissionsRequired.Count != 0 && ShowPermissionDialogCompleted != null)
                    {
                        ShowPermissionDialogCompleted(this, new PermissionCompletedEventArgs(PermissionsToString(_PermissionsRequired), _session));
                    }

                });

                LogSignInEvent("OnGetLoggedInUser: we have found the valid session key!");

            }
            else
            {
                LogSignInEvent("OnGetLoggedInUser: get exception " + e.Message);

                Dispatcher.BeginInvoke(() =>
                {
                    SetVisualStatus(LoginStatus.IsLoggedout);

                    if ((bool)o == true)
                        timer.Start();
                });

            }

        }

        /// <summary>
        /// Logout button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            SetVisualStatus(LoginStatus.LogoutinProgress);

            _session.Logout();
        }


        void session_LoginCompleted(object sender, AsyncCompletedEventArgs e)
        {
            LoggedIn();
        }

        void LoggedIn()
        {
            Dispatcher.BeginInvoke(() =>
                {
                    SetVisualStatus(LoginStatus.IsLoggedin);
                    this.cache.SetData(IsolatedCache.SessionKey, _session.SessionKey);
                    this.cache.SetData(IsolatedCache.SessionSecret, _session.SessionSecret);

                    if (LoginCompleted != null)
                    {
                        LoginCompleted(this, new LoginCompletedEventArgs(_session));
                    }
                });
        }

        void session_LogoutCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.cache.SetData(IsolatedCache.SessionKey, null);
            this.cache.SetData(IsolatedCache.SessionSecret, null);
            SetVisualStatus(LoginStatus.IsLoggedout);

            if (LogoutCompleted != null)
            {
                LogoutCompleted(this, null);
            }
        }

        void session_ShowPermissionDialogCompleted(object sender, PermissionCheckCompletedEventArgs e)
        {
            if (ShowPermissionDialogCompleted != null)
            {
                ShowPermissionDialogCompleted(this, new PermissionCompletedEventArgs(e.Accepted, _session));
            }
        }

        /// Status FB.ConnectState.connected : 1
        /// Status FB.ConnectState.userNotLoggedIn : 2
        /// Status FB.ConnectState.appNotAuthorized : ?
        void session_GetStatusCompleted(object sender, GetStatusCompletedEventArgs e)
        {
            if (e.Status == 1)
            {
                // Display the logout button
                SetVisualStatus(LoginStatus.LoginInProgress);
            }
            else
            {
                SetVisualStatus(LoginStatus.IsLoggedout);
            }
        }

        // Log all sign in events as this is tricky
        private void LogSignInEvent(string LogString)
        {
            System.Diagnostics.Debug.WriteLine("LoginControl!" + LogString);
        }

        /// <summary>
        /// Convert permission list to "read_stream, status_update, photo_upload, publish_stream" format
        /// </summary>
        /// <param name="permissions"></param>
        /// <returns></returns>
        private string PermissionsToString(List<Facebook.Schema.Enums.ExtendedPermissions> permissions)
        {
            StringBuilder sb = new StringBuilder();

            int i = 0;
            foreach (Facebook.Schema.Enums.ExtendedPermissions permission in permissions)
            {
                sb.Append(permission.ToString());
                i++;
                if (i < permissions.Count)
                {
                    sb.Append(", ");
                }
            }

            return sb.ToString();
        }

    }

}

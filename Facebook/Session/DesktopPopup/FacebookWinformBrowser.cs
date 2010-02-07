using System;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Web;
using Facebook.Utility;
using System.Collections.Generic;

namespace Facebook.Session.DesktopPopup
{
    internal sealed partial class FacebookWinformBrowser : Form
    {
        private FacebookWinformBrowser()
        {
            InitializeComponent();
        }

        public Dictionary<string, string> SessionProperties
        {
            get;
            set;
        }
        public string WindowTitle
        {
            get { return this.Text; }
            set { this.Text = value; }
        }
        [SecurityPermission(SecurityAction.LinkDemand)]
        internal FacebookWinformBrowser(string loginUrl)
            : this(new Uri(loginUrl))
        {
        }

        [SecurityPermission(SecurityAction.LinkDemand)]
        internal FacebookWinformBrowser(Uri loginUrl)
            : this()
        {
            wbFacebookLogin.Navigate(loginUrl);
        }


        private void wbFacebookLogin_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if(e.Url.PathAndQuery.StartsWith("/connect/login_success.html"))
            {
                DialogResult = DialogResult.OK;
                var sessionString = HttpUtility.ParseQueryString(e.Url.Query).Get("session");
                SessionProperties = JSONHelper.ConvertFromJSONAssoicativeArray(sessionString);
            }
        }

        private void FacebookWinformBrowser_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
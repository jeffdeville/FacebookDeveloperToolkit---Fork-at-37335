using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Threading;
using Facebook;
using Facebook.Session;
using Facebook.Rest;
using Facebook.Schema;
using Facebook.Utility;
using System.Reflection;
using System.Web;

namespace Facebook.Session.DesktopPopup
{
    /// <summary>
    /// Interaction logic for FacebookWPFBrowser.xaml
    /// </summary>
    public partial class FacebookWPFBrowser : Window
    {
        Dispatcher dispatcher;
        string _url;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url">The url to navigate to when loaded.</param>
        public FacebookWPFBrowser(string url)
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            InitializeComponent();
            _url = url;
            dispatcher.BeginInvoke(new Action(() => this.webBrowser.Navigate(new Uri(_url))));
        }
        /// <summary>
        /// Key value pairs of the session information returned by facebook includes session_key, secret, expires and uid
        /// </summary>
        public Dictionary<string, string> SessionProperties
        {
            get;
            set;
        }


        private void webBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Uri.PathAndQuery.StartsWith("/connect/login_success.html"))
            {
                var sessionString = HttpUtility.ParseQueryString(e.Uri.Query).Get("session");
                SessionProperties = JSONHelper.ConvertFromJSONAssoicativeArray(sessionString);
                DialogResult = true;
            }
        }  

    }
}

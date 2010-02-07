using System;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Facebook.Winforms.Forms
{
    internal sealed partial class FacebookAuthentication : Form
    {
        private FacebookAuthentication()
        {
            InitializeComponent();
        }

        [SecurityPermission(SecurityAction.LinkDemand)]
        internal FacebookAuthentication(string loginUrl)
            : this(new Uri(loginUrl))
        {
        }

        [SecurityPermission(SecurityAction.LinkDemand)]
        internal FacebookAuthentication(Uri loginUrl)
            : this()
        {
            wbFacebookLogin.Navigate(loginUrl);
        }


        private void wbFacebookLogin_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (e.Url.PathAndQuery.Contains("desktopapp.php"))
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void FacebookAuthentication_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
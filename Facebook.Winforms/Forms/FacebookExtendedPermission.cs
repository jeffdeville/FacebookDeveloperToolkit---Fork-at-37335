using System;
using System.Security.Permissions;
using System.Windows.Forms;
using Facebook.Winforms.Components;
using Facebook.Schema;

namespace Facebook.Winforms.Forms
{
    internal sealed partial class FacebookExtendedPermission : Form
    {
        private readonly FacebookService _facebookService;
        private readonly Enums.ExtendedPermissions _permission;

        private FacebookExtendedPermission()
        {
            InitializeComponent();
        }

        [SecurityPermission(SecurityAction.LinkDemand)]
        internal FacebookExtendedPermission(string loginUrl, Enums.ExtendedPermissions permission, FacebookService service)
            : this(new Uri(loginUrl), permission, service)
        {
        }

        [SecurityPermission(SecurityAction.LinkDemand)]
        internal FacebookExtendedPermission(Uri loginUrl, Enums.ExtendedPermissions permission, FacebookService service)
            : this()
        {
            _facebookService = service;
            _permission = permission;
            wbFacebookLogin.Navigate(loginUrl);
        }


        private void wbFacebookLogin_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            //if (e.Url.PathAndQuery.Contains("authorize.php") && e.Url.Fragment == "#")
            //{
            //    DialogResult = (_facebookService.HasPermission(_permission)) ? DialogResult.OK : DialogResult.Cancel;
            //}
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
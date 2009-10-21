using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Facebook.Controls
{
    public partial class ProfileListItem : BaseControl
    {
        public event EventHandler<ProfileItemSelectedEventArgs> ProfileItemSelected;

        private User _user = null;

        public User User
        {
            get { return _user; }
        }

        private ProfileListItem()
        {
            InitializeComponent();
        }

        public ProfileListItem(User user)
            : this()
        {
            _user = user;
            LoadUser(user);
        }

        private void LoadUser(User user)
        {
            pbProfilePicture.Image = user.Picture;
            lblName.Text = User.Name;
            lblNetworks.Text = DisplayNetworks(User.Affiliations);
        }

        private string DisplayNetworks(Collection<Network> networks)
        {
            StringBuilder networkList = new StringBuilder();

            foreach (Network network in networks)
            {
                networkList.AppendLine(network.Name);
            }

            return networkList.ToString();
        }

        private void lblName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ProfileItemSelected != null)
                ProfileItemSelected(this, new ProfileItemSelectedEventArgs(_user));
        }
    }
    public class ProfileItemSelectedEventArgs : EventArgs
    {
        private User _user;

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        public ProfileItemSelectedEventArgs(User user)
        {
            _user = user;
        }
    }
}

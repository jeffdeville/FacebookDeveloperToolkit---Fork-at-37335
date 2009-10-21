using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Facebook.Controls
{
    [ToolboxItem(true)]
    public partial class FriendList : BaseControl
    {
        public event EventHandler<FriendSelectedEventArgs> FriendSelected;

        private Collection<User> _friends = null;

        [Category("Facebook")]
        [Description("")]
        public Collection<User> Friends
        {
            get { return _friends; }
            set 
            {
                _friends = value;
                OnLoad();
            }
        }

        public FriendList()
        {
            InitializeComponent();
        }

        private void OnLoad()
        {
            if (!IsDesignTime() && _friends != null)
            {
                LoadFriends();
            }
        }

        private void LoadFriends()
        {
            flpFriends.Controls.Clear();

            int width = flpFriends.Width;

            foreach (User friend in _friends)
            {
                ProfileListItem profile = new ProfileListItem(friend);
                profile.Width = width - (flpFriends.Padding.Horizontal) - 20;
                flpFriends.Controls.Add(profile);
                profile.ProfileItemSelected += new EventHandler<ProfileItemSelectedEventArgs>(profile_ProfileItemSelected);
            }

            string plural = string.Empty;

            if (_friends.Count != 1)
            {
                plural = "s";
            }

            lblFriendCount.Text = String.Format(CultureInfo.InvariantCulture, Facebook.Properties.Resources.lblFriendCount, _friends.Count, plural);
        }

        void profile_ProfileItemSelected(Object sender, ProfileItemSelectedEventArgs e)
        {
            if (FriendSelected != null)
                FriendSelected(this, new FriendSelectedEventArgs(e.User));
        }

        private void FriendList_Load(object sender, EventArgs e)
        {
            OnLoad();
        }
    }
    public class FriendSelectedEventArgs : EventArgs
    {
        private User _user;

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        public FriendSelectedEventArgs(User user)
        {
            _user = user;
        }
    }
}

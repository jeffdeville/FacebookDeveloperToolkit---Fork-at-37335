using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using Facebook.Winforms.Properties;
using Facebook.Schema;

namespace Facebook.Winforms
{
	[ToolboxItem(true), ToolboxBitmap(typeof (BaseControl))]
	public partial class FriendList : BaseControl
	{
		private IList<user> _friends;

        /// <summary> 
        /// Constructor
        /// </summary>
        public FriendList()
		{
			InitializeComponent();
		}

        /// <summary> 
        /// Underlying list of friends
        /// </summary>
        [Category("Facebook")]
		[Description("")]
		public IList<user> Friends
		{
			get { return _friends; }
			set
			{
				_friends = value;
				OnLoad();
			}
		}

        /// <summary> 
        /// Event raised when a friend's profile is selected
        /// </summary>
        public event EventHandler<FriendSelectedEventArgs> FriendSelected;

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

			var width = flpFriends.Width;

			foreach (var friend in _friends)
			{
				var profile = new ProfileListItem(friend)
				              	{
				              		Width = (width - (flpFriends.Padding.Horizontal) - 20)
				              	};
				flpFriends.Controls.Add(profile);
				profile.ProfileItemSelected += profile_ProfileItemSelected;
			}

			var plural = string.Empty;

			if (_friends.Count != 1)
			{
				plural = "s";
			}

			lblFriendCount.Text = String.Format(CultureInfo.InvariantCulture, Resources.lblFriendCount, _friends.Count, plural);
		}

		private void profile_ProfileItemSelected(Object sender, ProfileItemSelectedEventArgs e)
		{
			if (FriendSelected != null)
				FriendSelected(this, new FriendSelectedEventArgs(e.User));
		}

		private void FriendList_Load(object sender, EventArgs e)
		{
			OnLoad();
		}
	}

    /// <summary> 
    /// Class to contain the relevant data for the friendselectedevent
    /// </summary>
    public class FriendSelectedEventArgs : EventArgs
	{
        /// <summary> 
        /// constructor
        /// </summary>
        public FriendSelectedEventArgs(user user)
		{
			User = user;
		}

        /// <summary> 
        /// underlying data
        /// </summary>
        public user User { get; set; }
	}
}
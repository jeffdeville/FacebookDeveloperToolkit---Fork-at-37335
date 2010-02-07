using System;
using System.Text;
using System.Windows.Forms;
using Facebook.Schema;

namespace Facebook.Winforms
{
    /// <summary> 
    /// Control that displays a user's profile
    /// </summary>
    public partial class ProfileListItem : BaseControl
	{
		private ProfileListItem()
		{
			InitializeComponent();
		}

        /// <summary> 
        /// Constructor
        /// </summary>
        public ProfileListItem(user user)
			: this()
		{
			User = user;
			LoadUser();
		}

        /// <summary> 
        /// the underlying object
        /// </summary>
        public user User { get; private set; }

        /// <summary> 
        /// Event raised when a profile is selected
        /// </summary>
        public event EventHandler<ProfileItemSelectedEventArgs> ProfileItemSelected;

		private void LoadUser()
		{
			if (User.pic != null)
			{
				pbProfilePicture.ImageLocation = User.pic;
			}
			else
			{
				pbProfilePicture.Image = User.picture;
			}
			lblName.Text = User.name;
			lblNetworks.Text = DisplayNetworks();
		}

		private string DisplayNetworks()
		{
			var networkList = new StringBuilder();

			foreach (var affiliation in User.affiliations.affiliation)
			{
				networkList.AppendLine(affiliation.name);
			}

			return networkList.ToString();
		}

		private void lblName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (ProfileItemSelected != null)
				ProfileItemSelected(this, new ProfileItemSelectedEventArgs(User));
		}
	}

    /// <summary> 
    /// Class used as part of the event when an profile item is selected from the list
    /// </summary>
    public class ProfileItemSelectedEventArgs : EventArgs
	{
        /// <summary> 
        /// Constructor
        /// </summary>
        public ProfileItemSelectedEventArgs(user user)
		{
			User = user;
		}

        /// <summary> 
        /// Underlying data
        /// </summary>
        public user User { get; set; }
	}
}
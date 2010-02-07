using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using Facebook.Winforms.Components;
using Facebook.Winforms.Properties;
using Facebook.Schema;

namespace Facebook.Winforms
{
	[ToolboxItem(true), ToolboxBitmap(typeof (BaseControl))]
	public partial class InviteeList : BaseControl
	{
		private Collection<event_members> eventMembers;

        /// <summary> 
        /// Constructor
        /// </summary>
        public InviteeList()
		{
			InitializeComponent();
		}

        /// <summary> 
        /// the underlying list of users invited to the event
        /// </summary>
        [Category("Facebook")]
		[Description("")]
		public Collection<event_members> EventMembers
		{
			get { return eventMembers; }
			set
			{
				eventMembers = value;
				OnLoad();
			}
		}

        /// <summary> 
        /// Instance of the component used to access the api
        /// </summary>
        [Category("Facebook"), Description("")]
		public FacebookService FacebookService { get; set; }

        /// <summary> 
        /// Underlying data
        /// </summary>
        [Category("Facebook"), Description("")]
		public facebookevent FacebookEvent { get; set; }

        /// <summary> 
        /// Event raised when an invited profile is selected
        /// </summary>
        public event EventHandler<InviteeSelectedEventArgs> InviteeSelected;

		private void OnLoad()
		{
			if (!IsDesignTime() && FacebookService != null && eventMembers != null)
			{
				LoadInvitees(GetDisplayMode());
			}
			else
			{
				lblInviteeCount.Visible = false;
				groupBox1.Visible = false;
			}
		}

		private void LoadInvitees(string rsvp_status)
		{
			lblInviteeCount.Visible = true;
			groupBox1.Visible = true;
			flpInvitees.Controls.Clear();

			var width = flpInvitees.Width;

			foreach (var type in eventMembers)
			{
				foreach (var attendee in type.attending.uid)
				{
					var profile = new ProfileListItem(FacebookService.Users.GetInfo(attendee))
					              	{
					              		Width = (width - (flpInvitees.Padding.Horizontal) - 20)
					              	};
					flpInvitees.Controls.Add(profile);
					profile.ProfileItemSelected += profile_ProfileItemSelected;
				}
			}

			var plural = string.Empty;

			if (eventMembers.Count != 1) plural = "s";

			lblInviteeCount.Text = String.Format(CultureInfo.InvariantCulture, Resources.lblInvitees, FacebookEvent.name,
												 eventMembers.Count, plural);
		}

		private void profile_ProfileItemSelected(object sender, ProfileItemSelectedEventArgs e)
		{
			if (InviteeSelected != null)
				InviteeSelected(this, new InviteeSelectedEventArgs(e.User));
		}

		private void InviteeList_Load(object sender, EventArgs e)
		{
			OnLoad();
		}

		private string GetDisplayMode()
		{
			if (rbAccepted.Checked)
			{
				return "attending";
			}
			if (rbDeclined.Checked)
			{
				return "declined";
			}
			return rbUnsure.Checked ? "unsure" : "not_replied";
		}

		private void rb_CheckedChanged(object sender, EventArgs e)
		{
			LoadInvitees(GetDisplayMode());
		}
	}

    /// <summary> 
    /// Class to contain the relevant data for the inviteeselectedevent
    /// </summary>
    public class InviteeSelectedEventArgs : EventArgs
	{
        /// <summary> 
        /// Constructor
        /// </summary>
        public InviteeSelectedEventArgs(user invitee)
		{
			Invitee = invitee;
		}

        /// <summary> 
        /// Underlying data
        /// </summary>
        public user Invitee { get; set; }
	}
}
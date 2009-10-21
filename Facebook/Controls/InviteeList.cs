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
    public partial class InviteeList : BaseControl
    {
        public event EventHandler<InviteeSelectedEventArgs> InviteeSelected;

        private Collection<EventUser> _invitees = null;
        private FacebookEvent _event = null;

        [Category("Facebook")]
        [Description("")]
        public Collection<EventUser> Invitees
        {
            get { return _invitees; }
            set 
            {
                _invitees = value;
                OnLoad();
            }
        }
        [Category("Facebook")]
        [Description("")]
        public FacebookEvent FacebookEvent
        {
            get { return _event; }
            set
            {
                _event = value;
            }
        }

        public InviteeList()
        {
            InitializeComponent();
        }

        private void OnLoad()
        {
            if (!IsDesignTime() && _invitees != null)
            {
                LoadInvitees(GetDisplayMode());
            }
            else
            {
                lblInviteeCount.Visible = false;
                groupBox1.Visible = false;
            }
        }

        private void LoadInvitees(RSVPStatus rsvpStatus)
        {
            lblInviteeCount.Visible = true;
            groupBox1.Visible = true;
            flpInvitees.Controls.Clear();

            int width = flpInvitees.Width;

            foreach (EventUser invitee in _invitees)
            {
                if (invitee.Attending == rsvpStatus)
                {
                    ProfileListItem profile = new ProfileListItem(invitee.User);
                    profile.Width = width - (flpInvitees.Padding.Horizontal) - 20;
                    flpInvitees.Controls.Add(profile);
                    profile.ProfileItemSelected += new EventHandler<ProfileItemSelectedEventArgs>(profile_ProfileItemSelected);
                }
            }

            string plural = string.Empty;

            if (_invitees.Count != 1)
            {
                plural = "s";
            }

            lblInviteeCount.Text = String.Format(CultureInfo.InvariantCulture, Facebook.Properties.Resources.lblInvitees, _event.Name, _invitees.Count, plural);
        }

        void profile_ProfileItemSelected(object sender, ProfileItemSelectedEventArgs e)
        {
            if (InviteeSelected != null)
                InviteeSelected(this, new InviteeSelectedEventArgs(e.User));
        }

        private void InviteeList_Load(object sender, EventArgs e)
        {
            OnLoad();
        }

        private RSVPStatus GetDisplayMode()
        {
            if (rbAccepted.Checked)
            {
                return RSVPStatus.Attending;
            }
            else if (rbDeclined.Checked)
            {
                return RSVPStatus.Declined;
            }
            else if (rbUnsure.Checked)
            {
                return RSVPStatus.Unsure;
            }
            else
            {
                return RSVPStatus.NoReply;
            }
        }
        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            LoadInvitees(GetDisplayMode());
        }
    }

    public class InviteeSelectedEventArgs : EventArgs
    {
        private User _invitee;

        public User Invitee
        {
            get { return _invitee; }
            set { _invitee = value; }
        }

        public InviteeSelectedEventArgs(User invitee)
        {
            _invitee = invitee;
        }
    }
}

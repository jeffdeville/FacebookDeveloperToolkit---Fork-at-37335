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
    public partial class EventListItem : BaseControl
    {
        public event EventHandler<EventItemSelectedEventArgs> EventItemSelected;

        private FacebookEvent _facebookEvent = null;

        public FacebookEvent Event
        {
            get { return _facebookEvent; }
        }

        private EventListItem()
        {
            InitializeComponent();
        }

        public EventListItem(FacebookEvent facebookEvent)
            : this()
        {
            _facebookEvent = facebookEvent;
            LoadEvent(facebookEvent);
        }

        private void LoadEvent(FacebookEvent facebookEvent)
        {
            pbEventPicture.Image = facebookEvent.Picture;
            lblEvent.Text = facebookEvent.Name;
            lblHostedBy.Text = facebookEvent.Host;
            lblType.Text = facebookEvent.Type;
            lblWhere.Text = facebookEvent.Location;
            lblWhen.Text = facebookEvent.StartDate.ToString();
        }

        private void lblEvent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (EventItemSelected != null)
                EventItemSelected(this, new EventItemSelectedEventArgs(_facebookEvent));

        }
    }
    public class EventItemSelectedEventArgs : EventArgs
    {
        private FacebookEvent _facebookEvent;

        public FacebookEvent FacebookEvent
        {
            get { return _facebookEvent; }
            set { _facebookEvent = value; }
        }

        public EventItemSelectedEventArgs(FacebookEvent facebookEvent)
        {
            _facebookEvent = facebookEvent;
        }
    }
}

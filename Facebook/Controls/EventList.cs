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
    public partial class EventList : BaseControl
    {
        public event EventHandler<EventSelectedEventArgs> EventSelected;

        private Collection<FacebookEvent> _events = null;

        [Category("Facebook")]
        [Description("")]
        public Collection<FacebookEvent> FacebookEvents
        {
            get { return _events; }
            set
            {
                _events = value;
                OnLoad();
            }
        }

        public EventList()
        {
            InitializeComponent();
        }

        private void OnLoad()
        {
            if (!IsDesignTime() && _events != null)
            {
                LoadEvents();
            }
        }

        private void LoadEvents()
        {
            flpEvents.Controls.Clear();

            int width = flpEvents.Width;

            foreach (FacebookEvent facebookEvent in _events)
            {
                EventListItem eventListItem = new EventListItem(facebookEvent);
                eventListItem.Width = width - flpEvents.Padding.Horizontal - 20;
                flpEvents.Controls.Add(eventListItem);
                eventListItem.EventItemSelected += new EventHandler<EventItemSelectedEventArgs>(eventListItem_EventItemSelectedEventHandler);
            }

            string plural = string.Empty;

            if (_events.Count != 1)
            {
                plural = "s";
            }

            lblEventCount.Text = String.Format(CultureInfo.InvariantCulture, Facebook.Properties.Resources.lblEventCount, _events.Count, plural);
        }
        void eventListItem_EventItemSelectedEventHandler(object sender, EventItemSelectedEventArgs e)
        {
            if (e.FacebookEvent != null)
                EventSelected(this, new EventSelectedEventArgs(e.FacebookEvent));
        }

        private void EventList_Load(object sender, EventArgs e)
        {
            OnLoad();
        }
    }
    public class EventSelectedEventArgs : EventArgs
    {
        private FacebookEvent _facebookEvent;

        public FacebookEvent FacebookEvent
        {
            get { return _facebookEvent; }
            set { _facebookEvent = value; }
        }

        public EventSelectedEventArgs(FacebookEvent facebookEvent)
        {
            _facebookEvent = facebookEvent;
        }
    }
}

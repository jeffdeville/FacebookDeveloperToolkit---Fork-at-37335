using System;
using System.Windows.Forms;
using Facebook.Schema;


namespace Facebook.Winforms
{
    /// <summary> 
    /// Control that displays an event
    /// </summary>
    public partial class EventListItem : BaseControl
	{
		private readonly facebookevent _facebookEvent;

		private EventListItem()
		{
			InitializeComponent();
		}

        /// <summary> 
        /// Constructor
        /// </summary>
        public EventListItem(facebookevent facebookEvent)
			: this()
		{
			_facebookEvent = facebookEvent;
			LoadEvent(facebookEvent);
		}

        /// <summary> 
        /// the underlying object
        /// </summary>
        public facebookevent Event
		{
			get { return _facebookEvent; }
		}

        /// <summary> 
        /// Event raised when a event is selected
        /// </summary>
        public event EventHandler<EventItemSelectedEventArgs> EventItemSelected;

		private void LoadEvent(facebookevent facebookEvent)
		{
			pbEventPicture.Image = facebookEvent.picture;
			lblEvent.Text = facebookEvent.name;
			lblHostedBy.Text = facebookEvent.host;
			lblType.Text = facebookEvent.event_type;
			lblWhere.Text = facebookEvent.location;
			lblWhen.Text = facebookEvent.start_date.ToString();
		}

		private void lblEvent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (EventItemSelected != null)
				EventItemSelected(this, new EventItemSelectedEventArgs(_facebookEvent));
		}
	}
    /// <summary> 
    /// Class used as part of the event when an event item is selected from the list
    /// </summary>
	public class EventItemSelectedEventArgs : EventArgs
	{
        /// <summary> 
        /// Constructor
        /// </summary>
        public EventItemSelectedEventArgs(facebookevent facebookEvent)
		{
			FacebookEvent = facebookEvent;
		}

        /// <summary> 
        /// the event data
        /// </summary>
        public facebookevent FacebookEvent { get; set; }
	}
}
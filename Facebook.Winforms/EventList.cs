using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using Facebook.Winforms.Properties;
using Facebook.Schema;

namespace Facebook.Winforms
{
    /// <summary> 
    /// Control that displays a list of events
    /// </summary>
    [ToolboxItem(true), ToolboxBitmap(typeof(BaseControl))]
	public partial class EventList : BaseControl
	{
		private Collection<facebookevent> _events;

        /// <summary> 
        /// Constructor
        /// </summary>
        public EventList()
		{
			InitializeComponent();
		}

        /// <summary> 
        /// Underlyng collection of events
        /// </summary>
        [Category("Facebook")]
		[Description("")]
		public Collection<facebookevent> FacebookEvents
		{
			get { return _events; }
			set
			{
				_events = value;
				OnLoad();
			}
		}

        /// <summary> 
        /// Event raised when a event is selected
        /// </summary>
        public event EventHandler<EventSelectedEventArgs> EventSelected;

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

			var width = flpEvents.Width;

			foreach (var facebookEvent in _events)
			{
				var eventListItem = new EventListItem(facebookEvent)
				                    	{
				                    		Width = (width - flpEvents.Padding.Horizontal - 20)
				                    	};
				flpEvents.Controls.Add(eventListItem);
				eventListItem.EventItemSelected += eventListItem_EventItemSelectedEventHandler;
			}

			var plural = string.Empty;

			if (_events.Count != 1)
			{
				plural = "s";
			}

			lblEventCount.Text = String.Format(CultureInfo.InvariantCulture, Resources.lblEventCount, _events.Count, plural);
		}

		private void eventListItem_EventItemSelectedEventHandler(object sender, EventItemSelectedEventArgs e)
		{
			if (!Equals(e.FacebookEvent,null))
				EventSelected(this, new EventSelectedEventArgs(e.FacebookEvent));
		}

		private void EventList_Load(object sender, EventArgs e)
		{
			OnLoad();
		}
	}

    /// <summary> 
    /// Class to contain the relevant data for the eventselectedevent
    /// </summary>
    public class EventSelectedEventArgs : EventArgs
	{
        /// <summary> 
        /// Constructor
        /// </summary>
        public EventSelectedEventArgs(facebookevent facebookEvent)
		{
			FacebookEvent = facebookEvent;
		}

        /// <summary> 
        /// Underlying data
        /// </summary>
        public facebookevent FacebookEvent { get; set; }
	}
}
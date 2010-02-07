using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook.Schema;
using Facebook.Utility;

namespace Facebook.Web
{
	/// <summary>
	/// FriendList web control to show a list of friends for a given user
	/// </summary>
	[ToolboxData("<{0}:FriendList runat=server></{0}:FriendList>")]
	public class FriendList : WebControl, IPostBackEventHandler
	{
		#region Delegates

        /// <summary> 
        /// function to callback when a friend is selected from the list
        /// </summary>
        public delegate void FriendListItemClickedEventHandler(object sender, FriendListItemClickEventArgs e);

		#endregion

		private const string FRIENDNAME_LABEL_DIV = "friendNameLabel";
		private const string HEADER_DIV = "friendListHeader";
		private const string INNER_DIV = "friendListItem";
		private const string OUTER_DIV = "friendList";

		private const string VIEWSTATE_FRIENDLIST = "friendList";
        private IList<user> _friends;
		private bool _useViewState = true;

		//event to throw when the user is clicked.

		/// <summary>
		/// A flag indicating whether the control should store its state in ViewState.  By default this is
		/// set to True.  If this is set to False, the host page needs to take care of keeping track of state
		/// and setting the UserProfile control's User Property with each Postback. 
		/// </summary>
		public bool UseViewState
		{
			get { return _useViewState; }
			set { _useViewState = value; }
		}

		/// <summary>
		/// The collection of friends that we'll show
		/// </summary>
		public IList<user> Friends
		{
			get { return _friends; }
			set
			{
				_friends = value;
				if (UseViewState)
				{
					ViewState[VIEWSTATE_FRIENDLIST] = _friends.Select(friend => Utilities.SerializeToXml<user>(friend)).ToList();
				}
			}
		}

		#region IPostBackEventHandler Members

		/// <summary>
		/// Called when a Postback occurs.
		/// </summary>
		/// <param name="eventArgument">The userID of the friend that was clicked.</param>
		public void RaisePostBackEvent(string eventArgument)
		{
			OnClick(new FriendListItemClickEventArgs(eventArgument));
		}

		#endregion

        /// <summary> 
        /// event raised when a friend is clicked
        /// </summary>
        public event FriendListItemClickedEventHandler FriendClick;

        /// <summary> 
        /// Callback
        /// </summary>
        protected virtual void OnClick(FriendListItemClickEventArgs e)
		{
			if (FriendClick != null)
			{
				FriendClick(this, e);
			}
		}

		/// <summary>
		/// Called each time the control is loaded.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			LoadFromViewState();
		}

		/// <summary>
		/// Loads the Facebook FriendList from ViewState.  This is used to pull back data during a Postback operation.
		/// </summary>
		private void LoadFromViewState()
		{
			object friends = ViewState[VIEWSTATE_FRIENDLIST];
			if (!Equals(friends, null))
			{
				_friends = ((IEnumerable<string>)friends).Select(friendXML => Utilities.DeserializeXML<user>(friendXML)).ToList<user>();
			}
		}

		/// <summary>
		/// RenderContents is called by the ASP.Net framework when our control needs to render itself.  Here we
		/// will simply output our resulting HTML.
		/// </summary>
		/// <param name="output">The writer to which we will write our HTML</param>
		protected override void RenderContents(HtmlTextWriter output)
		{
			if (!Equals(_friends, null))
			{
				//Write the outer DIV tag so the page author that uses this control can 
				//specify styles based on this DIV.
				output.AddAttribute(HtmlTextWriterAttribute.Id, OUTER_DIV);
				output.RenderBeginTag(HtmlTextWriterTag.Div);

				output.AddAttribute(HtmlTextWriterAttribute.Id, HEADER_DIV);
				output.RenderBeginTag(HtmlTextWriterTag.Div);
				output.Write("Friends List");
				output.RenderEndTag(); //Header div


				if (_friends.Count == 0)
				{
					output.Write("You have no friends.");
				}
				else if (_friends.Count == 1)
				{
					output.Write("You have a friend!");
				}
				else
				{
					output.Write("You have " + _friends.Count + " friends.");
				}

				//Loop through the list of friends, write them out
				foreach (user friend in _friends)
				{
					//Write the inner DIV tag.  This enables finer control of any styles.
					output.AddAttribute(HtmlTextWriterAttribute.Id, INNER_DIV);
					output.RenderBeginTag(HtmlTextWriterTag.Div);

					//Write out the image.
					if (!Equals(friend.pic, null))
					{
						output.AddAttribute(HtmlTextWriterAttribute.Src, friend.pic.ToString());
						output.RenderBeginTag(HtmlTextWriterTag.Img);
						output.RenderEndTag();
					}

					//Write out the friend name
					output.AddAttribute(HtmlTextWriterAttribute.Id, FRIENDNAME_LABEL_DIV);
					output.RenderBeginTag(HtmlTextWriterTag.Div);

					//Add the javascript for a postback event which will pass the User's userID as a parameter.  This
					//is used to raise the OnClick event, which let's the page author know that the user clicked on a
					//friend in the list.
					output.AddAttribute(HtmlTextWriterAttribute.Href, Page.ClientScript.GetPostBackClientHyperlink(this, friend.uid.ToString()));
					output.RenderBeginTag(HtmlTextWriterTag.A);

					output.Write(friend.name);
					output.Write(" (");
                    //TODO: Make sure that State Returns the state abreviation, not the entire state name
                    output.Write(friend.hometown_location.city + ", " + friend.hometown_location.state);
					output.Write(")");
					output.RenderEndTag(); //A
					output.RenderEndTag(); //DIV
					output.RenderEndTag(); //DIV
				}
				output.RenderEndTag();
			}
		}
	}

	/// <summary>
	/// Custom event args class used when we throw our OnClick event.
	/// </summary>
	public class FriendListItemClickEventArgs : EventArgs
	{
		private readonly string _friendId;

        /// <summary> 
        /// Constructor
        /// </summary>
        public FriendListItemClickEventArgs(string id)
		{
			_friendId = id;
		}

        /// <summary> 
        /// Underlying Data
        /// </summary>
        public string FriendId
		{
			get { return _friendId; }
		}
	}
}
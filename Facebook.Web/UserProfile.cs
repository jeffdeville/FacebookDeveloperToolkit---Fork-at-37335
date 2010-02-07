using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Facebook.Schema;
using Facebook.Utility;


namespace Facebook.Web
{
	/// <summary>
	/// UserProfile web control to show the details of a user
	/// </summary>
	[ToolboxData("<{0}:UserProfile runat=server></{0}:UserProfile>")]
	public class UserProfile : WebControl
	{
		private const string DATA_CLASS = "userProfileData";
		private const string HEADER_DIV = "userProfileHeader";
		private const string LABEL_CLASS = "userProfileLabel";
		private const string OUTER_DIV = "userProfile";
		private user _user;
		private bool _useViewState = true;


		/// <summary>
		/// The Facebook User that will be shown on the control.
		/// </summary>
		public user User
		{
			get { return _user; }
			set
			{
				_user = value;
				if (UseViewState)
				{
                    ViewState["user"] = Utilities.SerializeToXml<user>(_user);
				}
			}
		}

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
		/// Called each time the control is loaded.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			LoadFromViewState();
		}

		/// <summary>
		/// Loads the Facebook User from ViewState.  This is used to pull back data during a Postback operation.
		/// </summary>
		private void LoadFromViewState()
		{
			if (!UseViewState) return;
            var userXML = (string)ViewState["user"];
			if (!Equals(userXML, null))
			{
				_user = Utilities.DeserializeXML<user>(userXML);
			}
		}

		/// <summary>
		/// RenderContents is called by the ASP.Net framework when our control needs to render itself.  Here we
		/// will simply output our resulting HTML.
		/// </summary>
		/// <param name="output">The writer to which we will write our HTML</param>
		protected override void RenderContents(HtmlTextWriter output)
		{
			if (!Equals(_user, null))
			{
				//Write the outer DIV tag so the page author that uses this control can 
				//specify styles based on this DIV.
				output.AddAttribute(HtmlTextWriterAttribute.Id, OUTER_DIV);
				output.RenderBeginTag(HtmlTextWriterTag.Div);

				output.AddAttribute(HtmlTextWriterAttribute.Id, HEADER_DIV);
				output.RenderBeginTag(HtmlTextWriterTag.Div);
				output.Write("User Profile");
				output.RenderEndTag(); //Header div


				string age = "Unknown";
				if (!Equals(_user, null) && !(_user.birthday_date == new DateTime(1900, 1, 1)))
				{
					age = (DateTime.Now.Year - _user.birthday_date.Year).ToString();
				}

				if (!Equals(_user.pic, null))
				{
                    output.AddAttribute(HtmlTextWriterAttribute.Src, _user.pic.ToString());
					output.RenderBeginTag(HtmlTextWriterTag.Img);
					output.RenderEndTag();
				}

				//Write each name/value pair.  We could show more Facebook User properties here if we choose.
				WriteUserData(output, "Name", _user.name);
				WriteUserData(output, "Age", age);
				WriteUserData(output, "Sex", _user.sex.ToString());
				WriteUserData(output, "Hometown", _user.hometown_location.city);

				output.RenderEndTag(); //Outer Div
			}
		}

		/// <summary>
		/// Write a section of name/value pairs in HTML 
		/// </summary>
		/// <param name="output">The writer to write the HTML to.</param>
		/// <param name="labelText">The label to write</param>
		/// <param name="dataText">The data to write</param>
		private static void WriteUserData(HtmlTextWriter output, string labelText, string dataText)
		{
			output.RenderBeginTag(HtmlTextWriterTag.Div);

			//Data Label
			output.AddAttribute(HtmlTextWriterAttribute.Class, LABEL_CLASS);
			output.RenderBeginTag(HtmlTextWriterTag.Span);
			output.Write(labelText + " ");
			output.RenderEndTag();

			//Data value
			output.AddAttribute(HtmlTextWriterAttribute.Class, DATA_CLASS);
			output.RenderBeginTag(HtmlTextWriterTag.Span);
			output.Write(dataText);
			output.RenderEndTag();

			output.RenderEndTag(); //</Div>
		}
	}
}
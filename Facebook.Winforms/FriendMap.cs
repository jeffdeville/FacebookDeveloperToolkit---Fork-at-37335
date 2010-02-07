using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Facebook.Winforms.Properties;
using Facebook.Schema;

namespace Facebook.Winforms
{
    /// <summary> 
    /// Control that displays a list of friends on a map
    /// </summary>
    [ToolboxItem(true), ToolboxBitmap(typeof(BaseControl))]
	public partial class FriendMap : BaseControl
	{
		private Collection<user> _users;

        /// <summary> 
        /// Constructor
        /// </summary>
        public FriendMap()
		{
			InitializeComponent();
		}

        /// <summary> 
        /// The underlying collection
        /// </summary>
        [Category("Facebook")]
		[Description("")]
		public Collection<user> Friends
		{
			get { return _users; }
			set
			{
				_users = value;
				OnLoad();
			}
		}

		private void OnLoad()
		{
			if (!IsDesignTime() && _users != null)
			{
				LoadLocations();
			}
		}

		private void LoadLocations()
		{
			var addresses = new StringBuilder();
			var labels = new StringBuilder();

			foreach (var user in _users)
			{
				addresses.Append("'");
				addresses.Append(user.current_location.city);
				addresses.Append(", ");
				addresses.Append(user.current_location.state);
				addresses.Append(", ");
				addresses.Append(user.current_location.country);
				addresses.Append("',");

				labels.Append("'");
				labels.Append(user.name);
				labels.Append("',");
			}

			var addressList = addresses.ToString();
			var labelList = labels.ToString();

			if (addressList.EndsWith(","))
				addressList = addressList.Remove(addressList.Length - 1);

			if (labelList.EndsWith(","))
				labelList = labelList.Remove(labelList.Length - 1);

			var html = Resources.MapPage;
			html = html.Replace("{0}", addressList);
			html = html.Replace("{1}", labelList);
			virtualEarthBrowser.DocumentText = html;
		}

		private void FriendMap_Load(object sender, EventArgs e)
		{
			OnLoad();
		}
	}
}
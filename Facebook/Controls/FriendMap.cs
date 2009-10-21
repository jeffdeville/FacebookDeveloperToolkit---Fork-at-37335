using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Facebook.Controls
{
    public partial class FriendMap : BaseControl
    {
        private Collection<User> _users = null;

        [Category("Facebook")]
        [Description("")]
        public Collection<User> Friends
        {
            get { return _users; }
            set 
            { 
                _users = value;
                OnLoad();
            }
        }

        public FriendMap()
        {
            InitializeComponent();
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
            StringBuilder addresses = new StringBuilder();
            StringBuilder labels = new StringBuilder();
            
            foreach (User user in _users)
            {
                if (!String.IsNullOrEmpty(user.CurrentLocation.City))
                {
                    addresses.Append("'");
                    addresses.Append(user.CurrentLocation.City);
                    if (user.CurrentLocation.StateAbbreviation != StateAbbreviation.Unknown)
                    {
                        addresses.Append(", ");
                        addresses.Append(user.CurrentLocation.StateAbbreviation.ToString());
                    }
                    addresses.Append("',");

                    labels.Append("'");
                    labels.Append(user.Name);
                    labels.Append("',");
                }
            }

            string addressList = addresses.ToString();
            string labelList = labels.ToString();

            if (addressList.EndsWith(","))
                addressList = addressList.Remove(addressList.Length - 1);

            if (labelList.EndsWith(","))
                labelList = labelList.Remove(labelList.Length - 1);

            string html = Facebook.Properties.Resources.MapPage;
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

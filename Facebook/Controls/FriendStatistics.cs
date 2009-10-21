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
    public partial class FriendStatistics : BaseControl
    {
        private Collection<User> _friends = null;

        [Category("Facebook")]
        [Description("")]
        public Collection<User> Friends
        {
            get { return _friends; }
            set
            {
                _friends = value;
                OnLoad();
            }
        }

        public FriendStatistics()
        {
            InitializeComponent();
        }

        private void OnLoad()
        {
            if (!IsDesignTime() && _friends != null)
            {
                LoadStats();
            }
        }

        private void LoadStats()
        {
            LoadSexStats();
        }

        private void LoadSexStats()
        {
            int male = 0;
            int female = 0;

            foreach (User friend in _friends)
            {
                switch (friend.Sex)
                {
                    case Gender.Female:
                        female++;
                        break;

                    case Gender.Male:
                        male++;
                        break;
                }
            }

            lblMale.Text = (((double)male / (double)_friends.Count)).ToString("p0", CultureInfo.InvariantCulture);
            lblFemale.Text = (((double)female / (double)_friends.Count)).ToString("p0", CultureInfo.InvariantCulture);
        }
    }
}

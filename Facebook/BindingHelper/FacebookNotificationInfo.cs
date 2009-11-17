using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;
using System.Windows;
using Facebook.Utility;
using Facebook.Schema;

namespace Facebook.BindingHelper
{
    /// <summary>
    /// Defines a facebook stream comment 
    /// </summary>
    [DataContract]
    public sealed class FacebookNotificationInfo : notifications, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes FacebookNotificationInfo object
        /// </summary>
        internal FacebookNotificationInfo()
        {
        }

        /// <summary>
        /// Initializes FacebookNotificationInfo object
        /// </summary>
        internal FacebookNotificationInfo(notifications info)
        {
            this.messages = info.messages;
            this.pokes = info.pokes;
            this.shares = info.shares;
            this.friend_requests = info.friend_requests;
            this.group_invites = info.group_invites;
            this.event_invites = info.event_invites;
            this.messages = info.messages;
        }

        FacebookProfileCollection _profiles;

        /// <summary>
        /// Profiles of all friend requests
        /// </summary>
        public FacebookProfileCollection FriendRequestsProfiles
        {
            get
            {
                if (_profiles == null)
                {
                    if (friend_requests != null && friend_requests.uid.Count != 0)
                    {
                        _profiles = BindingManager.Instance.GetProfiles(friend_requests.uid.ToArray());
                    }
                }
                return _profiles;
            }
        }

        List<facebookevent> _events;

        /// <summary>
        /// Event info of all events requests
        /// </summary>
        public List<facebookevent> EventRequests
        {
            get
            {
                if (_events == null)
                {
                    if (event_invites != null && event_invites.eid.Count != 0)
                    {
                        BindingManager.Instance.Api.Events.GetAsync(null, event_invites.eid, null, null, null, OnGetEvents, null);
                    }
                }
                return _events;
            }
        }

        List<group> _groups;

        /// <summary>
        /// Event info of all events requests
        /// </summary>
        public IList<group> GroupRequests
        {
            get
            {
                if (_groups == null)
                {
                    if (group_invites != null && group_invites.gid.Count != 0)
                    {
                        BindingManager.Instance.Api.Groups.GetAsync(group_invites.gid, OnGetGroups, null);
                    }
                }
                return _groups;
            }
        }
        /// <summary>
        /// Messages Info
        /// </summary>
        [DataMember(Name = "messages")]
        public notification_count Messages
        {
            get { return this.messages; }
            set { this.messages = value; }
        }

        /// <summary>
        /// Pokes Info
        /// </summary>
        [DataMember(Name = "pokes")]
        public notification_count Pokes
        {
            get { return this.pokes; }
            set { this.pokes = value; }
        }

        /// <summary>
        /// Shares Info
        /// </summary>
        [DataMember(Name = "shares")]
        public notification_count Shares
        {
            get { return this.shares; }
            set { this.shares = value; }
        }

        /// <summary>
        /// List of friend requests
        /// </summary>
        [DataMember(Name = "friend_requests")]
        public List<long> FriendRequests
        {
            get { return this.friend_requests.uid; }
            set { this.friend_requests.uid = value; }
        }

        /// <summary>
        /// List of group invies
        /// </summary>
        [DataMember(Name = "group_invites")]
        public List<long> GroupInvites
        {
            get { return this.group_invites.gid; }
            set {this.group_invites.gid = value;}
        }

        /// <summary>
        /// List of event invites
        /// </summary>
        [DataMember(Name = "event_invites")]
        public List<long> EventInvites
        {
            get {return event_invites.eid;}
            set {this.event_invites.eid = value;}
        }


        #region INotifyPropertyChanged Members

        /// <summary>
        /// Property change event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        void OnGetEvents(IList<facebookevent> events, Object state, FacebookException e)
        {
            if (e == null && events != null)
            {
                _events = new List<facebookevent>();
                _events.AddRange(events);
                this.NotifyPropertyChanged(PropertyChanged, o => o.EventRequests);
            }
        }

        void OnGetGroups(IList<group> groups, Object state, FacebookException e)
        {
            if (e == null && groups != null)
            {
                _groups = new List<group>();
                _groups.AddRange(groups);
                this.NotifyPropertyChanged(PropertyChanged, o => o.GroupRequests);
            }
        }

    }
}

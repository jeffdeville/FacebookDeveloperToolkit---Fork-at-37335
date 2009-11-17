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
    public sealed class ActivityComment : IEquatable<ActivityComment>
    {
        /// <summary>
        /// Initializes ActivityComment object
        /// </summary>
        internal ActivityComment()
        {
        }
        
        /// <summary>
        /// Initializes ActivityComment object from underlying facebook data object
        /// </summary>
        /// <param name="comment">comment object</param>
        internal ActivityComment(FacebookComment comment)
        {
            this.CommentId = comment.CommentId;
            this.FromUserId = comment.FromId;
            this.Time = comment.Time;
            this.Text = comment.Text;
        }
        /// <summary>
        /// Initializes ActivityComment object from underlying facebook data object
        /// </summary>
        /// <param name="comment">comment object</param>
        internal ActivityComment(comment comment)
        {
            CommentId = comment.id.ToString();
            FromUserId = comment.fromid;
            Time = DateHelper.ConvertDoubleToDate(comment.time);
            Text = comment.text;

        }

        /// <summary>
        /// Id of User who posted this comment
        /// </summary>
        [DataMember]
        public long FromUserId { get; internal set; }
        /// <summary>
        /// Time the comment was posted
        /// </summary>
        [DataMember]
        public DateTime Time { get; internal set; }
        /// <summary>
        /// Comment text
        /// </summary>
        [DataMember]
        public string Text { get; internal set; }

        /// <summary>
        /// Id of comment
        /// </summary>
        [DataMember]
        public string CommentId { get; internal set; }

        /// <summary>
        /// Profile object of user who posted this comment
        /// </summary>
        public FacebookProfile FromUser
        {
            get
            {
                FacebookProfile profile = BindingManager.Instance.GetProfileFromCache(this.FromUserId);
                if (profile == null)
                {
                    BindingManager.Instance.GetProfiles(new long[] { this.FromUserId });
                }
                return profile;
            }
        }

        /// <summary>
        /// Indicates if this comment was posted by currently logged in user
        /// </summary>
        public bool IsMine
        {
            get
            {
                return FromUserId == BindingManager.Instance.CurrentUserId;
            }
        }


        #region IEquatable<ActivityComment> Members

        /// <summary>
        /// Overridden operator used to compare to ActivityComments
        /// </summary>
        public bool Equals(ActivityComment other)
        {
            return (other != null && this.CommentId == other.CommentId);
        }

        #endregion
    }
}

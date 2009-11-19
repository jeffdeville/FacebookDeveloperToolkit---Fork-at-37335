using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Collections.Specialized;
using Facebook.Schema;
using Facebook.Utility;
using System.Collections.Generic;

namespace Facebook.BindingHelper
{
    /// <summary>
    /// Defines a stream post object
    /// </summary>
    [DataContract]
    public class ActivityPost : INotifyPropertyChanged, IEquatable<ActivityPost>
    {
        const int DefaultCommentCount = 4;
        private const string _StreamTableColumns = "updated_time, comments, likes";
        private const string _GetStreamByIdQueryString = "SELECT " + _StreamTableColumns + " FROM stream WHERE post_id = '{0}'";

        /// <summary>
        /// Initializes a ActivityPost object
        /// </summary>
        /// <param name="post"></param>
        internal ActivityPost(FacebookStreamPost post)
        {
            CreatedTime = post.CreatedTime;
            UpdatedTime = post.UpdatedTime;
            Message = post.Message;
            ActorUserId = post.ActorId;
            TargetUserId = !string.IsNullOrEmpty(post.TargetId) ? long.Parse(post.TargetId) : new Nullable<long>();
            CanLike = post.Likes.CanLikes;
            HasLiked = post.Likes.UserLikes;
            Likes = new ActivityPostLikes(post.Likes);
            CanComment = post.StreamComments != null ? post.StreamComments.CanPost : false;
            CanRemoveComments = post.StreamComments != null ? post.StreamComments.CanRemove : false;
            CommentCount = post.StreamComments != null ? post.StreamComments.Count : 0;
            PostId = post.PostId;
            Type = post.Type;
            Attribution = post.Attribution;
            AppId = post.AppId;
            Attachment = new ActivityPostAttachment(post.Attachment);
            if (post.StreamComments != null)
            {
                _comments = new ActivityCommentCollection(post.StreamComments.Comments);
            }
            Likes.ProfileInfoChangeEvent += new EventHandler<EventArgs>(Likes_ProfileInfoChangeEvent);
            //FilterKey = post.FilterKey;
            //Permalink = post.Permalink;

        }

        /// <summary>
        /// Initializes a ActivityPost object
        /// </summary>
        /// <param name="post"></param>
        internal ActivityPost(stream_post post)
        {
            CreatedTime = DateHelper.ConvertDoubleToDate(post.created_time);  // TODO: Test conversion?
            UpdatedTime = DateHelper.ConvertDoubleToDate(post.updated_time);  // TODO: Test conversion?
            Message = post.message;
            ActorUserId = post.actor_id;
            TargetUserId = post.target_id;
            CanLike = post.likes.user_likes;
            HasLiked = post.likes.user_likes;
            Likes = new ActivityPostLikes(post.likes);
            CanComment = post.comments.can_post;
            CanRemoveComments = post.comments.can_remove;
            CommentCount = post.comments.count;
            PostId = post.post_id;
            Type = post.type;
            Attribution = post.attribution;
            AppId = post.app_id;
            Attachment = new ActivityPostAttachment(post.attachment);
            _comments = new ActivityCommentCollection(post.comments.comment_list.comment);
            Likes.ProfileInfoChangeEvent += new EventHandler<EventArgs>(Likes_ProfileInfoChangeEvent);
            //FilterKey = post.filterkey;
            //Permalink = post.permalink;
        }


        /// <summary>
        /// Time this post was created
        /// </summary>
        [DataMember]
        public DateTime CreatedTime { get; internal set; }
        /// <summary>
        /// Time post was last upated
        /// </summary>
        [DataMember]
        public DateTime UpdatedTime { get; internal set; }

        /// <summary>
        /// Post message
        /// </summary>
        [DataMember]
        public string Message { get; internal set; }

        /// <summary>
        /// Id of user who posted this message
        /// </summary>
        [DataMember]
        public long ActorUserId { get; internal set; }

        /// <summary>
        /// Id of target user or page
        /// </summary>
        [DataMember]
        public long? TargetUserId { get; internal set; }

        /// <summary>
        /// Identifies if currently logged in user can like this post
        /// </summary>
        [DataMember]
        public bool CanLike { get; internal set; }

        /// <summary>
        /// Identifies if currently logged in user has liked this post
        /// </summary>
        [DataMember]
        public bool HasLiked { get; internal set; }

        /// <summary>
        /// People who have liked this post
        /// </summary>
        [DataMember]
        public ActivityPostLikes Likes
        {
            get; internal set;
        }

        /// <summary>
        /// Number of people who have liked this post
        /// </summary>
        public int LikedCount
        {
            get { return Likes.Count; }
        }

        /// <summary>
        /// Identifies if currently logged in user can comment on this post
        /// </summary>
        [DataMember]
        public bool CanComment { get; internal set; }

        /// <summary>
        /// Identifies if currently logged in user can remove this post
        /// </summary>
        [DataMember]
        public bool CanRemoveComments { get; internal set; }

        /// <summary>
        /// Number of comments for this post
        /// </summary>
        [DataMember]
        public int CommentCount { get; internal set; }

        /// <summary>
        /// Filter key of this post
        /// </summary>
        [DataMember]
        public string FilterKey { get; internal set; }

        /// <summary>
        /// Id of this post
        /// </summary>
        [DataMember]
        public string PostId { get; internal set; }

        /// <summary>
        /// Type of this post
        /// </summary>
        [DataMember]
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Attribution of this post
        /// </summary>
        [DataMember]
        public string Attribution
        {
            get;
            set;
        }

        /// <summary>
        /// Permalink for the post
        /// </summary>
        [DataMember(Name = "permalink")]
        public string Permalink
        {
            get;
            set;
        }

        /// <summary>
        /// AppId of this post
        /// </summary>
        [DataMember]
        public long? AppId
        {
            get;
            set;
        }

        /// <summary>
        /// Attachment information
        /// </summary>
        [DataMember]
        public Facebook.BindingHelper.ActivityPostAttachment Attachment
        {
            get;
            set;
        }

        /// <summary>
        /// Profile object of user who posted this post
        /// </summary>
        public FacebookProfile Actor
        {
            get
            {
                return  BindingManager.Instance.GetProfileFromCache(this.ActorUserId);
            }
        }

        ActivityCommentCollection _comments;
        /// <summary>
        /// List of comments for this post
        /// </summary>
        [DataMember]
        public ActivityCommentCollection Comments
        {
            get
            {
                return _comments;
            }
            internal set
            {
                _comments = value;
                this.NotifyPropertyChanged( PropertyChanged,o=>o.Comments);
            }
        }

        #region Public methods

        /// <summary>
        /// Gets most recent comments for this post, in ascending order
        /// </summary>
        /// <param name="limit">maximum number of comments to retrieve</param>
        public void GetComments(int limit)
        {
            BindingManager.Instance.GetComments(PostId, limit, null, OnGetCommentsCompleted, false);
        }


        /// <summary>
        /// Adds a comment to steram post
        /// </summary>
        /// <param name="text"></param>
        public void AddComment(string text)
        {
            if (CanComment)
            {
                BindingManager.Instance.Api.Stream.AddCommentAsync(PostId, text, OnStreamAddComment, null);
            }
        }

        /// <summary>
        /// Removes a comment from stream post
        /// </summary>
        /// <param name="comment"></param>
        public void RemoveComment(ActivityComment comment)
        {
            if (CanRemoveComments || comment.FromUserId == BindingManager.Instance.CurrentUser.UserId)
            {
                BindingManager.Instance.Api.Stream.RemoveCommentAsync(comment.CommentId, OnStreamRemoveItem, comment);
            }
        }

        /// <summary>
        /// Adds like to this post
        /// </summary>
        public void AddLike()
        {
            if (CanLike)
            {
                BindingManager.Instance.Api.Stream.AddLikeAsync(PostId, OnStreamModifiedLike, null);
            }
        }

        /// <summary>
        /// Removes a like 
        /// </summary>
        public void RemoveLike()
        {
            if (CanLike)
            {
                BindingManager.Instance.Api.Stream.RemoveLikeAsync(PostId, OnStreamModifiedLike, null);
            }
        }

        /// <summary>
        /// Syncs 2 post, raising property change event if necessary
        /// </summary>
        /// <param name="newPost"></param>
        public void SyncWithPost(ActivityPost newPost)
        {
            if (UpdatedTime != newPost.UpdatedTime)
            {
                UpdatedTime = newPost.UpdatedTime;
                this.NotifyPropertyChanged(PropertyChanged, o => o.UpdatedTime);
            }

            if (string.Compare(Message, newPost.Message, StringComparison.OrdinalIgnoreCase) != 0)
            {
                Message = newPost.Message;
                this.NotifyPropertyChanged(PropertyChanged,o=>o.Message);
            }

            if (CanLike != newPost.CanLike)
            {
                CanLike = newPost.CanLike;
                this.NotifyPropertyChanged( PropertyChanged,o=>o.CanLike);
            }

            if (HasLiked != newPost.HasLiked)
            {
                HasLiked = newPost.HasLiked;
                this.NotifyPropertyChanged( PropertyChanged,o=>o.HasLiked);
            }

            if (!Likes.Equals(newPost.Likes))
            {
                Likes = newPost.Likes;
                this.NotifyPropertyChanged( PropertyChanged,o=>o.Likes);
            }

            if (CanComment != newPost.CanComment)
            {
                CanComment = newPost.CanComment;
                this.NotifyPropertyChanged( PropertyChanged,o=>o.CanComment);
            }

            if (CanRemoveComments != newPost.CanRemoveComments)
            {
                CanRemoveComments = newPost.CanRemoveComments;
                this.NotifyPropertyChanged( PropertyChanged,o=>o.CanRemoveComments);
            }

            if (CommentCount != newPost.CommentCount)
            {
                CommentCount = newPost.CommentCount;
                Comments = newPost.Comments;
                this.NotifyPropertyChanged( PropertyChanged,o=>o.CommentCount);
            }
            else
            {
                if (Comments.Count > 0 && (newPost.Comments.Count != Comments.Count ||
                    (Comments[0].Time != newPost.Comments[0].Time || 
                    Comments[Comments.Count-1].Time != newPost.Comments[Comments.Count-1].Time)))
                {
                    Comments = newPost.Comments;
                }
            }
        }


        #endregion


        #region Callbacks

        void OnStreamAddComment(string CommentId, Object state, FacebookException e)
        {
            //refresh the comments list
            if (e == null)
            {
                GetComments(DefaultCommentCount);
            }
        }

        void OnStreamRemoveItem(bool result, Object state, FacebookException e)
        {
            //refresh the comments list
            if (e == null)
            {
                GetComments(DefaultCommentCount);
            }
        }

        void OnStreamModifiedLike(bool result, Object state, FacebookException e)
        {
            BindingManager.Instance.ExecuteFqlQuery<stream_dataPosts>(string.Format(_GetStreamByIdQueryString, PostId), OnGetFqlSteamQueryCompleted, null);
            this.NotifyPropertyChanged(PropertyChanged, o=>o.Likes);
        }

        void OnGetFqlSteamQueryCompleted(stream_dataPosts posts, Object state, FacebookException e)
        {
            if (e != null || posts == null || posts.stream_post == null || posts.stream_post.Count == 0)
            {
                return;
            }

            var post = posts.stream_post[0];
            UpdatedTime = DateHelper.ConvertUnixTimeToDateTime(post.updated_time);
            HasLiked = post.likes.user_likes;
            Likes = new ActivityPostLikes(post.likes);

            this.NotifyPropertyChanged( PropertyChanged,o=>o.UpdatedTime);
            this.NotifyPropertyChanged( PropertyChanged,o=>o.HasLiked);
            this.NotifyPropertyChanged( PropertyChanged,o=>o.Likes);
            this.NotifyPropertyChanged( PropertyChanged,o=>o.LikedCount);
        }

        void OnGetCommentsCompleted(ActivityCommentCollection comments, Object state, FacebookException e)
        {
            if ((e != null))
                return;

            Comments = comments;
        }

        #endregion


        #region INotifyPropertyChanged Members
        /// <summary>
        /// Property change event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region IEquatable<ActivityPost> Members

        /// <summary>
        /// Checks if 2 posts are same
        /// </summary>
        /// <param name="other">Post to compare with</param>
        /// <returns>true if the posts are same</returns>
        public bool Equals(ActivityPost other)
        {
            bool Result = false;
            if (other != null)
            {
                Result = (other.PostId == PostId) && (other.FilterKey == FilterKey);
            }

            return Result;
        }

        #endregion

        #region Private Members

        void Likes_ProfileInfoChangeEvent(object sender, EventArgs e)
        {
            this.NotifyPropertyChanged(PropertyChanged, o => o.Likes);
        }


        #endregion

    }


    /// <summary>
    /// Represent stream like object
    /// </summary>
    [DataContract]
    public class ActivityPostLikes : stream_likes, IEquatable<ActivityPostLikes>
    {
        internal event EventHandler<EventArgs> ProfileInfoChangeEvent;
        /// <summary>
        /// Initialized new ActivityPostLikes object
        /// </summary>
        /// <param name="like"></param>
        internal ActivityPostLikes(stream_likes like)
        {
            this.href = like.href;
            this.count = like.count;
            this.sample = like.sample;
            this.friends = like.friends;
            this.user_likes = like.user_likes;
        }
        internal ActivityPostLikes(FacebookStreamLikes like)
        {
            this.href = like.Href;
            this.count = like.Count;
            this.friends.uid = like.Friends;
            this.user_likes = like.UserLikes;
        }

        /// <summary>
        /// Href of likes information
        /// </summary>
        [DataMember(Name = "href")]
        public string Href
        {
            get { return this.href; }
            set { this.href = value; }
        }

        /// <summary>
        /// Count of likes
        /// </summary>
        [DataMember(Name = "count")]
        public int Count
        {
            get { return this.count; }
            set { this.count = value; }
        }

        ///<summary>
        ///</summary>
        [DataMember(Name = "sample")]
        public List<long> Sample
        {
            get {return this.sample.uid;}
            set {this.sample.uid = value;}
        }

        /// <summary>
        /// List of friends who liked the pst
        /// </summary>
        [DataMember(Name = "friends")]
        public List<long> Friends
        {
            get { return this.friends.uid; }
            set { this.friends.uid = value; }
        }

        /// <summary>
        /// User Likes
        /// </summary>
        [DataMember(Name = "user_likes")]
        public bool UserLikes
        {
            get {return this.user_likes;}
            set { this.user_likes = value; }
        }

        /// <summary>
        /// specified if user can like the comment
        /// </summary>
        [DataMember(Name = "can_like")]
        public bool CanLikes
        {
            get { return this.user_likes; }
            set { this.user_likes = value; }
        }


        FacebookProfileCollection _friendsProfile;

        /// <summary>
        /// Collection of profile related to this Activity Post
        /// </summary>
        public FacebookProfileCollection FriendsProfile
        {
            get
            {
                if (_friendsProfile == null && Friends != null)
                {
                    _friendsProfile = BindingManager.Instance.GetProfiles(Friends.ToArray());
                    _friendsProfile.CollectionChanged += _friendsProfile_CollectionChanged;
                }

                return _friendsProfile;
            }
        }


        FacebookProfileCollection _sampleProfile;

        /// <summary>
        /// Collection of profile related to this Activity Post
        /// </summary>
        public FacebookProfileCollection SampleProfile
        {
            get
            {
                if (_sampleProfile == null && Sample != null)
                {
                    _sampleProfile = BindingManager.Instance.GetProfiles(Sample.ToArray());
                    _sampleProfile.CollectionChanged += _sampleProfile_CollectionChanged;
                }

                return _sampleProfile;
            }
        }

        /// <summary>
        /// Operator overload to compare this against another instance
        /// </summary>
        public bool Equals(ActivityPostLikes other)
        {
            if (string.Compare(this.Href, other.Href, StringComparison.OrdinalIgnoreCase) == 0)
            {
                if (this.Count == other.Count && this.UserLikes == other.UserLikes && this.CanLikes == other.CanLikes)
                    return true;
            }
            return false;
        }

        void _sampleProfile_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            FacebookProfileCollection profiles = sender as FacebookProfileCollection;
            profiles.CollectionChanged -= _sampleProfile_CollectionChanged;
            if (ProfileInfoChangeEvent != null)
            {
                ProfileInfoChangeEvent(this, new EventArgs());
            }
        }

        void _friendsProfile_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            FacebookProfileCollection profiles = sender as FacebookProfileCollection;
            profiles.CollectionChanged -= _friendsProfile_CollectionChanged;
            if (ProfileInfoChangeEvent != null)
            {
                ProfileInfoChangeEvent(this, new EventArgs());
            }
        }


    }

}

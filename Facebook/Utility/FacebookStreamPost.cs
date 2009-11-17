using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Facebook.BindingHelper;
using Facebook.Schema;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using Facebook.Rest;

namespace Facebook.Utility
{
    /// <summary>
    /// Represents collection of Stream posts
    /// </summary>
    public class FacebookStreamPostCollection : List<FacebookStreamPost>
    {
                /// <summary>
        /// Initializes FacebookContactCollection object
        /// </summary>
        internal FacebookStreamPostCollection()
            : base()
        {
        }

        /// <summary>
        /// Initializes FacebookContactCollection from contacts list
        /// </summary>
        /// <param name="posts">list of posts</param>
        internal FacebookStreamPostCollection(IEnumerable<FacebookStreamPost> posts)
            : base(posts)
        {
        }
    }

    /// <summary>
    /// Contains information about a stream post
    /// </summary>
    [DataContract]
    public class FacebookStreamPost
    {
        /// <summary>
        /// Constructor that hydrates this object from xml data transfer object
        /// </summary>
        public FacebookStreamPost(stream_post post)
        {
            this.ActionLinks = new List<FacebookStreamActionLink>(from l in post.action_links.stream_action_link select new FacebookStreamActionLink(l));
            this.ActorId = post.actor_id;
            this.AppData = new FacebookStreamAppData(post.app_data);
            this.AppId = post.app_id.HasValue ? post.app_id.Value : 0;
            this.Attachment = new FacebookStreamAttachment(post.attachment);
            this.Attribution = post.attribution;
            this.Created = post.created_time;
            this.Likes = new FacebookStreamLikes(post.likes);
            this.Message = post.message;
            this.PostId = post.post_id;
            this.SourceId = post.source_id;
            this.TargetId = post.target_id.ToString();
            this.Type = post.type;
            this.Updated = post.updated_time;
            this.ViewerId = post.viewer_id;
            this.StreamComments = new FacebookStreamComments();
            this.StreamComments.CanPost = post.comments.can_post;
            this.StreamComments.CanRemove = post.comments.can_remove;
            this.StreamComments.Count = post.comments.count;
            this.StreamComments.Comments = new List<FacebookComment>(from c in post.comments.comment_list.comment select new FacebookComment(c));
        }
        /// <summary>
        /// Id of this post
        /// </summary>
        [DataMember(Name = "post_id")]
        public string PostId
        {
            get;
            set;
        }

        /// <summary>
        /// viewer id
        /// </summary>
        [DataMember(Name = "viewer_id")]
        public long ViewerId
        {
            get;
            set;
        }

        /// <summary>
        /// View information
        /// </summary>
        [DataMember(Name = "view")]
        public string View
        {
            get;
            set;
        }

        /// <summary>
        /// Source id of this post
        /// </summary>
        [DataMember(Name = "source_id")]
        public long SourceId
        {
            get;
            set;
        }

        /// <summary>
        /// Type of this post
        /// </summary>
        [DataMember(Name = "type")]
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// App id that posted this
        /// </summary>
        [DataMember(Name = "app_id")]
        public long AppId
        {
            get;
            set;
        }

        /// <summary>
        /// Attribution information
        /// </summary>
        [DataMember(Name = "attribution")]
        public string Attribution
        {
            get;
            set;
        }

        /// <summary>
        /// userId of the user who posted this post
        /// </summary>
        [DataMember(Name = "actor_id")]
        public long ActorId
        {
            get;
            set;
        }

        ///<summary>
        ///</summary>
        public FacebookProfile ActorProfile
        {
            get;
            internal set;
        }

        /// <summary>
        /// Target id
        /// </summary>
        [DataMember(Name = "target_id")]
        public string TargetId
        {
            get;
            set;
        }

        /// <summary>
        /// Post message
        /// </summary>
        [DataMember(Name = "message")]
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Attachment information
        /// </summary>
        [DataMember(Name = "attachment")]
        public FacebookStreamAttachment Attachment
        {
            get;
            set;
        }

        /// <summary>
        /// App data for this post
        /// </summary>
        [DataMember(Name = "app_data")]
        public FacebookStreamAppData AppData
        {
            get;
            set;
        }

        /// <summary>
        /// Action links for this post
        /// </summary>
        [DataMember(Name = "action_links")]
        public List<FacebookStreamActionLink> ActionLinks
        {
            get;
            set;
        }

        /// <summary>
        /// Comments information for this post
        /// </summary>
        [DataMember(Name = "comments")]
        public FacebookStreamComments StreamComments
        {
            get;
            set;
        }

        /// <summary>
        /// Likes information for this post
        /// </summary>
        [DataMember(Name = "likes")]
        public FacebookStreamLikes Likes
        {
            get;
            set;
        }
        
        /// <summary>
        /// Updated time in facebook format
        /// </summary>
        [DataMember(Name = "updated_time")]
        internal long Updated
        {
            get;
            set;
        }

        /// <summary>
        /// Time of last update to this post
        /// </summary>
        public DateTime UpdatedTime
        {
            get
            {
                return DateHelper.ConvertUnixTimeToDateTime(Updated);
            }
        }

        /// <summary>
        /// Created time in facebook format
        /// </summary>
        [DataMember(Name = "created_time")]
        internal long Created
        {
            get;
            set;
        }

        /// <summary>
        /// This this post was created
        /// </summary>
        public DateTime CreatedTime
        {
            get
            {
                return DateHelper.ConvertUnixTimeToDateTime(Created);
            }
        }

    }

    /// <summary>
    /// Represents Stream property information
    /// </summary>
    [DataContract]
    public class FacebookStreamActionLink
    {

        /// <summary>
        /// Constructor to hydrate given an object serialized from xml
        /// </summary>
        public FacebookStreamActionLink(stream_action_link link)
        {
            this.Href = link.href;
            this.Name = link.text;
            this.Text = link.text;
        }
        /// <summary>
        /// Name of property
        /// </summary>
        [DataMember(Name = "name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Text of property
        /// </summary>
        [DataMember(Name = "text")]
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Href of property
        /// </summary>
        [DataMember(Name = "href")]
        public string Href
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Represents stream app data information
    /// </summary>
    [DataContract]
    public class FacebookStreamAppData
    {
        /// <summary>
        /// Constructor that populates this object for xml deserialized version
        /// </summary>
        public FacebookStreamAppData(stream_app_data data)
        {
            this.AttachmentData = data.attachment_data;
            this.Images = data.images;
            this.TbId = data.tbid;
        }
        /// <summary>
        /// Id of data
        /// </summary>
        [DataMember(Name = "tbid")]
        public long TbId
        {
            get;
            set;
        }

        /// <summary>
        /// Attachement data
        /// </summary>
        [DataMember(Name = "attachment_data")]
        public string AttachmentData
        {
            get;
            set;
        }

        /// <summary>
        /// Images information
        /// </summary>
        [DataMember(Name = "images")]
        public string Images
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Represents comments information
    /// </summary>
    [DataContract]
    public class FacebookStreamComments
    {
        /// <summary>
        /// Specifies if user can remove a comment
        /// </summary>
        [DataMember(Name = "can_remove")]
        public bool CanRemove
        {
            get;
            set;
        }

        /// <summary>
        /// Specifies if user can post a comment
        /// </summary>
        [DataMember(Name = "can_post")]
        public bool CanPost
        {
            get;
            set;
        }

        /// <summary>
        /// Number of comments for the post
        /// </summary>
        [DataMember(Name = "count")]
        public int Count
        {
            get;
            set;
        }

        /// <summary>
        /// List of comments for this post
        /// </summary>
        [DataMember(Name = "comment_list")]
        public List<FacebookComment> Comments
        {
            get;
            set;
        }

    }

    /// <summary>
    /// Represents a facebook comment
    /// </summary>
    [DataContract]
    public class FacebookComment
    {
        /// <summary>
        /// Hydrates this object from a comment
        /// </summary>
        public FacebookComment(comment c)
        {
            this.CommentId = c.id;
            this.FromId = c.fromid;
            this.PostId = c.post_id;
            this.ReplyXId = c.reply_xid;
            this.Text = c.text;
            this.Time = DateHelper.ConvertUnixTimeToDateTime(c.time);
            this.UserName = c.username;
            this.XId = c.xid;

        }
        /// <summary>
        /// Id
        /// </summary>
        [DataMember(Name = "xid")]
        public string XId
        {
            get;
            set;
        }

        /// <summary>
        /// Id of user who commented
        /// </summary>
        [DataMember(Name = "fromid")]
        public long FromId
        {
            get;
            set;
        }

        ///<summary>
        ///</summary>
        public FacebookProfile FromUser
        {
            get;
            internal set;
        }


        /// <summary>
        /// Time in facebook format
        /// </summary>
        [DataMember(Name = "time")]
        internal long time
        {
            get;
            set;
        }

        /// <summary>
        /// Time when this comment was made
        /// </summary>
        public DateTime Time
        {
            get
            {
                return DateHelper.ConvertUnixTimeToDateTime(time);
            }
            set
            {
                time = DateHelper.ConvertDateToFacebookDate(value);
            }
        }

        /// <summary>
        /// Text of comment
        /// </summary>
        [DataMember(Name = "text")]
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Comment id
        /// </summary>
        [DataMember(Name = "id")]
        public string CommentId
        {
            get;
            set;
        }

        /// <summary>
        /// Username who made this comment
        /// </summary>
        [DataMember(Name = "username")]
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Reply xid
        /// </summary>
        [DataMember(Name = "reply_xid")]
        public string ReplyXId
        {
            get;
            set;
        }

        /// <summary>
        /// Post id for which the comment was made.
        /// </summary>
        [DataMember(Name = "post_id")]
        public string PostId
        {
            get;
            set;
        }

    }

    /// <summary>
    /// Contains information about stream likes
    /// </summary>
    [DataContract]
    public class FacebookStreamLikes
    {
        /// <summary>
        /// hydrate the object from schema object deserialized from xml response.
        /// </summary>
        public FacebookStreamLikes(stream_likes likes)
        {
            this.Count = likes.count;
            this.Friends = likes.friends.uid;
            this.Href = likes.href;
            this.UserLikes = likes.user_likes;
        }
        /// <summary>
        /// Href of likes information
        /// </summary>
        [DataMember(Name = "href")]
        public string Href
        {
            get;
            set;
        }

        /// <summary>
        /// Count of likes
        /// </summary>
        [DataMember(Name = "count")]
        public int Count
        {
            get;
            set;
        }

        //[DataMember(Name = "sample")]
        //public IList<long> Sample
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// List of friends who liked the pst
        /// </summary>
        [DataMember(Name = "friends")]
        public List<long> Friends
        {
            get;
            set;
        }

        /// <summary>
        /// User Likes
        /// </summary>
        [DataMember(Name = "user_likes")]
        public bool UserLikes
        {
            get;
            set;
        }

        /// <summary>
        /// specified if user can like the comment
        /// </summary>
        [DataMember(Name = "can_like")]
        public bool CanLikes
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Represents attachment information
    /// </summary>
    [DataContract]
    public class FacebookStreamAttachment : attachment
    {
        /// <summary>
        /// hydrates object with object from xml deserialization
        /// </summary>
        public FacebookStreamAttachment(stream_attachment attachment)
        {
            this.Caption = attachment.caption;
            this.Description = attachment.description;
            this.Href = attachment.href;
            //this.Latitude = attachment.properties.s
            this.Media = new List<FacebookStreamMedia>(from m in attachment.media.stream_media select new FacebookStreamMedia(m));
            this.Name = attachment.name;
            
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public FacebookStreamAttachment()
        {
        }
        /// <summary>
        /// List of media information
        /// </summary>
        [DataMember(Name = "media")]
        public List<FacebookStreamMedia> Media
        {
            get;
            set;
        }

        /// <summary>
        /// Name of attachment
        /// </summary>
        [DataMember(Name = "name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Href of attachement
        /// </summary>
        [DataMember(Name = "href")]
        public string Href
        {
            get;
            set;
        }

        /// <summary>
        /// Caption of attachment
        /// </summary>
        [DataMember(Name = "caption")]
        public string Caption
        {
            get;
            set;
        }

        /// <summary>
        /// Description of attachment
        /// </summary>
        [DataMember(Name = "description")]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Latitude of attachment
        /// </summary>
        [DataMember(Name = "latitude")]
        public string Latitude
        {
            get;
            set;
        }

        /// <summary>
        /// Longitude of attachment
        /// </summary>
        [DataMember(Name = "longitude")]
        public string Longitude
        {
            get;
            set;
        }
        /// <summary>
        /// Icon
        /// </summary>
        [DataMember]
        public string Icon
        {
            get;
            internal set;
        }
    }

    /// <summary>
    /// Represent sstream media information
    /// </summary>
    [DataContract]
    public class FacebookStreamMedia
    {

        /// <summary>
        /// Href of media
        /// </summary>
        [DataMember(Name = "href")]
        public string Href
        {
            get;
            set;
        }

        /// <summary>
        /// Alt information
        /// </summary>
        [DataMember(Name = "alt")]
        public string Alt
        {
            get;
            set;
        }

        /// <summary>
        /// Type of media
        /// </summary>
        [DataMember(Name = "type")]
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Media source
        /// </summary>
        [DataMember(Name = "src")]
        public string Source
        {
            get;
            set;
        }

        /// <summary>
        /// Video information
        /// </summary>
        [DataMember(Name = "video")]
        public FacebookStreamVideo Video
        {
            get;
            set;
        }

        /// <summary>
        /// Object information
        /// </summary>
        [DataMember(Name = "obj")]
        public string Obj
        {
            get;
            set;
        }

        /// <summary>
        /// Music information
        /// </summary>
        [DataMember(Name = "music")]
        public FacebookStreamMusic Music
        {
            get;
            set;
        }

        /// <summary>
        /// Data information
        /// </summary>
        [DataMember(Name = "data")]
        public string Data
        {
            get;
            set;
        }

        /// <summary>
        /// Photo information
        /// </summary>
        [DataMember(Name = "photo")]
        public FacebookStreamPhoto Photo
        {
            get;
            set;
        }

        /// <summary>
        /// Flash object information
        /// </summary>
        [DataMember(Name = "swf")]
        public FacebookStreamSwf Swf
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public FacebookStreamMedia()
        {
        }

        /// <summary>
        /// hydrates object from object from xml deserialization
        /// </summary>
        public FacebookStreamMedia(stream_media media)
        {
            Alt = media.alt;
            Data = media.data;
            Href = media.href;
            Music = new FacebookStreamMusic(media.music);
            Obj = media.obj;
            Photo = new FacebookStreamPhoto(media.photo);
            Source = media.src;
            Swf = new FacebookStreamSwf(media.swf);
            Type = media.type;
            Video = new FacebookStreamVideo(media.video);
        }
    }

    /// <summary>
    /// Represents a video in stream
    /// </summary>
    [DataContract]
    public class FacebookStreamVideo
    {
        /// <summary>
        /// Display url
        /// </summary>
        [DataMember(Name = "display_url")]
        public string DisplayUrl
        {
            get;
            set;
        }

        /// <summary>
        /// source url
        /// </summary>
        [DataMember(Name = "source_url")]
        public string SourceUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Owner information
        /// </summary>
        [DataMember(Name = "owner")]
        public long Owner
        {
            get;
            set;
        }

        /// <summary>
        /// Perma link
        /// </summary>
        [DataMember(Name = "permalink")]
        public string PermaLink
        {
            get;
            set;
        }

        /// <summary>
        /// Preview image
        /// </summary>
        [DataMember(Name = "preview_img")]
        public string PreviewImg
        {
            get;
            set;
        }

        internal FacebookStreamVideo()
        {
        }

        internal FacebookStreamVideo(stream_video video)
        {
            DisplayUrl = video.display_url;
            Owner = video.owner;
            PermaLink = video.permalink;
            PreviewImg = video.preview_img;
            SourceUrl = video.source_url;
        }
        internal FacebookStreamVideo(FacebookStreamVideo video)
        {
            DisplayUrl = video.DisplayUrl;
            Owner = video.Owner;
            PermaLink = video.PermaLink;
            PreviewImg = video.PreviewImg;
            SourceUrl = video.SourceUrl;
        }
    }

    /// <summary>
    /// Represents music information in steam
    /// </summary>
    [DataContract]
    public class FacebookStreamMusic
    {
        /// <summary>
        /// Source url
        /// </summary>
        [DataMember(Name = "source_url")]
        public string SourceUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Title 
        /// </summary>
        [DataMember(Name = "title")]
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Artist info
        /// </summary>
        [DataMember(Name = "artist")]
        public string Artist
        {
            get;
            set;
        }

        /// <summary>
        /// Album info
        /// </summary>
        [DataMember(Name = "album")]
        public string Album
        {
            get;
            set;
        }

        internal FacebookStreamMusic()
        {
        }

        internal FacebookStreamMusic(stream_music music)
        {
            Album = music.album;
            Artist = music.artist;
            SourceUrl = music.source_url;
            Title = music.title;
        }
        internal FacebookStreamMusic(FacebookStreamMusic music)
        {
            Album = music.Album;
            Artist = music.Artist;
            SourceUrl = music.SourceUrl;
            Title = music.Title;
        }
    }

    /// <summary>
    /// Represents photo info in stream
    /// </summary>
    [DataContract]
    public class FacebookStreamPhoto
    {
        /// <summary>
        /// Album id
        /// </summary>
        [DataMember(Name = "aid")]
        public string AlbumId
        {
            get;
            set;
        }

        /// <summary>
        /// Photo Id
        /// </summary>
        [DataMember(Name = "pid")]
        public string PhotoId
        {
            get;
            set;
        }

        /// <summary>
        /// Owner information
        /// </summary>
        [DataMember(Name = "owner")]
        public long Owner
        {
            get;
            set;
        }

        /// <summary>
        /// Index information
        /// </summary>
        [DataMember(Name = "index")]
        public int Index
        {
            get;
            set;
        }

        internal FacebookStreamPhoto()
        {
        }

        internal FacebookStreamPhoto(stream_photo photo)
        {
            AlbumId = photo.aid;
            Index = photo.index;
            Owner = photo.owner;
            PhotoId = photo.pid;
        }
    }

    /// <summary>
    /// Represents flash object information
    /// </summary>
    [DataContract]
    public class FacebookStreamSwf
    {
        /// <summary>
        /// source url
        /// </summary>
        [DataMember(Name = "source_url")]
        public string SourceUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Preivew image url
        /// </summary>
        [DataMember(Name = "preview_img")]
        public string PreviewImg
        {
            get;
            set;
        }

        /// <summary>
        /// flash vars
        /// </summary>
        [DataMember(Name = "flash_vars")]
        public string FlashVars
        {
            get;
            set;
        }

        /// <summary>
        /// width info
        /// </summary>
        [DataMember(Name = "width")]
        public int Width
        {
            get;
            set;
        }

        /// <summary>
        /// Height info
        /// </summary>
        [DataMember(Name = "height")]
        public int Height
        {
            get;
            set;
        }

        internal FacebookStreamSwf()
        {
        }

        internal FacebookStreamSwf(stream_swf swf)
        {
            FlashVars = swf.flash_vars;
            Height = swf.height;
            PreviewImg = swf.preview_img;
            SourceUrl = swf.source_url;
            Width = swf.width;
        }
        internal FacebookStreamSwf(FacebookStreamSwf swf)
        {
            FlashVars = swf.FlashVars;
            Height = swf.Height;
            PreviewImg = swf.PreviewImg;
            SourceUrl = swf.SourceUrl;
            Width = swf.Width;
        }
    }

    /// <summary>
    /// StreamFilter
    /// </summary>
    [DataContract]
    public class StreamFilter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public StreamFilter(stream_filter filter)
        {
            this.FilterKey = filter.filter_key;
            this.IconUrl = filter.icon_url;
            this.Name = filter.name;
            this.Rank = filter.rank;
            this.Type = filter.type;
            this.UserId = filter.uid;
            this.value = filter.value.HasValue ? filter.value.Value : 0;
        }
        /// <summary>
        /// UserId of user
        /// </summary>
        [DataMember(Name = "uid")]
        public long UserId
        {
            get;
            set;
        }

        /// <summary>
        /// A key identifying a particular filter for a user's stream
        /// </summary>
        [DataMember(Name = "filter_key")]
        public string FilterKey
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the filter 
        /// </summary>
        [DataMember(Name = "name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// A 32-bit int that indicates where the filter appears in the sort
        /// </summary>
        [DataMember(Name = "rank")]
        public int Rank
        {
            get;
            set;
        }

        /// <summary>
        /// The URL to the filter icon
        /// </summary>
        [DataMember(Name = "icon_url")]
        public string IconUrl
        {
            get;
            set;
        }

        /// <summary>
        /// If true, indicates that the filter is visible on the home page
        /// </summary>
        [DataMember(Name = "is_visible")]
        public bool Visible
        {
            get;
            set;
        }

        /// <summary>
        /// The type of filter. One of application, newsfeed, friendlist, network, or publicprofile
        /// </summary>
        [DataMember(Name = "type")]
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// A 64-bit ID for the filter type
        /// </summary>
        [DataMember(Name = "value")]
        public long value
        {
            get;
            set;
        }
    }
}






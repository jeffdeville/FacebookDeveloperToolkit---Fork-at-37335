
using System.Collections.Specialized;
using System.IO;

namespace Facebook.BindingHelper
{
    using System.ComponentModel;
    using System;
    using System.Runtime.Serialization;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using Facebook.Utility;
    using Facebook.Schema;

    /// <summary>
    /// Defines a Facebook Photo entity
    /// </summary>
    [DataContract]
    public class FacebookPhoto : photo, INotifyPropertyChanged, IEquatable<FacebookPhoto>
    {
        /// <summary>
        /// Initializes FacebookPhoto object
        /// </summary>
        public FacebookPhoto()
            : base()
        {
        }

        /// <summary>
        /// Initializes FacebookPhoto object
        /// </summary>
        internal FacebookPhoto(photo photo)
        {
            aid = photo.aid;
            caption = photo.caption;
            created = photo.created;
            link = photo.link;
            modified = photo.modified;
            owner = photo.owner;
            pid = photo.pid;
            src = photo.src;
            src_big = photo.src_big;
            src_small = photo.src_small;
            story_fbid = photo.story_fbid;
        }

        FacebookImage _image;

        /// <summary>
        /// Facebook Image object defining size images of photo object
        /// </summary>
        public FacebookImage Image
        {
            get
            {
                if (_image == null)
                {
                    _image = new FacebookImage(this.src, this.src_big, this.src_small, null);
                }
                return _image;
            }
        }

        FacebookPhotoTagCollection _tags;
        /// <summary>
        /// Gets list of tags for this photo.
        /// </summary>
        public FacebookPhotoTagCollection Tags 
        {
            get
            {
                if (_tags == null)
                {
                    _tags = new FacebookPhotoTagCollection();
                }
                return _tags;
            }
            internal set
            {
                _tags = value;
                this.NotifyPropertyChanged(PropertyChanged, o=>o.Tags);
            }
        }
        /// <summary>
        /// Photo Id
        /// </summary>
        [DataMember(Name = "pid")]
        public string PhotoId
        {
            get { return this.pid; }
            set { this.pid = value; }
        }

        /// <summary>
        /// Album Id
        /// </summary>
        [DataMember(Name = "aid")]
        public string AlbumId
        {
            get { return this.aid; }
            set { this.aid = value; }
        }

        /// <summary>
        /// Owner of this photo
        /// </summary>
        [DataMember(Name = "owner")]
        public long Owner
        {
            get { return this.owner; }
            set { this.owner = value; }
        }

        /// <summary>
        /// Url of the photo
        /// </summary>
        [DataMember(Name = "src")]
        public string Source
        {
            get { return this.src; }
            set { this.src = value; }
        }

        /// <summary>
        /// Url of big size photo
        /// </summary>
        [DataMember(Name = "src_big")]
        public string SourceBig
        {
            get {return this.src_big;}
            set { this.src_big = value; }
        }

        /// <summary>
        /// Url of small size photo
        /// </summary>
        [DataMember(Name = "src_small")]
        public string SourceSmall
        {
            get { return this.src_small; }
            set { this.src_small = value; }
        }

        /// <summary>
        /// Link information
        /// </summary>
        [DataMember(Name = "link")]
        public string Link
        {
            get { return this.link; }
            set { this.link = value; }
        }

        /// <summary>
        /// Caption of photo
        /// </summary>
        [DataMember(Name = "caption")]
        public string Caption
        {
            get { return this.caption; }
            set { this.caption = value; }
        }

        /// <summary>
        /// Created time in facebook format
        /// </summary>
        [DataMember(Name = "created")]
        internal long Created
        {
            get { return this.created; }
            set { this.created = value; }
        }

        /// <summary>
        /// Created time
        /// </summary>
        public DateTime CreatedTime
        {
            get
            {
                return DateHelper.ConvertUnixTimeToDateTime(this.Created);
            }
        }

        /// <summary>
        /// Story Fbid
        /// </summary>
        [DataMember(Name = "story_fbid")]
        public long StoryFbid
        {
            get { return this.story_fbid; }
            set { this.story_fbid = value; }
        }

        /// <summary>
        /// Modified time in facebook format
        /// </summary>
        [DataMember(Name = "modified")]
        internal long Modified
        {
            get { return this.modified; }
            set { this.modified = value; }
        }

        /// <summary>
        /// Modified time
        /// </summary>
        public DateTime ModifiedTime
        {
            get
            {
                return DateHelper.ConvertUnixTimeToDateTime(this.Modified);
            }
        }

        /// <summary>
        /// Adds a tag to the photo.
        /// </summary>
        public void AddTag(FacebookPhotoTag tag)
        {
            long subject = 0;

            if (tag.Subject != null)
            {
                long.TryParse(tag.Subject, out subject);
            }
            BindingManager.Instance.Api.Photos.AddTagAsync(PhotoId, subject, tag.Text, 
                                                                    (float)Convert.ToDouble(tag.Xcoord), (float)Convert.ToDouble(tag.Ycoord),
                                                                    OnAddPhotoTagCompleted, null);
        }

        FacebookPhotoAlbum _album;
        /// <summary>
        /// Gets album object holding this photo
        /// </summary>
        public FacebookPhotoAlbum Album
        {
            get
            {
                if (_album == null)
                {
                    FacebookPhotoAlbumCollection albums = BindingManager.Instance.GetAlbums(new string[] {this.AlbumId});
                    if (albums.Count != 0)
                    {
                        _album = albums[0];
                    }
                    else
                    {
                        albums.CollectionChanged += new NotifyCollectionChangedEventHandler(albums_CollectionChanged);
                    }
                }
                return _album;
            }
        }

        /// <summary>
        /// FacebookContact object for owner of this album
        /// </summary>
        public FacebookContact OwnerInfo
        {
            get
            {
                return BindingManager.Instance.GetUserFromCache(Owner);
            }
        }

        /// <summary>
        /// stream the file to save locally
        /// </summary>
        public void Save(Stream stm)
        {
            WebClientHelper wc = new WebClientHelper(stm);
            wc.RequestCompleted += new EventHandler<RequestCompletedEventArgs>(DownLoadCompleted);
            wc.SendRequest(new Uri(this.SourceBig), null, null);

        }

        #region Callbacks

        void albums_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            FacebookPhotoAlbumCollection albums = (FacebookPhotoAlbumCollection)sender;
            if (albums.Count != 0)
            {
                _album = albums[0];
                Utilities.NotifyPropertyChanged(this, PropertyChanged, "Album");
            }
        }

        void OnGetTags(IList<photo_tag> tags, Object state, FacebookException e)
        {
            if (e == null && tags != null)
            {
                foreach (photo_tag tag in tags)
                {
                    if(!Tags.Contains(new FacebookPhotoTag(tag)))
                        Tags.AddInternal(new FacebookPhotoTag(tag));
                }
            }
        }

        void OnAddPhotoTagCompleted(bool added, object state, FacebookException ex)
        {
            BindingManager.Instance.Api.Photos.GetTagsAsync(new List<string>() { this.PhotoId }, OnGetTags, null);
        }

        void DownLoadCompleted(object sender, RequestCompletedEventArgs e)
        {
            if(e.Error == null)
            {
                Stream stm = (Stream)e.UserState;
                using (BinaryReader reader = new BinaryReader(e.Response))
                {
                    var buffer = new byte[2048];
                    int bytesRead;

                    do
                    {
                        bytesRead = reader.Read(buffer, 0, buffer.Length);
                        stm.Write(buffer, 0, bytesRead);
                    } while (bytesRead > 0);
                }

#if !SILVERLIGHT
                Application.Current.Dispatcher.BeginInvoke(new Action(()=> stm.Close()));
#else
                Deployment.Current.Dispatcher.BeginInvoke(() => stm.Close());
#endif

            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// PropertyChange notification event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        #region IEquatable<FacebookPhoto> Members

        /// <summary>
        /// Overridden operator to compare this instance to another photo
        /// </summary>
        public bool Equals(FacebookPhoto other)
        {
            return (other != null && other.PhotoId == this.PhotoId);
        }

        #endregion
    }
    /// <summary>
    /// Represents photo tag collection
    /// </summary>
    public class PhotoTagsCollection : List<FacebookPhotoTag>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PhotoTagsCollection(IEnumerable<FacebookPhotoTag> tags)
            : base(tags)
        {

        }

    }
    /// <summary>
    /// Contains photo tag information
    /// </summary>
    [DataContract]
    public class FacebookPhotoTag : IEquatable<FacebookPhotoTag>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FacebookPhotoTag()
        {
        }
        /// <summary>
        /// Constructor that hydrates this object from xml deserialized object
        /// </summary>
        public FacebookPhotoTag(photo_tag tag)
        {
            this.Created = tag.created;
            this.PhotoId = tag.pid;
            this.Subject = tag.subject.ToString();
            this.Text = tag.text;
            this.Xcoord = tag.xcoord;
            this.Ycoord = tag.ycoord;
        }
        /// <summary>
        /// photo Id
        /// </summary>
        [DataMember(Name = "pid")]
        public string PhotoId
        {
            get;
            set;
        }

        /// <summary>
        /// Subject 
        /// </summary>
        [DataMember(Name = "subject")]
        public string Subject
        {
            get;
            set;
        }

        /// <summary>
        /// Text of tag
        /// </summary>
        [DataMember(Name = "text")]
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Xcoord
        /// </summary>
        [DataMember(Name = "xcoord")]
        public decimal Xcoord
        {
            get;
            set;
        }

        /// <summary>
        /// Ycoord
        /// </summary>
        [DataMember(Name = "ycoord")]
        public decimal Ycoord
        {
            get;
            set;
        }

        /// <summary>
        /// Created time in facebook format
        /// </summary>
        [DataMember(Name = "created")]
        internal long Created
        {
            get;
            set;
        }

        /// <summary>
        /// Created time
        /// </summary>
        public DateTime CreatedTime
        {
            get
            {
                return DateHelper.ConvertUnixTimeToDateTime(this.Created);
            }
        }


        #region IEquatable<FacebookPhotoTag> Members

        /// <summary>
        /// Operator overload to compare this instance against another FacebookPhotoTag
        /// </summary>
        public bool Equals(FacebookPhotoTag other)
        {
            return (other != null && other.Created == this.Created && other.PhotoId == this.PhotoId && other.Subject == this.Subject && other.Xcoord == this.Xcoord && other.Ycoord == this.Ycoord && other.Text == this.Text);
        }

        #endregion
    }

}
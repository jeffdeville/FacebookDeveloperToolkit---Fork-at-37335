using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using Facebook.Utility;
using Facebook.Schema;

namespace Facebook.BindingHelper
{
    /// <summary>
    /// Defines a stream post object
    /// </summary>
    [DataContract]
    public class ActivityPostAttachment
    {

        /// <summary>
        /// Initializes a ActivityPost object
        /// </summary>
        /// <param name="attachment">activitypost attachment</param>
        internal ActivityPostAttachment(stream_attachment attachment)
        {
            Name = attachment.name;
            Href = attachment.href;
            Caption = attachment.caption;
            Description = attachment.description;
            Icon = attachment.icon;
            Media = new List<ActivityPostAttachmentMedia>(from m in attachment.media.stream_media select new ActivityPostAttachmentMedia(m));
        }
        /// <summary>
        /// Initializes a ActivityPost object
        /// </summary>
        /// <param name="attachment">activitypost attachment</param>
        internal ActivityPostAttachment(FacebookStreamAttachment attachment)
        {
            Name = attachment.Name;
            Href = attachment.Href;
            Caption = attachment.Caption;
            Description = attachment.Description;
            Icon = attachment.Icon;
            Media = new List<ActivityPostAttachmentMedia>(from m in attachment.Media select new ActivityPostAttachmentMedia(m));
        }

        /// <summary>
        /// List of media information
        /// </summary>
        [DataMember]
        public IList<ActivityPostAttachmentMedia> Media
        {
            get;
            internal set;
        }

        /// <summary>
        /// Name of attachment
        /// </summary>
        [DataMember]
        public string Name
        {
            get;
            internal set;
        }

        /// <summary>
        /// Href of attachement
        /// </summary>
        [DataMember]
        public string Href
        {
            get;
            internal set;
        }

        /// <summary>
        /// Caption of attachment
        /// </summary>
        [DataMember]
        public string Caption
        {
            get;
            internal set;
        }

        /// <summary>
        /// Description of attachment
        /// </summary>
        [DataMember]
        public string Description
        {
            get;
            internal set;
        }

        /// <summary>
        /// Icon of attachment
        /// </summary>
        [DataMember]
        public string Icon
        {
            get;
            internal set;
        }
		

    }

    /// <summary>
    /// Represents an attachment media object
    /// </summary>
    [DataContract]
    public class ActivityPostAttachmentMedia
    {
        /// <summary>
        /// Href of media
        /// </summary>
        [DataMember]
        public string Href
        {
            get;
            set;
        }

        /// <summary>
        /// Alt information
        /// </summary>
        [DataMember]
        public string Alt
        {
            get;
            set;
        }

        /// <summary>
        /// Type of media
        /// </summary>
        [DataMember]
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Media source
        /// </summary>
        [DataMember]
        public string Source
        {
            get;
            set;
        }

        /// <summary>
        /// Video information
        /// </summary>
        [DataMember]
        public FacebookStreamVideo Video
        {
            get;
            set;
        }

        /// <summary>
        /// Object information
        /// </summary>
        [DataMember]
        public string Obj
        {
            get;
            set;
        }

        /// <summary>
        /// Music information
        /// </summary>
        [DataMember]
        public FacebookStreamMusic Music
        {
            get;
            set;
        }

        /// <summary>
        /// Data information
        /// </summary>
        [DataMember]
        public string Data
        {
            get;
            set;
        }

        /// <summary>
        /// Photo information
        /// </summary>
        [DataMember]
        public ActivityPostAttachmentPhotoInfo Photo
        {
            get;
            set;
        }

        /// <summary>
        /// Flash object information
        /// </summary>
        [DataMember]
        public FacebookStreamSwf Swf
        {
            get;
            set;
        }

        internal ActivityPostAttachmentMedia(FacebookStreamMedia media)
        {
            this.Alt = media.Alt;
            this.Data = media.Data;
            this.Href = media.Href;
            this.Music = new FacebookStreamMusic(media.Music);
            this.Obj = media.Obj;
            this.Source = media.Source;
            this.Swf = new FacebookStreamSwf(media.Swf);
            this.Type = media.Type;
            this.Video = new FacebookStreamVideo(media.Video);

            if (media.Photo != null)
            {
                this.Photo = new ActivityPostAttachmentPhotoInfo(media.Photo);
            }
        }
        /// <summary>
        /// Initializes new AcitivtyPostAttachmentMedia object
        /// </summary>
        /// <param name="media"></param>
        internal ActivityPostAttachmentMedia(stream_media media)
        {
            this.Alt = media.alt;
            this.Data = media.data;
            this.Href = media.href;
            this.Music = new FacebookStreamMusic(media.music);
            this.Obj = media.obj;
            this.Source = media.src;
            this.Swf = new FacebookStreamSwf(media.swf);
            this.Type = media.type;
            this.Video = new FacebookStreamVideo(media.video);

            if (media.photo != null)
            {
                this.Photo = new ActivityPostAttachmentPhotoInfo(media.photo);
            }
        }
    }

    /// <summary>
    /// Represents photo info in Attachment
    /// </summary>
    [DataContract]
    public class ActivityPostAttachmentPhotoInfo : INotifyPropertyChanged
    {
        /// <summary>
        /// Album id
        /// </summary>
        [DataMember]
        public string AlbumId
        {
            get;
            set;
        }

        FacebookPhotoAlbum _album;
        /// <summary>
        /// Album object 
        /// </summary>
        public FacebookPhotoAlbum Album
        {
            get
            {
                lock (this)
                {
                    if (_album == null)
                    {
                        FacebookPhotoAlbumCollection albums =
                            BindingManager.Instance.GetAlbums(new string[] {this.AlbumId});

                        if (albums.Count > 0)
                            _album = albums[0];
                        else
                            albums.CollectionChanged += albums_CollectionChanged;
                    }

                    return _album;
                }

            }
        }

        /// <summary>
        /// Photo Id
        /// </summary>
        [DataMember]
        public string PhotoId
        {
            get;
            set;
        }

        FacebookPhoto _photo;
        /// <summary>
        /// Photo object
        /// </summary>
        public FacebookPhoto Photo
        {
            get
            {
                lock (this)
                {
                    if (_photo == null)
                    {
                        BindingManager.Instance.Api.Photos.GetAsync(null, this.AlbumId, new List<string>() { this.PhotoId }, OnGetPhotos, null);
                    }

                    return _photo;
                }

            }
        }
        /// <summary>
        /// Owner information
        /// </summary>
        [DataMember]
        public long Owner
        {
            get;
            set;
        }

        /// <summary>
        /// Index information
        /// </summary>
        [DataMember]
        public int Index
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes new ActivityPostAttachmentPhotoInfo object
        /// </summary>
        /// <param name="photo">Photo information</param>
        internal ActivityPostAttachmentPhotoInfo(stream_photo photo)
        {
            this.AlbumId = photo.aid;
            this.PhotoId = photo.pid;
            this.Owner = photo.owner;
            this.Index = photo.index;
        }
        /// <summary>
        /// Initializes new ActivityPostAttachmentPhotoInfo object
        /// </summary>
        /// <param name="photo">Photo information</param>
        internal ActivityPostAttachmentPhotoInfo(FacebookStreamPhoto photo)
        {
            this.AlbumId = photo.AlbumId;
            this.PhotoId = photo.PhotoId;
            this.Owner = photo.Owner;
            this.Index = photo.Index;
        }
        

        #region INotifyPropertyChanged Members
        /// <summary>
        /// Property change event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Callbacks

        void OnGetPhotos(IList<photo> photos, Object state, FacebookException e)
        {
            if (e == null && photos != null && photos.Count != 0)
            {
                _photo = new FacebookPhoto(photos[0]);
                this.NotifyPropertyChanged(PropertyChanged, o => o.Photo);
            }
        }

        void albums_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            FacebookPhotoAlbumCollection albums = (FacebookPhotoAlbumCollection)sender;
            albums.CollectionChanged -= albums_CollectionChanged;

            if (albums.Count > 0)
                _album = albums[0];

            this.NotifyPropertyChanged(PropertyChanged, o => o.Album);
        }
        #endregion
    }
}

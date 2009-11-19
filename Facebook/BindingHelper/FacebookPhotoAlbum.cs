using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;
using Facebook.Schema;
using Facebook.Utility;
using System;

namespace Facebook.BindingHelper
{

    /// <summary>
    /// Represents a photo album object
    /// </summary>
    [DataContract]
    public class FacebookPhotoAlbum : album, INotifyPropertyChanged, IEquatable<FacebookPhotoAlbum>
    {
        FacebookPhotoCollection _photos;

        /// <summary>
        /// Initializes FacebookPhotoAlbum object
        /// </summary>
        public FacebookPhotoAlbum()
        {
        }

        /// <summary>
        /// Initializes FacebookPhotoAlbum object from underlying album object
        /// </summary>
        /// <param name="a">Album object</param>
        public FacebookPhotoAlbum(album a)
        {
            aid = a.aid;
            cover_pid = a.cover_pid;
            owner = a.owner;
            name = a.name;
            created = a.created;
            modified = a.modified;
            description = a.description;
            location = a.location;
            link = a.link;
            size = a.size;
            visible = a.visible;
        }

        ///<summary>
        /// Is the album the owners
        ///</summary>
        public bool IsMyProfilePicturesAlbum
        {
            get { return (-3 & 0xffffffff) + (owner << 32) == long.Parse(aid); }

        }
        /// <summary>
        /// Photos collection in this album
        /// </summary>
        public FacebookPhotoCollection Photos
        {
            get
            {
                lock (this)
                {
                    if (_photos == null)
                    {
                        _photos = BindingManager.Instance.GetPhotos(aid);
                    }
                }
                return _photos;
            }
        }


        /// <summary>
        /// FacebookContact object for owner of this album
        /// </summary>
        public FacebookContact Owner
        {
            get
            {
                FacebookContact contact = BindingManager.Instance.GetUserFromCache(owner);
                if (contact == null)
                {
                    FacebookContactCollection fcc = BindingManager.Instance.Friends;
                    fcc.CollectionChanged += this.users_CollectionChanged;
                }
                return contact;
            }
        }

        /// <summary>
        /// The name of the user who owns this photo album
        /// </summary>
        public string OwnerName
        {
            get
            {
                FacebookContact contact = BindingManager.Instance.GetUserFromCache(OwnerId);
                if (contact != null)
                {
                    return contact.Name;
                }

                FacebookProfile profile = BindingManager.Instance.GetProfileFromCache(OwnerId);
                if (profile != null)
                {
                    return profile.Name;
                }
                return null;
            }
        }

        FacebookPhoto _coverPhoto;
        /// <summary>
        /// Coverphoto object
        /// </summary>
        [DataMember]
        public FacebookPhoto CoverPhoto
        {
            get
            {
                return _coverPhoto;
            }

            internal set
            {
                _coverPhoto = value;
                this.NotifyPropertyChanged( PropertyChanged,o=>o.CoverPhoto);
            }
        }
        /// <summary>
        /// Album id
        /// </summary>
        [DataMember(Name = "aid")]
        public string AlbumId
        {
            get{ return this.aid;}
            internal set { this.aid = value; }
        }

        /// <summary>
        /// Cover photo id
        /// </summary>
        [DataMember(Name = "cover_pid")]
        public string CoverPid
        {
            get { return this.cover_pid; }
            set { this.cover_pid = value; }
        }

        /// <summary>
        /// Owner id
        /// </summary>
        [DataMember(Name = "owner")]
        public long OwnerId
        {
            get { return this.owner; }
            set { this.owner = value; }
        }

        /// <summary>
        /// Name of album
        /// </summary>
        [DataMember(Name = "name")]
        public string Name
        {
            get { return this.name; }
            internal set { this.name = value; }
        }

        /// <summary>
        /// Created time in facebook format
        /// </summary>
        [DataMember(Name = "created")]
        internal long Created
        {
            get {return this.created;}
            set { this.created = Created; }
        }

        /// <summary>
        /// Created time of album
        /// </summary>
        public DateTime CreatedTime
        {
            get
            {
                return DateHelper.ConvertUnixTimeToDateTime(this.Created);
            }
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
        /// Modified time of album
        /// </summary>
        public DateTime ModifiedTime
        {
            get
            {
                return DateHelper.ConvertUnixTimeToDateTime(this.Modified);
            }
        }

        /// <summary>
        /// Description of album
        /// </summary>
        [DataMember(Name = "description")]
        public string Description
        {
            get { return this.description; }
            internal set { this.description = value; }
        }

        /// <summary>
        /// Location information
        /// </summary>
        [DataMember(Name = "location")]
        public string Location
        {
            get { return this.location; }
            internal set { this.location = value; }
        }

        /// <summary>
        /// Link information
        /// </summary>
        [DataMember(Name = "link")]
        public string Link
        {
            get { return this.link; }
            internal set {this.link = value;}
        }

        /// <summary>
        /// Number of photos in the album
        /// </summary>
        [DataMember(Name = "size")]
        public int Size
        {
            get { return this.size; }
            internal set {this.size = value;}
        }

        /// <summary>
        /// Visible information
        /// </summary>
        [DataMember(Name = "visible")]
        public string Visible
        {
            get { return this.visible; }
            internal set { this.visible = value; }
        }

        #region public methods

        /// <summary>
        /// Makes an async call to refresh photos information for this album
        /// </summary>
        public void Refresh()
        {
            FacebookPhotoCollection photos = BindingManager.Instance.GetPhotos(AlbumId);
            photos.CollectionChanged += photos_CollectionChanged;

            FacebookPhotoAlbumCollection albums = BindingManager.Instance.GetAlbums(new [] {AlbumId}, false);
            if (albums.Count == 1)
                MergeCoverAlbum(albums[0]);

        }


        void MergeCoverAlbum(FacebookPhotoAlbum album)
        {
            CoverPid = album.CoverPid;
            foreach (var photo in Photos)
            {
                if (photo.PhotoId == CoverPid)
                {
                    CoverPhoto = photo;
                    break;
                }
            }
            this.NotifyPropertyChanged( PropertyChanged,o=>o.CoverPid);
        }

        #endregion


        #region INotifyPropertyChanged Members

        /// <summary>
        /// Proeprty change event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        #region Helpers

        void photos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            FacebookPhotoCollection photos = (FacebookPhotoCollection)sender;
            photos.CollectionChanged -= photos_CollectionChanged;
            this.Photos.Merge(photos);

            this.NotifyPropertyChanged( PropertyChanged,o=>o.Size);
        }

        void users_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            FacebookContactCollection fcc = sender as FacebookContactCollection;
            fcc.CollectionChanged -= this.users_CollectionChanged;

            // let's try again
            if (BindingManager.Instance.GetUserFromCache(OwnerId) != null)
            {
                this.NotifyPropertyChanged(PropertyChanged, o => o.Owner);
            }
        }

        #endregion

        #region IEquatable<FacebookPhotoAlbum> Members

        /// <summary>
        /// Overridden operator to compare this album to another album
        /// </summary>
        public bool Equals(FacebookPhotoAlbum other)
        {
            return (other != null && this.AlbumId == other.AlbumId);
        }

        #endregion
    }
}

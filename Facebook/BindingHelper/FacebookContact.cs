using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Utility;

namespace Facebook.BindingHelper
{
    /// <summary>
    /// Represents user collection
    /// </summary>
    public sealed class FacebookContactCollection : FacebookDataCollection<FacebookContact>
    {
        /// <summary>
        /// Initializes FacebookContactCollection object
        /// </summary>
        public FacebookContactCollection()
            : base()
        {
        }

        /// <summary>
        /// Initializes FacebookContactCollection from contacts list
        /// </summary>
        /// <param name="contacts">list of comments</param>
        public FacebookContactCollection(IEnumerable<FacebookContact> contacts)
            : base(contacts)
        {
        }
    }

    /// <summary>
    /// Contains user information
    /// </summary>
    [DataContract]
    public sealed class FacebookContact : user, IEquatable<FacebookContact>
    {
        internal const int Stream_PerUserLimit = 15;
        /// <summary>
        /// Constructor
        /// </summary>
        public FacebookContact() { }
        /// <summary>
        /// Constructor that hydrates this object for object deserialized from xml
        /// </summary>
        public FacebookContact(user user)
        {
            this.about_me = user.about_me;
            this.activities = user.activities;
            this.affiliations = user.affiliations;
            this.birthday = user.birthday;
            this.books = user.books;
            this.current_location = user.current_location;
            this.education_history = user.education_history;
            this.email_hashes = user.email_hashes;
            this.first_name = user.first_name;
            this.has_added_app = user.has_added_app;
            this.has_added_appSpecified = user.has_added_appSpecified;
            this.hometown_location = user.hometown_location;
            this.hs_info = user.hs_info;
            this.interests = user.interests;
            this.is_app_user = user.is_app_user;
            this.is_app_userSpecified = user.is_app_userSpecified;
            this.last_name = user.last_name;
            this.locale = user.locale;
            this.meeting_for = user.meeting_for;
            this.meeting_sex = user.meeting_sex;
            this.movies = user.movies;
            this.music = user.music;
            this.name = user.name;
            this.notes_count = user.notes_count;
            this.notes_countSpecified = user.notes_countSpecified;
            this.pic = user.pic;
            this.pic_big = user.pic_big;
            this.pic_small = user.pic_small;
            this.pic_square = user.pic_square;
            this.political = user.political;
            this.profile_update_time = user.profile_update_time;
            this.profile_update_timeSpecified = user.profile_update_timeSpecified;
            this.quotes = user.quotes;
            this.relationship_status = user.relationship_status;
            this.religion = user.religion;
            this.sex = user.sex;
            this.significant_other_id = user.significant_other_id;
            this.significant_other_idSpecified = user.significant_other_idSpecified;
            this.status = user.status;
            this.timezone = user.timezone;
            this.timezoneSpecified = user.timezoneSpecified;
            this.tv = user.tv;
            this.uid = user.uid;
            this.uidSpecified = user.uidSpecified;
            this.wall_count = user.wall_count;
            this.wall_countSpecified = user.wall_countSpecified;
            this.work_history = user.work_history;
        }

        private FacebookPhotoCollection _photosOf;
        /// <summary>
        /// Gets all photos tagged with this user
        /// </summary>
        public FacebookPhotoCollection PhotosOf
        {
            get
            {
                lock (this)
                {
                    if (_photosOf == null)
                    {
                        _photosOf = BindingManager.Instance.GetPhotosOf(this.uid.Value);
                    }
                }

                return _photosOf;
            }
        }

        private FacebookPhotoCollection _photosBy;
        /// <summary>
        /// Gets top photos added by this user
        /// </summary>
        public FacebookPhotoCollection PhotosBy
        {
            get
            {
                lock (this)
                {
                    if (_photosBy == null)
                    {
                        _photosBy = BindingManager.Instance.GetPhotosBy(this.uid.Value);
                    }
                }

                return _photosBy;
            }
        }


        private FacebookPhotoAlbumCollection _albums;
        /// <summary>
        /// Gets all photo albums created by this user
        /// </summary>
        public FacebookPhotoAlbumCollection PhotoAlbums
        {
            get
            {
                lock (this)
                {
                    if (_albums == null)
                    {
                        _albums = BindingManager.Instance.GetAlbums(this.uid.Value);
                    }
                }
                return _albums;
            }
        }

        private ActivityPostCollection _posts;
        /// <summary>
        /// Gets all posting by this user
        /// </summary>
        public ActivityPostCollection RecentActivity
        {
            get
            {
                lock (this)
                {
                    if (_posts == null)
                    {
                        _posts = BindingManager.Instance.GetStream(new List<long>() { this.uid.Value }, null, null, Stream_PerUserLimit, null);
                    }
                }
                return _posts;
            }
        }

        FacebookImage _image;
        /// <summary>
        /// returns a facebook image object for user profile picture
        /// </summary>
        public FacebookImage Image
        {
            get
            {
                if (_image == null)
                {
                    _image = new FacebookImage(this.pic, this.pic_big, this.pic_small, this.pic_square);
                }
                return _image;
            }
        }
        /// <summary>
        /// About me information
        /// </summary>
        [DataMember(Name = "about_me")]
        public string AboutMe
        {
            get { return this.about_me; }
            set { this.about_me = AboutMe; }
        }

        /// <summary>
        /// Activities
        /// </summary>
        [DataMember(Name = "activities")]
        public string Activities
        {
            get { return this.activities; }
            set { this.activities = value; }
        }

        /// <summary>
        /// Birthday of user
        /// </summary>
        [DataMember(Name = "birthday")]
        public string Birthday
        {
            get{ return this.birthday;}
            set { this.birthday = value; }
        }

        /// <summary>
        /// Books information
        /// </summary>
        [DataMember(Name = "books")]
        public string Books
        {
            get { return this.books; }
            set {this.books = value;}
        }

        /// <summary>
        /// current location
        /// </summary>
        [DataMember(Name = "current_location")]
        public location CurrentLocation
        {
            get{return this.current_location;}
            set{this.current_location = value;}
        }

        /// <summary>
        /// Hometown location
        /// </summary>
        [DataMember(Name = "hometown_location")]
        public location HomeTownLocation
        {
            get {return this.hometown_location;}
            set { this.hometown_location = value; }
        }

        /// <summary>
        /// Firstname
        /// </summary>
        [DataMember(Name = "first_name")]
        public string FirstName
        {
            get { return this.first_name; }
            set {this.first_name = value;}
        }

        /// <summary>
        /// Last name
        /// </summary>
        [DataMember(Name = "last_name")]
        public string LastName
        {
            get { return this.last_name; }
            set {this.last_name = value;}
        }

        /// <summary>
        /// Full name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Interests
        /// </summary>
        [DataMember(Name = "interests")]
        public string Interests
        {
            get { return this.interests; }
            set { this.interests = value; }
        }

        /// <summary>
        /// Interests
        /// </summary>
        [DataMember(Name = "music")]
        public string Music
        {
            get { return this.music; }
            set { this.music = value; }
        }

        /// <summary>
        /// Interests
        /// </summary>
        [DataMember(Name = "movies")]
        public string Movies
        {
            get { return this.movies; }
            set { this.movies = value; }
        }

        /// <summary>
        /// Noted count for this user
        /// </summary>
        [DataMember(Name = "notes_count")]
        public int NotesCount
        {
            get{return this.notes_count.Value;}
            set { this.notes_count = value; }
        }

        /// <summary>
        /// Picture of user
        /// </summary>
        [DataMember(Name = "pic")]
        public string Picture
        {
            get { return this.pic; }
            set {this.pic = value;}
        }

        /// <summary>
        /// Big picture of the user
        /// </summary>
        [DataMember(Name = "pic_big")]
        public string PictureBig
        {
            get {return this.pic_big;}
            set{this.pic_big = value;}
        }

        /// <summary>
        /// Small picture of user
        /// </summary>
        [DataMember(Name = "pic_small")]
        public string PictureSmall
        {
            get {return this.pic_small;}
            set {this.pic_small = value;}
        }

        /// <summary>
        /// Square picture of user
        /// </summary>
        [DataMember(Name = "pic_square")]
        public string PictureSquare
        {
            get { return this.pic_square; }
            set { this.pic_square = value; }
        }

        /// <summary>
        /// Sex of user
        /// </summary>
        [DataMember(Name = "sex")]
        public string Sex
        {
            get { return this.sex; }
            set { this.sex = value; }
        }

        /// <summary>
        /// political info of user
        /// </summary>
        [DataMember(Name = "political")]
        public string Political
        {
            get { return this.political; }
            set { this.political = value; }
        }

        /// <summary>
        /// Profile updated time in facebook format
        /// </summary>
        [DataMember(Name = "profile_update_time")]
        internal long? profileupdatetime
        {
            get { return this.profile_update_time; }
            set { this.profile_update_time = value; }
        }

        /// <summary>
        /// Time profile was updated
        /// </summary>
        public DateTime ProfileUpdateTime
        {
            get
            {
                if (this.profileupdatetime != null)
                {
                    return DateHelper.ConvertUnixTimeToDateTime(this.profileupdatetime.Value);
                }
                return new DateTime();
            }
        }

        /// <summary>
        /// User id
        /// </summary>
        [DataMember(Name = "uid")]
        public long UserId
        {
            get { return this.uid.Value; }
            set { this.uid = value; }
        }

        /// <summary>
        /// wall count
        /// </summary>
        [DataMember(Name = "wall_count")]
        public int WallCount
        {
            get{return this.wall_count.Value;}
            set {this.wall_count = value;}
        }

        /// <summary>
        /// quotes
        /// </summary>
        [DataMember(Name = "quotes")]
        public string Quotes
        {
            get { return this.quotes; }
            set{this.quotes = value;}
        }

        /// <summary>
        /// User status information
        /// </summary>
        [DataMember(Name = "status")]
        public user_status Status
        {
            get { return this.status; }
            set {this.status = value;}
        }

        /// <summary>
        /// RelationshipStatus
        /// </summary>
        [DataMember(Name = "relationship_status")]
        public string RelationshipStatus
        {
            get { return this.relationship_status; }
            set { this.relationship_status = value; }
        }

        /// <summary>
        /// religion
        /// </summary>
        [DataMember(Name = "religion")]
        public string Religion
        {
            get { return this.religion; }
            set { this.religion = value; }
        }

        /// <summary>
        /// locale
        /// </summary>
        [DataMember(Name = "locale")]
        public string Locale
        {
            get { return this.locale; }
            set { this.locale = value; }
        }

        /// <summary>
        /// significant_other_id
        /// </summary>
        [DataMember(Name = "significant_other_id")]
        public long SignificantOtherId
        {
            get { return this.significant_other_id.Value; }
            set { this.significant_other_id = value; }
        }


        #region IEquatable<FacebookContact> Members

        /// <summary>
        /// Operator overload to compare this instance against another FacebookContact
        /// </summary>
        public bool Equals(FacebookContact other)
        {
            return (other != null && this.UserId == other.UserId);
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook.Schema;
using System.Runtime.Serialization;
using Facebook.Utility;

namespace Facebook.BindingHelper
{
    /// <summary>
    /// Represents facebook profile information
    /// </summary>
    public class FacebookProfile : profile, IEquatable<FacebookProfile>
    {
        FacebookImage _image;

        /// <summary>
        /// Constructor
        /// </summary>
        public FacebookProfile() { }
        /// <summary>
        /// Constructor used to populate this object from an xml deserialized version of same data
        /// </summary>
        public FacebookProfile(profile p)
        {
            this.id = p.id;
            this.name = p.name;
            this.pic_square = p.pic_square;
            this.url = p.url;
            this.type = p.type;
        }
        /// <summary>
        /// FacebookImage object for all sizes of profile image.
        /// </summary>
        public FacebookImage Image 
        {
            get
            {
                lock (this)
                {
                    if (_image == null)
                    {
                        _image = new FacebookImage(this.Picture, this.PictureBig, this.PictureSmall, this.PictureSquare);
                    }
                    return _image;
                }
            }
        }

        /// <summary>
        /// UserId of user
        /// </summary>
        [DataMember(Name = "id")]
        public long UserId
        {
            get {return this.id;}
            set {this.id = value;}
        }

        /// <summary>
        /// Url on facebook site for this user
        /// </summary>
        [DataMember(Name = "url")]
        public string Url
        {
            get { return this.url; }
            set {this.url = value;}
        }

        /// <summary>
        /// Name of user
        /// </summary>
        [DataMember(Name = "name")]
        public string Name
        {
            get { return this.name; }
            set {this.name = value;}
        }

        /// <summary>
        /// Url of users picture
        /// </summary>
        [DataMember(Name = "pic")]
        public string Picture
        {
            get { return this.pic_square; }
            internal set { this.pic_square = value; }
        }

        /// <summary>
        /// Url of users picture
        /// </summary>
        [DataMember(Name = "pic_square")]
        public string PictureSquare
        {
            get { return this.pic_square; }
            internal set { this.pic_square = value; }
        }

        /// <summary>
        /// Url of users picture
        /// </summary>
        [DataMember(Name = "pic_small")]
        public string PictureSmall
        {
            get { return this.pic_square; }
            internal set { this.pic_square = value; }
        }


        /// <summary>
        /// Url of users picture
        /// </summary>
        [DataMember(Name = "pic_big")]
        public string PictureBig
        {
            get { return this.pic_square; }
            internal set { this.pic_square = value; }
        }

        #region IEquatable<FacebookProfile> Members

        /// <summary>
        /// Overridden operator to compare to FacebookProfiles
        /// </summary>
        public bool Equals(FacebookProfile other)
        {
            return (other != null && this.UserId == other.UserId);
        }

        #endregion
    }
}

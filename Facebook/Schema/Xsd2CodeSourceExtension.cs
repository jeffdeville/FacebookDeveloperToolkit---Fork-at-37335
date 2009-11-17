
using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Serialization;
using Facebook.Utility;

#if !SILVERLIGHT
using System.Drawing;
#endif

namespace Facebook.Schema
{

#pragma warning disable 1591 // Disable "Missing XML comment" warnings for all code in this file


    [System.Xml.Serialization.XmlRootAttribute("stream_dataPosts_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
    public class stream_dataPosts_response : stream_dataPosts
    { }
    [System.Xml.Serialization.XmlRootAttribute("stream_dataProfiles_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
    public class stream_dataProfiles_response : stream_dataProfiles
    { }
    [System.Xml.Serialization.XmlRootAttribute("permissions_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
    public class permissions_response
    { 
        public permissions permissions;
    }
    public class permissions
    {
        public bool read_stream;
        public bool status_update;
        public bool photo_upload;
        public bool publish_stream;
        public bool email;
        public bool create_listing;
        public bool offline_access;
        public bool rsvp_event;
        public bool sms;
        public bool video_upload;
        public bool create_event;
        public bool create_note;
        public bool share_item;
        public bool manage_mailbox;
        public bool read_mailbox;
    }

	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://api.facebook.com/1.0/")]
	public class request_argsLocalType
	{
		[System.Xml.Serialization.XmlAttribute]
		public bool list { get; set; }

		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool listSpecified { get; set; }

		[System.Xml.Serialization.XmlElementAttribute("arg")]
		public List<arg> arg { get; set; }
	}

	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://api.facebook.com/1.0/")]
	[System.Xml.Serialization.XmlRootAttribute("error_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class error_response
	{
		public int error_code {get; set;}
		public string error_msg {get; set;}

		public request_argsLocalType request_args {get; set;}
	}

	public partial class album
	{
		public DateTime created_date
		{
			get { return DateHelper.ConvertDoubleToDate(Convert.ToDouble(this.created)); }
		}
		public DateTime modified_date
		{
			get { return DateHelper.ConvertDoubleToDate(Convert.ToDouble(this.modified)); }
		}
	}
	public partial class photo_tag : IEquatable<photo_tag>
	{
		public DateTime created_date
		{
			get { return DateHelper.ConvertDoubleToDate(Convert.ToDouble(this.created)); }
		}

        #region IEquatable<photo_tag> Members

        public bool Equals(photo_tag other)
        {
            if (this.pid == other.pid && this.xcoord == other.xcoord && this.ycoord == other.ycoord && this.text == other.text)
                return true;
            else
                return false;

        }

        #endregion
    }
	public partial class photo
	{
#if !SILVERLIGHT
        private Image _picture;
		private Image _pictureBig;
		private Image _pictureSmall;

		public Image picture
		{
			get
			{
				if (this.src == null)
				{
				    return ImageHelper.MissingPicture;
				}
				else if (_picture == null)
				{
					WebClient webClient = new WebClient();
					Byte[] pictureBytes = webClient.DownloadData(this.src);
					_picture = ImageHelper.ConvertBytesToImage(pictureBytes);
					return _picture;
				}
				else
				{
					return _picture;
				}
			}
		}
		public Image picture_big
		{
			get
			{
				if (this.src_big == null)
				{
                    return ImageHelper.MissingPicture;
				}
				else if (_pictureBig == null)
				{
					WebClient webClient = new WebClient();
					Byte[] pictureBytes = webClient.DownloadData(this.src_big);
					_pictureBig = ImageHelper.ConvertBytesToImage(pictureBytes);
					return _pictureBig;
				}
				else
				{
					return _pictureBig;
				}
			}
		}
		public Image picture_small
		{
			get
			{
				if (this.src_small == null)
				{
                    return ImageHelper.MissingPicture;
				}
				else if (_pictureSmall == null)
				{
					WebClient webClient = new WebClient();
					Byte[] pictureBytes = webClient.DownloadData(this.src_small);
					_pictureSmall = ImageHelper.ConvertBytesToImage(pictureBytes);
					return _pictureSmall;
				}
				else
				{
					return _pictureSmall;
				}
			}
		}

#endif

		public DateTime created_date
		{
			get { return DateHelper.ConvertDoubleToDate(Convert.ToDouble(this.created)); }
		}
	}
	public partial class facebookevent
	{
#if !SILVERLIGHT
        
        private Image _picture;
		private Image _pictureBig;
		private Image _pictureSmall;

		public Image picture
		{
			get
			{
				if (this.pic == null)
				{
                    return ImageHelper.MissingPicture;
				}
				else if (_picture == null)
				{
					WebClient webClient = new WebClient();
					Byte[] pictureBytes = webClient.DownloadData(this.pic);
					_picture = ImageHelper.ConvertBytesToImage(pictureBytes);
					return _picture;
				}
				else
				{
					return _picture;
				}
			}
		}
		public Image picture_big
		{
			get
			{
				if (this.pic_big == null)
				{
                    return ImageHelper.MissingPicture;
				}
				else if (_pictureBig == null)
				{
					WebClient webClient = new WebClient();
					Byte[] pictureBytes = webClient.DownloadData(this.pic_big);
					_pictureBig = ImageHelper.ConvertBytesToImage(pictureBytes);
					return _pictureBig;
				}
				else
				{
					return _pictureBig;
				}
			}
		}
		public Image picture_small
		{
			get
			{
				if (this.pic_small == null)
				{
                    return ImageHelper.MissingPicture;
				}
				else if (_pictureSmall == null)
				{
					WebClient webClient = new WebClient();
					Byte[] pictureBytes = webClient.DownloadData(this.pic_small);
					_pictureSmall = ImageHelper.ConvertBytesToImage(pictureBytes);
					return _pictureSmall;
				}
				else
				{
					return _pictureSmall;
				}
			}
		}

#endif

		public DateTime start_date
		{
			get { return DateHelper.ConvertDoubleToDate(Convert.ToDouble(this.start_time)); }
			set { this.start_time = (int)DateHelper.ConvertDateToDouble(value); }
		}
		public DateTime end_date
		{
			get { return DateHelper.ConvertDoubleToDate(Convert.ToDouble(this.end_time)); }
			set { this.end_time = (int)DateHelper.ConvertDateToDouble(value); }
		}
		public DateTime update_date
		{
			get { return DateHelper.ConvertDoubleToDate(Convert.ToDouble(this.update_time)); }
		}
	}
    public partial class stream_post
    {
        private string filter_keyField;
        public string filter_key
        {
            get
            {
                return this.filter_keyField;
            }
            set
            {
                this.filter_keyField = value;
            }
        }

    }
	public partial class group
	{
#if !SILVERLIGHT

		private Image _picture;
		private Image _pictureBig;
		private Image _pictureSmall;
		public Image picture
		{
			get
			{
				if (this.pic == null)
				{
                    return ImageHelper.MissingPicture;
				}
				else if (_picture == null)
				{
					WebClient webClient = new WebClient();
					Byte[] pictureBytes = webClient.DownloadData(this.pic);
					_picture = ImageHelper.ConvertBytesToImage(pictureBytes);
					return _picture;
				}
				else
				{
					return _picture;
				}
			}
		}
		public Image picture_big
		{
			get
			{
				if (this.pic_big == null)
				{
                    return ImageHelper.MissingPicture;
				}
				else if (_pictureBig == null)
				{
					WebClient webClient = new WebClient();
					Byte[] pictureBytes = webClient.DownloadData(this.pic_big);
					_pictureBig = ImageHelper.ConvertBytesToImage(pictureBytes);
					return _pictureBig;
				}
				else
				{
					return _pictureBig;
				}
			}
		}
		public Image picture_small
		{
			get
			{
				if (this.pic_small == null)
				{
                    return ImageHelper.MissingPicture;
				}
				else if (_pictureSmall == null)
				{
					WebClient webClient = new WebClient();
					Byte[] pictureBytes = webClient.DownloadData(this.pic_small);
					_pictureSmall = ImageHelper.ConvertBytesToImage(pictureBytes);
					return _pictureSmall;
				}
				else
				{
					return _pictureSmall;
				}
			}
		}

#endif

		public DateTime update_date
		{
			get { return DateHelper.ConvertDoubleToDate(Convert.ToDouble(this.update_time)); }
		}
	}
	public partial class user
	{
#if !SILVERLIGHT

		private Image _picture;
		private Image _pictureBig;
		private Image _pictureSmall;
		private Image _pictureSquare;
        public Image picture
		{
			get
			{
				if (this.pic == null)
				{
                    return ImageHelper.MissingPicture;

				}
				else if (_picture == null)
				{
					WebClient webClient = new WebClient();
					Byte[] pictureBytes = webClient.DownloadData(this.pic);
					_picture = ImageHelper.ConvertBytesToImage(pictureBytes);
					return _picture;
				}
				else
				{
					return _picture;
				}
			}
		}
		public Image picture_big
		{
			get
			{
				if (this.pic_big == null)
				{
                    return ImageHelper.MissingPicture;
				}
				else if (_pictureBig == null)
				{
					WebClient webClient = new WebClient();
					Byte[] pictureBytes = webClient.DownloadData(this.pic_big);
					_pictureBig = ImageHelper.ConvertBytesToImage(pictureBytes);
					return _pictureBig;
				}
				else
				{
					return _pictureBig;
				}
			}
		}
		public Image picture_small
		{
			get
			{
				if (this.pic_small == null)
				{
                    return ImageHelper.MissingPicture;
				}
				else if (_pictureSmall == null)
				{
					WebClient webClient = new WebClient();
					Byte[] pictureBytes = webClient.DownloadData(this.pic_small);
					_pictureSmall = ImageHelper.ConvertBytesToImage(pictureBytes);
					return _pictureSmall;
				}
				else
				{
					return _pictureSmall;
				}
			}
		}
		public Image picture_square
		{
			get
			{
				if (this.pic_square == null)
				{
                    return ImageHelper.MissingPicture;
				}
				else if (_pictureSquare == null)
				{
					WebClient webClient = new WebClient();
					Byte[] pictureBytes = webClient.DownloadData(this.pic_square);
					_pictureSquare = ImageHelper.ConvertBytesToImage(pictureBytes);
					return _pictureSquare;
				}
				else
				{
					return _pictureSquare;
				}
			}
		}

#endif
        private string profile_urlField;
        private string proxied_emailField;

		public DateTime birthday_date
		{
			get
			{
				//If we have a full date, it will come back as a double
				double dblBirthDay;
				if (Double.TryParse(this.birthday, out dblBirthDay))
					return DateHelper.ConvertDoubleToDate(dblBirthDay);

				//If the user just has their birthday without a year, append 1901 to it and try to parse it.
				try
				{
					var tempBirthday = this.birthday;
					tempBirthday = tempBirthday + " 1901";
					return DateTime.Parse(tempBirthday);

				}
				catch (Exception)
				{
					//If we couldn't get any date, return 
					return new DateTime(1900, 1, 1);
				}


			}

		}
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string proxied_email
        {
            get
            {
                return this.proxied_emailField;
            }
            set
            {
                this.proxied_emailField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string profile_url
        {
            get
            {
                return this.profile_urlField;
            }
            set
            {
                this.profile_urlField = value;
            }
        }


	}
	public class feed_image
	{
		/// <summary>
		/// The URL of an image to be displayed in the News Feed story.
		/// </summary>
		public string image_url { get; set; }

		/// <summary>
		/// The URL destination after a click on the image referenced by ImageURL.
		/// </summary>
		public string image_link_url { get; set; }

		/// <summary>
		/// constructor
		/// </summary>
		public feed_image(string image_url, string image_link_url)
		{
			this.image_url = image_url;
			this.image_link_url = image_link_url;
		}
	}


	#region Classes not generated by Xsd2Code

	public class BooleanResponse
	{
		[XmlText]
		public bool TypedValue { get; set; }
	}

	public class IntResponse
	{
		[XmlText]
		public int TypedValue { get; set; }
	}

	public class LongResponse
	{
		[XmlText]
		public long TypedValue { get; set; }
	}

	public class StringResponse
	{
		[XmlText]
		public string TypedValue { get; set; }
	}

	[XmlRootAttribute("admin_banUsers_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class admin_banUsers_response : BooleanResponse { }

	[XmlRootAttribute("admin_getAllocation_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class admin_getAllocation_response : IntResponse { }

	[XmlRootAttribute("admin_getAppProperties_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class admin_getAppProperties_response : StringResponse { }
	
	[XmlRootAttribute("admin_getRestrictionInfo_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class admin_getRestrictionInfo_response : StringResponse { }

	[XmlRootAttribute("admin_setAppProperties_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class admin_setAppProperties_response : BooleanResponse { }
	
	[XmlRootAttribute("admin_setRestrictionInfo_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class admin_setRestrictionInfo_response : BooleanResponse { }

	[XmlRootAttribute("admin_unbanUsers_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class admin_unbanUsers_response : BooleanResponse { }

	[XmlRootAttribute("application_getPublicInfo_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class application_getPublicInfo_response : app_info { }

	[XmlRootAttribute("auth_createToken_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class auth_createToken_response : StringResponse { }

	[XmlRootAttribute("auth_expireSession_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class auth_expireSession_response : BooleanResponse { }

	[XmlRootAttribute("auth_getSession_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class auth_getSession_response :	session_info { }

	[XmlRootAttribute("auth_promoteSession_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class auth_promoteSession_response : StringResponse { }

	[XmlRootAttribute("auth_revokeAuthorization_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class auth_revokeAuthorization_response : BooleanResponse { }

	[XmlRootAttribute("auth_revokeExtendedPermission_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class auth_revokeExtendedPermission_response : BooleanResponse { }

	[XmlRootAttribute("comments_add_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class comments_add_response : IntResponse { }

	[XmlRootAttribute("comments_remove_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class comments_remove_response : BooleanResponse { }

	[XmlRootAttribute("connect_getUnconnectedFriendsCount_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class connect_getUnconnectedFriendsCount_response : IntResponse { }

	[XmlRootAttribute("data_createObject_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_createObject_response : LongResponse { }

	[XmlRootAttribute("data_createObjectType_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_createObjectType_response : BooleanResponse { }

	[XmlRootAttribute("data_defineAssociation_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_defineAssociation_response : BooleanResponse { }

	[XmlRootAttribute("data_defineObjectProperty_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_defineObjectProperty_response : BooleanResponse { }

	[XmlRootAttribute("data_deleteObject_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_deleteObject_response : BooleanResponse { }

	[XmlRootAttribute("data_deleteObjects_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_deleteObjects_response : BooleanResponse { }

	[XmlRootAttribute("data_dropObjectType_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_dropObjectType_response : BooleanResponse { }

	[XmlRoot("data_getAssociatedObjectCount_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_getAssociatedObjectCount_response : IntResponse { }

	[XmlRootAttribute("data_getAssociationDefinition_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_getAssociationDefinition_response : object_assoc_info { }

	[XmlRootAttribute("data_getHashValue_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_getHashValue_response : StringResponse { }

	[XmlRootAttribute("data_getObject_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_getObject_response : container { }

	[XmlRootAttribute("data_getObjectProperty_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_getObjectProperty_response : StringResponse { }

	[XmlRootAttribute("data_getUserPreference_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_getUserPreference_response : StringResponse { }

	[XmlRootAttribute("data_incHashValue_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_incHashValue_response : IntResponse { }

	[XmlRootAttribute("data_removeAssociatedObjects_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_removeAssociatedObjects_response : BooleanResponse { }
	
	[XmlRootAttribute("data_removeAssociation_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_removeAssociation_response : BooleanResponse { }

	[XmlRootAttribute("data_removeAssociations_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_removeAssociations_response : BooleanResponse { }

	[XmlRootAttribute("data_removeHashKey_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_removeHashKey_response : BooleanResponse { }

	[XmlRootAttribute("data_removeHashKeys_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_removeHashKeys_response : BooleanResponse { }
	
	[XmlRootAttribute("data_renameAssociation_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_renameAssociation_response : BooleanResponse { }

	[XmlRootAttribute("data_renameObjectProperty_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_renameObjectProperty_response : BooleanResponse { }

	[XmlRootAttribute("data_renameObjectType_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_renameObjectType_response : BooleanResponse { }

	[XmlRootAttribute("data_setAssociation_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_setAssociation_response : BooleanResponse { }

	[XmlRootAttribute("data_setAssociations_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_setAssociations_response : BooleanResponse { }

	[XmlRootAttribute("data_setCookie_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_setCookie_response : BooleanResponse { }

	[XmlRootAttribute("data_setHashValue_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_setHashValue_response : LongResponse { }

	[XmlRootAttribute("data_setObjectProperty_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_setObjectProperty_response : BooleanResponse { }

	[XmlRootAttribute("data_setUserPreference_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_setUserPreference_response : BooleanResponse { }

	[XmlRootAttribute("data_setUserPreferences_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_setUserPreferences_response : BooleanResponse { }

	[XmlRootAttribute("data_undefineAssociation_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_undefineAssociation_response : BooleanResponse { }

	[XmlRootAttribute("data_undefineObjectProperty_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_undefineObjectProperty_response : BooleanResponse { }

	[XmlRootAttribute("data_updateObject_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class data_updateObject_response : BooleanResponse { }

	[XmlRootAttribute("events_cancel_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class events_cancel_response : BooleanResponse { }

	[XmlRootAttribute("events_create_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class events_create_response : LongResponse { }

	[XmlRootAttribute("events_edit_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class events_edit_response : BooleanResponse { }

	[XmlRootAttribute("events_getMembers_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class events_getMembers_response : event_members { }

	[XmlRootAttribute("events_rsvp_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class events_rsvp_response : BooleanResponse { }

	[XmlRootAttribute("fbml_deleteCustomTags_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class fbml_deleteCustomTags_response : BooleanResponse { }

	[XmlRootAttribute("fbml_refreshImgSrc_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class fbml_refreshImgSrc_response : BooleanResponse { }

	[XmlRootAttribute("fbml_refreshRefUrl_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class fbml_refreshRefUrl_response : BooleanResponse { }

	[XmlRootAttribute("fbml_registerCustomTags_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class fbml_registerCustomTags_response : IntResponse { }

	[XmlRootAttribute("fbml_setRefHandle_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class fbml_setRefHandle_response : BooleanResponse { }

	[XmlRootAttribute("fbml_uploadNativeStrings_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class fbml_uploadNativeStrings_response : BooleanResponse { }

	[XmlRootAttribute("feed_deactivateTemplateBundleByID_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class feed_deactivateTemplateBundleByID_response : BooleanResponse { }

	[XmlRootAttribute("feed_getRegisteredTemplateBundleByID_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class feed_getRegisteredTemplateBundleByID_response : template_bundle { }

	[XmlRootAttribute("feed_publishTemplatizedAction_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class feed_publishTemplatizedAction_response : BooleanResponse { }

	[XmlRootAttribute("feed_registerTemplateBundle_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class feed_registerTemplateBundle_response : LongResponse { }

	[XmlRootAttribute("groups_getMembers_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class groups_getMembers_response : group_members { }

    [XmlRootAttribute("intl_uploadNativeStrings_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
    public class intl_uploadNativeStrings_response : LongResponse { }
    
    [XmlRootAttribute("links_post_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class links_post_response : LongResponse { }

	[XmlRootAttribute("liveMessage_send_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class liveMessage_send_response : BooleanResponse { }

	[XmlRootAttribute("marketplace_createListing_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class marketplace_createListing_response : LongResponse { }

	[XmlRootAttribute("marketplace_removeListing_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class marketplace_removeListing_response : BooleanResponse { }

	[XmlRootAttribute("notes_create_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class notes_create_response : LongResponse { }

	[XmlRootAttribute("notes_delete_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class notes_delete_response : BooleanResponse { }

	[XmlRootAttribute("notes_edit_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class notes_edit_response : BooleanResponse { }

	[XmlRootAttribute("notifications_get_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class notifications_get_response : notifications { }

	[XmlRootAttribute("notifications_send_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class notifications_send_response : StringResponse { }

	[XmlRootAttribute("notifications_sendEmail_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class notifications_sendEmail_response : StringResponse { }

	[XmlRootAttribute("pages_isAdmin_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class pages_isAdmin_response : BooleanResponse { }

	[XmlRootAttribute("pages_isAppAdded_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class pages_isAppAdded_response : BooleanResponse { }

	[XmlRootAttribute("pages_isFan_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class pages_isFan_response : BooleanResponse { }

	[XmlRootAttribute("permissions_grantApiAccess_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class permissions_grantApiAccess_response : BooleanResponse { }

	[XmlRootAttribute("permissions_revokeApiAccess_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class permissions_revokeApiAccess_response : BooleanResponse { }

	[XmlRootAttribute("photos_addTag_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class photos_addTag_response : BooleanResponse { }

	[XmlRootAttribute("photos_createAlbum_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
    public class notifications_markRead_response : BooleanResponse { }

    [XmlRootAttribute("notifications_GetList_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
    public class notifications_GetList_response : notification_data { }

    [XmlRootAttribute("photos_createAlbum_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
    public class photos_createAlbum_response : album { }

	[XmlRootAttribute("photos_upload_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class photos_upload_response : photo { }

	[XmlRootAttribute("profile_getFBML_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class profile_getFBML_response : StringResponse { }

	[XmlRootAttribute("profile_getInfo_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class profile_getInfo_response : user_info { }

	[XmlRootAttribute("profile_setFBML_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class profile_setFBML_response : BooleanResponse { }

	[XmlRootAttribute("profile_setInfo_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class profile_setInfo_response : BooleanResponse { }

	[XmlRootAttribute("profile_setInfoOptions_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class profile_setInfoOptions_response : BooleanResponse { }

	[XmlRootAttribute("status_set_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class status_set_response : BooleanResponse { }

	[XmlRootAttribute("stream_addComment_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class stream_addComment_response : StringResponse { }

	[XmlRootAttribute("stream_addLike_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class stream_addLike_response : BooleanResponse { }

	[XmlRootAttribute("stream_get_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class stream_get_response : stream_data { }

	[XmlRootAttribute("stream_publish_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class stream_publish_response : StringResponse { }

	[XmlRootAttribute("stream_remove_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class stream_remove_response : BooleanResponse { }

	[XmlRootAttribute("stream_removeComment_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class stream_removeComment_response : BooleanResponse { }

	[XmlRootAttribute("stream_removeLike_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class stream_removeLike_response : BooleanResponse { }

	[XmlRootAttribute("users_getLoggedInUser_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class users_getLoggedInUser_response : LongResponse { }

	[XmlRootAttribute("users_hasAppPermission_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class users_hasAppPermission_response : BooleanResponse { }

	[XmlRootAttribute("users_isAppUser_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class users_isAppUser_response : BooleanResponse { }

	[XmlRootAttribute("users_isVerified_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class users_isVerified_response : BooleanResponse { }

	[XmlRootAttribute("users_setStatus_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class users_setStatus_response : BooleanResponse { }

	[XmlRootAttribute("video_getUploadLimits_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class video_getUploadLimits_response : video_limits { }

	[XmlRootAttribute("video_upload_response", Namespace = "http://api.facebook.com/1.0/", IsNullable = false)]
	public class video_upload_response : video { }

	#endregion

#pragma warning restore 1591

}

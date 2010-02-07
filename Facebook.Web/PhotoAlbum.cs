using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook.Schema;
using Facebook.Rest;
using Facebook.Utility;


namespace Facebook.Web
{
	/// <summary>
	/// PhotoAlbum web control to show the user's photo albums and let them browser through the pictures.
	/// </summary>
	[ToolboxData("<{0}:PhotoAlbum runat=server></{0}:PhotoAlbum>")]
	public class PhotoAlbum : WebControl, IPostBackEventHandler
	{
		private const string HEADER_DIV = "photoAlbumHeader";
		private const string INNER_DIV = "photo";
		private const int MAX_ALBU_LENGTH = 25;
		private const string OUTER_DIV = "photoAlbum";
		private const string PHOTO_LABEL_DIV = "photoAlbumLabel";

		private const string PHOTO_NAV_NEXT = "next";
		private const string PHOTO_NAV_PREVIOUS = "previous";

		private const string VIEWSTATE_ALBUMS = "albums";
		private const string VIEWSTATE_CURRENT_ALBUM = "currentAlbum";
		private const string VIEWSTATE_CURRENT_IMAGE = "currentImage";
		private const string VIEWSTATE_PHOTOS = "photos";
		private const string VIEWSTATE_USERID = "userId";
        private IList<album> _albums;
		private int _currentAlbumIndex;
		private int _currentImageIndex;
        private Api _facebookApi;
		private IList<photo> _photos;
		private int _userId;
		private bool _useViewState = true;

		/// <summary>
		/// A flag indicating whether the control should store its state in ViewState.  By default this is
		/// set to True.  If this is set to False, the host page needs to take care of keeping track of state
		/// and setting the UserProfile control's User Property with each Postback. 
		/// </summary>
		public bool UseViewState
		{
			get { return _useViewState; }
			set { _useViewState = value; }
		}

		/// <summary>
		/// An insance of the Facebook Api.  We use this to look up the albums and photos. 
		/// </summary>
		public Api FacebookApi
		{
			get { return _facebookApi; }
            set { _facebookApi = value; }
		}

		/// <summary>
		/// The UserId for the account whose photo albums we're browsing.
		/// </summary>
		public int UserId
		{
			get { return _userId; }
			set
			{
				_userId = value;
				ViewState[VIEWSTATE_USERID] = _userId;
			}
		}

		private IList<album> Albums
		{
			set
			{
				_albums = value;
				if (UseViewState)
				{
					ViewState[VIEWSTATE_ALBUMS] = _albums.Select(album => Utilities.SerializeToXml<album>(album)).ToList();
				}
			}
		}

        private IList<photo> Photos
		{
			set
			{
				_photos = value;
				if (UseViewState)
				{
					ViewState[VIEWSTATE_PHOTOS] = _photos.Select(photo => Utilities.SerializeToXml<photo>(photo)).ToList();
				}
				ResetImageIndex();
			}
		}

		#region IPostBackEventHandler Members

		/// <summary>
		/// Called when a Postback occurs.
		/// </summary>
		/// <param name="eventArgument">An indicator that starts with "album" followed by the album ID that
		/// the user selected from the drop down, or the string "previous" or "next" in the case where the 
		/// user clicked one of the navigation links.</param>
		public void RaisePostBackEvent(string eventArgument)
		{
			if (Equals(_photos, null) || _photos.Count == 0)
			{
				return;
			}

			if (!Equals(ViewState[VIEWSTATE_CURRENT_IMAGE], null))
			{
				_currentImageIndex = (int) ViewState[VIEWSTATE_CURRENT_IMAGE];
			}

			if (!Equals(ViewState[VIEWSTATE_CURRENT_ALBUM], null))
			{
				_currentAlbumIndex = (int) ViewState[VIEWSTATE_CURRENT_ALBUM];
			}

			if (eventArgument.StartsWith("album"))
			{
				string albumId = eventArgument.Substring(5);
				_currentAlbumIndex = Convert.ToInt32(albumId);
				UpdateAlbumIndex();
				LoadPhotos();
			}
			else
			{
				if (eventArgument.Equals(PHOTO_NAV_NEXT))
				{
					MoveNextImage();
				}
				else if (eventArgument.Equals(PHOTO_NAV_PREVIOUS))
				{
					MovePreviousImage();
				}
			}
		}

		#endregion

		/// <summary>
		/// RenderContents is called by the ASP.Net framework when our control needs to render itself.  Here we
		/// will simply output our resulting HTML.
		/// </summary>
		/// <param name="output">The writer to which we will write our HTML</param>
		protected override void RenderContents(HtmlTextWriter output)
		{
			if (!Equals(_photos, null) && _photos.Count > 0)
			{
				//Write the outer DIV tag so the page author that uses this control can 
				//specify styles based on this DIV.
				output.AddAttribute(HtmlTextWriterAttribute.Id, OUTER_DIV);
				output.RenderBeginTag(HtmlTextWriterTag.Div);

				output.AddAttribute(HtmlTextWriterAttribute.Id, HEADER_DIV);
				output.RenderBeginTag(HtmlTextWriterTag.Div);
				output.Write("Photo Album");
				output.RenderEndTag(); //Header div

				//Build the drop down with all the photo albums.  Make sure to include an OnChange attribute
				//so we can Postback when the user changes between photo albums.
				output.AddAttribute(HtmlTextWriterAttribute.Id, "albumList");
				output.AddAttribute(HtmlTextWriterAttribute.Onchange,
				                    "javascript:__doPostBack('MyPhotoAlbum','album' + document.form1.albumList.selectedIndex)");

				//Write out each photo album.
				output.RenderBeginTag(HtmlTextWriterTag.Select);
				for (int x = 0; x < _albums.Count; x++)
				{
					output.AddAttribute(HtmlTextWriterAttribute.Value, x.ToString());
					if (x == _currentAlbumIndex)
					{
						output.AddAttribute(HtmlTextWriterAttribute.Selected, "SELECTED");
					}
					output.RenderBeginTag(HtmlTextWriterTag.Option);
					if (_albums[x].name.Length > MAX_ALBU_LENGTH)
					{
						output.Write(_albums[x].name.Substring(0, MAX_ALBU_LENGTH));
					}
					else
					{
						output.Write(_albums[x].name);
					}
					output.RenderEndTag(); //OPTION
				}
				output.RenderEndTag(); //SELECT

				//Write the inner DIV tag.  This enables finer control of any styles.
				output.AddAttribute(HtmlTextWriterAttribute.Id, INNER_DIV);
				output.RenderBeginTag(HtmlTextWriterTag.Div);

				//Write out the image
				output.AddAttribute(HtmlTextWriterAttribute.Src, GetImageUri(_currentImageIndex));
				output.RenderBeginTag(HtmlTextWriterTag.Img);
				output.RenderEndTag();

				output.AddAttribute(HtmlTextWriterAttribute.Id, PHOTO_LABEL_DIV);
				output.RenderBeginTag(HtmlTextWriterTag.Div);

				output.Write(_photos[_currentImageIndex].caption);
				output.RenderBeginTag(HtmlTextWriterTag.Br);
				output.RenderEndTag();
				output.Write(String.Format(CultureInfo.InvariantCulture, "{0} of {1}", _currentImageIndex + 1, _photos.Count));

				output.RenderEndTag(); //PHOTO_LABEL_DIV

				//Previous link
				output.AddAttribute(HtmlTextWriterAttribute.Href,
				                    Page.ClientScript.GetPostBackClientHyperlink(this, PHOTO_NAV_PREVIOUS));
				output.RenderBeginTag(HtmlTextWriterTag.A);
				output.Write("Previous");
				output.RenderEndTag(); //A

				output.Write(" ");

				//Next link
				output.AddAttribute(HtmlTextWriterAttribute.Href, Page.ClientScript.GetPostBackClientHyperlink(this, PHOTO_NAV_NEXT));
				output.RenderBeginTag(HtmlTextWriterTag.A);
				output.Write("Next");
				output.RenderEndTag(); //A

				output.RenderEndTag(); //INNER_DIV
				output.RenderEndTag(); //OUTER_DIV            
			}
		}

		/// <summary>
		/// Called each time the control is loaded.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (UseViewState)
			{
				LoadFromViewState();
			}
		}

		/// <summary>
		/// Loads the data from ViewState.  This is used to pull back data during a Postback operation.
		/// </summary>
		private void LoadFromViewState()
		{
			object photos = ViewState[VIEWSTATE_PHOTOS];
			if (!Equals(photos, null))
			{
				_photos = ((IEnumerable<string>)photos).Select(photoXML => Utilities.DeserializeXML<photo>(photoXML)).ToList();
			}

			object albums = ViewState[VIEWSTATE_ALBUMS];
			if (!Equals(albums, null))
			{
                _albums = ((IEnumerable<string>)albums).Select(albumXML => Utilities.DeserializeXML<album>(albumXML)).ToList();
			}

			object userid = ViewState[VIEWSTATE_USERID];
			if (!Equals(userid, null))
			{
				_userId =  (int) userid;
			}
		}

		/// <summary>
		/// Loads the albums for the Facebook User.
		/// </summary>
		public void LoadAlbums()
		{
			if (Equals(_facebookApi, null))
			{
				throw new Exception("Cannot load albums before FacebookService property is set.");
			}
			if (Equals(_userId, null) || _userId.Equals(string.Empty))
			{
				throw new Exception("Cannot load albums before UserID property is set.");
			}
			
            Albums = _facebookApi.Photos.GetAlbums(_userId);
			ResetAlbumIndex();
			LoadPhotos();
		}

		/// <summary>
		/// Loads the albums for the Facebook User.
		/// </summary>
		public void LoadAlbums(Api fb, int userId)
		{
            FacebookApi = fb;
			UserId = userId;
			LoadAlbums();
		}

		/// <summary>
		/// Extracts the photos URI for a given Facebook photo.
		/// </summary>
		/// <param name="index">The index of the photo to load.</param>
		/// <returns>the string representation of the URI to the photo.</returns>
		private string GetImageUri(int index)
		{
			if (_photos != null && _photos.Count > 0)
			{
			    return _photos[index].src;
			}
			return string.Empty;
		}

		/// <summary>
		/// Returns the current photo album
		/// </summary>
		/// <returns></returns>
		private album GetSelectedAlbum()
		{
			if (_currentAlbumIndex >= 0 && _currentAlbumIndex < _albums.Count)
			{
				return _albums[_currentAlbumIndex];
			}
			else
			{
				return _albums[0];
			}
		}


		private void ResetImageIndex()
		{
			_currentImageIndex = 0;
			UpdateImageIndex();
		}

		private void UpdateImageIndex()
		{
			ViewState[VIEWSTATE_CURRENT_IMAGE] = _currentImageIndex;
		}

		private void MovePreviousImage()
		{
			if (_currentImageIndex == 0)
			{
				_currentImageIndex = _photos.Count - 1;
			}
			else
			{
				_currentImageIndex--;
			}
			UpdateImageIndex();
		}

		private void MoveNextImage()
		{
			if (_currentImageIndex + 1 < _photos.Count)
			{
				_currentImageIndex++;
			}
			else
			{
				_currentImageIndex = 0;
			}
			UpdateImageIndex();
		}

		private void LoadPhotos()
		{
			if (!Equals(_albums, null) && _albums.Count > 0)
			{
				//Todo: See if the new call is the same as the following:
                //Photos = _facebookService.GetPhotos(GetSelectedAlbum().AlbumId);
                Photos = FacebookApi.Photos.Get("", GetSelectedAlbum().aid, null);
			}
			else
			{
				Photos = null;
			}
		}

		private void ResetAlbumIndex()
		{
			_currentAlbumIndex = 0;
			UpdateAlbumIndex();
		}

		private void UpdateAlbumIndex()
		{
			ViewState[VIEWSTATE_CURRENT_ALBUM] = _currentAlbumIndex;
		}
	}

    /// <summary> 
    /// Event Args for event when user moves between photos
    /// </summary>
    public class PhotoNavigationItemClickEventArgs : EventArgs
	{
		private readonly string _photoId;

        /// <summary> 
        /// Constructor
        /// </summary>
        public PhotoNavigationItemClickEventArgs(string id)
		{
			_photoId = id;
		}

        /// <summary> 
        /// Underlying Data
        /// </summary>
        public string PhotoId
		{
			get { return _photoId; }
		}
	}
}
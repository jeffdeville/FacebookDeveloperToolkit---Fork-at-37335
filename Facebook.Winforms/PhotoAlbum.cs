using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Facebook.Winforms.Components;
using Facebook.Winforms.Properties;
using Facebook.Schema;

namespace Facebook.Winforms
{
	[ToolboxItem(true), ToolboxBitmap(typeof (BaseControl))]
	public partial class PhotoAlbum : BaseControl
	{
        /// <summary> 
        /// Callback
        /// </summary>
        public delegate album GetAlbumPhotosHandler();
        /// <summary> 
        /// Callback
        /// </summary>
        public delegate void LoadImageHandler(int index);

		private IList<album> _albums;
		private int _currentImageIndex;
		private IList<photo> _photos;
		private string _selectedPath = string.Empty;

        /// <summary> 
        /// Constructor
        /// </summary>
        public PhotoAlbum()
		{
			InitializeComponent();
		}

        /// <summary> 
        /// The underlying list of Albums
        /// </summary>
        [Category("Facebook")]
		[Description("")]
		public IList<album> Albums
		{
			get { return _albums; }
			set
			{
				_albums = value;
				OnLoad();
			}
		}

        /// <summary> 
        /// Instance of the component used to access the api
        /// </summary>
        [Category("Facebook"), Description("")]
		public FacebookService FacebookService { get; set; }

		private void OnLoad()
		{
			if (!IsDesignTime() && FacebookService != null && _albums != null)
			{
				LoadAlbums();
			}
		}

		private void LoadAlbums()
		{
			cboAlbums.Items.Clear();

			if (_albums.Count > 0)
			{
				foreach (var album in _albums)
				{
					cboAlbums.Items.Add(album);
				}
				cboAlbums.DisplayMember = "name";
				cboAlbums.ValueMember = "aid";

				cboAlbums.SelectedIndex = 0;
			}
			else
			{
				pbPhoto.Image = Resources.missingPicture;
				lblPhotoCount.Text = string.Empty;
				lblTag.Text = string.Empty;
				_photos = null;
				cboAlbums.Text = string.Empty;
			}
		}

		private void lbnext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OnChangeImage(1);
		}

		private void lbPrevious_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OnChangeImage(-1);
		}

		private void cboAlbums_SelectedIndexChanged(object sender, EventArgs e)
		{
			_photos = null;
			lblPhotoCount.Text = GetSelectedAlbum().name + " album is loading.";
			lblTag.Text = "  Please wait...";
			pbPhoto.Image = Resources.missingPicture;
			var backgroundThread = new Thread(LoadPhotos);
			backgroundThread.Start();
		}

		private void LoadPhotos()
		{
			if (FacebookService == null) return;
			_photos = FacebookService.Photos.Get(null,GetSelectedAlbum().aid,null);
			LoadImage(0);
		}

		private album GetSelectedAlbum()
		{
			return InvokeRequired
			       	? (album) Invoke(new GetAlbumPhotosHandler(GetSelectedAlbum), null)
			       	: (album) cboAlbums.SelectedItem;
		}

		private void LoadImage(int index)
		{
			if (InvokeRequired)
			{
				var parms = new object[1];
				parms[0] = index;
				Invoke(new LoadImageHandler(LoadImage), parms);
			}
			else
			{
				if (_photos != null && _photos.Count > 0)
				{
					if (_photos[index].src != null)
					{
						pbPhoto.ImageLocation = _photos[index].src;
					}
					else
					{
						pbPhoto.Image = _photos[index].picture;
					}
					lblPhotoCount.Text = String.Format(CultureInfo.InvariantCulture, "{0} of {1}", index + 1, _photos.Count);
					lblTag.Text = _photos[index].caption;
					_currentImageIndex = index;
				}
			}
		}

		private void OnChangeImage(int direction)
		{
			if (_photos == null) return;

			var nextIndex = _currentImageIndex + direction;

			if (nextIndex >= _photos.Count)
			{
				_currentImageIndex = 0;
			}
			else if (nextIndex < 0)
			{
				_currentImageIndex = _photos.Count - 1;
			}
			else
				_currentImageIndex = nextIndex;

			LoadImage(_currentImageIndex);
		}

		private void OnExport()
		{
			if (_photos == null) return;

			if (fbdExport.ShowDialog() == DialogResult.OK)
			{
				_selectedPath = fbdExport.SelectedPath;
				var backgroundThread = new Thread(SaveImages);
				backgroundThread.Start();
			}
		}

		private void SaveImages()
		{
			foreach (var photo in _photos)
			{
				Thread.Sleep(200);

				var bitmap = new Bitmap(photo.picture);
				bitmap.Save(Path.Combine(_selectedPath, photo.pid + ".jpg"), ImageFormat.Jpeg);
			}
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			OnExport();
		}
	}
}
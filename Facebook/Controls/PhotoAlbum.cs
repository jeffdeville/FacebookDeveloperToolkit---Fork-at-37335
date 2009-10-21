using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;
using Facebook.Components;

namespace Facebook.Controls
{
    [ToolboxItem(true)]
    public partial class PhotoAlbum : BaseControl
    {
        private Collection<Album> _albums = null;
        private Collection<Photo> _photos = null;
        private int _currentImageIndex = 0;
        private FacebookService _facebookService = null;
        private string _selectedPath = string.Empty;

        public delegate Album GetAlbumPhotosHandler();
        public delegate void LoadImageHandler(int index);

        [Category("Facebook")]
        [Description("")]
        public Collection<Album> Albums
        {
            get { return _albums; }
            set
            {
                _albums = value;
                OnLoad();
            }
        }

        [Category("Facebook")]
        [Description("")]
        public FacebookService FacebookService
        {
            get { return _facebookService; }
            set { _facebookService = value; }
        }

        public PhotoAlbum()
        {
            InitializeComponent();
        }

        private void OnLoad()
        {
            if (!IsDesignTime() && _facebookService != null && _albums != null)
            {
                LoadAlbums();
            }
        }

        private void LoadAlbums()
        {
            cboAlbums.Items.Clear();

            if (_albums.Count > 0)
            {
                foreach (Album album in _albums)
                {
                    cboAlbums.Items.Add(album);
                }
                cboAlbums.DisplayMember = "Name";
                cboAlbums.ValueMember = "AlbumId";

                cboAlbums.SelectedIndex = 0;
            }
            else
            {
                pbPhoto.Image = Facebook.Properties.Resources.missingPicture;
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
            lblPhotoCount.Text = GetSelectedAlbum().Name + " album is loading.";
            lblTag.Text = "  Please wait...";
            pbPhoto.Image = Facebook.Properties.Resources.missingPicture;
            Thread backgroundThread = new Thread(new ThreadStart(this.LoadPhotos));
            backgroundThread.Start();
        }

        private void LoadPhotos()
        {
            if (_facebookService == null) return;
            _photos = _facebookService.GetPhotos(GetSelectedAlbum().AlbumId);
            LoadImage(0);
        }

        private Album GetSelectedAlbum()
        {
            if (InvokeRequired)
            {
                return (Album)Invoke(new GetAlbumPhotosHandler(GetSelectedAlbum), null);
            }
            else
            {
                return (Album)cboAlbums.SelectedItem;
            }
        }

        private void LoadImage(int index)
        {
            if (InvokeRequired)
            {
                object[] parms = new object[1];
                parms[0] = index;
                Invoke(new LoadImageHandler(LoadImage), parms); 
            }
            else
            {
                if (_photos != null && _photos.Count > 0)
                {
                    pbPhoto.Image = _photos[index].Image;
                    lblPhotoCount.Text = String.Format(CultureInfo.InvariantCulture, "{0} of {1}", index + 1, _photos.Count);
                    lblTag.Text = _photos[index].Caption;
                    _currentImageIndex = index;
                }
            }
        }

        private void OnChangeImage(int direction)
        {
            if (_photos == null) return;

            int nextIndex = _currentImageIndex + direction;

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
                Thread backgroundThread = new Thread(new ThreadStart(this.SaveImages));
                backgroundThread.Start();
            }
        }

        private void SaveImages()
        {
            foreach (Photo photo in _photos)
            {
                Thread.Sleep(200);

                Bitmap bitmap = new Bitmap(photo.Image);
                bitmap.Save(Path.Combine(_selectedPath, photo.PhotoId + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            OnExport();
        }


    }
}

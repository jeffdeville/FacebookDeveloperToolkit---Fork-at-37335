
namespace Facebook.BindingHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents a album collection
    /// </summary>
    public sealed class FacebookPhotoAlbumCollection : FacebookDataCollection<FacebookPhotoAlbum>
    {
        /// <summary>
        /// Initializes FacebookPhotoAlbumCollection object
        /// </summary>
        public FacebookPhotoAlbumCollection()
            : base()
        {
        }

        /// <summary>
        /// Initializes FacebookPhotoAlbumCollection object from list of albums
        /// </summary>
        /// <param name="albums">album list</param>
        public FacebookPhotoAlbumCollection(IEnumerable<FacebookPhotoAlbum> albums)
            : base(albums)
        {
        }

    }
}

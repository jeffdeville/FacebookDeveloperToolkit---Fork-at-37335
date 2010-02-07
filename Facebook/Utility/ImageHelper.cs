#if !SILVERLIGHT
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace Facebook.Utility
{
	internal static class ImageHelper
    {
        #region Private Members

        private static Image _missingPicture;

        #endregion Private Members

        static ImageHelper()
        {
            // Populate and store missing picture object
            _missingPicture = GetMissingPicture();
        }

	    internal static Image MissingPicture
	    {
	        get
	        {
                if(_missingPicture == null)
                {
                    _missingPicture = GetMissingPicture();
                }
	            return _missingPicture;
	        }
	    }

        internal static Image ConvertBytesToImage(byte[] imageBytes)
        {
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                ms.Write(imageBytes, 0, imageBytes.Length);
                return new Bitmap(ms);
            }

        }

	    internal static byte[] ConvertImageToBytes(Image image)
		{
			using (var ms = new MemoryStream())
			{
				image.Save(ms, ImageFormat.Jpeg);
				return ms.ToArray();
			}
		}

        internal static Image GetImage(Uri imageURL)
        {
            byte[] imageBytes = GetBytesFromWeb(imageURL);
            return ConvertBytesToImage(imageBytes);
        }

	    //internal static Image GetImage(Uri imageURL, Image image, out byte[] imageBytes)
        //{
        //    imageBytes = ConvertImageToBytes(Resources.missingPicture);
        //    if (image.Equals(Resources.missingPicture))
        //    {
        //        imageBytes = GetBytesFromWeb(imageURL);
        //        return ConvertBytesToImage(imageBytes);
        //    }
        //    return image;
        //}

		private static byte[] GetBytesFromWeb(Uri imageURL)
		{
            try
            {
                if (imageURL.Equals(Constants.MissingPictureUrl))
                {
                    return ConvertImageToBytes(MissingPicture);
                }

                var webClient = new WebClient();
                return webClient.DownloadData(imageURL);
            }
            catch
            {
                return ConvertImageToBytes(MissingPicture);
            }
		}

		internal static byte[] GetBytes(Uri imageURL, byte[] imageBytes)
		{
            return imageBytes.Equals(ConvertImageToBytes(MissingPicture)) ? GetBytesFromWeb(imageURL) : imageBytes;
		}

		internal static byte[] GetBytes(Uri imageURL, out Image image, byte[] imageBytes)
		{
			imageBytes = GetBytes(imageURL, imageBytes);
			image = ConvertBytesToImage(imageBytes);

			return imageBytes;
		}

        private static Image GetMissingPicture()
        {
            // Verify missing picture object has been populated
            if (_missingPicture == null)
            {
                var webClient = new WebClient();
                var rawPicture = webClient.DownloadData(Constants.MissingPictureUrl);
                _missingPicture = ConvertBytesToImage(rawPicture);
            }

            return _missingPicture;
        }
    }
}
#endif
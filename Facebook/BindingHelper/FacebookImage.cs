using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Runtime.Serialization;
using System.IO;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Windows.Threading;

using Facebook.Utility;

namespace Facebook.BindingHelper
{
    /// <summary>
    /// Enumeration for image sizes
    /// </summary>
    public enum FacebookImageDimensions
    {
        /// <summary>
        /// Normal size image
        /// </summary>
        Normal,
        /// <summary>
        /// Big size image
        /// </summary>
        Big,
        /// <summary>
        /// Small size image
        /// </summary>
        Small,
        /// <summary>
        /// Square size image
        /// </summary>
        Square,
    }

    /// <summary>
    /// Represents a image object at its different size
    /// </summary>
    [DataContract]
    public class FacebookImage 
    {
        [DataMember(Name = "Uri")]
        private Uri _normal;
        [DataMember(Name = "BigUri")]
        private Uri _big;
        [DataMember(Name="SmallUri")]
        private Uri _small;
        [DataMember(Name="SquareUri", IsRequired=false)]
        private Uri _square;

        Dispatcher dispatcher;

        /// <summary>
        /// Content type of image
        /// </summary>
        public string ContentType
        {
            get;
            private set;
        }

        /// <summary>
        /// Uri used to download the image
        /// </summary>
        public Uri Uri
        {
            get;
            private set;
        }
        /// <summary>
        /// Initializes Facebook image object
        /// </summary>
        /// <param name="normalPic"></param>
        /// <param name="bigPic"></param>
        /// <param name="smallPic"></param>
        /// <param name="squarePic"></param>
        internal FacebookImage(string normalPic, string bigPic, string smallPic, string squarePic)
        {
            Uri normal = CreateUri(normalPic);
            Uri big = CreateUri(bigPic);
            Uri small = CreateUri(smallPic);
            Uri square = CreateUri(squarePic); 

            // If one url type isn't available, try to fallback on other provided values.
            _big = big ?? normal ?? small ?? square;
            _normal = normal ?? big ?? small ?? square;
            _small = small ?? normal ?? big ?? square;
            _square = square ?? small ?? normal ?? big;

        }

        /// <summary>
        /// Starts download of requested image
        /// </summary>
        /// <param name="requestedSize">Size of the image</param>
        /// <param name="callback">Callback that will be called when download completes</param>
        public void GetImageAsync(FacebookImageDimensions requestedSize, GetImageSourceAsyncCallback callback)
        {
#if !SILVERLIGHT
            dispatcher = Dispatcher.CurrentDispatcher;
#else
            dispatcher = System.Windows.Deployment.Current.Dispatcher;
#endif
            Uri = _normal;
            switch (requestedSize)
            {
                case FacebookImageDimensions.Big: Uri = _big; break;
                case FacebookImageDimensions.Small: Uri = _small; break;
                case FacebookImageDimensions.Square: Uri = _square; break;
            }

            if (Uri == null)
            {
                callback(this, new GetImageSourceCompletedEventArgs(new InvalidOperationException(), false, this));
                return;
            }


            WebClientHelper wc = new WebClientHelper(callback);
            wc.RequestCompleted += new EventHandler<RequestCompletedEventArgs>(wc_RequestCompleted);
            wc.SendRequest(Uri, null, null);
        }

        void wc_RequestCompleted(object sender, RequestCompletedEventArgs e)
        {
            WebClientHelper wc = sender as WebClientHelper;
            ContentType = wc.ContentType;

             dispatcher.BeginInvoke(new Action(()=>
                {
                    GetImageSourceCompletedEventArgs eventArgs;

                    if (e.Error != null)
                    {
                        eventArgs = new GetImageSourceCompletedEventArgs(e.Error, false, this);
                    }
                    else
                    {
                        try
                        {
                            eventArgs = CreateBitmapFromStream(e.Response);
                        }
                        finally
                        {
                            e.Response.Close();
                        }
                    }

                    GetImageSourceAsyncCallback callback = (GetImageSourceAsyncCallback)e.UserState;
                    callback(this, eventArgs);

                }));
        }

        GetImageSourceCompletedEventArgs CreateBitmapFromStream(Stream stream)
        {
            GetImageSourceCompletedEventArgs eventArgs;
            try
            {
                // Create a BitmapImage from memory stream.
                BitmapImage bitmapImage = new BitmapImage();
#if !SILVERLIGHT
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
#else
                bitmapImage.SetSource(stream);
#endif
                eventArgs = new GetImageSourceCompletedEventArgs(bitmapImage, this);
            }
            catch (InvalidOperationException exception)
            {
                eventArgs = new GetImageSourceCompletedEventArgs(exception, false, this);
            }
            catch (Exception exception)
            {
                eventArgs = new GetImageSourceCompletedEventArgs(exception, false, this);
            }

            return eventArgs;
        }

        static Uri CreateUri(string path)
        {
            Uri uri = null;
            if (!string.IsNullOrEmpty(path))
            {
                Uri result;
                if (Uri.TryCreate(path, UriKind.RelativeOrAbsolute, out result))
                {
                    uri = result;
                }
            }
            return uri;
        }

    }
}

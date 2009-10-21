using System;
using System.Collections.Generic;
using Facebook.Schema;
using System.IO;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
    /// <summary>
    /// Facebook Video API methods.
    /// </summary>
    public class Video : RestBase, Facebook.Rest.IVideo
    {
        #region Private Members

        private static Dictionary<Enums.FileType, string> _mimeTypes = new Dictionary<Enums.FileType, string> {
            {Enums.FileType.asf, "video/x-ms-asf"},
            {Enums.FileType.avi, "video/avi"},
            {Enums.FileType.flv, "video/x-flv"},
            {Enums.FileType.m4v, "video/mp4"},
            {Enums.FileType.mkv, "video/x-matroska"},
            {Enums.FileType.mov, "video/quicktime"},
            {Enums.FileType.mp4, "video/mp4"},
            {Enums.FileType.mpe, "video/mpeg"},
            {Enums.FileType.mpeg, "video/mpeg"},
            {Enums.FileType.mpeg4, "video/mp4"},
            {Enums.FileType.mpg, "video/mpeg"},
            {Enums.FileType.nsv, "application/x-winamp"},
            {Enums.FileType.ogm, "video/ogg"},
            {Enums.FileType.qt, "video/quicktime"},
            {Enums.FileType.vob, "video/dvd"},
            {Enums.FileType.wmv, "video/x-ms-wmv"},
            {Enums.FileType.gif, "image/gif"},
            {Enums.FileType.jpg, "image/jpeg"},
            {Enums.FileType.png, "image/png"},
            {Enums.FileType.psd, "image/psd"},
            {Enums.FileType.tiff, "image/tiff"},
            {Enums.FileType.jp2, "image/jp2"},
            {Enums.FileType.iff, "image/iff"},
            {Enums.FileType.wbmp, "image/vnd.wap.wbmp"},
            {Enums.FileType.xbm, "image/x-xbitmap"},
            {Enums.FileType.bmp, "image/bmp"},
            };
        #endregion
        #region Methods

        #region Constructor

        /// <summary>
        /// Public constructor for facebook.video
        /// </summary>
        /// <param name="session">Needs a connected Facebook Session object for making requests</param>
        public Video(IFacebookSession session)
            : base(session)
        {
        }

        #endregion Constructor

        #region Public Methods

#if !SILVERLIGHT

        #region Synchronous Methods

        /// <summary>
        /// Returns the file size and length limits for a video that the current user can upload through your application.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Video.GetUploadLimits();
        /// </code>
        /// </example>
        /// <returns>This method returns a video_limits object length and size keys mapped to the number of seconds and bytes, respectively.</returns>
        public video_limits GetUploadLimits()
        {
            return GetUploadLimits(false, null, null);
        }

        /// <summary>
        /// Uploads a video owned by the current session user and returns the video.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var data = new FileInfo(@"C:\Butterfly.wmv");
        /// var result = api.Video.Upload("my new video", "a video upload test", data);
        /// </code>
        /// </example>
        /// <param name="title">The name of the video. The name cannot be longer than 65 characters. Longer titles will get truncated and will not return an error.</param>
        /// <param name="description">A description of the video. There is no limit to the length of the description.</param>
        /// <param name="data">The raw image data for the video.</param>
        /// <returns>This method returns a video object containing information about the uploaded object.</returns>
        public video Upload(string title, string description, FileInfo data)
        {
            return Upload(title, description, data, null, null, false, null, null);
        }

        #endregion Synchronous Methods

#endif

        #region Asynchronous Methods
        
        /// <summary>
        /// Returns the file size and length limits for a video that the current user can upload through your application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Video.GetUploadLimitsAsync(AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(video_limits result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns a video_limits object length and size keys mapped to the number of seconds and bytes, respectively.</returns>
        public void GetUploadLimitsAsync(GetUploadLimitsCallback callback, Object state)
        {
            GetUploadLimits(true, callback, state);
        }
        
        /// <summary>
        /// Uploads a video owned by the current session user and returns the video.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     var filePath = @"C:\Butterfly.wmv";
        ///     var fileStream = System.IO.File.OpenRead(filePath);
        ///     var reader = new System.IO.BinaryReader(fileStream);
        ///     var fileData = reader.ReadBytes((int)fileStream.Length);
        ///     api.Video.UploadAsync("test", "video upload test", System.IO.Path.GetFileName(filePath), fileData, "video/avi", AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(video result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="title">The name of the video. The name cannot be longer than 65 characters. Longer titles will get truncated and will not return an error.</param>
        /// <param name="description">A description of the video. There is no limit to the length of the description.</param>
        /// <param name="data">The raw image data for the video.</param>
        /// <param name="contentType">Content type of the video.  Supported types are: video/x-ms-asf,video/avi,video/x-flv,video/mp4,video/x-matroska,video/quicktime,video/mp4,video/mpeg,video/mpeg,video/mp4,video/mpeg,application/x-winamp,video/ogg,video/quicktime,video/dvd,video/x-ms-wmv</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns a video object containing information about the uploaded object.</returns>
        public void UploadAsync(string title, string description, byte[] data, string contentType, UploadCallback callback, Object state)
        {
            Upload(title, description, null, data, contentType, true, callback, state);
        }
        /// <summary>
        /// Uploads a video owned by the current session user and returns the video.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     var filePath = @"C:\Butterfly.wmv";
        ///     var fileStream = System.IO.File.OpenRead(filePath);
        ///     var reader = new System.IO.BinaryReader(fileStream);
        ///     var fileData = reader.ReadBytes((int)fileStream.Length);
        ///     api.Video.UploadAsync("test", "video upload test", System.IO.Path.GetFileName(filePath), fileData, "video/avi", AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(video result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="title">The name of the video. The name cannot be longer than 65 characters. Longer titles will get truncated and will not return an error.</param>
        /// <param name="description">A description of the video. There is no limit to the length of the description.</param>
        /// <param name="data">The raw image data for the video.</param>
        /// <param name="fileType">One of the video type</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns a video object containing information about the uploaded object.</returns>
        public void UploadAsync(string title, string description, byte[] data, Enums.FileType fileType, UploadCallback callback, Object state)
        {
            Upload(title, description, null, data, _mimeTypes[fileType], true, callback, state);
        }


        #endregion Asynchronous Methods

        #endregion Public Methods

        #region Private Methods

        private video_limits GetUploadLimits(bool isAsync, GetUploadLimitsCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.video.getUploadLimits" } };

            if (isAsync)
            {
				SendRequestAsync(parameterList, new FacebookCallCompleted<video_limits>(callback), state);
                return null;
            }

            return SendRequest<video_limits>(parameterList, true);
        }

        private video Upload(string title, string description, FileSystemInfo data, byte[] rawData, string contentType, bool isAsync, UploadCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.video.upload" } };
            Utilities.AddOptionalParameter(parameterList, "title", title);
            Utilities.AddOptionalParameter(parameterList, "description", description);

            if (isAsync)
            {
		        UploadVideoFile(parameterList, rawData, contentType, new AsyncResult(OnFacebookCallCompleted<video>, new FacebookCallCompleted<video>(callback), state));
                return null;
            }

#if !SILVERLIGHT
            var response = ExecuteApiVideoUpload(data, parameterList);
            return !string.IsNullOrEmpty(response) ? Utilities.DeserializeXML<video_upload_response>(response) : null;
#else
            return null;
#endif
        }

        #endregion Private Methods

        #endregion Methods

        #region Delegates

        /// <summary>
        /// Delegate called when GetVideoUploadLimits call completed
        /// </summary>
        /// <param name="limits">video limits data</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetUploadLimitsCallback(video_limits limits, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when VideoUpload call completed
        /// </summary>
        /// <param name="video">information about uploaded video</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void UploadCallback(video video, Object state, FacebookException e);

        #endregion Delegates
    }
}
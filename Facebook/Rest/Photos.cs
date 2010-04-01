using System;
using System.Collections.Generic;
using System.IO;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
    /// <summary>
    /// Facebook Photo API methods.
    /// </summary>
    public class Photos : AuthorizedRestBase, Facebook.Rest.IPhotos
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
        /// Public constructor for facebook.Photo
        /// </summary>
        /// <param name="session">Needs a connected Facebook Session object for making requests</param>
        public Photos(IFacebookSession session)
            : base(session)
        {
        }

        #endregion Constructor

        #region Public Methods

#if !SILVERLIGHT
        
        #region Synchronous Methods

        /// <summary>
        /// Adds a tag with the given information to a photo.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// api.Photos.AddTag(Constants.PhotoId, Constants.UserId, "test", 0F, 0F);
        /// </code>
        /// </example>
        /// <param name="pid">The photo id of the photo to be tagged.</param>
        /// <param name="tag_uid">The user id of the user being tagged. Either tag_uid or tag_text must be specified.</param>
        /// <param name="tag_text">Some text identifying the person being tagged. Either tag_uid or tag_text must be specified. This parameter is ignored if tag_uid is specified.</param>
        /// <param name="x">The horizontal position of the tag, as a percentage from 0 to 100, from the left of the photo.</param>
        /// <param name="y">The vertical position of the tag, as a percentage from 0 to 100, from the top of the photo.</param>
        /// <remarks>Tags can only be added to pending photos owned by the current session user. Once a photo has been approved or rejected, it can no longer be tagged with this method. There is a limit of 20 tags per pending photo.</remarks>
        public bool AddTag(string pid, long? tag_uid, string tag_text, float x, float y)
        {
            return AddTag(pid, 0, tag_text, x, y, tag_uid.Value);
        }

        /// <summary>
        /// Adds a tag with the given information to a photo.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// api.Photos.AddTag(Constants.PhotoId, Constants.UserId, "test", 0F, 0F);
        /// </code>
        /// </example>
        /// <param name="pid">The photo id of the photo to be tagged.</param>
        /// <param name="tag_uid">The user id of the user being tagged. Either tag_uid or tag_text must be specified.</param>
        /// <param name="tag_text">Some text identifying the person being tagged. Either tag_uid or tag_text must be specified. This parameter is ignored if tag_uid is specified.</param>
        /// <param name="x">The horizontal position of the tag, as a percentage from 0 to 100, from the left of the photo.</param>
        /// <param name="y">The vertical position of the tag, as a percentage from 0 to 100, from the top of the photo.</param>
        /// <remarks>Tags can only be added to pending photos owned by the current session user. Once a photo has been approved or rejected, it can no longer be tagged with this method. There is a limit of 20 tags per pending photo.</remarks>
		public bool AddTag(string pid, long tag_uid, string tag_text, float x, float y)
        {
            return AddTag(pid, tag_uid, tag_text, x, y, 0);
        }

        /// <summary>
        /// Adds a tag with the given information to a photo.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        /// api.Session.UserId = Constants.UserId;
        /// api.Photos.AddTag(Constants.PhotoId, Constants.UserId, "test", 0F, 0F, Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="pid">The photo id of the photo to be tagged.</param>
        /// <param name="tag_uid">The user id of the user being tagged. Either tag_uid or tag_text must be specified.</param>
        /// <param name="tag_text">Some text identifying the person being tagged. Either tag_uid or tag_text must be specified. This parameter is ignored if tag_uid is specified.</param>
        /// <param name="x">The horizontal position of the tag, as a percentage from 0 to 100, from the left of the photo.</param>
        /// <param name="y">The vertical position of the tag, as a percentage from 0 to 100, from the top of the photo.</param>
        /// <param name="owner_uid">The user ID of the user whose photo you are tagging. If this parameter is not specified, then it defaults to the session user.  Note: This parameter applies only to Web applications and is required by them only if the session_key is not specified. Facebook ignores this parameter if it is passed by a desktop application.</param>
        /// <remarks>Tags can only be added to pending photos owned by the current session user. Once a photo has been approved or rejected, it can no longer be tagged with this method. There is a limit of 20 tags per pending photo.</remarks>
        public bool AddTag(string pid, long tag_uid, string tag_text, float x, float y, long owner_uid)
		{
			return AddTag(pid, tag_uid, tag_text, x, y, owner_uid, false, null, null);
		}

        /// <summary>
        /// Creates and returns a new album owned by the current session user.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Photos.CreateAlbum("test album name", "Chicago, IL", "Code sample album");
        /// </code>
        /// </example>
        /// <param name="name">The album name.</param>
        /// <param name="location">Optional - The album location.</param>
        /// <param name="description">Optional - The album description.</param>
        /// <returns>A new album owned by the current session user</returns>
        /// <remarks>The returned cover_pid is always 0.</remarks>
        public album CreateAlbum(string name, string location, string description)
        {
            return CreateAlbum(name, location, description, -1);
        }

        /// <summary>
        /// Creates and returns a new album owned by the current session user.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Photos.CreateAlbum("test album name", "Chicago, IL", "Code sample album", Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="name">The album name.</param>
        /// <param name="location">Optional - The album location.</param>
        /// <param name="description">Optional - The album description.</param>
        /// <param name="uid"></param>
        /// <returns>A new album owned by the current session user</returns>
        /// <remarks>The returned cover_pid is always 0.</remarks>
        public album CreateAlbum(string name, string location, string description, long uid)
		{
		    return CreateAlbum(name, location, description, uid, false, null, null);
    	}

        /// <summary>
        /// Returns all visible photos according to the filters specified.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Photos.Get(string.Empty, Constants.AlbumId.ToString(), null);
        /// </code>
        /// </example>
        /// <param name="subj_id">Filter by photos tagged with this user.</param>
        /// <param name="aid">Filter by photos in this album.</param>
        /// <param name="pids">Filter by photos in this list. This is a comma-separated list of pids.</param>
        /// <returns>This method returns all visible photos satisfying the filters specified. The method can be used to return all photos tagged with user, in an album, query a specific set of photos by a list of pids, or filter on any combination of these three.</returns>
        /// <remarks>It is an error to omit all three of the subj_id, aid, and pids parameters. They have no defaults.</remarks>
        public IList<photo> Get(string subj_id, string aid, List<string> pids)
		{
		    return Get(subj_id, aid, pids, false, null, null);
		}

        /// <summary>
        /// Returns the set of user tags on all photos specified.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Photos.GetTags(new List&lt;string&gt; { Constants.PhotoId });
        /// </code>
        /// </example>
        /// <param name="pids">The list of photos from which to extract photo tags.</param>
        /// <returns>If no photo tags are found, the method will return an empty photos_getTags_response element. Text tags not corresponding to a user are not currently returned. Some tags may be text-only and will have an empty subect element. This occurs in the case where a user did not specifically tag another account, but just supplied text information.</returns>
        /// <remarks> A tag of a user will be visible to an application only if that user has not turned off access to the Facebook Platform.</remarks>
        public IList<photo_tag> GetTags(List<string> pids)
		{
		    return GetTags(pids, false, null, null);
		}

        /// <summary>
        /// Uploads a photo owned by the current session user and returns the new photo.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var data = new FileInfo(@"C:\Fry.jpg");
        /// var result = api.Photos.Upload(Constants.AlbumId.ToString(), "uploading test", data);
        /// </code>
        /// </example>
        /// <param name="aid">Optional - The album id of the destination album.</param>
        /// <param name="caption">Optional - The caption of the photo.</param>
        /// <param name="data">The raw image data for the photo.</param>
        /// <returns>Photo information, including the photo URL.</returns>
        /// <remarks>If no album is specified, the photo will be uploaded to a default album for the application, which will be created if necessary. Regular albums have a size limit of 60 photos. Default application albums have a size limit of 1000 photos. It is strongly recommended that you scale the image in your application before adding it to the request. The largest dimension should be at most 604 pixels (the largest display size Facebook supports).</remarks>
        public photo Upload(string aid, string caption, FileInfo data)
        {
            return Upload(aid, caption, data, 0);
        }

        /// <summary>
        /// Uploads a photo owned by the current session user and returns the new photo.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var data = new FileInfo(@"C:\Fry.jpg");
        /// var result = api.Photos.Upload(Constants.AlbumId.ToString(), "uploading test", data, Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="aid">Optional - The album id of the destination album.</param>
        /// <param name="caption">Optional - The caption of the photo.</param>
        /// <param name="data">The raw image data for the photo.</param>
        /// <param name="uid"></param>
        /// <returns>Photo information, including the photo URL.</returns>
        /// <remarks>If no album is specified, the photo will be uploaded to a default album for the application, which will be created if necessary. Regular albums have a size limit of 60 photos. Default application albums have a size limit of 1000 photos. It is strongly recommended that you scale the image in your application before adding it to the request. The largest dimension should be at most 604 pixels (the largest display size Facebook supports).</remarks>
        public photo Upload(string aid, string caption, FileInfo data, long uid)
		{
		    return Upload(aid, caption, data, uid, null, null, false, null, null);
        }

        /// <summary>
        /// Returns metadata about all of the photo albums uploaded by the specified user.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Photos.GetAlbums();
        /// </code>
        /// </example>
        /// <returns>This method returns all visible photos satisfying the filters specified. The method can be used to return all photo albums created by a user, query a specific set of albums by a list of aids, or filter on any combination of these two. The album ids returned by this function can be passed in to facebook.photos.get.</returns>
        /// <remarks>It is an error to omit both of the uid and aids parameters. They have no defaults. In this call, an album owned by a user will be returned to an application if that user has not turned off access to the Facbook Platform.</remarks>
        public IList<album> GetAlbums()
        {
            return GetAlbums(Session.UserId, null);
        }

        /// <summary>
        /// Returns metadata about all of the photo albums uploaded by the specified user.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Photos.GetAlbums(Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="uid">Return albums created by this user.</param>
        /// <returns>This method returns all visible photos satisfying the filters specified. The method can be used to return all photo albums created by a user, query a specific set of albums by a list of aids, or filter on any combination of these two. The album ids returned by this function can be passed in to facebook.photos.get.</returns>
        /// <remarks>It is an error to omit both of the uid and aids parameters. They have no defaults. In this call, an album owned by a user will be returned to an application if that user has not turned off access to the Facbook Platform.</remarks>
        public IList<album> GetAlbums(long uid)
        {
            return GetAlbums(uid, null);
        }

        /// <summary>
        /// Returns metadata about all of the photo albums uploaded by the specified user.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Photos.GetAlbums(new List&lt;string&gt; {Constants.AlbumId.ToString()});
        /// </code>
        /// </example>
        /// <param name="aids">Return albums with aids in this list. This is a List of aids. You must specify either uid or aids. The aids parameter has no default value.</param>
        /// <returns>This method returns all visible photos satisfying the filters specified. The method can be used to return all photo albums created by a user, query a specific set of albums by a list of aids, or filter on any combination of these two. The album ids returned by this function can be passed in to facebook.photos.get.</returns>
        /// <remarks>It is an error to omit both of the uid and aids parameters. They have no defaults. In this call, an album owned by a user will be returned to an application if that user has not turned off access to the Facbook Platform.</remarks>
        public IList<album> GetAlbums(List<string> aids)
        {
            return GetAlbums(0, aids);
        }

        /// <summary>
        /// Returns metadata about all of the photo albums uploaded by the specified user.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Photos.GetAlbums(Constants.UserId, new List&lt;string&gt; {Constants.AlbumId.ToString()});
        /// </code>
        /// </example>
        /// <param name="uid">Return albums created by this user.</param>
        /// <param name="aids">Return albums with aids in this list. This is a List of aids. You must specify either uid or aids. The aids parameter has no default value.</param>
        /// <returns>This method returns all visible photos satisfying the filters specified. The method can be used to return all photo albums created by a user, query a specific set of albums by a list of aids, or filter on any combination of these two. The album ids returned by this function can be passed in to facebook.photos.get.</returns>
        /// <remarks>It is an error to omit both of the uid and aids parameters. They have no defaults. In this call, an album owned by a user will be returned to an application if that user has not turned off access to the Facbook Platform.</remarks>
        public IList<album> GetAlbums(long uid, List<string> aids)
		{
			return GetAlbums(uid, aids, false, null, null);
		}

        #endregion Synchronous Methods

#endif

        #region Asynchronous Methods

        /// <summary>
        /// Adds a tag with the given information to a photo.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     api.Photos.AddTagAsync(Constants.PhotoId, Constants.UserId, "test", 30F, 30F, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="pid">The photo id of the photo to be tagged.</param>
        /// <param name="uid">The user ID of the user whose photo you are tagging. If this parameter is not specified, then it defaults to the session user.  Note: This parameter applies only to Web applications and is required by them only if the session_key is not specified. Facebook ignores this parameter if it is passed by a desktop application.</param>
        /// <param name="tag_text">Some text identifying the person being tagged. Either tag_uid or tag_text must be specified. This parameter is ignored if tag_uid is specified.</param>
        /// <param name="x">The horizontal position of the tag, as a percentage from 0 to 100, from the left of the photo.</param>
        /// <param name="y">The vertical position of the tag, as a percentage from 0 to 100, from the top of the photo.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <remarks>Tags can only be added to pending photos owned by the current session user. Once a photo has been approved or rejected, it can no longer be tagged with this method. There is a limit of 20 tags per pending photo.</remarks>
        public void AddTagAsync(string pid, long? uid, string tag_text, float x, float y, AddTagCallback callback, Object state)
        {
            AddTagAsync(pid, uid.Value, tag_text, x, y, 0, callback, state);
        }

        /// <summary>
        /// Adds a tag with the given information to a photo.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     api.Photos.AddTagAsync(Constants.PhotoId, Constants.UserId, "test", 30F, 30F, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="pid">The photo id of the photo to be tagged.</param>
        /// <param name="tag_uid">The user id of the user being tagged. Either tag_uid or tag_text must be specified.</param>
        /// <param name="tag_text">Some text identifying the person being tagged. Either tag_uid or tag_text must be specified. This parameter is ignored if tag_uid is specified.</param>
        /// <param name="x">The horizontal position of the tag, as a percentage from 0 to 100, from the left of the photo.</param>
        /// <param name="y">The vertical position of the tag, as a percentage from 0 to 100, from the top of the photo.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <remarks>Tags can only be added to pending photos owned by the current session user. Once a photo has been approved or rejected, it can no longer be tagged with this method. There is a limit of 20 tags per pending photo.</remarks>
        public void AddTagAsync(string pid, long tag_uid, string tag_text, float x, float y, AddTagCallback callback, Object state)
        {
            AddTagAsync(pid, tag_uid, tag_text, x, y, 0, callback, state);
        }

        /// <summary>
        /// Adds a tag with the given information to a photo.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new FacebookApi(new FBMLCanvasSession(Constants.WebApplicationKey, Constants.WebSecret));
        ///     api.Session.UserId = Constants.UserId;
        ///     api.Photos.AddTagAsync(Constants.PhotoId, Constants.UserId, "test", 30F, 30F, Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="pid">The photo id of the photo to be tagged.</param>
        /// <param name="tag_uid">The user id of the user being tagged. Either tag_uid or tag_text must be specified.</param>
        /// <param name="tag_text">Some text identifying the person being tagged. Either tag_uid or tag_text must be specified. This parameter is ignored if tag_uid is specified.</param>
        /// <param name="x">The horizontal position of the tag, as a percentage from 0 to 100, from the left of the photo.</param>
        /// <param name="y">The vertical position of the tag, as a percentage from 0 to 100, from the top of the photo.</param>
        /// <param name="owner_uid">The user ID of the user whose photo you are tagging. If this parameter is not specified, then it defaults to the session user.  Note: This parameter applies only to Web applications and is required by them only if the session_key is not specified. Facebook ignores this parameter if it is passed by a desktop application.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <remarks>Tags can only be added to pending photos owned by the current session user. Once a photo has been approved or rejected, it can no longer be tagged with this method. There is a limit of 20 tags per pending photo.</remarks>
		public void AddTagAsync(string pid, long tag_uid, string tag_text, float x, float y, long owner_uid, AddTagCallback callback, Object state)
		{
			AddTag(pid, tag_uid, tag_text, x, y, owner_uid, true, callback, state);
		}

        /// <summary>
        /// Creates and returns a new album owned by the current session user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Photos.CreateAlbumAsync("test album name", "Chicago, IL", "Code sample album", AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(album result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="name">The album name.</param>
        /// <param name="location">Optional - The album location.</param>
        /// <param name="description">Optional - The album description.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>A new album owned by the current session user</returns>
        /// <remarks>The returned cover_pid is always 0.</remarks>
        public void CreateAlbumAsync(string name, string location, string description, CreateAlbumCallback callback, Object state)
        {
            CreateAlbumAsync(name, location, description, 0, callback, state);
        }

        /// <summary>
        /// Creates and returns a new album owned by the current session user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Photos.CreateAlbumAsync("test album name", "Chicago, IL", "Code sample album", Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(album result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="name">The album name.</param>
        /// <param name="location">Optional - The album location.</param>
        /// <param name="description">Optional - The album description.</param>
        /// <param name="uid"></param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>A new album owned by the current session user</returns>
        /// <remarks>The returned cover_pid is always 0.</remarks>
        public void CreateAlbumAsync(string name, string location, string description, long uid, CreateAlbumCallback callback, Object state)
		{
			CreateAlbum(name, location, description, uid, true, callback, state);
		}

        /// <summary>
        /// Returns all visible photos according to the filters specified.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Photos.GetAsync(string.Empty, Constants.AlbumId.ToString(), null, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;photo&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="subj_id">Filter by photos tagged with this user.</param>
        /// <param name="aid">Filter by photos in this album.</param>
        /// <param name="pids">Filter by photos in this list. This is a comma-separated list of pids.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns all visible photos satisfying the filters specified. The method can be used to return all photos tagged with user, in an album, query a specific set of photos by a list of pids, or filter on any combination of these three.</returns>
        /// <remarks>It is an error to omit all three of the subj_id, aid, and pids parameters. They have no defaults.</remarks>
        public void GetAsync(string subj_id, string aid, List<string> pids, GetCallback callback, Object state)
        {
            Get(subj_id, aid, pids, true, callback, state);
        }

        /// <summary>
        /// Returns metadata about all of the photo albums uploaded by the specified user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Photos.GetAlbumsAsync(AsyncDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncDemoCompleted(IList&lt;album&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns all visible photos satisfying the filters specified. The method can be used to return all photo albums created by a user, query a specific set of albums by a list of aids, or filter on any combination of these two. The album ids returned by this function can be passed in to facebook.photos.get.</returns>
        /// <remarks>It is an error to omit both of the uid and aids parameters. They have no defaults. In this call, an album owned by a user will be returned to an application if that user has not turned off access to the Facbook Platform.</remarks>
        public void GetAlbumsAsync(GetAlbumsCallback callback, Object state)
		{
			GetAlbumsAsync(Session.UserId, null, callback, state);
		}

        /// <summary>
        /// Returns metadata about all of the photo albums uploaded by the specified user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Photos.GetAlbumsAsync(Constants.UserId, AsyncDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncDemoCompleted(IList&lt;album&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">Return albums created by this user.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns all visible photos satisfying the filters specified. The method can be used to return all photo albums created by a user, query a specific set of albums by a list of aids, or filter on any combination of these two. The album ids returned by this function can be passed in to facebook.photos.get.</returns>
        /// <remarks>It is an error to omit both of the uid and aids parameters. They have no defaults. In this call, an album owned by a user will be returned to an application if that user has not turned off access to the Facbook Platform.</remarks>
        public void GetAlbumsAsync(long uid, GetAlbumsCallback callback, Object state)
		{
			GetAlbumsAsync(uid, null, callback, state);
		}

        /// <summary>
        /// Returns metadata about all of the photo albums uploaded by the specified user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Photos.GetAlbumsAsync(new List&lt;string&gt; {Constants.AlbumId.ToString()}, AsyncDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncDemoCompleted(IList&lt;album&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="aids">Return albums with aids in this list. This is a List of aids. You must specify either uid or aids. The aids parameter has no default value.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns all visible photos satisfying the filters specified. The method can be used to return all photo albums created by a user, query a specific set of albums by a list of aids, or filter on any combination of these two. The album ids returned by this function can be passed in to facebook.photos.get.</returns>
        /// <remarks>It is an error to omit both of the uid and aids parameters. They have no defaults. In this call, an album owned by a user will be returned to an application if that user has not turned off access to the Facbook Platform.</remarks>
        public void GetAlbumsAsync(List<string> aids, GetAlbumsCallback callback, Object state)
        {
            GetAlbumsAsync(0, aids, callback, state);
        }

        /// <summary>
        /// Returns metadata about all of the photo albums uploaded by the specified user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Photos.GetAlbumsAsync(Constants.UserId, new List&lt;string&gt; {Constants.AlbumId.ToString()}, AsyncDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncDemoCompleted(IList&lt;album&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">Return albums created by this user.</param>
        /// <param name="aids">Return albums with aids in this list. This is a List of aids. You must specify either uid or aids. The aids parameter has no default value.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns all visible photos satisfying the filters specified. The method can be used to return all photo albums created by a user, query a specific set of albums by a list of aids, or filter on any combination of these two. The album ids returned by this function can be passed in to facebook.photos.get.</returns>
        /// <remarks>It is an error to omit both of the uid and aids parameters. They have no defaults. In this call, an album owned by a user will be returned to an application if that user has not turned off access to the Facbook Platform.</remarks>
        public void GetAlbumsAsync(long uid, List<string> aids, GetAlbumsCallback callback, Object state)
        {
            GetAlbums(uid, aids, true, callback, state);
        }

        /// <summary>
        /// Returns the set of user tags on all photos specified.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Photos.GetTagsAsync(new List&lt;string&gt; {Constants.PhotoId}, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;photo_tag&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="pids">The list of photos from which to extract photo tags.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>If no photo tags are found, the method will return an empty photos_getTags_response element. Text tags not corresponding to a user are not currently returned. Some tags may be text-only and will have an empty subect element. This occurs in the case where a user did not specifically tag another account, but just supplied text information.</returns>
        /// <remarks> A tag of a user will be visible to an application only if that user has not turned off access to the Facebook Platform.</remarks>
        public void GetTagsAsync(List<string> pids, GetTagsCallback callback, Object state)
        {
            GetTags(pids, true, callback, state);
        }

        /// <summary>
        /// Uploads a photo owned by the current session user and returns the new photo.
        /// </summary>
        /// <example>
        /// <code> 
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     var aid = Constants.AlbumId;
        ///     var caption = "caption";
        ///     var filePath = @"C:\Clarity.jpg";
        ///     var fileStream = System.IO.File.OpenRead(filePath);
        ///     var reader = new System.IO.BinaryReader(fileStream);
        ///     var fileData = reader.ReadBytes((int)fileStream.Length);
        ///     api.Photos.UploadAsync(aid, caption, System.IO.Path.GetFileName(filePath), fileData, "image/jpeg", AsyncDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncDemoCompleted(photo result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="aid">Optional - The album id of the destination album.</param>
        /// <param name="caption">Optional - The caption of the photo.</param>
        /// <param name="data">The raw image data for the photo.</param>
        /// <param name="contentType">You can upload the following image file formats through this call: image/gif,image/jpeg,image/png,image/psd,image/tiff,image/jp2,image/iff,image/vnd.wap.wbmp,image/x-xbitmap,image/bmp.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>Photo information, including the photo URL.</returns>
        /// <remarks>If no album is specified, the photo will be uploaded to a default album for the application, which will be created if necessary. Regular albums have a size limit of 60 photos. Default application albums have a size limit of 1000 photos. It is strongly recommended that you scale the image in your application before adding it to the request. The largest dimension should be at most 604 pixels (the largest display size Facebook supports).</remarks>
        public void UploadAsync(string aid, string caption, byte[] data, string contentType, UploadCallback callback, Object state)
        {
            Upload(aid, caption, data, contentType, true, callback, state);
        }

        /// <summary>
        /// Uploads a photo owned by the current session user and returns the new photo.
        /// </summary>
        /// <example>
        /// <code> 
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        ///     var aid = Constants.AlbumId;
        ///     var caption = "caption";
        ///     var filePath = @"C:\Clarity.jpg";
        ///     var fileStream = System.IO.File.OpenRead(filePath);
        ///     var reader = new System.IO.BinaryReader(fileStream);
        ///     var fileData = reader.ReadBytes((int)fileStream.Length);
        ///     api.Photos.UploadAsync(aid, caption, System.IO.Path.GetFileName(filePath), fileData, "image/jpeg", AsyncDemoCompleted, null);
        /// }
        /// 
        /// private static void AsyncDemoCompleted(photo result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="aid">Optional - The album id of the destination album.</param>
        /// <param name="caption">Optional - The caption of the photo.</param>
        /// <param name="data">The raw image data for the photo.</param>
        /// <param name="fileType">One of the image file types</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>Photo information, including the photo URL.</returns>
        /// <remarks>If no album is specified, the photo will be uploaded to a default album for the application, which will be created if necessary. Regular albums have a size limit of 60 photos. Default application albums have a size limit of 1000 photos. It is strongly recommended that you scale the image in your application before adding it to the request. The largest dimension should be at most 604 pixels (the largest display size Facebook supports).</remarks>
        public void UploadAsync(string aid, string caption, byte[] data, Enums.FileType fileType, UploadCallback callback, Object state)
        {
            Upload(aid, caption, data, _mimeTypes[fileType], true, callback, state);
        }

        #endregion Asynchronous Methods

        #endregion Public Methods

        #region Private Methods

        private bool AddTag(string pid, long tag_uid, string tag_text, float x, float y, long owner_uid
            ,bool isAsync, AddTagCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> {{"method", "facebook.photos.addTag"}};
			Utilities.AddRequiredParameter(parameterList, "pid", pid);
			Utilities.AddOptionalParameter(parameterList, "tag_uid", tag_uid);
			Utilities.AddOptionalParameter(parameterList, "tag_text", tag_text);
            Utilities.AddOptionalParameter(parameterList, "owner_uid", owner_uid);
            Utilities.AddRequiredParameter(parameterList, "x", x);
            Utilities.AddRequiredParameter(parameterList, "y", y);

            if (isAsync)
            {
                SendRequestAsync<photos_addTag_response, bool>(parameterList, owner_uid <= 0, new FacebookCallCompleted<bool>(callback), state);
                return true;
            }

		    var response = SendRequest<photos_addTag_response>(parameterList, owner_uid <= 0);
			return response == null ? false : response.TypedValue;
		}

		private album CreateAlbum(string name, string location, string description, long uid
            ,bool isAsync, CreateAlbumCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> {{"method", "facebook.photos.createAlbum"}};
			Utilities.AddRequiredParameter(parameterList, "name", name);
			Utilities.AddOptionalParameter(parameterList, "location", location);
			Utilities.AddOptionalParameter(parameterList, "description", description);
            Utilities.AddOptionalParameter(parameterList, "uid", uid);
        
            if (isAsync)
            {
                SendRequestAsync(parameterList, uid <= 0, new FacebookCallCompleted<album>(callback), state);
                return null;
            }

			return SendRequest<photos_createAlbum_response>(parameterList, uid <= 0);
        }

        private IList<photo> Get(string subj_id, string aid, List<string> pids
            ,bool isAsync, GetCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> {{"method", "facebook.photos.get"}};
			Utilities.AddOptionalParameter(parameterList, "subj_id", subj_id);
			Utilities.AddOptionalParameter(parameterList, "aid", aid);
			Utilities.AddList(parameterList, "pids", pids);
            
            if (isAsync)
            {
                SendRequestAsync<photos_get_response, IList<photo>>(parameterList, new FacebookCallCompleted<IList<photo>>(callback), state, "photo");
                return null;
            }

			var response = SendRequest<photos_get_response>(parameterList);
			return response == null ? null : response.photo;
        }

        private IList<album> GetAlbums(long uid, List<string> aids, bool isAsync, GetAlbumsCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> {{"method", "facebook.photos.getAlbums"}};
			Utilities.AddOptionalParameter(parameterList, "uid", uid);
			Utilities.AddList(parameterList, "aids", aids);
            
            if (isAsync)
            {
                SendRequestAsync<photos_getAlbums_response, IList<album>>(parameterList, new FacebookCallCompleted<IList<album>>(callback), state, "album");
               return null;
            }

			var response = SendRequest<photos_getAlbums_response>(parameterList);
			return response == null ? null : response.album;
        }

        private IList<photo_tag> GetTags(List<string> pids, bool isAsync, GetTagsCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> {{"method", "facebook.photos.getTags"}};
			Utilities.AddList(parameterList, "pids", pids);
            
            if (isAsync)
            {
                SendRequestAsync<photos_getTags_response, IList<photo_tag>>(parameterList, new FacebookCallCompleted<IList<photo_tag>>(callback), state, "photo_tag");
                return null;
            }

			var response = SendRequest<photos_getTags_response>(parameterList);
			return response == null ? null : response.photo_tag;
        }

        private void Upload(string aid, string caption, byte[] rawData, string contentType, bool isAsync, 
            UploadCallback callback, Object state)
        {
            Upload(aid, caption, null, 0, rawData, contentType, isAsync, callback, state);
        }

        private photo Upload(string aid, string caption, FileSystemInfo data, long uid, byte[] rawData, string contentType,
            bool isAsync, UploadCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> {{"method", "facebook.photos.upload"}};
			Utilities.AddOptionalParameter(parameterList, "aid", aid);
			Utilities.AddOptionalParameter(parameterList, "caption", caption);
            Utilities.AddOptionalParameter(parameterList, "uid", uid);
            
            if (isAsync)
            {
                AsyncResult ar = new AsyncResult(OnFacebookCallCompleted<photo>, new FacebookCallCompleted<photo>(callback), state);
                UploadFile(parameterList, rawData, contentType, ar);
                return null;
            }

#if !SILVERLIGHT
            var response = ExecuteApiImageUpload(data, parameterList);
            return !string.IsNullOrEmpty(response) ? Utilities.DeserializeXML<photos_upload_response>(response) : null;
#else
            return null;
#endif
		}
        
        #endregion Private Methods
        
        #endregion Methods

        #region Delegates

        /// <summary>
        /// Delegate called when AddPhotoTag call completed
        /// </summary>
        /// <param name="result">result of operation</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void AddTagCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when CreateAlbum call completed
        /// </summary>
        /// <param name="album">album that was created</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void CreateAlbumCallback(album album, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetPhotos call completed
        /// </summary>
        /// <param name="photos">list of photos</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetCallback(IList<photo> photos, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetPhotos call completed
        /// </summary>
        /// <param name="albums">list of albums</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetAlbumsCallback(IList<album> albums, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when GetPhotos call completed
        /// </summary>
        /// <param name="tags">list of tags</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetTagsCallback(IList<photo_tag> tags, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when PhotoUpload call completed
        /// </summary>
        /// <param name="photo">information about uploaded photo</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void UploadCallback(photo photo, Object state, FacebookException e);
        
        #endregion Delegates
    }
}
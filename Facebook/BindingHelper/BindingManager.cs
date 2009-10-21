using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows;
using Facebook.BindingHelper;
using Facebook.Rest;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;
using System.Xml;
using System.Xml.Linq;

namespace Facebook.BindingHelper
{

    internal delegate void GetProfileCallback(FacebookProfileCollection profiles, Object state, FacebookException e);
    internal delegate void GetStreamCommentsCallback(ActivityCommentCollection comments, Object state, FacebookException e);

    /// <summary>
    /// Facebook service object
    /// </summary>
    public class BindingManager : INotifyPropertyChanged
    {
        // predefine some constant
        const string ProfileUrl = "http://www.facebook.com/profile.php?id={0}";

        // Temporary set some limit on the FQL query
        private const int FQLFriendsAlbumLimit = 100;
        private const string FQLPhotosByUserLimit = " LIMIT 100";

        // Cache locations in Isolated Storage.

        private FacebookDataCache _cache;
        #region Queries

        private const string _UserFields = "about_me, activities, birthday, books, first_name, interests, last_name, name, pic_big, pic_small, pic_square, pic, profile_update_time, quotes, status, timezone, uid, hometown_location, current_location";
        private const string _AlbumFields = "aid, cover_pid, owner, name, created, modified, description, location, link, size, visible";
        private const string _PhotoFields = "pid, aid, owner, src, src_big, src_small, link, caption, created";
        private const string _ProfileFields = "id, name, url, pic, pic_big, pic_square, pic_small";
        private const string _CommentsField = "id, fromid, post_id, time, text";
        private const string _StreamTableColumns = "post_id, viewer_id, app_id, source_id, updated_time, created_time, filter_key, attribution, actor_id, target_id, message, app_data, action_links, attachment, comments, likes, privacy, type";
        private const string _PhotoTagFields = "pid, subject, text, xcoord, ycoord, created";
        private const string _FriendListFields = "flid, name, owner";

        private const string _SelectFriendsClause = "(SELECT uid2 FROM friend WHERE uid1={0})";
        private const string _GetUsersQueryString = "SELECT " + _UserFields + " FROM user WHERE uid IN ({0})" + " Order by last_name";
        private const string _GetFriendQueryString = "SELECT " + _UserFields + " FROM user WHERE uid IN " + _SelectFriendsClause + " Order by last_name";
        private const string _GetAlbumsQueryString = "SELECT " + _AlbumFields + " FROM album WHERE owner={0} ORDER BY modified DESC";
        private const string _GetAlbumsByIdQueryString = "SELECT " + _AlbumFields + " FROM album WHERE aid in ({0}) ORDER BY modified DESC";
        private const string _GetFriendsAlbumsQueryString = "SELECT " + _AlbumFields + " FROM album WHERE owner IN " + _SelectFriendsClause;
        private const string _GetStreamPostsQueryString = "SELECT " + _StreamTableColumns + " FROM stream WHERE filter_key = '{0}' ORDER BY created_time DESC";
        private const string _GetStreamPostsCreatedQueryString = "SELECT " + _StreamTableColumns + " FROM stream WHERE filter_key = '{0}' AND created_time < {1} ORDER BY created_time DESC";
        private const string _GetAllStreamPostsQueryString = "SELECT " + _StreamTableColumns + " FROM stream WHERE filter_key in (SELECT filter_key FROM stream_filter WHERE uid={0}) AND updated_time > {1} ORDER BY updated_time ASC";
        private const string _GetFriendListsQueryString = "SELECT " + _FriendListFields + " FROM friendlist WHERE owner={0}";

        private const string _GetProfileQueryString = "SELECT " + _ProfileFields + " FROM profile WHERE id in ({0})";
        private const string _GetPhotosQueryString = "SELECT " + _PhotoFields + " FROM photo where aid = '{0}'";
        private const string _GetPhotosOfUserQueryString = "SELECT " + _PhotoFields + " FROM photo where pid in (SELECT pid from photo_tag where subject = {0}) ORDER BY created DESC";
        private const string _GetPhotosByUserQueryString = "SELECT " + _PhotoFields + " FROM photo where aid in (SELECT aid from album where owner = {0} ORDER BY created DESC) ORDER BY created DESC" + FQLPhotosByUserLimit;
        private const string _GetCommentsQueryString = "SELECT " + _CommentsField + " FROM comment where post_id = '{0}' ORDER BY time DESC";
        private const string _GetCommentsLastModifiedQueryString = "SELECT " + _CommentsField + " FROM comment where post_id = '{0}' AND time > {1} ORDER BY time DESC";
        private const string _GetAlbumCoverQueryString = "SELECT " + _PhotoFields + " FROM photo where pid in (select cover_pid from #Albums)";
        private const string _GetAlbumOwnerQueryString = "SELECT " + _UserFields + " FROM user where uid in (select owner from #Albums)";
        private const string _GetPhotoTagsQueryString = "SELECT " + _PhotoTagFields + " FROM photo_tag where pid in (select pid from #Photos)";
        private const string _GetPhotoOwnerQueryString = "SELECT " + _UserFields + " FROM user where uid in (select owner from #Photos)";
        private const string _GetCommentsProfileQueryString = "SELECT " + _ProfileFields + " FROM profile where id in (select fromid from #Comments)";
        private const string _GetPhotoAlbumsQueryString = "SELECT " + _AlbumFields + " FROM album where aid in (select aid from #Photos)";
        #endregion

        #region Members

        Api _fbApi;
        FacebookSession _session;

        #endregion

        #region Events
        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Login completed events
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> LoginCompletedEvent;

        /// <summary>
        /// Logout completed events
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> LogoutCompletedEvent;

        #endregion

        # region Properties

        /// <summary>
        /// Facebook api object
        /// </summary>
        public Api Api
        {
            get
            {
                return _fbApi;
            }
        }


        FacebookContactCollection _friends;
        /// <summary>
        /// Friends collection of logged in user
        /// </summary>
        public FacebookContactCollection Friends
        {
            get
            {
                lock (this)
                {
                    if (_friends == null)
                    {
                        _friends = new FacebookContactCollection();
                        this.RefreshFriends();
                    }
                }
                return _friends;
            }
            private set
            {
                _friends = value;
                this.NotifyPropertyChanged(PropertyChanged, s => s.Friends);
            }

        }

        FacebookPhotoAlbumCollection _albums;
        /// <summary>
        /// Albums collection of logged in user
        /// </summary>
        public FacebookPhotoAlbumCollection MyPhotoAlbums
        {
            get
            {
                lock (this)
                {
                    if (_albums == null)
                    {
                        _albums = GetAlbums(this._session.UserId);
                    }
                }
                return _albums;
            }
            private set
            {
                _albums = value;
                this.NotifyPropertyChanged(PropertyChanged, s => s.MyPhotoAlbums);
            }

        }

        FacebookPhotoAlbumCollection _friendsAlbums;
        /// <summary>
        /// Albums collections of all user friends
        /// </summary>
        public FacebookPhotoAlbumCollection FriendsAlbums
        {
            get
            {
                lock (this)
                {
                    if (_friendsAlbums == null)
                    {
                        _friendsAlbums = new FacebookPhotoAlbumCollection();
                        this.RefreshFriendsAlbums();
                    }
                }
                return _friendsAlbums;
            }
            private set
            {
                _friendsAlbums = value;
                this.NotifyPropertyChanged(PropertyChanged, s => s.FriendsAlbums);
            }
        }

        ActivityPostCollection _stream;

        /// <summary>
        /// Stream information for current user
        /// </summary>
        public ActivityPostCollection Stream
        {
            get
            {
                lock (this)
                {
                    if (_stream == null)
                    {
                        _stream = new ActivityPostCollection();
                        _fbApi.Stream.GetAsync(_session.UserId, null, null, null, null, null, OnGetStream, _stream);
                    }
                }
                return _stream;
            }
            private set
            {
                _stream = value;
                this.NotifyPropertyChanged(PropertyChanged, s => s.Stream);
            }

        }

        FacebookNotificationInfo _notifications;
        /// <summary>
        /// Notifications for current user
        /// </summary>
        public FacebookNotificationInfo Notifications
        {
            get
            {
                lock (this)
                {
                    if (_notifications == null)
                    {
                        _notifications = GetNotifications();
                    }
                }
                return _notifications;
            }

            private set
            {
                _notifications = value;
                this.NotifyPropertyChanged(PropertyChanged, s => s.Notifications);
            }
        }


        /// <summary>
        /// User information about currently logged in user
        /// </summary>
        public FacebookContact CurrentUser
        {
            get;
            private set;
        }

        /// <summary>
        /// UserID about currently logged in user
        /// </summary>
        internal long CurrentUserId
        {
            get
            {
                return _session.UserId;
            }
        }

        /// <summary>
        /// Gets an instance of this object
        /// </summary>
        internal static BindingManager Instance
        {
            get;
            private set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Created an instance of this object.
        /// We always create new instance here for login/logout event.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        static public BindingManager CreateInstance(FacebookSession session)
        {
            Instance = new BindingManager(session);
            return Instance;
        }


        /// <summary>
        /// Initializes new BindingManager object
        /// </summary>
        /// <param name="session">Session object</param>
        private BindingManager(FacebookSession session)
        {
            _session = session;
            _fbApi = new Api(session);
            _cache = new FacebookDataCache();
            Initialize();
        }

        /// <summary>
        /// Initializes new BindingManager object
        /// </summary>
        /// <param name="appKey">Application Key</param>
        /// <param name="appSecret">Application Secret key</param>
        private BindingManager(string appKey, string appSecret)
        {
            //Check if we can use browsersession, if not fall back to desktop session
#if SILVERLIGHT
            try
            {
                _session = new BrowserSession(appKey);
            }
            catch (FacebookException)
            {
                _session = new CachedSession(appKey, null, null);
            }
#else
            _session = new DesktopSession(appKey,null,null,true);
#endif

            _session.LoginCompleted += new EventHandler<AsyncCompletedEventArgs>(session_LoginCompleted);
            _session.LogoutCompleted += new EventHandler<AsyncCompletedEventArgs>(session_LogoutCompleted);
        }


        /// <summary>
        /// Logs in the user using appkey and secret provided
        /// </summary>
        public void Login()
        {
            _session.Login();
        }

        /// <summary>
        /// Refreshes current user albums that is downloaded by service object
        /// </summary>
        public void RefreshCurrentUserAlbums()
        {
            this.GetAlbumsAndMerge(this._session.UserId, MyPhotoAlbums);
        }

        /// <summary>
        /// Refreshes friends albums that is downloaded by service object
        /// </summary>
        public void RefreshFriendsAlbums()
        {
            DateTime lastUpdated = DateTime.UtcNow.AddDays(-30);
            if (_friendsAlbums.Count > 0)
                lastUpdated = _friendsAlbums[0].ModifiedTime;

            this.GetFriendsAlbums(FQLFriendsAlbumLimit, lastUpdated, null, false);
        }

        /// <summary>
        /// Refreshes friends that is downloaded by service object
        /// </summary>
        public void RefreshFriends()
        {
            string query = string.Format(_GetFriendQueryString, _session.UserId);
            ExecuteFqlQuery<users_getInfo_response>(query, OnGetFriendsCompleted, _friends);
        }

        /// <summary>
        /// Checks if given user is a friend
        /// </summary>
        /// <param name="userId">userId to check against</param>
        /// <returns>true if user is friend</returns>
        public bool IsFriend(long userId)
        {
            if (_cache.GetUser(userId) != null)
                return true;
            return false;
        }

        /// <summary>
        /// Executes an fql query and deserialiizes it to type specified
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize</typeparam>
        /// <param name="query">Query string</param>
        /// <param name="callback">Callback to be called when request completes</param>
        /// <param name="state">User state object</param>
        public void ExecuteFqlQuery<T>(string query, Fql.QueryCallback<T> callback, Object state)
        {
            _fbApi.Fql.QueryAsync<T>(query, callback, state);
        }

        /// <summary>
        /// Executes an fql query to get the albums of the friends
        /// </summary>
        /// <param name="limit">The maximum number of albums to return</param>
        /// <param name="startTime">Only albums after this date</param>
        /// <param name="endTime">Only albums before this date</param>
        /// <param name="getProfilePictureAlbums">Indicator if we want to include profile picture albums</param>
        public FacebookPhotoAlbumCollection GetFriendsAlbums(int? limit, DateTime? startTime, DateTime? endTime, bool? getProfilePictureAlbums)
        {
            string query = string.Format(_GetFriendsAlbumsQueryString, _session.UserId);

            if (startTime != null || startTime != null || getProfilePictureAlbums != null)
            {
                query += " AND ";
            }

            if (startTime != null)
            {
                query += string.Format("modified > {0} ", DateHelper.ConvertDateToFacebookDate(startTime.Value));
            }

            if (endTime != null)
            {
                query += string.Format("AND modified < {0} ", DateHelper.ConvertDateToFacebookDate(endTime.Value));
            }

            if (getProfilePictureAlbums != null && !getProfilePictureAlbums.Value)
            {
                query += "AND name != 'Profile Pictures' ";
            }

            query += "ORDER BY modified DESC";

            if (limit != null)
            {
                query += string.Format(" LIMIT {0}", limit.Value);
            }


            GetAlbumsWithCoverPid(query, _friendsAlbums);
            return _friendsAlbums;
        }



        /// <summary>
        /// Gets photo information for given album id or photo ids. Either albumid or
        /// photoids need to be specified
        /// </summary>
        /// <param name="albumid">Album id</param>
        /// <returns>FacebookPhotoCollection collection object</returns>
        public FacebookPhotoCollection GetPhotos(string albumid)
        {
            FacebookPhotoCollection photos = new FacebookPhotoCollection();
            string query = string.Format(_GetPhotosQueryString, albumid);
            GetPhotosWithTags(query, photos);
            return photos;
        }

        /// <summary>
        /// Gets photo information for photos in which the given user is tagged
        /// </summary>
        /// <param name="userId">User Id of the tagged user</param>
        /// <returns></returns>
        public FacebookPhotoCollection GetPhotosOf(long userId)
        {
            FacebookPhotoCollection photos = new FacebookPhotoCollection();
            string query = string.Format(_GetPhotosOfUserQueryString, userId);
            GetPhotosWithTags(query, photos);
            return photos;
        }

        /// <summary>
        /// Gets photo information for most recent photos added by the given user
        /// </summary>
        /// <param name="userId">User Id of the user</param>
        /// <returns></returns>
        public FacebookPhotoCollection GetPhotosBy(long userId)
        {
            FacebookPhotoCollection photos = new FacebookPhotoCollection();
            string query = string.Format(_GetPhotosByUserQueryString, userId);
            GetPhotosWithTags(query, photos);
            return photos;
        }

        /// <summary>
        /// Retrieves information for given set of user ids
        /// </summary>
        /// <param name="userIds">userids for users for which informaiton needs to be retrieved</param>
        /// <returns>FacebookUsersCollection object</returns>
        public FacebookContactCollection GetUsers(long[] userIds)
        {
            List<long> uids = new List<long>(userIds.Length);
            FacebookContactCollection users = new FacebookContactCollection();

            foreach (long id in userIds)
            {
                FacebookContact user = _cache.GetUser(id);
                if (user != null)
                {
                    users.Add(user);
                }
                else
                {
                    uids.Add(id);
                }
            }

            if (uids.Count != 0)
            {
                string query = string.Format(_GetUsersQueryString, StringHelper.ConvertToCommaSeparated(uids.ToArray()));
                ExecuteFqlQuery<users_getInfo_response>(query, GetUserInfoCompleted, users);
            }
            return users;
        }

        /// <summary>
        /// Updates the user status
        /// </summary>
        /// <param name="newStatus">new status string</param>
        public void UpdateStatus(string newStatus)
        {
            this.Api.Status.SetAsync(newStatus, (b, o, e) => { }, null);
        }

        /// <summary>
        /// Gets stream data.
        /// </summary>
        /// <param name="userIds">The list of IDs of the users used to filter the posts.</param>
        /// <param name="startTime">The earliest time  for which to retrieve posts from the stream.</param>
        /// <param name="endTime">The latest time  for which to retrieve posts from the stream</param>
        /// <param name="limit">The total number of posts to return.</param>
        /// <param name="filter">User specified filter</param>
        public ActivityPostCollection GetStream(List<string> userIds, DateTime? startTime, DateTime? endTime, int? limit, string filter)
        {
            ActivityPostCollection posts = new ActivityPostCollection();
            _fbApi.Stream.GetAsync(_session.UserId, userIds, startTime, endTime, limit, filter, OnGetStream, posts);
            return posts;
        }

        /// <summary>
        /// Gets stream data for given filter key
        /// </summary>
        /// <param name="filterKey">Filter key</param>
        /// <param name="limit">Optional limit on number of items to retrieve.</param>
        /// <param name="createdTime">Optional datetime used to filter items that have created_time less than specified value.</param>
        /// <returns>ActivityPostCollection collection object</returns>
        public ActivityPostCollection GetStream(string filterKey, int? limit, DateTime? createdTime)
        {
            string query = string.Empty;

            if (createdTime.HasValue)
            {
                long facebookDate = DateHelper.ConvertDateToFacebookDate(createdTime.Value);
                query = string.Format(_GetStreamPostsCreatedQueryString, filterKey, facebookDate);
            }
            else
                query = string.Format(_GetStreamPostsQueryString, filterKey);

            if (limit != null)
            {
                query += " LIMIT " + limit.Value.ToString();
            }

            return GetActivityPostCollection(query);
        }

        private ActivityPostCollection GetActivityPostCollection(string query)
        {
            ActivityPostCollection posts = new ActivityPostCollection();

            FqlMultiQueryInfo[] queries = new FqlMultiQueryInfo[]
            {
                new FqlMultiQueryInfo()
                {
                    Key = "Stream",
                    Query = query,
                    Type = typeof(stream_dataPosts_response),
                },
                new FqlMultiQueryInfo()
                {
                    Key = "ProfileInfo",
                    Query = "select " + _ProfileFields + " from profile where id in (select actor_id from #Stream)",
                    Type = typeof(stream_dataProfiles_response),
                },
                new FqlMultiQueryInfo()
                {
                    Key = "ProfileInfo2",
                    Query = "select " + _ProfileFields + " from profile where id in (select fromid from comment where post_id in (select post_id from #Stream))",
                    Type = typeof(stream_dataProfiles_response),
                }
            };

            Api.Fql.MultiqueryAsync(queries, OnGetActivityPostCollectionCompleted, posts);
            return posts;
        }

        /// <summary>
        /// Gets all posts since a given datetime value
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="lastModified">Optional datetime used for obtaining incremental changes (i.e. only those comments that have been modified since specified value)</param>
        /// <returns></returns>
        public ActivityPostCollection GetAllStreams(long userId, DateTime lastModified)
        {
            long facebookDate = 0;
            ActivityPostCollection posts = new ActivityPostCollection();

            if (lastModified != null)
                facebookDate = DateHelper.ConvertDateToFacebookDate(lastModified);

            string query = string.Format(_GetAllStreamPostsQueryString, userId, facebookDate);

            return GetActivityPostCollection(query);
        }

        /// <summary>
        /// Search the users stream
        /// </summary>
        /// <param name="keywords">keywords to use in the search</param>
        /// <param name="userId">userid to search</param>
        /// <param name="globalSearch">global search or not</param>
        /// <param name="limit">maximum results to return</param>
        public ActivityPostCollection SearchOnStream(string keywords, long userId, bool globalSearch, int? limit)
        {
            string query = string.Format("SELECT {0} FROM stream WHERE CONTAINS('{1}')", _StreamTableColumns, keywords);

            if (!globalSearch)
            {
                query += string.Format(" AND (actor_id IN (SELECT uid2 FROM friend WHERE uid1 = {0}) OR actor_id = {0})", userId);
            }

            if(limit != null)
                query += " LIMIT " + limit.ToString();

            return GetActivityPostCollection(query);
        }

        /// <summary>
        /// Gets albums form given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public FacebookPhotoAlbumCollection GetAlbums(long userId)
        {
            FacebookPhotoAlbumCollection albums = new FacebookPhotoAlbumCollection();
            GetAlbumsAndMerge(userId, albums);
            return albums;
        }

        /// <summary>
        /// Makes an async request to get album matching given album id
        /// </summary>
        /// <param name="albumIds">id of album</param>
        /// <returns>Albumcollection object containing the album</returns>
        public FacebookPhotoAlbumCollection GetAlbums(string[] albumIds)
        {
            return GetAlbums(albumIds, true);
        }

        /// <summary>
        /// Makes an async request to get album matching given album id, specifying whether to use cache
        /// </summary>
        /// <param name="albumIds">id of album</param>
        /// <param name="useCache">whether to cache the results or not</param>
        /// <returns>Albumcollection object containing the album</returns>
        internal FacebookPhotoAlbumCollection GetAlbums(string[] albumIds, bool useCache)
        {
            FacebookPhotoAlbumCollection albums = new FacebookPhotoAlbumCollection();
            List<string> aids = new List<string>();

            foreach (string albumId in albumIds)
            {
                FacebookPhotoAlbum album = useCache ? _cache.GetAlbum(albumId) : null;

                if (album != null)
                {
                    albums.Add(album);
                }
                else
                {
                    aids.Add(albumId);
                }
            }

            if (aids.Count != 0)
            {
                string query = string.Format(_GetAlbumsByIdQueryString, StringHelper.ConvertToCommaSeparated(aids.ToArray()));
                GetAlbumsWithCoverPid(query, albums);
            }

            return albums;
        }

        /// <summary>
        /// Gets profiles for given user ids
        /// </summary>
        /// <param name="userIds">array of userIds</param>
        /// <returns>FacebookProfile object</returns>
        public FacebookProfileCollection GetProfiles(long[] userIds)
        {
            List<long> uids = new List<long>();
            FacebookProfileCollection profiles = new FacebookProfileCollection();

            foreach (long uid in userIds)
            {
                FacebookProfile profile = _cache.GetProfile(uid);
                if (profile != null)
                {
                    profiles.Add(profile);
                }
                else
                {
                    uids.Add(uid);
                }
            }

            if (uids.Count != 0)
            {
                GetProfileAsync(uids.ToArray(), profiles);
            }

            return profiles;
        }

        #endregion

        #region Private Helpers

        void Initialize()
        {
            GetLoggedInUserInfo();
        }

        /// <summary>
        /// Login completed event from session object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void session_LoginCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Initialize();
            }

            if (LoginCompletedEvent != null)
            {
                LoginCompletedEvent(this, new AsyncCompletedEventArgs(e.Error, false, null));
            }
        }

        /// <summary>
        /// Logout completed event from session object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void session_LogoutCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (LogoutCompletedEvent != null)
            {
                LogoutCompletedEvent(this, new AsyncCompletedEventArgs(e.Error, false, null));
            }
        }

        void GetLoggedInUserInfo()
        {
            FacebookContact user = _cache.GetUser(this._session.UserId);

            if (user == null)
            {
                string query = string.Format(_GetUsersQueryString, this._session.UserId);
                ExecuteFqlQuery<users_getInfo_response>(query, GetUserInfoCompleted, null);
            }
            else
            {
                SetCurrentUserInfo(user);
            }
        }

        void UpdateAlbumsInfo(FacebookPhotoAlbumCollection albums)
        {
            if (albums.Count != 0)
            {
                List<string> photoIds = new List<string>();
                Dictionary<string, FacebookPhotoAlbum> hash = new Dictionary<string, FacebookPhotoAlbum>();

                foreach (FacebookPhotoAlbum album in albums)
                {
                    //if album is already in cache, we have all the info.
                    if (_cache.GetAlbum(album.AlbumId) != null)
                    {
                        continue;
                    }

                    _cache.AddAlbum(album);
                    if (album.CoverPhoto == null && album.CoverPid != "0")
                    {
                        photoIds.Add(album.CoverPid);
                        hash.Add(album.CoverPid, album);
                    }
                }

                if (photoIds.Count != 0)
                {
                    Photos photos = new Photos(this._session);
                    photos.GetAsync(null, null, photoIds, OnGetAlbumCoverPhotos, hash);
                }
            }
        }


        void GetAlbumsWithCoverPid(string query, Object state)
        {
            FqlMultiQueryInfo[] queries = new FqlMultiQueryInfo[]
            {
                new FqlMultiQueryInfo()
                {
                    Key = "Albums",
                    Query = query,
                    Type = typeof(photos_getAlbums_response),
                },
                new FqlMultiQueryInfo()
                {
                    Key = "Photos",
                    Query = _GetAlbumCoverQueryString,
                    Type = typeof(photos_get_response),
                },
                new FqlMultiQueryInfo()
                {
                    Key = "OwnerInfo",
                    Query = _GetAlbumOwnerQueryString,
                    Type = typeof(users_getInfo_response),
                },
            };

            Api.Fql.MultiqueryAsync(queries, OnGetAlbumsMultiQueryCompleted, state);
        }

        void GetPhotosWithTags(string query, Object state)
        {
            FqlMultiQueryInfo[] queries = new FqlMultiQueryInfo[]
            {
                new FqlMultiQueryInfo()
                {
                    Key = "Photos",
                    Query = query,
                    Type = typeof(photos_get_response),
                },
                new FqlMultiQueryInfo()
                {
                    Key = "OwnerInfo",
                    Query = _GetPhotoOwnerQueryString,
                    Type = typeof(users_getInfo_response),
                },
                new FqlMultiQueryInfo()
                {
                    Key = "PhotoTags",
                    Query = _GetPhotoTagsQueryString,
                    Type = typeof(photos_getTags_response),
                },
                new FqlMultiQueryInfo()
                {
                    Key = "PhotoAlbums",
                    Query = _GetPhotoAlbumsQueryString,
                    Type = typeof(photos_getAlbums_response),
                },
            };

            Api.Fql.MultiqueryAsync(queries, OnGetPhotosMultiQueryCompleted, state);
        }

        void SetCurrentUserInfo(FacebookContact user)
        {
            CurrentUser = user;
            _cache.AddUser(user);

            FacebookProfile profile = new FacebookProfile()
            {
                Name = CurrentUser.Name,
                UserId = CurrentUser.UserId,
                Picture = CurrentUser.Picture,
                PictureSquare = CurrentUser.PictureSquare,
                PictureSmall = CurrentUser.PictureSmall,
                PictureBig = CurrentUser.PictureBig,
                Url = string.Format(ProfileUrl, CurrentUser.UserId),
            };

            _cache.AddProfile(profile);
            this.NotifyPropertyChanged(PropertyChanged, s => s.CurrentUser);
        }

        FacebookNotificationInfo GetNotifications()
        {
            FacebookNotificationInfo notifications = new FacebookNotificationInfo();
            this._fbApi.Notifications.GetAsync(OnGetNotifications, null);
            return notifications;
        }

        void GetAlbumsAndMerge(long userId, FacebookPhotoAlbumCollection albums)
        {
            string query = string.Format(_GetAlbumsQueryString, userId);
            GetAlbumsWithCoverPid(query, albums);
        }

        #endregion

        #region FacebookApi Callbacks

        void GetUserInfoCompleted(users_getInfo_response users, Object o, FacebookException e)
        {
            FacebookContactCollection userCollection = o as FacebookContactCollection;

            if (e == null && users != null)
            {
                if (userCollection == null)
                {
                    SetCurrentUserInfo(new FacebookContact(users.user[0]));
                }
                else
                {
                    foreach (user user in users.user)
                    {
                        _cache.AddUser(new FacebookContact(user));
                    }
                    userCollection.Merge(new FacebookContactCollection(from user in users.user select new FacebookContact(user)));
                }
            }

            if (userCollection != null)
            {
                userCollection.OnDataRetrievalComplete(e);
            }
        }

        void OnGetFriendsCompleted(users_getInfo_response friends, Object userState, FacebookException e)
        {
            if (e == null && friends != null)
            {
                _friends.Merge(new FacebookContactCollection(from user in friends.user select new FacebookContact(user)));

                foreach (user user in friends.user)
                {
                    _cache.AddUser(new FacebookContact(user));
                }
            }

            _friends.OnDataRetrievalComplete(e);
        }

        void OnGetStream(stream_data data, Object state, FacebookException e)
        {
            ActivityPostCollection posts = (ActivityPostCollection)state;

            if (e == null && data != null)
            {
                if (data.albums != null)
                {
                    var albums = new FacebookPhotoAlbumCollection(from album in data.albums.album select new FacebookPhotoAlbum(album));
                    UpdateAlbumsInfo(albums);
                }

                var activityPosts = from p in data.posts.stream_post select new ActivityPost(p);
                posts.Merge(activityPosts);

                if (data.profiles != null)
                {
                    foreach (profile p in data.profiles.profile)
                    {
                        if (_cache.GetProfile(p.id) == null)
                            _cache.AddProfile(new FacebookProfile(p));
                    }
                }
            }

            posts.OnDataRetrievalComplete(e);
        }

        void OnGetFqlGetCommentsCompleted(IList<object> results, Object state, FacebookException[] exceptions)
        {
            AsyncResult result = (AsyncResult)state;
            GetStreamCommentsCallback callback = (GetStreamCommentsCallback)result.AsyncState;
            FacebookException e = null;
            ActivityCommentCollection comments = null;

            if (exceptions[0] != null || exceptions[1] != null)
            {
                e = new FacebookException("Could not get comments", exceptions[0] ?? exceptions[1]);
            }
            else
            {

                List<FacebookComment> commentsList = new List<FacebookComment>(from c in ((comments_get_response)results[0]).comment select new FacebookComment(c)); 
                FacebookProfileCollection profiles = new FacebookProfileCollection(from p in ((stream_dataProfiles_response)results[1]).profile select new FacebookProfile(p));

                if (profiles != null)
                {
                    foreach (FacebookProfile profile in profiles)
                    {
                        _cache.AddProfile(profile);
                    }
                }

                comments = new ActivityCommentCollection();

                if (commentsList != null)
                {
                    commentsList.Reverse();
                    foreach (FacebookComment c in commentsList)
                    {
                        comments.Add(new ActivityComment(c));
                    }
                }
            }

            if (callback != null)
            {
                callback(comments, result.AsyncExternalState, e);
            }


        }

        void OnGetProfile(stream_dataProfiles_response profiles, Object o, FacebookException e)
        {
            if (e == null)
            {
                FacebookProfileCollection pc = (FacebookProfileCollection)o;

                if (profiles != null)
                {
                    foreach (profile p in profiles.profile)
                    {
                        _cache.AddProfile(new FacebookProfile(p));

                        if (pc != null)
                        {
                            pc.AddInternal(new FacebookProfile(p));
                        }
                    }
                }
            }
        }

        void OnGetAlbumCoverPhotos(IList<photo> photos, Object userState, FacebookException e)
        {
            if (e == null && photos != null)
            {
                Dictionary<string, FacebookPhotoAlbum> hash = (Dictionary<string, FacebookPhotoAlbum>)userState;
                foreach (photo photo in photos)
                {
                    FacebookPhotoAlbum album = hash[photo.pid];
                    album.CoverPhoto = new FacebookPhoto(photo);
                }
            }
        }

        void OnGetAlbumsMultiQueryCompleted(IList<object> results, Object state, FacebookException[] exceptions)
        {
            FacebookPhotoAlbumCollection albumCollection = (FacebookPhotoAlbumCollection)state;

            if ((exceptions[0] != null) || (exceptions[1] != null) || (exceptions[2] != null) || results[0] == null)
            {
                albumCollection.OnDataRetrievalComplete(new FacebookException("Could not get Albums", exceptions[0] ?? exceptions[1] ?? exceptions[2]));
                return;
            }

            Dictionary<string, FacebookPhoto> hash = new Dictionary<string, FacebookPhoto>();
            FacebookPhotoAlbumCollection albums = null;
            FacebookPhotoCollection photos = null;
            FacebookContactCollection users = null;

            albums = new FacebookPhotoAlbumCollection(
                from a in ((photos_getAlbums_response)results[0]).album select new FacebookPhotoAlbum(a));

            if (results[1] != null)
            {
                photos = new FacebookPhotoCollection(
                    from p in ((photos_get_response)results[1]).photo select new FacebookPhoto(p));
            }
            if (results[2] != null)
            {
                users = new FacebookContactCollection(
                    from u in ((users_getInfo_response)results[2]).user select new FacebookContact(u));
            } 
            

            if (users != null)
            {
                foreach (FacebookContact user in users)
                {
                    _cache.AddUser(user);
                }
            }

            if (photos != null)
            {
                foreach (FacebookPhoto photo in photos)
                {
                    hash.Add(photo.PhotoId, photo);
                }
            }

            foreach (FacebookPhotoAlbum album in albums)
            {
                if (album.CoverPid != null)
                {
                    if (hash.ContainsKey(album.CoverPid))
                    {
                        album.CoverPhoto = hash[album.CoverPid];
                    }
                }
                _cache.AddAlbum(album);
            }

            if (albumCollection.Count == 0)
                albumCollection.Merge(albums);
            else
                albumCollection.InsertRange(0, albums);

            albumCollection.OnDataRetrievalComplete(null);
        }

        void OnGetPhotosMultiQueryCompleted(IList<object> results, Object state, FacebookException[] exceptions)
        {
            FacebookPhotoCollection photoCollection = (FacebookPhotoCollection)state;

            if ((exceptions[0] != null) || (exceptions[1] != null) || (exceptions[2] != null))
            {
                photoCollection.OnDataRetrievalComplete(new FacebookException("Could not get Photos", exceptions[0] ?? exceptions[1] ?? exceptions[2]));
                return;
            }

            FacebookPhotoCollection photos = null;
            FacebookPhotoTagCollection tags = null;
            FacebookContactCollection users = null;
            FacebookPhotoAlbumCollection albums = null;

            if (results[0] != null)
            {
                photos = new FacebookPhotoCollection(
                    from p in ((photos_get_response)results[0]).photo select new FacebookPhoto(p));
            }

            if (results[1] != null)
            {
                users  = new FacebookContactCollection(
                    from c in ((users_getInfo_response)results[1]).user select new FacebookContact(c));
            }

            if (results[2] != null)
            {
                tags = new FacebookPhotoTagCollection(((photos_getTags_response)results[2]).photo_tag);
            }

            if (results[3] != null)
            {
                albums = new FacebookPhotoAlbumCollection(from a in ((photos_getAlbums_response)results[3]).album select new FacebookPhotoAlbum(a));
            }


            if (users != null)
            {
                foreach (FacebookContact user in users)
                {
                    _cache.AddUser(user);
                }
            }

            if (albums != null)
            {
                foreach (FacebookPhotoAlbum album in albums)
                {
                    _cache.AddAlbum(album);
                }
            }

            Dictionary<string, FacebookPhoto> hash = new Dictionary<string, FacebookPhoto>();

            if (photos != null)
            {
                foreach (FacebookPhoto photo in photos)
                {
                    hash.Add(photo.PhotoId, photo);
                }
            }

            if (tags != null)
            {
                foreach (FacebookPhotoTag tag in tags)
                {
                    if (hash.ContainsKey(tag.PhotoId))
                    {
                        hash[tag.PhotoId].Tags.Add(tag);
                    }
                }
            }

            if (photos != null)
            {
                photoCollection.Merge(photos);
            }

            photoCollection.OnDataRetrievalComplete(null);
        }

        void OnGetActivityPostCollectionCompleted(IList<object> results, Object state, FacebookException[] exceptions)
        {
            ActivityPostCollection posts = state as ActivityPostCollection;

            if ((exceptions[0] != null) || (exceptions[1] != null) || (exceptions[2] != null))
            {
                posts.OnDataRetrievalComplete(new FacebookException("Could not get stream data", exceptions[0] ?? exceptions[1] ?? exceptions[2]));
                return;
            }

            //  0=FacebookStreamPostCollection, 1&2=FacebookProfileCollection 
            for (int index = 1; index <= 2; ++index)
            {
                if (results[index] != null)
                {
                    FacebookProfileCollection profiles = new FacebookProfileCollection(from p in ((stream_dataProfiles_response)results[index]).profile select new FacebookProfile(p));
                    foreach (FacebookProfile profile in profiles)
                    {
                        if (_cache.GetProfile(profile.UserId) == null)
                            _cache.AddProfile(profile);
                    }
                }
            }

            if (results[0] != null)
            {
                FacebookStreamPostCollection facebookStreamPostCollection = new FacebookStreamPostCollection(from p in ((stream_dataPosts_response)results[0]).stream_post select new FacebookStreamPost(p));

                List<ActivityPost> newPosts = new List<ActivityPost>();

                foreach (FacebookStreamPost p in facebookStreamPostCollection)
                {
                    ActivityPost post = new ActivityPost(p);
                    newPosts.Add(post);
                }

                posts.Merge(newPosts);
            }
            posts.OnDataRetrievalComplete(null);
        }

        void OnGetNotifications(notifications notificationInfo, Object o, FacebookException e)
        {
            if (e == null)
            {
                _notifications = new FacebookNotificationInfo(notificationInfo);
                this.NotifyPropertyChanged(PropertyChanged, s => s.Notifications);
            }
        }

        #endregion

        #region Internal functions

        internal FacebookContact GetUserFromCache(long ownerid)
        {
            return _cache.GetUser(ownerid);
        }

        internal FacebookProfile GetProfileFromCache(long userId)
        {
            return _cache.GetProfile(userId);
        }

        internal void GetProfileAsync(long[] userIds, Object state)
        {
            string query = string.Format(_GetProfileQueryString, StringHelper.ConvertToCommaSeparated(userIds));
            ExecuteFqlQuery<stream_dataProfiles_response>(query, OnGetProfile, state);
        }

        /// <summary>
        /// Gets new comments for given post, in ascending order
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="limit">Optional number of coments to get</param>
        /// <param name="lastModified">Option datetime used for obtaining incremental changes (i.e. only those comments that have been modified since specified value)</param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        internal void GetComments(string postId, int? limit, DateTime? lastModified, GetStreamCommentsCallback callback, Object state)
        {
            string commentsQueryString = string.Empty;
            if (lastModified.HasValue)
            {
                long facebookDate = DateHelper.ConvertDateToFacebookDate(lastModified.Value);
                commentsQueryString = string.Format(_GetCommentsLastModifiedQueryString, postId, facebookDate);
            }
            else
                commentsQueryString = string.Format(_GetCommentsQueryString, postId);

            if (limit.HasValue)
                commentsQueryString += " LIMIT " + limit.Value.ToString();

            this.GetCommentsEx(commentsQueryString, callback, state);
        }


        private void GetCommentsEx(string commentsQueryString, GetStreamCommentsCallback callback, Object state)
        {
            FqlMultiQueryInfo[] queries = new FqlMultiQueryInfo[]
            {
                new FqlMultiQueryInfo()
                {
                    Key = "Comments",
                    Query = commentsQueryString,
                    Type = typeof(comments_get_response),
                },
                new FqlMultiQueryInfo()
                {
                    Key = "Profiles",
                    Query = _GetCommentsProfileQueryString,
                    Type = typeof(stream_dataProfiles_response),
                },
            };

            AsyncResult result = new AsyncResult(null, callback, state);
            Api.Fql.MultiqueryAsync(queries, OnGetFqlGetCommentsCompleted, result);
        }

        #endregion
    }

}

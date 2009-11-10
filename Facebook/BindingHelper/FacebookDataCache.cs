using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Facebook.Utility;

namespace Facebook.BindingHelper
{

    internal class FacebookDataCache
    {
        FacebookHashTable<long, FacebookContact> _userCache = new FacebookHashTable<long, FacebookContact>();
        FacebookHashTable<long, FacebookProfile> _profileCache = new FacebookHashTable<long, FacebookProfile>();
        FacebookHashTable<string, FacebookPhotoAlbum> _albumCache = new FacebookHashTable<string, FacebookPhotoAlbum>();

        public IEnumerable<FacebookPhotoAlbum> GetAlbums(long userId)
        {
            return from a in _albumCache.Values.ToList() where a.OwnerId == userId select a;
        }

        public FacebookPhotoAlbum GetAlbum(string albumId)
        {
            return _albumCache.GetValue(albumId);
        }

        public FacebookContact GetUser(long userId)
        {
            return _userCache.GetValue(userId);
        }

        public FacebookProfile GetProfile(long userId)
        {
            return _profileCache.GetValue(userId);
        }

        public void AddUser(FacebookContact user)
        {
            _userCache.SetValue(user.UserId, user);
        }

        public void AddProfile(FacebookProfile profile)
        {
            _profileCache.SetValue(profile.UserId, profile);
        }

        public void AddAlbum(FacebookPhotoAlbum album)
        {
            _albumCache.SetValue(album.AlbumId, album);
        }

    }
}

using System;
namespace Facebook.Rest
{
	public interface IPhotos : IAuthenticatedService
	{
		bool AddTag(string pid, long tag_uid, string tag_text, float x, float y);
		bool AddTag(string pid, long tag_uid, string tag_text, float x, float y, long owner_uid);
		bool AddTag(string pid, long? tag_uid, string tag_text, float x, float y);
		void AddTagAsync(string pid, long tag_uid, string tag_text, float x, float y, Photos.AddTagCallback callback, object state);
		void AddTagAsync(string pid, long tag_uid, string tag_text, float x, float y, long owner_uid, Photos.AddTagCallback callback, object state);
		void AddTagAsync(string pid, long? uid, string tag_text, float x, float y, Photos.AddTagCallback callback, object state);
		Facebook.Schema.album CreateAlbum(string name, string location, string description);
		Facebook.Schema.album CreateAlbum(string name, string location, string description, long uid);
		void CreateAlbumAsync(string name, string location, string description, Photos.CreateAlbumCallback callback, object state);
		void CreateAlbumAsync(string name, string location, string description, long uid, Photos.CreateAlbumCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.photo> Get(string subj_id, string aid, System.Collections.Generic.List<string> pids);
		System.Collections.Generic.IList<Facebook.Schema.album> GetAlbums();
		System.Collections.Generic.IList<Facebook.Schema.album> GetAlbums(System.Collections.Generic.List<string> aids);
		System.Collections.Generic.IList<Facebook.Schema.album> GetAlbums(long uid);
		System.Collections.Generic.IList<Facebook.Schema.album> GetAlbums(long uid, System.Collections.Generic.List<string> aids);
		void GetAlbumsAsync(Photos.GetAlbumsCallback callback, object state);
		void GetAlbumsAsync(System.Collections.Generic.List<string> aids, Photos.GetAlbumsCallback callback, object state);
		void GetAlbumsAsync(long uid, Photos.GetAlbumsCallback callback, object state);
		void GetAlbumsAsync(long uid, System.Collections.Generic.List<string> aids, Photos.GetAlbumsCallback callback, object state);
		void GetAsync(string subj_id, string aid, System.Collections.Generic.List<string> pids, Photos.GetCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.photo_tag> GetTags(System.Collections.Generic.List<string> pids);
		void GetTagsAsync(System.Collections.Generic.List<string> pids, Photos.GetTagsCallback callback, object state);
		Facebook.Schema.photo Upload(string aid, string caption, System.IO.FileInfo data);
		Facebook.Schema.photo Upload(string aid, string caption, System.IO.FileInfo data, long uid);
		void UploadAsync(string aid, string caption, byte[] data, Facebook.Schema.Enums.FileType fileType, Photos.UploadCallback callback, object state);
		void UploadAsync(string aid, string caption, byte[] data, string contentType, Photos.UploadCallback callback, object state);
	}
}

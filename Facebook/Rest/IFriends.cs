using System;
namespace Facebook.Rest
{
	public interface IFriends : IRestBase
	{
		System.Collections.Generic.IList<Facebook.Schema.friend_info> AreFriends(Facebook.Schema.user user1, Facebook.Schema.user user2);
		System.Collections.Generic.IList<Facebook.Schema.friend_info> AreFriends(System.Collections.Generic.List<Facebook.Schema.user> uids1, System.Collections.Generic.List<Facebook.Schema.user> uids2);
		System.Collections.Generic.IList<Facebook.Schema.friend_info> AreFriends(System.Collections.Generic.List<long> uids1, System.Collections.Generic.List<long> uids2);
		System.Collections.Generic.IList<Facebook.Schema.friend_info> AreFriends(long uid1, long uid2);
		void AreFriendsAsync(Facebook.Schema.user user1, Facebook.Schema.user user2, Friends.AreFriendsCallback callback, object state);
		void AreFriendsAsync(System.Collections.Generic.List<Facebook.Schema.user> uids1, System.Collections.Generic.List<Facebook.Schema.user> uids2, Friends.AreFriendsCallback callback, object state);
		void AreFriendsAsync(System.Collections.Generic.List<long> uids1, System.Collections.Generic.List<long> uids2, Friends.AreFriendsCallback callback, object state);
		void AreFriendsAsync(long uid1, long uid2, Friends.AreFriendsCallback callback, object state);
		System.Collections.Generic.IList<long> Get();
		System.Collections.Generic.IList<long> Get(long uid);
		System.Collections.Generic.IList<long> Get(long uid, long flid);
		System.Collections.Generic.IList<long> GetAppUsers();
		void GetAppUsersAsync(Friends.GetAppUsersCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.user> GetAppUsersObjects();
		void GetAppUsersObjectsAsync(Users.GetInfoCallback callback, object state);
		void GetAsync(Friends.GetFriendsCallback callback, object state);
		void GetAsync(long uid, Friends.GetFriendsCallback callback, object state);
		void GetAsync(long uid, long flid, Friends.GetFriendsCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.friendlist> GetLists();
		void GetListsAsync(Friends.GetListsCallback callback, object state);
		System.Collections.Generic.List<long> GetMutualFriends(long target_uid);
		System.Collections.Generic.List<long> GetMutualFriends(long target_uid, long? source_uid);
		void GetMutualFriendsAsync(long target_uid, Friends.GetMutualFriendsCallback callback, object state);
		void GetMutualFriendsAsync(long target_uid, long? source_uid, Friends.GetMutualFriendsCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.user> GetUserObjects();
		System.Collections.Generic.IList<Facebook.Schema.user> GetUserObjects(long uid);
		System.Collections.Generic.IList<Facebook.Schema.user> GetUserObjects(long uid, long flid);
		void GetUserObjectsAsync(Users.GetInfoCallback callback, object state);
		void GetUserObjectsAsync(long uid, Users.GetInfoCallback callback, object state);
		void GetUserObjectsAsync(long uid, long flid, Users.GetInfoCallback callback, object state);
	}
}

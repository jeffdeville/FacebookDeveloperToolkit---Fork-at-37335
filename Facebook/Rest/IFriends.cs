using System.Collections.Generic;
using Facebook.Schema;

namespace Facebook.Rest
{
	public interface IFriends : IAuthorizedRestBase
	{
		IList<friend_info> AreFriends(user user1, user user2);
		IList<friend_info> AreFriends(IList<user> uids1, IList<user> uids2);
		IList<friend_info> AreFriends(IList<long> uids1, IList<long> uids2);
		IList<friend_info> AreFriends(long uid1, long uid2);
		void AreFriendsAsync(user user1, user user2, Friends.AreFriendsCallback callback, object state);
		void AreFriendsAsync(IList<user> uids1, IList<user> uids2, Friends.AreFriendsCallback callback, object state);
		void AreFriendsAsync(IList<long> uids1, IList<long> uids2, Friends.AreFriendsCallback callback, object state);
		void AreFriendsAsync(long uid1, long uid2, Friends.AreFriendsCallback callback, object state);
		IList<long> Get();
		IList<long> Get(long uid);
		IList<long> Get(long uid, long flid);
		IList<long> GetAppUsers();
		void GetAppUsersAsync(Friends.GetAppUsersCallback callback, object state);
		IList<user> GetAppUsersObjects();
		void GetAppUsersObjectsAsync(Users.GetInfoCallback callback, object state);
		void GetAsync(Friends.GetFriendsCallback callback, object state);
		void GetAsync(long uid, Friends.GetFriendsCallback callback, object state);
		void GetAsync(long uid, long flid, Friends.GetFriendsCallback callback, object state);
		IList<friendlist> GetLists();
		void GetListsAsync(Friends.GetListsCallback callback, object state);
		IList<long> GetMutualFriends(long target_uid);
		IList<long> GetMutualFriends(long target_uid, long? source_uid);
		void GetMutualFriendsAsync(long target_uid, Friends.GetMutualFriendsCallback callback, object state);
		void GetMutualFriendsAsync(long target_uid, long? source_uid, Friends.GetMutualFriendsCallback callback, object state);
		IList<user> GetUserObjects();
		IList<user> GetUserObjects(long uid);
		IList<user> GetUserObjects(long uid, long flid);
		void GetUserObjectsAsync(Users.GetInfoCallback callback, object state);
		void GetUserObjectsAsync(long uid, Users.GetInfoCallback callback, object state);
		void GetUserObjectsAsync(long uid, long flid, Users.GetInfoCallback callback, object state);
	}
}
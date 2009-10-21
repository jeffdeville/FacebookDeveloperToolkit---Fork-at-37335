using System;
namespace Facebook.Rest
{
	public interface IConnect : IRestBase
	{
		int GetUnconnectedFriends();
		void GetUnconnectedFriendsAsync(Connect.GetUnconnectedFriendsCallback callback, object state);
		System.Collections.Generic.IList<string> RegisterUsers(System.Collections.Generic.List<ConnectAccountMap> accounts);
		void RegisterUsersAsync(System.Collections.Generic.List<ConnectAccountMap> accounts, Connect.RegisterUsersCallback callback, object state);
		System.Collections.Generic.IList<string> UnregisterUsers(System.Collections.Generic.List<string> email_hashes);
		void UnregisterUsersAsync(System.Collections.Generic.List<string> email_hashes, Connect.UnregisterUsersCallback callback, object state);
	}
}

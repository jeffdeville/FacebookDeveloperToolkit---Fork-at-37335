using System;
namespace Facebook.Rest
{
	public interface IGroups : IAuthorizedRestBase
	{
		System.Collections.Generic.IList<Facebook.Schema.group> Get();
		System.Collections.Generic.IList<Facebook.Schema.group> Get(System.Collections.Generic.List<long> gids);
		System.Collections.Generic.IList<Facebook.Schema.group> Get(long uid);
		System.Collections.Generic.IList<Facebook.Schema.group> Get(long uid, System.Collections.Generic.List<long> gids);
		void GetAsync(Groups.GetCallback callback, object state);
		void GetAsync(System.Collections.Generic.List<long> gids, Groups.GetCallback callback, object state);
		void GetAsync(long uid, Groups.GetCallback callback, object state);
		void GetAsync(long uid, System.Collections.Generic.List<long> gids, Groups.GetCallback callback, object state);
		Facebook.Schema.group_members GetMembers(long gid);
		void GetMembersAsync(long gid, Groups.GetMembersCallback callback, object state);
	}
}

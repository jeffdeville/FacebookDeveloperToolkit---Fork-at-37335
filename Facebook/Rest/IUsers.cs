using System.Collections.Generic;
using Facebook.Schema;

namespace Facebook.Rest
{
	public interface IUsers : IRestBase
	{
		user GetInfo();
		IList<user> GetInfo(IList<long> uids);
		user GetInfo(long uid);
		void GetInfoAsync(Users.GetInfoCallback callback, object state);
		void GetInfoAsync(IList<long> uids, Users.GetInfoCallback callback, object state);
		void GetInfoAsync(long uid, Users.GetInfoCallback callback, object state);
		void GetInfoAsync(string uids, Users.GetInfoCallback callback, object state);
		long GetLoggedInUser();
		void GetLoggedInUserAsync(Users.GetLoggedInUserCallback callback, object state);
		IList<user> GetStandardInfo(List<long> uids);
		IList<user> GetStandardInfo(List<long> uids, IList<string> fields);
		IList<user> GetStandardInfo(string uids);
		IList<user> GetStandardInfo(string uids, IList<string> fields);
		void GetStandardInfoAsync(List<long> uids, Users.GetStandardInfoCallback callback, object state);
		void GetStandardInfoAsync(List<long> uids, IList<string> fields, Users.GetStandardInfoCallback callback, object state);
		void GetStandardInfoAsync(string uids, Users.GetStandardInfoCallback callback, object state);
		void GetStandardInfoAsync(string uids, IList<string> fields, Users.GetStandardInfoCallback callback, object state);
		bool HasAppPermission(Enums.ExtendedPermissions ext_perm);
		bool HasAppPermission(Enums.ExtendedPermissions ext_perm, long uid);
		void HasAppPermissionAsync(Enums.ExtendedPermissions ext_perm, Users.HasAppPermissionCallback callback, object state);

		void HasAppPermissionAsync(Enums.ExtendedPermissions ext_perm, long uid, Users.HasAppPermissionCallback callback,
		                           object state);

		bool IsAppUser(long uid);
		void IsAppUserAsync(long uid, Users.IsAppUserCallback callback, object state);
		bool IsVerified(long uid);
		void IsVerifiedAsync(long uid, Users.IsVerifiedCallback callback, object state);
		bool SetStatus(string status);
		bool SetStatus(string status, bool status_includes_verb);
		bool SetStatus(string status, bool status_includes_verb, long uid);
		bool SetStatus(string status, long uid);
		bool SetStatusAsync(string status, Users.SetStatusCallback callback, object state);
		bool SetStatusAsync(string status, bool status_includes_verb, Users.SetStatusCallback callback, object state);
		bool SetStatusAsync(string status, bool status_includes_verb, long uid, Users.SetStatusCallback callback, object state);
		bool SetStatusAsync(string status, long uid, Users.SetStatusCallback callback, object state);
	}
}
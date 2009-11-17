using System;
namespace Facebook.Rest
{
	public interface IUsers : IRestBase
	{
		Facebook.Schema.user GetInfo();
		System.Collections.Generic.IList<Facebook.Schema.user> GetInfo(System.Collections.Generic.List<long> uids);
		Facebook.Schema.user GetInfo(long uid);
		void GetInfoAsync(Users.GetInfoCallback callback, object state);
		void GetInfoAsync(System.Collections.Generic.List<long> uids, Users.GetInfoCallback callback, object state);
		void GetInfoAsync(long uid, Users.GetInfoCallback callback, object state);
		void GetInfoAsync(string uids, Users.GetInfoCallback callback, object state);
		long GetLoggedInUser();
		void GetLoggedInUserAsync(Users.GetLoggedInUserCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.user> GetStandardInfo(System.Collections.Generic.List<long> uids);
		System.Collections.Generic.IList<Facebook.Schema.user> GetStandardInfo(System.Collections.Generic.List<long> uids, System.Collections.Generic.IList<string> fields);
		System.Collections.Generic.IList<Facebook.Schema.user> GetStandardInfo(string uids);
		System.Collections.Generic.IList<Facebook.Schema.user> GetStandardInfo(string uids, System.Collections.Generic.IList<string> fields);
		void GetStandardInfoAsync(System.Collections.Generic.List<long> uids, Users.GetStandardInfoCallback callback, object state);
		void GetStandardInfoAsync(System.Collections.Generic.List<long> uids, System.Collections.Generic.IList<string> fields, Users.GetStandardInfoCallback callback, object state);
		void GetStandardInfoAsync(string uids, Users.GetStandardInfoCallback callback, object state);
		void GetStandardInfoAsync(string uids, System.Collections.Generic.IList<string> fields, Users.GetStandardInfoCallback callback, object state);
		bool HasAppPermission(Facebook.Schema.Enums.ExtendedPermissions ext_perm);
		bool HasAppPermission(Facebook.Schema.Enums.ExtendedPermissions ext_perm, long uid);
		void HasAppPermissionAsync(Facebook.Schema.Enums.ExtendedPermissions ext_perm, Users.HasAppPermissionCallback callback, object state);
		void HasAppPermissionAsync(Facebook.Schema.Enums.ExtendedPermissions ext_perm, long uid, Users.HasAppPermissionCallback callback, object state);
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

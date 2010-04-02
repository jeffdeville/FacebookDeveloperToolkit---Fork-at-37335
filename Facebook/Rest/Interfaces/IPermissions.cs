using System;
namespace Facebook.Rest
{
	public interface IPermissions
	{
		void BeginPermissionsMode(string callAsApiKey);
		System.Collections.Generic.IList<string> CheckAvailableApiAccess(string permissions_apikey);
		System.Collections.Generic.IList<string> CheckAvailableApiAccessAsync(string permissions_apikey, Permissions.CheckAvailableApiAccessCallback callback, object state);
		System.Collections.Generic.IList<string> CheckGrantedApiAccess(string permissions_apikey);
		System.Collections.Generic.IList<string> CheckGrantedApiAccessAsync(string permissions_apikey, Permissions.CheckGrantedApiAccessCallback callback, object state);
		void EndPermissionsMode(string callAsApiKey);
		bool GrantApiAccess(string apiKeyGrantedAccess, System.Collections.Generic.List<string> method_arr);
		bool GrantApiAccessAsync(string apiKeyGrantedAccess, System.Collections.Generic.List<string> method_arr, Permissions.GrantApiAccessCallback callback, object state);
		bool RevokeApiAccess(string permissions_apikey);
		bool RevokeApiAccessAsync(string permissions_apikey, Permissions.RevokeApiAccessCallback callback, object state);
	}
}

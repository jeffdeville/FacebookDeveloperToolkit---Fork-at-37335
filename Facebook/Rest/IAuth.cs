using System;
namespace Facebook.Rest
{
	public interface IAuth : IRestBase
	{
		string CreateToken();
		void CreateTokenAsync(Auth.CreateTokenCallback callback, object state);
		bool ExpireSession();
		void ExpireSessionAsync(Auth.ExpireSessionCallback callback, object state);
		Facebook.Schema.session_info GetSession();
		Facebook.Schema.session_info GetSession(string auth_token);
		void GetSessionAsync(string authToken, Auth.GetSessionCallback callback, object state);
		string PromoteSession();
		void PromoteSessionAsync(Auth.PromoteSessionCallback callback, object state);
		void RevokeAuthorization();
		void RevokeAuthorizationAsync(Auth.RevokeAuthorizationCallback callback, object state);
		bool RevokeExtendedPermission(Facebook.Schema.Enums.ExtendedPermissions ext_perm);
		bool RevokeExtendedPermission(Facebook.Schema.Enums.ExtendedPermissions ext_perm, long uid);
		void RevokeExtendedPermissionAsync(Facebook.Schema.Enums.ExtendedPermissions ext_perm, Auth.RevokeExtendedPermissionCallback callback, object state);
		void RevokeExtendedPermissionAsync(Facebook.Schema.Enums.ExtendedPermissions ext_perm, long uid, Auth.RevokeExtendedPermissionCallback callback, object state);
	}
}

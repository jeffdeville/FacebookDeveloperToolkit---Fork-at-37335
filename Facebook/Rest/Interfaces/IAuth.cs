using Facebook.Schema;

namespace Facebook.Rest
{
	public interface ILoggedInAuth : IAuthenticatedService
	{
		bool ExpireSession();
		void ExpireSessionAsync(LoggedInAuth.ExpireSessionCallback callback, object state);
		void RevokeAuthorization();
		void RevokeAuthorizationAsync(LoggedInAuth.RevokeAuthorizationCallback callback, object state);
		bool RevokeExtendedPermission(Enums.ExtendedPermissions ext_perm);
		bool RevokeExtendedPermission(Enums.ExtendedPermissions ext_perm, long uid);

		void RevokeExtendedPermissionAsync(Enums.ExtendedPermissions ext_perm,
		                                   LoggedInAuth.RevokeExtendedPermissionCallback callback, object state);

		void RevokeExtendedPermissionAsync(Enums.ExtendedPermissions ext_perm, long uid,
		                                   LoggedInAuth.RevokeExtendedPermissionCallback callback, object state);
	}

	public interface IAuth : IFacebookNetworkWrapper
	{
		string CreateToken();
		void CreateTokenAsync(Auth.CreateTokenCallback callback, object state);

		session_info GetSession();
		session_info GetSession(string auth_token);
		string ProxyGetSession(string authtoken, string generate_session_secret);
		void GetSessionAsync(string authToken, Auth.GetSessionCallback callback, object state);
		string PromoteSession();
		void PromoteSessionAsync(Auth.PromoteSessionCallback callback, object state);
	}
}
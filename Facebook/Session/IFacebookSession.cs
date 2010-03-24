using System;
using System.Collections.Generic;
using Facebook.Schema;

namespace Facebook.Session
{
	public interface IFacebookSession
	{
		List<Enums.ExtendedPermissions> RequiredPermissions { get; set; }
		string CheckPermissions();
		string ApplicationKey { get; }
		string ApplicationSecret { get; }
		bool CompressHttp { get; set; }
		DateTime ExpiryTime { get; }
		bool SessionExpires { get; set; }
		string SessionKey { get; set; }
		string SessionSecret { get; set; }
		string Secret { get; }
		long UserId { get; set; }
	}

	public interface IFacebookSessionManagement
	{
		void Login();
		void Logout();
	}

	public interface IFacebookAuthorization
	{		
		string CheckPermissions(List<Enums.ExtendedPermissions> requiredPermissions);
		string GetPermissionUrl(string permissionString);
		string GetPermissionUrl(string permissionString, string nextUrl);
	}
}
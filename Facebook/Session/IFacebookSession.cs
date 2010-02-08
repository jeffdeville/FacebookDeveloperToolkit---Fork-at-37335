using System;
using System.Collections.Generic;
using Facebook.Schema;

namespace Facebook.Session
{
	public interface IFacebookSession
	{
		string ApplicationKey { get; set; }
		string ApplicationSecret { get; set; }
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
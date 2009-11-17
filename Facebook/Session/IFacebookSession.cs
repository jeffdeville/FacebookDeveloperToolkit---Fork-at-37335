using System;
namespace Facebook.Session
{
	public interface IFacebookSession
	{
		string ApplicationKey { get; set; }
		string ApplicationSecret { get; set; }
		string CheckPermissions();
		bool CompressHttp { get; set; }
		DateTime ExpiryTime { get; }
		string GetPermissionUrl(string permissionString);
		string GetPermissionUrl(string permissionString, string nextUrl);
		void Login();
		event EventHandler<System.ComponentModel.AsyncCompletedEventArgs> LoginCompleted;
		void Logout();
		event EventHandler<System.ComponentModel.AsyncCompletedEventArgs> LogoutCompleted;
		System.Collections.Generic.List<Facebook.Schema.Enums.ExtendedPermissions> RequiredPermissions { get; set; }
		bool SessionExpires { get; set; }
		string SessionKey { get; set; }
		string SessionSecret { get; set; }
		string Secret { get; }
		long UserId { get; set; }
	}
}

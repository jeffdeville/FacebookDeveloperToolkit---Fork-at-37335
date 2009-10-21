using System;
using Facebook.Session;

namespace Facebook.Rest
{
	public interface IFacebookApi : IRestBase
	{
		IFacebookApi Initialize(FacebookSession session);
		IAdmin Admin { get; }
		IApplication Application { get; }
		IAuth Auth { get; }
		string AuthToken { get; set; }
		IComments Comments { get; }
		IConnect Connect { get; }
		IData Data { get; }
		IEvents Events { get; }
		string ExtendedPermissionUrl(Facebook.Schema.Enums.ExtendedPermissions permission);
		IFbml Fbml { get; }
		IFeed Feed { get; }
		IFql Fql { get; }
		IFriends Friends { get; }
		IGroups Groups { get; }
		IIntl Intl { get; }
		ILinks Links { get; }
		ILiveMessage LiveMessage { get; }
		string LoginUrl { get; }
		string LogOffUrl { get; }
		IMarketplace Marketplace { get; }
		IMessage Message { get; }
		INotes Notes { get; }
		INotifications Notifications { get; }
		IPages Pages { get; }
		IPhotos Photos { get; }
		IProfile Profile { get; }
		IStatus Status { get; }
		IStream Stream { get; }
		IUsers Users { get; }
		IVideo Video { get; }
	}
}

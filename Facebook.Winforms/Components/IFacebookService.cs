using System;
namespace Facebook.Winforms.Components
{
	interface IFacebookService
	{
		Facebook.Rest.IAdmin Admin { get; }
		Facebook.Rest.IFacebookApi Api { get; }
		Facebook.Rest.IApplication Application { get; }
		string ApplicationKey { get; set; }
		Facebook.Rest.IBatch Batch { get; }
		Facebook.Rest.IComments Comments { get; }
		void ConnectToFacebook();
		void ConnectToFacebook(System.Collections.Generic.List<Facebook.Schema.Enums.ExtendedPermissions> permissions);
		Facebook.Rest.IData Data { get; }
		Facebook.Rest.IEvents Events { get; }
		Facebook.Rest.IFbml Fbml { get; }
		Facebook.Rest.IFeed Feed { get; }
		Facebook.Rest.IFql Fql { get; }
		Facebook.Rest.IFriends Friends { get; }
		Facebook.Rest.IGroups Groups { get; }
		Facebook.Rest.IIntl Intl { get; }
		Facebook.Rest.ILinks Links { get; }
		Facebook.Rest.ILiveMessage LiveMessage { get; }
		void LogOff();
		Facebook.Rest.IMarketplace Marketplace { get; }
		Facebook.Rest.IMessage Message { get; }
		Facebook.Rest.INotifications Notifications { get; }
		Facebook.Rest.IPages Pages { get; }
		Facebook.Rest.IPermissions Permissions { get; }
		Facebook.Rest.IPhotos Photos { get; }
		Facebook.Rest.IProfile Profile { get; }
		//string Secret { get; set; }
		bool SessionExpires { get; }
		string SessionKey { get; set; }
		Facebook.Rest.IStatus Status { get; }
		Facebook.Rest.IStream Stream { get; }
		long uid { get; set; }
		Facebook.Rest.IUsers Users { get; }
		Facebook.Rest.IVideo Video { get; }
	}
}

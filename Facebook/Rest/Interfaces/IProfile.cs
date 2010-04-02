using System;
namespace Facebook.Rest
{
	public interface IProfile : IAuthenticatedService
	{
		string GetFBML(long uid, int type);
		void GetFBMLAsync(long uid, int type, Profile.GetFBMLCallback callback, object state);
		Facebook.Schema.user_info GetInfo(long uid);
		void GetInfoAsync(long uid, Profile.GetInfoCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.info_item> GetInfoOptions(string field);
		void GetInfoOptionsAsync(string field, Profile.GetInfoOptionsCallback callback, object state);
		bool SetFBML(long uid, string profile, string profile_main, string mobile_profile);
		void SetFBMLAsync(long uid, string profile, string profile_main, string mobile_profile, Profile.SetFBMLCallback callback, object state);
		bool SetInfo(string title, int type, System.Collections.Generic.List<Facebook.Schema.info_field> info_fields, long uid);
		void SetInfoAsync(string title, int type, System.Collections.Generic.List<Facebook.Schema.info_field> info_fields, long uid, Profile.SetInfoCallback callback, object state);
		bool SetInfoOptions(string field, System.Collections.Generic.List<Facebook.Schema.info_item> options);
		void SetInfoOptionsAsync(string field, System.Collections.Generic.List<Facebook.Schema.info_item> options, Profile.SetInfoCallback callback, object state);
	}
}

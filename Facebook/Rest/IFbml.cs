using System;
namespace Facebook.Rest
{
	public interface IFbml : IRestBase
	{
		bool DeleteCustomTags(System.Collections.Generic.List<string> names);
		void DeleteCustomTagsAsync(System.Collections.Generic.List<string> names, Fbml.DeleteCustomTagsCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.custom_tag> GetCustomTags();
		System.Collections.Generic.IList<Facebook.Schema.custom_tag> GetCustomTags(string app_id);
		void GetCustomTagsAsync(Fbml.GetCustomTagsCallback callback, object state);
		void GetCustomTagsAsync(string app_id, Fbml.GetCustomTagsCallback callback, object state);
		bool RefreshImgSrc(string url);
		void RefreshImgSrcAsync(string url, Fbml.RefreshImgSrcCallback callback, object state);
		bool RefreshRefUrl(string url);
		void RefreshRefUrlAsync(string url, Fbml.RefreshRefUrlCallback callback, object state);
		int RegisterCustomTags(System.Collections.Generic.List<CustomTag> tags);
		void RegisterCustomTagsAsync(System.Collections.Generic.List<CustomTag> tags, Fbml.RegisterCustomTagsCallback callback, object state);
		bool SetRefHandle(string handle, string fbml);
		void SetRefHandleAsync(string handle, string fbml, Fbml.SetRefHandleCallback callback, object state);
		bool UploadNativeStrings(System.Collections.Generic.Dictionary<string, string> native_strings);
		void UploadNativeStringsAsync(System.Collections.Generic.Dictionary<string, string> native_strings, Fbml.UploadNativeStringsCallback callback, object state);
	}
}

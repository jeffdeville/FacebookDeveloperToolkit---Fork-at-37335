using System;
namespace Facebook.Rest
{
	public interface IIntl : IRestBase
	{
		System.Collections.Generic.List<Facebook.Schema.locale_data> GetTranslations(string locale, bool all);
		void GetTranslationsAsync(string locale, bool all, Intl.GetTranslationsCallback callback, object state);
		long UploadNativeStrings(System.Collections.Generic.List<native_string> native_strings);
		void UploadNativeStringsAsync(System.Collections.Generic.List<native_string> native_strings, Intl.UploadNativeStringsCallback callback, object state);
	}
}

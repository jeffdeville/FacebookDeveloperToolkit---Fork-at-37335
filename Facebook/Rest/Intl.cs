using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
	/// <summary>
	/// Facebook Application API methods.
	/// </summary>
	public class Intl : AuthorizedRestBase, Facebook.Rest.IIntl
	{
		#region Methods

		#region Constructor

		/// <summary>
		/// Public constructor for facebook.Application
		/// </summary>
		/// <param name="session">Needs a connected Facebook Session object for making requests</param>
		public Intl(IFacebookSession session)
			: base(session)
		{
		}

		#endregion Constructor

		#region Public Methods

#if !SILVERLIGHT

		#region Synchronous Methods

        /// <summary>
        /// Returns an array of strings from your application that you submitted for translation. This call returns the original native strings, the best (or all) translations of native strings into a given locale, whether the string has been approved, and by whom. 
        /// By default, the translation for a given string that gets returned is the current best translation, as it appears in the Facebook Translations application database. The translation was approved either automatically by the Translations application or was manually approved by you, another developer of your application, or a translator with whom you have a contract. 
        /// If there is no such translation, then the native string doesn't get returned. 
        /// You do not have to pass a session key with this method. However, you must include your application secret. 
        /// </summary>
        /// <param name="locale">The locale from where you are retrieving the translated strings. To return translated strings from every locale where someone has translated your application, specify all for locale (however, this is not recommended for performance reasons). Locales are of the format ll_CC, where ll is a two-letter language code (in lowercase), and CC is a two-letter country code (in uppercase). For a list of locales Facebook supports, see Facebook Locales. If the default is used, then only the native strings get returned. (Default value is en_US.) </param>
        /// <param name="all">When true, this call returns every translation for every native string. When false, this call returns the best translation for every native string. (Default value is false.) </param>
		public List<locale_data> GetTranslations(string locale, bool all)
		{
			return GetTranslations(locale, all, false, null, null);
		}

        /// <summary>
        /// Lets you insert text strings in their native language into the Facebook Translations database so they can be translated. See Translating Platform Applications for more information about translating your applications. 
        /// </summary>
        /// <param name="native_strings">A JSON-encoded array of strings to translate. Each element of the string array is an object, with text storing the actual string, description storing the description of the text.</param>
        /// <returns>If successful, this method returns the number of strings uploaded. </returns>
        public long UploadNativeStrings(List<native_string> native_strings)
		{
			return UploadNativeStrings(native_strings, false, null, null);
		}

		#endregion

#endif

		#region Asynchronous Methods

        /// <summary>
        /// Returns an array of strings from your application that you submitted for translation. This call returns the original native strings, the best (or all) translations of native strings into a given locale, whether the string has been approved, and by whom. 
        /// By default, the translation for a given string that gets returned is the current best translation, as it appears in the Facebook Translations application database. The translation was approved either automatically by the Translations application or was manually approved by you, another developer of your application, or a translator with whom you have a contract. 
        /// If there is no such translation, then the native string doesn't get returned. 
        /// You do not have to pass a session key with this method. However, you must include your application secret. 
        /// </summary>
        /// <param name="locale">The locale from where you are retrieving the translated strings. To return translated strings from every locale where someone has translated your application, specify all for locale (however, this is not recommended for performance reasons). Locales are of the format ll_CC, where ll is a two-letter language code (in lowercase), and CC is a two-letter country code (in uppercase). For a list of locales Facebook supports, see Facebook Locales. If the default is used, then only the native strings get returned. (Default value is en_US.) </param>
        /// <param name="all">When true, this call returns every translation for every native string. When false, this call returns the best translation for every native string. (Default value is false.) </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        public void GetTranslationsAsync(string locale, bool all, GetTranslationsCallback callback, Object state)
        {
            GetTranslations(locale, all, true, callback, state);
        }

        /// <summary>
        /// Lets you insert text strings in their native language into the Facebook Translations database so they can be translated. See Translating Platform Applications for more information about translating your applications. 
        /// </summary>
        /// <param name="native_strings">A JSON-encoded array of strings to translate. Each element of the string array is an object, with text storing the actual string, description storing the description of the text.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>If successful, this method returns the number of strings uploaded. </returns>
        public void UploadNativeStringsAsync(List<native_string> native_strings, UploadNativeStringsCallback callback, Object state)
        {
            UploadNativeStrings(native_strings, true, callback, state);
        }

		#endregion

		#endregion Public Methods
        
		#region Private Methods

        private List<locale_data> GetTranslations(string locale, bool all, bool isAsync, GetTranslationsCallback callback, Object state)
        {
			var parameterList = new Dictionary<string, string> { { "method", "facebook.intl.getTranslations" } };
			Utilities.AddOptionalParameter(parameterList, "locale", locale);
			Utilities.AddParameter(parameterList, "all", all);

			if (isAsync)
			{
                SendRequestAsync<intl_getTranslations_response, List<locale_data>>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<List<locale_data>>(callback), state);
				return null;
			}

            return SendRequest<intl_getTranslations_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey)).locale_data;
        }

        private long UploadNativeStrings(List<native_string> native_strings, bool isAsync, UploadNativeStringsCallback callback, Object state)
        {
			var parameterList = new Dictionary<string, string> { { "method", "facebook.intl.uploadNativeStrings" } };
            var translationList = new List<string>();

			foreach (var item in native_strings)
			{
				var translation = new Dictionary<string, string>{
                       {"text", item.text}, {"description", item.description}
                        };
				translationList.Add(JSONHelper.ConvertToJSONAssociativeArray(translation));
			}
			Utilities.AddRequiredParameter(parameterList, "native_strings", JSONHelper.ConvertToJSONArray(translationList));

			if (isAsync)
			{
				SendRequestAsync<intl_uploadNativeStrings_response, long>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<long>(callback), state);
				return 0;
			}

			var response = SendRequest<intl_uploadNativeStrings_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? -1 : response.TypedValue;
		}


		#endregion Private Methods
        
		#endregion Methods

		#region Delegates

        /// <summary>
        /// Call back used with Async call to GetTranslations completes
        /// </summary>
        public delegate void GetTranslationsCallback(List<locale_data> locale_data, Object state, FacebookException e);

        /// <summary>
        /// Call back used with Async call to UploadNativeStrings completes
        /// </summary>
        public delegate void UploadNativeStringsCallback(long count, Object state, FacebookException e);

		#endregion Delegates
	}
    /// <summary>
    /// Item for native_strings collection
    /// </summary>
    public class native_string
    {
        /// <summary>
        /// the actual string
        /// </summary>
        public string text;
        /// <summary>
        /// the description of the text
        /// </summary>
        public string description;
    }
}
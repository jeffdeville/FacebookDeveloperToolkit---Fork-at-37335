using System.Collections.Generic;
using facebook.Schema;
using facebook.Utility;

namespace Facebook.Rest
{
	/// <summary>
	/// Facebook application api methods.
	/// </summary>
	public class Application
	{
		private readonly Api _api;
		/// <summary>
		/// Public constructor for facebook.application
		/// </summary>
		/// <param name="parent">Needs a connected API object for making requests</param>
		public Application(Api parent)
		{
			_api = parent;
		}

		/// <summary>
		/// Returns public information for an application (as shown in the application directory) by either application ID, API key, or canvas page name. 
		/// </summary>
		/// <param name="application_id">Application ID of the desired application. You must specify exactly one of application_id, application_api_key or application_canvas_name. </param>
		/// <param name="application_api_key">API key of the desired application. You must specify exactly one of application_id, application_api_key or application_canvas_name. </param>
		/// <param name="application_canvas_name">Canvas page name of the desired application. You must specify exactly one of application_id, application_api_key or application_canvas_name. </param>
		/// <returns>app_info object</returns>
        public app_info getPublicInfo(long? application_id, string application_api_key, string application_canvas_name)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.application.getPublicInfo" } };
            _api.AddOptionalParameter(parameterList, "application_id", application_id);
			_api.AddOptionalParameter(parameterList, "application_api_key", application_api_key);
			_api.AddOptionalParameter(parameterList, "application_canvas_name", application_canvas_name);

			var response = _api.SendRequest(parameterList);
			return !string.IsNullOrEmpty(response) ? XmlSerializer.Deserialize<application_getPublicInfo_response>(response) : null;
		}
        /// <summary>
        /// Returns public information for the current application (as shown in the application directory) by either application ID, API key, or canvas page name. 
        /// </summary>
        /// <returns>app_info object</returns>
        public app_info getPublicInfo()
        {
            return getPublicInfo(null, _api.ApplicationKey, null);
        }

	}
}

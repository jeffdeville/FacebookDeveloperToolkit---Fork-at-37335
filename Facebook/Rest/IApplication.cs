using System;
namespace Facebook.Rest
{
	public interface IApplication : IRestBase
	{
		Facebook.Schema.app_info GetPublicInfo();
		Facebook.Schema.app_info GetPublicInfo(long? application_id, string application_api_key, string application_canvas_name);
		Facebook.Schema.app_info GetPublicInfoAsync(Application.GetPublicInfoCallback callback, object state);
		Facebook.Schema.app_info GetPublicInfoAsync(long? application_id, string application_api_key, string application_canvas_name, Application.GetPublicInfoCallback callback, object state);
	}
}

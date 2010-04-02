using System;
namespace Facebook.Rest
{
	public interface IVideo : IAuthenticatedService
	{
		Facebook.Schema.video_limits GetUploadLimits();
		void GetUploadLimitsAsync(Video.GetUploadLimitsCallback callback, object state);
		Facebook.Schema.video Upload(string title, string description, System.IO.FileInfo data);
		void UploadAsync(string title, string description, byte[] data, Facebook.Schema.Enums.FileType fileType, Video.UploadCallback callback, object state);
		void UploadAsync(string title, string description, byte[] data, string contentType, Video.UploadCallback callback, object state);
	}
}

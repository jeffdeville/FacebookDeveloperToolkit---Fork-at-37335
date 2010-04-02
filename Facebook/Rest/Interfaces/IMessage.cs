using System;
namespace Facebook.Rest
{
	public interface IMessage : IAuthenticatedService
	{
		System.Collections.Generic.IList<Facebook.Schema.thread> GetThreadsInFolder(int folder_id, int uid, int limit, int offset);
		void GetThreadsInFolderAsynch(int folder_id, int uid, int limit, int offset, Message.GetThreadsInFolderCallback callback, object state);
	}
}

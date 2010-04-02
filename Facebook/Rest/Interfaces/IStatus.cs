using System;
namespace Facebook.Rest
{
	public interface IStatus : IAuthenticatedService
	{
		System.Collections.Generic.IList<Facebook.Schema.user_status> Get();
		System.Collections.Generic.IList<Facebook.Schema.user_status> Get(long uid);
		System.Collections.Generic.IList<Facebook.Schema.user_status> Get(long uid, int limit);
		void GetAsync(Status.GetCallback callback, object state);
		void GetAsync(long uid, Status.GetCallback callback, object state);
		void GetAsync(long uid, int limit, Status.GetCallback callback, object state);
		bool Set(long uid, string status);
		bool Set(string status);
		void SetAsync(long uid, string status, Status.SetCallback callback, object state);
		void SetAsync(string status, Status.SetCallback callback, object state);
	}
}

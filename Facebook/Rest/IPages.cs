using System;
namespace Facebook.Rest
{
	public interface IPages : IRestBase
	{
		System.Collections.Generic.List<string> GetFields();
		System.Collections.Generic.IList<Facebook.Schema.page> GetInfo(System.Collections.Generic.List<string> fields, System.Collections.Generic.List<long> page_ids, long? uid);
		void GetInfoAsync(System.Collections.Generic.List<string> fields, System.Collections.Generic.List<long> page_ids, long? uid, Pages.GetInfoCallback callback, object state);
		bool IsAdmin(long page_id);
		void IsAdminAsync(long page_id, Pages.IsAdminCallback callback, object state);
		bool IsAppAdded(long page_id);
		void IsAppAddedAsync(long page_id, Pages.IsAppAddedCallback callback, object state);
		bool IsFan(long page_id);
		bool IsFan(long page_id, long uid);
		void IsFanAsync(long page_id, Pages.IsFanCallback callback, object state);
		void IsFanAsync(long page_id, long uid, Pages.IsFanCallback callback, object state);
	}
}

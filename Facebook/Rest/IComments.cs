using System;
namespace Facebook.Rest
{
	public interface IComments : IAuthorizedRestBase
	{
		int Add(string xid, string text);
		int Add(string xid, string text, long uid);
		int Add(string xid, string text, long uid, string title, string url, bool publish_to_stream);
		void AddAsync(string xid, string text, Comments.AddCallback callback, object state);
		void AddAsync(string xid, string text, long uid, Comments.AddCallback callback, object state);
		void AddAsync(string xid, string text, long uid, string title, string url, bool publish_to_stream, Comments.AddCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.comment> Get(string xid);
		void GetAsync(string xid, Comments.GetCallback callback, object state);
		bool Remove(string xid, int comment_id);
		bool Remove(string xid, int comment_id, bool useSession);
		void RemoveAsync(string xid, int comment_id, Comments.RemoveCallback callback, object state);
		void RemoveAsync(string xid, int comment_id, bool useSession, Comments.RemoveCallback callback, object state);
	}
}

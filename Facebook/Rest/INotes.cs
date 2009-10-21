using System;
namespace Facebook.Rest
{
	public interface INotes : IRestBase
	{
		long Create(long uid, string title, string content);
		void CreateAsync(long uid, string title, string content, Notes.CreateCallback callback, object state);
		bool Delete(long note_id);
		void DeleteAsync(long note_id, Notes.DeleteCallback callback, object state);
		bool Edit(long note_id, string title, string content);
		void EditAsync(long note_id, string title, string content, Notes.EditCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.note> Get();
		System.Collections.Generic.IList<Facebook.Schema.note> Get(long uid);
		void GetAsync(Notes.GetCallback callback, object state);
		void GetAsync(long uid, Notes.GetCallback callback, object state);
	}
}

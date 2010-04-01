using System;
namespace Facebook.Rest
{
	public interface IStream : IAuthorizedRestBase
	{
		string AddComment(long uid, string postId, string comment);
		string AddComment(string postId, string comment);
		void AddCommentAsync(long uid, string postId, string comment, Stream.AddCommentCallback callback, object state);
		void AddCommentAsync(string postId, string comment, Stream.AddCommentCallback callback, object state);
		bool AddLike(long uid, string postId);
		bool AddLike(string postId);
		void AddLikeAsync(long uid, string postId, Stream.AddLikeCallback callback, object state);
		void AddLikeAsync(string postId, Stream.AddLikeCallback callback, object state);
		Facebook.Schema.stream_data Get(System.Collections.Generic.List<long> sourceIds, DateTime? startTime, DateTime? endTime, int? limit);
		Facebook.Schema.stream_data Get(long viewerId, System.Collections.Generic.List<long> sourceIds, DateTime? startTime, DateTime? endTime, int? limit, string filter_key);
		void GetAsync(System.Collections.Generic.List<long> sourceIds, DateTime? startTime, DateTime? endTime, int? limit, Stream.GetCallback callback, object state);
		void GetAsync(long viewerId, System.Collections.Generic.List<long> sourceIds, DateTime? startTime, DateTime? endTime, int? limit, string filter_key, Stream.GetCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.comment> GetComments(string post_id);
		void GetCommentsAsync(string post_id, Stream.GetCommentsCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.stream_filter> GetFilters();
		System.Collections.Generic.IList<Facebook.Schema.stream_filter> GetFilters(long uid);
		void GetFiltersAsync(Stream.GetFiltersCallback callback, object state);
		void GetFiltersAsync(long uid, Stream.GetFiltersCallback callback, object state);
		string Publish(string message);
		string Publish(string message, attachment attachment, System.Collections.Generic.IList<Facebook.Schema.action_link> actionLinks, string target_id, long uid);
		void PublishAsync(string message, attachment attachment, System.Collections.Generic.IList<Facebook.Schema.action_link> actionLinks, string target_id, long uid, Stream.PublishCallback callback, object state);
		void PublishAsync(string message, Stream.PublishCallback callback, object state);
		bool Remove(long uid, string postId);
		bool Remove(string post_id);
		void RemoveAsync(long uid, string postId, Stream.RemoveCallback callback, object state);
		void RemoveAsync(string postId, Stream.RemoveCallback callback, object state);
		bool RemoveComment(long uid, string commentId);
		bool RemoveComment(string comment_id);
		void RemoveCommentAsync(long uid, string commentId, Stream.RemoveCommentCallback callback, object state);
		void RemoveCommentAsync(string commentId, Stream.RemoveCommentCallback callback, object state);
		bool RemoveLike(long uid, string postId);
		bool RemoveLike(string postId);
		void RemoveLikeAsync(long uid, string postId, Stream.RemoveLikeCallback callback, object state);
		void RemoveLikeAsync(string postId, Stream.RemoveLikeCallback callback, object state);
	}
}

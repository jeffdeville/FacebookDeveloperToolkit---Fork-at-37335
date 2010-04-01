using System;
namespace Facebook.Rest
{
	public interface IFql : IAuthorizedRestBase
	{
		System.Collections.Generic.IList<Facebook.Schema.fql_result> Multiquery(FqlMultiQueryInfo[] queries);
		System.Collections.Generic.IList<Facebook.Schema.fql_result> Multiquery(System.Collections.Generic.Dictionary<string, string> queries);
		void MultiqueryAsync(FqlMultiQueryInfo[] queries, Fql.MultiqueryParsedCallback callback, object state);
		void MultiqueryAsync(System.Collections.Generic.Dictionary<string, string> queries, Fql.MultiqueryCallback callback, object state);
		string Query(string query);
		T Query<T>(string query);
		void QueryAsync(string query, Fql.QueryCallback callback, object state);
		void QueryAsync<T>(string query, Fql.QueryCallback<T> callback, object state);
		bool UseJson { get; set; }
	}
}

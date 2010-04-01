using System;
namespace Facebook.Rest
{
	public interface ILinks : IAuthorizedRestBase
	{
		System.Collections.Generic.IList<Facebook.Schema.link> Get();
		System.Collections.Generic.IList<Facebook.Schema.link> Get(long uid);
		void GetAsync(Links.GetCallback callback, object state);
		void GetAsync(long uid, Links.GetCallback callback, object state);
		long Post(long uid, Uri url, string comment);
		long PostAsync(long uid, Uri url, string comment, Links.PostCallback callback, object state);
	}
}

using System;
namespace Facebook.Rest
{
	public interface IRestBase
	{
		Batch Batch { get; set; }
		string ExecuteApiImageUpload(System.IO.FileSystemInfo uploadFile, System.Collections.Generic.IDictionary<string, string> parameterList);
		string ExecuteApiVideoUpload(System.IO.FileSystemInfo uploadFile, System.Collections.Generic.IDictionary<string, string> parameterList);
		Permissions Permissions { get; set; }
		string SendRequest(System.Collections.Generic.IDictionary<string, string> parameterDictionary);
		string SendRequest(System.Collections.Generic.IDictionary<string, string> parameterDictionary, bool useSession);
		T SendRequest<T>(System.Collections.Generic.IDictionary<string, string> parameterDictionary);
		T SendRequest<T>(System.Collections.Generic.IDictionary<string, string> parameterDictionary, bool useSession);
		void SendRequestAsync(System.Collections.Generic.IDictionary<string, string> parameterList, Facebook.Utility.AsyncResult ar);
		void SendRequestAsync(System.Collections.Generic.IDictionary<string, string> parameterList, bool useSession, Facebook.Utility.AsyncResult ar);
		void SendRequestAsync<T>(System.Collections.Generic.Dictionary<string, string> parameterList, FacebookCallCompleted<T> callback, object state);
		void SendRequestAsync<T>(System.Collections.Generic.Dictionary<string, string> parameterList, bool useSession, FacebookCallCompleted<T> callback, object state);
		void SendRequestAsync<TObject, TResult>(System.Collections.Generic.Dictionary<string, string> parameterList, FacebookCallCompleted<TResult> callback, object state);
		void SendRequestAsync<TObject, TResult>(System.Collections.Generic.Dictionary<string, string> parameterList, FacebookCallCompleted<TResult> callback, object state, string propertyName);
		void SendRequestAsync<TObject, TResult>(System.Collections.Generic.Dictionary<string, string> parameterList, bool useSession, FacebookCallCompleted<TResult> callback, object state);
		void SendRequestAsync<TObject, TResult>(System.Collections.Generic.Dictionary<string, string> parameterList, bool useSession, FacebookCallCompleted<TResult> callback, object state, string propertyName);
		Facebook.Session.FacebookSession Session { get; }
		void UploadFile(System.Collections.Generic.IDictionary<string, string> parameters, byte[] data, string contentType, Facebook.Utility.AsyncResult ar);
		void UploadFile(System.Collections.Generic.IDictionary<string, string> parameters, byte[] data, string contentType, Uri uploadUrl, Facebook.Utility.AsyncResult ar);
		void UploadVideoFile(System.Collections.Generic.IDictionary<string, string> parameters, byte[] data, string contentType, Facebook.Utility.AsyncResult ar);
	}
}

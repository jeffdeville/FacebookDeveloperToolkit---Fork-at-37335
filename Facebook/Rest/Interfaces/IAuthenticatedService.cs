using System;
using System.Collections.Generic;
using System.IO;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
	public interface IFacebookNetworkWrapper
	{
		bool IsBatchActive { get; set; }
		string SendRequest(IDictionary<string, string> parameterDictionary, bool compressHttp);
		T SendRequest<T>(IDictionary<string, string> parameterDictionary, bool compressHttp);
		string SendRequestSynchronous(IDictionary<string, string> parameterDictionary, bool compressHttp);
		void SendRequestAsync(IDictionary<string, string> parameterList, AsyncResult ar, bool compressHttp);
		void SendRequestAsync<T>(Dictionary<string, string> parameterList, FacebookCallCompleted<T> callback, object state, bool compressHttp);
		void SendRequestAsync<TObject, TResult>(Dictionary<string, string> parameterList,
												FacebookCallCompleted<TResult> callback, object state, bool compressHttp);

		void SendRequestAsync<TObject, TResult>(Dictionary<string, string> parameterList,
												FacebookCallCompleted<TResult> callback, object state, string propertyName, bool compressHttp);
	}

	public interface IAuthenticatedService
	{
		//Batch Batch { get; set; }
		bool IsBatchActive { get; set; }
		Permissions Permissions { get; set; }
		IFacebookSession Session { get; }
		string ExecuteApiImageUpload(FileSystemInfo uploadFile, IDictionary<string, string> parameterList);
		string ExecuteApiVideoUpload(FileSystemInfo uploadFile, IDictionary<string, string> parameterList);

		string SendRequest(IDictionary<string, string> parameterDictionary);
		string SendRequest(IDictionary<string, string> parameterDictionary, bool useSession);

		T SendRequest<T>(IDictionary<string, string> parameterDictionary, bool useSession);
		T SendRequest<T>(IDictionary<string, string> parameterDictionary);

		void SendRequestAsync(IDictionary<string, string> parameterList, AsyncResult ar);
		void SendRequestAsync(IDictionary<string, string> parameterList, bool useSession, AsyncResult ar);
		void SendRequestAsync<T>(Dictionary<string, string> parameterList, FacebookCallCompleted<T> callback, object state);
		void SendRequestAsync<T>(Dictionary<string, string> parameterList, bool useSession, FacebookCallCompleted<T> callback, object state);
		void SendRequestAsync<TObject, TResult>(Dictionary<string, string> parameterList, FacebookCallCompleted<TResult> callback, object state);
		void SendRequestAsync<TObject, TResult>(Dictionary<string, string> parameterList, bool useSession, FacebookCallCompleted<TResult> callback, object state);
		void SendRequestAsync<TObject, TResult>(Dictionary<string, string> parameterList, FacebookCallCompleted<TResult> callback, object state, string propertyName);
		void SendRequestAsync<TObject, TResult>(Dictionary<string, string> parameterList, bool useSession, FacebookCallCompleted<TResult> callback, object state, string propertyName);

		void UploadFile(IDictionary<string, string> parameters, byte[] data, string contentType, AsyncResult ar);
		void UploadFile(IDictionary<string, string> parameters, byte[] data, string contentType, Uri uploadUrl, AsyncResult ar);
		void UploadVideoFile(IDictionary<string, string> parameters, byte[] data, string contentType, AsyncResult ar);		
	}
}
using System;
using System.Collections.Generic;
using System.IO;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
	public interface IRestBase
	{
		string SendRequest(IDictionary<string, string> parameterDictionary);
		T SendRequest<T>(IDictionary<string, string> parameterDictionary);
		void SendRequestAsync(IDictionary<string, string> parameterList, AsyncResult ar);
		void SendRequestAsync<T>(Dictionary<string, string> parameterList, FacebookCallCompleted<T> callback, object state);
		void SendRequestAsync<TObject, TResult>(Dictionary<string, string> parameterList,
												FacebookCallCompleted<TResult> callback, object state);

		void SendRequestAsync<TObject, TResult>(Dictionary<string, string> parameterList,
												FacebookCallCompleted<TResult> callback, object state, string propertyName);


	}

	public interface IAuthorizedRestBase : IRestBase
	{
		Batch Batch { get; set; }
		Permissions Permissions { get; set; }
		SessionInfo SessionInfo { get; }
		string ExecuteApiImageUpload(FileSystemInfo uploadFile, IDictionary<string, string> parameterList);
		string ExecuteApiVideoUpload(FileSystemInfo uploadFile, IDictionary<string, string> parameterList);

		string SendRequest(IDictionary<string, string> parameterDictionary, bool useSession);

		T SendRequest<T>(IDictionary<string, string> parameterDictionary, bool useSession);

		void SendRequestAsync(IDictionary<string, string> parameterList, bool useSession, AsyncResult ar);
		

		void SendRequestAsync<T>(Dictionary<string, string> parameterList, bool useSession, FacebookCallCompleted<T> callback,
		                         object state);

		
		void SendRequestAsync<TObject, TResult>(Dictionary<string, string> parameterList, bool useSession,
		                                        FacebookCallCompleted<TResult> callback, object state);

		void SendRequestAsync<TObject, TResult>(Dictionary<string, string> parameterList, bool useSession,
		                                        FacebookCallCompleted<TResult> callback, object state, string propertyName);

		void UploadFile(IDictionary<string, string> parameters, byte[] data, string contentType, AsyncResult ar);
		void UploadFile(IDictionary<string, string> parameters, byte[] data, string contentType, Uri uploadUrl, AsyncResult ar);
		void UploadVideoFile(IDictionary<string, string> parameters, byte[] data, string contentType, AsyncResult ar);
	}
}
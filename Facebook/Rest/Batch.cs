using System;
using System.Collections.Generic;
using System.Linq;
using Facebook.Session;
using Facebook.Utility;
using System.Xml.Linq;
using System.Reflection;
using System.Xml;
using System.Text;
using System.Runtime.Serialization.Json;

namespace Facebook.Rest
{
	/// <summary>
	/// Facebook Batch API methods.
	/// </summary>
	public class Batch : RestBase, Facebook.Rest.IBatch
	{
		#region Private Members
		
		private readonly List<string> _callList = new List<string>();
        private readonly List<BatchRecord> _callListAsync = new List<BatchRecord>();
		private bool _isActive;
        
        #endregion Private Members

		#region Public Properties

		///<summary>
		///</summary>
		public bool IsActive
		{
			get { return _isActive; }
		}

		///<summary>
		///</summary>
		public List<string> CallList
		{
			get { return _callList; }
		}

		///<summary>
		///</summary>
		public List<BatchRecord> CallListAsync
		{
			get { return _callListAsync; }
		}

		#endregion Public Properties

		#region Methods

		#region Constructor

		/// <summary>
		/// Public constructor for facebook.Batch
		/// </summary>
		/// <param name="session">Needs a connected Facebook Session object for making requests</param>
		public Batch(IFacebookSession session)
			: base(session)
		{
		}

		#endregion Constructor

		#region Public Methods

#if !SILVERLIGHT

		#region Synchronous Methods

        /// <summary>
        /// Execute a list of individual API calls in a single batch.
        /// </summary>
        /// <returns>An array of individual results as objects.</returns>
        public IList<Object> ExecuteBatch()
		{
			return ExecuteBatch(false);
		}

        /// <summary>
        /// Execute a list of individual API calls in a single batch.
        /// </summary>
        /// <param name="isSerial">n optional parameter to indicate whether the methods in the method_feed must be executed in order. The default value is false.</param>
        /// <returns>An array of individual results as objects.</returns>
		public IList<Object> ExecuteBatch(bool isSerial)
		{
			IList<Object> ret = new List<Object>();
			string response = Run(_callList, isSerial);
			_callList.Clear();

			var responses = SplitResponses(response);			
			var responseObjects = new List<object>();

			foreach (var r in responses)
			{
				Type t = TypeHelper.getResponseObjectType(r);
                var method = typeof(Utilities).GetMethods().First(m => m.Name=="DeserializeXML");
				MethodInfo methodInfo = method.MakeGenericMethod(t);
				responseObjects.Add(methodInfo.Invoke(null, new object[] { r }));
			}			

			if (responseObjects.Count > 0)
			{
				return responseObjects;
			}
			else
			{
				return null;
			}
		}

        #endregion Synchronous Methods

#endif

        #region Asynchronous Methods

        /// <summary>
        /// Execute a list of individual API calls in a single batch.
        /// </summary>
        /// <returns>An array of individual results as objects.</returns>
        public void ExecuteBatchAsync()
		{
			ExecuteBatchAsync(false);
		}

        /// <summary>
        /// Execute a list of individual API calls in a single batch.
        /// </summary>
        /// <param name="isSerial">n optional parameter to indicate whether the methods in the method_feed must be executed in order. The default value is false.</param>
        /// <returns>An array of individual results as objects.</returns>
        public void ExecuteBatchAsync(bool isSerial)
		{
			var callList = _callListAsync.Select(record => record.PostData).ToList();
			Run(callList, isSerial, true);
        }

        #endregion Asynchronous Methods

        /// <summary>
        /// Sets the current Batch state of the Batch object to Active.
        /// </summary>
        public void BeginBatch()
		{
			_isActive = true;
		}

		#endregion Public Methods

		#region Private Methods

		private string Run(List<string> callList, bool isSerial)
		{
			return Run(callList, isSerial, false);
		}

		private string Run(List<string> callList, bool isSerial, bool isAsync)
		{
			_isActive = false;
			var parameterList = new Dictionary<string, string> { { "method", "facebook.batch.run" } };
			Utilities.AddJSONArray(parameterList, "method_feed", callList);
			if (isSerial)
			{
				Utilities.AddRequiredParameter(parameterList, "serial_only", "1");
			}

			if (isAsync)
			{
                SendRequestAsync(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new AsyncResult(OnRunCompleted, null, null));
				return null;
			}

            return SendRequest(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
		}

		/// <summary>
		/// Completes the async operation that was started by BeginExecuteBatch
		/// </summary>
		/// <param name="ar">An IAsyncResult that references a pending request for token</param>
		private void OnRunCompleted(IAsyncResult ar)
		{
			AsyncResult asyncResult = (AsyncResult)ar;
			FacebookException exception = asyncResult.Exception;

			if (exception != null)
			{
				foreach (BatchRecord record in _callListAsync)
				{
					record.AsyncResult.SetComplete(null, asyncResult.Exception);
				}

				_callListAsync.Clear();
				return;
			}

			var responses = SplitResponses(asyncResult.Result).ToList();

			for (int i = 0; i < _callListAsync.Count; i++)
			{
				_callListAsync[i].AsyncResult.SetComplete(responses[i], null);
			}

			_callListAsync.Clear();
		}

		private IEnumerable<string> SplitResponses(string xmlResponse)
		{
			XDocument doc = XDocument.Parse(xmlResponse);
			XNamespace xname = XNamespace.Get(Constants.FacebookNamespace);
			return from element in doc.Descendants(xname + "batch_run_response_elt")
				   select element.Value;
		}

		#endregion Private Methods

		#endregion Methods
	}

	///<summary>
	///</summary>
	public class BatchRecord
	{
		///<summary>
		///</summary>
		public AsyncResult AsyncResult
		{
			get;
			set;
		}

		///<summary>
		///</summary>
		public string PostData { get; set; }
        
		///<summary>
		///</summary>
		///<param name="ar"></param>
		///<param name="postData"></param>
		public BatchRecord(AsyncResult ar, string postData)
		{
			AsyncResult = ar;
			PostData = postData;
		}
	}
}
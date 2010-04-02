using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
	/// <summary>
	/// Facebook LiveMessage API methods.
	/// </summary>
	public class Message : BaseAuthenticatedService, Facebook.Rest.IMessage
	{
		#region Methods

		#region Constructor

		/// <summary>
		/// Public constructor for facebook.LiveMessage
		/// </summary>
		/// <param name="session">Needs a connected Facebook Session object for making requests</param>
		public Message(IFacebookNetworkWrapper networkWrapper, IFacebookSession session)
			: base(networkWrapper, session)
		{
		}

		#endregion Constructor

		#region Public Methods

#if !SILVERLIGHT

		#region Synchronous Methods

        /// <summary>
        /// Returns all of a user's messages and threads from the Inbox. The user needs to grant the calling application the read_mailbox extended permission. 
        /// This method is a wrapper around the thread and message FQL tables; you can achieve more fine-grained control by using those two FQL tables in conjunction with the fql.multiquery API call. 
        /// Applications must pass a valid session key or a user ID. 
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Message.GetThreadsInFolder(
        /// </code>
        /// </example>
        /// <param name="folder_id">The ID of the folder you want to return. The ID can be one of: 0 (for Inbox), 1 (for Outbox), or 4 (for Updates). </param>
        /// <param name="uid">Indicates whether to include notifications that have already been read. By default, notifications a user has read are not included. </param>
        /// <param name="limit">Indicates the number of threads to return.</param>
        /// <param name="offset">Indicates how many threads to skip from the most recent thread.</param>
        /// <returns>On success, returns an array of threads, where each contains an array of messages. Or else it returns an error code..</returns>
        public IList<thread> GetThreadsInFolder(int folder_id, int uid, int limit, int offset)
		{
            return GetThreadsInFolder(folder_id, uid, limit, offset, false, null, null);
        }

        #endregion Synchronous Methods

#endif

        #region Asynchronous Methods

        /// <summary>
        /// Returns all of a user's messages and threads from the Inbox. The user needs to grant the calling application the read_mailbox extended permission. 
        /// This method is a wrapper around the thread and message FQL tables; you can achieve more fine-grained control by using those two FQL tables in conjunction with the fql.multiquery API call. 
        /// Applications must pass a valid session key or a user ID. 
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Message.GetThreadsInFolder(
        /// </code>
        /// </example>
        /// <param name="folder_id">The ID of the folder you want to return. The ID can be one of: 0 (for Inbox), 1 (for Outbox), or 4 (for Updates). </param>
        /// <param name="uid">Indicates whether to include notifications that have already been read. By default, notifications a user has read are not included. </param>
        /// <param name="limit">Indicates the number of threads to return.</param>
        /// <param name="offset">Indicates how many threads to skip from the most recent thread.</param>
        /// <param name="callback">The callback function if this is being called asynchronously.</param>
        /// <param name="state">The state of the object to return for the asynch call.</param>
        /// <returns>On success, returns an array of threads, where each contains an array of messages. Or else it returns an error code..</returns>
        public void GetThreadsInFolderAsynch(int folder_id, int uid, int limit, int offset, GetThreadsInFolderCallback callback, Object state)
        {
            GetThreadsInFolder(folder_id, uid, limit, offset, true, callback, state);
        }


        #endregion Asynchronous Methods

        #endregion Public Methods

        #region Private Methods

        private IList<thread> GetThreadsInFolder(int folder_id, int uid, int limit, int offset, bool isAsync, GetThreadsInFolderCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.message.getThreadsInFolder" } };
            Utilities.AddOptionalParameter(parameterList, "folder_id", folder_id.ToString());
            Utilities.AddOptionalParameter(parameterList, "uid", uid.ToString());
            Utilities.AddOptionalParameter(parameterList, "limit", limit.ToString());
            Utilities.AddOptionalParameter(parameterList, "offset", offset.ToString());

			if (isAsync)
			{
				SendRequestAsync<message_getThreadsInFolder_response, IList<thread>>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<IList<thread>>(callback), state);
				return null;
			}

            var response = SendRequest<message_getThreadsInFolder_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? null : response.thread;
		}

		#endregion Private Methods
        
		#endregion Methods

		#region Delegates

        /// <summary>
        /// Delegate called when Send call is completed.
        /// </summary>
        /// <param name="result">boolean result</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetThreadsInFolderCallback(IList<thread> result, Object state, FacebookException e);

		#endregion Delegates
	}
}
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
	public class LiveMessage : RestBase, Facebook.Rest.ILiveMessage
	{
		#region Methods

		#region Constructor

		/// <summary>
		/// Public constructor for facebook.LiveMessage
		/// </summary>
		/// <param name="session">Needs a connected Facebook Session object for making requests</param>
		public LiveMessage(IFacebookSession session)
			: base(session)
		{
		}

		#endregion Constructor

		#region Public Methods

#if !SILVERLIGHT

		#region Synchronous Methods

        /// <summary>
        /// Sends a "message" directly to a user's browser, which can be handled in FBJS.
        /// See the facebook guide for more information.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.LiveMessage.Send(Constants.UserId, "live message event", "sending a message...");
        /// </code>
        /// </example>
        /// <param name="recipient">The user ID of the message recipient.</param>
        /// <param name="event_name">Name of the "event" for which messages will be sent and received.</param>
        /// <param name="message">The message to send (max length: 1024 bytes).</param>
        /// <returns>This method returns true if message was sent successfully.</returns>
        public bool Send(long recipient, string event_name, string message)
		{
			return Send(recipient, event_name, message, false, null, null);
        }

        #endregion Synchronous Methods

#endif

        #region Asynchronous Methods

        /// <summary>
        /// Sends a "message" directly to a user's browser, which can be handled in FBJS.
        /// See the facebook guide for more information.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.LiveMessage.SendAsync(Constants.UserId, "live message event", "sending a message...", AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="recipient">The user ID of the message recipient.</param>
        /// <param name="event_name">Name of the "event" for which messages will be sent and received.</param>
        /// <param name="message">The message to send (max length: 1024 bytes).</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true if message was sent successfully.</returns>
        public void SendAsync(long recipient, string event_name, string message, SendCallback callback, Object state)
		{
            Send(recipient, event_name, message, true, callback, state);
        }

        #endregion Asynchronous Methods

        #endregion Public Methods

        #region Private Methods

        private bool Send(long recipient, string event_name, string message, bool isAsync, SendCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.liveMessage.send" } };
			Utilities.AddRequiredParameter(parameterList, "recipient", recipient.ToString());
			Utilities.AddRequiredParameter(parameterList, "event_name", event_name);
			Utilities.AddJSONAssociativeArray(parameterList, "message", new Dictionary<string, string> { { "from", Session.UserId.ToString() }, { "msg", message } });

			if (isAsync)
			{
				SendRequestAsync<liveMessage_send_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
				return true;
			}

			var response = SendRequest<liveMessage_send_response>(parameterList);
			return response == null ? true : response.TypedValue;
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
        public delegate void SendCallback(bool result, Object state, FacebookException e);

		#endregion Delegates
	}
}
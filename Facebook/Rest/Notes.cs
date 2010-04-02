using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
	/// <summary>
	/// Facebook Notes API methods.
	/// </summary>
	public class Notes : AuthorizedRestBase, Facebook.Rest.INotes
	{
		#region Methods

		#region Constructor

		/// <summary>
		/// Public constructor for facebook.Notes
		/// </summary>
		/// <param name="session">Needs a connected Facebook Session object for making requests</param>
		public Notes(SessionInfo session)
			: base(session)
		{
		}

		#endregion Constructor

		#region Public Methods

#if !SILVERLIGHT
		
		#region Synchronous Methods

        /// <summary>
        /// Returns a list of all of the visible notes written by the specified user.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Notes.Get();
        /// </code>
        /// </example>
        /// <returns>This method returns a List of notes with their data in their respective fields (note_id, title, content, created_time, updated_time, uid).</returns>
        /// <remarks> For desktop applications, don't specify a uid; keep the default. (Default value is the logged-in user.)</remarks>
        public IList<note> Get()
		{
			return Get(0);
		}

        /// <summary>
        /// Returns a list of all of the visible notes written by the specified user.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Notes.Get(Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="uid">The user ID of the user whose notes you want to retrieve.</param>
        /// <returns>This method returns a List of notes with their data in their respective fields (note_id, title, content, created_time, updated_time, uid).</returns>
        /// <remarks> For desktop applications, don't specify a uid; keep the default. (Default value is the logged-in user.)</remarks>
        public IList<note> Get(long uid)
		{
			return Get(uid, false, null, null);
		}

        /// <summary>
        /// Lets a user write a Facebook note through your application.
        /// See the facebook guide for more information.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        /// var result = api.Notes.Create(Constants.UserId, "A test note", "My note content");
        /// </code>
        /// </example>
        /// <param name="uid">The user ID of the user posting the link.</param>
        /// <param name="title">The title of the note.</param>
        /// <param name="content">The note's content.</param>
        /// <returns>If successful, this method returns the note_id of the note that was just created.</returns>
		public long Create(long uid, string title, string content)
		{
			return Create(uid, title, content, false, null, null);
		}

        /// <summary>
        /// Lets a user edit a Facebook note through your application.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Notes.Edit(Constants.NoteId, "A test note (Updated)", "My note content has also been updated!");
        /// </code>
        /// </example>
        /// <param name="note_id">The ID of the note to edit.</param>
        /// <param name="title">The title of the note.</param>
        /// <param name="content">The note's content.</param>
        /// <returns>This method returns true if successful.</returns>
        public bool Edit(long note_id, string title, string content)
		{
			return Edit(note_id, title, content, false, null, null);
		}

        /// <summary>
        /// Lets a user delete a Facebook note that was written through your application.
        /// </summary>
        /// <example>
        /// <code>
        /// FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Notes.Delete(Constants.NoteId);
        /// </code>
        /// </example>
        /// <param name="note_id">The ID of the note to delete.</param>
        /// <returns>This method returns true if successful.</returns>
        public bool Delete(long note_id)
		{
			return Delete(note_id, false, null, null);
		}

		#endregion

#endif

		#region Asynchronous Methods

		/// <summary>
        /// Lets a user write a Facebook note through your application.
        /// See the facebook guide for more information.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Notes.CreateAsync(Constants.UserId, "A test note", "My note content", AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(long result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">The user ID of the user posting the link.</param>
        /// <param name="title">The title of the note.</param>
        /// <param name="content">The note's content.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>If successful, this method returns the note_id of the note that was just created.</returns>
        public void CreateAsync(long uid, string title, string content, CreateCallback callback, Object state)
		{
			Create(uid, title, content, true, callback, state);
		}

        /// <summary>
        /// Lets a user delete a Facebook note that was written through your application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Notes.DeleteAsync(Constants.NoteId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="note_id">The ID of the note to delete.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true if successful.</returns>
        public void DeleteAsync(long note_id, DeleteCallback callback, Object state)
		{
			Delete(note_id, true, callback, state);
		}

        /// <summary>
        /// Lets a user edit a Facebook note through your application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Notes.EditAsync(Constants.NoteId, "An Async test note (Updated)", "My Async note content has also been updated!", AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="note_id">The ID of the note to edit.</param>
        /// <param name="title">The title of the note.</param>
        /// <param name="content">The note's content.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true if successful.</returns>
        public void EditAsync(long note_id, string title, string content, EditCallback callback, Object state)
		{
			Edit(note_id, title, content, true, callback, state);
		}

        /// <summary>
        /// Returns a list of all of the visible notes written by the specified user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///    FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///    api.Notes.GetAsync(AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;note&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns a List of notes with their data in their respective fields (note_id, title, content, created_time, updated_time, uid).</returns>
        /// <remarks> For desktop applications, don't specify a uid; keep the default. (Default value is the logged-in user.)</remarks>
        public void GetAsync(GetCallback callback, Object state)
		{
		    GetAsync(0, callback, state);
		}

        /// <summary>
        /// Returns a list of all of the visible notes written by the specified user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///    FacebookApi api = new FacebookApi(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///    api.Notes.GetAsync(Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;note&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">The user ID of the user whose notes you want to retrieve.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns a List of notes with their data in their respective fields (note_id, title, content, created_time, updated_time, uid).</returns>
        /// <remarks> For desktop applications, don't specify a uid; keep the default. (Default value is the logged-in user.)</remarks>
        public void GetAsync(long uid, GetCallback callback, Object state)
		{
            Get(uid, true, callback, state);
		}

		#endregion

		#endregion Public Methods
        
		#region Private Methods

		private IList<note> Get(long uid, bool isAsync, GetCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.notes.get" } };
			Utilities.AddOptionalParameter(parameterList, "uid", uid);

			if (isAsync)
			{
			    SendRequestAsync<notes_get_response, IList<note>>(parameterList, new FacebookCallCompleted<IList<note>>(callback), state, "note");
				return null;
			}

			var response = SendRequest<notes_get_response>(parameterList);
			return response == null ? null : response.note;
		}
		
        private long Create(long uid, string title, string content, bool isAsync, CreateCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.notes.create" } };
			Utilities.AddRequiredParameter(parameterList, "uid", uid);
			Utilities.AddRequiredParameter(parameterList, "title", title);
			Utilities.AddRequiredParameter(parameterList, "content", content);

			if (isAsync)
			{
				SendRequestAsync<notes_create_response, long>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey), new FacebookCallCompleted<long>(callback), state);
				return 0;
			}

			var response = SendRequest<notes_create_response>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey));
			return response == null ? -1 : response.TypedValue;
		}
		
        private bool Edit(long note_id, string title, string content, bool isAsync, EditCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.notes.edit" } };
			Utilities.AddRequiredParameter(parameterList, "note_id", note_id);
			Utilities.AddRequiredParameter(parameterList, "title", title);
			Utilities.AddRequiredParameter(parameterList, "content", content);

			if (isAsync)
			{
				SendRequestAsync<notes_edit_response, bool>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey), new FacebookCallCompleted<bool>(callback), state);
				return true;
			}

			var response = SendRequest<notes_edit_response>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey));
			return response == null ? false : response.TypedValue;
		}

		private bool Delete(long note_id, bool isAsync, DeleteCallback callback, Object state)
		{
			var parameterList = new Dictionary<string, string> { { "method", "facebook.notes.delete" } };
			Utilities.AddRequiredParameter(parameterList, "note_id", note_id);
			
			if (isAsync)
			{
				SendRequestAsync<notes_delete_response, bool>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey), new FacebookCallCompleted<bool>(callback), state);
				return true;
			}

			var response = SendRequest<notes_delete_response>(parameterList, !string.IsNullOrEmpty(SessionInfo.SessionKey));
			return response == null ? false : response.TypedValue;
		}

		#endregion Private Methods

		#endregion Methods

		#region Delegates

        /// <summary>
        /// Delegate called when Get call is completed.
        /// </summary>
        /// <param name="notes">IList of notes</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
		public delegate void GetCallback(IList<note> notes, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when Create call is completed.
        /// </summary>
        /// <param name="note_id">Note identifier</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void CreateCallback(long note_id, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when Edit call is completed.
        /// </summary>
        /// <param name="result">boolean result</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void EditCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when Delete call is completed.
        /// </summary>
        /// <param name="result">boolean result</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void DeleteCallback(bool result, Object state, FacebookException e);

		#endregion Delegates
	}
}

using System;
using System.Collections.Generic;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
    /// <summary>
    /// Facebook Events API methods.
    /// </summary>
    public class Events : RestBase, Facebook.Rest.IEvents
    {
        #region Methods

        #region Constructor

        /// <summary>
        /// Public constructor for facebook.Events
        /// </summary>
        /// <param name="session">Needs a connected Facebook Session object for making requests</param>
        public Events(FacebookSession session)
            : base(session)
        {
        }

        #endregion Constructor

        #region Public Methods

#if !SILVERLIGHT

        #region Synchronous Methods

        /// <summary>
        /// Returns all visible events according to the filters specified.
        /// This may be used to find all events of a user, or to query specific eids.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Events.Get(Constants.UserId, new List&lt;long&gt; { Constants.EventId }, new DateTime(2000, 1, 1), DateTime.Now.AddYears(1), "attending");
        /// </code>
        /// </example>
        /// <param name="uid">Filter by events associated with a user with this uid.</param>
        /// <param name="eids">Filter by this list of event ids. This is a comma-separated list of eids.</param>
        /// <param name="startTime">Filter with this UTC as lower bound. A missing or zero parameter indicates no lower bound.</param>
        /// <param name="endTime">Filter with this UTC as upper bound. A missing or zero parameter indicates no upper bound.</param>
        /// <param name="rsvp_status">Filter by this RSVP status.  attending,unsure,declined,not_replied </param>
        /// <returns>This method returns all events satisfying the filters specified. The method can be used to return all events associated with user, or query a specific set of events by a list of eids.</returns> 
        public IList<facebookevent> Get(long? uid, List<long> eids, DateTime? startTime, DateTime? endTime, string rsvp_status)
        {
            return Get(uid, eids, startTime, endTime, rsvp_status, false, null, null);
        }

        /// <summary>
        /// Returns all visible events according to the filters specified.
        /// This may be used to find all events of a user, or to query specific eids.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Events.Get();
        /// </code>
        /// </example>
        /// <returns>This method returns all events satisfying the filters specified. The method can be used to return all events associated with user, or query a specific set of events by a list of eids.</returns> 
        public IList<facebookevent> Get()
        {
            return Get(Session.UserId, null, null, null, null);
        }

        /// <summary>
        /// Returns all visible events according to the filters specified.
        /// This may be used to find all events of a user, or to query specific eids.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Events.Get(Constants.UserId);
        /// </code>
        /// </example>
        /// <param name="uid">Filter by events associated with a user with this uid.</param>
        /// <returns>This method returns all events satisfying the filters specified. The method can be used to return all events associated with user, or query a specific set of events by a list of eids.</returns> 
        public IList<facebookevent> Get(long? uid)
        {
            return Get(uid, null, null, null, null);
        }

        /// <summary>
        /// Returns all visible events according to the filters specified.
        /// This may be used to find all events of a user, or to query specific eids.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Events.Get(Constants.UserId, new List&lt;long&gt; { Constants.EventId });
        /// </code>
        /// </example>
        /// <param name="uid">Filter by events associated with a user with this uid.</param>
        /// <param name="eids">Filter by this list of event ids. This is a comma-separated list of eids.</param>
        /// <returns>This method returns all events satisfying the filters specified. The method can be used to return all events associated with user, or query a specific set of events by a list of eids.</returns> 
        public IList<facebookevent> Get(long? uid, List<long> eids)
        {
            return Get(uid, eids, null, null, null);
        }

        /// <summary>
        /// Returns all visible events according to the filters specified.
        /// This may be used to find all events of a user, or to query specific eids.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Events.Get(new List&lt;long&gt; { Constants.EventId });
        /// </code>
        /// </example>
        /// <param name="eids">Filter by this list of event ids. This is a comma-separated list of eids.</param>
        /// <returns>This method returns all events satisfying the filters specified. The method can be used to return all events associated with user, or query a specific set of events by a list of eids.</returns> 
        public IList<facebookevent> Get(List<long> eids)
        {
            return Get(0, eids, null, null, null);
        }

        /// <summary>
        /// Returns all visible events according to the filters specified.
        /// This may be used to find all events of a user, or to query specific eids.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Events.Get(Constants.UserId, new List&lt;long&gt; { Constants.EventId }, new DateTime(2000, 1, 1), DateTime.Now.AddYears(1));
        /// </code>
        /// </example>
        /// <param name="uid">Filter by events associated with a user with this uid.</param>
        /// <param name="eids">Filter by this list of event ids. This is a comma-separated list of eids.</param>
        /// <param name="startTime">Filter with this UTC as lower bound. A missing or zero parameter indicates no lower bound.</param>
        /// <param name="endTime">Filter with this UTC as upper bound. A missing or zero parameter indicates no upper bound.</param>
        /// <returns>This method returns all events satisfying the filters specified. The method can be used to return all events associated with user, or query a specific set of events by a list of eids.</returns> 
        public IList<facebookevent> Get(long? uid, List<long> eids, DateTime? startTime, DateTime? endTime)
        {
            return Get(uid, eids, null, null, null);
        }

        /// <summary>
        /// Cancels an event. The application must be an admin of the event.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Events.Cancel(Constants.EventId, "cancel message");
        /// </code>
        /// </example>
        /// <param name="eid">The event ID.</param>
        /// <param name="cancelMessage">The message sent explaining why the event was canceled. You can pass an empty string if you don't want to provide an explanation.</param>
        /// <returns>This method returns true if the Cancel is successful.</returns>
        public bool Cancel(long eid, string cancelMessage)
        {
            return Cancel(eid, cancelMessage, false, null, null);
        }

        /// <summary>
        /// Creates an event on behalf of the user if the application has an active session; otherwise it creates an event on behalf of the application.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// facebookevent eventInfo = new facebookevent
        /// {
        ///     description = "event description",
        ///     end_date = DateTime.Now.AddDays(7),
        ///     event_subtype = "1",
        ///     event_type = "1",
        ///     location = "location",
        ///     venue = new location { city = "chicago" },
        ///     start_date = DateTime.Now.AddDays(1),
        ///     host = "Facebook Samples",
        ///     name = "test event"
        /// };
        /// var result = api.Events.Create(eventInfo);
        /// </code>
        /// </example>
        /// <param name="eventInfo">The event information. See the Facebook API Notes for information on what parameters to include in the object.</param>
        /// <returns>This method returns the identifier of the created event.</returns>
        /// <remarks>
        /// Create an event - You must pass the following parameters in the event_info array: 
        /// name 
        /// category 
        /// subcategory 
        /// location 
        /// start_time 
        /// end_time 
        /// The start_time and end_time are the times that were input by the event creator, converted to UTC after assuming that they were in Pacific time (Daylight Savings or Standard, depending on the date of the event), then converted into Unix epoch time. 
        ///
        /// Optionally, you can pass the following parameters in the event_info array: 
        ///    
        /// street 
        /// phone 
        /// email 
        /// host_id 
        /// host 
        /// desc 
        /// privacy_type 
        /// tagline 
        /// </remarks>
        public long Create(facebookevent eventInfo)
        {
            return Create(eventInfo, false, null, null);
        }

        /// <summary>
        /// Edits an existing event. The application must be an admin of the event.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// facebookevent eventInfo = new facebookevent
        /// {
        ///     description = "event description 2",
        ///     end_date = DateTime.Now.AddDays(7),
        ///     event_subtype = "1",
        ///     event_type = "1",
        ///     location = "location",
        ///     venue = new location { city = "chicago" },
        ///     start_date = DateTime.Now.AddDays(1),
        ///     host = "Facebook Samples",
        ///     name = "test event"
        /// };
        /// var result = api.Events.Edit(Constants.EventId, eventInfo);
        /// </code>
        /// </example>
        /// <param name="eid">The event ID.</param>
        /// <param name="eventInfo">The event information. See the Facebook API Notes for information on what parameters to include in the object.</param>
        /// <returns>This method returns true if successful, or an error code otherwise.</returns>
        public bool Edit(long eid, facebookevent eventInfo)
        {
            return Edit(eid, eventInfo, false, null, null);
        }

        /// <summary>
        /// Sets the attendance option for the current user.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Events.Rsvp(Constants.EventId, "attending");
        /// </code>
        /// </example>
        /// <param name="eid">The event ID.</param>
        /// <param name="rsvp_Status">The user's RSVP status. Specify attending, unsure, or declined.</param>
        /// <returns>This method returns true if successful.</returns>
        public bool Rsvp(long eid, string rsvp_Status)
        {
            return Rsvp(eid, rsvp_Status, false, null, null);
        }

        /// <summary>
        /// Returns membership list data associated with an event.
        /// </summary>
        /// <example>
        /// <code>
        /// Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.SessionSecret, Constants.SessionKey));
        /// var result = api.Events.GetMembers(Constants.EventId);
        /// </code>
        /// </example>
        /// <param name="eid">The event ID.</param>
        /// <returns>This method returns four (possibly empty) lists of users associated with an event, keyed on their RSVP statuses. These lists should never share any members.</returns>
        public event_members GetMembers(long eid)
        {
            return GetMembers(eid, false, null, null);
        }

        #endregion Synchronous Methods

#endif

        #region Asynchronous Methods

        /// <summary>
        /// Returns all visible events according to the filters specified.
        /// This may be used to find all events of a user, or to query specific eids.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Events.GetAsync(AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;facebookevent&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <returns>This method returns all events satisfying the filters specified. The method can be used to return all events associated with user, or query a specific set of events by a list of eids.</returns> 
        public void GetAsync(GetEventsCallback callback, Object state)
		{
			Get(Session.UserId, null, null, null, null, true, callback, state);
		}

        /// <summary>
        /// Returns all visible events according to the filters specified.
        /// This may be used to find all events of a user, or to query specific eids.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Events.GetAsync(Constants.UserId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;facebookevent&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">Filter by events associated with a user with this uid.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <returns>This method returns all events satisfying the filters specified. The method can be used to return all events associated with user, or query a specific set of events by a list of eids.</returns> 
        public void GetAsync(long? uid, GetEventsCallback callback, Object state)
		{
			GetAsync(uid, null, null, null, null, callback, state);
		}

        /// <summary>
        /// Returns all visible events according to the filters specified.
        /// This may be used to find all events of a user, or to query specific eids.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Events.GetAsync(Constants.UserId, new List&lt;long&gt; { Constants.EventId }, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;facebookevent&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">Filter by events associated with a user with this uid.</param>
        /// <param name="eids">Filter by this list of event ids. This is a comma-separated list of eids.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <returns>This method returns all events satisfying the filters specified. The method can be used to return all events associated with user, or query a specific set of events by a list of eids.</returns> 
        public void GetAsync(long? uid, List<long> eids, GetEventsCallback callback, Object state)
		{
			GetAsync(uid, eids, null, null, null, callback, state);
		}

        /// <summary>
        /// Returns all visible events according to the filters specified.
        /// This may be used to find all events of a user, or to query specific eids.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Events.GetAsync(new List&lt;long&gt; { Constants.EventId }, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;facebookevent&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="eids">Filter by this list of event ids. This is a comma-separated list of eids.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <returns>This method returns all events satisfying the filters specified. The method can be used to return all events associated with user, or query a specific set of events by a list of eids.</returns> 
        public void GetAsync(List<long> eids, GetEventsCallback callback, Object state)
		{
			GetAsync(0, eids, null, null, null, callback, state);
		}

        /// <summary>
        /// Returns all visible events according to the filters specified.
        /// This may be used to find all events of a user, or to query specific eids.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Events.GetAsync(Constants.UserId, new List&lt;long&gt; { Constants.EventId }, new DateTime(2000, 1, 1), DateTime.Now.AddYears(1), AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;facebookevent&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">Filter by events associated with a user with this uid.</param>
        /// <param name="eids">Filter by this list of event ids. This is a comma-separated list of eids.</param>
        /// <param name="startTime">Filter with this UTC as lower bound. A missing or zero parameter indicates no lower bound.</param>
        /// <param name="endTime">Filter with this UTC as upper bound. A missing or zero parameter indicates no upper bound.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <returns>This method returns all events satisfying the filters specified. The method can be used to return all events associated with user, or query a specific set of events by a list of eids.</returns> 
        public void GetAsync(long? uid, List<long> eids, DateTime? startTime, DateTime? endTime, GetEventsCallback callback, Object state)
		{
			GetAsync(uid, eids, null, null, null, callback, state);
		}

        /// <summary>
        /// Returns all visible events according to the filters specified.
        /// This may be used to find all events of a user, or to query specific eids.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Events.GetAsync(Constants.UserId, new List&lt;long&gt; { Constants.EventId }, new DateTime(2000, 1, 1), DateTime.Now.AddYears(1), "attending", AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(IList&lt;facebookevent&gt; result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="uid">Filter by events associated with a user with this uid.</param>
        /// <param name="eids">Filter by this list of event ids. This is a comma-separated list of eids.</param>
        /// <param name="startTime">Filter with this UTC as lower bound. A missing or zero parameter indicates no lower bound.</param>
        /// <param name="endTime">Filter with this UTC as upper bound. A missing or zero parameter indicates no upper bound.</param>
        /// <param name="rsvp_status">Filter by this RSVP status.  attending,unsure,declined,not_replied </param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <returns>This method returns all events satisfying the filters specified. The method can be used to return all events associated with user, or query a specific set of events by a list of eids.</returns> 
        public void GetAsync(long? uid, List<long> eids, DateTime? startTime, DateTime? endTime, string rsvp_status, GetEventsCallback callback, Object state)
		{
            Get(uid, eids, startTime, endTime, rsvp_status, true, callback, state);
		}

        /// <summary>
        /// Cancels an event. The application must be an admin of the event.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Events.CancelAsync(Constants.EventId, "cancel message", AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="eid">The event ID.</param>
        /// <param name="cancelMessage">The message sent explaining why the event was canceled. You can pass an empty string if you don't want to provide an explanation.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <returns>This method returns true if the Cancel is successful.</returns>
        public void CancelAsync(long eid, string cancelMessage, CancelEventCallback callback, Object state)
        {
            Cancel(eid, cancelMessage, true, callback, state);
        }

        /// <summary>
        /// Creates an event on behalf of the user if the application has an active session; otherwise it creates an event on behalf of the application.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     facebookevent eventInfo = new facebookevent
        ///     {
        ///         description = "event description",
        ///         end_date = DateTime.Now.AddDays(7),
        ///         event_subtype = "1",
        ///         event_type = "1",
        ///         location = "location",
        ///         venue = new location { city = "chicago" },
        ///         start_date = DateTime.Now.AddDays(1),
        ///         host = "Facebook Samples",
        ///         name = "test event"
        ///     };
        ///     api.Events.CreateAsync(eventInfo, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(long result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="eventInfo">The event information. See the Facebook API Notes for information on what parameters to include in the object.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns the identifier of the created event.</returns>
        /// <remarks>
        /// Create an event - You must pass the following parameters in the event_info array: 
        /// name 
        /// category 
        /// subcategory 
        /// location 
        /// start_time 
        /// end_time 
        /// The start_time and end_time are the times that were input by the event creator, converted to UTC after assuming that they were in Pacific time (Daylight Savings or Standard, depending on the date of the event), then converted into Unix epoch time. 
        ///
        /// Optionally, you can pass the following parameters in the event_info array: 
        ///    
        /// street 
        /// phone 
        /// email 
        /// host_id 
        /// host 
        /// desc 
        /// privacy_type 
        /// tagline 
        /// </remarks>
        public void CreateAsync(facebookevent eventInfo, CreateEventCallback callback, Object state)
        {
            Create(eventInfo, true, callback, state);
        }

        /// <summary>
        /// Edits an existing event. The application must be an admin of the event.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     facebookevent eventInfo = new facebookevent
        ///     {
        ///         description = "event description 2",
        ///         end_date = DateTime.Now.AddDays(7),
        ///         event_subtype = "1",
        ///         event_type = "1",
        ///         location = "location",
        ///         venue = new location { city = "chicago" },
        ///         start_date = DateTime.Now.AddDays(1),
        ///         host = "Facebook Samples",
        ///         name = "test event"
        ///     };
        ///     api.Events.EditAsync(Constants.EventId, eventInfo, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="eid">The event ID.</param>
        /// <param name="eventInfo">The event information. See the Facebook API Notes for information on what parameters to include in the object.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true if successful, or an error code otherwise.</returns>
        public void EditAsync(long eid, facebookevent eventInfo, EditEventCallback callback, Object state)
        {
            Edit(eid, eventInfo, true, callback, state);
        }

        /// <summary>
        /// Sets the attendance option for the current user.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Events.RsvpAsync(Constants.EventId, "attending", AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(bool result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="eid">The event ID.</param>
        /// <param name="rsvp_Status">The user's RSVP status. Specify attending, unsure, or declined.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns true if successful.</returns>
        public void RsvpAsync(long eid, string rsvp_Status, RsvpCallback callback, Object state)
        {
            Rsvp(eid, rsvp_Status, true, callback, state);
        }

        /// <summary>
        /// Returns membership list data associated with an event.
        /// </summary>
        /// <example>
        /// <code>
        /// private static void RunDemoAsync()
        /// {
        ///     Api api = new Api(new DesktopSession(Constants.ApplicationKey, Constants.ApplicationSecret, Constants.ApplicationSecret, Constants.SessionKey));
        ///     api.Events.GetMembersAsync(Constants.EventId, AsyncDemoCompleted, null);
        /// }
        ///
        /// private static void AsyncDemoCompleted(event_members result, Object state, FacebookException e)
        /// {
        ///     var actual = result;
        /// }
        /// </code>
        /// </example>
        /// <param name="eid">The event ID.</param>
        /// <param name="callback">The AsyncCallback delegate</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>        
        /// <returns>This method returns four (possibly empty) lists of users associated with an event, keyed on their RSVP statuses. These lists should never share any members.</returns>
        public void GetMembersAsync(long eid, EventGetMembersCallback callback, Object state)
        {
            GetMembers(eid, true, callback, state);
        }

        #endregion Asynchronous Methods

        #endregion Public Methods
        
        #region Private Methods

        private IList<facebookevent> Get(long? uid, List<long> eids, DateTime? startTime, DateTime? endTime, string rsvp_status
            , bool isAsync, GetEventsCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.events.get" } };
            Utilities.AddOptionalParameter(parameterList, "uid", uid);
            Utilities.AddList(parameterList, "eids", eids);
            Utilities.AddOptionalParameter(parameterList, "start_time", DateHelper.ConvertDateToDouble(startTime));
            Utilities.AddOptionalParameter(parameterList, "end_time", DateHelper.ConvertDateToDouble(endTime));
            Utilities.AddOptionalParameter(parameterList, "rsvp_status", rsvp_status);

            if (isAsync)
            {
                SendRequestAsync<events_get_response, IList<facebookevent>>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<IList<facebookevent>>(callback), state, "facebookevent");
                return null;
            }

			var response = SendRequest<events_get_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? null : response.facebookevent;
        }

        private bool Cancel(long eid, string cancelMessage, bool isAsync, CancelEventCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.events.cancel" } };
            Utilities.AddRequiredParameter(parameterList, "eid", eid);
            Utilities.AddOptionalParameter(parameterList, "cancel_message", cancelMessage);

            if (isAsync)
            {
                SendRequestAsync<events_cancel_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
                return true;
            }

			var response = SendRequest<events_cancel_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? false : response.TypedValue;
        }

        private long Create(facebookevent event_info, bool isAsync, CreateEventCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.events.create" } };
            var dict = new Dictionary<string, string>
            {
                {"name", event_info.name},
                {"category", event_info.event_type},
                {"subcategory", event_info.event_subtype},
                {"host", event_info.host},
                {"location", event_info.location},
                {"city", event_info.venue.city},
                {"end_time", event_info.end_time.ToString()},
                {"start_time", event_info.start_time.ToString()}
            };
            if (event_info.venue.street != null)
            {
                dict.Add("street", event_info.venue.street);
            }
            if (event_info.description != null)
            {
                dict.Add("description", event_info.description);
            }
            if (event_info.privacy != null)
            {
                dict.Add("privacy_type", event_info.privacy);
            }
            if (event_info.tagline != null)
            {
                dict.Add("tagline", event_info.tagline);
            }

            Utilities.AddJSONAssociativeArray(parameterList, "event_info", dict);
                
            if (isAsync)
            {
                SendRequestAsync<events_create_response, long>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<long>(callback), state);
                return 0;
            }

			var response = SendRequest<events_create_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? 0 : response.TypedValue;
        }

        private bool Edit(long eid, facebookevent event_info, bool isAsync, EditEventCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.events.edit" } };
            Utilities.AddRequiredParameter(parameterList, "eid", eid);
            var dict = new Dictionary<string, string>
            {
                {"description", event_info.description},
                {"end_time", event_info.end_time.ToString()},
                {"category", event_info.event_type},
                {"subcategory", event_info.event_subtype},
                {"host", event_info.host},
                {"location", event_info.location},
                {"name", event_info.name},
                {"start_time", event_info.start_time.ToString()},
                {"tagline", event_info.tagline}
            };
            Utilities.AddJSONAssociativeArray(parameterList, "event_info", dict);
                
            if (isAsync)
            {
                SendRequestAsync<events_edit_response, bool>(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<bool>(callback), state);
                return true;
            }

			var response = SendRequest<events_edit_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? false : response.TypedValue;
        }

        private bool Rsvp(long eid, string rsvpStatus, bool isAsync, RsvpCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> {{"method", "facebook.events.rsvp"}};
            Utilities.AddRequiredParameter(parameterList, "eid", eid);
            Utilities.AddRequiredParameter(parameterList, "rsvp_status", rsvpStatus);

            if (isAsync)
            {
                SendRequestAsync<events_rsvp_response, bool>(parameterList, new FacebookCallCompleted<bool>(callback), state);
               return true;
            }

            var response = SendRequest<events_rsvp_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
			return response == null ? false : response.TypedValue;
        }

        private event_members GetMembers(long eid, bool isAsync, EventGetMembersCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.events.getMembers" } };
            Utilities.AddRequiredParameter(parameterList, "eid", eid);

            if (isAsync)
            {
                SendRequestAsync(parameterList, !string.IsNullOrEmpty(Session.SessionKey), new FacebookCallCompleted<event_members>(callback), state);
                return null;
            }

			return SendRequest<events_getMembers_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey));
        }

        #endregion Private Methods
        
        #endregion Methods

        #region Delegates

        /// <summary>
        /// Delegate called when GetEvents call completed
        /// </summary>
        /// <param name="events">events information</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void GetEventsCallback(IList<facebookevent> events, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when CancelEvent call completed
        /// </summary>
        /// <param name="result">result of operation</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void CancelEventCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when CreateEvent call is completed.
        /// </summary>
        /// <param name="eventId">event ID of the newly created event</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void CreateEventCallback(long eventId, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when EditEvent call completed
        /// </summary>
        /// <param name="result">result of operation</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void EditEventCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when Rsvp call completed
        /// </summary>
        /// <param name="result">result of operation</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void RsvpCallback(bool result, Object state, FacebookException e);

        /// <summary>
        /// Delegate called when EventGetMembers call completed
        /// </summary>
        /// <param name="evtMembers">event members infomration</param>
        /// <param name="state">An object containing state information for this asynchronous request</param>
        /// <param name="e">Exception object, if the call resulted in exception.</param>
        public delegate void EventGetMembersCallback(event_members evtMembers, Object state, FacebookException e);

        #endregion Delegates
    }
}
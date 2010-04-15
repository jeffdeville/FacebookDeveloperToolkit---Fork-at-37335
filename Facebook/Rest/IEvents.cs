using System;
namespace Facebook.Rest
{
	public interface IEvents : IRestBase
	{
		bool Cancel(long eid, string cancelMessage);
		void CancelAsync(long eid, string cancelMessage, Events.CancelEventCallback callback, object state);
		long Create(Facebook.Schema.facebookevent eventInfo);
		void CreateAsync(Facebook.Schema.facebookevent eventInfo, Events.CreateEventCallback callback, object state);
		bool Edit(long eid, Facebook.Schema.facebookevent eventInfo);
		void EditAsync(long eid, Facebook.Schema.facebookevent eventInfo, Events.EditEventCallback callback, object state);
		System.Collections.Generic.IList<Facebook.Schema.facebookevent> Get();
		System.Collections.Generic.IList<Facebook.Schema.facebookevent> Get(System.Collections.Generic.List<long> eids);
		System.Collections.Generic.IList<Facebook.Schema.facebookevent> Get(long? uid);
		System.Collections.Generic.IList<Facebook.Schema.facebookevent> Get(long? uid, System.Collections.Generic.List<long> eids);
		System.Collections.Generic.IList<Facebook.Schema.facebookevent> Get(long? uid, System.Collections.Generic.List<long> eids, DateTime? startTime, DateTime? endTime);
		System.Collections.Generic.IList<Facebook.Schema.facebookevent> Get(long? uid, System.Collections.Generic.List<long> eids, DateTime? startTime, DateTime? endTime, string rsvp_status);
		void GetAsync(Events.GetEventsCallback callback, object state);
		void GetAsync(System.Collections.Generic.List<long> eids, Events.GetEventsCallback callback, object state);
		void GetAsync(long? uid, Events.GetEventsCallback callback, object state);
		void GetAsync(long? uid, System.Collections.Generic.List<long> eids, Events.GetEventsCallback callback, object state);
		void GetAsync(long? uid, System.Collections.Generic.List<long> eids, DateTime? startTime, DateTime? endTime, Events.GetEventsCallback callback, object state);
		void GetAsync(long? uid, System.Collections.Generic.List<long> eids, DateTime? startTime, DateTime? endTime, string rsvp_status, Events.GetEventsCallback callback, object state);
		Facebook.Schema.event_members GetMembers(long eid);
		void GetMembersAsync(long eid, Events.EventGetMembersCallback callback, object state);
		bool Rsvp(long eid, string rsvp_Status);
		void RsvpAsync(long eid, string rsvp_Status, Events.RsvpCallback callback, object state);
	}
}

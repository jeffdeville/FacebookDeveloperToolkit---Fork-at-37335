using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Facebook.Rest;
using Facebook.Utility;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Schema;
using System.Collections.Generic;

namespace Facebook.Tests.Asynchronous
{
	[TestClass]
	public class EventsTest : AsyncFacebookTest
	{
		/// <summary>
		///A test for getMembers
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getMembersTest()
		{
			var eid = Constants.FBSamples_eid;
			_api.Events.GetMembersAsync(eid, GetMembersCompleted, null);			
		}

		private void GetMembersCompleted(event_members actual, object state, FacebookException e)
		{
			Assert.IsTrue(actual.attending.uid.Count > 0);
			EnqueueTestComplete();
		}

		// TODO: implement the other get tests from the sync tests

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getTest4()
		{
			var uid = Constants.FBSamples_UserId;
			var eids = new List<long> { Constants.FBSamples_eid };
			var start_time = Constants.MinFacebookDate;
			var end_time = DateTime.MaxValue;
			var rsvp_status = "attending";
			_api.Events.GetAsync(uid, eids, start_time, end_time, rsvp_status, GetCompleted, null);
		}

		private void GetCompleted(IList<facebookevent> actual, object state, FacebookException e)
		{
			Assert.IsTrue(actual.Count > 0);
			EnqueueTestComplete();
		}
		
		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void createEditRsvpDeleteTest()
		{
			var event_info = new facebookevent
			{
				description = "event description",
				end_date = System.DateTime.Now.AddDays(7),
				event_subtype = "1",
				event_type = "1",
				location = "location",
				venue = new location() { city = "chicago" },
				start_date = System.DateTime.Now.AddDays(1),
				host = "FB Samples",
				name = "create event test"
			};
			_api.Events.CreateAsync(event_info, CreateCompleted, event_info);
		}

		private void CreateCompleted(long actual, object state, FacebookException e)
		{
			Assert.IsTrue(actual > 0);
			var event_info = (facebookevent)state;
			event_info.name = "edited name";
			_api.Events.EditAsync(actual, event_info, EditCompleted, actual);
		}

		private void EditCompleted(bool result, object state, FacebookException e)
		{
			var eventId = (long)state;
			Assert.IsTrue(result);
			_api.Events.RsvpAsync(eventId, "attending", RsvpCompleted, eventId);
		}

		private void RsvpCompleted(bool result, object state, FacebookException e)
		{
			var eventId = (long)state;
			Assert.IsTrue(result);
			_api.Events.CancelAsync(eventId, "test cancel", CancelCompleted, null);
		}

		private void CancelCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void createRsvpEditRsvpDeleteTest2()
		{
			var event_info = new facebookevent
			{
				description = "event description",
				end_date = System.DateTime.Now.AddDays(7),
				event_subtype = "1",
				event_type = "1",
				location = "location",
				venue = new location() { city = "chicago" },
				start_date = System.DateTime.Now.AddDays(1),
				host = "FB Samples",
				name = "create event test"
			};
			_api.Events.CreateAsync(event_info, Create2Completed, event_info);			
		}

		private void Create2Completed(long actual, object state, FacebookException e)
		{
			Assert.IsTrue(actual > 0);
			_api.Events.RsvpAsync(actual, "unsure", UnsureRsvp2Completed, actual);
		}

		private void UnsureRsvp2Completed(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);

			var event_info = new facebookevent
			{
				description = "event description",
				end_date = System.DateTime.Now.AddDays(7),
				event_subtype = "1",
				event_type = "1",
				location = "location",
				venue = new location() { city = "chicago" },
				start_date = System.DateTime.Now.AddDays(1),
				host = "FB Samples",
				name = "create event test"
			};

			event_info.name = "edited name";
			var eventId = (long)state;
			_api.Events.EditAsync(eventId, event_info, Edit2Completed, eventId);
		}

		private void Edit2Completed(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			var eventId = (long)state;
			_api.Events.RsvpAsync(eventId, "declined", DeclinedRsvp2Completed, eventId);
		}

		private void DeclinedRsvp2Completed(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			var eventId = (long)state;
			_api.Events.CancelAsync(eventId, "test cancel", Cancel2Completed, null);
		}

		private void Cancel2Completed(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}
	}
}

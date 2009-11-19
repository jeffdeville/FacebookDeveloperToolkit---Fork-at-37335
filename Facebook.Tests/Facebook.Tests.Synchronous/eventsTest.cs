using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Schema;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Tests.Synchronous
{
	/// <summary>
	///This is a test class for eventsTest and is intended
	///to contain all eventsTest Unit Tests
	///</summary>
	[TestClass]
	public class eventsTest : Test
	{
		/// <summary>
		///A test for getMembers
		///</summary>
		[TestMethod]
		public void getMembersTest()
		{
			var eid = Constants.FBSamples_eid;
			var actual = _api.Events.GetMembers(eid);
			Assert.IsTrue(actual.attending.uid.Count > 0);
		}

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		public void getTest5()
		{
            var actual = _api.Events.Get();
			Assert.IsTrue(actual.Count > 0);
		}

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		public void getTest4()
		{
			var uid = Constants.FBSamples_UserId;
			var eids = new List<long> {Constants.FBSamples_eid};
			var start_time = Constants.MinFacebookDate;
			var end_time = DateTime.MaxValue;
			var rsvp_status = "attending";
            var actual = _api.Events.Get(uid, eids, start_time, end_time, rsvp_status);
			Assert.IsTrue(actual.Count > 0);
		}

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		public void getTest3()
		{
			var uid = Constants.FBSamples_UserId;
            var actual = _api.Events.Get(uid);
			Assert.IsTrue(actual.Count > 0);
		}

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		public void getTest2()
		{
			var uid = Constants.FBSamples_UserId;
			var eids = new List<long> {Constants.FBSamples_eid};
			var start_time = Constants.MinFacebookDate;
			var end_time = DateTime.MaxValue;
            var actual = _api.Events.Get(uid, eids, start_time, end_time);
			Assert.IsTrue(actual.Count > 0);
		}

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		public void getTest1()
		{
			var eids = new List<long> {Constants.FBSamples_eid};
            var actual = _api.Events.Get(eids);
			Assert.IsTrue(actual.Count > 0);
		}

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		public void getTest()
		{
			var uid = Constants.FBSamples_UserId;
			var eids = new List<long> {Constants.FBSamples_eid};
            var actual = _api.Events.Get(uid, eids);
			Assert.IsTrue(actual.Count > 0);
		}
        /// <summary>
        ///A test for get
        ///</summary>
        [TestMethod]
        public void createEditRsvpDeleteTest()
        {
            var event_info = new facebookevent { description = "event description", 
                end_date = System.DateTime.Now.AddDays(7), 
                event_subtype = "1", 
                event_type = "1", 
                location = "location", 
                venue = new location(){city="chicago"},
                start_date = System.DateTime.Now.AddDays(1),
                host = "FB Samples",
                name = "create event test" };
            var actual = _api.Events.Create(event_info);
            Assert.IsTrue(actual > 0);

            event_info.name = "edited name";
            var e = _api.Events.Edit(actual, event_info);
            Assert.IsTrue(e);
            var r = _api.Events.Rsvp(actual, "attending");
            Assert.IsTrue(r);
            var c = _api.Events.Cancel(actual, "test cancel");
            Assert.IsTrue(c);
        }
        /// <summary>
        ///A test for get
        ///</summary>
        [TestMethod]
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
            var actual = _api.Events.Create(event_info);
            Assert.IsTrue(actual > 0);
            var r1 = _api.Events.Rsvp(actual, "unsure");
            Assert.IsTrue(r1);
            event_info.name = "edited name";
            var e = _api.Events.Edit(actual, event_info);
            Assert.IsTrue(e);
            var r2 = _api.Events.Rsvp(actual, "declined");
            Assert.IsTrue(r2);
            var c = _api.Events.Cancel(actual, "test cancel");
            Assert.IsTrue(c);
        }
    }
}
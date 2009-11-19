using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Tests.Synchronous
{
	/// <summary>
	///This is a test class for groupsTest and is intended
	///to contain all groupsTest Unit Tests
	///</summary>
	[TestClass]
	public class groupsTest : Test
	{
        /// <summary>
		///A test for getMembers
		///</summary>
		[TestMethod]
		public void getMembersTest()
		{
			var gid = Constants.FBSamples_gid;
			var actual = _api.Groups.GetMembers(gid);
			Assert.IsTrue(actual.members.uid.Count > 0);
		}

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		public void getTest3()
		{
            var uid = _api.Session.UserId;
            var actual = _api.Groups.Get(uid);
			Assert.IsTrue(actual.Count > 0);
		}

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		public void getTest2()
		{
            var actual = _api.Groups.Get();
			Assert.IsTrue(actual.Count > 0);
		}

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		public void getTest1()
		{
            var uid = _api.Session.UserId;
			var gids = new List<long> {Constants.FBSamples_gid};
            var actual = _api.Groups.Get(uid, gids);
			Assert.IsTrue(actual.Count > 0);
		}

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		public void getTest()
		{
            var gids = new List<long> { Constants.FBSamples_gid };
            var actual = _api.Groups.Get(gids);
			Assert.IsTrue(actual.Count > 0);
		}
	}
}
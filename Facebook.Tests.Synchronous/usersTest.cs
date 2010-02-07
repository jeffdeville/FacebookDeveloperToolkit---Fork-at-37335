using Facebook.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Tests.Synchronous
{
	/// <summary>
	///This is a test class for usersTest and is intended
	///to contain all usersTest Unit Tests
	///</summary>
	[TestClass]
	public class usersTest : Test
	{
		/// <summary>
		///A test for isAppUser
		///</summary>
		[TestMethod]
		public void isAppUserTest()
		{
			var uid = Constants.FBSamples_UserId;
			var expected = true;
			var actual = _api.Users.IsAppUser(uid);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for setStatus
		///</summary>
		[TestMethod]
		public void setStatusTest1()
		{
            var status = "testing setStatus Api calls";
			var expected = true;
            var actual = _api.Users.SetStatus(status);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for setStatus
		///</summary>
		[TestMethod]
		public void setStatusTest()
		{
            var status = "just called setStatus from the test harness";
			var status_includes_verb = true;
			var expected = true;
            var actual = _api.Users.SetStatus(status, status_includes_verb);
			Assert.AreEqual(expected, actual);
		}


		/// <summary>
		///A test for hasAppPermission
		///</summary>
		[TestMethod]
		public void hasAppPermissionTest()
		{
            var ext_perm = Enums.ExtendedPermissions.create_event;
			var expected = true;
            var actual = _api.Users.HasAppPermission(ext_perm);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for getLoggedInUser
		///</summary>
		[TestMethod]
		public void getLoggedInUserTest()
		{
            var expected = Constants.FBSamples_UserId;
            var actual = _api.Users.GetLoggedInUser();
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for getInfo
		///</summary>
		[TestMethod]
		public void getInfoTest3()
		{
            var uid = Constants.FBSamples_UserId;
            var actual = _api.Users.GetInfo(uid);
			Assert.IsNotNull(actual);
			Assert.IsTrue(actual.name.Equals(Constants.FBSamples_Name));
		}

		/// <summary>
		///A test for getInfo
		///</summary>
		[TestMethod]
		public void getInfoTest2()
		{
            var actual = _api.Users.GetInfo();
			Assert.IsNotNull(actual);
			Assert.IsTrue(actual.name.Equals(Constants.FBSamples_Name));
		}

		/// <summary>
		///A test for getInfo
		///</summary>
		[TestMethod]
		public void getInfoTest1()
		{
            var uids = Constants.FBSamples_UserId;
            user actual = _api.Users.GetInfo(uids);
			Assert.IsNotNull(actual);
			Assert.IsTrue(actual.name.Equals(Constants.FBSamples_Name));
		}

		/// <summary>
		///A test for getInfo
		///</summary>
		[TestMethod]
		public void getInfoTest()
		{
            var userIds = Constants.FBSamples_uids;
            var actual = _api.Users.GetInfo(userIds);
			Assert.IsNotNull(actual);
			Assert.IsTrue(actual.Count > 1);
			Assert.IsTrue(actual[0].name.Equals(Constants.FBSamples_Name));
		}
        /// <summary>
        ///A test for getInfo
        ///</summary>
        [TestMethod]
        public void getStandardInfoTest()
        {
            var userIds = Constants.FBSamples_uids;
			var actual = _apiWeb.Users.GetStandardInfo(userIds);
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count > 1);
            Assert.IsTrue(actual[0].name.Equals(Constants.FBSamples_Name));
        }

		// TODO: Uncomment this test when the issue is fixed on Facebook's side. Because they're sending an incorrect return value, our test fails.
		// The bug is being tracked at http://bugs.developers.facebook.com/show_bug.cgi?id=5129 . When it is resolved, we should be able to uncomment this test.
		///// <summary>
		/////A test for getInfo
		/////</summary>
		//[TestMethod]
		//public void isVerifiedTest()
		//{
		//    var actual = _api.Users.IsVerified(Constants.FBSamples_UserId);
		//    Assert.IsTrue(actual);
		//}
    }
}
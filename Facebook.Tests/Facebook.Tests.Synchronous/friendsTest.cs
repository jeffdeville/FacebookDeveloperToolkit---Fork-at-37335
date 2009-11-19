using System.Collections.Generic;
using Facebook.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Session;
using Facebook.Rest;

namespace Facebook.Tests.Synchronous
{
	/// <summary>
	///This is a test class for friendsTest and is intended
	///to contain all friendsTest Unit Tests
	///</summary>
	[TestClass]
	public class friendsTest : Test
	{
        /// <summary>
        ///A test for getUserObjects
        ///</summary>
        [TestMethod]
        public void getUserObjectsTest2()
        {
            var uid = Constants.FBSamples_UserId;
            var actual = _api.Friends.GetUserObjects(uid);
            Assert.IsTrue(actual.Count > 0);
            Assert.IsNotNull(actual[0].name);
        }
        /// <summary>
		///A test for getUserObjects
		///</summary>
		[TestMethod]
		public void getUserObjectsTest1()
		{
			var flid = Constants.FBSamples_flid;
			var actual = _api.Friends.GetUserObjects(0, flid);
			Assert.IsTrue(actual.Count > 0);
			Assert.IsNotNull(actual[0].name);
		}

		/// <summary>
		///A test for getUserObjects
		///</summary>
		[TestMethod]
		public void getUserObjectsTest()
		{
			var actual = _api.Friends.GetUserObjects();
			Assert.IsTrue(actual.Count > 0);
			Assert.IsNotNull(actual[0].name);
		}

		/// <summary>
		///A test for getLists
		///</summary>
		[TestMethod]
		public void getListsTest()
		{
			var actual = _api.Friends.GetLists();
			Assert.IsTrue(actual.Count > 0);
			Assert.AreEqual(actual[0].flid, Constants.FBSamples_flid);
		}

		/// <summary>
		///A test for getAppUsersObjects
		///</summary>
		[TestMethod]
		public void getAppUsersObjectsTest()
		{
			var actual = _api.Friends.GetAppUsersObjects();
			Assert.IsTrue(actual.Count > 0);
			Assert.IsNotNull(actual[0].name);
		}

		/// <summary>
		///A test for getAppUsers
		///</summary>
		[TestMethod]
		public void getAppUsersTest()
		{
			var actual = _api.Friends.GetAppUsers();
			Assert.IsTrue(actual.Count > 0);
		}

		/// <summary>
		///A test for getMutualFriends
		///</summary>
		[TestMethod]
		public void getMutualFriendsTest1()
		{
			var friends = _api.Friends.GetMutualFriends(Constants.FBSamples_friend1);
			Assert.IsNotNull(friends);
			Assert.IsTrue(friends.Count > 0);
		}

		/// <summary>
		///A test for getMutualFriends
		///</summary>
		[TestMethod]
		public void getMutualFriendsTest2()
		{
			var friends = _apiWeb.Friends.GetMutualFriends(Constants.FBSamples_friend1, Constants.FBSamples_friend2);
			Assert.IsNotNull(friends);
			Assert.IsTrue(friends.Count > 0);
		}

        /// <summary>
        ///A test for get
        ///</summary>
        [TestMethod]
        public void getTest2()
        {
            var uid = Constants.FBSamples_UserId;
            var actual = _api.Friends.Get(uid);
            Assert.IsTrue(actual.Count > 0);
        }
        /// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		public void getTest1()
		{
			var flid = Constants.FBSamples_flid;
			var actual = _api.Friends.Get(0, flid);
			Assert.IsTrue(actual.Count > 0);
		}

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		public void getTest()
		{
			var actual = _api.Friends.Get();
			Assert.IsTrue(actual.Count > 0);
		}

		/// <summary>
		///A test for areFriends
		///</summary>
		[TestMethod]
		public void areFriendsTest3()
		{
			var uid1 = Constants.FBSamples_friend1;
			var uid2 = Constants.FBSamples_friend2;
			var actual = _api.Friends.AreFriends(uid1, uid2);
			Assert.IsTrue(actual[0].are_friends.Value);
		}

		/// <summary>
		///A test for areFriends
		///</summary>
		[TestMethod]
		public void areFriendsTest2()
		{
            var users1 = new List<user>();
			var users2 = new List<user>();
			users1.Add(new user {uid = Constants.FBSamples_friend1});
			users2.Add(new user {uid = Constants.FBSamples_friend2});

			var actual = _api.Friends.AreFriends(users1, users2);
			Assert.IsTrue(actual[0].are_friends.Value);
		}

		/// <summary>
		///A test for areFriends
		///</summary>
		[TestMethod]
		public void areFriendsTest1()
		{
            var user1 = new user { uid = Constants.FBSamples_friend1 };
			var user2 = new user {uid = Constants.FBSamples_friend2};
			var actual = _api.Friends.AreFriends(user1, user2);
			Assert.IsTrue(actual[0].are_friends.Value);
		}

		/// <summary>
		///A test for areFriends
		///</summary>
		[TestMethod]
		public void areFriendsTest()
		{
            var uids1 = new List<long> { Constants.FBSamples_friend1 };
			var uids2 = new List<long> {Constants.FBSamples_friend2};
			var actual = _api.Friends.AreFriends(uids1, uids2);
			Assert.IsTrue(actual[0].are_friends.Value);
		}
	}
}
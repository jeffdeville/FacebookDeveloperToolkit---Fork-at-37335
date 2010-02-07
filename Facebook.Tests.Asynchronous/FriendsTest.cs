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
using Microsoft.Silverlight.Testing;
using Microsoft.Silverlight.Testing.UnitTesting.Metadata.VisualStudio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Facebook.Utility;
using System.Collections;
using Facebook.Schema;

namespace Facebook.Tests.Asynchronous
{
	[TestClass]
	public class FriendsTest : AsyncFacebookTest
	{
		[TestMethod]
		[Asynchronous]
		public void GetTest()
		{
			_api.Friends.GetAsync(GetTestCompleted, null);			
		}

		private void GetTestCompleted(IList<long> friendList, object state, FacebookException e)
		{
			Assert.IsTrue(friendList.Count > 0);
			EnqueueTestComplete();
		}
		
		// TODO: implement getUserObjectsTest2, from sync code

		/// <summary>
		///A test for getUserObjects
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getUserObjectsTest1()
		{
			var flid = Constants.FBSamples_flid;
			_api.Friends.GetUserObjectsAsync(0, flid, GetUserObjectsCompleted, null);
			
		}

		private void GetUserObjectsCompleted(IList<user> actual, object state, FacebookException e)
		{
			Assert.IsTrue(actual.Count > 0);
			Assert.IsNotNull(actual[0].name);
			EnqueueTestComplete();
		}
		
		// TODO: implement GetUserObjectsTest

		/// <summary>
		///A test for getLists
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getListsTest()
		{
			_api.Friends.GetListsAsync(GetListsCompleted, null);
		}

		private void GetListsCompleted(IList<friendlist> actual, object state, FacebookException e)
		{
			Assert.IsTrue(actual.Count > 0);
			Assert.AreEqual(actual[0].flid, Constants.FBSamples_flid);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for getAppUsersObjects
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getAppUsersObjectsTest()
		{
			_api.Friends.GetAppUsersObjectsAsync(GetAppUsersObjectsCompleted, null);
		}

		private void GetAppUsersObjectsCompleted(IList<user> actual, object state, FacebookException e)
		{
			Assert.IsTrue(actual.Count > 0);
			Assert.IsNotNull(actual[0].name);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for getAppUsers
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getAppUsersTest()
		{
			_api.Friends.GetAppUsersAsync(GetAppUsersCompleted, null);
		}

		private void GetAppUsersCompleted(IList<long> actual, object state, FacebookException e)
		{
			Assert.IsTrue(actual.Count > 0);
			EnqueueTestComplete();
		}

		// TODO: implement getMutualFriendsTest1() from sync code

		/// <summary>
		///A test for getMutualFriends
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getMutualFriendsTest2()
		{
			_apiWeb.Friends.GetMutualFriendsAsync(Constants.FBSamples_friend1, Constants.FBSamples_friend2, GetMutualFriends2Completed, null);			
		}

		private void GetMutualFriends2Completed(IList<long> friends, object state, FacebookException e)
		{
			Assert.IsNotNull(friends);
			Assert.IsTrue(friends.Count > 0);
			EnqueueTestComplete();
		}

		// TODO: implement getTest2() from sync code

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getTest1()
		{
			var flid = Constants.FBSamples_flid;
			_api.Friends.GetAsync(0, flid, Get1Completed, null);
		}

		private void Get1Completed(IList<long> actual, object state, FacebookException e)
		{
			Assert.IsTrue(actual.Count > 0);
			EnqueueTestComplete();
		}

		// TODO: implement the other areFriends test methods from sync tests

		/// <summary>
		///A test for areFriends
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void areFriendsTest2()
		{
			var users1 = new List<user>();
			var users2 = new List<user>();
			users1.Add(new user { uid = Constants.FBSamples_friend1 });
			users2.Add(new user { uid = Constants.FBSamples_friend2 });

			_api.Friends.AreFriendsAsync(users1, users2, AreFriends2Completed, null);			
		}

		private void AreFriends2Completed(IList<friend_info> actual, object state, FacebookException e)
		{
			Assert.IsTrue(actual[0].are_friends.Value);
			EnqueueTestComplete();
		}
	}
}

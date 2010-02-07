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
	public class UsersTest : AsyncFacebookTest
	{
		/// <summary>
		///A test for isAppUser
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void isAppUserTest()
		{
			var uid = Constants.FBSamples_UserId;			
			_api.Users.IsAppUserAsync(uid, IsAppUserCompleted, null);			
		}

		private void IsAppUserCompleted(bool actual, object state, FacebookException e)
		{
			Assert.AreEqual(true, actual);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for setStatus
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void setStatusTest1()
		{
			var status = "testing setStatus Api calls";			
			_api.Users.SetStatusAsync(status, SetStatus1Completed, null);
		}

		private void SetStatus1Completed(bool actual, object state, FacebookException e)
		{
			var expected = true;
			Assert.AreEqual(expected, actual);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for setStatus
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void setStatusTest()
		{
			var status = "just called setStatus from the test harness";
			var status_includes_verb = true;			
			var actual = _api.Users.SetStatusAsync(status, status_includes_verb, SetStatusCompleted, null);
			
		}

		private void SetStatusCompleted(bool actual, object state, FacebookException e)
		{
			var expected = true;
			Assert.AreEqual(expected, actual);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for hasAppPermission
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void hasAppPermissionTest()
		{
			var ext_perm = Enums.ExtendedPermissions.create_event;			
			_api.Users.HasAppPermissionAsync(ext_perm, HasAppPermissionCompleted, null);
		}

		private void HasAppPermissionCompleted(bool actual, object state, FacebookException e)
		{
			var expected = true;
			Assert.AreEqual(expected, actual);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for getLoggedInUser
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getLoggedInUserTest()
		{			
			_api.Users.GetLoggedInUserAsync(GetLoggedInUserCompleted, null);
		}

		private void GetLoggedInUserCompleted(long actual, object state, FacebookException e)
		{
			var expected = Constants.FBSamples_UserId;
			Assert.AreEqual(expected, actual);
			EnqueueTestComplete();
		}

		// TODO: implement other getInfo tests from sync tests

		/// <summary>
		///A test for getInfo
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getInfoTest()
		{
			var userIds = Constants.FBSamples_uids;
			_api.Users.GetInfoAsync(userIds, GetInfoCompleted, null);			
		}

		private void GetInfoCompleted(IList<user> actual, object state, FacebookException e)
		{
			Assert.IsNotNull(actual);
			Assert.IsTrue(actual.Count > 1);
			Assert.IsTrue(actual[0].name.Equals(Constants.FBSamples_Name));
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for getStandardInfo
		///</summary>
		[TestMethod]
		public void getStandardInfoTest()
		{
			var userIds = Constants.FBSamples_uids;
			_apiWeb.Users.GetStandardInfoAsync(userIds, GetStandardInfoCompleted, null);
			
		}

		private void GetStandardInfoCompleted(IList<user> actual, object state, FacebookException e)
		{
			Assert.IsNotNull(actual);
			Assert.IsTrue(actual.Count > 1);
			Assert.IsTrue(actual[0].name.Equals(Constants.FBSamples_Name));
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for getInfo
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void isVerifiedTest()
		{
			_api.Users.IsVerifiedAsync(Constants.FBSamples_UserId, IsVerifiedCompleted, null);
			
		}

		private void IsVerifiedCompleted(bool actual, object state, FacebookException e)
		{
			Assert.IsTrue(actual);
			EnqueueTestComplete();
		}
	}
}

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
	public class GroupsTest : AsyncFacebookTest
	{
		/// <summary>
		///A test for getMembers
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getMembersTest()
		{
			var gid = Constants.FBSamples_gid;
			_api.Groups.GetMembersAsync(gid, GetMembersCompleted, null);
		}

		private void GetMembersCompleted(group_members actual, object state, FacebookException e)
		{
			Assert.IsTrue(actual.members.uid.Count > 0);
			EnqueueTestComplete();
		}

		// TODO: implement the other get tests from the sync tests

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getTest1()
		{
			var uid = _api.Session.UserId;
			var gids = new List<long> { Constants.FBSamples_gid };
			_api.Groups.GetAsync(uid, gids, GetCompleted, null);
		}

		private void GetCompleted(IList<group> actual, object state, FacebookException e)
		{
			Assert.IsTrue(actual.Count > 0);
			EnqueueTestComplete();
		}
	}
}

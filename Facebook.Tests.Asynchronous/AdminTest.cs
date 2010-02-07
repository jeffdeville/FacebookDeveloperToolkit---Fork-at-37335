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
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Utility;
using Microsoft.Silverlight.Testing;
using Facebook.Rest;

namespace Facebook.Tests.Asynchronous
{
	[TestClass]
	public class AdminTest : AsyncFacebookTest
	{
		/// <summary>
		///A test for banUsers
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void BanUsersTest()
		{
			List<long> expectedBannedUsers = Constants.FBSamples_uids;
			_apiWeb.Admin.BanUsersAsync(expectedBannedUsers, BanUsersCompleted, null);
		}

		private void BanUsersCompleted(bool result, object state, FacebookException e)
		{
			_apiWeb.Admin.GetBannedUsersAsync(GetBannedUsersCompleted, result);
		}

		private void GetBannedUsersCompleted(IList<long> actualBannedUsers, object state, FacebookException e)
		{
			var actual = (bool)state;
			List<long> expectedBannedUsers = Constants.FBSamples_uids;

			Assert.IsTrue(actual);
			Assert.AreEqual(actualBannedUsers.Count, expectedBannedUsers.Count);

			// Teardown
			_apiWeb.Admin.UnbanUsersAsync(expectedBannedUsers, UnbanUsersCompleted, null);
		}

		private void UnbanUsersCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result, "Unable to teardown (unban) users.");
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for getAllocation
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void GetAllocationTest()
		{
			Admin.IntegrationPointName name = Admin.IntegrationPointName.requests_per_day; // TODO: Initialize to an appropriate value
			_api.Admin.GetAllocationAsync(name, GetAllocationCompleted, null);
		}

		private void GetAllocationCompleted(int actual, object state, FacebookException e)
		{
			Assert.IsTrue(actual > 0);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for getAppProperties
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void GetAppPropertiesTest()
		{
			_apiWeb.Admin.GetAppPropertiesAsync(_apiWeb.Admin.GetApplicationPropertyNames(), GetAppPropertiesCompleted, null);
		}

		private void GetAppPropertiesCompleted(Dictionary<string, string> resultDict, object state, FacebookException e)
		{
			var expectedDict = new Dictionary<string, string>();

			foreach (var prop in _apiWeb.Admin.GetApplicationPropertyNames())
			{
				expectedDict.Add(prop, null);
			}

			Assert.AreEqual(expectedDict.Count, resultDict.Count);
			Assert.IsNotNull(resultDict);
			EnqueueTestComplete();
		}

		// TODO: implement the other Admin tests
	}
}

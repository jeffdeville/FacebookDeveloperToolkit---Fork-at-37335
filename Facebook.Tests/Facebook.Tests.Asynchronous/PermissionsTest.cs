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
	public class PermissionsTest : AsyncFacebookTest
	{
		/// <summary>
		///A test for revokeApiAccess
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void revokeApiAccessTest()
		{
			var api = Constants.FBSamples_WebApplicationKey2;
			var actual = _apiWeb.Permissions.RevokeApiAccessAsync(api, RevokeApiAccessCompleted, null);
		}

		private void RevokeApiAccessCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for grantApiAccess
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void grantApiAccessTest()
		{
			var api = Constants.FBSamples_WebApplicationKey2;
			var method_arr = new List<string> { "admin" };
			var actual = _apiWeb.Permissions.GrantApiAccessAsync(api, method_arr, GrantApiAccessCompleted, null);
		}

		private void GrantApiAccessCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for checkGrantedApiAccess
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void checkGrantedApiAccessTest()
		{
			var api = Constants.FBSamples_WebApplicationKey2;
			var actual = _apiWeb.Permissions.CheckGrantedApiAccessAsync(api, CheckGrantedApiAccessCompleted, null);
		}

		private void CheckGrantedApiAccessCompleted(IList<string> actual, object state, FacebookException e)
		{
			Assert.IsNotNull(actual);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for checkAvailableApiAccess
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void checkAvailableApiAccessTest()
		{
			var api = Constants.FBSamples_WebApplicationKey2;
			var actual = _apiWeb.Permissions.CheckAvailableApiAccessAsync(api, CheckAvailableApiAccessCompleted, null);
		}

		private void CheckAvailableApiAccessCompleted(IList<string> actual, object state, FacebookException e)
		{
			Assert.IsTrue(actual.Count == 0);
			EnqueueTestComplete();
		}

		// TODO: implement this test, filling in values for the TODOs below
		///// <summary>
		/////A test for checkAvailableApiAccess
		/////</summary>
		//[TestMethod()]
		//[Asynchronous]
		//public void permissionsModeTest()
		//{
		//    Api parent = _api;
		//    permissions target = new permissions(parent);
		//    string permissions_apikey = string.Empty; // TODO: Initialize to an appropriate value
		//    IList<string> expected = null; // TODO: Initialize to an appropriate value
		//    IList<string> actual;
		//    actual = target.checkAvailableApiAccess(permissions_apikey);
		//    Assert.AreEqual(expected, actual);
		//}
	}
}

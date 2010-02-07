using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Tests.Synchronous
{
	/// <summary>
	///This is a test class for permissionsTest and is intended
	///to contain all permissionsTest Unit Tests
	///</summary>
	[TestClass]
	public class permissionsTest : Test
	{
        /// <summary>
		///A test for revokeApiAccess
		///</summary>
		[TestMethod]
		public void revokeApiAccessTest()
		{
            var api = Constants.FBSamples_WebApplicationKey2;
			var expected = true;
			var actual = _apiWeb.Permissions.RevokeApiAccess(api);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for grantApiAccess
		///</summary>
		[TestMethod]
		public void grantApiAccessTest()
		{
			var api = Constants.FBSamples_WebApplicationKey2;
			var method_arr = new List<string> {"admin"};
			var expected = true;
			var actual = _apiWeb.Permissions.GrantApiAccess(api, method_arr);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for checkGrantedApiAccess
		///</summary>
		[TestMethod]
		public void checkGrantedApiAccessTest()
		{
			var api = Constants.FBSamples_WebApplicationKey2;
			var actual = _apiWeb.Permissions.CheckGrantedApiAccess(api);
			Assert.IsNotNull(actual);
		}

		/// <summary>
		///A test for checkAvailableApiAccess
		///</summary>
		[TestMethod]
		public void checkAvailableApiAccessTest()
		{
            var api = Constants.FBSamples_WebApplicationKey2;
            var actual = _apiWeb.Permissions.CheckAvailableApiAccess(api);
			Assert.IsTrue(actual.Count == 0);
		}

		///// <summary>
		/////A test for checkAvailableApiAccess
		/////</summary>
		//[TestMethod()]
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
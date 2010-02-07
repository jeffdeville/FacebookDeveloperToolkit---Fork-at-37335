using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Session;
using Facebook.Rest;

namespace Facebook.Tests.Synchronous
{
	/// <summary>
	///This is a test class for pagesTest and is intended
	///to contain all pagesTest Unit Tests
	///</summary>
	[TestClass]
	public class pagesTest : Test
	{
		/// <summary>
		///A test for isFan
		///</summary>
		[TestMethod]
		public void isFanTest1()
		{
            var page_id = Constants.FBSamples_page;
			var expected = true;
			var actual = _api.Pages.IsFan(page_id);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for isFan
		///</summary>
		[TestMethod]
		public void isFanTest()
		{
            var page_id = Constants.FBSamples_page;
			var uid = Constants.FBSamples_UserId;
			var expected = true;
            var actual = _api.Pages.IsFan(page_id, uid);
			Assert.AreEqual(expected, actual);
		}

        ///// <summary>
        /////A test for isAppAdded
        /////</summary>
        //[TestMethod]
        //[Obsolete("facebook deprecated")]
        //public void isAppAddedTest()
        //{
        //    var page_id = Constants.FBSamples_page;
        //    var expected = false;
        //    var actual = _api.Pages.IsAppAdded(page_id);
        //    Assert.AreEqual(expected, actual);
        //}

		/// <summary>
		///A test for isAdmin
		///</summary>
		[TestMethod]
		public void isAdminTest()
		{
            var page_id = Constants.FBSamples_page;
			var expected = true;
            var actual = _api.Pages.IsAdmin(page_id);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for getInfo
		///</summary>
		[TestMethod]
		public void getInfoTest()
		{
            var fields = _api.Pages.GetFields();
			var page_ids = new List<long> {Constants.FBSamples_page};
			int? uid = null;
            var actual = _api.Pages.GetInfo(fields, page_ids, uid);
			Assert.IsNotNull(actual);
			Assert.IsTrue(actual.Count > 0);
			Assert.IsNotNull(actual[0].page_id);
		}

		[TestMethod]
		public void getInfoTest1()
		{
            var fields = _api.Pages.GetFields();
			List<long> page_ids = null;
			int? uid = null;
            var actual = _api.Pages.GetInfo(fields, page_ids, uid);
			Assert.IsNotNull(actual);
			Assert.IsTrue(actual.Count > 0);
			Assert.IsNotNull(actual[0].page_id);
		}

		[TestMethod]
		public void getInfoTest2()
		{
            var fields = _api.Pages.GetFields();
			List<long> page_ids = null;
			long? uid = Constants.FBSamples_UserId;
            var actual = _api.Pages.GetInfo(fields, page_ids, uid);
			Assert.IsNotNull(actual);
			Assert.IsTrue(actual.Count > 0);
			Assert.IsNotNull(actual[0].page_id);
		}
	}
}
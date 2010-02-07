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
	public class PagesTest : AsyncFacebookTest
	{
		// TODO: implement this test
		///// <summary>
		/////A test for isFan
		/////</summary>
		//[TestMethod]
		//public void isFanTest1()
		//{
		//    var page_id = Constants.FBSamples_page;
		//    var expected = true;
		//    var actual = _api.Pages.IsFan(page_id);
		//    Assert.AreEqual(expected, actual);
		//}

		/// <summary>
		///A test for isFan
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void isFanTest()
		{
			var page_id = Constants.FBSamples_page;
			var uid = Constants.FBSamples_UserId;
			_api.Pages.IsFanAsync(page_id, uid, IsFanCompleted, null);
		}

		private void IsFanCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for isAdmin
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void isAdminTest()
		{
			var page_id = Constants.FBSamples_page;
			_api.Pages.IsAdminAsync(page_id, IsAdminCompleted, null);
		}

		private void IsAdminCompleted(bool isAdmin, object state, FacebookException e)
		{
			Assert.IsTrue(isAdmin);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for getInfo
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getInfoTest()
		{
			var fields = _api.Pages.GetFields();
			var page_ids = new List<long> { Constants.FBSamples_page };
			int? uid = null;
			_api.Pages.GetInfoAsync(fields, page_ids, uid, GetInfoCompleted, null);
		}

		private void GetInfoCompleted(IList<page> actual, object state, FacebookException e)
		{
			Assert.IsNotNull(actual);
			Assert.IsTrue(actual.Count > 0);
			Assert.IsNotNull(actual[0].name);
			EnqueueTestComplete();
		}

		// TODO: implement these tests
		//[TestMethod]
		//public void getInfoTest1()
		//{
		//    var fields = _api.Pages.GetFields();
		//    List<long> page_ids = null;
		//    int? uid = null;
		//    var actual = _api.Pages.GetInfo(fields, page_ids, uid);
		//    Assert.IsNotNull(actual);
		//    Assert.IsTrue(actual.Count > 0);
		//    Assert.IsNotNull(actual[0].name);
		//}

		//[TestMethod]
		//public void getInfoTest2()
		//{
		//    var fields = _api.Pages.GetFields();
		//    List<long> page_ids = null;
		//    long? uid = Constants.FBSamples_UserId;
		//    var actual = _api.Pages.GetInfo(fields, page_ids, uid);
		//    Assert.IsNotNull(actual);
		//    Assert.IsTrue(actual.Count > 0);
		//    Assert.IsNotNull(actual[0].name);
		//}
	}
}

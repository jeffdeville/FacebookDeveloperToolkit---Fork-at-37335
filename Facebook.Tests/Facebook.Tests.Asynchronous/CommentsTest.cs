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
	public class CommentsTest : AsyncFacebookTest
	{
		/// <summary>
		///A test for get
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void getTest()
		{
			_api.Comments.GetAsync("1", GetCompleted, null);
		}

		private void GetCompleted(IList<comment> actual, object state, FacebookException e)
		{
			Assert.IsNotNull(actual);
			EnqueueTestComplete();
		}

		// TODO: implement these tests
		///// <summary>
		/////A test for get
		/////</summary>
		//[TestMethod()]
		//public void addTest()
		//{
		//    var actual2 = _api.Comments.Add("xid_test", "test");
		//    Assert.IsNotNull(actual2);

		//}
		///// <summary>
		/////A test for get
		/////</summary>
		//[TestMethod()]
		//public void addTest2()
		//{
		//    var actual2 = _apiWeb.Comments.Add("xid_test", "test", Constants.FBSamples_UserId);
		//    Assert.IsNotNull(actual2);

		//}
		///// <summary>
		/////A test for get
		/////</summary>
		//[TestMethod()]
		//public void addTest3()
		//{
		//    var actual2 = _apiWeb.Comments.Add("xid_test", "test", Constants.FBSamples_UserId, "comment title", "http://www.claritycon.com", false);
		//    Assert.IsNotNull(actual2);
		//}

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void addTest4()
		{
			_apiWeb.Comments.AddAsync("xid_test", "test", Constants.FBSamples_UserId, "comment title", "http://www.claritycon.com", true, Add4Completed, null);
		}

		private void Add4Completed(int commentId, object state, FacebookException e)
		{
			Assert.IsTrue(commentId > 0);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void removeTest()
		{
			_api.Comments.AddAsync("xid_test", "test", AddForRemoveCompleted, null);
		}

		private void AddForRemoveCompleted(int commentId, object state, FacebookException e)
		{
			_api.Comments.RemoveAsync("xid_test", commentId, RemoveCompleted, null);
		}

		private void RemoveCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}
	}
}

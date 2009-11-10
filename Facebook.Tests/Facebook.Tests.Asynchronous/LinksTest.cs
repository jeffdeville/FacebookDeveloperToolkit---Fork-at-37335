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
	public class LinksTest : AsyncFacebookTest
	{
		// TODO: implement this test
		///// <summary>
		/////A test for get
		/////</summary>
		//[TestMethod()]
		//public void getTest()
		//{
		//    var actual = _api.Links.Get();
		//    Assert.IsNotNull(actual);

		//}

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void getTest2()
		{
			_api.Links.GetAsync(_api.Session.UserId, Get2Completed, null);
		}

		private void Get2Completed(IList<link> actual, object state, FacebookException e)
		{
			Assert.IsNotNull(actual);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for post (using desktop app)
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void postTest()
		{
			_api.Links.PostAsync(_api.Session.UserId, new Uri("http://www.claritycon.com"), "My company website", PostCompleted, null);
		}

		private void PostCompleted(long linkId, object state, FacebookException e)
		{
			Assert.IsTrue(linkId > 0);
			EnqueueTestComplete();
		}

		// TODO: implement this test
		///// <summary>
		/////A test for post (using web app)
		/////</summary>
		//[TestMethod()]
		//public void postTest2()
		//{
		//    var actual = _apiWeb.Links.Post(_api.Session.UserId, new Uri("http://www.claritycon.com"), "My company website");
		//    Assert.IsNotNull(actual);

		//}
	}
}

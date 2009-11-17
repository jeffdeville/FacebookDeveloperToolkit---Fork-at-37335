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
	public class StatusTest : AsyncFacebookTest
	{
		/// <summary>
		///A test for get
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void getTest()
		{
			_api.Status.GetAsync(GetCompleted, null);
		}

		private void GetCompleted(IList<user_status> actual, object state, FacebookException e)
		{
			Assert.IsNotNull(actual);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for set
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void setTest()
		{
			_api.Status.SetAsync("testing status.setStatus", SetCompleted, null);
		}

		private void SetCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for set
		///</summary>
		[TestMethod()]
		public void setTest2()
		{
			_api.Status.SetAsync(Constants.FBSamples_UserId, "testing status.setStatus", Set2Completed, null);
		}

		private void Set2Completed(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}
	}
}

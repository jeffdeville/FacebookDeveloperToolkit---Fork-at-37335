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
	public class LiveMessageTest : AsyncFacebookTest
	{
		/// <summary>
		///A test for send
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void sendTest()
		{
			var uid = Constants.FBSamples_UserId;
			var eventName = "send";
			var message = "Testing from the test harness.";
			_api.LiveMessage.SendAsync(uid, eventName, message, SendCompleted, null);
		}

		private void SendCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}
	}
}

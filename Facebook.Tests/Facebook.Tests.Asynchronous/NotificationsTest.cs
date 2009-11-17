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
	public class NotificationsTest : AsyncFacebookTest
	{
		/// <summary>
		///A test for sendEmail
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void sendEmailTest()
		{
			var recipients = Constants.FBSamples_UserId.ToString();
			var subject = "test SendEmail Api";
			var text = "This is a test email for the notifications.SendEmail Unit Test";
			string fbml = null;
			_api.Notifications.SendEmailAsync(recipients, subject, text, fbml, SendEmailCompleted, null);
		}

		private void SendEmailCompleted(string actual, object state, FacebookException e)
		{
			Assert.IsNotNull(actual);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for send
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void sendTest()
		{
			var to_ids = Constants.FBSamples_UserId.ToString();
			var notification = "test";
			_api.Notifications.SendAsync(to_ids, notification, SendCompleted, null);
		}

		private void SendCompleted(string actual, object state, FacebookException e)
		{
			Assert.IsNotNull(actual);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getTest()
		{
			_api.Notifications.GetAsync(GetCompleted, null);
		}

		private void GetCompleted(notifications actual, object state, FacebookException e)
		{
			Assert.IsNotNull(actual);
			EnqueueTestComplete();
		}
	}
}

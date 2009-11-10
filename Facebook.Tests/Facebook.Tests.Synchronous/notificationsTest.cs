using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Rest;
using Facebook.Session;
using System;
using Facebook.Utility;

namespace Facebook.Tests.Synchronous
{
	/// <summary>
	///This is a test class for notificationsTest and is intended
	///to contain all notificationsTest Unit Tests
	///</summary>
	[TestClass]
	public class notificationsTest : Test
	{
		/// <summary>
		///A test for sendEmail
		///</summary>
		[TestMethod]
		public void sendEmailTest()
		{
            var recipients = Constants.FBSamples_UserId.ToString();
			var subject = "test SendEmail Api";
			var text = "This is a test email for the notifications.SendEmail Unit Test";
			string fbml = null;
			var actual = _api.Notifications.SendEmail(recipients, subject, text, fbml);
			Assert.IsNotNull(actual);
		}

		/// <summary>
		///A test for send
		///</summary>
		[TestMethod]
		public void sendTest()
		{
            var to_ids = Constants.FBSamples_UserId.ToString();
			var notification = "test";
            var actual = _api.Notifications.Send(to_ids, notification);
			Assert.IsNotNull(actual);
		}

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		public void getTest()
		{
            var actual = _api.Notifications.Get();
            var list = _api.Notifications.GetList(DateHelper.BaseUTCDateTime, true);
			Assert.IsNotNull(actual);
		}
	}
}
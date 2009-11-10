using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Tests.Synchronous
{
	/// <summary>
	///This is a test class for liveMessageTest and is intended
	///to contain all liveMessageTest Unit Tests
	///</summary>
	[TestClass]
	public class liveMessageTest : Test
	{
		/// <summary>
		///A test for send
		///</summary>
		[TestMethod]
		public void sendTest()
		{
			var uid = Constants.FBSamples_UserId;
			var eventName = "send";
			var message = "Testing from the test harness.";
			var expected = true;
			var actual = _apiWeb.LiveMessage.Send(uid, eventName, message);
			Assert.AreEqual(expected, actual);
		}
	}
}
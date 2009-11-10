using Facebook.Rest;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Facebook.Utility;
using Facebook.Schema;

namespace Facebook.Tests.Asynchronous
{
	[TestClass]
	public class ConnectTest : AsyncFacebookTest
	{
		[TestMethod]
		[Asynchronous]
		public void GetUnconnectedFriendsTest()
		{
			_api.Connect.GetUnconnectedFriendsAsync(GetUnconnectedFriendsTestCompleted, null);			
		}

		private void GetUnconnectedFriendsTestCompleted(int friendCount, object state, FacebookException e)
		{
            Assert.IsNotNull(friendCount); 
            EnqueueTestComplete();
		}

        [TestMethod]
        [Asynchronous]
        public void RegisterUsersTest()
        {
            /// (FB example) The address mary@example.com converts to 4228600737_c96da02bba97aedfd26136e980ae3761.
            List<ConnectAccountMap> accountMappings = new List<ConnectAccountMap>();

            accountMappings.Add(new ConnectAccountMap
            {
                EmailAddress = "mary@example.com",
                AccountId = "10002"
            });

            accountMappings.Add(new ConnectAccountMap
            {
                EmailAddress = Constants.FBSamples_Email_Facebook,
                AccountUrl = "http://www.facebook.com/10001.htm"
            });

            _api.Connect.RegisterUsersAsync(accountMappings, RegisterUsersTestCompleted, null);
        }

        private void RegisterUsersTestCompleted(IList<string> result, object state, FacebookException e)
        {
            Assert.IsTrue(result.Count == 2);
            EnqueueTestComplete();
        }

        [TestMethod]
        [Asynchronous]
        public void UnregisterUsersTest()
        {
            // Retrieve existing hash (from user)
            _api.Users.GetInfoAsync(Constants.FBSamples_UserId, GetUserInfoPreCompleted, null);
        }

        private void GetUserInfoPreCompleted(IList<user> users, object state, FacebookException e)
        {
            user currentUser = users[0];
            var hashes = new List<string>();
            hashes.Add(currentUser.email_hashes.email_hashes_elt[0]);
            _api.Connect.UnregisterUsersAsync(hashes, UnregisterUsersTestCompleted, null);
        }

        private void UnregisterUsersTestCompleted(IList<string> result, object state, FacebookException e)
        {
            Assert.IsTrue(result.Count == 1);
           _api.Users.GetInfoAsync(Constants.FBSamples_UserId, GetUserInfoPostCompleted, null);
        }

        private void GetUserInfoPostCompleted(IList<user> users, object state, FacebookException e)
        {
            user currentUser = users[0];
            Assert.IsNull(currentUser.email_hashes);
            EnqueueTestComplete();
        }
	}
}

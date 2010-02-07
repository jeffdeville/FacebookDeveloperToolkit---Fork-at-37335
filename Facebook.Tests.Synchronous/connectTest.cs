using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Tests.Synchronous
{
    /// <summary>
    ///This is a test class for Connect and is intended
    ///to contain all connectTest Unit Tests
    ///</summary>
    [TestClass]
	public class connectTest : Test
    {
        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion
        
        /// <summary>
        ///A test for GetUnconnectedFriends
        ///</summary>
        [TestMethod]
        public void GetUnconnectedFriendsTest()
        {
            var actual = _api.Connect.GetUnconnectedFriends();
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for RegisterUsers
        ///</summary>
        [TestMethod]
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
            
            var actual = _apiWeb.Connect.RegisterUsers(accountMappings);
            Assert.IsTrue(actual.Count == 2);

            var userGetInfoResult = _apiWeb.Users.GetInfo(Constants.FBSamples_UserId);
            Assert.IsNotNull(userGetInfoResult.email_hashes);
        }

        /// <summary>
        ///A test for UnregisterUsersTest
        ///</summary>
        [TestMethod]
        public void UnregisterUsersTest()
        {
            RegisterUsersTest();

            // Retrieve existing hash
            var existingUser = _apiWeb.Users.GetInfo(Constants.FBSamples_UserId);
            Assert.IsNotNull(existingUser.email_hashes);

            var hashes = new List<string>();
            hashes.Add(existingUser.email_hashes.email_hashes_elt[0]);
            var actual = _apiWeb.Connect.UnregisterUsers(hashes);
            Assert.IsTrue(actual.Count == 1);

            var resultUser = _apiWeb.Users.GetInfo(Constants.FBSamples_UserId);
            Assert.IsNull(resultUser.email_hashes);
        }
    }
}

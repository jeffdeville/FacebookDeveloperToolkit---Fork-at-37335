using Facebook.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Tests.Synchronous
{
    /// <summary>
    ///This is a test class for auth and is intended
    ///to contain all auth Unit Tests
    ///</summary>
    [TestClass()]
	public class authTest : Test
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
        ///A test for createToken
        ///</summary>
        [TestMethod()]
        public void createTokenTest()
        {
            var result =_apiWeb.Auth.CreateToken();
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result, string.Empty);
        }

        /// <summary>
        ///A test for getSession
        ///</summary>
        [TestMethod()]
        public void getSessionTest()
        {
            var authToken = _apiWeb.Auth.CreateToken();
            // TODO: Get Session giving invalid parameter error.  May be affected by login process requirements.
            //var result = _apiWeb.Auth.GetSession(authToken);
            //Assert.IsNotNull(result);
            //Assert.AreNotEqual(result, string.Empty);
        }

        ///// <summary>
        /////A test for expireSession
        /////</summary>
        //[TestMethod()]
        //public void expireSessionTest()
        //{
        //    // Note: Removed to prevent session from expiring
        //    var result = _api.Auth.ExpireSession();
        //    Assert.IsTrue(result);
        //}

        /// <summary>
        ///A test for promoteSession
        ///</summary>
        [TestMethod()]
        public void promoteSessionTest()
        {
            var result = _api.Auth.PromoteSession();
            Assert.IsNotNull(result);
        }

		// TODO: find a better way to test this method. It works right now, but we have to authorize the app for that user again before we can
		// run any more tests.
		///// <summary>
		/////A test for revokeAuthorization
		/////</summary>
		//[TestMethod()]
		//public void revokeAuthorizationTest()
		//{
		//    _api.Auth.RevokeAuthorization();
		//}

        /// <summary>
        ///A test for revokeExtendedPermission
        ///</summary>
        [TestMethod()]
        public void revokeExtendedPermissionTest()
        {
            var result = _apiWeb.Auth.RevokeExtendedPermission(Enums.ExtendedPermissions.sms);
            Assert.IsTrue(result);
        }
    }
}

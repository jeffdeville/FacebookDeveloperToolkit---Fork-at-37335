using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Schema;
using System;
using Facebook.Session;
using Facebook.Rest;

namespace Facebook.Tests.Synchronous
{
    
    
    /// <summary>
    ///This is a test class for applicationTest and is intended
    ///to contain all applicationTest Unit Tests
    ///</summary>
    [TestClass()]
	public class linksTest : Test
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
        ///A test for getPublicInfo
        ///</summary>
        [TestMethod()]
        public void getTest()
        {
            var actual = _apiWeb.Links.Get();
            Assert.IsNotNull(actual);
            
        }
        /// <summary>
        ///A test for get
        ///</summary>
        [TestMethod()]
        public void getTest2()
        {
            var actual = _apiWeb.Links.Get(_api.Session.UserId);
            Assert.IsNotNull(actual);

        }

        /// <summary>
        ///A test for post
        ///</summary>
        [TestMethod()]
        public void postTest()
        {
            var actual = _apiWeb.Links.Post(_api.Session.UserId, new Uri("http://www.claritycon.com"), "My company website");
            Assert.IsNotNull(actual);

        }
        /// <summary>
        ///A test for getPublicInfo
        ///</summary>
        [TestMethod()]
        public void postTest2()
        {
            var actual = _apiWeb.Links.Post(_api.Session.UserId, new Uri("http://www.claritycon.com"), "My company website");
            Assert.IsNotNull(actual);

        }

    }
}

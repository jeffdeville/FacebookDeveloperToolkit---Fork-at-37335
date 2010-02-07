using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Tests.Synchronous
{
    /// <summary>
    ///This is a test class for applicationTest and is intended
    ///to contain all applicationTest Unit Tests
    ///</summary>
    [TestClass()]
	public class commentsTest : Test
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
        ///A test for get
        ///</summary>
        [TestMethod()]
        public void getTest()
        {
            var actual = _apiWeb.Comments.Get("1");
            Assert.IsNotNull(actual);
            
        }
        /// <summary>
        ///A test for get
        ///</summary>
        [TestMethod()]
        public void addTest()
        {
            var actual2 = _api.Comments.Add("xid_test", "test");
            Assert.IsNotNull(actual2);

        }
        /// <summary>
        ///A test for get
        ///</summary>
        [TestMethod()]
        public void addTest2()
        {
            var actual2 = _apiWeb.Comments.Add("xid_test", "test", Constants.FBSamples_UserId);
            Assert.IsNotNull(actual2);

        }
        /// <summary>
        ///A test for get
        ///</summary>
        [TestMethod()]
        public void addTest3()
        {
            var actual2 = _apiWeb.Comments.Add("xid_test", "test", Constants.FBSamples_UserId, "comment title", "http://www.claritycon.com", false);
            Assert.IsNotNull(actual2);
        }
        /// <summary>
        ///A test for get
        ///</summary>
        [TestMethod()]
        public void addTest4()
        {
            var actual2 = _apiWeb.Comments.Add("xid_test", "test", Constants.FBSamples_UserId, "comment title", "http://www.claritycon.com", true);
            Assert.IsNotNull(actual2);
        }
        /// <summary>
        ///A test for get
        ///</summary>
        [TestMethod()]
        public void removeTest()
        {
            var actual = _api.Comments.Add("xid_test", "test");
            var actual2 = _api.Comments.Remove("xid_test", actual);
            Assert.IsTrue(actual2);
        }
    }
}

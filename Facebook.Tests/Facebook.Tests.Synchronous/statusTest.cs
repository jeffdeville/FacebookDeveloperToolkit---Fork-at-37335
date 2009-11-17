using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Schema;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Tests.Synchronous
{
    
    
    /// <summary>
    ///This is a test class for applicationTest and is intended
    ///to contain all applicationTest Unit Tests
    ///</summary>
    [TestClass()]
	public class statusTest : Test
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
            var actual = _apiWeb.Status.Get();
            Assert.IsNotNull(actual);
        }
        /// <summary>
        ///A test for set
        ///</summary>
        [TestMethod()]
        public void setTest()
        {
            var actual = _apiWeb.Status.Set("testing status.setStatus");
            Assert.IsTrue(actual);
        }
        /// <summary>
        ///A test for set
        ///</summary>
        [TestMethod()]
        public void setTest2()
        {
            var actual = _apiWeb.Status.Set(Constants.FBSamples_UserId, "testing status.setStatus");
            Assert.IsTrue(actual);
        }

    }
}

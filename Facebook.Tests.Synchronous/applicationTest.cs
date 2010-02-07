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
	public class applicationTest : Test
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
        public void getPublicInfoTest()
        {
            int application_id = 0;
            string application_api_key = Constants.FBSamples_ApplicationKey; 
            string application_canvas_name = string.Empty; 
            app_info actual;
            actual = _apiWeb.Application.GetPublicInfo(application_id, application_api_key, application_canvas_name);
            Assert.AreEqual(actual.api_key, Constants.FBSamples_ApplicationKey);
            Assert.IsNotNull(actual.display_name);
        }

        /// <summary>
        ///A test for getPublicInfo
        ///</summary>
        [TestMethod()]
        public void getPublicInfoTest1()
        {
            app_info actual;
            actual = _apiWeb.Application.GetPublicInfo();
            Assert.AreEqual(actual.api_key, Constants.FBSamples_WebApplicationKey);
            Assert.IsNotNull(actual.display_name);

        }
    }
}

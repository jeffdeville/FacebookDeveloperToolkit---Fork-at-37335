using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Schema;
using System.IO;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Tests.Synchronous
{
    
    
    /// <summary>
    ///This is a test class for applicationTest and is intended
    ///to contain all applicationTest Unit Tests
    ///</summary>
    [TestClass()]
	public class videoTest : Test
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
        public void getUploadLimitsTest()
        {
            var actual = _api.Video.GetUploadLimits();
            Assert.IsNotNull(actual);
            
        }
        /// <summary>
        ///A test for getPublicInfo
        ///</summary>
        [TestMethod()]
        public void uploadTest()
        {
			// TODO: find a better way to be able to access this file universally from any computer, running the tests from any location
            var data = new FileInfo(@"C:\Butterfly.wmv");
            video actual = null;
            if (!data.Exists)
            {
                Assert.Fail(string.Format("Unable to load video from {0}.", data.FullName));
                return;
            }

            actual = _api.Video.Upload("test", "video upload test", data);
            Assert.IsNotNull(actual);
        }

    }
}

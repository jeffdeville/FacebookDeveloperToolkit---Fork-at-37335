using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Tests.Synchronous
{
    /// <summary>
    ///This is a test class for batchTest and is intended
    ///to contain all batchTest Unit Tests
    ///</summary>
    [TestClass()]
	public class batchTest : Test
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
        ///A test for executeBatch
        ///</summary>
        [TestMethod()]
        public void executeBatchTest1()
        {
            IList<object> actual;
            _api.Batch.BeginBatch();
            _api.Friends.Get();
            _api.Events.Get();
            actual = _api.Batch.ExecuteBatch();
            Assert.IsTrue(actual.Count>0);
        }

        /// <summary>
        ///A test for executeBatch
        ///</summary>
        [TestMethod()]
        public void executeBatchTest()
        {
            IList<object> actual;
            _api.Batch.BeginBatch();
            _api.Friends.Get();
            _api.Events.Get();
            actual = _api.Batch.ExecuteBatch(true);
            Assert.IsTrue(actual.Count > 0);
        }
    }
}

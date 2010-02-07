using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Rest;

namespace Facebook.Tests.Synchronous
{
    
    
    /// <summary>
    ///This is a test class for applicationTest and is intended
    ///to contain all applicationTest Unit Tests
    ///</summary>
    [TestClass()]
	public class notesTest : Test
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
            var actual = _apiWeb.Notes.Get();
            Assert.IsNotNull(actual);
            
        }
        /// <summary>
        ///A test for get
        ///</summary>
        [TestMethod()]
        public void createTest()
        {
            var actual = _apiWeb.Notes.Create(Constants.FBSamples_UserId, "Test Note Title", "Test note content");
            Assert.IsNotNull(actual);

        }
        /// <summary>
        ///A test for get
        ///</summary>
        [TestMethod()]
        public void createTest2()
        {
            var actual = _apiWeb.Notes.Create(Constants.FBSamples_UserId, "Test Note Title", "Test note content");
            Assert.IsNotNull(actual);

        }
        /// <summary>
        ///A test for get
        ///</summary>
        [TestMethod()]
        public void editTest()
        {
            var actual = _apiWeb.Notes.Create(Constants.FBSamples_UserId, "Test Note Title", "Test note content");
            var actual2 = _apiWeb.Notes.Edit(actual, "Test Note Title Edited", "Test note content edited");

            Assert.IsNotNull(actual2);
        }
        /// <summary>
        ///A test for get
        ///</summary>
        [TestMethod()]
        public void deleteTest()
        {
            var noteID = _apiWeb.Notes.Create(Constants.FBSamples_UserId, "Test Note Title", "Test note content");
            var actual = _apiWeb.Notes.Delete(noteID);

            Assert.IsTrue(actual);
        }
        /// <summary>
        ///A test for get
        ///</summary>
        [TestMethod()]
        public void editTest2()
        {
            var actual = _apiWeb.Notes.Create(Constants.FBSamples_UserId, "Test Note Title", "Test note content");
            var actual2 = _apiWeb.Notes.Edit(actual, "Test Note Title Edited", "Test note content edited");

            Assert.IsNotNull(actual2);
        }
    }
}

using System.Collections.Generic;
using System.IO;
using Facebook.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Tests.Synchronous
{
	/// <summary>
	///This is a test class for photosTest and is intended
	///to contain all photosTest Unit Tests
	///</summary>
	[TestClass]
	public class photosTest : Test
	{
		/// <summary>
		///A test for upload
		///</summary>
		[TestMethod]
		public void uploadTest()
		{
		    photo actual = null;
			var aid = Constants.FBSamples_aid;
			var caption = "caption";

			// TODO: find a better way to be able to access this file universally from any computer, running the tests from any location
            var data = new FileInfo(@"C:\Clarity.jpg");

            if(!data.Exists)
            {
                Assert.Fail(string.Format("Unable to load photo from {0}.", data.FullName));
                return;
            }

		    actual = _api.Photos.Upload(aid, caption, data);
			Assert.IsNotNull(actual);
		}

        /// <summary>
        ///A test for upload
        ///</summary>
        [TestMethod]
        public void uploadTest2()
        {
            photo actual = null;
            var aid = Constants.FBSamples_aid;
            var caption = "caption";

			// TODO: find a better way to be able to access this file universally from any computer, running the tests from any location
            var data = new FileInfo(@"C:\Clarity.jpg");

            if (!data.Exists)
            {
                Assert.Fail(string.Format("Unable to load photo from {0}.", data.FullName));
                return;
            }
            
            actual = _api.Photos.Upload(aid, caption, data, Constants.FBSamples_UserId);
            Assert.IsNotNull(actual);
        }

		/// <summary>
		///A test for getTags
		///</summary>
		[TestMethod]
		public void getTagsTest()
		{
            var pids = new List<string> { Constants.FBSamples_pid };
            var actual = _api.Photos.GetTags(pids);
			Assert.IsNotNull(actual);
		}

		/// <summary>
		///A test for getAlbums
		///</summary>
		[TestMethod]
		public void getAlbumsTest3()
		{
            var uid = Constants.FBSamples_UserId;
			var aids = new List<string> {Constants.FBSamples_aid};
            var actual = _api.Photos.GetAlbums(uid, aids);
			Assert.IsTrue(actual.Count > 0);
		}

		/// <summary>
		///A test for getAlbums
		///</summary>
		[TestMethod]
		public void getAlbumsTest2()
		{
            var aids = new List<string> { Constants.FBSamples_aid };
            var actual = _api.Photos.GetAlbums(aids);
			Assert.IsTrue(actual.Count > 0);
		}

		/// <summary>
		///A test for getAlbums
		///</summary>
		[TestMethod]
		public void getAlbumsTest1()
		{
            var uid = Constants.FBSamples_UserId;
            var actual = _api.Photos.GetAlbums(uid);
			Assert.IsTrue(actual.Count > 0);
		}

		/// <summary>
		///A test for getAlbums
		///</summary>
		[TestMethod]
		public void getAlbumsTest()
		{
            var actual = _api.Photos.GetAlbums();
			Assert.IsTrue(actual.Count > 0);
		}

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		public void getTest()
		{
            var subj_id = string.Empty;
			var aid = Constants.FBSamples_aid.ToString();
			var pids = new List<string> {Constants.FBSamples_pid};
            var actual = _api.Photos.Get(subj_id, aid, pids);
			Assert.IsTrue(actual.Count > 0);
		}

		/// <summary>
		///A test for createAlbum
		///</summary>
		[TestMethod]
		public void createAlbumTest()
		{
            var name = "test";
			var location = "test";
			var description = "test";
            var actual = _api.Photos.CreateAlbum(name, location, description);
			Assert.AreEqual(actual.description, "test");
		}
        /// <summary>
        ///A test for createAlbum
        ///</summary>
        [TestMethod]
        public void createAlbumTest2()
        {
            var name = "test";
            var location = "test";
            var description = "test";
			var actual = _apiWeb.Photos.CreateAlbum(name, location, description, Constants.FBSamples_UserId);
            Assert.AreEqual(actual.description, "test");
        }

		/// <summary>
		///A test for addTag
		///</summary>
		[TestMethod]
		public void addTagTest()
		{
            var pid = Constants.FBSamples_pid;
			var tag_uid = Constants.FBSamples_UserId;
			var tag_text = "test";
			var x = 0F;
			var y = 0F;

            var result = _api.Photos.AddTag(pid, tag_uid, tag_text, x, y);
			Assert.IsTrue(result);
		}
        /// <summary>
        ///A test for addTag
        ///</summary>
        [TestMethod]
        public void addTagTest2()
        {
            var pid = Constants.FBSamples_pid;
            var tag_uid = Constants.FBSamples_UserId;
            var tag_text = "test";
            var x = 0F;
            var y = 0F;

            var result = _apiWeb.Photos.AddTag(pid, tag_uid, tag_text, x, y, Constants.FBSamples_UserId);
            Assert.IsTrue(result);
        }
    }
}
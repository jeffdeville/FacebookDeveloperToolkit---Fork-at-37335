using System;
using System.Net;
using Facebook.Rest;
using Facebook.Utility;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Schema;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Facebook.Tests.Asynchronous
{
	[TestClass]
	public class PhotosTest : AsyncFacebookTest
	{
		// TODO: implement uploadTest
		///// <summary>
		/////A test for upload
		/////</summary>
		//[TestMethod]
		//[Asynchronous]
		//public void uploadTest()
		//{
		//    photo actual = null;
		//    var aid = Constants.FBSamples_aid;
		//    var caption = "caption";
		//    var data = new FileInfo(@"..\..\..\Data\Clarity.jpg");

		//    if (!data.Exists)
		//    {
		//        Assert.Fail(string.Format("Unable to load photo from {0}.", data.FullName));
		//        return;
		//    }

		//    actual = _api.Photos.UploadAsync(aid, caption, data);
		//    Assert.IsNotNull(actual);
		//}

		// TODO: implement uploadTest2
		///// <summary>
		/////A test for upload
		/////</summary>
		//[TestMethod]
		//public void uploadTest2()
		//{
		//    photo actual = null;
		//    var aid = Constants.FBSamples_aid;
		//    var caption = "caption";
		//    var data = new FileInfo(@"..\..\..\Data\Clarity.jpg");

		//    if (!data.Exists)
		//    {
		//        Assert.Fail(string.Format("Unable to load photo from {0}.", data.FullName));
		//        return;
		//    }

		//    actual = _api.Photos.Upload(aid, caption, data, Constants.FBSamples_UserId);
		//    Assert.IsNotNull(actual);
		//}

		/// <summary>
		///A test for getTags
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getTagsTest()
		{
			var pids = new List<string> { Constants.FBSamples_pid };
			_api.Photos.GetTagsAsync(pids, GetTagsCompleted, null);
		}

		private void GetTagsCompleted(IList<photo_tag> actual, object state, FacebookException e)
		{
			Assert.IsNotNull(actual);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for getAlbums
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getAlbumsTest3()
		{
			var uid = Constants.FBSamples_UserId;
			var aids = new List<string> { Constants.FBSamples_aid };
			_api.Photos.GetAlbumsAsync(uid, aids, GetAlbums3Completed, null);
		}

		private void GetAlbums3Completed(IList<album> actual, object state, FacebookException e)
		{
			Assert.IsTrue(actual.Count > 0);
			EnqueueTestComplete();
		}

		// TODO: implement the other getAlbums tests from sync tests

		/// <summary>
		///A test for get
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getTest()
		{
			var subj_id = string.Empty;
			var aid = Constants.FBSamples_aid.ToString();
			var pids = new List<string> { Constants.FBSamples_pid };
			_api.Photos.GetAsync(subj_id, aid, pids, GetCompleted, null);
		}

		private void GetCompleted(IList<photo> actual, object state, FacebookException e)
		{
			Assert.IsTrue(actual.Count > 0);
			EnqueueTestComplete();
		}

		// TODO: implement this test for async
		/// <summary>
		///A test for createAlbum
		///</summary>
		//[TestMethod]
		//public void createAlbumTest()
		//{
		//    var name = "test";
		//    var location = "test";
		//    var description = "test";
		//    var actual = _api.Photos.CreateAlbum(name, location, description);
		//    Assert.AreEqual(actual.description, "test");
		//}

		/// <summary>
		///A test for createAlbum
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void createAlbumTest2()
		{
			var name = "test";
			var location = "test";
			var description = "test";
			_apiWeb.Photos.CreateAlbumAsync(name, location, description, Constants.FBSamples_UserId, CreateAlbum2Comleted, null);
		}

		private void CreateAlbum2Comleted(album actual, object state, FacebookException e)
		{
			Assert.AreEqual(actual.description, "test");
			EnqueueTestComplete();
		}

		// TODO: implement addTagTest() from sync test

		/// <summary>
		///A test for addTag
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void addTagTest2()
		{
			var pid = Constants.FBSamples_pid;
			var tag_uid = Constants.FBSamples_UserId;
			var tag_text = "test";
			var x = 0F;
			var y = 0F;

			_apiWeb.Photos.AddTagAsync(pid, tag_uid, tag_text, x, y, Constants.FBSamples_UserId, AddTag2Completed, null);
		}

		private void AddTag2Completed(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}

		/// <summary>
		/// A test for upload
		/// </summary>
		[TestMethod]
		[Asynchronous]
		public void uploadTest()
		{
			var aid = Constants.FBSamples_aid;
			var caption = "caption";

			var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Facebook.Tests.Asynchronous.Data.Clarity.jpg");
			var reader = new BinaryReader(fileStream);
			var fileData = reader.ReadBytes((int)fileStream.Length);

			_api.Photos.UploadAsync(aid, caption, fileData, "image/jpeg", UploadCompleted, null);
		}

		private void UploadCompleted(photo actual, object state, FacebookException e)
		{
			Assert.IsNotNull(actual);
			EnqueueTestComplete();
		}
	}
}

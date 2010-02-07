using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Facebook.Rest;
using Facebook.Utility;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Schema;
using System.Collections.Generic;

namespace Facebook.Tests.Asynchronous
{
	[TestClass]
	public class StreamTest : AsyncFacebookTest
	{
		// TODO: implement these two tests
		///// <summary>
		/////A test for getTest
		/////</summary>
		//[TestMethod()]
		//public void getTest()
		//{
		//    var actual = _api.Stream.Get(0, null, Constants.MinFacebookDate, DateTime.MaxValue, 0, null);
		//    Assert.IsNotNull(actual);

		//}
		///// <summary>
		/////A test for getTest
		/////</summary>
		//[TestMethod()]
		//public void getTest1()
		//{
		//    var actual = _api.Stream.Get(0, null, Constants.MinFacebookDate, DateTime.MaxValue, 2, null);
		//    Assert.IsNotNull(actual);
		//    Assert.IsTrue(actual.albums.album.Count <= 2);
		//    Assert.IsTrue(actual.posts.stream_post.Count <= 2);

		//}

		/// <summary>
		///A test for getTest
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void getTest2()
		{
			_api.Stream.GetAsync(0, new List<long>() { 665621453, 630947564 }, Constants.MinFacebookDate, DateTime.MaxValue, 0, null, Get2Completed, null);
			
		}

		private void Get2Completed(stream_data actual, object state, FacebookException e)
		{
			Assert.IsNotNull(actual);
			
			foreach (var post in actual.posts.stream_post)
			{
				Assert.IsTrue(post.source_id == 665621453 || post.source_id == 630947564);
			}

			EnqueueTestComplete();
		}

		// TODO: implement this test
		/// <summary>
		///A test for getTest
		///</summary>
		//[TestMethod()]
		//public void getTest3()
		//{
		//    var actual = _api.Stream.Get(0, null, Constants.MinFacebookDate, DateTime.MaxValue, 0, "fl_26568837591");
		//    Assert.IsNotNull(actual);

		//}

		/// <summary>
		///A test for getFilters
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void getFiltersTest()
		{
			_api.Stream.GetFiltersAsync(0, GetFiltersCompleted, null);
		}

		private void GetFiltersCompleted(IList<stream_filter> actual, object state, FacebookException e)
		{
			Assert.IsNotNull(actual);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for addComment
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void addCommentTest()
		{
			_api.Stream.GetAsync(0, new List<long>() { Constants.FBSamples_UserId }, Constants.MinFacebookDate, DateTime.MaxValue, 0, null, GetForAddCommentCompleted, null);
		}

		private void GetForAddCommentCompleted(stream_data stream, object state, FacebookException e)
		{
			Assert.IsNotNull(stream);
			_api.Stream.AddCommentAsync(stream.posts.stream_post[0].post_id, "Testing stream.AddComment", AddCommentCompleted, null);
		}

		private void AddCommentCompleted(string actual, object state, FacebookException e)
		{
			Assert.IsTrue(!string.IsNullOrEmpty(actual));
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for addLike
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void addLikeTest()
		{
			_api.Stream.GetAsync(0, new List<long>() { Constants.FBSamples_UserId}, Constants.MinFacebookDate, DateTime.MaxValue, 0, null, GetForLikeCompleted, null);
			
		}

		private void GetForLikeCompleted(stream_data stream, object state, FacebookException e)
		{
			Assert.IsNotNull(stream);
			_api.Stream.AddLikeAsync(stream.posts.stream_post[0].post_id, AddLikeCompleted, null);
		}

		private void AddLikeCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for getComments
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void getCommentsTest()
		{
			_api.Stream.GetAsync(0, new List<long>() { Constants.FBSamples_UserId }, Constants.MinFacebookDate, DateTime.MaxValue, 0, null, GetForGetCommentsCompleted, null);
		}

		private void GetForGetCommentsCompleted(stream_data stream, object state, FacebookException e)
		{
			Assert.IsNotNull(stream);
			_api.Stream.GetCommentsAsync(stream.posts.stream_post[0].post_id, GetCommentsCompleted, null);
		}

		private void GetCommentsCompleted(IList<comment> actual, object state, FacebookException e)
		{
			Assert.IsNotNull(actual);
			EnqueueTestComplete();
		}

		// TODO: implement these other publish tests
		///// <summary>
		/////A test for getComments
		/////</summary>
		//[TestMethod()]
		//public void publishTest()
		//{
		//    var actual = _api.Stream.Publish("testing stream.publish");
		//    Assert.IsNotNull(actual);
		//}
		///// <summary>
		/////A test for getComments
		/////</summary>
		//[TestMethod()]
		//public void publishTest2()
		//{
		//    attachment attachment = new attachment();

		//    attachment.caption = "www.icanhascheezburger.com";
		//    attachment.name = "I am bursting with joy";
		//    attachment.href = "http://icanhascheezburger.com/2009/04/22/funny-pictures-bursting-with-joy/";
		//    attachment.description = "a funny looking cat";
		//    attachment.properties = new attachment_property()
		//    {
		//        category = new attachment_category()
		//        {
		//            href = "http://www.icanhascheezburger.com/category/humor",
		//            text = "humor"
		//        },
		//        ratings = "5 stars"
		//    };
		//    attachment.media = new List<attachment_media>(){new attachment_media_image()
		//                            {
		//                                src = "http://icanhascheezburger.files.wordpress.com/2009/03/funny-pictures-your-cat-is-bursting-with-joy1.jpg",
		//                                href = "http://icanhascheezburger.com/2009/04/22/funny-pictures-bursting-with-joy/"
		//                            }};

		//    var actual = _api.Stream.Publish("testing stream.publish with image attachment", attachment, null, null, 0);
		//    Assert.IsNotNull(actual);
		//}
		///// <summary>
		/////A test for getComments
		/////</summary>
		//[TestMethod()]
		//public void publishTest3()
		//{
		//    attachment attachment = new attachment();

		//    attachment.caption = "www.icanhascheezburger.com";
		//    attachment.name = "I am bursting with joy";
		//    attachment.href = "http://icanhascheezburger.com/2009/04/22/funny-pictures-bursting-with-joy/";
		//    attachment.description = "a funny looking cat";
		//    attachment.properties = new attachment_property()
		//    {
		//        category = new attachment_category()
		//        {
		//            href = "http://www.icanhascheezburger.com/category/humor",
		//            text = "humor"
		//        },
		//        ratings = "5 stars"
		//    };
		//    attachment.media = new List<attachment_media>(){new attachment_media_image()
		//                            {
		//                                src = "http://icanhascheezburger.files.wordpress.com/2009/03/funny-pictures-your-cat-is-bursting-with-joy1.jpg",
		//                                href = "http://icanhascheezburger.com/2009/04/22/funny-pictures-bursting-with-joy/"
		//                            }};
		//    var link = new action_link();
		//    link.href = "http://mine.icanhascheezburger.com/default.aspx?tiid=1192742&recap=1#step2";
		//    link.text = "Recaption this";

		//    var links = new List<action_link>() { link };
		//    var actual = _api.Stream.Publish("testing stream.publish with image attachment", attachment, links, null, 0);
		//    Assert.IsNotNull(actual);
		//}

		[TestMethod()]
		[Asynchronous]
		public void publishTest4()
		{
			attachment attachment = new attachment();

			attachment.caption = "www.youtube.com";
			attachment.name = "ninja cat";
			attachment.href = "http://www.youtube.com/watch?v=muLIPWjks_M";
			attachment.description = "a sneaky cat";
            attachment.properties = new List<attachment_property>()
            {
                new attachment_property 
                {
                    name = "category",
                    value = new attachment_property_value
                    {
					href = "http://www.youtube.com/browse?s=mp&t=t&c=15",
					text = "pets"
                    }
                },
                new attachment_property
                {
                    name = "ratings",
                    value = new attachment_property_value { text = "5 stars" }
                }
            };
			attachment.media = new List<attachment_media>(){new attachment_media_video()
                                    {
                                        video_src = "http://www.youtube.com/v/fzzjgBAaWZw&hl=en&fs=1",
                                        preview_img="http://img.youtube.com/vi/muLIPWjks_M/default.jpg?h=100&w=200&sigh=__wsYqEz4uZUOvBIb8g-wljxpfc3Q=",
                                        video_link = "http://www.youtube.com/watch?v=muLIPWjks_M",
                                        video_title = "ninja cat"
                                    }};
			var link = new action_link();
			link.href = "http://www.youtube.com/my_videos_upload";
			link.text = "Upload a video";

			var links = new List<action_link>() { link };
			_api.Stream.PublishAsync("Watch this video!", attachment, links, null, 0, OnPublish4Completed, null);
		}

		private void OnPublish4Completed(string actual, object state, FacebookException e)
		{
			Assert.IsNotNull(actual);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for remove
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void removeTest()
		{
			_api.Stream.PublishAsync("testing stream.publish", OnPublishForRemoveCompleted, null);
		}

		private void OnPublishForRemoveCompleted(string postId, object state, FacebookException e)
		{
			Assert.IsNotNull(postId);
			_api.Stream.RemoveAsync(postId, OnRemoveCompleted, null);
			
		}

		private void OnRemoveCompleted(bool result, object state, FacebookException e)
		{			
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for removeComment
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void removeCommentTest()
		{
			_api.Stream.PublishAsync("testing stream.publish", OnPublishForRemoveCommentCompleted, null);
		}

		private void OnPublishForRemoveCommentCompleted(string postId, object state, FacebookException e)
		{
			Assert.IsNotNull(postId);
			_api.Stream.AddCommentAsync(postId, "test comment to be removed", OnAddForRemoveCommentCompleted, null);
		}

		private void OnAddForRemoveCommentCompleted(string commentId, object state, FacebookException e)
		{
			_api.Stream.RemoveCommentAsync(commentId, OnRemoveCommentCompleted, null);
		}

		private void OnRemoveCommentCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for removeComment
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void removeLikeTest()
		{
			_api.Stream.PublishAsync("testing stream.publish", OnPublishForRemoveLikeCompleted, null);
		}

		private void OnPublishForRemoveLikeCompleted(string postId, object state, FacebookException e)
		{
			Assert.IsNotNull(postId);
			_api.Stream.AddLikeAsync(postId, OnAddForRemoveLikeCompleted, postId);
		}

		private void OnAddForRemoveLikeCompleted(bool result, object state, FacebookException e)
		{
			var postId = (string)state;
			_api.Stream.RemoveLikeAsync(postId, OnRemoveLikeCompleted, null);
		}

		private void OnRemoveLikeCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}
	}
}

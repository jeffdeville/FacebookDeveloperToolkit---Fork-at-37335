using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Schema;
using System;
using System.Collections.Generic;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Tests.Synchronous
{
    
    
    /// <summary>
    ///This is a test class for applicationTest and is intended
    ///to contain all applicationTest Unit Tests
    ///</summary>
    [TestClass()]
	public class streamTest : Test
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
        ///A test for getTest
        ///</summary>
        [TestMethod()]
        public void getTest()
        {
            var actual = _api.Stream.Get(0,null,Constants.MinFacebookDate, DateTime.MaxValue, 0,null);
            Assert.IsNotNull(actual);
            
        }
        /// <summary>
        ///A test for getTest
        ///</summary>
        [TestMethod()]
        public void getTest1()
        {
            var actual = _api.Stream.Get(0, null, Constants.MinFacebookDate, DateTime.MaxValue, 2, null);
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.albums.album.Count <= 2);
            Assert.IsTrue(actual.posts.stream_post.Count <= 2);

        }
        /// <summary>
        ///A test for getTest
        ///</summary>
        [TestMethod()]
        public void getTest2()
        {
            var actual = _api.Stream.Get(0, new List<long>() { 665621453, 630947564 }, Constants.MinFacebookDate, DateTime.MaxValue, 0, null);
            Assert.IsNotNull(actual);
            foreach (var post in actual.posts.stream_post)
            {
                Assert.IsTrue(post.source_id == 665621453 || post.source_id == 630947564);
            }
        }
        /// <summary>
        ///A test for getTest
        ///</summary>
        [TestMethod()]
        public void getTest3()
        {
            var actual = _api.Stream.Get(0, null, Constants.MinFacebookDate, DateTime.MaxValue, 0, "fl_26568837591");
            Assert.IsNotNull(actual);

        }
        /// <summary>
        ///A test for getFilters
        ///</summary>
        [TestMethod()]
        public void getFiltersTest()
        {
            var actual = _api.Stream.GetFilters(0);
            Assert.IsNotNull(actual);

        }
        /// <summary>
        ///A test for addComment
        ///</summary>
        [TestMethod()]
        public void addCommentTest()
        {
            var actual = _api.Stream.Get(0, new List<long>() { Constants.FBSamples_UserId }, Constants.MinFacebookDate, DateTime.MaxValue, 0, null);
            Assert.IsNotNull(actual);
            var actual2 = _api.Stream.AddComment(actual.posts.stream_post[0].post_id, "Testing stream.AddComment");
            Assert.IsTrue(!string.IsNullOrEmpty(actual2));
        }
        /// <summary>
        ///A test for addLike
        ///</summary>
        [TestMethod()]
        public void addLikeTest()
        {
			var actual = _api.Stream.Get(0, new List<long>() { Constants.FBSamples_UserId }, Constants.MinFacebookDate, DateTime.MaxValue, 0, null);
            Assert.IsNotNull(actual);
            var actual2 = _api.Stream.AddLike(actual.posts.stream_post[0].post_id);
            Assert.IsTrue(actual2);
        }
        /// <summary>
        ///A test for getComments
        ///</summary>
        [TestMethod()]
        public void getCommentsTest()
        {
            var actual = _api.Stream.Get(0, new List<long>() { Constants.FBSamples_UserId }, Constants.MinFacebookDate, DateTime.MaxValue, 0, null);
            Assert.IsNotNull(actual);
            var actual2 = _api.Stream.GetComments(actual.posts.stream_post[0].post_id);
            Assert.IsNotNull(actual2);
        }
        /// <summary>
        ///A test for getComments
        ///</summary>
        [TestMethod()]
        public void publishTest()
        {
            var actual = _api.Stream.Publish("testing stream.publish");
            Assert.IsNotNull(actual);
        }
        /// <summary>
        ///A test for getComments
        ///</summary>
        [TestMethod()]
        public void publishTest2()
        {
            attachment attachment = new attachment();

            attachment.caption = "www.icanhascheezburger.com";
            attachment.name = "I am bursting with joy";
            attachment.href = "http://icanhascheezburger.com/2009/04/22/funny-pictures-bursting-with-joy/";
            attachment.description = "a funny looking cat";
            attachment.properties = new List<attachment_property>()
            {
                new attachment_property 
                {
                    name = "category",
                    value = new attachment_property_value
                    {
                        href = "http://www.icanhascheezburger.com/category/humor",
                        text = "humor"
                    }
                },
                new attachment_property
                {
                    name = "ratings",
                    value = new attachment_property_value { text = "5 stars" }
                }
            };
            attachment.media = new List<attachment_media>(){new attachment_media_image()
                                    {
                                        src = "http://icanhascheezburger.files.wordpress.com/2009/03/funny-pictures-your-cat-is-bursting-with-joy1.jpg",
                                        href = "http://icanhascheezburger.com/2009/04/22/funny-pictures-bursting-with-joy/"
                                    }};

            var actual = _api.Stream.Publish("testing stream.publish with image attachment", attachment, null, null, 0);
            Assert.IsNotNull(actual);
        }
        /// <summary>
        ///A test for getComments
        ///</summary>
        [TestMethod()]
        public void publishTest3()
        {
            attachment attachment = new attachment();

            attachment.caption = "www.icanhascheezburger.com";
            attachment.name = "I am bursting with joy";
            attachment.href = "http://icanhascheezburger.com/2009/04/22/funny-pictures-bursting-with-joy/";
            attachment.description = "a funny looking cat";
            attachment.properties = new List<attachment_property>()
            {
                new attachment_property 
                {
                    name = "category",
                    value = new attachment_property_value
                    {
                        href = "http://www.icanhascheezburger.com/category/humor",
                        text = "humor"
                    }
                },
                new attachment_property
                {
                    name = "ratings",
                    value = new attachment_property_value { text = "5 stars" }
                }
            };
            attachment.media = new List<attachment_media>(){new attachment_media_image()
                                    {
                                        src = "http://icanhascheezburger.files.wordpress.com/2009/03/funny-pictures-your-cat-is-bursting-with-joy1.jpg",
                                        href = "http://icanhascheezburger.com/2009/04/22/funny-pictures-bursting-with-joy/"
                                    }};
            var link = new action_link();
            link.href= "http://mine.icanhascheezburger.com/default.aspx?tiid=1192742&recap=1#step2";
            link.text = "Recaption this";

            var links = new List<action_link>(){link};
            var actual = _api.Stream.Publish("testing stream.publish with image attachment", attachment, links, null, 0);
            Assert.IsNotNull(actual);
        }
        [TestMethod()]
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
                        text = "humor"
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
            var actual = _api.Stream.Publish("Watch this video!", attachment, links, null, 0);
            Assert.IsNotNull(actual);
        }
        /// <summary>
        ///A test for remove
        ///</summary>
        [TestMethod()]
        public void removeTest()
        {
            var actual = _api.Stream.Publish("testing stream.publish");
            Assert.IsNotNull(actual);
            var actual2 = _api.Stream.Remove(actual);
            Assert.IsTrue(actual2);
        }

        /// <summary>
        ///A test for removeComment
        ///</summary>
        [TestMethod()]
        public void removeCommentTest()
        {
            var actual = _api.Stream.Publish("testing stream.publish");
            Assert.IsNotNull(actual);
            var actual2 = _api.Stream.AddComment(actual, "test comment to be removed");
            var actual3 = _api.Stream.RemoveComment(actual2);
            Assert.IsTrue(actual3);
        }
        /// <summary>
        ///A test for removeComment
        ///</summary>
        [TestMethod()]
        public void removeLikeTest()
        {
            var actual = _api.Stream.Publish("testing stream.publish");
            Assert.IsNotNull(actual);
            var actual2 = _api.Stream.AddLike(actual);
            var actual3 = _api.Stream.RemoveLike(actual);
            Assert.IsTrue(actual3);
        }



    }
}

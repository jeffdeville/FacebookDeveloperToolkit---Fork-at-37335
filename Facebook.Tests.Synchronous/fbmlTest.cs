using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Tests.Synchronous
{
	/// <summary>
	///This is a test class for fbmlTest and is intended
	///to contain all fbmlTest Unit Tests
	///</summary>
	[TestClass]
	public class fbmlTest : Test
	{
		/// <summary>
		///A test for setRefHandle
		///</summary>
		[TestMethod]
		public void uploadNativeStringsTest()
		{
			var native_strings = new Dictionary<string, string> { { "text", "(Testing uploadNativeStrings) Do you want to add a friend?"}, {"description", "text string in a popup dialog" } };
			var expected = false;
			var actual = _apiWeb.Fbml.UploadNativeStrings(native_strings);
		    Assert.IsNotNull(actual);

            /*  This should return false, as the native string has not been uploaded.  
             * Uploading text would be visible to all users therefore a false result is expected
             */

			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for setRefHandle
		///</summary>
		[TestMethod]
		public void setRefHandleTest()
		{
			var handle = "test";
			var fbml = "test";
			var expected = true;
            var actual = _apiWeb.Fbml.SetRefHandle(handle, fbml);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for refreshRefUrl
		///</summary>
		[TestMethod]
		public void refreshRefUrlTest()
		{
			var url = "http://facebook.claritycon.com/Tests/FBML.html";
			var expected = true;
            var actual = _api.Fbml.RefreshRefUrl(url);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for refreshImgSrc
		///</summary>
		[TestMethod]
		public void refreshImgSrcTest()
		{
			var url = "http://facebook.claritycon.com/Tests/Clarity.jpg";
			var expected = true;
            var actual = _api.Fbml.RefreshImgSrc(url);
			Assert.AreEqual(expected, actual);
		}
        /// <summary>
        ///A test for registerCustomTags
        ///</summary>
        [TestMethod]
        public void registerCustomTagsTest()
        {
            var tags = new List<CustomTag>()
            {
                new CustomTag()
                {
                    Name="video",
                    Type="leaf",
                    Description="Renders a fb:swf tag that shows a video from my-video-site.tv. The video  is 425 pixels wide and 344 pixels tall.",
                    Attributes=new List<CustomTagAttribute>(){new CustomTagAttribute(){Name="id",Description="the id of the video",DefaultValue="1234"}},
                    FBML="<div class=\"my_videos_element\"><fb:swf swfsrc=\"http://my-video-site.tv/videos/${id}\" width=\"425\" height=\"344\"/></div>",
                    HeaderFBML="<style>div.my_videos_element { border: black solid 1px; padding: 5px;}</style>"
                },
                new CustomTag()
                {
                    Name = "gallery",
                    Type = "container",
                    Description = "Renders a standard header and footer around one or more \"video\" tags. The header contains the gallery's title, which the user can specify",
                    Attributes = new List<CustomTagAttribute>() { new CustomTagAttribute() { Name = "title", Description = "the title of the gallery" } },
                    OpenTagFBML= "<div class=\"my_videos_element\"><div class=\"video_gallery_title\">${title}</div><div class=\"my_videos_gallery\">",
                    CloseTagFBML= "</div></div>",
                    HeaderFBML = "<style>div.my_videos_element { border: black solid 1px; padding: 5px;}</style>"
                }
            };
            var expected = 2;

            var actual = _apiWeb.Fbml.RegisterCustomTags(tags);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for getCustomTags
        ///</summary>
        [TestMethod]
        public void getCustomTagsTest()
        {
            var actual = _apiWeb.Fbml.GetCustomTags();
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for getCustomTags
        ///</summary>
        [TestMethod]
        public void deleteCustomTagsTest()
        {
            var actual = _apiWeb.Fbml.DeleteCustomTags(new List<string> { "video", "gallery" });
            Assert.IsTrue(actual);
        }

    }
}
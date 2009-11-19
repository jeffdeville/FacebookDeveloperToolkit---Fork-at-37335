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
	public class FbmlTest : AsyncFacebookTest
	{
		/// <summary>
		///A test for uploadNativeStrings
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void uploadNativeStringsTest()
		{
			var native_strings = new Dictionary<string, string> { { "text", "(Testing uploadNativeStrings) Do you want to add a friend?" }, { "description", "text string in a popup dialog" } };
			_apiWeb.Fbml.UploadNativeStringsAsync(native_strings, UploadNativeStringsCompleted, null);
		}

		private void UploadNativeStringsCompleted(bool result, object state, FacebookException e)
		{
			/*  This should return false, as the native string has not been uploaded.  
			 * Uploading text would be visible to all users therefore a false result is expected
			 */
			Assert.IsFalse(result);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for setRefHandle
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void setRefHandleTest()
		{
			var handle = "test";
			var fbml = "test";
			_apiWeb.Fbml.SetRefHandleAsync(handle, fbml, SetRefHandleCompleted, null);
		}

		private void SetRefHandleCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for refreshRefUrl
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void refreshRefUrlTest()
		{
			var url = "http://facebook.claritycon.com/Tests/FBML.html";
			_api.Fbml.RefreshRefUrlAsync(url, RefreshRefUrlCompleted, null);
		}

		private void RefreshRefUrlCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for refreshImgSrc
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void refreshImgSrcTest()
		{
			var url = "http://facebook.claritycon.com/Tests/Clarity.jpg";
			_api.Fbml.RefreshImgSrcAsync(url, RefreshImgSrcCompleted, null);
		}

		private void RefreshImgSrcCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for registerCustomTags
		///</summary>
		[TestMethod]
		[Asynchronous]
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

			_apiWeb.Fbml.RegisterCustomTagsAsync(tags, RegisterCustomTagsCompleted, null);
		}

		private void RegisterCustomTagsCompleted(int actual, object state, FacebookException e)
		{
			var expected = 2;
			Assert.AreEqual(expected, actual);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for getCustomTags
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void getCustomTagsTest()
		{
			_apiWeb.Fbml.GetCustomTagsAsync(GetCustomTagsCompleted, null);
		}

		private void GetCustomTagsCompleted(IList<custom_tag> actual, object state, FacebookException e)
		{
			Assert.IsNotNull(actual);
			EnqueueTestComplete();
		}

		/// <summary>
		///A test for getCustomTags
		///</summary>
		[TestMethod]
		[Asynchronous]
		public void deleteCustomTagsTest()
		{
			_apiWeb.Fbml.DeleteCustomTagsAsync(new List<string> { "video", "gallery" }, DeleteCustomTagsCompleted, null);
		}

		private void DeleteCustomTagsCompleted(bool result, object state, FacebookException e)
		{
			Assert.IsTrue(result);
			EnqueueTestComplete();
		}
	}
}

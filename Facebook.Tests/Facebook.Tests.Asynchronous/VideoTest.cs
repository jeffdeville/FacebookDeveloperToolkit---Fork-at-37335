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
	public class VideoTest : AsyncFacebookTest
	{
		/// <summary>
		/// A test for getUploadLimits
		/// </summary>
		[TestMethod]
		[Asynchronous]
		public void GetUploadLimitsTest()
		{
			_api.Video.GetUploadLimitsAsync(GetUploadLimitsCompleted, null);
		}

		private void GetUploadLimitsCompleted(video_limits actual, object state, FacebookException e)
		{
			Assert.IsNotNull(actual);
			EnqueueTestComplete();
		}

		// TODO: Get this test to work once Facebook fixes their client access security policy. Apparently there's some problem with it, and since there's no way to turn that off
		// in Silverlight, the test won't work. This issue is being tracked in Facebook's bug tracker here: http://bugs.developers.facebook.com/show_bug.cgi?id=6013
		///// <summary>
		///// A test for upload
		///// </summary>
		//[TestMethod]
		//[Asynchronous]
		//public void uploadTest()
		//{
		//    var fileStream =  Assembly.GetExecutingAssembly().GetManifestResourceStream("Facebook.Tests.Asynchronous.Data.Butterfly.wmv");
		//    var reader = new BinaryReader(fileStream);
		//    var fileData = reader.ReadBytes((int)fileStream.Length);
		//    fileData = new byte[2];

		//    _api.Video.UploadAsync("test", "video upload test", "Butterfly.wmv", fileData, "video/avi", UploadCompleted, null);
		//}

		//private void UploadCompleted(video actual, object state, FacebookException e)
		//{
		//    Assert.IsNotNull(actual);
		//    EnqueueTestComplete();
		//}
	}
}

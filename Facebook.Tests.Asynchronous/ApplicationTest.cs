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
	public class ApplicationTest : AsyncFacebookTest
	{
		/// <summary>
		///A test for getPublicInfo
		///</summary>
		[TestMethod()]
		[Asynchronous]
		public void getPublicInfoTest()
		{
			int application_id = 0;
			string application_api_key = Constants.FBSamples_ApplicationKey;
			string application_canvas_name = string.Empty;
			_api.Application.GetPublicInfoAsync(application_id, application_api_key, application_canvas_name, GetPublicInfoCompleted, null);
		}

		private void GetPublicInfoCompleted(app_info actual, object state, FacebookException e)
		{
			Assert.AreEqual(actual.api_key, Constants.FBSamples_ApplicationKey);
			Assert.IsNotNull(actual.display_name);
			EnqueueTestComplete();
		}

		// TODO: implement this test
		///// <summary>
		/////A test for getPublicInfo
		/////</summary>
		//[TestMethod()]
		//[Asynchronous]
		//public void getPublicInfoTest1()
		//{
		//    app_info actual;
		//    actual = _api.Application.GetPublicInfo();
		//    Assert.AreEqual(actual.api_key, Constants.FBSamples_ApplicationKey);
		//    Assert.IsNotNull(actual.display_name);

		//}
	}
}

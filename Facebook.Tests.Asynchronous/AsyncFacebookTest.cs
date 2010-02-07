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
using Microsoft.Silverlight.Testing;
using Facebook.Rest;
using Facebook.Session;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facebook.Tests.Asynchronous
{
	public class AsyncFacebookTest : SilverlightTest
	{
		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext { get; set; }

		protected Api _api;
		protected Api _apiWeb;

		[TestInitialize()]
		public void Initialize()
		{
			_api = new Api(new CachedSession(Constants.FBSamples_ApplicationKey, Constants.FBSamples_SessionSecret, Constants.FBSamples_SessionKey));
			_api.Session.UserId = Constants.FBSamples_UserId;
			_apiWeb = new Api(new TestSession(Constants.FBSamples_WebApplicationKey, Constants.FBSamples_WebSecret));
			_apiWeb.Session.UserId = Constants.FBSamples_UserId;
		}
	}
}

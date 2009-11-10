using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook.Session;
using Facebook.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facebook.Tests.Synchronous
{
	public class Test
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
			_api = new Api(new DesktopSession(Constants.FBSamples_ApplicationKey, Constants.FBSamples_SessionSecret, Constants.FBSamples_SessionKey));
			_api.Session.UserId = Constants.FBSamples_UserId;
			_apiWeb = new Api(new FBMLCanvasSession(Constants.FBSamples_WebApplicationKey, Constants.FBSamples_WebSecret, false));
			_apiWeb.Session.UserId = Constants.FBSamples_UserId;
            _apiWeb.Session.SessionKey = Constants.FBSamples_WebSessionKey;
		}
	}
}

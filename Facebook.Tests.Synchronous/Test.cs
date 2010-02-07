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

		protected FacebookApi _api;
		protected FacebookApi _facebookApiWeb;

		[TestInitialize()]
		public void Initialize()
		{
			_api = new FacebookApi(new DesktopSession(Constants.FBSamples_ApplicationKey, Constants.FBSamples_SessionSecret, Constants.FBSamples_SessionKey));
			_api.Session.UserId = Constants.FBSamples_UserId;
			_facebookApiWeb = new FacebookApi(new FBMLCanvasSession(Constants.FBSamples_WebApplicationKey, Constants.FBSamples_WebSecret, false));
			_facebookApiWeb.Session.UserId = Constants.FBSamples_UserId;
            _facebookApiWeb.Session.SessionKey = Constants.FBSamples_WebSessionKey;
		}
	}
}

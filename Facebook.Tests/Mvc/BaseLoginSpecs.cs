using System;
using System.Collections.Specialized;
using Facebook.Mvc;
using Facebook.Session;
using Moq;
using NUnit.Framework;
using yellowbook.testing.common;

namespace Facebook.Tests.Mvc.BaseLoginSpecs
{

	[TestFixture]
	public class when_creating_any_login : Context
	{
		protected BaseLogin sut;

		public override void setupContext()
		{
			sut = new BaseLogin(new NameValueCollection());
		}

		public override void act(){}

		[Test]
		public void appkey_is_loaded_from_facebook_configuration()
		{
			var apiKey = new FacebookConfiguration().ApiKey;
			Assert.That(sut._applicationKey, Is.EqualTo(apiKey));
		}
	}

	[TestFixture]
	public class when_creating_a_login_where_InCanvas_is_1 : Context
	{
		protected BaseLogin sut;
		private string result;
		
		public override void setupContext()
		{
			var requestParameters = new NameValueCollection() { { QueryParameters.InCanvas, "1" } };			
			sut = new BaseLogin(requestParameters);
		}

		public override void act()
		{
			result = sut.GetLoginUrl();
		}

		[Test]
		public void canvas_argument_is_added()
		{
			Assert.That(result, Is.StringContaining("&canvas"));
		}
	}

	[TestFixture]
	public class when_creating_a_login_where_InIframe_is_1 : Context
	{
		protected BaseLogin sut;
		private NameValueCollection _requestParameters;
		private string result;

		public override void setupContext()
		{
			_requestParameters = new NameValueCollection() { { QueryParameters.InIframe, "1" } };
			sut = new BaseLogin(_requestParameters);
		}

		public override void act()
		{
			result = sut.GetLoginUrl();
		}

		[Test]
		public void canvas_argument_is_added()
		{
			Assert.That(result, Is.StringContaining("&canvas"));
		}
	}

	[TestFixture]
	public class when_creating_a_login_where_user_isnt_in_iframe_or_canvas : Context
	{
		protected BaseLogin sut;
		private string result;

		public override void setupContext()
		{
			sut = new BaseLogin(new NameValueCollection());
		}

		public override void act()
		{
			result = sut.GetLoginUrl();
		}

		[Test]
		public void canvas_argument_is_added()
		{
			Assert.That(result.IndexOf("&canvas"), Is.EqualTo(-1));
		}
	}
}
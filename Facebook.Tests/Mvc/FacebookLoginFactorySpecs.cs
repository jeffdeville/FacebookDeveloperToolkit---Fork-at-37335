using System;
using System.Collections.Specialized;
using Facebook.Mvc;
using Facebook.Session;
using NUnit.Framework;
using yellowbook.testing.common;

namespace Facebook.Tests.Mvc.FacebookLoginFactorySpecs
{
	[TestFixture]
	public class when_asking_for_a_handler_for_an_FBML_page : Context
	{
		protected FacebookLoginFactory sut;
		private ILoginHandler result;

		public override void setupContext()
		{
			sut = new FacebookLoginFactory(new Uri("http://www.arizona.edu"), new NameValueCollection(), null);			
		}

		public override void act()
		{
			result = sut.GetLoginHandler(FacebookPageType.Fbml);
		}

		[Test]
		public void the_FBML_handler_is_returned()
		{
			Assert.That(result as FbmlLogin, Is.Not.Null);
		}
	}

	[TestFixture]
	public class when_asking_for_a_handler_for_an_IFrame_page : Context
	{
		protected FacebookLoginFactory sut;
		private ILoginHandler result;

		public override void setupContext()
		{
			sut = new FacebookLoginFactory(new Uri("http://www.arizona.edu"), new NameValueCollection(), null);
		}

		public override void act()
		{
			result = sut.GetLoginHandler(FacebookPageType.IFrame);
		}

		[Test]
		public void the_FBML_handler_is_returned()
		{
			Assert.That(result as IFrameLogin, Is.Not.Null);
		}
	}

	[TestFixture]
	public class when_asking_for_a_handler_for_a_Connect_page : Context
	{
		protected FacebookLoginFactory sut;
		private ILoginHandler result;

		public override void setupContext()
		{
			sut = new FacebookLoginFactory(new Uri("http://www.arizona.edu"), new NameValueCollection(), null);
		}

		public override void act()
		{
			result = sut.GetLoginHandler(FacebookPageType.Connect);
		}

		[Test]
		public void the_FBML_handler_is_returned()
		{
			Assert.That(result as ConnectLogin, Is.Not.Null);
		}
	}
}
using System.Web;
using Facebook.Session;
using NUnit.Framework;
using yellowbook.testing.common;

namespace Facebook.Tests.Session.FacebookSessionFactorySpec
{
	[TestFixture]
	public class when_getting_a_SessionProvider_for_a_Connect_page : Context
	{
		protected SessionProviderFactory sut;
		private ISessionProvider sessionProvider;

		public override void setupContext()
		{
			sut = new SessionProviderFactory(null, null, null, null);
		}

		public override void act()
		{
			sessionProvider = sut.GetSessionProvider(FacebookPageType.Connect);
		}

		[Test]
		public void verify_the_connect()
		{
			Assert.That(sessionProvider as ConnectSessionProvider, Is.Not.Null);
		}
	}

	[TestFixture]
	public class when_getting_a_SessionProvider_for_an_FBML_page : Context
	{
		protected SessionProviderFactory sut;
		private ISessionProvider sessionProvider;

		public override void setupContext()
		{
			sut = new SessionProviderFactory(null, null, null, null);
		}

		public override void act()
		{
			sessionProvider = sut.GetSessionProvider(FacebookPageType.Fbml);
		}

		[Test]
		public void verify_the_connect()
		{
			Assert.That(sessionProvider as FbmlSessionProvider, Is.Not.Null);
		}
	}

	[TestFixture]
	public class when_getting_a_SessionProvider_for_an_IFrame_page : Context
	{
		protected SessionProviderFactory sut;
		private ISessionProvider sessionProvider;

		public override void setupContext()
		{
			sut = new SessionProviderFactory(new HttpCookieCollection(), new HttpCookieCollection(), null, null);
		}

		public override void act()
		{
			sessionProvider = sut.GetSessionProvider(FacebookPageType.IFrame);
		}

		[Test]
		public void verify_the_connect()
		{
			Assert.That(sessionProvider as IFrameSessionProvider, Is.Not.Null);
		}
	}
}
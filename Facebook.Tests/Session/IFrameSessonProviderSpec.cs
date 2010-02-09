using System.Collections.Specialized;
using System.Web;
using Facebook.Rest;
using Facebook.Session;
using Moq;
using NUnit.Framework;
using yellowbook.testing.common;

namespace Facebook.Tests.Session
{
	public abstract class BaseIFrameSessionProviderSpec : Context
	{
		protected IFrameSessionProvider sut;
		protected HttpCookieCollection responseCookies = new HttpCookieCollection();
		protected HttpCookieCollection requestCookies = new HttpCookieCollection();
		protected NameValueCollection inputParams;
		protected Mock<IAuth> mockAuth = new Mock<IAuth>();

		public override void setupContext()
		{
			sut = new IFrameSessionProvider(requestCookies, responseCookies, inputParams, mockAuth.Object);
		}
	}
	[TestFixture]
	public class when_creating_an_iframe_session_when_the_sessionkey_is_in_the_requestparameters : BaseIFrameSessionProviderSpec
	{
		public override void setupContext()
		{
			base.setupContext();

		}

		public override void act()
		{
			sut.GetSession();
		}

		[Test]
		public void session_has_a_sessionkey()
		{
			Assert.Fail("Not implemented");
		}

		[Test]
		public void session_has_a_userid()
		{
			Assert.Fail("Not implemented");
		}

		[Test]
		public void session_has_an_expiration()
		{
			Assert.Fail("Not implemented");
		}

		[Test]
		public void session_is_cached()
		{
			Assert.Fail("Not implemented");
		}
	}
}
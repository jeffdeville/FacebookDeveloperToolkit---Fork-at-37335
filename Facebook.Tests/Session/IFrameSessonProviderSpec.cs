using System;
using System.Collections.Specialized;
using System.Web;
using Facebook.Rest;
using Facebook.Schema;
using Facebook.Session;
using Moq;
using NUnit.Framework;
using yellowbook.testing.common;

namespace Facebook.Tests.Session.IFrameSessionProviderSpec
{
	public abstract class BaseIFrameSessionProviderSpec : Context
	{
		protected IFrameSessionProvider sut;
		protected HttpCookieCollection responseCookies = new HttpCookieCollection();
		protected HttpCookieCollection requestCookies = new HttpCookieCollection();
		protected NameValueCollection inputParams = new NameValueCollection();
		protected Mock<IAuth> mockAuth = new Mock<IAuth>();
	    protected IFacebookSession session;

		public override void setupContext()
		{
			sut = new IFrameSessionProvider(requestCookies, responseCookies, inputParams, mockAuth.Object);
		}

        public override void act()
        {
            session = sut.GetSession();
        }
	}
	[TestFixture]
	public class when_creating_an_iframe_session_when_the_sessionkey_is_in_the_requestparameters : BaseIFrameSessionProviderSpec
	{
	    public override void setupContext()
		{
			base.setupContext();
		    inputParams[QueryParameters.SessionKey] = QueryParameters.SessionKey;
		    inputParams[QueryParameters.User] = "1234567890";
		    inputParams[QueryParameters.Expires] = "12345";
		}

		[Test]
		public void session_has_a_sessionkey()
		{
		    Assert.That(session.SessionKey, Is.EqualTo(QueryParameters.SessionKey));
		}

		[Test]
		public void session_has_a_userid()
		{
		    Assert.That(session.UserId, Is.EqualTo(1234567890));
		}

		[Test, Ignore("Facebook does weird stuff w/ dates that I don't yet understand.")]
		public void session_has_an_expiration()
		{
			Assert.Fail("Not implemented");
		}

		[Test]
		public void session_is_cached()
		{
		    Assert.That(responseCookies.Count, Is.EqualTo(3));
		    Assert.That(responseCookies[IFrameSessionProvider.SESSION_KEY_COOKIE].Value, Is.EqualTo(QueryParameters.SessionKey));
            Assert.That(responseCookies[IFrameSessionProvider.USER_ID_COOKIE].Value, Is.EqualTo("1234567890"));
            Assert.That(responseCookies[IFrameSessionProvider.EXPIRY_TIME_COOKIE].Value, Is.Not.Null);

		}
	}

    [TestFixture]
    public class when_creating_an_iframe_session_when_the_sessionkey_is_missing_but_the_cookie_values_exist : BaseIFrameSessionProviderSpec
    {        
        public override void setupContext()
        {
            base.setupContext();
            requestCookies.Add(new HttpCookie(IFrameSessionProvider.SESSION_KEY_COOKIE,QueryParameters.SessionKey));
            requestCookies.Add(new HttpCookie(IFrameSessionProvider.USER_ID_COOKIE, "1234567890"));
            requestCookies.Add(new HttpCookie(IFrameSessionProvider.EXPIRY_TIME_COOKIE, DateTime.UtcNow.ToString()));
        }        

        [Test]
        public void session_has_a_sessionkey()
        {
            Assert.That(session.SessionKey, Is.EqualTo(QueryParameters.SessionKey));
        }

        [Test]
        public void session_has_a_userid()
        {
            Assert.That(session.UserId, Is.EqualTo(1234567890));
        }

        [Test, Ignore("Facebook does weird stuff w/ dates that I don't yet understand.")]
        public void session_has_an_expiration()
        {
            Assert.Fail("Not implemented");
        }

        [Test]
        public void session_is_cached()
        {
            Assert.That(responseCookies.Count, Is.EqualTo(3));
            Assert.That(responseCookies[IFrameSessionProvider.SESSION_KEY_COOKIE].Value, Is.EqualTo(QueryParameters.SessionKey));
            Assert.That(responseCookies[IFrameSessionProvider.USER_ID_COOKIE].Value, Is.EqualTo("1234567890"));
            Assert.That(responseCookies[IFrameSessionProvider.EXPIRY_TIME_COOKIE].Value, Is.Not.Null);

        }
    }

    [TestFixture]
    public class when_creating_an_iframe_session_when_no_sessionkey_or_cachedsession_exists_but_an_authtoken_does : BaseIFrameSessionProviderSpec
    {        
        public override void setupContext()
        {
            base.setupContext();
            inputParams[QueryParameters.AuthToken] = QueryParameters.AuthToken;
            mockAuth.Setup(svc=> svc.GetSession(QueryParameters.AuthToken))
                .Returns(new session_info(){session_key = QueryParameters.SessionKey, uid = 1234567890, expires = 12345})
                .Verifiable();
        }

        [Test]
        public void authtoken_is_exchanged_for_session()
        {
            mockAuth.Verify();
        }

        [Test]
        public void session_is_cached()
        {
            Assert.That(responseCookies.Count, Is.EqualTo(3));
            Assert.That(responseCookies[IFrameSessionProvider.SESSION_KEY_COOKIE].Value, Is.EqualTo(QueryParameters.SessionKey));
            Assert.That(responseCookies[IFrameSessionProvider.USER_ID_COOKIE].Value, Is.EqualTo("1234567890"));
            Assert.That(responseCookies[IFrameSessionProvider.EXPIRY_TIME_COOKIE].Value, Is.Not.Null);

        }
    }

    [TestFixture]
    public class when_no_session_info_is_available_at_all : BaseIFrameSessionProviderSpec
    {
        [Test]
        public void return_null_session()
        {
            Assert.That(session, Is.Null);
        }
        
    }
}
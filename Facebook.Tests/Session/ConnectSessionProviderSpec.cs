using System.Collections.Specialized;
using System.Web;
using Facebook.Session;
using NUnit.Framework;
using yellowbook.testing.common;

namespace Facebook.Tests.Session.FacebookConfigurationSpec
{
    [TestFixture]
    public class when_no_cookies_exist : Context
    {
        protected ConnectSessionProvider sut;
        
        public override void setupContext()
        {
            sut = new ConnectSessionProvider(new HttpCookieCollection());            
        }

        public override void act(){}

        [Test]
        public void IsConnected_is_false()
        {
            Assert.That(sut.IsConnected(), Is.False);
        }

        [Test]
        public void GetSession_returns_null()
        {
            Assert.That(sut.GetSession(),Is.Null);
        }
    }

    [TestFixture]
    public class when_getting_a_sessioncookie : Context
    {
        protected ConnectSessionProvider sut;
        protected HttpCookieCollection cookies;
        private string result;

        public override void setupContext()
        {
            cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("ApiKey_ping", "pong"));
            
            sut = new ConnectSessionProvider(cookies);
        }

        public override void act()
        {
            result = sut.GetCookie("ping");
        }

        [Test]
        public void apikey_is_prepended_to_the_name_that_is_queried()
        {
            Assert.That(result, Is.EqualTo("pong"));
        }
    }

    [TestFixture]
    public class when_reconstituting_a_facebook_session : Context
    {
        protected ConnectSessionProvider sut;
        private IFacebookSession session;
        private HttpCookieCollection cookies = new HttpCookieCollection();

        public override void setupContext()
        {
            sut = new ConnectSessionProvider(cookies);

            cookies.Add(new HttpCookie(sut.GetCookieName(ConnectSessionProvider.EXPIRES), ConnectSessionProvider.EXPIRES));
            cookies.Add(new HttpCookie(sut.GetCookieName(ConnectSessionProvider.SECRET_SESSION_KEY), "970de1596eef4d596e7c3987ebd75abb"));
            cookies.Add(new HttpCookie(sut.GetCookieName(ConnectSessionProvider.SESSION_KEY), ConnectSessionProvider.SESSION_KEY));
            cookies.Add(new HttpCookie(sut.GetCookieName(ConnectSessionProvider.SS), ConnectSessionProvider.SS));
            cookies.Add(new HttpCookie(sut.GetCookieName(ConnectSessionProvider.USER), "1234567890"));
            
        }

        public override void act()
        {
            session = sut.GetSession();
        }

        [Test]
        public void it_is_not_null()
        {
            Assert.That(session, Is.Not.Null);
        }
        [Test]
        public void the_session_key_is_included()
        {
            Assert.That(session.SessionKey, Is.EqualTo(ConnectSessionProvider.SESSION_KEY));
        }

        [Test]
        public void the_userid_is_included()
        {
            Assert.That(session.UserId, Is.EqualTo(1234567890));
        }
    }
}
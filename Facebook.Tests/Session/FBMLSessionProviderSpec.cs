using System;
using System.Collections.Specialized;
using Facebook.Rest;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;
using NUnit.Framework;
using yellowbook.testing.common;

namespace Facebook.Tests.Session.FBMLSessionProviderSpec
{
    [TestFixture]
    public class when_creating_an_fbml_session_on_a_profiletab_when_the_request_contains_the_sessionkey : Context
    {
        protected FBMLSessionProvider sut;
        private NameValueCollection inputParams = new NameValueCollection();
        private DateTime expiration;
        private IFacebookSession session;

        public override void setupContext()
        {
            inputParams[QueryParameters.ProfileSessionKey] = QueryParameters.ProfileSessionKey;
            inputParams[QueryParameters.ProfileUser] = "1234567890";
			inputParams[QueryParameters.InProfileTab] = "1";
            expiration = DateTime.UtcNow.AddMinutes(1);
        	inputParams[QueryParameters.Expires] = DateHelper.ConvertDateToFacebookDate(expiration).ToString();
            sut = new FBMLSessionProvider(inputParams, null);
        }

        public override void act()
        {
            session = sut.GetSession();
        }

        [Test]
        public void session_has_a_sessionkey()
        {
            Assert.That(session.SessionKey, Is.EqualTo(QueryParameters.ProfileSessionKey));
        }

        [Test]
        public void session_has_a_userid()
        {
			Assert.That(session.UserId, Is.EqualTo(1234567890));
        }

        [Test, Ignore("Because I don't follow what facebook is doing w/ their dates.")]
        public void session_has_an_expiration()
        {
			Assert.That(session.ExpiryTime.Date, Is.EqualTo(expiration.Date));
			Assert.That(session.ExpiryTime.Hour, Is.EqualTo(expiration.Hour));
			Assert.That(session.ExpiryTime.Minute, Is.EqualTo(expiration.Minute));
			Assert.That(session.ExpiryTime.Second, Is.EqualTo(expiration.Second));
        }

        [Test]
        public void InProfileTab_is_true()
        {
            Assert.That(sut.InProfileTab, Is.True);
        }
    }

    [TestFixture]
    public class when_creating_an_fbml_session_outside_of_a_profiletab_when_the_request_contains_the_sessionkey : Context
    {
		protected FBMLSessionProvider sut;
		private NameValueCollection inputParams = new NameValueCollection();
		private DateTime expiration;
		private IFacebookSession session;

		public override void setupContext()
		{
			inputParams[QueryParameters.SessionKey] = QueryParameters.SessionKey;
			inputParams[QueryParameters.User] = "1234567890";
			inputParams[QueryParameters.InProfileTab] = "0";
			expiration = DateTime.UtcNow.AddMinutes(1);
			inputParams[QueryParameters.Expires] = DateHelper.ConvertDateToFacebookDate(expiration).ToString();
			sut = new FBMLSessionProvider(inputParams, null);
		}

		public override void act()
		{
			session = sut.GetSession();
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

		[Test, Ignore("Because I don't follow what facebook is doing w/ their dates.")]
		public void session_has_an_expiration()
		{
			Assert.That(session.ExpiryTime.Date, Is.EqualTo(expiration.Date));
			Assert.That(session.ExpiryTime.Hour, Is.EqualTo(expiration.Hour));
			Assert.That(session.ExpiryTime.Minute, Is.EqualTo(expiration.Minute));
			Assert.That(session.ExpiryTime.Second, Is.EqualTo(expiration.Second));
		}

		[Test]
		public void InProfileTab_is_false()
		{
			Assert.That(sut.InProfileTab, Is.False);
		}
    }

    [TestFixture]
    public class when_an_authtoken_is_available_but_no_sessionkey : Context
    {
        protected FBMLSessionProvider sut;
        private NameValueCollection inputParams = new NameValueCollection();        
        private IFacebookSession session;

        public override void setupContext()
        {
        	container.Register(inputParams);
			container.GetMock<IAuth>().Setup(auth => auth.GetSession(QueryParameters.AuthToken))
				.Returns(new session_info() { session_key = QueryParameters.SessionKey, uid = 1234567890, expires = 1234 })
				.Verifiable();

			sut = container.Create<FBMLSessionProvider>();
        	inputParams[QueryParameters.AuthToken] = QueryParameters.AuthToken;
        }

        public override void act()
        {
            session = sut.GetSession();
        }

        [Test]
        public void token_is_exchanged_for_a_session()
        {
            container.GetMock<IAuth>().Verify();
        }

    	[Test]
    	public void sessionkey_is_set()
    	{
    		Assert.That(session.SessionKey, Is.EqualTo(QueryParameters.SessionKey));
    	}

    	[Test]
    	public void userid_is_set()
    	{
    		Assert.That(session.UserId, Is.EqualTo(1234567890));
    	}
    }

    [TestFixture]
    public class when_no_login_info_is_available : Context
    {
        protected FBMLSessionProvider sut;
        private IFacebookSession session;

        public override void setupContext()
        {
			sut = new FBMLSessionProvider(new NameValueCollection(), null);
        }

        public override void act()
        {
            session = sut.GetSession();
        }

        [Test]
        public void null_session_is_returned()
        {
        	Assert.That(session, Is.Null);
        }
    }
}
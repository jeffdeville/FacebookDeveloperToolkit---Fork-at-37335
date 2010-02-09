using System;
using System.Collections.Specialized;
using Facebook.Session;
using Facebook.Utility;
using NUnit.Framework;
using yellowbook.testing.common;

namespace Facebook.Tests.Session.FacebookConfigurationSpec
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
            inputParams[QueryParameters.ProfileUser] = QueryParameters.ProfileUser;
            expiration = DateTime.UtcNow.AddMinutes(1);
            inputParams[QueryParameters.Expires] = DateHelper.ConvertDateToFacebookDate(expiration).ToString();
            sut = new FBMLSessionProvider(inputParams);
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
            Assert.That(session.UserId, Is.EqualTo(QueryParameters.ProfileUser));
        }

        [Test]
        public void session_has_an_expiration()
        {
            Assert.That(session.ExpiryTime, Is.EqualTo(expiration));
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
            sut = new FBMLSessionProvider(inputParams);
        }

        public override void act()
        {
            sut.GetSession();
        }

        [Test]
        public void that_sessionkey_is_used_in_the_session()
        {
            Assert.Fail("Not implemented");
        }

        [Test]
        public void userid_is_added_to_the_session()
        {
            Assert.Fail("Not implemented");
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
        private DateTime expiration;
        private IFacebookSession session;


        public override void setupContext()
        {
            sut = new FBMLSessionProvider(inputParams);
        }

        public override void act()
        {
            sut.GetSession();
        }

        [Test]
        public void token_is_exchanged_for_a_session()
        {
            Assert.Fail("Not implemented");
        }
    }

    [TestFixture]
    public class when_no_login_info_is_available : Context
    {
        protected FBMLSessionProvider sut;
        private NameValueCollection inputParams = new NameValueCollection();
        private DateTime expiration;
        private IFacebookSession session;

        public override void setupContext()
        {
            sut = new FBMLSessionProvider(inputParams);
        }

        public override void act()
        {
            sut.GetSession();
        }

        [Test]
        public void null_session_is_returned()
        {
            Assert.Fail("Not implemented");
        }
    }
}
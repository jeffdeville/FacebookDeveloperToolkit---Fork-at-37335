using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Facebook.Session;
using NUnit.Framework;
using yellowbook.testing.common;


namespace Facebook.Tests.FacebookConfigurationSpec
{
    [TestFixture]
    public class when_creating_a_FacebookConfiguration : Context
    {
        protected FacebookConfiguration sut;

        public override void setupContext()
        {
            sut = new FacebookConfiguration();
        }

        public override void act(){}

        [Test]
        public void application_key_is_loaded_from_config()
        {
            Assert.That(sut.ApiKey, Is.EqualTo("ApiKey"));
        }

        [Test]
        public void application_secret_is_loaded_from_config()
        {
            Assert.That(sut.Secret, Is.EqualTo("Secret"));
        }
    }
}

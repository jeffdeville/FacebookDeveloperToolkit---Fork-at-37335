using System.Collections.Specialized;
using System.Web.Mvc;
using Facebook.Mvc;
using Facebook.Session;
using NUnit.Framework;
using yellowbook.testing.common;

namespace Facebook.Tests.Mvc.FbmlLoginSpecs
{
	[TestFixture]
	public class when_getting_an_FBML_redirect : Context
	{
		protected FbmlLogin sut;
		private ContentResult result;

		public override void setupContext()
		{
			sut = new FbmlLogin(new NameValueCollection(), null);
		}

		public override void act()
		{
			result = (ContentResult) sut.GetRedirect();
		}

		[Test]
		public void ensure_return_url_is_an_fbml_tag()
		{
			Assert.That(result.Content.StartsWith("<fb:redirect"), Is.True);
		}
	}
}
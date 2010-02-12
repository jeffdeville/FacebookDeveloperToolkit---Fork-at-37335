using System;
using System.Web.Mvc;
using Facebook.Mvc;
using Facebook.Session;
using NUnit.Framework;
using yellowbook.testing.common;

namespace Facebook.Tests.Mvc.ConnectLoginSpecs
{
	[TestFixture]
	public class when_getting_a_connect_redirect : Context
	{
		protected ConnectLogin sut;
		private RedirectResult result;
		private Uri currentUrl;
		public override void setupContext()
		{
			currentUrl = new Uri("http://www.arizona.edu");
			sut = new ConnectLogin(currentUrl);
		}

		public override void act()
		{
			result = (RedirectResult) sut.GetRedirect();
		}

		[Test]
		public void ensure_return_url_is_UriEscaped()
		{
			Assert.That(result.Url, Is.StringContaining(Uri.EscapeUriString(currentUrl.ToString())));
		}
	}
}
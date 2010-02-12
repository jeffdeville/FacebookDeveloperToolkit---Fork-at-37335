using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Facebook.Mvc;
using Facebook.Rest;
using Facebook.Session;
using Moq;
using MvcFakes;
using NUnit.Framework;
using yellowbook.testing.common;

namespace Facebook.Tests.Mvc.FacebookAuthorizationAttributeSpecs 
{

	[TestFixture]
	public class when_results_are_executed_for_non_fbml_pages : Context
	{
		protected FacebookAttribute sut;
		private ResultExecutedContext filterContext;

		public override void setupContext()
		{
			var context = new FakeHttpContext();
			filterContext = new ResultExecutedContext(){HttpContext = context};
			sut = new FacebookAttribute() { PageType = FacebookPageType.IFrame };
		}

		public override void act()
		{
			sut.OnResultExecuted(filterContext);
		}

		[Test]
		public void security_header_is_appended()
		{
			Assert.That(filterContext.HttpContext.Response.Headers["P3P"], Is.EqualTo("CP=\"CAO PSA OUR\""));
		}
	}

	[TestFixture]
	public class when_results_are_executed_for_fbml_pages : Context
	{
		protected FacebookAttribute sut;
		private ResultExecutedContext filterContext;

		public override void setupContext()
		{
			var context = new FakeHttpContext();
			filterContext = new ResultExecutedContext() { HttpContext = context };
			sut = new FacebookAttribute(){PageType = FacebookPageType.Fbml};
		}

		public override void act()
		{
			sut.OnResultExecuted(filterContext);
		}

		[Test]
		public void no_headers_are_appended()
		{
			Assert.That(filterContext.HttpContext.Response.Headers.Count, Is.EqualTo(0));
		}
	}

	[TestFixture]
	public class when_authorizing_a_page_with_no_session : Context
	{
		protected FacebookAttribute sut;
		private AuthorizationContext filterContext;

		public override void setupContext()
		{
			var mockContext = new FakeHttpContext();
			filterContext = new AuthorizationContext() { HttpContext = mockContext };
			container.GetMock<IFacebookLoginFactory>()
				.Setup(svc => svc.GetLoginHandler(It.IsAny<FacebookPageType>()))
				.Returns(container.GetMock<ILoginHandler>().Object);
			container.GetMock<ILoginHandler>()
				.Setup(svc=> svc.GetRedirect())
				.Verifiable();
			container.GetMock<ISessionProviderFactory>()
				.Setup(factory => factory.GetSessionProvider(It.IsAny<FacebookPageType>()))
				.Returns(container.GetMock<ISessionProvider>().Object);
			container.GetMock<ISessionProvider>()
				.Setup(svc => svc.GetSession());
			sut = container.Create<FacebookAttribute>();
		}

		public override void act()
		{
			sut.OnAuthorization(filterContext);
		}

		[Test]
		public void get_the_login_info_from_the_loginhandler()
		{
			container.GetMock<ILoginHandler>().Verify();
		}
	}

	[TestFixture]
	public class when_authorizing_a_page_with_a_session : Context
	{
		protected FacebookAttribute sut;
		private AuthorizationContext filterContext;

		public override void setupContext()
		{
			var mockContext = new FakeHttpContext();
			filterContext = new AuthorizationContext() { HttpContext = mockContext };

			container.GetMock<ISessionProviderFactory>()
				.Setup(factory => factory.GetSessionProvider(It.IsAny<FacebookPageType>()))
				.Returns(container.GetMock<ISessionProvider>().Object);
			container.GetMock<ISessionProvider>()
				.Setup(svc => svc.GetSession())
				.Returns(new FacebookSession(){UserId = 1234567890});
			container.GetMock<IFacebookApi>()
				.Setup(svc=> svc.Initialize(It.IsAny<IFacebookSession>()))
				.Verifiable();
			sut = container.Create<FacebookAttribute>();
		}

		public override void act()
		{
			sut.OnAuthorization(filterContext);
		}

		[Test]
		public void the_api_is_initialized()
		{
			container.GetMock<IFacebookApi>().Verify();
		}

		[Test]
		public void the_user_principal_is_updated()
		{
			Assert.That(filterContext.HttpContext.User.Identity.Name, Is.EqualTo("1234567890"));
		}
	}
}
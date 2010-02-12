using Facebook.Session;

namespace Facebook.Mvc
{
	public interface IFacebookLoginFactory
	{
		ILoginHandler GetLoginHandler(FacebookPageType pageType);
	}
}
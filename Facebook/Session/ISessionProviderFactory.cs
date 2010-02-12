namespace Facebook.Session
{
	public interface ISessionProviderFactory
	{
		ISessionProvider GetSessionProvider(FacebookPageType pageType);
	}
}
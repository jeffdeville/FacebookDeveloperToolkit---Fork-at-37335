namespace Facebook.Session
{
    public interface ISessionProvider
    {
        IFacebookSession GetSession();
    }
}
namespace Facebook.Api
{
    public interface IRedirectStrategy
    {
        void RedirectToLogin();
        void GetRedirect();         
    }
}
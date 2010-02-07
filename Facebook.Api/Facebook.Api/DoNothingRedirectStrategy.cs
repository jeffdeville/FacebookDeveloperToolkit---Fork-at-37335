using System;

namespace Facebook.Api
{
    public class DoNothingRedirectStrategy : IRedirectStrategy
    {
        #region Implementation of IRedirectStrategy

        public void RedirectToLogin()
        {
            throw new NotImplementedException();
        }

        public void GetRedirect()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
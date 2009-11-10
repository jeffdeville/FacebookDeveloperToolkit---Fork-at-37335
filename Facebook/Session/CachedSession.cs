#if SILVERLIGHT

using System;
using System.Net;
using System.ComponentModel;
using Facebook.Rest;
using Facebook.Utility;

namespace Facebook.Session
{
    /// <summary>
    /// Represents object holding the cached session information
    /// </summary>
    public class CachedSession : BrowserSession
    {
        /// <summary>
        /// Represents a cached session, used by OOB silverlight apps
        /// </summary>
        /// <param name="appKey">Application key</param>
        /// <param name="appSecret">Application secret key</param>
        /// <param name="sessionKey">Cached session key</param>
        /// <param name="sessionSecret">Cached session secret key</param>
        public CachedSession(string appKey, string sessionKey, string sessionSecret)
        {
            this.ApplicationKey = appKey;
            this.SessionKey = sessionKey;
            this.SessionSecret = sessionSecret;
        }

        /// <summary>
        /// Logs in user
        /// </summary>
        public override void Login()
        {
            //check if session is valid
            Users users = new Users(this);
            users.GetLoggedInUserAsync(OnGetLoggedInUser, null);
        }

        /// <summary>
        /// Logs out user
        /// </summary>
        public override void Logout()
        {
            this.OnLoggedOut(null);
        }


        void OnGetLoggedInUser(long userId, Object state, FacebookException e)
        {
            if (e == null)
            {
                this.UserId = userId;
            }

            this.OnLoggedIn(e);
        }

    }
}

#endif
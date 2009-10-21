using System;
using System.ComponentModel;
using Facebook;
using Facebook.Session;

namespace Facebook.Controls
{
    /// <summary>
    /// Provides data for LoginCompleted Event
    /// </summary>
    public class LoginCompletedEventArgs : AsyncCompletedEventArgs
    {
        /// <summary>
        /// Session obtained from server.
        /// </summary>
        public FacebookSession Session
        {
            get;
            private set;
        }

        internal LoginCompletedEventArgs(FacebookSession session)
            : base(null, false, null)
        {
            this.Session = session;
        }
    }

    /// <summary>
    /// Provides data to ShowPermissionDialogCompleted event 
    /// </summary>
    public class PermissionCompletedEventArgs : LoginCompletedEventArgs
    {
        /// <summary>
        /// Result of permission dialog
        /// </summary>
        public string Accepted
        {
            get;
            private set;
        }

        internal PermissionCompletedEventArgs(string accepted, FacebookSession session)
            : base(session)
        {
            this.Accepted = accepted;
        }
    }

}

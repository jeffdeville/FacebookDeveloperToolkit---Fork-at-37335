using System;
using System.Net;
using System.Windows;
using Facebook.Utility;

namespace Facebook.BindingHelper
{
    /// <summary>
    /// Represents event args for DataCollectionError
    /// </summary>
    public class DataCollectionErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Exception that was generated when updating the collection
        /// </summary>
        public FacebookException Exception
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates new DataCollectionErrorEventArgs object
        /// </summary>
        /// <param name="exception"></param>
        internal DataCollectionErrorEventArgs(FacebookException exception)
        {
            this.Exception = exception;
        }
    }

}

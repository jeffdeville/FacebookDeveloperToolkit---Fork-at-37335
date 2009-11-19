using System;
using System.Net;
using System.Windows;
using Facebook.Utility;

namespace Facebook.BindingHelper
{
    /// <summary>
    /// Represents event args for DataCollectionError
    /// </summary>
    public class DataRetrievalCompletedEventArgs : EventArgs
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
        internal DataRetrievalCompletedEventArgs(FacebookException exception)
        {
            this.Exception = exception;
        }
    }

}

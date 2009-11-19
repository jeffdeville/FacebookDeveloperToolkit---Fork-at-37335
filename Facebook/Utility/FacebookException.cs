using System;
using Facebook.Schema;

namespace Facebook.Utility
{
	/// <summary>
	/// Represents an error returned by the Facebook REST-like API.
	/// </summary>
    public class FacebookException: Exception
    {
		/// <summary>
		/// The error returned by Facebook in XML format.
		/// </summary>
        public string ErrorXml { get; set; }

		/// <summary>
		/// The specific error code returned by Facebook.
		/// </summary>
        public int ErrorCode { get; set; }

        private readonly string _message = "";

        /// <summary>
        /// The request that caused the error, in XML format.
        /// </summary>
        public FacebookApiExceptionRequest_args RequestArguments
        {
            get;
            set;
        }

		/// <summary>
		/// The error message returned by Facebook.
		/// </summary>
        public override string Message
        {
            get
            {
				return _message;
            }
        }

		/// <summary>
		/// The request that caused the error, in XML format.
		/// </summary>
        public string RequestXml { get; set; }

		/// <summary>
		/// A value representing the specific type of error returned by Facebook.
		/// </summary>
        public ErrorType ErrorType { get { return (ErrorType) ErrorCode; } }

		/// <summary>
		/// Constructor for the FacebookException.
		/// </summary>
		/// <param name="errorXml">The request that caused the error, in XML format.</param>
		/// <param name="errorCode">The specific error code returned by Facebook.</param>
		/// <param name="message">The error message returned by Facebook.</param>
		/// <param name="requestXml">The request that caused the error, in XML format.</param>
        public FacebookException(string errorXml,
            int errorCode,
            string message,
            string requestXml)
        {
            ErrorXml = errorXml;
            ErrorCode = errorCode;
            _message = message;
            RequestXml = requestXml;
        }

        /// <summary>
        /// Constructor for the FacebookException.
        /// </summary>
        /// <param name="errorCode">The specific error code returned by Facebook.</param>
        /// <param name="message">The error message returned by Facebook.</param>
        /// <param name="requestArgs">Request arguments for the call that caused this exception</param>
        public FacebookException(int errorCode, string message, FacebookApiExceptionRequest_args requestArgs)
            : base(message)
        {
            ErrorCode = errorCode;
			_message = message;
            RequestArguments = requestArgs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">The error message returned by Facebook</param>
        /// <param name="e">Inner exception</param>
        public FacebookException(string message, Exception e)
            : base(message, e)
        {
			_message = message;
        }

        /// <summary>
        /// Constructor for the FacebookException.
        /// </summary>
        /// <param name="message">The error message returned by Facebook</param>
        public FacebookException(string message)
            : this(message, null)
        {
        }

    }

    /// <summary>
    /// Contains Key-Value pair exception information returned by server
    /// </summary>
    //[DataContract]
    public class RequestArgs
    {
        /// <summary>
        /// Key variable 
        /// </summary>
        //[DataMember(Name = "key")]
        public string key
        {
            get;
            set;
        }

        /// <summary>
        /// Value variable
        /// </summary>
        //[DataMember(Name = "value")]
        public string value
        {
            get;
            set;
        }
    }

	/// <summary>
	/// A value representing the specific type of error returned by Facebook.
	/// </summary>
    public enum ErrorType
        {
            ///<summary>
            ///</summary>
            Unknown = 1,
            ///<summary>
            ///</summary>
            ServiceUnavailable = 2,
            ///<summary>
            ///</summary>
            RequestLimit = 4,
            ///<summary>
            ///</summary>
            Timeout = 102,
            ///<summary>
            ///</summary>
            Signing = 104,
            ///<summary>
            ///</summary>
            InvalidUser = 110,
            ///<summary>
            ///</summary>
            InvalidAlbum = 120,
            ///<summary>
            ///</summary>
            UserNotVisible = 210,
            ///<summary>
            ///</summary>
            AlbumNotVisible = 220,
            ///<summary>
            ///</summary>
            PhotoNotVIsible = 221,
            ///<summary>
            ///</summary>
            InvaldFQLSyntax = 601
        }
}


namespace Facebook
{
    ///<summary>
    ///</summary>
    internal static class Constants
    {
        internal static string DummyCallbackUrl = "http://dummy-callback-url/";
        internal static string FacebookAuthenticationFormName = "Facebook Login";

        internal static string FacebookLoginUrl = "https://login.facebook.com/login.php?api_key={0}&auth_token={1}&v=1.0&popup";
        internal static string FacebookLogoutUrl = "http://www.facebook.com/logout.php?api_key={0}&auth_token={1}&v=1.0&confirm=1";
        internal static string FacebookNamespace = "http://api.facebook.com/1.0/";
        internal static string FacebookRequestExtendedPermissionUrl = "http://www.facebook.com/authorize.php?api_key={0}&v=1.0&ext_perm={1}";
        internal static string FacebookRESTUrl = "http://api.facebook.com/restserver.php";
		internal static string FacebookVideoUploadUrl = "http://api-video.facebook.com/restserver.php";
        internal static string MissingPictureUrl = "http://static.ak.facebook.com/pics/s_default.jpg";
        //internal static string SendRequestUrl = "http://www.facebook.com/multi_friend_selector.php";
        internal static string FacebookActivityUrl = "http://api.facebook.com/activitystreams/feed.php";

        internal const string NEWLINE = "\r\n";
        internal const string PREFIX = "--";
        
        ///<summary>
        ///</summary>
        public const string VERSION = "1.0";
        
    }
}

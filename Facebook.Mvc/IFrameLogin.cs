using System.Collections.Specialized;
using System.Web.Mvc;

namespace Facebook.Mvc
{
	public class IFrameLogin : BaseLogin, ILoginHandler
	{
		public IFrameLogin(NameValueCollection requestParameters) : base(requestParameters)
		{}

		/// <summary>
		/// Get string for redirect response
		/// </summary>
		public ActionResult GetRedirect()
		{
			string url = GetLoginUrl();
			return new ContentResult(){Content = string.Format("<script type=\"text/javascript\">\n" +
			                     "if (parent != self) \n" +
			                     "top.location.href = \"" + url + @"&v=1.0" + "\";\n" +
			                     "else self.location.href = \"" + url + @"&v=1.0" + "\";\n" +
			                     "</script>")};
		}
	}
}
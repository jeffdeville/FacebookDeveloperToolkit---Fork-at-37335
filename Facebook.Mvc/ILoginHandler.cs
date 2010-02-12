using System.Web.Mvc;

namespace Facebook.Mvc
{
	public interface ILoginHandler
	{
		/// <summary>
		/// Get string for redirect response
		/// </summary>
		ActionResult GetRedirect();
	}
}
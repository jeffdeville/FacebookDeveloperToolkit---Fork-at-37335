using System;
using System.Web.Mvc;
using Facebook.Rest;
using Facebook.Session;

namespace Facebook.Web.Mvc
{
	public class FacebookController : Controller
	{
		public FacebookController(IFacebookApi facebook)
		{
			if (facebook == null) throw new ArgumentNullException("Facebook");
			Facebook = facebook;
		}

		private IFacebookApi _facebook;
		public IFacebookApi Facebook
		{
			get
			{
				if (_facebook == null)
					throw new ArgumentNullException("IFacebookApi", "The FacebookApi object can not be null");
				if(_facebook.Session == null)
				{
					if (!HttpContext.Items.Contains(FacebookAttribute.FACEBOOK_CANVAS_SESSION))
						throw new ArgumentNullException("CanvasSession",
						                                "The facebook session must be initialized by the FacebookAttribute setting being placed on the action method.");

					_facebook.Initialize(HttpContext.Items[FacebookAttribute.FACEBOOK_CANVAS_SESSION] as FacebookSession);
					
				}
				return _facebook;
			}
			set { _facebook = value; }
		}		
	}
}
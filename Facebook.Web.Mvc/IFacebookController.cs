using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook.Rest;

namespace Facebook.Web.Mvc
{
	public interface IFacebookController
	{
		IFacebookApi Facebook { get; set; }
	}
}

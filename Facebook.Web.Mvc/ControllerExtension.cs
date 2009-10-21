using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Facebook.Rest;
using Facebook.Session;
using System.Web.Configuration;

namespace Facebook.Web.Mvc
{
    public static class ControllerExtension
    {
        public static Api GetApi(this Controller controller, string apiKey, string secret)
        {
            FBMLCanvasSession session = new FBMLCanvasSession(apiKey ?? WebConfigurationManager.AppSettings["ApiKey"], secret ?? WebConfigurationManager.AppSettings["Secret"]);
            return new Api(session);
        }
        public static Api GetApi(this Controller controller)
        {
            FBMLCanvasSession session = new FBMLCanvasSession(WebConfigurationManager.AppSettings["ApiKey"], WebConfigurationManager.AppSettings["Secret"]);
            return new Api(session);
        }
    }
}

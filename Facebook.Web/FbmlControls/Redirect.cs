using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Facebook.Web;
using System.ComponentModel;
using System.Globalization;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Redirects a user's browser to a new URL within the Facebook canvas. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:redirect" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Redirect runat=\"server\" />")]
    public class Redirect : FbmlControl
    {
        /// <summary>
        /// The URL where you are redirecting the browser. Note this does not work within a profile box
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The URL where you are redirecting the browser. Note this does not work within a profile box")]
        public string Url
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_REDIRECT; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if(string.IsNullOrEmpty(Url))
                throw new MissingRequiredAttribute("Url", Url);

            writer.AddAttribute("url", Url);
            base.AddAttributesToRender(writer);
        }
    }
}

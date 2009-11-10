using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders a CAPTCHA on your canvas page inside of a form. When that form is submitted to a canvas page, your callback URL will be called with the additional POST parameter fb_sig_captcha_grade=1 to indicate a successfully completed CAPTCHA.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:captcha" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Captcha runat=\"server\" />")]
    public class Captcha : FbmlControl
    {
        /// <summary>
        /// When showalways is set, Facebook will show a CAPTCHA to all users. If it is not set, then only users with accounts unverified by Facebook will see the CAPTCHA. This is the same logic Facebook uses for determining whether to show CAPTCHAs. (Default value is false.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("When showalways is set, Facebook will show a CAPTCHA to all users. If it is not set, then only users with accounts unverified by Facebook will see the CAPTCHA. This is the same logic Facebook uses for determining whether to show CAPTCHAs. (Default value is false.) ")]
        public bool ShowAlways
        {
            get;
            set;
        }        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_CAPTCHA; }
        }
        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (ShowAlways)
                writer.AddAttribute("showalways", FbmlConstants.TRUE);

            base.AddAttributesToRender(writer);
        }
    }
}


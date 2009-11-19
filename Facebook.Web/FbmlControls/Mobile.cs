using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders the contained content only when viewed on the mobile website (http://m.facebook.com). Any content outside these tags does not get rendered on the mobile website.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:mobile" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Mobile runat=\"server\" />")]
    public class Mobile : ContentDisplayControl
    {
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_MOBILE; }
        }
    }
}


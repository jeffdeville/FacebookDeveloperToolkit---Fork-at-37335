using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders a standard Facebook dashboard header. 
    /// Dashboards can contain the following elements: 
    /// fb:action 
    /// fb:create-button 
    /// fb:help 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:dashboard" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Dashboard runat=\"server\" />")]
    public class Dashboard : ContentDisplayControl
    {
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_DASHBOARD; }
        }
    }
}


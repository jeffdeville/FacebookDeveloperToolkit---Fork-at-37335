using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders a group of standard Facebook navigation tabs. Must contain at least one fb:tab-item. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:tabs" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Tabs runat=\"server\" />")]
    public class Tabs : ContentDisplayControl
    {
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_TABS; }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Emulates the look of a wall environment.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:wall" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Wall runat=\"server\" />")]
    public class Wall : ContentDisplayControl
    {
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_WALL; }
        }
    }
}


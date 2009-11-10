using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Evaluates every fb: tag inside and returns the first one that evaluates to anything other than an empty string. You can use fb:default to specify a default that gets rendered if nothing else does before it was executed. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:switch" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Switch runat=\"server\" />")]
    public class Switch : ContentDisplayControl
    {
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_SWITCH; }
        }

    }
}


using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Displays a Facebook-skinned explanation message.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:explanation">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Explanation runat=\"server\" />")]
    public class Explanation : StatusMessageBase
    {
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_EXPLANATION; }
        }
    }
}

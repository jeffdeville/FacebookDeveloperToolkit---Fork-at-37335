using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Displays a Facebook-skinned success message.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:success">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Success runat=\"server\" />")]
    public sealed class Success : StatusMessageBase
    {
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_SUCCESS; }
        }
    }
}

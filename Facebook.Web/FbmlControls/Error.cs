using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Facebook.Web;
using System.Web.UI;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Displays a Facebook-skinned error message.
    /// Derived from http://facebookui.codeplex.com/ by Robert Paveza
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:error">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Error runat=\"server\" />")]
    public class Error : StatusMessageBase
    {
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_ERROR; }
        }
    }
}

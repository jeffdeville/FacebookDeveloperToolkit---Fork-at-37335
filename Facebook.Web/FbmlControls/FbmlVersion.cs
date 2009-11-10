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
    /// Prints the version of FBML currently in scope. You should use this tag only for debugging purposes.  
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:fbml-version" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:FbmlVersion runat=\"server\" />")]
    public class FbmlVersion : FbmlControl
    {
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_FBMLVERSION; }
        }
    }
}

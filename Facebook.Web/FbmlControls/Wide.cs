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
    /// The enclosed content appears only when profile box is in the wide column of the profile. See fb:narrow for the opposite.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:wide" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Wide runat=\"server\" />")]
    public class Wide : ContentDisplayControl
    {


        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_WIDE; }
        }


    }
}

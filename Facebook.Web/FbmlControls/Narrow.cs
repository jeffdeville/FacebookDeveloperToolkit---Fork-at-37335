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
    /// Renders the content contained by the tag only if the profile box is in the narrow column of the profile.
    /// If no fb:narrow or fb:wide tag is specified, all content is displayed in either column. 
    /// The narrow profile box is 200 pixels wide, including margins. There are 8 pixels of padding on each side, so the actual amount of column width you can use is 184 pixels. 
    /// If you want your fb:narrow information to appear when installed, set the Default Profile Box Column value to Narrow in Installation section of your application's settings in the Developer application. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:narrow" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Narrow runat=\"server\" />")]
    public class Narrow : ContentDisplayControl
    {


        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_NARROW; }
        }


    }
}

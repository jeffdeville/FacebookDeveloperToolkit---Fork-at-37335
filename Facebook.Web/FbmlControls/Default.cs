using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// For a group of fb: tags contained within an fb:switch tag, the fb:default tag renders any content inside itself if no other fb: tag inside the fb:switch tag renders code before it.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:default" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Default runat=\"server\" />")]
    public class Default : ContentDisplayControl
    {
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_DEFAULT; }
        }

    }
}


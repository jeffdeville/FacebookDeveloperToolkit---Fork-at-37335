using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// A container for one or more fb:editor-button tags, which are rendered next to each other with some space between each button.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:editor-buttonset" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:EditorButtonset runat=\"server\" />")]
    public class EditorButtonset : ContentDisplayControl
    {
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_EDITOR_BUTTONSET; }
        }
    }
}


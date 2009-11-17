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
    /// Renders a horizontal line separator in the column containing the form elements. Despite appearing like an &lt;hr&gt; element, it is actually a &lt;div&gt; with a class of divider. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:editor-divider" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:EditorDivider runat=\"server\" />")]
    public class EditorDivider : FbmlControl
    {
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_EDITOR_DIVIDER; }
        }
    }
}

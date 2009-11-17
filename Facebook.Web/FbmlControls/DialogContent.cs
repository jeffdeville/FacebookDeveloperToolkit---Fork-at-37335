using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// The fb:dialog-content tag is a child of fb:dialog and represents the content that gets displayed inside the popup dialog when it appears. 
    /// This section can be styled like any other part of your app. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:dialog-content" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:DialogContent runat=\"server\" />")]
    public class DialogContent : ContentDisplayControl
    {
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_DIALOG_CONTENT; }
        }
    }
}


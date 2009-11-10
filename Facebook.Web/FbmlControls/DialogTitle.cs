using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// fb:dialog-title is a child of fb:dialog and renders a title for what is displayed inside the popup dialog.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:dialog-title" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:DialogTitle runat=\"server\" />")]
    public class DialogTitle : FbmlControl
    {
        /// <summary>
        /// The title for the fb:title tag 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The title for the fb:title element ")]
        public string Text
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_DIALOG_TITLE; }
        }
        /// <inheritdoc />
        protected override void Render(HtmlTextWriter writer)
        {
            FbmlTextWriter wr = new FbmlTextWriter(writer);
            wr.RenderTagWithContents(ElementName, Text);
        }
    }
}


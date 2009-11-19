using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Sets the page's &lt;title&gt; tag to its contents. Alternatively, when used inside fb:comments, sets the title for the Wall
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:title" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Title runat=\"server\" />")]
    public class Title : FbmlControl
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
            get { return FbmlConstants.FB_TITLE; }
        }
        /// <inheritdoc />
        protected override void Render(HtmlTextWriter writer)
        {
            FbmlTextWriter wr = new FbmlTextWriter(writer);
            wr.RenderTagWithContents(ElementName, Text);
        }
    }
}


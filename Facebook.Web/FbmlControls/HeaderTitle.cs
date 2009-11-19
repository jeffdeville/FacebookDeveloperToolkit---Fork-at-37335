using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Specifies the header title for a fb:mediaheader. Note: fb:header-title does not support HTML placed inside this tag (ie/ if you insert html in between fb:header-title tags, the html is escaped). 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:header-title" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:HeaderTitle runat=\"server\" />")]
    public class HeaderTitle : FbmlControl
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
            get { return FbmlConstants.FB_HEADER_TITLE; }
        }
        /// <inheritdoc />
        protected override void Render(HtmlTextWriter writer)
        {
            FbmlTextWriter wr = new FbmlTextWriter(writer);
            wr.RenderTagWithContents(ElementName, Text);
        }
    }
}


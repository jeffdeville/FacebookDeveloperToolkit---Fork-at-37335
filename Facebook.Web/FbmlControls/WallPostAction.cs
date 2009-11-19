using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Displays a link at the bottom of a wallpost (even if it appears before other text within the fb:wallpost tag). 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:wallpost-action" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:WallPostAction runat=\"server\" />")]
    public class WallPostAction : FbmlControl
    {
        /// <summary>
        /// the URL of the link. must be absolute  
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("the URL of the link. must be absolute ")]
        public string Href
        {
            get;
            set;
        }
        /// <summary>
        /// The text of the link
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The text of the link")]
        public string Text
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_WALLPOST_ACTION; }
        }
        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(Href))
            {
                writer.AddAttribute("href", Href);
            }
            else
            {
                throw new MissingRequiredAttribute("Href", Href);
            }
            base.AddAttributesToRender(writer);
        }
        /// <inheritdoc />
        protected override void Render(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(Text))
            {
                throw new MissingRequiredAttribute("Text", Text);
            }
            FbmlTextWriter wr = new FbmlTextWriter(writer);

            AddAttributesToRender(wr);

            wr.RenderTagWithContents(ElementName, Text);
        }
    }
}


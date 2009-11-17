using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Specifies an action link to be displayed inside a fb:mediaheader when the viewer is the owner of the content 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:owner-action" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:OwnerAction runat=\"server\" />")]
    public class OwnerAction : FbmlControl
    {
        /// <summary>
        /// the URL corresponding to the action. 
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("the URL corresponding to the action. ")]
        public string Href
        {
            get;
            set;
        }
        /// <summary>
        /// The text to appear inside the button
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The text to appear inside the button")]
        public string Text
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_OWNER_ACTION; }
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


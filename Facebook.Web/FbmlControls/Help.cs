using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders a help link. This tag must be a child of fb:dashboard. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:help" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Help runat=\"server\" />")]
    public class Help : FbmlControl
    {
        /// <summary>
        /// The URL where the link for the button takes the user. The URL must be a canvas page or fragment url (# and following string). For example, href="http://apps.facebook.com/&lt;appname&gt;/new.php". 
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The URL where the link for the button takes the user. The URL must be a canvas page or fragment url (# and following string). For example, href=\"http://apps.facebook.com/<appname>/new.php\". ")]
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
            get { return FbmlConstants.FB_HELP; }
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


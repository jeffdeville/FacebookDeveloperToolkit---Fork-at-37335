using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders a Create button for adding user-generated content. This tag must be a child of fb:dashboard.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:create-button" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:CreateButton runat=\"server\" />")]
    public class CreateButton : FbmlControl
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
        /// <summary>
        /// Calls an FBJS function when a user clicks the button. When using an onclick action the href can be a fragment url to prevent page reloads.
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Calls an FBJS function when a user clicks the button. When using an onclick action the href can be a fragment url to prevent page reloads.")]
        public string OnClick
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_CREATE_BUTTON; }
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
            if (!string.IsNullOrEmpty(OnClick))
            {
                writer.AddAttribute("onclick", OnClick);
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


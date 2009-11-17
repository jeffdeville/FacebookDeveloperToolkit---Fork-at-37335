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
    /// Renders a standard Share button in a profile for the specified URL or content. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:share-button" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:ShareButton runat=\"server\" />")]
    public class ShareButton : ContentDisplayControl
    {
        /// <summary>
        /// The type of share button. When used as an FBML tag, valid values are url, to render a share of the URL specified with the href attribute, and meta, to render a share with the given data. When used as an XFBML tag, the value must be url. 
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The type of share button. When used as an FBML tag, valid values are url, to render a share of the URL specified with the href attribute, and meta, to render a share with the given data. When used as an XFBML tag, the value must be url. ")]
        public string Class
        {
            get;
            set;
        }
        /// <summary>
        /// The reference URL to share. This attribute is required for the url class only. 
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The reference URL to share. This attribute is required for the url class only. ")]
        public string Href
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_SHARE_BUTTON; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(Class))
                throw new MissingRequiredAttribute("Class", Class);

            writer.AddAttribute("class", Class);
            if (string.IsNullOrEmpty(Href))
                throw new MissingRequiredAttribute("Href", Href);

            writer.AddAttribute("href", Href);

            base.AddAttributesToRender(writer);
        }
    }
}

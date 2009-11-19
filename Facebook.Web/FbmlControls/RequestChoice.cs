using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Facebook.Web;
using System.ComponentModel;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Represents a request button for use on the user's requests page.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:request-choice" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:RequestChoice runat=\"server\" />")]
    public class RequestChoice : FbmlButtonBase
    {
        /// <summary>
        /// Gets or sets the URL to which this button links.
        /// </summary>
        /// <remarks>
        /// <para>This value should be the URL to the Facebook Add Application URL or the homepage of the application.</para>
        /// </remarks>
        [FbmlRequired(IsRequired = true)]
        [Browsable(true)]
        [DefaultValue(null)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("Specifies the url to which this button links.")]
        public string Url
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_REQ_CHOICE; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(Url))
                throw new MissingRequiredAttribute("Url", Url);

            base.AddAttributesToRender(writer);

            writer.AddAttribute("url", Url);
        }
    }
}

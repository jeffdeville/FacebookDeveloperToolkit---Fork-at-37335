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
    /// Renders a link, usually for navigational purposes. Its appearance depends on its container. 
    /// The tag must be a child of either fb:dashboard or fb:subtitle. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:action" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Action runat=\"server\" />")]
    public class Action : FbmlControl
    {
        /// <summary>
        /// The URL for the link. The URL must be a canvas page. For example, href="http://apps.facebook.com/&lt;appname&gt;/&lt;filename&gt;.php". 
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The URL for a \"See all\" link. This URL must be a facebook canvas page. ")]
        public string Href
        {
            get;
            set;
        }
        /// <summary>
        /// Call a FBJS function  
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Call a FBJS function ")]
        public string OnClick
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_ACTION; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if(string.IsNullOrEmpty(Href))
                throw new MissingRequiredAttribute("Href", Href);

            writer.AddAttribute("href", Href);

            if(!string.IsNullOrEmpty(OnClick))
                writer.AddAttribute("onclick", OnClick);

            base.AddAttributesToRender(writer);
        }
    }
}

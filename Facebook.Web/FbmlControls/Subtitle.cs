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
    /// Defines the subtitle for the profile box. The enclosed content is interpreted purely as plain text. 
    /// This tag may contain fb:action, an optional tag that renders an action link on the right-hand side of the subtitle. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:subtitle" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Subtitle runat=\"server\" />")]
    public class Subtitle : ContentDisplayControl
    {
        /// <summary>
        /// The URL for a "See all" link. This URL must be a facebook canvas page. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The URL for a \"See all\" link. This URL must be a facebook canvas page. ")]
        public string SeeAllUrl
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_SUBTITLE; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if(!string.IsNullOrEmpty(SeeAllUrl))
                writer.AddAttribute("seeallurl", SeeAllUrl.ToString(CultureInfo.InvariantCulture));

            base.AddAttributesToRender(writer);
        }
    }
}

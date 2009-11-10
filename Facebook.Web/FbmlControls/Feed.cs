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
    /// Renders an application-specific News Feed, which displays recent application stories about the logged in user's friends. This tag is in beta, and is only available on canvas pages. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:feed" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Feed runat=\"server\" />")]
    public class Feed : FbmlControl
    {
        /// <summary>
        /// The title that should be published at the top of an application's News Feed. (Default value is News Feed.)  
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The title that should be published at the top of an application's News Feed. (Default value is News Feed.) ")]
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// The maximum number of stories that should be displayed in the News Feed.   
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The maximum number of stories that should be displayed in the News Feed.")]
        [DefaultValue(0)]
        public int Max
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_FEED; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {

            if(!string.IsNullOrEmpty(Title))
                writer.AddAttribute("title", Title);
            if (Max>0)
                writer.AddAttribute("max", Max.ToString());

            base.AddAttributesToRender(writer);
        }
    }
}

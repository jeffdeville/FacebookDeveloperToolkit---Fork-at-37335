using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Facebook.Web;
using System.Web.UI;
using System.Globalization;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Displays the enclosed content only if the viewer has granted full permissions to the application
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:visible-to-app-users" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:VisibleToAppUsers runat=\"server\" />")]
    public class VisibleToAppUsers : ContentDisplayControl
    {
        /// <summary>
        /// The user's profile to link to the Publisher.
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The user's profile to link to the Publisher.")]
        public string BgColor
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(BgColor))
                writer.AddAttribute("bgcolor", BgColor.ToString(CultureInfo.InvariantCulture));

            base.AddAttributesToRender(writer);
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_VISIBLE_TO_APP_USERS; }
        }
    }
}

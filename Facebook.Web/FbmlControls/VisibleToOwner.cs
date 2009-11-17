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
    /// Displays content inside only if the viewer of the profile matches the profile owner. 
    /// This also works for Facebook Pages with the content only displayed to an admin of the Page. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:visible-to-owner" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:VisibleToOwner runat=\"server\" />")]
    public class VisibleToOwner : ContentDisplayControl
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
            get { return FbmlConstants.FB_VISIBLE_TO_OWNER; }
        }
    }
}

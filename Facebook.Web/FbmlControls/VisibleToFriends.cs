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
    /// Use this tag to display the content inside the tag on a user's profile only if the viewer is a friend of that user. 
    /// While this tag is still valid, you may find it more versatile to use fb:visible-to-connection, since it applies equally to user profiles and Facebook Pages.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:visible-to-friends" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:VisibleToFriends runat=\"server\" />")]
    public class VisibleToFriends : ContentDisplayControl
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
            get { return FbmlConstants.FB_VISIBLE_TO_FRIENDS; }
        }
    }
}

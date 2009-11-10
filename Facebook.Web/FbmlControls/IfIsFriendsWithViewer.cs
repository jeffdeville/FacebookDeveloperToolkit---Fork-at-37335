using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Globalization;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Displays the enclosed content only if the specified user is friends with the logged in user. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook Wiki documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:if-is-friends-with-viewer" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:IfIsFriendsWithViewer runat=\"server\"></{0}:IfIsFriendsWithViewer>")]
    [DefaultProperty("Uid")]
    public class IfIsFriendsWithViewer : ConditionalControl
    {
        /// <summary>
        /// Creates a new <see>IfIsFriendsWithViewer</see>.
        /// </summary>
        public IfIsFriendsWithViewer()
        {
            IncludeSelf = true;
        }

        /// <summary>
        /// The user ID to check. (Default value is loggedinuser.)
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The user ID to check. (Default value is loggedinuser.)")]
        [DefaultValue(0)]
        public long Uid
        {
            get;
            set;
        }

        /// <summary>
        /// Optionally gets or sets the ID of another user if the current user should be able to see the photo through another user.
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Specifies whether the control should treat the owner as friends with himself or herself.")]
        [DefaultValue(true)]
        public bool IncludeSelf
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (Uid != 0)
                writer.AddAttribute("uid", Uid.ToString(CultureInfo.InvariantCulture));
            if (!IncludeSelf)
                writer.AddAttribute("includeself", FbmlConstants.FALSE);
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_IF_IS_FRIENDS_WITH_VIEWER; }
        }
    }
}

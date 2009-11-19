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
    /// Renders a single cell of a table, which contains a thumbnail and name for a particular user, similar to the Mutual Friends table on profile pages. Must be used inside a fb:user-table tag. This tag only works on profile pages. Applications cannot use this on their canvas pages
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:user-item" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:UserItem runat=\"server\" />")]
    public class UserItem : FbmlControl
    {
        /// <summary>
        /// The ID of the user whose name and photo you want to show. 
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The ID of the user whose name and photo you want to show. ")]
        [DefaultValue(0)]
        public long Uid
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_USER_ITEM; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (Uid == 0)
                throw new MissingRequiredAttribute("Uid", Uid);

            writer.AddAttribute("uid", Uid.ToString(CultureInfo.InvariantCulture));

            base.AddAttributesToRender(writer);
        }
    }
}

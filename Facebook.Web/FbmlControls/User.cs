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
    /// Hides the content enclosed within this control from any user who is blocked by the user referenced with the 
    /// <see>UserID</see> property.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:user" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:User runat=\"server\" />")]
    public class User : ContentDisplayControl
    {
        /// <summary>
        /// The user ID of the user whose information will be contained in the tag. 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The user ID of the user whose information will be contained in the tag. ")]
        [DefaultValue(0)]
        [FbmlRequired(IsRequired = true)]
        public long Uid
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (Uid == 0)
                throw new MissingRequiredAttribute("Uid", Uid);

            writer.AddAttribute("uid", Uid.ToString(CultureInfo.InvariantCulture));

            base.AddAttributesToRender(writer);
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_USER; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Facebook.Web;
using System.Globalization;
using System.Web.UI;
using System.ComponentModel.Design;
using System.Drawing.Design;
using Facebook.Utility;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Displays content inside the tag only if the logged-in user (or specified user) is one of the specified users.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook Wiki documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:if-is-user" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:IfIsUser runat=\"server\"></{0}:IfIsUser>")]
    [DefaultProperty("Uid")]
    public class IfIsUser : ConditionalControl
    {
        /// <summary>
        /// Gets or sets the user IDs of the users who are allowed to view the content inside the tag.
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The user IDs of the users who are allowed to see the content.")]
        [DefaultValue("0")]
        [FbmlRequired(IsRequired = true)]
        public string Uid
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (Uid == null)
                throw new MissingRequiredAttribute("Uid", Uid);

            writer.AddAttribute("uid", Uid);
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_IF_IS_USER; }
        }
    }
}

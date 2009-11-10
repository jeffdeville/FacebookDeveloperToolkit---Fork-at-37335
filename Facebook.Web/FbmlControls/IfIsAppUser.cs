using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Displays the enclosed content only if the specified user has accepted the terms of service of the application (that is, authorized your application). For example, if the user has not authorized your application, you can display a prompt to for the user to authorize your application. If the user has already authorized your application you can use fb:else to render application content. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook Wiki documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:if-is-app-user" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:IfIsAppUser runat=\"server\"></{0}:IfIsAppUser>")]
    [DefaultProperty("Uid")]
    public class IfIsAppUser : ConditionalControl
    {
        /// <summary>
        /// The user ID to check. (Default value is loggedinuser.) 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The user ID to check. (Default value is loggedinuser.) ")]
        [DefaultValue(0)]
        public long Uid
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (Uid != 0)
                writer.AddAttribute("uid", Uid.ToString(CultureInfo.InvariantCulture));
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_IF_IS_APP_USER; }
        }
    }
}

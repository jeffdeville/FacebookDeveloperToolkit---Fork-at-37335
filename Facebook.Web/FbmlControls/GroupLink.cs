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
    /// Prints the specified group name and formats it as a link to the group's page. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:grouplink" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:GroupLink runat=\"server\" />")]
    public class GroupLink : FbmlControl
    {
        /// <summary>
        /// Group ID for the group whose name and link you want to retrieve.  
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("Group ID for the group whose name and link you want to retrieve. ")]
        [DefaultValue(0)]
        public long Gid
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_GROUPLINK; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (Gid == 0)
                throw new MissingRequiredAttribute("Gid", Gid);

            writer.AddAttribute("gid", Gid.ToString(CultureInfo.InvariantCulture));

            base.AddAttributesToRender(writer);
        }
    }
}

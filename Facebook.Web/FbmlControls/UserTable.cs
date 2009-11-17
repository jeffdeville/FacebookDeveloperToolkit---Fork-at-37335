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
    /// Renders a table, each cell of which contains a thumbnail and name for a particular user, similar to the Mutual Friends table on profile pages. Inside this tag, use fb:user-item tags to specify the set of users. This tag only works on profile pages. Applications cannot use this on their canvas pages. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:user-table" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:UserTable runat=\"server\" />")]
    public class UserTable : ContentDisplayControl
    {
        /// <summary>
        /// The number of columns in the table. (Default value is 6 for fb:wide, 3 for fb:narrow.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The number of columns in the table. (Default value is 6 for fb:wide, 3 for fb:narrow.)")]
        [DefaultValue(0)]
        public long Cols
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_USER_TABLE; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {

            if (Cols>0)
                writer.AddAttribute("cols", Cols.ToString());

            base.AddAttributesToRender(writer);
        }
    }
}

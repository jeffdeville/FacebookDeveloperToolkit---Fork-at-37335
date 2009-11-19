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
    /// Renders an anchor tag around the internal content pointing to a profile with the application's Publisher preselected.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:publisher-link" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:PublisherLink runat=\"server\" />")]
    public class PublisherLink : FbmlControl
    {
        /// <summary>
        /// The user's profile to link to the Publisher.
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The user's profile to link to the Publisher.")]
        [DefaultValue(0)]
        public long Uid
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (Uid > 0)
                writer.AddAttribute("uid", Uid.ToString(CultureInfo.InvariantCulture));

            base.AddAttributesToRender(writer);
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_PUBLISHER_LINK; }
        }
    }
}

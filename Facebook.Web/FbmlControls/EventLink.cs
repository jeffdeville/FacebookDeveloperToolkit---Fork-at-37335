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
    /// Prints the specified event name and formats it as a link to the event's page.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:eventlink" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:EventLink runat=\"server\" />")]
    public class EventLink : FbmlControl
    {
        /// <summary>
        /// Event ID for the event whose name and link you want to retrieve. 
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("Event ID for the event whose name and link you want to retrieve.")]
        [DefaultValue(0)]
        public long Eid
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_EVENTLINK; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (Eid == 0)
                throw new MissingRequiredAttribute("Eid", Eid);

            writer.AddAttribute("eid", Eid.ToString(CultureInfo.InvariantCulture));

            base.AddAttributesToRender(writer);
        }
    }
}

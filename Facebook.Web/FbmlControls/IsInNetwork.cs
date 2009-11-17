using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Facebook.Web;
using System.Globalization;
using System.Web.UI;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Displays content inside the tag only if the user is in a given network.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook Wiki documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:is-in-network" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:IsInNetwork runat=\"server\"></{0}:IsInNetwork>")]
    public class IsInNetwork : ConditionalControl
    {
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_IS_IN_NETWORK; }
        }

        /// <summary>
        ///The network ID to check. You can check one network at a time. 
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Browsable(true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The network ID to check. You can check one network at a time. ")]
        [DefaultValue(0)]
        public long Network
        {
            get;
            set;
        }

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

            if (Network == 0)
                throw new MissingRequiredAttribute("Network", Network);

            writer.AddAttribute("network", Network.ToString(CultureInfo.InvariantCulture));

            if (Uid != 0)
                writer.AddAttribute("uid", Uid.ToString(CultureInfo.InvariantCulture));
        }
    }
}

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
    /// Contains code to be output when selected by the fb:random tag. You can control the frequency of this pick with the weight attribute. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:random-option" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:RandomOption runat=\"server\" />")]
    public class RandomOption : FbmlControl
    {
        /// <summary>
        /// Allows for controlling the frequency of a choice. (Default value is 1.0.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Allows for controlling the frequency of a choice. (Default value is 1.0.) ")]
        [DefaultValue(0)]
        public decimal Weight
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_RANDOM_OPTION; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {

            if (Weight>0)
                writer.AddAttribute("weight", Weight.ToString());

            base.AddAttributesToRender(writer);
        }
    }
}

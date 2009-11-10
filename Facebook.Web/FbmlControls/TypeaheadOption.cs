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
    /// This tag specifies the values for a typeahead form input. You must use it in conjunction with Fb:typeahead-input. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:typeahead-option" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:TypeaheadOption runat=\"server\" />")]
    public class TypeaheadOption : FbmlControl
    {
        /// <summary>
        /// The value assigned to the fb:typeahead-input element, used in the POST request when the form is submitted. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The value assigned to the fb:typeahead-input element, used in the POST request when the form is submitted. ")]
        [DefaultValue(null)]
        public string Value
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_TYPEAHEAD_OPTION; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {

            if (!string.IsNullOrEmpty(Value))
                writer.AddAttribute("value", Value);

            base.AddAttributesToRender(writer);
        }
    }
}

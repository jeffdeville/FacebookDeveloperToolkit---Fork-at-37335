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
    /// Creates a type-ahead tool (as suggested) that will give you the results that you specify. To add options in the input box, use fb:typeahead-option. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:typeahead-input" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:TypeaheadInput runat=\"server\" />")]
    public class TypeaheadInput : ContentDisplayControl
    {
        /// <summary>
        /// The variable name that is sent in the POST request when the form is submitted. 
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The variable name that is sent in the POST request when the form is submitted. ")]
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// Set to off to prevent the browser's autocomplete feature from overriding this tag's ability to autocomplete. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Set to off to prevent the browser's autocomplete feature from overriding this tag's ability to autocomplete. ")]
        [DefaultValue(false)]
        public bool AutoCompleteOff
        {
            get;
            set;
        }
        /// <summary>
        /// The size of the input field.  
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The size of the input field. ")]
        [DefaultValue(0)]
        public int Size
        {
            get;
            set;
        }
        /// <summary>
        /// The pre-selected option value.   
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The pre-selected option value. ")]
        [DefaultValue(0)]
        public string Value
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_TYPEAHEAD_INPUT; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(Name))
                throw new MissingRequiredAttribute("Name", Name);

            writer.AddAttribute("name", Name);

            if (AutoCompleteOff)
                writer.AddAttribute("autocomplete", "off");
            if(Size>0)
                writer.AddAttribute("size", Size.ToString());
            if (!string.IsNullOrEmpty(Value))
                writer.AddAttribute("value", Value);


            base.AddAttributesToRender(writer);
        }
    }
}

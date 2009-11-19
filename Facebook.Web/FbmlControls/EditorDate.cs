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
    /// Creates two drop down list boxes that let a user select a date. The month is listed using a three-letter abbreviation, and the day is listed as a numerical digit from 1 to 31 (with no leading zeroes). 
    /// You can only use one date selector per page.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:editor-date" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:EditorDate runat=\"server\" />")]
    public class EditorDate : FbmlControl
    {
        /// <summary>
        /// The label to display to the left of both selectors.
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The label to display to the left of both selectors.")]
        public string Label
        {
            get;
            set;
        }
        /// <summary>
        /// The Unix timestamp of the date to display when the page loads. (Default value is Dec 31.)  
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The Unix timestamp of the date to display when the page loads. (Default value is Dec 31.)  ")]
        public long Value
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_EDITOR_DATE; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(Label))
                throw new MissingRequiredAttribute("Label", Label);

            writer.AddAttribute("label", Label);

            if (Value>0)
                writer.AddAttribute("value", Value.ToString());
            base.AddAttributesToRender(writer);
        }
    }
}

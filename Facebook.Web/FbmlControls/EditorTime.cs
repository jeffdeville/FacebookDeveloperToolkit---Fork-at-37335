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
    /// Creates a series of form selector elements showing the time in hours and minutes, and an AM/PM indicator. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:editor-time" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:EditorTime runat=\"server\" />")]
    public class EditorTime : FbmlControl
    {
        /// <summary>
        /// The label to display on the left side of the selector. (Default value is blank.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The label to display on the left side of the selector. (Default value is blank.) ")]
        public string Label
        {
            get;
            set;
        }
        /// <summary>
        /// The name of the field that is passed when the form is submitted. (Default value is "time".) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The name of the field that is passed when the form is submitted. (Default value is \"time\".) ")]
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// The default value in epoch seconds with which to populate the selector. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The default value in epoch seconds with which to populate the selector. ")]
        public long Value
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_EDITOR_TIME; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {

            if (!string.IsNullOrEmpty(Name))
                writer.AddAttribute("name", Name);
            if (!string.IsNullOrEmpty(Label))
                writer.AddAttribute("label", Label);
            if (Value > 0)
                writer.AddAttribute("value", Value.ToString());
            base.AddAttributesToRender(writer);
        }
    }
}

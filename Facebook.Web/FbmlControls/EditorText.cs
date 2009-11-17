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
    /// Creates an &lt;input&gt; of type text. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:editor-text" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:EditorText runat=\"server\" />")]
    public class EditorText : FbmlControl
    {
        /// <summary>
        /// The default text that populates the text box. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The default text that populates the text box. ")]
        public string Value
        {
            get;
            set;
        }
        /// <summary>
        /// The name for the control. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The name for the control. ")]
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// The label to display on the left side of the text box. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The label to display on the left side of the text box. ")]
        public string Label
        {
            get;
            set;
        }
        /// <summary>
        /// The maximum length of the input allowed in the text box. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The maximum length of the input allowed in the text box. ")]
        public int MaxLength
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_EDITOR_TEXT; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {

            if (!string.IsNullOrEmpty(Name))
                writer.AddAttribute("name", Name);
            if (!string.IsNullOrEmpty(Value))
                writer.AddAttribute("value", Value);
            if (!string.IsNullOrEmpty(Label))
                writer.AddAttribute("label", Label);
            if (MaxLength > 0)
                writer.AddAttribute("maxlength", MaxLength.ToString());
            base.AddAttributesToRender(writer);
        }
    }
}

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
    /// Creates a &lt;textarea&gt; element.  
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:editor-textarea" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:EditorTextArea runat=\"server\" />")]
    public class EditorTextArea : FbmlControl
    {
        /// <summary>
        /// The name of the field that is passed when the form is submitted. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The name of the field that is passed when the form is submitted. ")]
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
        /// The height of the text area in number of lines of text. This is identical to the HTML textarea tag. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The height of the text area in number of lines of text. This is identical to the HTML textarea tag. ")]
        public int Rows
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_EDITOR_TEXTAREA; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {

            if (!string.IsNullOrEmpty(Name))
                writer.AddAttribute("name", Name);
            if (!string.IsNullOrEmpty(Label))
                writer.AddAttribute("label", Label);
            if (Rows>0)
                writer.AddAttribute("rows", Rows.ToString());
            base.AddAttributesToRender(writer);
        }
    }
}

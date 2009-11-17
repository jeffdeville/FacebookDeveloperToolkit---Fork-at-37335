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
    /// Renders a Cancel button inside an fb:editor tag. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:editor-cancel" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:EditorCancel runat=\"server\" />")]
    public class EditorCancel : FbmlControl
    {
        /// <summary>
        /// The caption for the button. (Default value is "Cancel".) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The caption for the button. (Default value is \"Cancel\".) ")]
        public string Value
        {
            get;
            set;
        }
        /// <summary>
        /// The URL to redirect to upon clicking. (Default value is "#".) This doesn't actually cancel anything, so you should set the href to at least reload the page. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The URL to redirect to upon clicking. (Default value is \"#\".) This doesn't actually cancel anything, so you should set the href to at least reload the page. ")]
        public string Href
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_EDITOR_CANCEL; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {

            if (!string.IsNullOrEmpty(Value))
                writer.AddAttribute("value", Value);
            if (!string.IsNullOrEmpty(Href))
                writer.AddAttribute("href", Href);
            base.AddAttributesToRender(writer);
        }
    }
}

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
    /// Renders a button of type submit inside an fb:editor tag. 
    /// This tag can be a child of an fb:editor-buttonset container to render multiple buttons next to each other. If the button is not in a fb:editor-buttonset, the button still renders, but is not styled as an editor-button
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:editor-button" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:EditorButton runat=\"server\" />")]
    public class EditorButton : FbmlControl
    {
        /// <summary>
        /// The text label for the button.
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The text label for the button.")]
        public string Value
        {
            get;
            set;
        }
        /// <summary>
        /// The variable name that is sent in the POST request when the form is submitted. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The variable name that is sent in the POST request when the form is submitted. ")]
        public string Name
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_EDITOR_BUTTON; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(Value))
                throw new MissingRequiredAttribute("Value", Value);

            writer.AddAttribute("value", Value);

            if (!string.IsNullOrEmpty(Name))
                writer.AddAttribute("name", Name.ToString());
            base.AddAttributesToRender(writer);
        }
    }
}

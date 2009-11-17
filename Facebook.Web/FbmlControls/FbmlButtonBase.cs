using System;
using System.Collections.Generic;
using System.Text;
using Facebook.Web;
using System.ComponentModel;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Represents common functionality for buttons represented in FBML.
    /// </summary>
    [DefaultProperty("Text")]
    public abstract class FbmlButtonBase : FbmlControl
    {
        /// <summary>
        /// Gets or sets the label that should be displayed on the button face.
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Browsable(true)]
        [DefaultValue(null)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("Specifies the text that should be displayed on the button.")]
        public string Text
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(Text))
                throw new MissingRequiredAttribute("Text", Text);

            base.AddAttributesToRender(writer);

            writer.AddAttribute("label", Text, true);
        }
    }
}

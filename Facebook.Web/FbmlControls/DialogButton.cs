using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders a button for the fb:dialog popup. 
    /// The fb:dialog-button tag is a child of fb:dialog and must be contained within it. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:dialog-button" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:DialogButton runat=\"server\" />")]
    public class DialogButton : FbmlControl
    {
        private DialogButtonType _type = DialogButtonType.None;

        /// <summary>
        /// The type of button. Specify button for a general button or submit for a Submit button to submit the form.
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The type of button. Specify button for a general button or submit for a Submit button to submit the form.")]
        [DefaultValue(DialogButtonType.None)]
        public DialogButtonType Type
        {
            get { return _type; }
            set
            {
                if (!Enum.IsDefined(typeof(DialogButtonType), value))
                    throw new InvalidEnumArgumentException("Type", (int)value, typeof(DialogButtonType));
                _type = value;
            }
        }
        /// <summary>
        /// The label text for the button. 
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The label text for the button. ")]
        public string Value
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates whether to close the popup dialog when the user clicks the button.
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("Indicates whether to close the popup dialog when the user clicks the button.")]
        public bool CloseDialog
        {
            get;
            set;
        }
        /// <summary>
        /// The URL where the user is taken after clicking the button. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The URL where the user is taken after clicking the button. ")]
        public string Href
        {
            get;
            set;
        }
        /// <summary>
        /// The ID passed with the form when it is submitted after the user clicks the button. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The ID passed with the form when it is submitted after the user clicks the button. ")]
        public string FormId
        {
            get;
            set;
        }
        /// <summary>
        /// This attribute is used for mock-AJAX with the dialog. See Mock AJAX  
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("This attribute is used for mock-AJAX with the dialog. See Mock AJAX ")]
        public string ClickRewriteUrl
        {
            get;
            set;
        }
        /// <summary>
        /// This attribute is used for mock-AJAX with the dialog. See Mock AJAX  
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("This attribute is used for mock-AJAX with the dialog. See Mock AJAX ")]
        public string ClickRewriteId
        {
            get;
            set;
        }
        /// <summary>
        /// This attribute is used for mock-AJAX with the dialog. See Mock AJAX  
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("This attribute is used for mock-AJAX with the dialog. See Mock AJAX ")]
        public string ClickRewriteForm
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_DIALOG_BUTTON; }
        }
        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (Type != DialogButtonType.None)
            {
                writer.AddAttribute("type", Type.ToString().ToLowerInvariant());
            }
            else
            {
                throw new MissingRequiredAttribute("Type", Type);
            }
            if (!string.IsNullOrEmpty(Value))
            {
                writer.AddAttribute("value", Value);
            }
            else
            {
                throw new MissingRequiredAttribute("Value", Value);
            }
            if(CloseDialog)
                writer.AddAttribute("close_dialog", FbmlConstants.TRUE);
            else
                writer.AddAttribute("close_dialog", FbmlConstants.FALSE);

            if (!string.IsNullOrEmpty(Href))
            {
                writer.AddAttribute("href", Href);
            }
            if (!string.IsNullOrEmpty(FormId))
            {
                writer.AddAttribute("form_id", FormId);
            }
            if (!string.IsNullOrEmpty(ClickRewriteUrl))
            {
                writer.AddAttribute("clickrewriteurl", ClickRewriteUrl);
            }
            if (!string.IsNullOrEmpty(ClickRewriteId))
            {
                writer.AddAttribute("clickrewriteid", ClickRewriteId);
            }
            if (!string.IsNullOrEmpty(ClickRewriteForm))
            {
                writer.AddAttribute("clickrewriteform", ClickRewriteForm);
            }
            base.AddAttributesToRender(writer);
        }
    }
}


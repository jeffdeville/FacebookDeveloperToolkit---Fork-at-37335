using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// The fb:dialog tag displays a lightbox-type dialog box when a user clicks on some element. The dialog box can then show some specified content or form. Clicking on the dialog buttons can post the form or use mock-AJAX to display the results back into the dialog with the use of fb:dialogresponse. 
    /// The fb:dialog tag must contain: 
    /// fb:dialog-content - the FBML contents of your dialog. You can now style this section of the dialog. 
    /// The fb:dialog tag may contain: 
    /// fb:dialog-title - which displays a title on your dialog 
    /// fb:dialog-button - which adds buttons to the bottom of your dialog 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:dialog" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Dialog runat=\"server\" />")]
    public class Dialog : ContentDisplayControl
    {
        /// <summary>
        /// The unique identifier for your dialog, which is used to invoke your dialog.
        /// </summary>
        [FbmlRequired(IsRequired=true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The unique identifier for your dialog, which is used to invoke your dialog. ")]
        public string Identifier
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates whether to display a Cancel button to close the dialog. 
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("Indicates whether to display a Cancel button to close the dialog. ")]
        public bool CancelButton
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_DIALOG; }
        }
        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(Identifier))
                throw new MissingRequiredAttribute("Identifier", Identifier);

            writer.AddAttribute("id", Identifier);
            if(CancelButton)
                writer.AddAttribute("cancel_button", FbmlConstants.TRUE);


            base.AddAttributesToRender(writer);
        }
    }
}


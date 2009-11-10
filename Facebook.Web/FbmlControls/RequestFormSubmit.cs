using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Creates a button that submits an fb:request-form. Use this along with any combination of fb:multi-friend-input or fb:friend-selector tags inside an fb:request-form if you would like to have the user send a request or invitation. When this button is clicked, a confirmation dialog appears that allows the user to confirm the sending of the request or invitations. 
    /// You can also use a single user ID as the label for the button. This means that instead of using other elements in the fb:request-form, the button results in a request being sent to the user that you specify for the label. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:request-form-submit" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:RequestFormSubmit runat=\"server\" />")]
    public class RequestFormSubmit : FbmlControl
    {
        /// <summary>
        /// Set this to the user ID of a person you would like a request to be sent to. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Set this to the user ID of a person you would like a request to be sent to. ")]
        public long Uid
        {
            get;
            set;
        }
        /// <summary>
        /// If you are using uid, use this tag to specify the text you want to appear as the label for the button. The text must include "%n" or "%N" which gets replaced with the first name or full name for the user ID, respectively. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("If you are using uid, use this tag to specify the text you want to appear as the label for the button. The text must include \"%n\" or \"%N\" which gets replaced with the first name or full name for the user ID, respectively.")]
        public string Label
        {
            get;
            set;
        }        
        
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_REQUEST_FORM_SUBMIT; }
        }
        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (Uid>0)
                writer.AddAttribute("uid", Uid.ToString());
            if (!string.IsNullOrEmpty(Label))
                writer.AddAttribute("label", Label);

            base.AddAttributesToRender(writer);
        }
    }
}


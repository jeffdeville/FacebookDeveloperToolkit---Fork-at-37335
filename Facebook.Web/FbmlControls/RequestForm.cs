using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Creates a form that sends requests to the selected users. To send requests/invitations to multiple users, create one of these forms and include either an fb:multi-friend-selector or any combination of fb:multi-friend-input, fb:friend-selector and fb:request-form-submit. The form is submitted after the user confirms the sending of the request. 
    /// If you want to invite only one friend, without making the user select from a list of friends, use the fb:request-form-submit tag within the fb:request-form tag, and make sure to set the appropriate attributes (uid, label) on the submit button to indicate that you are sending an invite to only one friend. 
    /// In general, use fb:multi-friend-selector in a nearly full-page invitation interface where the user is intended to select a large number of people, and fb:friend-selector or fb:multi-friend-input in situations where the user is selecting a smaller number of users and you want to integrate it into the context of your own page. As a middle-ground alternative, you can use the fb:multi-friend-selector (condensed) for places where the user might select a medium-sized list of people without needing a full-page interstitial invitations interface. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:request-form" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:RequestForm runat=\"server\" />")]
    public class RequestForm : ContentDisplayControl
    {
        /// <summary>
        /// The type of request or invitation to generate. This corresponds to the word that is displayed on the home page. For example, "event." 
        /// </summary>
        [FbmlRequired(IsRequired=true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The type of request or invitation to generate. This corresponds to the word that is displayed on the home page. For example, \"event.\" ")]
        public string Type
        {
            get;
            set;
        }
        /// <summary>
        /// The contents of the request or invitation to be sent. It should use FBML formatting that contains only links and the special tag &lt;fb:req-choice url="" label="" /&gt; to specify the buttons to be included in the request.
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The contents of the request or invitation to be sent. It should use FBML formatting that contains only links and the special tag <fb:req-choice url=\"\" label=\"\" /> to specify the buttons to be included in the request.")]
        public string Content
        {
            get;
            set;
        }
        /// <summary>
        /// Set this to true if you want to send an invitation or false if you want to send a request. The difference between them is in the content that the user sees. (Default value is false.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Set this to true if you want to send an invitation or false if you want to send a request. The difference between them is in the content that the user sees. (Default value is false.) ")]
        public bool Invite
        {
            get;
            set;
        }
        /// <summary>
        /// The place where a user gets redirected after submitting the form through the fb:request-form-submit button or when they click Skip this Step. By default the user is directed to http://apps.facebook.com/yourapp/null
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The place where a user gets redirected after submitting the form through the fb:request-form-submit button or when they click Skip this Step. By default the user is directed to http://apps.facebook.com/yourapp/null")]
        public string Action
        {
            get;
            set;
        }
        private HttpMethod _method = HttpMethod.None;

        /// <summary>
        /// Set it to either GET or POST, as you would with a form.   
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Set it to either GET or POST, as you would with a form.   ")]
        [DefaultValue(HttpMethod.None)]
        public HttpMethod Method
        {
            get { return _method; }
            set
            {
                if (!Enum.IsDefined(typeof(HttpMethod), value))
                    throw new InvalidEnumArgumentException("Method", (int)value, typeof(HttpMethod));
                _method = value;
            }
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_REQUEST_FORM; }
        }
        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(Type))
                throw new MissingRequiredAttribute("Type", Type);

            writer.AddAttribute("type", Type);
            if (string.IsNullOrEmpty(Content))
                throw new MissingRequiredAttribute("Content", Content);

            writer.AddAttribute("Content", Content, true);

            if (!string.IsNullOrEmpty(Action))
                writer.AddAttribute("action", Action);
            if (Method != HttpMethod.None)
                writer.AddAttribute("method", Method.ToString().ToUpperInvariant());

            base.AddAttributesToRender(writer);
        }
    }
}


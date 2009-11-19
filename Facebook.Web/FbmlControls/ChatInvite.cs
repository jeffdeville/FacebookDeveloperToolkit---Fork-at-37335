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
    /// Enables your users to initiate Facebook Chat with their friends from within your applications. 
    /// This tag renders a list of the current user's friends on your canvas page. When the user selects a friend, a Facebook Chat window opens, and can display a pre-populated message in the text field. This way your users can interact in real time with each other, whether to play a game with each other or to collaborate with a productivity tool. 
    /// It's best that you make the message concise, and if you include a URL, keep it as short as possible. 
        /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:chat-invite" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:ChatInvite runat=\"server\" />")]
    public class ChatInvite : FbmlControl
    {
        /// <summary>
        /// The default message that gets sent to the friend along with the chat invite. You can use plain text only here. However, if you include a URL, it gets formatted properly as a hyperlink.
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The default message that gets sent to the friend along with the chat invite. You can use plain text only here. However, if you include a URL, it gets formatted properly as a hyperlink.")]
        public string Msg
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates whether to display the user's profile pic and name, or just the user's name. (Default value is false.)   
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates whether to display the user's profile pic and name, or just the user's name. (Default value is false.) ")]
        [DefaultValue(false)]
        public bool Condensed
        {
            get;
            set;
        }
        /// <summary>
        /// A comma-separated list of user IDs to exclude from the chat invite (like you can do with fb:multi-friend-selector). 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("A comma-separated list of user IDs to exclude from the chat invite (like you can do with fb:multi-friend-selector). ")]
        [DefaultValue(false)]
        public string ExcludeIds
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_CHAT_INVITE; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if(string.IsNullOrEmpty(Msg))
                throw new MissingRequiredAttribute("Msg", Msg);

            writer.AddAttribute("msg", Msg);

            if(!string.IsNullOrEmpty(ExcludeIds))
                writer.AddAttribute("exclude_ids", ExcludeIds);
            if (Condensed)
                writer.AddAttribute("condensed", FbmlConstants.FALSE);

            base.AddAttributesToRender(writer);
        }
    }
}

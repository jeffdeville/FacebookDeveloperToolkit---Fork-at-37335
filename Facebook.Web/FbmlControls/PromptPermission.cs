using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders the content of the tag as a link that, when clicked, initiates a dialog requesting the specified extended permissions from the user. You can prompt the user for a series of permissions. 
    /// If the user has already granted a permission, a dialog for that permission does not appear. If the user has not already authorized the application before clicking the link, he or she is prompted to authorize it before being prompted for the permission. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:prompt-permission" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:PromptPermission runat=\"server\" />")]
    public class PromptPermission : ContentDisplayControl
    {
        /// <summary>
        /// A comma-separated string representing the extended permissions being requested. Specify any of the following permissions: email, read_stream, publish_stream, offline_access, status_update, photo_upload, create_event, rsvp_event, sms, video_upload, create_note, share_item. 
        /// </summary>
        [FbmlRequired(IsRequired=true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("A comma-separated string representing the extended permissions being requested. Specify any of the following permissions: email, read_stream, publish_stream, offline_access, status_update, photo_upload, create_event, rsvp_event, sms, video_upload, create_note, share_item.  ")]
        public string Perms
        {
            get;
            set;
        }
        /// <summary>
        /// The FBJS that will be called if the user grants the requested permission.
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The FBJS that will be called if the user grants the requested permission.")]
        public string NextFbjs
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_PROMPT_PERMISSION; }
        }
        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(Perms))
                throw new MissingRequiredAttribute("Perms", Perms);

            writer.AddAttribute("perms", Perms);

            if (!string.IsNullOrEmpty(NextFbjs))
                writer.AddAttribute("next_fbjs", NextFbjs);

            base.AddAttributesToRender(writer);
        }
    }
}


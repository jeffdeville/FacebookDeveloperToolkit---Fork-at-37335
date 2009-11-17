using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders a standard media header, intended mainly for displaying content contributed by a particular user. The media header is shown at the top of See All pages throughout Facebook. It contains a photo of the media owner and links to actions on that user. 
    /// The links shown to the content owners must be specified using fb:owner-action tags. Links to non-owners are always Profile, Send a Message and Poke, subject to standard privacy controls. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:mediaheader" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:MediaHeader runat=\"server\" />")]
    public class MediaHeader : ContentDisplayControl
    {
        /// <summary>
        /// The user ID of the content owner. 
        /// </summary>
        [FbmlRequired(IsRequired=true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The user ID of the content owner. ")]
        public long Uid
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_MEDIAHEADER; }
        }
        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (Uid == 0)
                throw new MissingRequiredAttribute("Uid", Uid);

            writer.AddAttribute("uid", Uid.ToString());

            base.AddAttributesToRender(writer);
        }
    }
}


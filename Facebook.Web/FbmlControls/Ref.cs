using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Facebook.Web;
using System.ComponentModel;
using System.Globalization;
using Facebook.Utility;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Fetches and renders FBML from a given ref source – either a ref string "handle" you've created using fbml.setRefHandle or a URL that serves FBML. You can use this ref to publish identical FBML to a large number of user profiles and subsequently update those profiles, without having to republish FBML on behalf of each user (that is, using profile.setFBML for each user). For a high level discussion of the benefits of fb:ref and how to use it, read this forum post 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:ref" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Ref runat=\"server\" />")]
    public class Ref : FbmlControl
    {
        /// <summary>
        /// The URL from which to fetch the FBML. Facebook caches the content retrieved from that URL until you call fbml.refreshRefUrl. You must specify either url or handle, but not both.
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The URL from which to fetch the FBML. Facebook caches the content retrieved from that URL until you call fbml.refreshRefUrl. You must specify either url or handle, but not both.")]
        public string Url
        {
            get;
            set;
        }
        /// <summary>
        /// The string previously set by fbml.setRefHandle that identifies the FBML. You must specify either url or handle, but not both. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The URL where you are redirecting the browser. Note this does not work within a profile box")]
        public string Handle
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_REF; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if(string.IsNullOrEmpty(Url) && string.IsNullOrEmpty(Handle))
                throw new FacebookException("FbmlControls.Ref Error: Either Url or Handle is required");
            if (!string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(Handle))
                throw new FacebookException("FbmlControls.Ref Error: Either Url or Handle is required not both");

            if (!string.IsNullOrEmpty(Url))
                writer.AddAttribute("url", Url);
            if (!string.IsNullOrEmpty(Handle))
                writer.AddAttribute("handle", Handle);
            base.AddAttributesToRender(writer);
        }
    }
}

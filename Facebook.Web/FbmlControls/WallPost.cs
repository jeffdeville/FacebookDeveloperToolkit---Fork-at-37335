using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders a Wall-style post. You should use it inside fb:wall tags, but it renders fine without them. It can also contain an fb:wallpost-action tag, which places a link at the bottom of the post. 
    /// Names and profile links follow standard Facebook privacy rules for other users viewing the Wall posts. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:wallpost" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:WallPost runat=\"server\" />")]
    public class WallPost : ContentDisplayControl
    {
        /// <summary>
        /// The user ID of the author of the post. FBML cannot parse without it, resulting in a runtime error. If the user ID is invalid (for example, the account has been deleted) then an image of a question mark and no name appear. 
        /// </summary>
        [FbmlRequired(IsRequired=true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The user ID of the author of the post. FBML cannot parse without it, resulting in a runtime error. If the user ID is invalid (for example, the account has been deleted) then an image of a question mark and no name appear. ")]
        public string Uid
        {
            get;
            set;
        }
        /// <summary>
        /// The current time, which is displayed in epoch seconds. (Default value is empty, no time/date is displayed with the Wall post.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The current time, which is displayed in epoch seconds. (Default value is empty, no time/date is displayed with the Wall post.) ")]
        public long T
        {
            get;
            set;
        }
        /// <summary>
        /// Alternate text to display if the logged in user cannot access the user specified. To specify an empty string instead of the default, use ifcantsee="". (Default value is Facebook User.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Alternate text to display if the logged in user cannot access the user specified. To specify an empty string instead of the default, use ifcantsee=\"\". (Default value is Facebook User.) ")]
        public string IfCantSee
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_WALLPOST; }
        }
        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(Uid))
                throw new MissingRequiredAttribute("Uid", Uid);

            writer.AddAttribute("uid", Uid);
            if(T>0)
                writer.AddAttribute("t", T.ToString());
            if (!string.IsNullOrEmpty(IfCantSee))
                writer.AddAttribute("ifcantsee", IfCantSee);

            base.AddAttributesToRender(writer);
        }
    }
}


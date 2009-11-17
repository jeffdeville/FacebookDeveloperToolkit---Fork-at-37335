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
    /// Returns the status of the user specified by uid. If there is a URL in the status that starts with either http or https, it can be formatted as a hyperlink. 
    /// If the viewing user doesn't have permission to see the user's status, an empty string is returned. 
    /// Note: This tag only returns the status, not the user's name, so it might be more useful to display the user's name along with the status.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:user-status" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:UserStatus runat=\"server\" />")]
    public class UserStatus : FbmlControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserStatus() { Linked = true; }
        /// <summary>
        /// The user ID of the user whose status you want to retrieve. 
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The user ID of the user whose status you want to retrieve.")]
        [DefaultValue(0)]
        public long Uid
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates whether URLs in the status should be formatted as hyperlinks. (Default value is true.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates whether URLs in the status should be formatted as hyperlinks. (Default value is true.) ")]
        [DefaultValue(true)]
        public bool Linked { get; set; }


        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_USER_STATUS; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (Uid == 0)
                throw new MissingRequiredAttribute("Uid", Uid);

            writer.AddAttribute("uid", Uid.ToString(CultureInfo.InvariantCulture));

            if (!Linked)
                writer.AddAttribute("linked", FbmlConstants.FALSE);

            base.AddAttributesToRender(writer);
        }
    }
}

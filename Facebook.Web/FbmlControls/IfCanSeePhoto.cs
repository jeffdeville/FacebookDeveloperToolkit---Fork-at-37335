using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using Facebook.Web;
using System.Globalization;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Displays the enclosed content only if the logged in user can see the photo specified. 
    /// You should use this tag when specifying content that should only be shown when using fb:photo.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook Wiki documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:if-can-see-photo" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:IfCanSeePhoto runat=\"server\"></{0}:IfCanSeePhoto>")]
    [DefaultProperty("Pid")]
    public class IfCanSeePhoto : ConditionalControl
    {
        /// <summary>
        /// Gets or sets the ID of the photo to check.
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("Specifies the photo ID to check.")]
        [DefaultValue("0")]
        [FbmlRequired(IsRequired = true)]
        public string Pid
        {
            get;
            set;
        }

        /// <summary>
        /// If pid is not an API-supplied pid, this should be the id parameter in the query string used to find the pid. 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("If pid is not an API-supplied pid, this should be the id parameter in the query string used to find the pid. ")]
        [DefaultValue(0)]
        public long Uid
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (Pid == "0")
                throw new MissingRequiredAttribute("Pid", Pid);
            writer.AddAttribute("pid", Pid);

            if (Uid != 0)
                writer.AddAttribute("uid", Uid.ToString(CultureInfo.InvariantCulture));
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_IF_CAN_SEE_PHOTO; }
        }
    }
}

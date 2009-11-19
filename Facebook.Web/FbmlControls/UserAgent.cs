using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Displays the contents wrapped inside the tag to the specified user-agents. You can use fb:user-agent on the canvas page and the profile box. It serves as a tool to deal with browser idiosyncrasies.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:user-agent" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:UserAgent runat=\"server\" />")]
    public class UserAgent : ContentDisplayControl
    {
        /// <summary>
        /// A comma-delimited list of strings to test for inclusion against the HTTP request's user-agent string. If a given include string matches the user-agent string, the content inside the include tag appears; otherwise it does not. 
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("A comma-delimited list of strings to test for inclusion against the HTTP request's user-agent string. If a given include string matches the user-agent string, the content inside the include tag appears; otherwise it does not. ")]
        public string Includes
        {
            get;
            set;
        }
        /// <summary>
        /// A comma-delimited list of strings to test for exclusion against the HTTP request's user-agent string. If a given include string matches the user-agent string, the content inside the include tag does not appear; otherwise it does appear. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("A comma-delimited list of strings to test for exclusion against the HTTP request's user-agent string. If a given include string matches the user-agent string, the content inside the include tag does not appear; otherwise it does appear. ")]
        public string Excludes
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_USER_AGENT; }
        }
        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(Includes))
                throw new MissingRequiredAttribute("Includes", Includes);

            writer.AddAttribute("includes", Includes);


            if (!string.IsNullOrEmpty(Excludes))
                writer.AddAttribute("excludes", Excludes);

            base.AddAttributesToRender(writer);
        }
    }
}

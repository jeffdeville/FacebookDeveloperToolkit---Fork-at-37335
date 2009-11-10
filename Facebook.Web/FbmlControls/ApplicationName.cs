using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders the application name. Use this tag to render the name of an application. 
    /// You can also use this tag in instances when you cannot use the application name directly. For example, to include the application name in a Mini-Feed or News Feed story when that application name contains the word "message". 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:application-name" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:ApplicationName runat=\"server\" />")]
    public class ApplicationName : FbmlControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationName() { Linked = true; }
        /// <summary>
        /// Indicates whether the application name is linked to the application's About page. When set to true, the name is linked; when set to false, the name is not linked. (Default value is true.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates whether the application name is linked to the application's About page. When set to true, the name is linked; when set to false, the name is not linked. (Default value is true.)  ")]
        [DefaultValue(true)]
        public bool Linked
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_APPLICATION_NAME; }
        }
        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (!Linked)
                writer.AddAttribute("linked", FbmlConstants.FALSE);
            base.AddAttributesToRender(writer);
        }
    }
}


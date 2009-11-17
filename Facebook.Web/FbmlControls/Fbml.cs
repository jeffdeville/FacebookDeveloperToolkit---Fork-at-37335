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
    /// This tag serves two purposes. You can use this tag to: 
    /// Define a block of FBML to be rendered in a specific version of FBML. 
    /// Define a namespace to use with Custom Tags. 
    /// When using this tag with custom tags, the xmlns attribute defines a namespace for the custom tags registered by an application. Adding an xmlns attribute effectively imports the custom tags from an application into a namespace of your choice within the scope of the fb:fbml tag. 
    /// The value of the xmlns attribute must be a URL of the form "http://external.facebook.com/apps/&lt;app_name&gt;". The &lt;app_name&gt; must match the name of the registering application's name in the application's canvas URL (that is, "http://apps.facebook.com/&lt;app_name&gt;"). 
    /// Inside fb:fbml, you can use any custom tags you registered for your application using fbml.registerCustomTags by prefixing those tags with the namespace you selected (see Examples below). 
    /// An fb:fbml tag may have more than one xmlns property, each of which defines a different namespace. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:fbml" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Fbml runat=\"server\" />")]
    public class Fbml : ContentDisplayControl
    {
        /// <summary>
        /// The version of FBML with which to render the content. (Default value is [the current version].)   
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The version of FBML with which to render the content. (Default value is [the current version].) ")]
        [DefaultValue(0)]
        public decimal Version
        {
            get;
            set;
        }
        /// <summary>
        /// The name of the custom tag namespace. You cannot create a namespace that starts with fb or f8. You can include alphanumeric characters, underscores ("_"), and hyphens ("-"). Note: You can include multiple xmlns attributes in one fb:fbml tag.    
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The name of the custom tag namespace. You cannot create a namespace that starts with fb or f8. You can include alphanumeric characters, underscores (\"_\"), and hyphens (\"-\"). Note: You can include multiple xmlns attributes in one fb:fbml tag. ")]
        [DefaultValue(null)]
        public string XmlNs
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_FBML; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(XmlNs))
                writer.AddAttribute("xmlns", XmlNs);
            if (Version > 0)
                writer.AddAttribute("version", Version.ToString());
            base.AddAttributesToRender(writer);
        }
    }
}

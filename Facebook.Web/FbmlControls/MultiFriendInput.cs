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
    /// Renders a multi-friend form entry field like the one used in the message composer. You can use the field inside an fb:request-form to select users for whom a request can be sent. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:multi-friend-input" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:MultiFriendInput runat=\"server\" />")]
    public class MultiFriendInput : FbmlControl
    {
        /// <summary>
        /// The width of entry field. (Default value is 350px.)  
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The width of entry field. (Default value is 350px.) ")]
        [DefaultValue(0)]
        public int Width
        {
            get;
            set;
        }

        /// <summary>
        /// The color of entry field border. (Default value is #8496ba.)     
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The color of entry field border. (Default value is #8496ba.) ")]
        [DefaultValue(null)]
        public string BorderColor
        {
            get;
            set;
        }


        /// <summary>
        /// Indicates whether or not to include the logged in user in the suggested options. (Default value is false.)     
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates whether or not to include the logged in user in the suggested options. (Default value is false.) ")]
        [DefaultValue(false)]
        public bool IncludeMe
        {
            get;
            set;
        }
        /// <summary>
        /// A list of user IDs to exclude from the selector. (comma-separated) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("A list of user IDs to exclude from the selector. (comma-separated) ")]
        [DefaultValue(null)]
        public string ExcludeIds
        {
            get;
            set;
        }
        /// <summary>
        /// The maximum number of people that can be selected. (Default value is 20.)   
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The maximum number of people that can be selected. (Default value is 20.) ")]
        [DefaultValue(0)]
        public int Max
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates whether or not to include friend lists in the suggested options. (Default value is false.)    
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates whether or not to include friend lists in the suggested options. (Default value is false.) ")]
        [DefaultValue(false)]
        public bool IncludeLists
        {
            get;
            set;
        }
        /// <summary>
        /// A comma separated list of user IDs to pre-populate in the selector. Note that this cannot be used inside an &lt;fb:request-form&gt;. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("A comma separated list of user IDs to pre-populate in the selector. Note that this cannot be used inside an <fb:request-form>. ")]
        [DefaultValue(null)]
        public string PrefillIds
        {
            get;
            set;
        }
        /// <summary>
        /// Set to true to prevent editing of the pre-populated IDs.  
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Set to true to prevent editing of the pre-populated IDs. ")]
        [DefaultValue(false)]
        public bool PrefillLocked
        {
            get;
            set;
        }
        /// <summary>
        /// The name that should be given to the array of selected user IDs. This defaults to ids for the first &lt;fb:multi-friend-input&gt;, and to fb_multi_friend_input_ids&lt;n&gt; for all by the first. In general, you should include name attributes if you include more than one &lt;fb:multi-friend-input&gt; in a single page.
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The name that should be given to the array of selected user IDs. This defaults to ids for the first <fb:multi-friend-input>, and to fb_multi_friend_input_ids<n> for all by the first. In general, you should include name attributes if you include more than one <fb:multi-friend-input> in a single page.")]
        [DefaultValue(null)]
        public string Name
        {
            get;
            set;
        }        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_MULTI_FRIEND_INPUT; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if(Width > 0)
                writer.AddAttribute("width", Width.ToString());
            if (!string.IsNullOrEmpty(BorderColor))
                writer.AddAttribute("bordercolor", BorderColor);
            if (IncludeMe)
                writer.AddAttribute("include_me", FbmlConstants.TRUE);
            if (Max > 0)
                writer.AddAttribute("max", Max.ToString());
            if (!string.IsNullOrEmpty(ExcludeIds))
                writer.AddAttribute("exclude_ids", ExcludeIds);
            if (!string.IsNullOrEmpty(PrefillIds))
                writer.AddAttribute("prefill_ids", PrefillIds);
            if (PrefillLocked)
                writer.AddAttribute("prefill_locked", FbmlConstants.TRUE);
            if (!string.IsNullOrEmpty(Name))
                writer.AddAttribute("name", Name);

            base.AddAttributesToRender(writer);
        }
    }
}

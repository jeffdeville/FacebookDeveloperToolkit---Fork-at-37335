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
    /// Renders a predictive friend selector input for a given person. You can use this tag inside an fb:request-form to select users for whom a request can be sent. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:friend-selector" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:FriendSelector runat=\"server\" />")]
    public class FriendSelector : FbmlControl
    {
        /// <summary>
        /// The user whose friends you can select. (Default value is the uid of the currently logged-in user.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The user whose friends you can select. (Default value is the uid of the currently logged-in user.) ")]
        [DefaultValue(0)]
        public long Uid
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the form element. (Default value is friend_selector_name.)    
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The name of the form element. (Default value is friend_selector_name.) ")]
        [DefaultValue(null)]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the hidden form element that contains the user ID of the selected friend. If you are using this tag inside fb:request-form, do not override the default. (Default value is friend_selector_id.)    
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The name of the hidden form element that contains the user ID of the selected friend. If you are using this tag inside fb:request-form, do not override the default. (Default value is friend_selector_id.) ")]
        [DefaultValue(null)]
        public string IdName
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
        /// A single user ID to pre-populate in the selector. If the viewing user cannot see the prefilled user's name due to privacy, then this parameter will be ignored. Note that this cannot be used inside an &lt;fb:request-form&gt;. (Default value is null.)     
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("A single user ID to pre-populate in the selector. If the viewing user cannot see the prefilled user's name due to privacy, then this parameter will be ignored. Note that this cannot be used inside an <fb:request-form>. (Default value is null.) ")]
        [DefaultValue(0)]
        public long PrefillId
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_FRIEND_SELECTOR; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {

            if(Uid>0)
                writer.AddAttribute("uid", Uid.ToString());
            if (!string.IsNullOrEmpty(Name))
                writer.AddAttribute("name", Name);
            if (IncludeMe)
                writer.AddAttribute("include_me", FbmlConstants.TRUE);
            if (IncludeLists)
                writer.AddAttribute("include_lists", FbmlConstants.TRUE);
            if (PrefillId > 0)
                writer.AddAttribute("prefill_id", PrefillId.ToString());
            if (!string.IsNullOrEmpty(ExcludeIds))
                writer.AddAttribute("exclude_ids", ExcludeIds);

            base.AddAttributesToRender(writer);
        }
    }
}

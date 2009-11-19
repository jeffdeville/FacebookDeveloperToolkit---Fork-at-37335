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
    /// There are actually two versions of this button - the full version and the condensed version. This page describes the full version. For information about the condensed version, see fb:multi-friend-selector (condensed). 
    /// This is a nearly full-page interface intended to be used on canvas pages to allow the user to send a "large" number of requests or invitations (where "large" is generally some number more than 4). This tag must be used inside an fb:request-form tag. This interface includes a series of &lt;input type="hidden" name="ids[]" value="[friend id]"&gt; which are included for selected users in the form that gets submitted to your &lt;fb:request-form&gt; action URL. Both the Skip this Step button and the Submit button take the user to the parent fb:request-form action URL. 
    /// Your users can invite their friends who aren't yet on Facebook by entering their email addresses in the text box at the bottom of the multi-friend selector. You need to use Express Registration in order to have those friends join Facebook and authorize your application at the same time. After the friend authorizes your application, it gets bookmarked automatically. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:multi-friend-selector" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:MultiFriendSelector runat=\"server\" />")]
    public class MultiFriendSelector : FbmlControl
    {
        /// <summary>
        /// An instructional message to display to users at the top of the multi-friend-selector.   
        /// </summary>
        [FbmlRequired(IsRequired=true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("An instructional message to display to users at the top of the multi-friend-selector. ")]
        [DefaultValue(null)]
        public string ActionText
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates whether you want a border around the outside of the multi-friend-selector.   
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates whether you want a border around the outside of the multi-friend-selector. ")]
        [DefaultValue(false)]
        public bool ShowBorder
        {
            get;
            set;
        }
        /// <summary>
        /// The number of rows of friends to show in the multi-friend-selector. (Default value is 5 and the value must be between 3 and 10.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The number of rows of friends to show in the multi-friend-selector. (Default value is 5 and the value must be between 3 and 10.) ")]
        [DefaultValue(0)]
        public int Rows
        {
            get;
            set;
        }
        /// <summary>
        /// The maximum number of users that can be selected. This value ranges from 1 to 35, and is capped at the number of friend requests the user has remaining under their limit. This attribute is ignored if it is greater than the number of requests your application is able to send.
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The maximum number of users that can be selected. This value ranges from 1 to 35, and is capped at the number of friend requests the user has remaining under their limit. This attribute is ignored if it is greater than the number of requests your application is able to send.")]
        [DefaultValue(0)]
        public int Max
        {
            get;
            set;
        }
        /// <summary>
        /// A comma-separated list of user IDs to exclude from the multi-friend-selector. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("A comma-separated list of user IDs to exclude from the multi-friend-selector. ")]
        [DefaultValue(null)]
        public string ExcludeIds
        {
            get;
            set;
        }
        private Bypass _bypass = Bypass.Skip;

        /// <summary>
        /// The size of the image to display. (Default value is thumb.). Other valid values are thumb (t) (50px wide), small (s) (100px wide), normal (n) (200px wide), and square (q) (50px by 50px). Or, you can specify width and height settings instead, just like an img tag. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The size of the image to display. (Default value is thumb.). Other valid values are thumb (t) (50px wide), small (s) (100px wide), normal (n) (200px wide), and square (q) (50px by 50px). Or, you can specify width and height settings instead, just like an img tag. ")]
        [DefaultValue(Bypass.Skip)]
        public Bypass Bypass
        {
            get { return _bypass; }
            set
            {
                if (!Enum.IsDefined(typeof(Bypass), value))
                    throw new InvalidEnumArgumentException("Bypass", (int)value, typeof(Bypass));
                _bypass = value;
            }
        }
        /// <summary>
        /// Indicates whether you want to display an email invite section in the multi-friend-selector. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates whether you want to display an email invite section in the multi-friend-selector. ")]
        [DefaultValue(false)]
        public bool EmailInvite
        {
            get;
            set;
        }
        /// <summary>
        /// If we should render the regular or condensed multi-friend-selector
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("If we should render the regular or condensed multi-friend-selector")]
        [DefaultValue(false)]
        public bool IsCondensed
        {
            get;
            set;
        }
        /// <summary>
        /// The number of columns of friends to show in the multi-friend selector, which also determines the width of the invite box. The value must be one of: 2 (which sets the box to 368px wide), 3 (which sets it to 466px wide), or 5 (which sets it to 740px wide). (Default value is 5.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The number of columns of friends to show in the multi-friend selector, which also determines the width of the invite box. The value must be one of: 2 (which sets the box to 368px wide), 3 (which sets it to 466px wide), or 5 (which sets it to 740px wide). (Default value is 5.) ")]
        [DefaultValue(0)]
        public int Cols
        {
            get;
            set;
        }
        /// <summary>
        /// The number of rows of friends to display in the unselected part of the condensed multi-friend-selector. (Default value is 6, and must be a number between 4 and 15.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The number of rows of friends to display in the unselected part of the condensed multi-friend-selector. (Default value is 6, and must be a number between 4 and 15.) ")]
        [DefaultValue(0)]
        public int UnselectedRows
        {
            get;
            set;
        }
        /// <summary>
        /// The number of rows of friends to display in the selected part of the condensed multi-friend-selector. (Default value is 5, and must be a number between 5 and 15; or set it to 0 to indicate that you want only a single box for both selected and unselected friends.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The number of rows of friends to display in the selected part of the condensed multi-friend-selector. (Default value is 5, and must be a number between 5 and 15; or set it to 0 to indicate that you want only a single box for both selected and unselected friends.)")] 
        [DefaultValue(0)]
        public int SelectedRows
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_MULTI_FRIEND_SELECTOR; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {

            if (!IsCondensed)
            {
                if (string.IsNullOrEmpty(ActionText))
                    throw new MissingRequiredAttribute("ActionText", ActionText);
                else
                    writer.AddAttribute("actiontext", ActionText);

                if (ShowBorder)
                    writer.AddAttribute("showborder", FbmlConstants.TRUE);
                if (EmailInvite)
                    writer.AddAttribute("email_invite", FbmlConstants.TRUE);
                if (Rows > 0)
                    writer.AddAttribute("rows", Rows.ToString());
                if (Cols > 0)
                    writer.AddAttribute("cols", Cols.ToString());
                if (Bypass != Bypass.Skip)
                    writer.AddAttribute("bypass", Bypass.ToString().ToLowerInvariant());
            }
            else
            {
                writer.AddAttribute("condensed", FbmlConstants.TRUE);
                if (UnselectedRows > 0)
                    writer.AddAttribute("unselected_rows", UnselectedRows.ToString());
                if (SelectedRows > 0)
                    writer.AddAttribute("selected_rows", SelectedRows.ToString());
            }
            if (Max > 0)
                writer.AddAttribute("max", Max.ToString());
            if (!string.IsNullOrEmpty(ExcludeIds))
                writer.AddAttribute("exclude_ids", ExcludeIds);

            base.AddAttributesToRender(writer);
        }
    }
}

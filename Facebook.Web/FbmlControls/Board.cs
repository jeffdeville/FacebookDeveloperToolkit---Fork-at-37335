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
    /// Renders a link, usually for navigational purposes. Its appearance depends on its container. 
    /// The tag must be a child of either fb:dashboard or fb:subtitle. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:board" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Board runat=\"server\" />")]
    public class Board : TitleDisplayControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Board() { CanPost = true; CanCreateTopic = true; }
        /// <summary>
        /// The unique identifier for this board. The board name can contain alphanumeric characters (Aa-Zz, 0-9), hyphens (-) and underscores (_) only.
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The unique identifier for this board. The board name can contain alphanumeric characters (Aa-Zz, 0-9), hyphens (-) and underscores (_) only.")]
        public string Xid
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates whether the viewing user can post on this board. (Default value is true.)   
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates whether the viewing user can post on this board. (Default value is true.) ")]
        [DefaultValue(true)]
        public bool CanPost
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates whether the viewing user can delete any post or topic on this board. (Default value is false.)   
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates whether the viewing user can delete any post or topic on this board. (Default value is false.) ")]
        [DefaultValue(false)]
        public bool CanDelete
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates whether the viewing user can mark a post as relevant or irrelevant. (Default value is false.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates whether the viewing user can mark a post as relevant or irrelevant. (Default value is false.) ")]
        [DefaultValue(false)]
        public bool CanMark
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates whether the viewing user can create a topic on this board. (Default value is true.)    
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates whether the viewing user can create a topic on this board. (Default value is true.) ")]
        [DefaultValue(true)]
        public bool CanCreateTopic
        {
            get;
            set;
        }
        /// <summary>
        /// The maximum number of topics to show in the box. (Default value is 3.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The maximum number of topics to show in the box. (Default value is 3.) ")]
        [DefaultValue(0)]
        public int NumTopics
        {
            get;
            set;
        }
        /// <summary>
        /// The URL to refetch this configuration. (Default value is the current page.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The URL to refetch this configuration. (Default value is the current page.) ")]
        [DefaultValue(null)]
        public string CallbackUrl
        {
            get;
            set;
        }
        /// <summary>
        /// The URL where the user is returned after selecting a "back" link. (Default value is the current page.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The URL where the user is returned after selecting a \"back\" link. (Default value is the current page.) ")]
        [DefaultValue(null)]
        public string ReturnUrl
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_BOARD; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if(string.IsNullOrEmpty(Xid))
                throw new MissingRequiredAttribute("Xid", Xid);

            writer.AddAttribute("xid", Xid);

            if(!CanPost)
                writer.AddAttribute("canpost", FbmlConstants.FALSE);
            if (CanDelete)
                writer.AddAttribute("candelete", FbmlConstants.TRUE);
            if (CanMark)
                writer.AddAttribute("canmark", FbmlConstants.TRUE);
            if (!CanCreateTopic)
                writer.AddAttribute("cancreatetopic", FbmlConstants.FALSE);
            if (NumTopics>0)
                writer.AddAttribute("numtopics", NumTopics.ToString());


            if(!string.IsNullOrEmpty(CallbackUrl))
                writer.AddAttribute("callbackurl", CallbackUrl);
            if (!string.IsNullOrEmpty(ReturnUrl))
                writer.AddAttribute("returnurl", ReturnUrl);

            base.AddAttributesToRender(writer);
        }
    }
}

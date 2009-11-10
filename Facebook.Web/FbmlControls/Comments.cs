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
    /// Displays a set of comments for a unique identifier. Facebook handles posting, drawing, and see all page. 
    /// The fb:comments tag is essentially a Wall for developers to drop on canvas pages and application tabs on profiles easily. Using the tag implies a Wall-like comments set exists that can be posted or identified by the passed XID.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:comments" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Comments runat=\"server\" />")]
    public class Comments : TitleDisplayControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Comments() { CanPost = true; }
        /// <summary>
        /// The unique identifier for this set of comments. Comments can contain alphanumeric characters (Aa-Zz, 0-9), hyphens (-), percent (%), period (.), and underscores (_) (in effect, the result of any urlencode can be a valid XID). 
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The unique identifier for this set of comments. Comments can contain alphanumeric characters (Aa-Zz, 0-9), hyphens (-), percent (%), period (.), and underscores (_) (in effect, the result of any urlencode can be a valid XID). ")]
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
        /// <summary>
        /// Boolean whether to show the form (canpost "true" only) for inline posting. Posts using this form will not go to a see-all page after posting, but rather refresh the page. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Boolean whether to show the form (canpost \"true\" only) for inline posting. Posts using this form will not go to a see-all page after posting, but rather refresh the page. ")]
        [DefaultValue(false)]
        public bool ShowForm
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates whether to publish a Feed story when the comment gets posted. The comment must be at least 5 words in length in order to be published to Feed.
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates whether to publish a Feed story when the comment gets posted. The comment must be at least 5 words in length in order to be published to Feed.")]
        [DefaultValue(false)]
        public bool PublishFeed
        {
            get;
            set;
        }
        /// <summary>
        /// Removes the rounded box around the text area to allow greater customization.   
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Removes the rounded box around the text area to allow greater customization. ")]
        [DefaultValue(false)]
        public bool Simple
        {
            get;
            set;
        }
        /// <summary>
        /// Changes the order of comments and comment area to allow greater customization. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Changes the order of comments and comment area to allow greater customization.  ")]
        [DefaultValue(false)]
        public bool Reverse
        {
            get;
            set;
        }
        /// <summary>
        /// When true, ensures that posts to this board don't send any notifications. By default, they are automatically sent to the last n commenters, whether or not they are friends with the user currently posting the comment. (Default value is false.)   
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("When true, ensures that posts to this board don't send any notifications. By default, they are automatically sent to the last n commenters, whether or not they are friends with the user currently posting the comment. (Default value is false.) ")]
        [DefaultValue(false)]
        public bool Quiet
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_COMMENTS; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if(string.IsNullOrEmpty(Xid))
                throw new MissingRequiredAttribute("Xid", Xid);

            writer.AddAttribute("xid", Xid);
            if (NumTopics>0)
                writer.AddAttribute("numtopics", NumTopics.ToString());
            else
                throw new MissingRequiredAttribute("NumTopics", NumTopics);


            if(!CanPost)
                writer.AddAttribute("canpost", FbmlConstants.FALSE);
            else
                writer.AddAttribute("canpost", FbmlConstants.TRUE);
            if(!CanDelete)
                writer.AddAttribute("candelete", FbmlConstants.FALSE);
            else
                writer.AddAttribute("candelete", FbmlConstants.TRUE);


            if(!string.IsNullOrEmpty(CallbackUrl))
                writer.AddAttribute("callbackurl", CallbackUrl);
            if (!string.IsNullOrEmpty(ReturnUrl))
                writer.AddAttribute("returnurl", ReturnUrl);

            if(CanPost && !ShowForm)
                writer.AddAttribute("showform", FbmlConstants.FALSE);
            else if(CanPost)
                writer.AddAttribute("showform", FbmlConstants.TRUE);
            if(!PublishFeed)
                writer.AddAttribute("publishfeed", FbmlConstants.FALSE);
            else
                writer.AddAttribute("publishfeed", FbmlConstants.TRUE);
            if(!Simple)
                writer.AddAttribute("simple", FbmlConstants.FALSE);
            else
                writer.AddAttribute("simple", FbmlConstants.TRUE);
            if(!Reverse)
                writer.AddAttribute("reverse", FbmlConstants.FALSE);
            else
                writer.AddAttribute("reverse", FbmlConstants.TRUE);
            if(!Quiet)
                writer.AddAttribute("quiet", FbmlConstants.FALSE);
            else
                writer.AddAttribute("quiet", FbmlConstants.TRUE);

            base.AddAttributesToRender(writer);
        }
    }
}

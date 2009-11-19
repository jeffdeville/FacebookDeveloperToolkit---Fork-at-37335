using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Facebook.Web;
using System.Web.UI;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Use this tag to render a Live Stream Box social widget on your FBML canvas pages or Facebook Connect sites. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook Wiki documentation for this control can be found
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:live-stream" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:LiveStream runat=\"server\" />")]
    public class LiveStream : FbmlControl
    {

        /// <summary>
        /// This is the application ID from the application you just created above. You must specify either the application ID or the API key.
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("This is the application ID from the application you just created above. You must specify either the application ID or the API key.")]
        [DefaultValue(false)]
        public string EventAppId
        {
            get;
            set;
        }
        /// <summary>
        /// The API key from the application you just created above. You must specify either the application ID or the API key. 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The API key from the application you just created above. You must specify either the application ID or the API key. ")]
        [DefaultValue(false)]
        public string ApiKey
        {
            get;
            set;
        }
        /// <summary>
        /// The width of the box in pixels. (Default value is 450 pixels.) 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The width of the box in pixels. (Default value is 450 pixels.) ")]
        [DefaultValue(0)]
        public int Width
        {
            get;
            set;
        }
        /// <summary>
        /// The height of the box in pixels. (Default value is 400 pixels.) 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The height of the box in pixels. (Default value is 400 pixels.)  ")]
        [DefaultValue(0)]
        public int Height
        {
            get;
            set;
        }
        /// <summary>
        /// If you want to have multiple Live Stream Boxes on your canvas pages, specify a unique XID for each box. Specify xid=EVENT_NAME, where EVENT_NAME represents the event. EVENT_NAME can include only numbers, letters, and underscores. (Default value is default.) 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("If you want to have multiple Live Stream Boxes on your canvas pages, specify a unique XID for each box. Specify xid=EVENT_NAME, where EVENT_NAME represents the event. EVENT_NAME can include only numbers, letters, and underscores. (Default value is default.) ")]
        [DefaultValue(false)]
        public string Xid
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
 
            if (!string.IsNullOrEmpty(EventAppId))
                writer.AddAttribute("event_app_id", EventAppId);
            if (!string.IsNullOrEmpty(ApiKey))
                writer.AddAttribute("apikey", ApiKey);
            if (Width > 0)
                writer.AddAttribute("width", Width.ToString());
            if (Height > 0)
                writer.AddAttribute("height", Height.ToString());
            if (!string.IsNullOrEmpty(Xid))
                writer.AddAttribute("xid", Xid);
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_LIVE_STREAM; }
        }
    }
}

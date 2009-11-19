using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Facebook.Web;
using System.Web.UI;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Inserts an &lt;iframe&gt; tag into an application canvas page; you cannot use the &lt;fb:iframe&gt; tag on the profile page (that is, application tabs and profile boxes). You cannot use FBML inside an iframe; use XFBML tags instead. 
    /// The conventional &lt;iframe&gt; tag has been re-created in FBML and became &lt;fb:iframe&gt;. You edit the attributes for &lt;fb:iframe&gt; the same way you would for HTML tags. 
    /// If you aren't requiring a session secret to be passed, you must use your server's Src as the src for your iframe. Otherwise, apps.facebook.com wraps your page with the Facebook layout. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook Wiki documentation for this control can be found
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:iframe" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:IFrame runat=\"server\" />")]
    public class IFrame : FbmlControl
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public IFrame() { FrameBorder = true; IncludeFbSig = true; }

        /// <summary>
        /// Gets or sets the Src displayed by this iframe.  This property is required.
        /// </summary>
        [Browsable(true)]
        [Description("Specifies the Src that should be loaded into this iframe.")]
        [DefaultValue(null)]
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        public string Src
        {
            get;
            set;
        }

        /// <summary>
        /// This parameter smartly sizes the iframe to fit the remaining space on the page and disables the outer scrollbars. If you include more than one smartsizing iframe, they automatically distribute the size appropriately. (Default value is false.) 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("This parameter smartly sizes the iframe to fit the remaining space on the page and disables the outer scrollbars. If you include more than one smartsizing iframe, they automatically distribute the size appropriately. (Default value is false.) ")]
        [DefaultValue(false)]
        public bool SmartSize
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates whether to show (1) or hide (0) an iframe border. (Default value is 1.) 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates whether to show (1) or hide (0) an iframe border. (Default value is 1.) ")]
        [DefaultValue(true)]
        public bool FrameBorder
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates whether to show scrollbars. (Default value is yes.) - use "yes", "no", or "auto" (not "true" or "false") 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates whether to show scrollbars. (Default value is yes.) - use \"yes\", \"no\", or \"auto\" (not \"true\" or \"false\") ")]
        [DefaultValue(IframeScrollMode.Enabled)]
        public IframeScrollMode Scrolling
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates a custom inline style for the iframe. 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates a custom inline style for the iframe. ")]
        public string Style
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates the width of the iframe.  
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates the width of the iframe. ")]
        [DefaultValue(0)]
        public int Width
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates the height of the iframe.  
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates the height of the iframe. ")]
        [DefaultValue(0)]
        public int Height
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether the iframe should be resizable.
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Specifies whether the iframe should be resizable.")]
        [DefaultValue(false)]
        public bool Resizable
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether Facebook should attempt to size the frame by its content size.
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Specifies whether the iframe should be sized according to its content by Facebook.")]
        [DefaultValue(false)]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether Facebook should attempt to size the frame by its content size.
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Specifies whether the iframe should be sized according to its content by Facebook.")]
        [DefaultValue(true)]
        public bool IncludeFbSig
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (string.IsNullOrEmpty(Src))
                throw new MissingRequiredAttribute("Src", Src);
            else
                writer.AddAttribute("src", Src);

            if (SmartSize)
                writer.AddAttribute("smartsize", "true");

            if (!FrameBorder)
                writer.AddAttribute("frameborder", "0");
            if (!IncludeFbSig)
                writer.AddAttribute("include_fb_sig", FbmlConstants.FALSE);
            if (!string.IsNullOrEmpty(Style))
                writer.AddAttribute("style", Style);
            if (Width>0)
                writer.AddAttribute("width", Width.ToString());
            if (Height>0)
                writer.AddAttribute("height", Height.ToString());

            switch (Scrolling)
            {
                case IframeScrollMode.Auto:
                    writer.AddAttribute("scrolling", "auto");
                    break;
                case IframeScrollMode.Disabled:
                    writer.AddAttribute("scrolling", "no");
                    break;
            }
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_IFRAME; }
        }
    }
}

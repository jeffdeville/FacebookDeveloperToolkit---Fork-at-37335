using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Facebook.Web;
using System.Web.UI;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders a Shockwave Flash (SWF) object. On profile pages, an image appears first. When the user clicks the image, it turns into the Flash object. On canvas pages, the image is ignored, and the Flash object is directly included. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook Wiki documentation for this control can be found
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:swf" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Swf runat=\"server\" />")]
    public class Swf : FbmlControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Swf() { WaitForClick = true; }
        /// <summary>
        /// The URL of the Flash object. The URL must be absolute.
        /// </summary>
        [Browsable(true)]
        [Description("The URL of the Flash object. The URL must be absolute.")]
        [DefaultValue(null)]
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        public string SwfSrc
        {
            get;
            set;
        }

        /// <summary>
        /// The URL of the image (.gif and .jpg formats only). (Default value is http://static.ak.facebook.com/images/spacer.gif; Note that this renders the Flash object unusable and invisible on profile pages, if no height/width parameters are set..) 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The URL of the image (.gif and .jpg formats only). (Default value is http://static.ak.facebook.com/images/spacer.gif; Note that this renders the Flash object unusable and invisible on profile pages, if no height/width parameters are set..) ")]
        [DefaultValue(false)]
        public string ImgSrc
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates the width of the swf.  
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates the width of the swf. ")]
        [DefaultValue(0)]
        public int Width
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates the height of the swf.  
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates the height of the swf. ")]
        [DefaultValue(0)]
        public int Height
        {
            get;
            set;
        }
        /// <summary>
        /// The style attribute for the image. 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The style attribute for the image. ")]
        public string ImgStyle
        {
            get;
            set;
        }
        /// <summary>
        /// The class attribute for the image. 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The class attribute for the image.")] 
        public string ImgClass
        {
            get;
            set;
        }
        /// <summary>
        /// The URL-encoded Flash variables. Also passes the fb_sig_ values as described in the section on Forms. 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The URL-encoded Flash variables. Also passes the fb_sig_ values as described in the section on Forms. ")]
        public string FlashVars
        {
            get;
            set;
        }
        /// <summary>
        /// The hex-encoded background color for the Flash object. By default, a Flash object's background defaults to transparent, so if you want a background color, specify one for this attribute. 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The hex-encoded background color for the Flash object. By default, a Flash object's background defaults to transparent, so if you want a background color, specify one for this attribute. ")]
        public string SwfBgColor
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates whether to autoplay the Flash object (false) when allowed. false does not work in profiles for security and aesthetic reasons, except after an AJAX call. (Default value is true.) 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates whether to autoplay the Flash object (false) when allowed. false does not work in profiles for security and aesthetic reasons, except after an AJAX call. (Default value is true.) ")]
        [DefaultValue(true)]
        public bool WaitForClick
        {
            get;
            set;
        }
        private SwfSAlign _salign = SwfSAlign.None;

        /// <summary>
        /// The salign attribute from normal Flash &lt;embed&gt;.
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The salign attribute from normal Flash <embed>.")]
        [DefaultValue(SwfSAlign.None)]
        public SwfSAlign SAlign
        {
            get { return _salign; }
            set
            {
                if (!Enum.IsDefined(typeof(SwfSAlign), value))
                    throw new InvalidEnumArgumentException("SAlign", (int)value, typeof(SwfSAlign));
                _salign = value;
            }
        }
        /// <summary>
        /// Indicates whether to play the Flash object continuously.
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates whether to play the Flash object continuously.")]
        [DefaultValue(false)]
        public bool Loop
        {
            get;
            set;
        }
        private SwfQuality _quality = SwfQuality.None;

        /// <summary>
        /// Indicates the quality of the object. Specify best, high, medium or low. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates the quality of the object. Specify best, high, medium or low. ")]
        [DefaultValue(SwfQuality.None)]
        public SwfQuality Quality
        {
            get { return _quality; }
            set
            {
                if (!Enum.IsDefined(typeof(SwfQuality), value))
                    throw new InvalidEnumArgumentException("Quality", (int)value, typeof(SwfQuality));
                _quality = value;
            }
        }

        private SwfScale _scale = SwfScale.None;

        /// <summary>
        /// The scaling to apply to the object. Specify showall, noborder, exactfit
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The scaling to apply to the object. Specify showall, noborder, exactfit")]
        [DefaultValue(SwfScale.None)]
        public SwfScale Scale
        {
            get { return _scale; }
            set
            {
                if (!Enum.IsDefined(typeof(SwfScale), value))
                    throw new InvalidEnumArgumentException("Scale", (int)value, typeof(SwfScale));
                _scale = value;
            }
        }
        private SwfAlign _align = SwfAlign.None;

        /// <summary>
        /// Indicates how the browser aligns the obect. Specify left, center or right
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates how the browser aligns the obect. Specify left, center or right")]
        [DefaultValue(SwfAlign.None)]
        public SwfAlign Align
        {
            get { return _align; }
            set
            {
                if (!Enum.IsDefined(typeof(SwfAlign), value))
                    throw new InvalidEnumArgumentException("Align", (int)value, typeof(SwfAlign));
                _align = value;
            }
        }
        private SwfWMode _wmode = SwfWMode.Transparent;

        /// <summary>
        /// Indicates the opacity setting for the object. Specify transparent, opaque or window. (Default value is transparent.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates the opacity setting for the object. Specify transparent, opaque or window. (Default value is transparent.) ")]
        [DefaultValue(SwfWMode.Transparent)]
        public SwfWMode WMode
        {
            get { return _wmode; }
            set
            {
                if (!Enum.IsDefined(typeof(SwfWMode), value))
                    throw new InvalidEnumArgumentException("WMode", (int)value, typeof(SwfWMode));
                _wmode = value;
            }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (string.IsNullOrEmpty(SwfSrc))
                throw new MissingRequiredAttribute("SwfSrc", SwfSrc);
            else
                writer.AddAttribute("swfsrc", SwfSrc);

            if (!string.IsNullOrEmpty(ImgSrc))
                writer.AddAttribute("imgsrc", ImgSrc);
            if (Width > 0)
                writer.AddAttribute("width", Width.ToString());
            if (Height > 0)
                writer.AddAttribute("height", Height.ToString());
            if (!string.IsNullOrEmpty(ImgStyle))
                writer.AddAttribute("imgstyle", ImgStyle);
            if (!string.IsNullOrEmpty(ImgClass))
                writer.AddAttribute("imgclass", ImgClass);
            if (!string.IsNullOrEmpty(SwfBgColor))
                writer.AddAttribute("swfbgcolor", SwfBgColor);
            if (!string.IsNullOrEmpty(FlashVars))
                writer.AddAttribute("flashvars", FlashVars);
            if (!WaitForClick)
                writer.AddAttribute("waitforclick", FbmlConstants.FALSE);
            if (Loop)
                writer.AddAttribute("loop", FbmlConstants.TRUE);
            if (Quality != SwfQuality.None)
                writer.AddAttribute("quality", Quality.ToString().ToLowerInvariant());
            if (Scale != SwfScale.None)
                writer.AddAttribute("scale", Scale.ToString().ToLowerInvariant());
            if (Align != SwfAlign.None)
                writer.AddAttribute("align", Align.ToString().ToLowerInvariant());
            if (WMode != SwfWMode.Transparent)
                writer.AddAttribute("wmode", WMode.ToString().ToLowerInvariant());


            switch (SAlign)
            {
                case SwfSAlign.Bottom:
                    writer.AddAttribute("salign", "b");
                    break;
                case SwfSAlign.BottomLeft:
                    writer.AddAttribute("salign", "bl");
                    break;
                case SwfSAlign.BottomRight:
                    writer.AddAttribute("salign", "br");
                    break;
                case SwfSAlign.Left:
                    writer.AddAttribute("salign", "l");
                    break;
                case SwfSAlign.Right:
                    writer.AddAttribute("salign", "r");
                    break;
                case SwfSAlign.Top:
                    writer.AddAttribute("salign", "t");
                    break;
                case SwfSAlign.TopLeft:
                    writer.AddAttribute("salign", "tl");
                    break;
                case SwfSAlign.TopRight:
                    writer.AddAttribute("salign", "tr");
                    break;
            }
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_SWF; }
        }
    }
}

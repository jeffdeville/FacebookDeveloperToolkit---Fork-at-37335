using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Facebook.Web;
using System.Web.UI;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders a Flash-based FLV player that can stream arbitrary FLV (video/audio) files on the page
    /// </summary>
    /// <remarks>
    /// <para>The Facebook Wiki documentation for this control can be found
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:flv" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Flv runat=\"server\" />")]
    public class Flv : FbmlControl
    {
        /// <summary>
        /// The URL of the FLV file. The URL must be absolute.
        /// </summary>
        [Browsable(true)]
        [Description("The URL of the FLV File. The URL must be absolute.")]
        [DefaultValue(null)]
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        public string Src
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
        /// The name of the video. The title appears on the movie's control bar.  
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The name of the video. The title appears on the movie's control bar. ")]
        public string Title
        {
            get;
            set;
        }
        /// <summary>
        /// The URL of the image displayed behind the play button before the movie starts playing. The URL must be absolute.  
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The URL of the image displayed behind the play button before the movie starts playing. The URL must be absolute. ")] 
        public string Img
        {
            get;
            set;
        }
        /// <summary>
        /// The hex value for background color while the movie plays. (Default value is #000000.) 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The hex value for background color while the movie plays. (Default value is #000000.) ")]
        public string Color
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

        /// <inheritdoc />
        protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (string.IsNullOrEmpty(Src))
                throw new MissingRequiredAttribute("Src", Src);
            else
                writer.AddAttribute("src", Src);

            if (!string.IsNullOrEmpty(Img))
                writer.AddAttribute("img", Img);
            if (!string.IsNullOrEmpty(Color))
                writer.AddAttribute("color", Color);
            if (!string.IsNullOrEmpty(Title))
                writer.AddAttribute("title", Title);
            if (Width > 0)
                writer.AddAttribute("width", Width.ToString());
            if (Height > 0)
                writer.AddAttribute("height", Height.ToString());
            if (Scale != SwfScale.None)
                writer.AddAttribute("scale", Scale.ToString().ToLowerInvariant());
            if (Align != SwfAlign.None)
                writer.AddAttribute("align", Align.ToString().ToLowerInvariant());


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
            get { return FbmlConstants.FB_FLV; }
        }
    }
}

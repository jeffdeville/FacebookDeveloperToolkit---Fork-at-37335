using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Facebook.Web;
using System.Web.UI;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders a Flash-based audio player.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook Wiki documentation for this control can be found
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:mp3" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Mp3 runat=\"server\" />")]
    public class Mp3 : FbmlControl
    {


        /// <summary>
        /// The URL of the audio file. The URL must be absolute. 
        /// </summary>
        [Browsable(true)]
        [Description("The URL of the audio file. The URL must be absolute. ")]
        [DefaultValue(null)]
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        public string Src
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the song. 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The name of the song. ")]
        public string Title
        {
            get;
            set;
        }
        /// <summary>
        /// The name of the artist performing the song. 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The name of the artist performing the song. ")]
        public string Artist
        {
            get;
            set;
        }
        /// <summary>
        /// The title of the album.  
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The title of the album. ")]
        public string Album
        {
            get;
            set;
        }

        /// <summary>
        /// The width of the player in pixels. (Default value is 300.) 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The width of the player in pixels. (Default value is 300.) ")]
        [DefaultValue(0)]
        public int Width
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
                writer.AddAttribute(HtmlTextWriterAttribute.Src, Src);

            if (!string.IsNullOrEmpty(Title))
                writer.AddAttribute("title", Title);
            if (!string.IsNullOrEmpty(Artist))
                writer.AddAttribute("artist", Artist);
            if (!string.IsNullOrEmpty(Album))
                writer.AddAttribute("album", Album);
            if (Width > 0)
                writer.AddAttribute("width", Width.ToString());
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_MP3; }
        }
    }
}

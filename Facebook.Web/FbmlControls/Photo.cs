using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Facebook.Web;
using System.Web.UI;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders a Facebook photo.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook Wiki documentation for this control can be found
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:photo" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Photo runat=\"server\" />")]
    public class Photo : FbmlControl
    {


        /// <summary>
        /// An API-supplied pid of the photo. The pid cannot be longer than 50 characters. (The use of a pid found in the query string of a photo URL on Facebook is deprecated.) 
        /// </summary>
        [Browsable(true)]
        [Description("An API-supplied pid of the photo. The pid cannot be longer than 50 characters. (The use of a pid found in the query string of a photo URL on Facebook is deprecated.) ")]
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        public string Pid
        {
            get;
            set;
        }

        private PhotoSize _size = PhotoSize.Normal;

        /// <summary>
        /// The size of the photo to display. (Default value is normal.). Other valid values are thumb (t) (75px width), small (s) (max of 130px width or height), and normal (n) (max of 604px width or height). 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The size of the photo to display. (Default value is normal.). Other valid values are thumb (t) (75px width), small (s) (max of 130px width or height), and normal (n) (max of 604px width or height). ")]
        [DefaultValue(PhotoSize.Normal)]
        public PhotoSize Size
        {
            get { return _size; }
            set
            {
                if (!Enum.IsDefined(typeof(PhotoSize), value))
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(PhotoSize));
                _size = value;
            }
        }

        private PhotoAlign _align = PhotoAlign.Left;

        /// <summary>
        /// The size of the photo to display. (Default value is normal.). Other valid values are thumb (t) (75px width), small (s) (max of 130px width or height), and normal (n) (max of 604px width or height). 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The size of the photo to display. (Default value is normal.). Other valid values are thumb (t) (75px width), small (s) (max of 130px width or height), and normal (n) (max of 604px width or height). ")]
        [DefaultValue(PhotoAlign.Left)]
        public PhotoAlign Align
        {
            get { return _align; }
            set
            {
                if (!Enum.IsDefined(typeof(PhotoAlign), value))
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(PhotoAlign));
                _align = value;
            }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (string.IsNullOrEmpty(Pid))
                throw new MissingRequiredAttribute("Pid", Pid);
            else
                writer.AddAttribute("pid", Pid);

            if (Size != PhotoSize.Normal)
                writer.AddAttribute("size", Size.ToString().ToLowerInvariant());
            if (Align != PhotoAlign.Left)
                writer.AddAttribute("align", Align.ToString().ToLowerInvariant());
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_PHOTO; }
        }
    }
}

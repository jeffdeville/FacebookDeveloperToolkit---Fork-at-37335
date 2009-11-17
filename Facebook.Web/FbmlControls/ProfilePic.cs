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
    /// Turns into an img tag for the specified user's or Facebook Page's profile picture. 
    /// The tag itself is treated like a standard img tag, so attributes valid for img are valid with fb:profile-pic as well. So you could specify width and height settings instead of using the size attribute, for example.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:profile-pic">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:ProfilePic runat=\"server\" />")]
    public class ProfilePic : FbmlControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProfilePic() { Linked = true; FacebookLogo = true; }
        /// <summary>
        /// The user ID of the profile or Facebook Page for the picture you want to display. On a canvas page, you can also use "loggedinuser". 
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The user ID of the profile or Facebook Page for the picture you want to display. On a canvas page, you can also use \"loggedinuser\"")]
        [DefaultValue("0")]
        public string Uid
        {
            get;
            set;
        }

        private ProfilePicSize _size = ProfilePicSize.Thumb;

        /// <summary>
        /// The size of the image to display. (Default value is thumb.). Other valid values are thumb (t) (50px wide), small (s) (100px wide), normal (n) (200px wide), and square (q) (50px by 50px). Or, you can specify width and height settings instead, just like an img tag. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The size of the image to display. (Default value is thumb.). Other valid values are thumb (t) (50px wide), small (s) (100px wide), normal (n) (200px wide), and square (q) (50px by 50px). Or, you can specify width and height settings instead, just like an img tag. ")]
        [DefaultValue(ProfilePicSize.Thumb)]
        public ProfilePicSize Size
        {
            get { return _size; }
            set
            {
                if (!Enum.IsDefined(typeof(ProfilePicSize), value))
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(ProfilePicSize));
                _size = value;
            }
        }


        /// <summary>
        /// Image Height
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Image height")]
        [DefaultValue(false)]
        public int Height { get; set; }

        /// <summary>
        /// Image Height
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Image width")]
        [DefaultValue(false)]
        public int Width { get; set; }


        /// <summary>
        /// Make the image a link to the user's profile. (Default value is true.)  
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Make the image a link to the user's profile. (Default value is true.) ")]
        [DefaultValue(true)]
        public bool Linked { get; set; }

        /// <summary>
        /// (For use with Facebook Connect only.) When set to true, it returns the Facebook favicon image, which gets overlaid on top of the user's profile picture on a site.
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("(For use with Facebook Connect only.) When set to true, it returns the Facebook favicon image, which gets overlaid on top of the user's profile picture on a site.")]
        [DefaultValue(true)]
        public bool FacebookLogo { get; set; }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_PROFILE_PIC; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (Uid == "0")
                throw new MissingRequiredAttribute("Uid", Uid);

            writer.AddAttribute("uid", Uid.ToString(CultureInfo.InvariantCulture));

            if (Size != ProfilePicSize.Thumb)
                writer.AddAttribute("size", Size.ToString().ToLowerInvariant());
            if (Width>0)
                writer.AddAttribute("width", Width.ToString());
            if (Height > 0)
                writer.AddAttribute("height", Height.ToString());
            if (!Linked)
                writer.AddAttribute("linked", FbmlConstants.FALSE);
            if (FacebookLogo)
                writer.AddAttribute("facebook-logo", FbmlConstants.TRUE);

            base.AddAttributesToRender(writer);
        }
    }
}

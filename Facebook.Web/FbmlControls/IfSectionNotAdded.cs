using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;
using System.Globalization;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders the content wrapped within this tag on an application canvas page if the user hasn't added a condensed profile box or info section to her profile. Use this to encourage your users to add a box or info section to their profiles. 
    /// If the user already has a condensed profile box or an info section from the application, then nothing appears on that application's canvas page for this tag. 
    /// The content cannot be more than 100 pixels in height. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:if-section-not-added" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:IfSectionNotAdded runat=\"server\" />")]
    public class IfSectionNotAdded : ContentDisplayControl
    {

        private CanvasSection _section = CanvasSection.None;

        /// <summary>
        /// The size of the image to display. (Default value is thumb.). Other valid values are thumb (t) (50px wide), small (s) (100px wide), normal (n) (200px wide), and square (q) (50px by 50px). Or, you can specify width and height settings instead, just like an img tag. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The size of the image to display. (Default value is thumb.). Other valid values are thumb (t) (50px wide), small (s) (100px wide), normal (n) (200px wide), and square (q) (50px by 50px). Or, you can specify width and height settings instead, just like an img tag. ")]
        [DefaultValue(CanvasSection.None)]
        public CanvasSection Section
        {
            get { return _section; }
            set
            {
                if (!Enum.IsDefined(typeof(CanvasSection), value))
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(CanvasSection));
                _section = value;
            }
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_IF_SECTION_NOT_ADDED; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (Section == CanvasSection.None )
                throw new MissingRequiredAttribute("Section", Section);

            writer.AddAttribute("section", Section.ToString().ToLowerInvariant());

            base.AddAttributesToRender(writer);
        }
    }
}

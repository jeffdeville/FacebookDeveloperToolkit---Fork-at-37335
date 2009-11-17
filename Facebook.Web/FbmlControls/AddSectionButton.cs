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
    /// Renders an Add to Profile or Add to Info button (depending upon which section attribute you specify) on an application's canvas page. This button allows a user to add either a new condensed profile box to the main profile or a new application info section to the Info tab. 
    /// Important: The button appears only if the application has already called profile.setInfo or profile.setFBML and set info for that user. We recommend you call profile.setInfo after the user has entered enough data for your application to be able to make a compelling info section. 
    /// If the user already has a condensed profile box or an info section from the application, the button does not appear on that application's canvas page. 
    /// The Platform Guidelines state that you cannot incentivize users to add integration points, so you cannot know whether the user has added a profile box. However, you can use the fb:if-section-not-added tag to have Facebook render contents -- such as the fb:add-section-button tag with an explanation for the user -- only if the user hasn't added the profile box or info section. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:add-section-button" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:AddSectionButton runat=\"server\" />")]
    public class AddSectionButton : FbmlControl
    {

        private CanvasSection _size = CanvasSection.None;

        /// <summary>
        /// The size of the image to display. (Default value is thumb.). Other valid values are thumb (t) (50px wide), small (s) (100px wide), normal (n) (200px wide), and square (q) (50px by 50px). Or, you can specify width and height settings instead, just like an img tag. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The size of the image to display. (Default value is thumb.). Other valid values are thumb (t) (50px wide), small (s) (100px wide), normal (n) (200px wide), and square (q) (50px by 50px). Or, you can specify width and height settings instead, just like an img tag. ")]
        [DefaultValue(CanvasSection.None)]
        public CanvasSection Section
        {
            get { return _size; }
            set
            {
                if (!Enum.IsDefined(typeof(CanvasSection), value))
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(CanvasSection));
                _size = value;
            }
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_ADD_SECTION_BUTTON; }
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

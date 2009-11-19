using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders a standard Facebook title header.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:header" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Header runat=\"server\" />")]
    public class Header : FbmlControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Header() { Icon = true; }
        /// <summary>
        /// Toggles whether the application icon is displayed. No other icon can be displayed. (Default value is true.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Toggles whether the application icon is displayed. No other icon can be displayed. (Default value is true.) ")]
        [DefaultValue(true)]
        public bool Icon
        {
            get;
            set;
        }
        private HeaderStyle _decoration = HeaderStyle.None;

        /// <summary>
        /// Customize the appearance of the title by choosing among three styles: add_border - Adds a 1px solid #ccc border to the bottom of the header. Useful for pages with gray backgrounds below the header. no_padding - Removes the 20px of padding that is on the header by default. shorten - Removes the 20px of padding from the bottom of the header. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Customize the appearance of the title by choosing among three styles: add_border - Adds a 1px solid #ccc border to the bottom of the header. Useful for pages with gray backgrounds below the header. no_padding - Removes the 20px of padding that is on the header by default. shorten - Removes the 20px of padding from the bottom of the header. ")]
        [DefaultValue(HeaderStyle.None)]
        public HeaderStyle Decoration
        {
            get { return _decoration; }
            set
            {
                if (!Enum.IsDefined(typeof(HeaderStyle), value))
                    throw new InvalidEnumArgumentException("Decoration", (int)value, typeof(HeaderStyle));
                _decoration = value;
            }
        }
        /// <summary>
        /// The text to appear inside the button
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The text to appear inside the button")]
        public string Text
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_HEADER; }
        }
        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            switch (Decoration)
            {
                case HeaderStyle.AddBorder:
                    writer.AddAttribute("decoration", "add_border");
                    break;
                case HeaderStyle.NoPadding:
                    writer.AddAttribute("decoration", "no_padding");
                    break;
                case HeaderStyle.Shorten:
                    writer.AddAttribute("decoration", "shorten");
                    break;
            }
            if(!Icon)
                writer.AddAttribute("icon", FbmlConstants.FALSE);

            base.AddAttributesToRender(writer);
        }
        /// <inheritdoc />
        protected override void Render(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(Text))
            {
                throw new MissingRequiredAttribute("Text", Text);
            }
            FbmlTextWriter wr = new FbmlTextWriter(writer);

            AddAttributesToRender(wr);

            wr.RenderTagWithContents(ElementName, Text);
        }
    }
}


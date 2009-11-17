using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders a standard Facebook tab. Must always be a child of fb:tabs.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:tab-item" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:TabItem runat=\"server\" />")]
    public class TabItem : FbmlControl
    {
        /// <summary>
        /// the URL corresponding to the action. 
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("the URL corresponding to the action. ")]
        public string Href
        {
            get;
            set;
        }
        /// <summary>
        /// The title of the tab
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The title of the tab")]
        public string Title
        {
            get;
            set;
        }
        private TabAlign _align = TabAlign.None;

        /// <summary>
        /// Specify alignment for this tab item. (Default value is left.). Other valid value is right 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Specify alignment for this tab item. (Default value is left.). Other valid value is right ")]
        [DefaultValue(TabAlign.None)]
        public TabAlign Align
        {
            get { return _align; }
            set
            {
                if (!Enum.IsDefined(typeof(TabAlign), value))
                    throw new InvalidEnumArgumentException("Align", (int)value, typeof(TabAlign));
                _align = value;
            }
        }
        /// <summary>
        /// Indicates whether this tab item has the selected state. (Default value is false.) 
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("Indicates whether this tab item has the selected state. (Default value is false.) ")]
        public bool Selected
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_TAB_ITEM; }
        }
        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(Href))
            {
                writer.AddAttribute("href", Href);
            }
            else
            {
                throw new MissingRequiredAttribute("Href", Href);
            }
            if (!string.IsNullOrEmpty(Title))
            {
                writer.AddAttribute("title", Title);
            }
            else
            {
                throw new MissingRequiredAttribute("Title", Title);
            }
            if (Align != TabAlign.None)
            {
                writer.AddAttribute("align", Align.ToString().ToLowerInvariant());
            }
            if (Selected)
            {
                writer.AddAttribute("selected", FbmlConstants.TRUE);
            }
            base.AddAttributesToRender(writer);
        }

    }
}


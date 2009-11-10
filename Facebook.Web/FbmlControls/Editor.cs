using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Creates a form with two columns, just like the form on the edit-profile page. The children of fb:editor specify the rows of the form. For example, an fb:editor-text child adds a row with a text field in the right column. The label attribute of the fb:editor-* child specifies what text appears in the left column of that row. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:editor" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Editor runat=\"server\" />")]
    public class Editor : ContentDisplayControl
    {
        /// <summary>
        /// The URL to which the form's data is posted. The URL must be relative.
        /// </summary>
        [FbmlRequired(IsRequired=true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The URL to which the form's data is posted. The URL must be relative.")]
        public string Action
        {
            get;
            set;
        }
        /// <summary>
        /// The width of the second column (not the whole form/table), in pixels. (Default value is 425.)  
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The width of the second column (not the whole form/table), in pixels. (Default value is 425.) ")]
        [DefaultValue(0)]
        public int Width
        {
            get;
            set;
        }
        /// <summary>
        /// The width of the first column of the form/table, in pixels. (Default value is 75.). Note: This value cannot be 0 as it is ignored; use 1 instead.  
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The width of the first column of the form/table, in pixels. (Default value is 75.). Note: This value cannot be 0 as it is ignored; use 1 instead.")]
        [DefaultValue(0)]
        public int LabelWidth
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_EDITOR; }
        }
        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(Action))
                throw new MissingRequiredAttribute("Action", Action);

            writer.AddAttribute("action", Action);

            if(Width>0)
                writer.AddAttribute("width", Width.ToString());
            if (LabelWidth > 0)
                writer.AddAttribute("labelwidth", LabelWidth.ToString());

            base.AddAttributesToRender(writer);
        }
    }
}


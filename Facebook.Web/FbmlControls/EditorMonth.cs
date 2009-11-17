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
    /// Creates a form selector element displaying the month. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:editor-month" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:EditorMonth runat=\"server\" />")]
    public class EditorMonth : FbmlControl
    {
        /// <summary>
        /// The value to pre-fill (the number of the month, 1 for Jan, 2 for Feb, etc). (Default value is 1.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The value to pre-fill (the number of the month, 1 for Jan, 2 for Feb, etc). (Default value is 1.) ")]
        public int Value
        {
            get;
            set;
        }
        /// <summary>
        /// The name for the control. (Default value is month.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The name for the control. (Default value is month.) ")]
        public string Name
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_EDITOR_MONTH; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {

            if (!string.IsNullOrEmpty(Name))
                writer.AddAttribute("name", Name);
            if (Value>0)
                writer.AddAttribute("value", Value.ToString());
            base.AddAttributesToRender(writer);
        }
    }
}

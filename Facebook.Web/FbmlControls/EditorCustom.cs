using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Allows you to put any content into an fb:editor block, as long as it is valid FBML. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:editor-custom" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:EditorCustom runat=\"server\" />")]
    public class EditorCustom : ContentDisplayControl
    {
        /// <summary>
        /// The label text for left hand side.
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The label text for left hand side.")]
        public string Label
        {
            get;
            set;
        }
        /// <summary>
        /// The identifier tag for fb:editor. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The identifier tag for fb:editor. ")]
        public string Identifier
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_EDITOR_CUSTOM; }
        }
        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {

            if(!string.IsNullOrEmpty(Label))
                writer.AddAttribute("label", Label);
            if (!string.IsNullOrEmpty(Identifier))
                writer.AddAttribute("id", Identifier);

            base.AddAttributesToRender(writer);
        }
    }
}


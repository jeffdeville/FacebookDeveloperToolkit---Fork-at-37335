using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Creates a JavaScript submission mechanism for a form, which makes image or text links act as Submit buttons. Markup contained by this tag is surrounded with an &lt;a&gt; tag that includes a submit onclick action. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:submit" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Submit runat=\"server\" />")]
    public class Submit : ContentDisplayControl
    {
        /// <summary>
        /// The ID of the form to be submitted. 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The ID of the form to be submitted. ")]
        public string Form
        {
            get;
            set;
        }        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_SUBMIT; }
        }
        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(Form))
                writer.AddAttribute("form", Form);

            base.AddAttributesToRender(writer);
        }
    }
}


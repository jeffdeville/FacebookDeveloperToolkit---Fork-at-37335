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
    /// This tag renders a block of FBML into an FBML block variable instead of rendering it on the page. You can use this variable in your JavaScript with setInnerFBML. See FBJS for more information.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:js-string" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:JsString runat=\"server\" />")]
    public class JsString : ContentDisplayControl
    {
        /// <summary>
        /// A valid JavaScript identifier.  
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("A valid JavaScript identifier.  ")]
        public string Var
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_JS_STRING; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(Var))
                throw new MissingRequiredAttribute("Var", Var);

            writer.AddAttribute("var", Var);
            base.AddAttributesToRender(writer);
        }
    }
}

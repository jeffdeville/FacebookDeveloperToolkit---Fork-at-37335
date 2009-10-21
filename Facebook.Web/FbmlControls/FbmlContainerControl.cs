using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Provides services to controls that contain other FBML and HTML elements.
    /// </summary>
    public abstract class FbmlContainerControl : FbmlControl
    {
        /// <inheritdoc />
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);
            writer.RenderBeginTag(ElementName);

            RenderChildren(writer);

            writer.RenderEndTag();
        }
    }
}

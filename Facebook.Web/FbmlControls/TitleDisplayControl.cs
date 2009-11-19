using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Represents the base functionality for a control that can render one fb:title within its contents.
    /// </summary>
    public abstract class TitleDisplayControl : FbmlControl
    {
        /// <summary>
        /// The title for the fb:title tag 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The title for the fb:title element ")]
        public string Title
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected override void Render(HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);
            writer.RenderBeginTag(ElementName);
            if (!string.IsNullOrEmpty(Title))
            {
                Title title = new Title() { Text = Title };
                title.RenderControl(writer);
            }
            writer.RenderEndTag();
        }

    }
}


using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Drawing;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Represents the parent class for all FBML controls in the 
    /// <see>n:Facebook.Web.FbmlControls</see> namespace.
    /// </summary>
    [ToolboxBitmap(typeof(FbmlControl))]
    public abstract class FbmlControl : Control
    {
        private bool? _useXfbml;

        /// <summary>
        /// Initializes a new <see>FbmlControl</see>.
        /// </summary>
        protected FbmlControl()
        {

        }

        /// <summary>
        /// Specifies whether, during rendering, the control should use XFBML semantics.
        /// </summary>
        /// <remarks>
        /// <para>Setting this property to <see langword="true" /> indicates that XFBML rendering should be used,
        /// so that full beginning and end tags should be rendered.  This is required when using the Facebook
        /// Connect service.</para> 
        /// </remarks>
        [Category("Behavior")]
        [Description("Specifies whether to use XFBML semantics, which should be true for Facebook Connect applications.")]
        [DefaultValue(false)]
        public bool Xfbml
        {
            get { return _useXfbml ?? false; }
            set { _useXfbml = value; }
        }

        /// <summary>
        /// When implemented in derived classes, gets the element name to be rendered.
        /// </summary>
        [Browsable(false)]
        protected internal abstract string ElementName
        {
            get;
        }

        /// <inheritdoc />
        protected override void Render(HtmlTextWriter writer)
        {
            FbmlTextWriter wr = new FbmlTextWriter(writer);

            AddAttributesToRender(wr);

            wr.RenderFullTag(ElementName, !Xfbml);
        }

        /// <summary>
        /// When implemented in derived classes, causes the derived classes to add attributes to the rendering
        /// stream.
        /// </summary>
        /// <param name="writer">The stream writer to which content is added.</param>
        protected virtual void AddAttributesToRender(HtmlTextWriter writer)
        {

        }
    }
}

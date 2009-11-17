using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// </summary>
    internal class ConditionalTemplateContainer : FbmlTemplateControlContainer
    {
        private FbmlControl _parent;

        /// <summary>
        /// Creates a new <see>ConditionalTemplateContainer</see>.
        /// </summary>
        /// <param name="parent">The <see>ConditionalControl</see> hosting this container.</param>
        public ConditionalTemplateContainer(FbmlControl parent)
        {
            _parent = parent;
        }

        /// <summary>
        /// Gets a reference to the parent control hosting the template contained by this control.
        /// </summary>
        public FbmlControl TemplateParent
        {
            get { return _parent; }
        }
    }
}

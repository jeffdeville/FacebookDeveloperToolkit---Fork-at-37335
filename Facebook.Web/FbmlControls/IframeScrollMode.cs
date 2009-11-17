using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Specifies the type of scrolling available in an <see>Iframe</see>.
    /// </summary>
    public enum IframeScrollMode
    {
        /// <summary>
        /// Specifies that scroll bars will always appear.
        /// </summary>
        Enabled,
        /// <summary>
        /// Specifies that scrolling is disabled.
        /// </summary>
        Disabled,
        /// <summary>
        /// Specifies that Facebook should decide whether to scroll the iframe.
        /// </summary>
        Auto
    }
}

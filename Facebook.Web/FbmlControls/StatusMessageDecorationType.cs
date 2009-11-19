using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Specifies the types of error decorations available for use with the <see>Error</see> control.
    /// </summary>
    public enum StatusMessageDecorationType
    {
        /// <summary>
        /// Specifies the standard decoration style.
        /// </summary>
        Standard,
        /// <summary>
        /// Specifies that the top padding should be removed from the error message container.
        /// </summary>
        NoTopPadding,
        /// <summary>
        /// Specifies that the bottom padding should be removed from the error message container.
        /// </summary>
        NoBottomPadding,
    }
}

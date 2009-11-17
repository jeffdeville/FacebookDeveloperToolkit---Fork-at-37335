using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Specifies the type of cookie support the <see>GoogleAnalytics</see> control can use.
    /// </summary>
    public enum GoogleAnalyticsDomainCookieType
    {
        /// <summary>
        /// Specifies that Facebook should decide what type of tracking cookie to use.
        /// </summary>
        Auto,
        /// <summary>
        /// Specifies that no tracking cookie should be used.
        /// </summary>
        None,
        /// <summary>
        /// Specifies that the cookie should be tracked across the whole domain.
        /// </summary>
        Domain,
    }
}

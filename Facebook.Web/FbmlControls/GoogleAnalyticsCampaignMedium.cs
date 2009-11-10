using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Specifies the type of campaign medium used in Google Analytics tracking supported by the <see>GoogleAnalytics</see> control.
    /// </summary>
    public enum GoogleAnalyticsCampaignMedium
    {
        /// <summary>
        /// Specifies the default campaign type.
        /// </summary>
        Default,
        /// <summary>
        /// Specifies a CPC campaign.
        /// </summary>
        Cpc,
        /// <summary>
        /// Specifies a CPM campaign.
        /// </summary>
        Cpm,
        /// <summary>
        /// Specifies a link-based campaign.
        /// </summary>
        Link,
        /// <summary>
        /// Specifies an email campaign.
        /// </summary>
        Email,
        /// <summary>
        /// Specifies an organic campaign.
        /// </summary>
        Organic,
    }
}

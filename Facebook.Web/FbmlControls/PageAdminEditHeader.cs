using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// For apps that can be added to Facebook Pages, this adds a standardized edit header for canvas pages so that the Page owner can easily jump to their Page's app configuration. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:page-admin-edit-header" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:PageAdminEditHeader runat=\"server\" />")]
    public class PageAdminEditHeader : FbmlControl
    {
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_PAGE_ADMIN_EDIT_HEADER; }
        }
    }
}


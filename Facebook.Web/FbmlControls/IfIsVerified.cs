using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Facebook.Web;
using System.Globalization;
using System.Web.UI;
using System.ComponentModel.Design;
using System.Drawing.Design;
using Facebook.Utility;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Displays the enclosed content only if Facebook has verified the current user. You should use this tag with fb:else to provide alternate content in case Facebook has not verified the user. 
    /// Important 
    /// You may never disclose a user's verified state anywhere in your application or on Facebook, whether to that user or the user's friends. 
    /// You should use this tag to gate elements in your application that already are privacy-controlled. Do not use this tag to gate content like rendered user data, as a savvy user can view the source and see what's hidden. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook Wiki documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:if-is-verified" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:IfIsVerified runat=\"server\"></{0}:IfIsUser>")]
    public class IfIsVerified : ConditionalControl
    {


        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_IF_IS_VERIFIED; }
        }
    }
}

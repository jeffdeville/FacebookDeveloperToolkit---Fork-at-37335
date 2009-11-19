using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Drawing;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Restricts content to users who are age 18 or older. 
    /// You should use this tag with fb:else so you can provide alternate content in case the viewing user doesn't meet the age restriction. 
    /// Keep in mind that the content you provide to people 18 and older must still conform to the Facebook Platform Guidelines and Statement of Rights and Responsibilities
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:18-plus" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Age18Plus runat=\"server\"></{0}:Age18Plus>")]
    public sealed class Age18Plus : MinimumAgeControl
    {
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_AGE_18_PLUS; }
        }
    }
}

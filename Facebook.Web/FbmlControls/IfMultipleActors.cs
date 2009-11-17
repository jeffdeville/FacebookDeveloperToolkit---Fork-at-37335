using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Facebook.Web;
using System.Globalization;
using System.Web.UI;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Displays the enclosed content when more than one actor is involved in a Feed story. Use this tag in the templates you create with the Feed Template Console or the feed.registerTemplateBundle method. 
    /// If two or more stories are aggregated, then the {actor} token is replaced with the names of all of the users whose actions are being aggregated.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook Wiki documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:if-multiple-actors" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:IfMultipleActors runat=\"server\"></{0}:IfMultipleActors>")]
    [DefaultProperty("UserID")]
    public class IfMultipleActors : ConditionalControl
    {

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_IF_MULTIPLE_ACTORS; }
        }
    }
}

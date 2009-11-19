using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Displays a Rock the Vote &amp; CREDO Mobile registration widget inline in your application. The text inside the tags is formatted as a hyperlink. When the user clicks the link, a US voter registration form appears. When the user is done, they are prompted to share it with their friends. Then they are returned to your page. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:rock-the-vote" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:RockTheVote runat=\"server\" />")]
    public class RockTheVote : ContentDisplayControl
    {
        /// <summary>
        /// The api_key that you got from Rock the Vote to track your online voter registrations - get one at http://www.rockthevote.com/partners. If you don't have an api_key, one will be provided for you.
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The api_key that you got from Rock the Vote to track your online voter registrations - get one at http://www.rockthevote.com/partners. If you don't have an api_key, one will be provided for you.")]
        public string ApiKey
        {
            get;
            set;
        }
        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_ROCK_THE_VOTE; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {

            if (!string.IsNullOrEmpty(ApiKey))
                writer.AddAttribute("api_key", ApiKey);

            base.AddAttributesToRender(writer);
        }
    }
}


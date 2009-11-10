using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Facebook.Web;
using System.ComponentModel;
using System.Globalization;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Randomly chooses an item inside the tags based on the weights provided. 
    /// fb:random allows the developer to input a series of tags, of which one or more are shown randomly. Each item can have a weight and the tag can be specified to show more than one choice. Each option should be wrapped in an fb:random-option tag. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:random" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Random runat=\"server\" />")]
    public class Random : ContentDisplayControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Random() { Unique = true; }
        /// <summary>
        /// The number of items to choose from the random subset. (Default value is 1.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The number of items to choose from the random subset. (Default value is 1.) ")]
        [DefaultValue(0)]
        public int Pick
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates whether to force uniqueness if pick > 1. (Default value is true.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates whether to force uniqueness if pick > 1. (Default value is true.) ")]
        [DefaultValue(true)]
        public bool Unique
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_RANDOM; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {

            if (Pick>0)
                writer.AddAttribute("pick", Pick.ToString());
            if(Pick>1 && !Unique)
                writer.AddAttribute("unique", FbmlConstants.FALSE);


            base.AddAttributesToRender(writer);
        }
    }
}

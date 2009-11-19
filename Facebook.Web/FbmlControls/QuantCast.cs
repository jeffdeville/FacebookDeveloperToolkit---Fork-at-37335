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
    /// Inserts appropriate Quantcast code into a canvas page. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:quantcast" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:QuantCast runat=\"server\" />")]
    public class QuantCast : FbmlControl
    {
        /// <summary>
        /// Your Quantcast account ID.
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("Your Quantcast account ID.")]
        public string Qacct
        {
            get;
            set;
        }
        private MediaType _media = MediaType.Widget;

        /// <summary>
        /// Specifies a media type. Can be one of widget,video,audio,music, game,advertisement, or other. (Default value is "widget".) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Specifies a media type. Can be one of widget,video,audio,music, game,advertisement, or other. (Default value is \"widget\".) ")]
        [DefaultValue(MediaType.Widget)]
        public MediaType Media
        {
            get { return _media; }
            set
            {
                if (!Enum.IsDefined(typeof(MediaType), value))
                    throw new InvalidEnumArgumentException("Media", (int)value, typeof(MediaType));
                _media = value;
            }
        }
        /// <summary>
        /// Specifies one or more hierarchical media labels. Multiple labels can be separated by commas and hierarchy is represented by periods. (Default value is "".) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Specifies one or more hierarchical media labels. Multiple labels can be separated by commas and hierarchy is represented by periods. (Default value is \"\".) ")]
        public string Labels
        {
            get;
            set;
        }


        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_QUANTCAST; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if(string.IsNullOrEmpty(Qacct))
                throw new MissingRequiredAttribute("Qacct", Qacct);

            writer.AddAttribute("qacct", Qacct);

            if (Media!= MediaType.Widget)
                writer.AddAttribute("media", Media.ToString().ToLowerInvariant());
            if(!string.IsNullOrEmpty(Labels))
                writer.AddAttribute("labels", Labels);

            base.AddAttributesToRender(writer);
        }
    }
}

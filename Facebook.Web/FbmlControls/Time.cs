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
    /// Renders the date and time in the user's time zone. In order to show dates in a more flexible format, use Fb:date. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:time" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Time runat=\"server\" />")]
    public class Time : FbmlControl
    {
        /// <summary>
        /// The time to display in epoch seconds
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The time to display in epoch seconds")]
        [DefaultValue(0)]
        public long T
        {
            get;
            set;
        }
        /// <summary>
        /// The time zone in which to display t. Acceptable formats include PHP's List of Supported Timezones and +/- formats such as Etc/GMT-7. Note: Due to a bug (see below) when using a timezone in the Etc/GMT format the time is relative to the timezone. (Default value is loggedinuser's timezone.)   
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The time zone in which to display t. Acceptable formats include PHP's List of Supported Timezones and +/- formats such as Etc/GMT-7. Note: Due to a bug (see below) when using a timezone in the Etc/GMT format the time is relative to the timezone. (Default value is loggedinuser's timezone.) ")]
        public string Tz
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates whether to automatically insert prepositions as appropriate into the time, where "at" prepends the time and "on" prepends the date if it appears. (Default value is false.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Indicates whether to automatically insert prepositions as appropriate into the time, where \"at\" prepends the time and \"on\" prepends the date if it appears. (Default value is false.) ")]
        public bool Preposition
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_TIME; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if(T<0)
                throw new MissingRequiredAttribute("T", T);

            writer.AddAttribute("t", T.ToString());

            if(!string.IsNullOrEmpty(Tz))
                writer.AddAttribute("tz", Tz.ToString());
            if(Preposition)
                writer.AddAttribute("preposition", FbmlConstants.TRUE);

            base.AddAttributesToRender(writer);
        }
    }
}
